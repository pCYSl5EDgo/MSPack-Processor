// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Build.Framework;
using MSPack.Processor.Core;
using MSPack.Processor.Core.Report;

namespace MSPack.Processor.MSBuild.Tasks
{
    public class Generator : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string Input { get; set; }

        public string ResolverName { get; set; }

        public string LibraryPaths { get; set; }

        public string DefinitionPaths { get; set; }

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

            var libraryPaths = Split(LibraryPaths);
            var definitionPaths = Split(DefinitionPaths);

            try
            {
                new CodeGenerator(x => this.Log.LogMessage(x), new NoHook())
                    .Generate(
                        Input,
                        ResolverName ?? "MessagePack.GeneratedResolver",
                        libraryPaths,
                        definitionPaths,
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

        private string[] Split(string paths)
        {
            return string.IsNullOrWhiteSpace(paths) ? Array.Empty<string>() : paths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
