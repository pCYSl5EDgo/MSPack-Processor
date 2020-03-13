// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Report;
using System;
using System.Linq;

namespace MSPack.Processor.Core.Provider
{
    public static class AssemblyNameReferenceInjector
    {
        public static AssemblyNameReference Inject(ModuleDefinition module, AssemblyNameReference name, IReportHook reportHook)
        {
            var version = name.Version;
            foreach (var nameReference in module.AssemblyReferences)
            {
                if (Predicate(name, nameReference, version))
                {
                    return nameReference;
                }
            }

            reportHook.ImportNotPreviouslyReferencedAssemblyNameReference(name.Name, version.Major, version.Minor, version.Build, version.Revision, name.Culture, name.PublicKeyToken);
            module.AssemblyReferences.Add(name);
            return name;
        }

        private static bool Predicate(AssemblyNameReference name, AssemblyNameReference nameReference, Version version)
        {
            return string.Equals(nameReference.Name, name.Name, StringComparison.Ordinal)
                   && string.Equals(nameReference.Culture, name.Culture, StringComparison.Ordinal)
                   && nameReference.Version.Equals(version)
                   && nameReference.PublicKeyToken.SequenceEqual(name.PublicKeyToken);
        }
    }
}
