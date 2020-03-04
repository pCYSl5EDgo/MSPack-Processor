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
    public sealed class GenericClassStringKeyFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;
        private readonly ModuleImporter importer;

        public GenericClassStringKeyFormatterImplementor(ModuleDefinition module, TypeProvider provider, DataHelper dataHelper, ModuleImporter importer)
        {
            this.module = module;
            this.provider = provider;
            this.dataHelper = dataHelper;
            this.importer = importer;
        }

        /// <summary>
        /// Implement Serialize/Deserialize &amp; Constructor.
        /// </summary>
        /// <param name="info">information.</param>
        /// <param name="formatter">formatter type. Must be empty.</param>
        public void Implement(in GenericClassSerializationInfo info, TypeDefinition formatter)
        {
            formatter.IsBeforeFieldInit = false;
            GenericsUtility.TransplantGenericParameters(formatter, info.Definition, importer, out var genericInstanceFormatter, out var targetGenericInstanceType);

            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(targetGenericInstanceType);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric));
            formatter.Interfaces.Add(new InterfaceImplementation(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterNoGeneric));

            FieldDefinition keyMapping = new FieldDefinition(nameof(keyMapping), FieldAttributes.Private | FieldAttributes.InitOnly, provider.AutomataDictionaryHelper.AutomataDictionary);
            formatter.Fields.Add(keyMapping);

            var keyMappingGeneric = new FieldReference(keyMapping.Name, keyMapping.FieldType, genericInstanceFormatter);

            var constructor = GenerateConstructor(in info, keyMappingGeneric);
            formatter.Methods.Add(constructor);

            var shouldCallback = CallbackTestUtility.ShouldCallback(info.Definition);

            var serialize = GenerateSerialize(in info, shouldCallback, targetGenericInstanceType);
            serialize.Body.Optimize();
            formatter.Methods.Add(serialize);

            var deserialize = GenerateDeserialize(in info, shouldCallback, keyMappingGeneric, targetGenericInstanceType);
            deserialize.Body.Optimize();
            formatter.Methods.Add(deserialize);
        }

        private MethodDefinition GenerateConstructor(in GenericClassSerializationInfo info, FieldReference keyMappingGeneric)
        {
            var constructor = new MethodDefinition(
                ".ctor",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                module.TypeSystem.Void)
            {
                HasThis = true,
            };
            var processor = constructor.Body.GetILProcessor();

            processor.Append(Instruction.Create(OpCodes.Ldarg_0));
            processor.Append(Instruction.Create(OpCodes.Dup));
            processor.Append(Instruction.Create(OpCodes.Call, provider.SystemObjectHelper.Ctor));
            processor.Append(Instruction.Create(OpCodes.Newobj, provider.AutomataDictionaryHelper.Ctor));

            var index = 0;
            foreach (var (key, _) in info.EnumerateStringKeyValuePairs())
            {
                processor.Append(Instruction.Create(OpCodes.Dup));
                processor.Append(Instruction.Create(OpCodes.Ldstr, string.Intern(key)));
                processor.Append(InstructionUtility.LdcI4(index++));
                processor.Append(Instruction.Create(OpCodes.Callvirt, provider.AutomataDictionaryHelper.Add));
            }

            processor.Append(Instruction.Create(OpCodes.Stfld, keyMappingGeneric));
            processor.Append(Instruction.Create(OpCodes.Ret));

            return constructor;
        }

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in GenericClassSerializationInfo info, in bool shouldCallback, FieldReference keyMappingGeneric, GenericInstanceType targetGenericInstanceType)
        {
            var targetCtor = new MethodReference(".ctor", module.TypeSystem.Void, targetGenericInstanceType)
            {
                HasThis = true,
            };

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
                        new VariableDefinition(module.TypeSystem.Int32), // map length
                        new VariableDefinition(module.TypeSystem.Int32), // map index
                        new VariableDefinition(targetGenericInstanceType), // target
                        new VariableDefinition(provider.InterfaceFormatterResolverHelper.IFormatterResolver), // resolver
                        new VariableDefinition(module.TypeSystem.Int32), // TryGetValue destination value
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

            Assignment(processor, in info, keyMappingGeneric, targetGenericInstanceType);

            PostProcess(processor, shouldCallback, targetGenericInstanceType);

            return deserialize;
        }

        private void Assignment(ILProcessor processor, in GenericClassSerializationInfo info, FieldReference keyMappingGeneric, GenericInstanceType targetGenericInstanceType)
        {
            var continuousCondition = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, continuousCondition));
            var loopStart = Instruction.Create(OpCodes.Ldarg_0);
            processor.Append(loopStart);
            processor.Append(Instruction.Create(OpCodes.Ldfld, keyMappingGeneric));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.CodeGenHelpersHelper.ReadStringSpan));
            processor.Append(Instruction.Create(OpCodes.Ldloca_S, processor.Body.Variables[4]));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.AutomataDictionaryHelper.TryGetValue));
            var foundInstruction = Instruction.Create(OpCodes.Ldloc_S, processor.Body.Variables[4]);
            processor.Append(Instruction.Create(OpCodes.Brtrue_S, foundInstruction));

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip));
            var nextInstruction = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));

            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(info, targetGenericInstanceType);

            processor.Append(foundInstruction);
            processor.Append(Instruction.Create(OpCodes.Switch, switchTable));

            foreach (var instruction in defaultInstructions)
            {
                processor.Append(instruction);
            }

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

        private (Instruction[][] switchInstructions, Instruction[] defaultInstructions, Instruction[] switchTable) GenerateSwitchStatements(in GenericClassSerializationInfo info, GenericInstanceType targetGenericInstanceType)
        {
            var answers = new Instruction[info.KeyCount][];
            var @default = new[]
            {
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip),
            };

            var index = 0;
            foreach (var (_, (result, field, property)) in info.EnumerateStringKeyValuePairs())
            {
                answers[index++] = FillAnswer(result, field, property, @default, targetGenericInstanceType);
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
                    return DeserializeProperty(property, @default, targetGenericInstanceType);

                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        private Instruction[] DeserializeProperty(PropertySerializationInfo property, Instruction[] @default, GenericInstanceType targetGenericInstanceType)
        {
            if (property.IsMessagePackPrimitive)
            {
                return DeserializePropertyPrimitive(property, @default, targetGenericInstanceType);
            }

            return DeserializePropertyNotPrimitive(property, @default, targetGenericInstanceType);
        }

        private Instruction[] DeserializePropertyNotPrimitive(in PropertySerializationInfo property, Instruction[] @default, GenericInstanceType targetGenericInstanceType)
        {
            if (property.BackingFieldReference is null)
            {
                if (property.Definition.SetMethod is null)
                {
                    return @default;
                }

                var setMethod = GenericsUtility.Transplant(property.Definition.SetMethod, targetGenericInstanceType, importer);
                var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(setMethod.Parameters[0].ParameterType);
                var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(setMethod.Parameters[0].ParameterType);
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
                var backingField = GenericsUtility.Transplant(property.BackingFieldReference, targetGenericInstanceType, importer);
                var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(backingField.FieldType);
                var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(backingField.FieldType);
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

        private Instruction[] DeserializePropertyPrimitive(in PropertySerializationInfo property, Instruction[] @default, GenericInstanceType targetGenericInstanceType)
        {
            if (property.BackingFieldReference is null)
            {
                if (property.Definition.SetMethod is null)
                {
                    return @default;
                }

                var setMethod = GenericsUtility.Transplant(property.Definition.SetMethod, targetGenericInstanceType, importer);
                return new[]
                {
                    Instruction.Create(OpCodes.Ldloc_2),
                    Instruction.Create(OpCodes.Ldarg_1),
                    Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(setMethod.Parameters[0].ParameterType)),
                    Instruction.Create(property.CanCallSet ? OpCodes.Call : OpCodes.Callvirt, setMethod),
                };
            }

            var backingField = GenericsUtility.Transplant(property.BackingFieldReference, targetGenericInstanceType, importer);
            return new[]
            {
                Instruction.Create(OpCodes.Ldloc_2),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(backingField.FieldType)),
                Instruction.Create(OpCodes.Stfld, backingField),
            };
        }

        private Instruction[] DeserializeField(in FieldSerializationInfo field, GenericInstanceType targetGenericInstanceType)
        {
            var storeFieldReference = GenericsUtility.Transplant(field.Definition, targetGenericInstanceType, importer);
            if (field.IsMessagePackPrimitive)
            {
                return new[]
                {
                    Instruction.Create(OpCodes.Ldloc_2),
                    Instruction.Create(OpCodes.Ldarg_1),
                    Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(storeFieldReference.FieldType)),
                    Instruction.Create(OpCodes.Stfld, storeFieldReference),
                };
            }

            var formatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(storeFieldReference.FieldType);
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(storeFieldReference.FieldType);
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

        private void CallbackAfterDeserialization(ILProcessor processor, GenericInstanceType targetGenericInstanceType)
        {
            var onAfterDeserialize = new MethodReference("OnAfterDeserialize", module.TypeSystem.Void, targetGenericInstanceType)
            {
                HasThis = true,
            };
            processor.Append(Instruction.Create(OpCodes.Ldloc_2));
            processor.Append(Instruction.Create(OpCodes.Callvirt, importer.Import(onAfterDeserialize)));
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
        private MethodDefinition GenerateSerialize(in GenericClassSerializationInfo info, in bool shouldCallback, GenericInstanceType targetGenericInstanceType)
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

            var notNullWriteMapHeader = Instruction.Create(OpCodes.Ldarg_1);

            processor.Append(Instruction.Create(OpCodes.Brtrue_S, notNullWriteMapHeader));

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            processor.Append(Instruction.Create(OpCodes.Ret));

            WriteMapHeader(processor, info, notNullWriteMapHeader);

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
            var readOnlySpanByte = provider.SystemReadOnlySpanHelper.ReadOnlySpanGeneric(module.TypeSystem.Byte);
            var readOnlySpanCtor = provider.SystemReadOnlySpanHelper.CtorPointer(readOnlySpanByte);

            foreach (var serializationInfo in info.FieldInfos)
            {
                WriteHead(processor, readOnlySpanCtor, serializationInfo);
                SerializeField(in serializationInfo, processor, ref resolverCalled, targetGenericInstanceType);
            }

            foreach (var serializationInfo in info.PropertyInfos)
            {
                WriteHead(processor, readOnlySpanCtor, serializationInfo);
                SerializeProperty(in serializationInfo, processor, ref resolverCalled, targetGenericInstanceType);
            }

            if (resolverCalled)
            {
                processor.Append(Instruction.Create(OpCodes.Pop));
            }

            processor.Append(Instruction.Create(OpCodes.Ret));
            return serialize;
        }

        private void SerializeProperty(in PropertySerializationInfo serializationInfo, ILProcessor processor, ref bool resolverCalled, GenericInstanceType targetGenericInstanceType)
        {
            if (!(serializationInfo.BackingFieldReference is null))
            {
                var backingField = GenericsUtility.Transplant(serializationInfo.BackingFieldReference, targetGenericInstanceType, importer);
                SerializePropertyBackingField(serializationInfo, processor, ref resolverCalled, backingField);
            }
            else if (serializationInfo.IsReadable)
            {
                SerializePropertyGetter(serializationInfo, processor, ref resolverCalled, targetGenericInstanceType);
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            }
        }

        private void SerializePropertyGetter(in PropertySerializationInfo serializationInfo, ILProcessor processor, ref bool resolverCalled, GenericInstanceType targetGenericInstanceType)
        {
            var getMethod = GenericsUtility.Transplant(serializationInfo.Definition.GetMethod, targetGenericInstanceType, importer);
            if (serializationInfo.IsMessagePackPrimitive)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(serializationInfo.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, getMethod));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(getMethod.ReturnType)));
            }
            else
            {
                SerializePropertyGetterNotPrimitive(serializationInfo, processor, ref resolverCalled, getMethod);
            }
        }

        private void SerializePropertyGetterNotPrimitive(in PropertySerializationInfo serializationInfo, ILProcessor processor, ref bool resolverCalled, MethodReference getMethod)
        {
            if (!resolverCalled)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Resolver));
                resolverCalled = true;
            }

            processor.Append(Instruction.Create(OpCodes.Dup));
            var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(getMethod.ReturnType);
            processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));

            processor.Append(Instruction.Create(serializationInfo.CanCallGet ? OpCodes.Call : OpCodes.Callvirt, getMethod));

            processor.Append(Instruction.Create(OpCodes.Ldarg_3));
            var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(getMethod.ReturnType));
            processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
        }

        private void SerializePropertyBackingField(in PropertySerializationInfo serializationInfo, ILProcessor processor, ref bool resolverCalled, FieldReference backingField)
        {
            if (serializationInfo.IsMessagePackPrimitive)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, backingField));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(backingField.FieldType)));
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
                var getFormatterWithVerifyGeneric = provider.FormatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(backingField.FieldType);
                processor.Append(Instruction.Create(OpCodes.Call, getFormatterWithVerifyGeneric));
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, backingField));
                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(backingField.FieldType));
                processor.Append(Instruction.Create(OpCodes.Callvirt, serializeGeneric));
            }
        }

        private void SerializeField(in FieldSerializationInfo serializationInfo, ILProcessor processor, ref bool resolverCalled, GenericInstanceType targetGenericInstanceType)
        {
            var fieldReference = GenericsUtility.Transplant(serializationInfo.Definition, targetGenericInstanceType, importer);
            if (serializationInfo.IsMessagePackPrimitive)
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
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
                processor.Append(Instruction.Create(OpCodes.Ldarg_2));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Ldarg_3));
                var serializeGeneric = provider.InterfaceMessagePackFormatterHelper.SerializeGeneric(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(fieldReference.FieldType));
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

        private void WriteMapHeader(ILProcessor processor, in GenericClassSerializationInfo info, Instruction notNullWriteMapHeader)
        {
            processor.Append(notNullWriteMapHeader);
            processor.Append(InstructionUtility.LdcI4(info.KeyCount));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMapHeaderInt));
        }
        #endregion
    }
}
