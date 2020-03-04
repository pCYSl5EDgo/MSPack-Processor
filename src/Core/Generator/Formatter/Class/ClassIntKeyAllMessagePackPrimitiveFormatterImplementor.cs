// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using System;
using System.Linq;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class ClassIntKeyAllMessagePackPrimitiveFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;

        public ClassIntKeyAllMessagePackPrimitiveFormatterImplementor(ModuleDefinition module, TypeProvider provider)
        {
            this.module = module;
            this.provider = provider;
        }

        /// <summary>
        /// Implement Serialize/Deserialize &amp; Constructor.
        /// </summary>
        /// <param name="info">information.</param>
        /// <param name="formatter">formatter type. Must be empty.</param>
        public void Implement(in ClassSerializationInfo info, TypeDefinition formatter)
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
        private MethodDefinition GenerateSerialize(in ClassSerializationInfo info, bool shouldCallback)
        {
            var serialize = new MethodDefinition("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, module.TypeSystem.Void)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(provider.MessagePackWriterHelper.Writer)),
                    new ParameterDefinition("value", ParameterAttributes.None, provider.Importer.Import(info.Definition)),
                    new ParameterDefinition("options", ParameterAttributes.None, provider.MessagePackSerializerOptionsHelper.Options),
                },
            };

            var processor = serialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));

            var notNullWriteArrayHeader = Instruction.Create(OpCodes.Ldarg_1);

            processor.Append(Instruction.Create(OpCodes.Brtrue_S, notNullWriteArrayHeader));

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            processor.Append(Instruction.Create(OpCodes.Ret));

            processor.Append(notNullWriteArrayHeader);
            processor.Append(InstructionUtility.LdcI4(info.MaxIntKey + 1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteArrayHeaderInt));

            if (shouldCallback)
            {
                if (!CallbackTestUtility.NoOperationInBeforeSerializationCallback(info.Definition, out var callback))
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                    processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
                }
            }

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
                        SerializeField(field, processor);

                        break;
                    case IndexerAccessResult.Property:
                        SerializeProperty(property, processor);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            processor.Append(Instruction.Create(OpCodes.Ret));

            return serialize;
        }

        private void SerializeField(FieldSerializationInfo field, ILProcessor processor)
        {
            var fieldReference = provider.Importer.Import(field.Definition);
            var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(fieldTypeReference)));
        }

        private void SerializeProperty(PropertySerializationInfo property, ILProcessor processor)
        {
            var backingField = property.BackingFieldReference;
            if (!(backingField is null))
            {
                SerializeBackingField(processor, backingField);
            }
            else if (property.IsReadable)
            {
                var propertyReference = provider.Importer.Import(property.Definition.GetMethod);
                var propertyTypeReference = provider.Importer.Import(property.Definition.PropertyType);
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(property.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, propertyReference));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(propertyTypeReference)));
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            }
        }

        private void SerializeBackingField(ILProcessor processor, FieldReference backingField)
        {
            var fieldReference = provider.Importer.Import(backingField);
            var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(fieldTypeReference)));
        }

        #endregion

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in ClassSerializationInfo info, bool shouldCallback)
        {
            var target = provider.Importer.Import(info.Definition);
            var targetCtor = provider.Importer.Import(info.Definition.Methods.FirstOrDefault(x => x.Name == ".ctor" && x.Parameters.Count == 0 && x.HasThis));
            if (targetCtor is null)
            {
                throw new MessagePackGeneratorResolveFailedException("serializable class type should have zero param constructor. type : " + info.Definition.FullName);
            }

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
                        new VariableDefinition(target), // target
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
            ReadArrayHeader(processor);
            InitializeObject(processor, targetCtor);

            Assignment(processor, info);

            PostProcess(processor, info, shouldCallback);

            return deserialize;
        }

        private void Assignment(ILProcessor processor, in ClassSerializationInfo info)
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

            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(info);
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

        private (Instruction[][] switchInstructions, Instruction[] defaultInstructions, Instruction[] switchTable) GenerateSwitchStatements(in ClassSerializationInfo info)
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
                answers[i - info.MinIntKey] = FillAnswer(result, field, property, @default);
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

        private static void InitializeObject(ILProcessor processor, MethodReference targetCtor)
        {
            processor.Append(Instruction.Create(OpCodes.Newobj, targetCtor));
            processor.Append(Instruction.Create(OpCodes.Stloc_2));
            processor.Append(InstructionUtility.LdcI4(0));
            processor.Append(Instruction.Create(OpCodes.Stloc_1));
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
