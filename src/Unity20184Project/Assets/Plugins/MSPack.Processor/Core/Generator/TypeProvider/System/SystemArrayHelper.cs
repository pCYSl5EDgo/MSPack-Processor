// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemArrayHelper
    {
        private readonly ModuleDefinition module;
        private readonly ModuleImporter importer;
#if CSHARP_8_0_OR_NEWER
        private MethodReference? resize;
#else
        private MethodReference resize;
#endif

        public TypeReference Array { get; }

        public SystemArrayHelper(ModuleDefinition module, ModuleImporter importer)
        {
            this.module = module;
            this.importer = importer;
            Array = new TypeReference("System", nameof(Array), module, module.TypeSystem.CoreLibrary, false);
        }

        public MethodReference ResizeBase
        {
            get
            {
                if (resize is null)
                {
                    resize = new MethodReference(nameof(Resize), module.TypeSystem.Void, Array)
                    {
                        HasThis = false,
                    };
                    var t = new GenericParameter("T", resize);
                    resize.GenericParameters.Add(t);
                    resize.Parameters.Add(new ParameterDefinition("array", ParameterAttributes.None, new ByReferenceType(new ArrayType(t))));
                    resize.Parameters.Add(new ParameterDefinition("newSize", ParameterAttributes.None, module.TypeSystem.Int32));
                }

                return resize;
            }
        }

        private readonly List<(TypeReference elementType, GenericInstanceMethod genericInstanceMethod)> memoResize = new List<(TypeReference elementType, GenericInstanceMethod genericInstanceMethod)>();

        public GenericInstanceMethod Resize(TypeReference element)
        {
            foreach (var (elementType, genericInstanceMethod) in memoResize)
            {
                if (ReferenceEquals(element, elementType))
                {
                    return genericInstanceMethod;
                }
            }

            var answer = new GenericInstanceMethod(ResizeBase)
            {
                GenericArguments =
                {
                    importer.Import(element).Reference,
                },
            };
            memoResize.Add((element, answer));
            return answer;
        }
    }
}
