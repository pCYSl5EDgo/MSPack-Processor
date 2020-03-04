// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using System;

namespace MSPack.Processor.Core.Formatter
{
    public class GenericClassIntKeyFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly ModuleImporter importer;

        public GenericClassIntKeyFormatterImplementor(ModuleDefinition module, TypeProvider provider, ModuleImporter importer)
        {
            this.module = module;
            this.provider = provider;
            this.importer = importer;
        }

        /// <summary>
        /// Implement Serialize/Deserialize &amp; Constructor.
        /// </summary>
        /// <param name="info">information.</param>
        /// <param name="formatter">formatter type. Must be empty.</param>
        public void Implement(in GenericClassSerializationInfo info, TypeDefinition formatter)
        {
            GenericsUtility.TransplantGenericParameters(formatter, info.Definition, importer, out _, out var targetGenericInstanceType);

            formatter.Methods.Add(ConstructorUtility.GenerateDefaultConstructor(module, provider.SystemObjectHelper));
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(targetGenericInstanceType);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric.Reference));
            formatter.Interfaces.Add(new InterfaceImplementation(provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterNoGeneric));

            var shouldCallback = CallbackTestUtility.ShouldCallback(info.Definition);

            var serialize = GenerateSerialize(in info, shouldCallback, targetGenericInstanceType);
            serialize.Body.Optimize();
            formatter.Methods.Add(serialize);

            var deserialize = GenerateDeserialize(in info, shouldCallback, targetGenericInstanceType);
            deserialize.Body.Optimize();
            formatter.Methods.Add(deserialize);
        }

        #region Serialize
        private MethodDefinition GenerateSerialize(in GenericClassSerializationInfo info, bool shouldCallback, GenericInstanceType targetGenericInstanceType)
        {
            var serialize = new MethodDefinition("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, module.TypeSystem.Void)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(provider.MessagePackWriterHelper.Writer)),
                    new ParameterDefinition("value", ParameterAttributes.None, targetGenericInstanceType),
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
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Constrained, targetGenericInstanceType));
                var onBeforeSerialize = new MethodReference("OnBeforeSerialize", module.TypeSystem.Void, targetGenericInstanceType)
                {
                    HasThis = true,
                };
                processor.Append(Instruction.Create(OpCodes.Callvirt, onBeforeSerialize));
            }

            var resolverCalled = false;

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
                        SerializeField(field, processor, provider.MessagePackWriterHelper, ref resolverCalled, targetGenericInstanceType);
                        break;
                    case IndexerAccessResult.Property:
                        var propertyReference = GenericsUtility.Transplant(property.Definition.GetMethod, targetGenericInstanceType, importer);
                        SerializeProperty(property, processor, provider.MessagePackWriterHelper, propertyReference, ref resolverCalled, targetGenericInstanceType);
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

        private void SerializeProperty(in PropertySerializationInfo info, ILProcessor processor, MessagePackWriterHelper writeHelper, MethodReference transplantedPropertyReference, ref bool resolverCalled, GenericInstanceType targetGenericInstanceType)
        {
            if (!(info.BackingFieldReference is null))
            {
                SerializePropertyBackingField(info, processor, writeHelper, ref resolverCalled, GenericsUtility.Transplant(info.BackingFieldReference, targetGenericInstanceType, importer));
            }
            else if (info.IsReadable)
            {
                SerializePropertyGetter(info, processor, writeHelper, transplantedPropertyReference, ref resolverCalled);
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteNil));
            }
        }

        private void SerializePropertyGetter(in PropertySerializationInfo info, ILProcessor processor, MessagePackWriterHelper writeHelper, MethodReference transplantedPropertyReference, ref bool resolverCalled)
        {
            if (info.IsMessagePackPrimitive)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(info.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, transplantedPropertyReference));
                processor.Append(Instruction.Create(OpCodes.Call, writeHelper.WriteMessagePackPrimitive(importer.Import(info.Definition.PropertyType).Reference)));
            }
            else
            {
                SerializePropertyGetterNotPrimitive(info, processor, transplantedPropertyReference, ref resolverCalled);
            }
        }

        private void SerializePropertyGetterNotPrimitive(in PropertySerializationInfo info, ILProcessor processor, MethodReference transplantedPropertyReference, ref bool resolverCalled)
        {
            if (!resolverCalled)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
                resolverCalled = true;
            }

            processor.Append(Instruction.Create(OpCodes.Dup));

            var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(info.Definition.PropertyType);
            processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(info.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, transplantedPropertyReference));
            processor.Append(Instruction.Create(OpCodes.Ldarg_3));
            var interfaceMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(info.Definition.PropertyType);
            var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric((GenericInstanceType)interfaceMessagePackFormatterGeneric.Reference);
            processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
        }

        private void SerializePropertyBackingField(in PropertySerializationInfo info, ILProcessor processor, MessagePackWriterHelper writeHelper, ref bool resolverCalled, FieldReference propertyBackingFieldReference)
        {
            var fieldTypeReference = importer.Import(propertyBackingFieldReference.FieldType);
            if (info.IsMessagePackPrimitive)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, propertyBackingFieldReference));
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

                var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(fieldTypeReference);
                processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, propertyBackingFieldReference));
                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                var interfaceMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(propertyBackingFieldReference.FieldType);
                var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric((GenericInstanceType)interfaceMessagePackFormatterGeneric.Reference);
                processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
            }
        }

        private void SerializeField(in FieldSerializationInfo info, ILProcessor processor, MessagePackWriterHelper writeHelper, ref bool resolverCalled, GenericInstanceType targetGenericInstanceType)
        {
            var fieldReference = GenericsUtility.Transplant(info.Definition, targetGenericInstanceType, importer);
            var fieldTypeReference = fieldReference.FieldType;
            if (info.IsMessagePackPrimitive)
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
                var interfaceMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(fieldReference.FieldType);
                var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric((GenericInstanceType)interfaceMessagePackFormatterGeneric.Reference);
                processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
            }
        }
        #endregion

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in GenericClassSerializationInfo info, bool shouldCallback, GenericInstanceType targetGenericInstanceType)
        {
            var targetCtor = new MethodReference(".ctor", module.TypeSystem.Void, targetGenericInstanceType)
            {
                HasThis = true,
            };
            if (targetCtor is null)
            {
                throw new MessagePackGeneratorResolveFailedException("serializable class type should have zero param constructor. type : " + info.Definition.FullName);
            }

            var deserialize = new MethodDefinition("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, targetGenericInstanceType)
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
                        new VariableDefinition(targetGenericInstanceType), // target
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

            Assignment(processor, info, targetGenericInstanceType);

            PostProcess(processor, shouldCallback, targetGenericInstanceType);

            return deserialize;
        }

        private static void InitializeObject(ILProcessor processor, MethodReference targetCtor)
        {
            processor.Append(Instruction.Create(OpCodes.Newobj, targetCtor));
            processor.Append(Instruction.Create(OpCodes.Stloc_2));
            processor.Append(InstructionUtility.LdcI4(0));
            processor.Append(Instruction.Create(OpCodes.Stloc_1));
        }

        private void Assignment(ILProcessor processor, in GenericClassSerializationInfo info, GenericInstanceType targetGenericInstanceType)
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

            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(info, targetGenericInstanceType);
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

        private void PostProcess(ILProcessor processor, bool shouldCallback, GenericInstanceType targetGenericInstanceType)
        {
            if (shouldCallback)
            {
                CallbackAfterDeserialization(processor, targetGenericInstanceType);
            }

            DecrementDepth(processor);

            processor.Append(Instruction.Create(OpCodes.Ldloc_2));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private (Instruction[][] switchInstructions, Instruction[] defaultInstructions, Instruction[] switchTable) GenerateSwitchStatements(in GenericClassSerializationInfo info, GenericInstanceType targetGenericInstanceType)
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
                answers[i - info.MinIntKey] = FillAnswer(result, field, property, @default, targetGenericInstanceType);
            }

            var table = new Instruction[answers.Length];
            for (var i = 0; i < table.Length; i++)
            {
                table[i] = answers[i][0];
            }

            return (answers, @default, table);
        }

        private Instruction[] FillAnswer(IndexerAccessResult result, in FieldSerializationInfo field, in PropertySerializationInfo property, Instruction[] @default, GenericInstanceType targetGenericInstanceType)
        {
            switch (result)
            {
                case IndexerAccessResult.None:
                    return @default;

                case IndexerAccessResult.Field:
                    return DeserializeField(field, targetGenericInstanceType);

                case IndexerAccessResult.Property:
                    if (property.IsMessagePackPrimitive)
                    {
                        return DeserializePropertyPrimitive(property, targetGenericInstanceType, @default);
                    }
                    else
                    {
                        return DeserializePropertyNotPrimitive(property, targetGenericInstanceType, @default);
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Instruction[] DeserializePropertyNotPrimitive(PropertySerializationInfo property, GenericInstanceType targetGenericInstanceType, Instruction[] @default)
        {
            if (property.BackingFieldReference is null)
            {
                if (property.Definition.SetMethod is null)
                {
                    return @default;
                }

                return DeserializePropertyNotPrimitiveSetter(property, targetGenericInstanceType);
            }

            var backing = GenericsUtility.Transplant(property.BackingFieldReference, targetGenericInstanceType, importer);
            return DeserializePropertyNotPrimitiveBackingField(backing);
        }

        private Instruction[] DeserializePropertyNotPrimitiveBackingField(FieldReference backingField)
        {
            var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(backingField.FieldType);
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(backingField.FieldType);
            var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric((GenericInstanceType)iMessagePackFormatterGeneric.Reference);
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

        private Instruction[] DeserializePropertyNotPrimitiveSetter(in PropertySerializationInfo property, GenericInstanceType targetGenericInstanceType)
        {
            var setMethod = GenericsUtility.Transplant(property.Definition.SetMethod, targetGenericInstanceType, importer);
            var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(setMethod.Parameters[0].ParameterType);
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(setMethod.Parameters[0].ParameterType);
            var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric((GenericInstanceType)iMessagePackFormatterGeneric.Reference);
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

        private Instruction[] DeserializePropertyPrimitive(in PropertySerializationInfo property, GenericInstanceType targetGenericInstanceType, Instruction[] @default)
        {
            if (property.BackingFieldReference is null)
            {
                if (property.Definition.SetMethod is null)
                {
                    return @default;
                }

                return DeserializePropertyPrimitiveSetter(property, targetGenericInstanceType);
            }

            return DeserializePropertyPrimitiveBackingField(GenericsUtility.Transplant(property.BackingFieldReference, targetGenericInstanceType, importer));
        }

        private Instruction[] DeserializePropertyPrimitiveSetter(in PropertySerializationInfo property, GenericInstanceType targetGenericInstanceType)
        {
            var setMethod = GenericsUtility.Transplant(property.Definition.SetMethod, targetGenericInstanceType, importer);
            return new[]
            {
                Instruction.Create(OpCodes.Ldloc_2),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(setMethod.Parameters[0].ParameterType)),
                Instruction.Create(property.CanCallSet ? OpCodes.Call : OpCodes.Callvirt, setMethod),
            };
        }

        private Instruction[] DeserializePropertyPrimitiveBackingField(FieldReference transplantedBackingFieldReference)
        {
            return new[]
            {
                Instruction.Create(OpCodes.Ldloc_2),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(transplantedBackingFieldReference.FieldType)),
                Instruction.Create(OpCodes.Stfld, transplantedBackingFieldReference),
            };
        }

        private Instruction[] DeserializeField(in FieldSerializationInfo field, GenericInstanceType targetGenericInstanceType)
        {
            var storeFieldReference = GenericsUtility.Transplant(field.Definition, targetGenericInstanceType, importer);
            if (field.IsMessagePackPrimitive)
            {
                return DeserializeFieldPrimitive(storeFieldReference);
            }

            return DeserializeFieldNotPrimitive(storeFieldReference);
        }

        private Instruction[] DeserializeFieldPrimitive(FieldReference storeFieldReference)
        {
            return new[]
            {
                Instruction.Create(OpCodes.Ldloc_2),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(storeFieldReference.FieldType)),
                Instruction.Create(OpCodes.Stfld, storeFieldReference),
            };
        }

        private Instruction[] DeserializeFieldNotPrimitive(FieldReference storeFieldReference)
        {
            var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(storeFieldReference.FieldType);
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(storeFieldReference.FieldType);
            var deserializeGeneric = provider.InterfaceMessagePackFormatterHelper.DeserializeGeneric((GenericInstanceType)iMessagePackFormatterGeneric.Reference);
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

        private void CallbackAfterDeserialization(ILProcessor processor, GenericInstanceType targetGenericInstanceType)
        {
            var onAfterDeserialize = new MethodReference("OnAfterDeserialize", module.TypeSystem.Void, targetGenericInstanceType)
            {
                HasThis = true,
            };
            processor.Append(Instruction.Create(OpCodes.Ldloc_2));
            processor.Append(Instruction.Create(OpCodes.Constrained, targetGenericInstanceType));
            processor.Append(Instruction.Create(OpCodes.Callvirt, GenericsUtility.Transplant(onAfterDeserialize, targetGenericInstanceType, importer)));
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
