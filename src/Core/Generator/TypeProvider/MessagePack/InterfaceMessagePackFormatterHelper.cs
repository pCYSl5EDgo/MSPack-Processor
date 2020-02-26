// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class InterfaceMessagePackFormatterHelper
    {
        private readonly ModuleDefinition module;
        private readonly IMetadataScope messagePackScope;
        private readonly MessagePackWriterHelper writerHelper;
        private readonly MessagePackReaderHelper readerHelper;
        private readonly MessagePackSerializerOptionsHelper optionsHelper;
        private readonly ModuleImporter importer;

        private TypeReference? _IMessagePackFormatterNoGeneric;
        private TypeReference? _IMessagePackFormatterBase;

        public InterfaceMessagePackFormatterHelper(ModuleDefinition module, IMetadataScope messagePackScope, MessagePackWriterHelper writerHelper, MessagePackReaderHelper readerHelper, MessagePackSerializerOptionsHelper optionsHelper, ModuleImporter importer)
        {
            this.module = module;
            this.messagePackScope = messagePackScope;
            this.writerHelper = writerHelper;
            this.readerHelper = readerHelper;
            this.optionsHelper = optionsHelper;
            this.importer = importer;
        }

        public TypeReference IMessagePackFormatterNoGeneric => _IMessagePackFormatterNoGeneric ??= new TypeReference("MessagePack.Formatters", "IMessagePackFormatter", module, messagePackScope, false);

        public TypeReference IMessagePackFormatterBase
        {
            get
            {
                if (_IMessagePackFormatterBase == null)
                {
                    _IMessagePackFormatterBase = new TypeReference("MessagePack.Formatters", "IMessagePackFormatter`1", module, messagePackScope, false);
                    _IMessagePackFormatterBase.GenericParameters.Add(new GenericParameter("T", _IMessagePackFormatterBase));
                }

                return _IMessagePackFormatterBase;
            }
        }

        private readonly List<(TypeReference elementType, GenericInstanceType answerType)> memoGeneric = new List<(TypeReference elementType, GenericInstanceType answerType)>();

        public GenericInstanceType IMessagePackFormatterGeneric(TypeReference element)
        {
            foreach (var (elementType, answerType) in memoGeneric)
            {
                if (ReferenceEquals(element, elementType))
                {
                    return answerType;
                }
            }

            var importedElementType = importer.Import(element);
            var answer = new GenericInstanceType(IMessagePackFormatterBase)
            {
                GenericArguments = { importedElementType, },
            };
            memoGeneric.Add((element, answer));
            return answer;
        }

        private readonly List<(GenericInstanceType elementType, MethodReference answerType)> memoSerializeGeneric = new List<(GenericInstanceType elementType, MethodReference answerType)>();

        public MethodReference SerializeGeneric(GenericInstanceType genericFormatter)
        {
            foreach (var (elementType, answerType) in memoSerializeGeneric)
            {
                if (ReferenceEquals(elementType, genericFormatter))
                {
                    return answerType;
                }
            }

            var imported = importer.Import(genericFormatter);
            var answer = new MethodReference("Serialize", module.TypeSystem.Void, imported)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(writerHelper.Writer)),
                    new ParameterDefinition("value", ParameterAttributes.None, IMessagePackFormatterBase.GenericParameters[0]),
                    new ParameterDefinition("options", ParameterAttributes.None, optionsHelper.Options),
                },
            };
            memoSerializeGeneric.Add((genericFormatter, answer));
            return answer;
        }

        private readonly List<(GenericInstanceType elementType, MethodReference answerType)> memoDeserializeGeneric = new List<(GenericInstanceType elementType, MethodReference answerType)>();

        public MethodReference DeserializeGeneric(GenericInstanceType genericFormatter)
        {
            foreach (var (elementType, answerType) in memoDeserializeGeneric)
            {
                if (ReferenceEquals(elementType, genericFormatter))
                {
                    return answerType;
                }
            }

            var imported = importer.Import(genericFormatter);
            var answer = new MethodReference("Deserialize", IMessagePackFormatterBase.GenericParameters[0], imported)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(readerHelper.Reader)),
                    new ParameterDefinition("options", ParameterAttributes.None, optionsHelper.Options),
                },
            };
            memoDeserializeGeneric.Add((genericFormatter, answer));
            return answer;
        }
    }
}
