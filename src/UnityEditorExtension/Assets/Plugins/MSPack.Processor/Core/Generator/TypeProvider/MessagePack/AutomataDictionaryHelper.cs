// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class AutomataDictionaryHelper
    {
        private readonly ModuleDefinition module;
        private readonly SystemReadOnlySpanHelper readOnlySpanHelper;

#if CSHARP_8_0_OR_NEWER
        private MethodReference? ctor;
        private MethodReference? add;
        private MethodReference? tryGetValue;
#else
        private MethodReference ctor;
        private MethodReference add;
        private MethodReference tryGetValue;
#endif

        public TypeReference AutomataDictionary { get; }

        public AutomataDictionaryHelper(ModuleDefinition module, IMetadataScope messagePackScope, SystemReadOnlySpanHelper readOnlySpanHelper)
        {
            this.module = module;
            this.readOnlySpanHelper = readOnlySpanHelper;
            AutomataDictionary = new TypeReference("MessagePack.Internal", "AutomataDictionary", module, messagePackScope, false);
        }

        public MethodReference Ctor
        {
            get
            {
                if (ctor == null)
                {
                    ctor = new MethodReference(".ctor", module.TypeSystem.Void, AutomataDictionary)
                    {
                        HasThis = true,
                    };
                }

                return ctor;
            }
        }

        public MethodReference Add
        {
            get
            {
                if (add == null)
                {
                    add = new MethodReference(nameof(Add), module.TypeSystem.Void, AutomataDictionary)
                    {
                        HasThis = true,
                        Parameters =
                        {
                            new ParameterDefinition("str", ParameterAttributes.None, module.TypeSystem.String),
                            new ParameterDefinition("value", ParameterAttributes.None, module.TypeSystem.Int32),
                        },
                    };
                }

                return add;
            }
        }

        public MethodReference TryGetValue
        {
            get
            {
                if (tryGetValue == null)
                {
                    tryGetValue = new MethodReference(nameof(TryGetValue), module.TypeSystem.Boolean, AutomataDictionary)
                    {
                        HasThis = true,
                        Parameters =
                        {
                            new ParameterDefinition("bytes", ParameterAttributes.None, readOnlySpanHelper.ReadOnlySpanGeneric(module.TypeSystem.Byte)),
                            new ParameterDefinition("value", ParameterAttributes.Out, new ByReferenceType(module.TypeSystem.Int32)),
                        },
                    };
                }

                return tryGetValue;
            }
        }
    }
}
