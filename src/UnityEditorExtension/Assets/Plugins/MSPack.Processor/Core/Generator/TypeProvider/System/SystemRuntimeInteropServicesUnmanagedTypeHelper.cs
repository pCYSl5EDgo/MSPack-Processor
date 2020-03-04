// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemRuntimeInteropServicesUnmanagedTypeHelper
    {
        private readonly ModuleDefinition module;
        private readonly SystemValueTypeHelper valueTypeHelper;

#if CSHARP_8_0_OR_NEWER
        private TypeReference? systemRuntimeInteropServicesUnmanagedType;
        private RequiredModifierType? modreqValueTypeUnmanaged;
#else
        private TypeReference systemRuntimeInteropServicesUnmanagedType;
        private RequiredModifierType modreqValueTypeUnmanaged;
#endif

        public SystemRuntimeInteropServicesUnmanagedTypeHelper(ModuleDefinition module, SystemValueTypeHelper valueTypeHelper)
        {
            this.module = module;
            this.valueTypeHelper = valueTypeHelper;
        }

        public TypeReference SystemRuntimeInteropServicesUnmanagedType
        {
            get
            {
                if (systemRuntimeInteropServicesUnmanagedType is null)
                {
                    systemRuntimeInteropServicesUnmanagedType = new TypeReference("System.Runtime.InteropServices", "UnmanagedType", module, module.TypeSystem.CoreLibrary, true);
                }

                return systemRuntimeInteropServicesUnmanagedType;
            }
        }

        public RequiredModifierType ModReqValueTypeUnmanaged
        {
            get
            {
                if (modreqValueTypeUnmanaged is null)
                {
                    modreqValueTypeUnmanaged = new RequiredModifierType(SystemRuntimeInteropServicesUnmanagedType, valueTypeHelper.ValueType);
                }

                return modreqValueTypeUnmanaged;
            }
        }
    }
}
