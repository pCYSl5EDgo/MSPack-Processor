// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class StructIntKeyAllMessagePackPrimitiveFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;

        public StructIntKeyAllMessagePackPrimitiveFormatterImplementor(ModuleDefinition module, TypeProvider provider)
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
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(info.Definition);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric));
            formatter.Interfaces.Add(new InterfaceImplementation(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterNoGeneric));

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
            var valueParam = new ParameterDefinition("value", ParameterAttributes.None, provider.Importer.Import(info.Definition));
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

            var intPtrVariable = default(VariableDefinition);
            for (var i = 0; i <= info.MaxIntKey; i++)
            {
                var (result, field, property) = info[i];
                SerializeEach(processor, valueParam, field, property, result, ref intPtrVariable);
            }

            processor.Append(Instruction.Create(OpCodes.Ret));

            return serialize;
        }

        private void SerializeEach(ILProcessor processor, ParameterDefinition valueParam, in FieldSerializationInfo field, in PropertySerializationInfo property, IndexerAccessResult result, ref VariableDefinition? intPtrVariable)
        {
            switch (result)
            {
                case IndexerAccessResult.None:
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
                    break;
                case IndexerAccessResult.Field:
                    SerializeField(field, processor, valueParam, ref intPtrVariable);
                    break;
                case IndexerAccessResult.Property:
                    SerializeProperty(property, processor, valueParam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SerializeField(in FieldSerializationInfo serializationInfo, ILProcessor processor, ParameterDefinition valueParam, ref VariableDefinition? intPtrVariable)
        {
            if (serializationInfo.IsFixedSizeBuffer)
            {
                FixedSizeBufferUtility.SerializeFixedSizeBuffer(
                    processor,
                    valueParam,
                    serializationInfo.Definition,
                    module,
                    provider.MessagePackWriterHelper,
                    provider.Importer,
                    serializationInfo.ElementType,
                    serializationInfo.FixedSizeBufferCount,
                    ref intPtrVariable);
            }
            else
            {
                var fieldReference = provider.Importer.Import(serializationInfo.Definition);
                var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(fieldTypeReference)));
            }
        }

        private void SerializeProperty(in PropertySerializationInfo property, ILProcessor processor, ParameterDefinition valueParam)
        {
            var backingField = property.BackingFieldReference;
            if (!(backingField is null))
            {
                var fieldReference = provider.Importer.Import(backingField);
                var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(fieldTypeReference)));
            }
            else if (property.IsReadable)
            {
                var propertyReference = provider.Importer.Import(property.Definition.GetMethod);
                var propertyTypeReference = provider.Importer.Import(property.Definition.PropertyType);
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Call, propertyReference));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(propertyTypeReference)));
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            }
        }

        private void SerializeWriteArrayHeader(ILProcessor processor, StructSerializationInfo info)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(InstructionUtility.LdcI4(info.MaxIntKey + 1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteArrayHeaderInt));
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
        #endregion

        private MethodDefinition GenerateDeserialize(in StructSerializationInfo info, bool shouldCallback)
        {
            var target = provider.Importer.Import(info.Definition);
            var targetVariable = new VariableDefinition(target);
            var deserialize = new MethodDefinition("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, target)
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

            Assignment(processor, info, targetVariable);

            PostProcess(processor, info, shouldCallback, targetVariable);

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
                    else
                    {
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(field.Definition.FieldType)),
                            Instruction.Create(OpCodes.Stfld, provider.Importer.Import(field.Definition)),
                        };
                    }

                case IndexerAccessResult.Property:
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

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EnsureDepthStep(ILProcessor processor, Instruction notNil)
        {
            processor.Append(notNil);
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Security));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSecurityHelper.DepthStep));
        }

        private void ReadArrayHeader(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadArrayHeader));
            processor.Append(Instruction.Create(OpCodes.Stloc_0));
        }

        private void PostProcess(ILProcessor processor, StructSerializationInfo info, bool shouldCallback, VariableDefinition targetVariable)
        {
            if (shouldCallback)
            {
                CallbackAfterDeserialization(info, processor, targetVariable);
            }

            DecrementDepth(processor);

            processor.Append(Instruction.Create(OpCodes.Ldloc_2));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private void CallbackAfterDeserialization(in StructSerializationInfo info, ILProcessor processor, VariableDefinition targetVariable)
        {
            if (CallbackTestUtility.NoOperationInAfterDeserializationCallback(info.Definition, out var callback))
            {
                return;
            }

            processor.Append(Instruction.Create(OpCodes.Ldloca_S, targetVariable));
            processor.Append(Instruction.Create(OpCodes.Constrained, targetVariable.VariableType));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
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
    }
}
