﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Embed;
using MSPack.Processor.Core.Provider;
using System;
using System.Linq;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class ClassStringKeyAllMessagePackPrimitiveImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;
        private readonly AutomataEmbeddingHelper automataHelper;

        public ClassStringKeyAllMessagePackPrimitiveImplementor(ModuleDefinition module, TypeProvider provider, DataHelper dataHelper, AutomataEmbeddingHelper automataHelper)
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
        public void Implement(in ClassSerializationInfo info, TypeDefinition formatter)
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

            var getIndex = automataHelper.GetIndex(info.StringKeyValuePairs);
            var deserialize = GenerateDeserialize(in info, shouldCallback, getIndex);
            deserialize.Body.Optimize();
            formatter.Methods.Add(deserialize);
        }

        #region Serialize
        private MethodDefinition GenerateSerialize(in ClassSerializationInfo info, in bool shouldCallback)
        {
            var serialize = new MethodDefinition("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, module.TypeSystem.Void)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(provider.MessagePackWriterHelper.Writer)),
                    new ParameterDefinition("value", ParameterAttributes.None, provider.Importer.Import(info.Definition).Reference),
                    new ParameterDefinition("options", ParameterAttributes.None, provider.MessagePackSerializerOptionsHelper.Options),
                },
            };

            var processor = serialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));

            var notNullWriteMapHeader = Instruction.Create(OpCodes.Ldarg_1);

            processor.Append(Instruction.Create(OpCodes.Brtrue_S, notNullWriteMapHeader));

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            processor.Append(Instruction.Create(OpCodes.Ret));

            WriteMapHeader(processor, info, notNullWriteMapHeader);

            if (shouldCallback)
            {
                if (!CallbackTestUtility.NoOperationInBeforeSerializationCallback(info.Definition, out var callback))
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                    processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
                }
            }

            var readOnlySpanCtor = provider.SystemReadOnlySpanHelper.CtorPointerByte();

            foreach (var serializationInfo in info.FieldInfos)
            {
                WriteHead(processor, readOnlySpanCtor, serializationInfo);
                WriteElement(processor, in serializationInfo);
            }

            foreach (var serializationInfo in info.PropertyInfos)
            {
                WriteHead(processor, readOnlySpanCtor, serializationInfo);
                WriteElement(processor, in serializationInfo);
            }

            processor.Append(Instruction.Create(OpCodes.Ret));
            return serialize;
        }

        private void WriteElement(ILProcessor processor, in PropertySerializationInfo serializationInfo)
        {
            var backingField = serializationInfo.BackingFieldReference;
            if (!(backingField is null))
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, provider.Importer.Import(backingField)));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(backingField.FieldType)));
            }
            else if (serializationInfo.IsReadable)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(serializationInfo.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, provider.Importer.Import(serializationInfo.Definition.GetMethod)));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(serializationInfo.Definition.PropertyType)));
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            }
        }

        private void WriteElement(ILProcessor processor, in FieldSerializationInfo serializationInfo)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(OpCodes.Ldfld, provider.Importer.Import(serializationInfo.Definition)));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(serializationInfo.Definition.FieldType)));
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

        private void WriteMapHeader(ILProcessor processor, in ClassSerializationInfo info, Instruction notNullWriteMapHeader)
        {
            processor.Append(notNullWriteMapHeader);
            processor.Append(InstructionUtility.LdcI4(info.Count));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMapHeaderInt));
        }
        #endregion

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in ClassSerializationInfo info, in bool shouldCallback, MethodReference getIndex)
        {
            var target = provider.Importer.Import(info.Definition);
            var targetCtor = provider.Importer.Import(info.Definition.Methods.FirstOrDefault(x => x.Name == ".ctor" && x.Parameters.Count == 0 && x.HasThis));
            if (targetCtor is null)
            {
                throw new MessagePackGeneratorResolveFailedException("serializable class type should have zero param constructor. type : " + info.Definition.FullName);
            }

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
                        new VariableDefinition(module.TypeSystem.Int32), // map length
                        new VariableDefinition(module.TypeSystem.Int32), // map index
                        new VariableDefinition(target.Reference), // target
                        new VariableDefinition(provider.InterfaceFormatterResolverHelper.IFormatterResolver), // resolver
                    },
                },
            };

            var processor = deserialize.Body.GetILProcessor();

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.TryReadNil));
            var notNil = Instruction.Create(OpCodes.Ldarg_2);
            processor.Append(Instruction.Create(OpCodes.Brfalse_S, notNil));

            processor.Append(Instruction.Create(OpCodes.Ldnull));
            processor.Append(Instruction.Create(OpCodes.Ret));

            EnsureDepthStep(processor, notNil);
            ReadMapHeader(processor);
            LoadResolver(processor);
            InitializeObject(processor, targetCtor);

            Assignment(processor, in info, getIndex);

            PostProcess(processor, info, shouldCallback);

            return deserialize;
        }

        private void Assignment(ILProcessor processor, in ClassSerializationInfo info, MethodReference getIndex)
        {
            var continuousCondition = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, continuousCondition));
            var loopStart = Instruction.Create(OpCodes.Ldarg_1);
            processor.Append(loopStart);
            processor.Append(Instruction.Create(OpCodes.Call, provider.CodeGenHelpersHelper.ReadStringSpan));
            processor.Append(Instruction.Create(OpCodes.Call, getIndex));

            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(info);
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

                if (index != switchInstructions.Length - 1)
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

        private (Instruction[][] switchInstructions, Instruction[] defaultInstructions, Instruction[] switchTable) GenerateSwitchStatements(in ClassSerializationInfo info)
        {
            var answers = new Instruction[info.Count][];
            var @default = new[]
            {
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip),
            };

            var index = 0;
            foreach (var (_, (result, field, property)) in info.StringKeyValuePairs)
            {
                answers[index++] = FillAnswer(result, field, property, @default);
            }

            var table = new Instruction[answers.Length];
            for (var i = 0; i < table.Length; i++)
            {
                table[i] = answers[i][0];
            }

            return (answers, @default, table);
        }

        private Instruction[] FillAnswer(IndexerAccessResult result, in FieldSerializationInfo field, in PropertySerializationInfo property, Instruction[] @default)
        {
            switch (result)
            {
                case IndexerAccessResult.None:
                    return @default;

                case IndexerAccessResult.Field:
                    var storeFieldReference = provider.Importer.Import(field.Definition);
                    return new[]
                    {
                        Instruction.Create(OpCodes.Ldloc_2),
                        Instruction.Create(OpCodes.Ldarg_1),
                        Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(field.Definition.FieldType)),
                        Instruction.Create(OpCodes.Stfld, storeFieldReference),
                    };

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
                            Instruction.Create(OpCodes.Ldloc_2),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(field.Definition.FieldType)),
                            Instruction.Create(property.CanCallSet ? OpCodes.Call : OpCodes.Callvirt, setMethod),
                        };
                    }

                    var backingField = provider.Importer.Import(property.BackingFieldReference);
                    return new[]
                    {
                        Instruction.Create(OpCodes.Ldloc_2),
                        Instruction.Create(OpCodes.Ldarg_1),
                        Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(property.Definition.PropertyType)),
                        Instruction.Create(OpCodes.Stfld, backingField),
                    };

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

        private static void InitializeObject(ILProcessor processor, MethodReference targetCtor)
        {
            processor.Append(Instruction.Create(OpCodes.Newobj, targetCtor));
            processor.Append(Instruction.Create(OpCodes.Stloc_2));
        }

        private void PostProcess(ILProcessor processor, in ClassSerializationInfo info, bool shouldCallback)
        {
            if (shouldCallback)
            {
                CallbackAfterDeserialization(info, processor);
            }

            DecrementDepth(processor);

            processor.Append(Instruction.Create(OpCodes.Ldloc_2));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private void CallbackAfterDeserialization(in ClassSerializationInfo info, ILProcessor processor)
        {
            if (!CallbackTestUtility.NoOperationInAfterDeserializationCallback(info.Definition, out var callback))
            {
                processor.Append(Instruction.Create(OpCodes.Ldloc_2));
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
