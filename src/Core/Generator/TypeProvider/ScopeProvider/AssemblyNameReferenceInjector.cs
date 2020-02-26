using System;
using System.Linq;
using MSPack.Processor.Core.Report;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public static class AssemblyNameReferenceInjector
    {
        public static AssemblyNameReference Inject(ModuleDefinition module, AssemblyNameReference name, IReportHook reportHook)
        {
            var version = name.Version;
            foreach (var nameReference in module.AssemblyReferences)
            {
                if (string.Equals(nameReference.Name, name.Name, StringComparison.Ordinal)
                    && string.Equals(nameReference.Culture, name.Culture, StringComparison.Ordinal)
                    && nameReference.Version.Equals(version)
                    && nameReference.PublicKeyToken.SequenceEqual(name.PublicKeyToken))
                {
                    return nameReference;
                }
            }

            reportHook.ImportNotPreviouslyReferencedAssemblyNameReference(name.Name, version.Major, version.Minor, version.Build, version.Revision, name.Culture, name.PublicKeyToken);
            module.AssemblyReferences.Add(name);
            return name;
        }
    }
}
