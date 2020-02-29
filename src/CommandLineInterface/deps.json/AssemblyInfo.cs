// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace MSPack.Processor.CLI
{
    public class AssemblyInfo
    {
        public string assemblyVersion { get; set; }

        public string fileVersion { get; set; }

        public bool ShouldSerializeassemblyVersion()
        {
            return !ReferenceEquals(assemblyVersion, null);
        }

        public bool ShouldSerializefileVersion()
        {
            return !ReferenceEquals(fileVersion, null);
        }
    }
}
