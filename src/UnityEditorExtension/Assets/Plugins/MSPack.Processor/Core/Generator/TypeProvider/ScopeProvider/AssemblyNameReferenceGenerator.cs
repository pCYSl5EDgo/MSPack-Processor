// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Linq;

namespace MSPack.Processor.Core.Provider
{
    public struct AssemblyNameReferenceGenerator
    {
        private const string SystemCollections = "System.Collections";
        private const string SystemRuntimeExtensions = "System.Runtime.Extensions";

        public AssemblyNameReference With(AssemblyNameReference nameReference, string name)
        {
            return new AssemblyNameReference(name, nameReference.Version)
            {
                PublicKeyToken = nameReference.PublicKeyToken,
                Culture = nameReference.Culture,
            };
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryGetDictionaryContainerNameReference(ModuleDefinition module, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out AssemblyNameReference? nameReference)
#else
        public bool TryGetDictionaryContainerNameReference(ModuleDefinition module, out AssemblyNameReference nameReference)
#endif
        {
            switch (RuntimeDetector.DetectRuntimeTarget(module))
            {
                case RuntimeTarget.Runtime:
                    nameReference = module.AssemblyReferences.FirstOrDefault(x => x.Name == SystemCollections)
                                    ?? With((AssemblyNameReference)module.TypeSystem.CoreLibrary, SystemCollections);
                    return true;
                case RuntimeTarget.NetStandard:
                case RuntimeTarget.MicrosoftCoreLibrary:
                    nameReference = default;
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryGetSystemRuntimeExtensionDllNameReference(ModuleDefinition module, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out AssemblyNameReference? nameReference)
#else
        public bool TryGetSystemRuntimeExtensionDllNameReference(ModuleDefinition module, out AssemblyNameReference nameReference)
#endif
        {
            switch (RuntimeDetector.DetectRuntimeTarget(module))
            {
                case RuntimeTarget.Runtime:
                    nameReference = module.AssemblyReferences.FirstOrDefault(x => x.Name == SystemRuntimeExtensions)
                                    ?? With((AssemblyNameReference)module.TypeSystem.CoreLibrary, SystemRuntimeExtensions);
                    return true;
                case RuntimeTarget.NetStandard:
                case RuntimeTarget.MicrosoftCoreLibrary:
                    nameReference = default;
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
