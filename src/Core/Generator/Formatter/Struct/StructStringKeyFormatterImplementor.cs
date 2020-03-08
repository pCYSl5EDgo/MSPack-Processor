// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Embed;
using MSPack.Processor.Core.Provider;
using System;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class StructStringKeyFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;
        private readonly AutomataEmbeddingHelper automataHelper;

        public StructStringKeyFormatterImplementor(ModuleDefinition module, TypeProvider provider, DataHelper dataHelper, AutomataEmbeddingHelper automataHelper)
        {
            this.module = module;
            this.provider = provider;
            this.dataHelper = dataHelper;
            this.automataHelper = automataHelper;
        }

        /// <summary>
        /// Implement Serialize/Deserialize &amp; Constructor.
        /// </summary>
        /// <param name="info">information.</param>
        /// <param name="formatter">formatter type. Must be empty.</param>
        public void Implement(in StructSerializationInfo info, TypeDefinition formatter)
        {
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(info.Definition);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric.Reference));
            formatter.Interfaces.Add(new InterfaceImplementation(provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterNoGeneric));

            var constructor = ConstructorUtility.GenerateDefaultConstructor(module, provider.SystemObjectHelper);
            formatter.Methods.Add(constructor);

            var shouldCallback = CallbackTestUtility.ShouldCallback(info.Definition);

            var serialize = GenerateSerialize(in info, shouldCallback);
            serialize.Body.Optimize();
            formatter.Methods.Add(serialize);

            var getIndex = automataHelper.GetIndex(info.FieldInfos, info.PropertyInfos);
            var deserialize = GenerateDeserialize(in info, shouldCallback, getIndex);
            deserialize.Body.Optimize();
            formatter.Methods.Add(deserialize);
        }

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in StructSerializationInfo info, in bool shouldCallback, MethodReference getIndex)
        {
            var target = provider.Importer.Import(info.Definition);
            var targetVariable = new VariableDefinition(target.Reference);
            var deserialize = new MethodDefinition("Deserialize",
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                target.Reference)
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
                        new VariableDefinition(module.TypeSystem.Int32), // map length
                        new VariableDefinition(module.TypeSystem.Int32), // map index
                        targetVariable, // target
                        new VariableDefinition(provider.InterfaceFormatterResolverHelper.IFormatterResolver), // resolver
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
            ReadMapHeader(processor);
            LoadResolver(processor);

            Assignment(processor, info, getIndex, targetVariable);

            PostProcess(processor, info, shouldCallback, targetVariable);

            return deserialize;
        }

        private void Assignment(ILProcessor processor, in StructSerializationInfo info, MethodReference getIndex, VariableDefinition targetVariable)
        {
            var continuousCondition = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, continuousCondition));
            var loopStart = Instruction.Create(OpCodes.Ldarg_1);
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.CodeGenHelpersHelper.ReadStringSpan));
            processor.Append(Instruction.Create(OpCodes.Call, getIndex));

            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(info, targetVariable);
            processor.Append(Instruction.Create(OpCodes.Switch, switchTable));

            foreach (var instruction in defaultInstructions)
            {
                processor.Append(instruction);
            }

            var nextInstruction = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));

            var @default = defaultInstructions[0];
            for (var index = 0; index < switchInstructions.Length; index++)
            {
                var instructions = switchInstructions[index];
                if (ReferenceEquals(@default, instructions[0]))
                {
                    continue;
                }

                foreach (var instruction in instructions)
                {
                    processor.Append(instruction);
                }

                if (index != instructions.Length - 1)
                {
                    processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));
                }
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
            var answers = new Instruction[info.Count][];
            var @default = new[]
            {
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip),
            };

            var index = 0;
            foreach (var (_, (result, field, property)) in info.EnumerateStringKeyValuePairs())
            {
                answers[index++] = FillAnswer(result, field, property, @default, targetVariable);
            }

            var table = new Instruction[answers.Length];
            for (var i = 0; i < table.Length; i++)
            {
                table[i] = answers[i][0];
            }

            return (answers, @default, table);
        }

        private Instruction[] FillAnswer(IndexerAccessResult result, in FieldSerializationInfo serializationInfo, in PropertySerializationInfo property, Instruction[] @default, VariableDefinition targetVariable)
        {
            switch (result)
            {
                case IndexerAccessResult.None:
                    return @default;

                case IndexerAccessResult.Field:
                    var storeFieldReference = provider.Importer.Import(serializationInfo.Definition);
                    if (serializationInfo.IsFixedSizeBuffer)
                    {
                        return FixedSizeBufferUtility.DeserializeFixedSizeBuffer(
                            targetVariable,
                            serializationInfo.Definition,
                            module,
                            provider.MessagePackReaderHelper,
                            provider.Importer,
                            provider.SystemInvalidOperationExceptionHelper,
                            serializationInfo.ElementType,
                            serializationInfo.FixedSizeBufferCount);
                    }
                    else if (serializationInfo.IsMessagePackPrimitive)
                    {
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloca_S, targetVariable),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(serializationInfo.Definition.FieldType)),
                            Instruction.Create(OpCodes.Stfld, storeFieldReference),
                        };
                    }
                    else
                    {
                        var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(serializationInfo.Definition.FieldType);
                        var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(serializationInfo.Definition.FieldType);
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
                                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(serializationInfo.Definition.FieldType)),
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
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        private void EnsureDepthStep(ILProcessor processor, Instruction notNil)
        {
            processor.Append(notNil);
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Security));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSecurityHelper.DepthStep));
        }

        private void ReadMapHeader(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMapHeader));
            processor.Append(Instruction.Create(OpCodes.Stloc_0));
        }

        private void LoadResolver(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
            processor.Append(Instruction.Create(OpCodes.Stloc_3));
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
        #endregion

        #region Serialize
        private MethodDefinition GenerateSerialize(in StructSerializationInfo info, in bool shouldCallback)
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
            WriteMapHeader(processor, info);

            if (shouldCallback)
            {
                Preprocess(info, processor, valueParam);
            }

            var resolverCalled = false;
            var readOnlySpanCtor = provider.SystemReadOnlySpanHelper.CtorPointerByte();
            var intPtrVariable = default(VariableDefinition);

            foreach (var serializationInfo in info.FieldInfos)
            {
                WriteHead(processor, readOnlySpanCtor, serializationInfo);
                var fieldReference = provider.Importer.Import(serializationInfo.Definition);
                SerializeField(processor, in serializationInfo, fieldReference, valueParam, ref resolverCalled, ref intPtrVariable);
            }

            foreach (var serializationInfo in info.PropertyInfos)
            {
                WriteHead(processor, readOnlySpanCtor, serializationInfo);
                SerializeProperty(processor, in serializationInfo, valueParam, ref resolverCalled);
            }

            if (resolverCalled)
            {
                processor.Append(Instruction.Create(OpCodes.Pop));
            }

            processor.Append(Instruction.Create(OpCodes.Ret));
            return serialize;
        }

        private void Preprocess(in StructSerializationInfo info, ILProcessor processor, ParameterDefinition valueParam)
        {
            if (CallbackTestUtility.NoOperationInBeforeSerializationCallback(info.Definition, out var callback))
            {
                return;
            }

            processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
        }

        private void SerializeProperty(ILProcessor processor, in PropertySerializationInfo serializationInfo, ParameterDefinition valueParam, ref bool resolverCalled)
        {
            var backingField = serializationInfo.BackingFieldReference;
            if (serializationInfo.IsMessagePackPrimitive)
            {
                if (!(backingField is null))
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                    processor.Append(Instruction.Create(OpCodes.Ldfld, provider.Importer.Import(backingField)));
                    processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(backingField.FieldType)));
                }
                else if (serializationInfo.IsReadable)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                    processor.Append(Instruction.Create(OpCodes.Call, provider.Importer.Import(serializationInfo.Definition.GetMethod)));
                    processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(serializationInfo.Definition.PropertyType)));
                }
                else
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
                }
            }
            else
            {
                if (backingField is null && !serializationInfo.IsReadable)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
                    return;
                }

                if (!resolverCalled)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                    processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
                    resolverCalled = true;
                }

                processor.Append(Instruction.Create(OpCodes.Dup));
                var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(serializationInfo.Definition.PropertyType);
                processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));

                processor.Append(backingField is null
                    ? Instruction.Create(OpCodes.Call, provider.Importer.Import(serializationInfo.Definition.GetMethod))
                    : Instruction.Create(OpCodes.Ldfld, provider.Importer.Import(backingField)));

                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                var interfaceMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(serializationInfo.Definition.PropertyType);
                var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric((GenericInstanceType)interfaceMessagePackFormatterGeneric.Reference);
                processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
            }
        }

#if CSHARP_8_0_OR_NEWER
        private void SerializeField(ILProcessor processor, in FieldSerializationInfo serializationInfo, FieldReference fieldReference, ParameterDefinition valueParam, ref bool resolverCalled, ref VariableDefinition? intPtrVariable)
#else
        private void SerializeField(ILProcessor processor, in FieldSerializationInfo serializationInfo, FieldReference fieldReference, ParameterDefinition valueParam, ref bool resolverCalled, ref VariableDefinition intPtrVariable)
#endif
        {
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
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(fieldReference.FieldType)));
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

        private void WriteHead<T>(ILProcessor processor, MethodReference readOnlySpanCtor, in T serializationInfo)
            where T : struct, IMemberSerializeInfo
        {
            var embedBytes = EmbeddedStringHelper.Encode(serializationInfo.StringKey);
            if (!dataHelper.TryGetOrAdd(embedBytes, out var field))
            {
                throw new MessagePackGeneratorResolveFailedException("name should not be empty. name : " + serializationInfo.FullName);
            }

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Ldsflda, field));
            processor.Append(InstructionUtility.LdcI4(embedBytes.Length));
            processor.Append(Instruction.Create(OpCodes.Newobj, readOnlySpanCtor));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteRaw));
        }

        private void WriteMapHeader(ILProcessor processor, in StructSerializationInfo info)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(InstructionUtility.LdcI4(info.Count));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMapHeaderInt));
        }
        #endregion
    }
}
