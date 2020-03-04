// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemReadOnlySpanHelper
    {
        private readonly ModuleDefinition module;
        private readonly Func<IMetadataScope> spanScope;
        private readonly ModuleImporter importer;

#if CSHARP_8_0_OR_NEWER
        private TypeReference? readOnlySpan;
#else
        private TypeReference readOnlySpan;
#endif

        public SystemReadOnlySpanHelper(ModuleDefinition module, ModuleImporter importer, Func<IMetadataScope> spanScope)
        {
            this.module = module;
            this.spanScope = spanScope;
            this.importer = importer;
        }

        public TypeReference ReadOnlySpanBase
        {
            get
            {
                if (readOnlySpan is null)
                {
                    readOnlySpan = new TypeReference("System", "ReadOnlySpan`1", module, spanScope(), true);
                    readOnlySpan.GenericParameters.Add(new GenericParameter("T", readOnlySpan));
                }

                return readOnlySpan;
            }
        }

        private readonly List<(TypeReference elementType, GenericInstanceType answerType)> memoGeneric = new List<(TypeReference elementType, GenericInstanceType answerType)>();

        public GenericInstanceType ReadOnlySpanGeneric(TypeReference type)
        {
            foreach (var (elementType, answerType) in memoGeneric)
            {
                if (ReferenceEquals(elementType, type))
                {
                    return answerType;
                }
            }

            var answer = new GenericInstanceType(ReadOnlySpanBase)
            {
                GenericArguments =
                {
                    importer.Import(type).Reference,
                },
            };
            memoGeneric.Add((type, answer));

            return answer;
        }

        private readonly List<(GenericInstanceType genericInstanceType, MethodReference ctorMethod)> memoCtorPointer = new List<(GenericInstanceType genericInstanceType, MethodReference ctorMethod)>();

        public MethodReference CtorPointer(GenericInstanceType type)
        {
            foreach (var (genericInstanceType, ctorMethod) in memoCtorPointer)
            {
                if (ReferenceEquals(genericInstanceType, type))
                {
                    return ctorMethod;
                }
            }

            var ctor = new MethodReference(".ctor", module.TypeSystem.Void, importer.Import(type).Reference)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("pointer", ParameterAttributes.None,new PointerType(module.TypeSystem.Void)),
                    new ParameterDefinition("length", ParameterAttributes.None, module.TypeSystem.Int32),
                },
            };
            memoCtorPointer.Add((type, ctor));
            return ctor;
        }
    }
}
