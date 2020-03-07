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
        private readonly Func<SystemRuntimeInteropServicesInAttributeHelper> inAttributeHelperFunc;
        private readonly ModuleImporter importer;

#if CSHARP_8_0_OR_NEWER
        private TypeReference? readOnlySpan;
        private MethodReference? opEqualityByte;
        private GenericInstanceType? readOnlySpanByte;
        private MethodReference? ctorPointerByte;
        private MethodReference? sliceStartByte;
        private MethodReference? sliceStartLengthByte;
        private MethodReference? getPinnableReferenceByte;
        private MethodReference? getItemByte;
        private MethodReference? getLengthByte;
#else
        private TypeReference readOnlySpan;
        private MethodReference op_Equality_Byte;
        private GenericInstanceType readOnlySpanByte;
        private MethodReference ctorPointerByte;
        private MethodReference sliceStartByte;
        private MethodReference sliceStartLengthByte;
        private MethodReference getPinnableReferenceByte;
        private MethodReference getItemByte;
        private MethodReference getLengthByte;
#endif

        public SystemReadOnlySpanHelper(ModuleDefinition module, ModuleImporter importer, Func<IMetadataScope> spanScope, Func<SystemRuntimeInteropServicesInAttributeHelper> inAttributeHelperFunc)
        {
            this.module = module;
            this.spanScope = spanScope;
            this.inAttributeHelperFunc = inAttributeHelperFunc;
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

        public GenericInstanceType ReadOnlySpanByte()
        {
            if (readOnlySpanByte is null)
            {
                readOnlySpanByte = new GenericInstanceType(ReadOnlySpanBase)
                {
                    GenericArguments =
                    {
                        module.TypeSystem.Byte,
                    },
                };
            }

            return readOnlySpanByte;
        }

        private GenericInstanceType ReadOnlySpanGeneric(TypeReference type)
        {
            if (type.FullName == "System.Byte")
            {
                return ReadOnlySpanByte();
            }

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

        public MethodReference CtorPointerByte()
        {
            if (ctorPointerByte is null)
            {
                ctorPointerByte = new MethodReference(".ctor", module.TypeSystem.Void, ReadOnlySpanByte())
                {
                    HasThis = true,
                    Parameters =
                    {
                        new ParameterDefinition("pointer", ParameterAttributes.None,new PointerType(module.TypeSystem.Void)),
                        new ParameterDefinition("length", ParameterAttributes.None, module.TypeSystem.Int32),
                    },
                };
            }

            return ctorPointerByte;
        }

        public MethodReference OpEqualityByte()
        {
            if (opEqualityByte is null)
            {
                opEqualityByte = new MethodReference("op_Equality", module.TypeSystem.Boolean, ReadOnlySpanGeneric(module.TypeSystem.Byte))
                {
                    HasThis = false,
                    Parameters =
                    {
                        new ParameterDefinition("left", ParameterAttributes.None, ReadOnlySpanGeneric(ReadOnlySpanBase.GenericParameters[0])),
                        new ParameterDefinition("right", ParameterAttributes.None, ReadOnlySpanGeneric(ReadOnlySpanBase.GenericParameters[0])),
                    },
                };
            }

            return opEqualityByte;
        }

        public MethodReference GetItemByte()
        {
            if (getItemByte is null)
            {
                var returnType = new RequiredModifierType(inAttributeHelperFunc().InAttribute(), new ByReferenceType(ReadOnlySpanBase.GenericParameters[0]));
                getItemByte = new MethodReference("get_Item", returnType, ReadOnlySpanByte())
                {
                    HasThis = true,
                    Parameters =
                    {
                        new ParameterDefinition("index", ParameterAttributes.None, module.TypeSystem.Int32),
                    },
                };
            }

            return getItemByte;
        }

        public MethodReference GetLengthByte()
        {
            if (getLengthByte is null)
            {
                getLengthByte = new MethodReference("get_Length", module.TypeSystem.Int32, ReadOnlySpanByte())
                {
                    HasThis = true,
                };
            }

            return getLengthByte;
        }

        public MethodReference GetPinnableReferenceByte()
        {
            if (getPinnableReferenceByte is null)
            {
                var returnType = new RequiredModifierType(inAttributeHelperFunc().InAttribute(), new ByReferenceType(ReadOnlySpanBase.GenericParameters[0]));
                getPinnableReferenceByte = new MethodReference("GetPinnableReference", returnType, ReadOnlySpanByte())
                {
                    HasThis = true,
                };
            }

            return getPinnableReferenceByte;
        }

        public MethodReference SliceStartByte()
        {
            if (sliceStartByte is null)
            {
                sliceStartByte = new MethodReference("Slice", ReadOnlySpanByte(), ReadOnlySpanByte())
                {
                    HasThis = true,
                    Parameters =
                    {
                        new ParameterDefinition("start", ParameterAttributes.None, module.TypeSystem.Int32),
                    },
                };
            }

            return sliceStartByte;
        }

        public MethodReference SliceStartLengthByte()
        {
            if (sliceStartLengthByte is null)
            {
                sliceStartLengthByte = new MethodReference("Slice", ReadOnlySpanByte(), ReadOnlySpanByte())
                {
                    HasThis = true,
                    Parameters =
                    {
                        new ParameterDefinition("start", ParameterAttributes.None, module.TypeSystem.Int32),
                        new ParameterDefinition("length", ParameterAttributes.None, module.TypeSystem.Int32),
                    },
                };
            }

            return sliceStartLengthByte;
        }
    }
}
