﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using System;

namespace MSPack.Processor.Core.Formatter
{
    public class StructIntKeyFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;

        public StructIntKeyFormatterImplementor(ModuleDefinition module, TypeProvider provider)
        {
            this.module = module;
            this.provider = provider;
        }

        /// <summary>
        /// Implement Serialize/Deserialize &amp; Constructor.
        /// </summary>
        /// <param name="info">information.</param>
        /// <param name="formatter">formatter type. Must be empty.</param>
        public void Implement(in StructSerializationInfo info, TypeDefinition formatter)
        {
            formatter.Methods.Add(ConstructorUtility.GenerateDefaultConstructor(module, provider.SystemObjectHelper));
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(info.Definition);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric.Reference));
            formatter.Interfaces.Add(new InterfaceImplementation(provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterNoGeneric));

            var shouldCallback = CallbackTestUtility.ShouldCallback(info.Definition);

            var serialize = GenerateSerialize(in info, shouldCallback);
            serialize.Body.Optimize();
            formatter.Methods.Add(serialize);

            var deserialize = GenerateDeserialize(in info, shouldCallback);
            deserialize.Body.Optimize();
            formatter.Methods.Add(deserialize);
        }

        #region Serialize
        private MethodDefinition GenerateSerialize(in StructSerializationInfo info, bool shouldCallback)
        {
            var valueParam = new ParameterDefinition("value", ParameterAttributes.None, provider.Importer.Import(info.Definition).Reference);
            var serialize = new MethodDefinition("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, module.TypeSystem.Void)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(provider.MessagePackWriterHelper.Writer)),
                    valueParam,
                    new ParameterDefinition("options", ParameterAttributes.None, provider.MessagePackSerializerOptionsHelper.Options),
                },
            };

            var processor = serialize.Body.GetILProcessor();

            SerializeWriteArrayHeader(processor, info);

            Preprocess(info, processor, valueParam, shouldCallback);

            var resolverCalled = false;
            var intPtrVariable = default(VariableDefinition);

            for (var i = 0; i <= info.MaxIntKey; i++)
            {
                var (result, field, property) = info[i];
                switch (result)
                {
                    case IndexerAccessResult.None:
                        processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                        processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
                        break;
                    case IndexerAccessResult.Field:
                        var fieldReference = provider.Importer.Import(field.Definition);
                        SerializeField(field, processor, valueParam, fieldReference, provider.MessagePackWriterHelper, ref resolverCalled, ref intPtrVariable);
                        break;
                    case IndexerAccessResult.Property:
                        var propertyReference = provider.Importer.Import(property.Definition.GetMethod);
                        SerializeProperty(property, processor, valueParam, provider.MessagePackWriterHelper, propertyReference, ref resolverCalled);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (resolverCalled)
            {
                processor.Append(Instruction.Create(OpCodes.Pop));
            }

            processor.Append(Instruction.Create(OpCodes.Ret));

            return serialize;
        }

        private void Preprocess(in StructSerializationInfo info, ILProcessor processor, ParameterDefinition valueParam, bool shouldCallback)
        {
            if (!shouldCallback || CallbackTestUtility.NoOperationInBeforeSerializationCallback(info.Definition, out var callback))
            {
                return;
            }

            processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
            processor.Append(Instruction.Create(OpCodes.Constrained, valueParam.ParameterType));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
        }

        private void SerializeWriteArrayHeader(ILProcessor processor, StructSerializationInfo info)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(InstructionUtility.LdcI4(info.MaxIntKey + 1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteArrayHeaderInt));
        }

        private void SerializeProperty(PropertySerializationInfo property, ILProcessor processor, ParameterDefinition valueParam, MessagePackWriterHelper writeHelper, MethodReference propertyReference, ref bool resolverCalled)
        {
            var propertyBackingFieldReference = property.BackingFieldReference;
            if (!(propertyBackingFieldReference is null))
            {
                var fieldTypeReference = provider.Importer.Import(propertyBackingFieldReference.FieldType);
                var fieldReference = provider.Importer.Import(propertyBackingFieldReference);
                if (property.IsMessagePackPrimitive)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                    processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                    processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteMessagePackPrimitive(fieldTypeReference.Reference)));
                }
                else
                {
                    if (!resolverCalled)
                    {
                        processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                        processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
                        resolverCalled = true;
                    }

                    processor.Append(Instruction.Create(OpCodes.Dup));

                    var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(fieldReference.FieldType);
                    processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                    processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                    processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                    var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric((GenericInstanceType)provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(fieldReference.FieldType).Reference);
                    processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
                }
            }
            else if (property.IsReadable)
            {
                var propertyTypeReference = provider.Importer.Import(property.Definition.PropertyType);
                if (property.IsMessagePackPrimitive)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                    processor.Append(Instruction.Create(OpCodes.Call, propertyReference));
                    processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteMessagePackPrimitive(propertyTypeReference.Reference)));
                }
                else
                {
                    if (!resolverCalled)
                    {
                        processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                        processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
                        resolverCalled = true;
                    }

                    processor.Append(Instruction.Create(OpCodes.Dup));

                    var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(property.Definition.PropertyType);
                    processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                    processor.Append(Instruction.Create(OpCodes.Call, propertyReference));
                    processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                    var interfaceMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(property.Definition.PropertyType);
                    var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric((GenericInstanceType)interfaceMessagePackFormatterGeneric.Reference);
                    processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
                }
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteNil));
            }
        }

#if CSHARP_8_0_OR_NEWER
        private void SerializeField(in FieldSerializationInfo serializationInfo, ILProcessor processor, ParameterDefinition valueParam, FieldReference fieldReference, MessagePackWriterHelper writeHelper, ref bool resolverCalled, ref VariableDefinition? intPtrVariable)
#else
        private void SerializeField(in FieldSerializationInfo serializationInfo, ILProcessor processor, ParameterDefinition valueParam, FieldReference fieldReference, MessagePackWriterHelper writeHelper, ref bool resolverCalled, ref VariableDefinition intPtrVariable)
#endif
        {
            var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
            if (serializationInfo.IsFixedSizeBuffer)
            {
                FixedSizeBufferUtility.SerializeFixedSizeBuffer(
                    processor,
                    valueParam,
                    fieldReference,
                    module,
                    provider.MessagePackWriterHelper,
                    serializationInfo.ElementType,
                    serializationInfo.FixedSizeBufferCount,
                    ref intPtrVariable);
            }
            else if (serializationInfo.IsMessagePackPrimitive)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteMessagePackPrimitive(fieldTypeReference.Reference)));
            }
            else
            {
                if (!resolverCalled)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                    processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
                    resolverCalled = true;
                }

                processor.Append(Instruction.Create(OpCodes.Dup));

                var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(fieldReference.FieldType);
                processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                var interfaceMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(fieldReference.FieldType);
                var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric((GenericInstanceType)interfaceMessagePackFormatterGeneric.Reference);
                processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
            }
        }
        #endregion

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in StructSerializationInfo info, bool shouldCallback)
        {
            var target = provider.Importer.Import(info.Definition);
            var targetVariable = new VariableDefinition(target.Reference);
            var deserialize = new MethodDefinition("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, target.Reference)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(provider.MessagePackReaderHelper.Reader)),
                    new ParameterDefinition("options", ParameterAttributes.None, provider.MessagePackSerializerOptionsHelper.Options),
                },
                Body =
                {
                    InitLocals = true,
                    Variables =
                    {
                        new VariableDefinition(module.TypeSystem.Int32), // length
                        new VariableDefinition(module.TypeSystem.Int32), // index
                        targetVariable, // target
                        new VariableDefinition(provider.InterfaceFormatterResolverHelper.IFormatterResolver),
                    },
                },
            };

            var processor = deserialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.TryReadNil));

            var notNil = Instruction.Create(OpCodes.Ldarg_2);

            processor.Append(Instruction.Create(OpCodes.Brfalse_S, notNil));

            processor.Append(Instruction.Create(OpCodes.Ldstr, "typecode is null, struct not supported"));
            processor.Append(Instruction.Create(OpCodes.Newobj, provider.SystemInvalidOperationExceptionHelper.Ctor));
            processor.Append(Instruction.Create(OpCodes.Throw));

            EnsureDepthStep(processor, notNil);
            ReadArrayHeader(processor);
            LoadResolver(processor);

            Assignment(processor, info, targetVariable);

            PostProcess(processor, info, targetVariable, shouldCallback);

            return deserialize;
        }

        private void Assignment(ILProcessor processor, in StructSerializationInfo info, VariableDefinition targetVariable)
        {
            var continuousCondition = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, continuousCondition));
            var loopStart = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(loopStart);
            if (info.MinIntKey != 0)
            {
                processor.Append(InstructionUtility.LdcI4(info.MinIntKey));
                processor.Append(Instruction.Create(OpCodes.Sub));
            }

            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(info, targetVariable);
            processor.Append(Instruction.Create(OpCodes.Switch, switchTable));

            foreach (var instruction in defaultInstructions)
            {
                processor.Append(instruction);
            }

            var nextInstruction = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));

            var @default = defaultInstructions[0];
            foreach (var instructions in switchInstructions)
            {
                if (ReferenceEquals(@default, instructions[0]))
                {
                    continue;
                }

                foreach (var instruction in instructions)
                {
                    processor.Append(instruction);
                }

                processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));
            }

            processor.Append(nextInstruction);
            processor.Append(InstructionUtility.LdcI4(1));
            processor.Append(Instruction.Create(OpCodes.Add));
            processor.Append(Instruction.Create(OpCodes.Stloc_1));
            processor.Append(continuousCondition);
            processor.Append(Instruction.Create(OpCodes.Ldloc_0));
            processor.Append(Instruction.Create(OpCodes.Blt, loopStart));
        }

        private void LoadResolver(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
            processor.Append(Instruction.Create(OpCodes.Stloc_3));
        }

        private void PostProcess(ILProcessor processor, StructSerializationInfo info, VariableDefinition targetVariable, bool shouldCallback)
        {
            if (shouldCallback)
            {
                CallbackAfterDeserialization(info, processor, targetVariable);
            }

            DecrementDepth(processor);

            processor.Append(Instruction.Create(OpCodes.Ldloc_2));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private (Instruction[][] switchInstructions, Instruction[] defaultInstructions, Instruction[] switchTable) GenerateSwitchStatements(in StructSerializationInfo info, VariableDefinition targetVariable)
        {
            var answers = new Instruction[info.MaxIntKey - info.MinIntKey + 1][];
            var @default = new[]
            {
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip),
            };
            for (var i = info.MinIntKey; i <= info.MaxIntKey; i++)
            {
                var (result, field, property) = info[i];
                answers[i - info.MinIntKey] = FillAnswer(result, field, property, @default, targetVariable);
            }

            var table = new Instruction[answers.Length];
            for (var i = 0; i < table.Length; i++)
            {
                table[i] = answers[i][0];
            }

            return (answers, @default, table);
        }

        private Instruction[] FillAnswer(IndexerAccessResult result, in FieldSerializationInfo field, in PropertySerializationInfo property, Instruction[] @default, VariableDefinition targetVariable)
        {
            switch (result)
            {
                case IndexerAccessResult.None:
                    return @default;

                case IndexerAccessResult.Field:
                    var storeFieldReference = provider.Importer.Import(field.Definition);
                    if (field.IsFixedSizeBuffer)
                    {
                        return FixedSizeBufferUtility.DeserializeFixedSizeBuffer(
                            targetVariable,
                            field.Definition,
                            module,
                            provider.MessagePackReaderHelper,
                            provider.Importer,
                            provider.SystemInvalidOperationExceptionHelper,
                            field.ElementType,
                            field.FixedSizeBufferCount);
                    }
                    else if (field.IsMessagePackPrimitive)
                    {
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(field.Definition.FieldType)),
                            Instruction.Create(OpCodes.Stfld, storeFieldReference),
                        };
                    }
                    else
                    {
                        var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(field.Definition.FieldType);
                        var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(field.Definition.FieldType);
                        var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric((GenericInstanceType)iMessagePackFormatterGeneric.Reference);
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                            Instruction.Create(OpCodes.Ldloc_3),
                            Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Ldarg_2),
                            Instruction.Create(OpCodes.Callvirt, deserializeGeneric),
                            Instruction.Create(OpCodes.Stfld, storeFieldReference),
                        };
                    }

                case IndexerAccessResult.Property:
                    if (property.IsMessagePackPrimitive)
                    {
                        if (property.BackingFieldReference is null)
                        {
                            if (property.Definition.SetMethod is null)
                            {
                                goto case IndexerAccessResult.None;
                            }

                            var setMethod = provider.Importer.Import(property.Definition.SetMethod);
                            return new[]
                            {
                                Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                                Instruction.Create(OpCodes.Ldarg_1),
                                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(field.Definition.FieldType)),
                                Instruction.Create(OpCodes.Call, setMethod),
                            };
                        }

                        var backingField = provider.Importer.Import(property.BackingFieldReference);
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(property.Definition.PropertyType)),
                            Instruction.Create(OpCodes.Stfld, backingField),
                        };
                    }
                    else
                    {
                        var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(property.Definition.PropertyType);
                        var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(property.Definition.PropertyType);
                        var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric((GenericInstanceType)iMessagePackFormatterGeneric.Reference);
                        if (property.BackingFieldReference is null)
                        {
                            if (property.Definition.SetMethod is null)
                            {
                                goto case IndexerAccessResult.None;
                            }

                            var setMethod = provider.Importer.Import(property.Definition.SetMethod);
                            return new[]
                            {
                                Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                                Instruction.Create(OpCodes.Ldloc_3),
                                Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                                Instruction.Create(OpCodes.Ldarg_1),
                                Instruction.Create(OpCodes.Ldarg_2),
                                Instruction.Create(OpCodes.Callvirt, deserializeGeneric),
                                Instruction.Create(OpCodes.Call, setMethod),
                            };
                        }

                        var backingField = provider.Importer.Import(property.BackingFieldReference);
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                            Instruction.Create(OpCodes.Ldloc_3),
                            Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Ldarg_2),
                            Instruction.Create(OpCodes.Callvirt, deserializeGeneric),
                            Instruction.Create(OpCodes.Stfld, backingField),
                        };
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ReadArrayHeader(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadArrayHeader));
            processor.Append(Instruction.Create(OpCodes.Stloc_0));
        }

        private void EnsureDepthStep(ILProcessor processor, Instruction notNil)
        {
            processor.Append(notNil);
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Security));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSecurityHelper.DepthStep));
        }

        private void CallbackAfterDeserialization(in StructSerializationInfo info, ILProcessor processor, VariableDefinition targetVariable)
        {
            if (!CallbackTestUtility.NoOperationInAfterDeserializationCallback(info.Definition, out var callback))
            {
                processor.Append(Instruction.Create(OpCodes.Ldloca_S, targetVariable));
                processor.Append(Instruction.Create(OpCodes.Constrained, targetVariable.VariableType));
                processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
            }
        }

        private void DecrementDepth(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Dup));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.get_Depth));
            processor.Append(InstructionUtility.LdcI4(1));
            processor.Append(Instruction.Create(OpCodes.Sub));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.set_Depth));
        }
        #endregion
    }
}
