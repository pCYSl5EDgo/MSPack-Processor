// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace MSPack.Processor.Core.Formatter
{
    public class ClassIntKeyFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;

        public ClassIntKeyFormatterImplementor(ModuleDefinition module, TypeProvider provider)
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

            void WriteNil()
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            }

            WriteNil();
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

            var resolverCalled = false;

            for (var i = 0; i <= info.MaxIntKey; i++)
            {
                var (result, field, property) = info[i];
                switch (result)
                {
                    case IndexerAccessResult.None:
                        WriteNil();
                        break;
                    case IndexerAccessResult.Field:
                        var fieldReference = provider.Importer.Import(field.Definition);
                        SerializeField(field.IsMessagePackPrimitive, processor, fieldReference, provider.MessagePackWriterHelper, ref resolverCalled);
                        break;
                    case IndexerAccessResult.Property:
                        var propertyReference = provider.Importer.Import(property.Definition.GetMethod);
                        SerializeProperty(property, processor, provider.MessagePackWriterHelper, propertyReference, ref resolverCalled);
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

        private void SerializeProperty(PropertySerializationInfo property, ILProcessor processor, MessagePackWriterHelper writeHelper, MethodReference propertyReference, ref bool resolverCalled)
        {
            var propertyBackingFieldReference = property.BackingFieldReference;
            if (!(propertyBackingFieldReference is null))
            {
                SerializeField(property.IsMessagePackPrimitive, processor, provider.Importer.Import(propertyBackingFieldReference), writeHelper, ref resolverCalled);
            }
            else if (property.IsReadable)
            {
                var propertyTypeReference = provider.Importer.Import(property.Definition.PropertyType);
                if (property.IsMessagePackPrimitive)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                    processor.Append(Instruction.Create(property.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, propertyReference));
                    processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteMessagePackPrimitive(propertyTypeReference)));
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
                    processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                    processor.Append(Instruction.Create(property.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, propertyReference));
                    processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                    var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(property.Definition.PropertyType));
                    processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
                }
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteNil));
            }
        }

        private void SerializeField(bool isMessagePackPrimitive, ILProcessor processor, FieldReference fieldReference, MessagePackWriterHelper writeHelper, ref bool resolverCalled)
        {
            var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
            if (isMessagePackPrimitive)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteMessagePackPrimitive(fieldTypeReference)));
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
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(fieldReference.FieldType));
                processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
            }
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
                        new VariableDefinition(provider.InterfaceFormatterResolverHelper.IFormatterResolver),
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
            LoadResolver(processor);
            InitializeObject(processor, targetCtor);

            Assignment(processor, info);

            PostProcess(processor, info, shouldCallback);

            return deserialize;
        }

        private static void InitializeObject(ILProcessor processor, MethodReference targetCtor)
        {
            processor.Append(Instruction.Create(OpCodes.Newobj, targetCtor));
            processor.Append(Instruction.Create(OpCodes.Stloc_2));
            processor.Append(InstructionUtility.LdcI4(0));
            processor.Append(Instruction.Create(OpCodes.Stloc_1));
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

        private void LoadResolver(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
            processor.Append(Instruction.Create(OpCodes.Stloc_3));
        }

        private void PostProcess(ILProcessor processor, ClassSerializationInfo info, bool shouldCallback)
        {
            if (shouldCallback)
            {
                CallbackAfterDeserialization(info, processor);
            }

            DecrementDepth(processor);

            processor.Append(Instruction.Create(OpCodes.Ldloc_2));
            processor.Append(Instruction.Create(OpCodes.Ret));
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
                    if (field.IsMessagePackPrimitive)
                    {
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloc_2),
                            Instruction.Create(OpCodes.Ldarg_1),
                            Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(field.Definition.FieldType)),
                            Instruction.Create(OpCodes.Stfld, storeFieldReference),
                        };
                    }
                    else
                    {
                        var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(field.Definition.FieldType);
                        var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(field.Definition.FieldType);
                        var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric(iMessagePackFormatterGeneric);
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldloc_2),
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
                    }
                    else
                    {
                        if (property.BackingFieldReference is null)
                        {
                            if (property.Definition.SetMethod is null)
                            {
                                goto case IndexerAccessResult.None;
                            }

                            var setMethod = provider.Importer.Import(property.Definition.SetMethod);
                            var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(property.Definition.PropertyType);
                            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(property.Definition.PropertyType);
                            var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric(iMessagePackFormatterGeneric);
                            return new[]
                            {
                                Instruction.Create(OpCodes.Ldloc_2),
                                Instruction.Create(OpCodes.Ldloc_3),
                                Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                                Instruction.Create(OpCodes.Ldarg_1),
                                Instruction.Create(OpCodes.Ldarg_2),
                                Instruction.Create(OpCodes.Callvirt, deserializeGeneric),
                                Instruction.Create(property.CanCallSet ? OpCodes.Call : OpCodes.Callvirt, setMethod),
                            };
                        }
                        else
                        {
                            var backingField = provider.Importer.Import(property.BackingFieldReference);
                            var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(property.Definition.PropertyType);
                            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(property.Definition.PropertyType);
                            var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric(iMessagePackFormatterGeneric);
                            return new[]
                            {
                                Instruction.Create(OpCodes.Ldloc_2),
                                Instruction.Create(OpCodes.Ldloc_3),
                                Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                                Instruction.Create(OpCodes.Ldarg_1),
                                Instruction.Create(OpCodes.Ldarg_2),
                                Instruction.Create(OpCodes.Callvirt, deserializeGeneric),
                                Instruction.Create(OpCodes.Stfld, backingField),
                            };
                        }
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
