// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemRuntimeInteropServicesInAttributeHelper
    {
        private readonly ModuleDefinition module;
#if CSHARP_8_0_OR_NEWER
        private TypeReference? inAttribute;
#else
        private TypeReference inAttribute;
#endif

        public SystemRuntimeInteropServicesInAttributeHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference InAttribute()
        {
            if (inAttribute is null)
            {
                inAttribute = new TypeReference("System.Runtime.InteropServices", "InAttribute", module, module.TypeSystem.CoreLibrary, false);
            }

            return inAttribute;
        }
    }
}
