// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class EnumFormatterImplementor
    {
        private readonly TypeProvider provider;
        private readonly ModuleDefinition module;
        private readonly MessagePackWriterHelper writerHelper;
        private readonly MessagePackReaderHelper readerHelper;
        private readonly ModuleImporter importer;
        private readonly MessagePackSerializerOptionsHelper optionsHelper;

        public EnumFormatterImplementor(TypeProvider provider)
        {
            this.module = provider.Module;
            this.writerHelper = provider.MessagePackWriterHelper;
            this.readerHelper = provider.MessagePackReaderHelper;
            this.importer = provider.Importer;
            this.optionsHelper = provider.MessagePackSerializerOptionsHelper;
            this.provider = provider;
        }

        public void Implement(in EnumSerializationInfo info, TypeDefinition formatter)
        {
            formatter.Methods.Add(ConstructorUtility.GenerateDefaultConstructor(module, provider.SystemObjectHelper));
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterGeneric(info.Type);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric.Reference));
            formatter.Interfaces.Add(new InterfaceImplementation(provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterNoGeneric));

            var serialize = GenerateSerialize(in info);
            formatter.Methods.Add(serialize);

            var deserialize = GenerateDeserialize(in info);
            formatter.Methods.Add(deserialize);
        }

        private MethodDefinition GenerateDeserialize(in EnumSerializationInfo info)
        {
            var target = provider.Importer.Import(info.Type);
            var deserialize = new MethodDefinition("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, target.Reference)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(provider.MessagePackReaderHelper.Reader)),
                    new ParameterDefinition("options", ParameterAttributes.None, provider.MessagePackSerializerOptionsHelper.Options),
                },
            };

            var processor = deserialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, readerHelper.ReadMessagePackPrimitive(info.GetUnderlyingTypeReference(module))));
            processor.Append(Instruction.Create(OpCodes.Ret));

            return deserialize;
        }

        private MethodDefinition GenerateSerialize(in EnumSerializationInfo info)
        {
            var serialize = new MethodDefinition("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, module.TypeSystem.Void)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(writerHelper.Writer)),
                    new ParameterDefinition("value", ParameterAttributes.None, importer.Import(info.Type).Reference),
                    new ParameterDefinition("options", ParameterAttributes.None, optionsHelper.Options),
                },
            };

            var processor = serialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));
            processor.Append(Instruction.Create(OpCodes.Call, writerHelper.WriteMessagePackPrimitive(info.GetUnderlyingTypeReference(module))));
            processor.Append(Instruction.Create(OpCodes.Ret));

            return serialize;
        }
    }
}
