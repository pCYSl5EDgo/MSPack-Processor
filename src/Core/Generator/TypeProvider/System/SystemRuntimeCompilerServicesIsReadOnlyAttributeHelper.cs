// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper
    {
        private readonly ModuleDefinition module;

        private TypeReference? isReadOnlyAttribute;
        private MethodReference? ctor;

        public SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference IsReadOnlyAttribute => isReadOnlyAttribute ??= new TypeReference("System.Runtime.CompilerServices", "IsReadOnlyAttribute", module, module.TypeSystem.CoreLibrary, false);

        public MethodReference Ctor => ctor ??= new MethodReference(".ctor", module.TypeSystem.Void, IsReadOnlyAttribute);
    }
}
