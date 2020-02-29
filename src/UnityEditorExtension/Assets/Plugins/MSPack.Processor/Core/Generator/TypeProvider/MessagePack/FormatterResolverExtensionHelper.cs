// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Provider
{
    public sealed class FormatterResolverExtensionHelper
    {
        private readonly ModuleDefinition module;
        private readonly InterfaceFormatterResolverHelper iFormatterResolverHelper;
        private readonly InterfaceMessagePackFormatterHelper iMessagePackFormatterHelper;
        private readonly ModuleImporter importer;
#if CSHARP_8_0_OR_NEWER
        private MethodReference? getFormatterWithVerifyBase;
#else
        private MethodReference getFormatterWithVerifyBase;
#endif

        public FormatterResolverExtensionHelper(ModuleDefinition module, IMetadataScope messagePackScope, InterfaceFormatterResolverHelper iFormatterResolverHelper, InterfaceMessagePackFormatterHelper iMessagePackFormatterHelper, ModuleImporter importer)
        {
            this.module = module;
            this.iFormatterResolverHelper = iFormatterResolverHelper;
            this.iMessagePackFormatterHelper = iMessagePackFormatterHelper;
            this.importer = importer;
            FormatterResolverExtension = new TypeReference("MessagePack", "FormatterResolverExtensions", module, messagePackScope, false);
        }

        public MethodReference GetFormatterWithVerifyBase
        {
            get
            {
                if (getFormatterWithVerifyBase is null)
                {
                    getFormatterWithVerifyBase = new MethodReference("GetFormatterWithVerify", module.TypeSystem.Void, FormatterResolverExtension)
                    {
                        HasThis = false,
                        Parameters =
                        {
                            new ParameterDefinition("resolver", ParameterAttributes.None, iFormatterResolverHelper.IFormatterResolver),
                        },
                    };
                    var t = new GenericParameter("T", getFormatterWithVerifyBase);
                    getFormatterWithVerifyBase.GenericParameters.Add(t);
                    getFormatterWithVerifyBase.ReturnType = iMessagePackFormatterHelper.IMessagePackFormatterGeneric(t);
                }

                return getFormatterWithVerifyBase;
            }
        }

        public TypeReference FormatterResolverExtension { get; }

        private readonly List<(TypeReference elementType, GenericInstanceMethod answerMethod)> memoGeneric = new List<(TypeReference, GenericInstanceMethod)>();

        public GenericInstanceMethod GetFormatterWithVerifyGeneric(TypeReference element)
        {
            foreach (var (elementType, answerMethod) in memoGeneric)
            {
                if (ReferenceEquals(elementType, element))
                {
                    return answerMethod;
                }
            }

            var importedElement = importer.Import(element);
            var answer = new GenericInstanceMethod(GetFormatterWithVerifyBase)
            {
                GenericArguments = { importedElement, },
            };
            memoGeneric.Add((element, answer));
            return answer;
        }
    }
}
