// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemTypeHelper
    {
        private readonly ModuleDefinition module;
        private readonly SystemRuntimeTypeHandleHelper runtimeTypeHandleHelper;
        private TypeReference? type;
        private MethodReference? getTypeFromHandle;

        public SystemTypeHelper(ModuleDefinition module, SystemRuntimeTypeHandleHelper runtimeTypeHandleHelper)
        {
            this.module = module;
            this.runtimeTypeHandleHelper = runtimeTypeHandleHelper;
        }

        public TypeReference Type => type ??= new TypeReference("System", "Type", module, module.TypeSystem.CoreLibrary, false);

        public MethodReference GetTypeFromHandle => getTypeFromHandle ??= new MethodReference(nameof(GetTypeFromHandle), Type, Type)
        {
            HasThis = false,
            Parameters =
            {
                new ParameterDefinition("handle", ParameterAttributes.None, runtimeTypeHandleHelper.RuntimeTypeHandle),
            },
        };
    }
}
