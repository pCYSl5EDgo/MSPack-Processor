// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemReadOnlySpanHelper
    {
        private readonly ModuleDefinition module;
        private readonly Func<IMetadataScope> spanScope;
        private readonly ModuleImporter importer;

        private TypeReference? readOnlySpan;

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
                    importer.Import(type),
                },
            };
            memoGeneric.Add((type, answer));

            return answer;
        }

        private readonly List<(GenericInstanceType genericInstanceType, MethodReference method)> memoGetLength = new List<(GenericInstanceType genericInstanceType, MethodReference method)>();

        public MethodReference get_Length(GenericInstanceType type)
        {
            foreach (var (genericInstanceType, answerMethod) in memoGetLength)
            {
                if (ReferenceEquals(genericInstanceType, type))
                {
                    return answerMethod;
                }
            }

            var method = new MethodReference("get_Length", module.TypeSystem.Int32, importer.Import(type))
            {
                HasThis = true,
            };
            memoGetLength.Add((type, method));
            return method;
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

            var ctor = new MethodReference(".ctor", module.TypeSystem.Void, importer.Import(type))
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
