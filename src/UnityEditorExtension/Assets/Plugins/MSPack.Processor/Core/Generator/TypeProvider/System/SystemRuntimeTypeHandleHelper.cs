// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemRuntimeTypeHandleHelper
    {
        private readonly ModuleDefinition module;
#if CSHARP_8_0_OR_NEWER
        private TypeReference? runtimeTypeHandle;
        private MethodReference? _get_Value;
#else
        private TypeReference runtimeTypeHandle;
        private MethodReference _get_Value;
#endif

        public SystemRuntimeTypeHandleHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference RuntimeTypeHandle
        {
            get
            {
                if (runtimeTypeHandle == null)
                {
                    runtimeTypeHandle = new TypeReference("System", "RuntimeTypeHandle", module, module.TypeSystem.CoreLibrary, true);
                }

                return runtimeTypeHandle;
            }
        }

        public MethodReference get_Value
        {
            get
            {
                if (_get_Value == null)
                {
                    _get_Value = new MethodReference("get_Value", module.TypeSystem.IntPtr, RuntimeTypeHandle)
                    {
                        HasThis = true,
                    };
                }

                return _get_Value;
            }
        }
    }
}
