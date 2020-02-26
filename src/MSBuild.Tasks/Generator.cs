using System;
using Microsoft.Build.Framework;
using MSPack.Processor.Core;
using MSPack.Processor.Core.Report;

namespace MSBuild.Tasks
{
    // Copyright (c) All contributors. All rights reserved.
    // Licensed under the MIT license. See LICENSE file in the project root for full license information.

    public class Generator : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string Target { get; set; }

        public string ResolverName { get; set; }

        public string LibraryPaths { get; set; }

        public string LoadFactor { get; set; }

        public bool UseMapMode { get; set; }

        private class NoHook : IReportHook
        {
            public void ImportNotPreviouslyReferencedAssemblyNameReference(string name, int majorVersion, int minorVersion, int buildVersion, int revisionVersion, string culture, byte[] publicKeyToken)
            {
            }
        }

        public override bool Execute()
        {
            if (!double.TryParse(LoadFactor, out var loadFactor))
            {
                loadFactor = 0.75;
            }

            var libraryPaths = string.IsNullOrWhiteSpace(LibraryPaths) ? Array.Empty<string>() : LibraryPaths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                new CodeGenerator(x => this.Log.LogMessage(x), new NoHook())
                    .Generate(
                        Target,
                        ResolverName ?? "MessagePack.GeneratedResolver",
                        libraryPaths,
                        UseMapMode,
                        loadFactor);
            }
            catch (Exception ex)
            {
                this.Log.LogErrorFromException(ex, true);
                return false;
            }

            return true;
        }
    }
}
