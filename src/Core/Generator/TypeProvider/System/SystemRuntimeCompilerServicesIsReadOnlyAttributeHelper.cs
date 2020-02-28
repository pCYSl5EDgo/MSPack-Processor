// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper
    {
        private readonly ModuleDefinition module;

#if CSHARP_8_0_OR_NEWER
        private TypeReference? isReadOnlyAttribute;
        private MethodReference? ctor;
#else
        private TypeReference isReadOnlyAttribute;
        private MethodReference ctor;
#endif

        public SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference IsReadOnlyAttribute
        {
            get
            {
                if (isReadOnlyAttribute == null)
                {
                    isReadOnlyAttribute = new TypeReference("System.Runtime.CompilerServices", "IsReadOnlyAttribute", module, module.TypeSystem.CoreLibrary, false);
                }

                return isReadOnlyAttribute;
            }
        }

        public MethodReference Ctor
        {
            get
            {
                if (ctor == null)
                {
                    ctor = new MethodReference(".ctor", module.TypeSystem.Void, IsReadOnlyAttribute);
                }

                return ctor;
            }
        }
    }
}
