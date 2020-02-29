// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemTypeHelper
    {
        private readonly ModuleDefinition module;
        private readonly SystemRuntimeTypeHandleHelper runtimeTypeHandleHelper;
#if CSHARP_8_0_OR_NEWER
        private TypeReference? type;
        private MethodReference? getTypeFromHandle;
#else
        private TypeReference type;
        private MethodReference getTypeFromHandle;
#endif

        public SystemTypeHelper(ModuleDefinition module, SystemRuntimeTypeHandleHelper runtimeTypeHandleHelper)
        {
            this.module = module;
            this.runtimeTypeHandleHelper = runtimeTypeHandleHelper;
        }

        public TypeReference Type
        {
            get
            {
                if (type == null)
                {
                    type = new TypeReference("System", "Type", module, module.TypeSystem.CoreLibrary, false);
                }

                return type;
            }
        }

        public MethodReference GetTypeFromHandle
        {
            get
            {
                if (getTypeFromHandle == null)
                {
                    getTypeFromHandle = new MethodReference(nameof(GetTypeFromHandle), Type, Type)
                    {
                        HasThis = false,
                        Parameters =
                        {
                            new ParameterDefinition("handle", ParameterAttributes.None, runtimeTypeHandleHelper.RuntimeTypeHandle),
                        },
                    };
                }

                return getTypeFromHandle;
            }
        }
    }
}
