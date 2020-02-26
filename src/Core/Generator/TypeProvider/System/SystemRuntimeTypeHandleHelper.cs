// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemRuntimeTypeHandleHelper
    {
        private readonly ModuleDefinition module;
        private TypeReference? runtimeTypeHandle;
        private MethodReference? _get_Value;

        public SystemRuntimeTypeHandleHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference RuntimeTypeHandle => runtimeTypeHandle ??= new TypeReference("System", "RuntimeTypeHandle", module, module.TypeSystem.CoreLibrary, true);

        public MethodReference get_Value => _get_Value ??= new MethodReference("get_Value", module.TypeSystem.IntPtr, RuntimeTypeHandle)
        {
            HasThis = true,
        };
    }
}
