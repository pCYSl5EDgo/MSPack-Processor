// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

namespace MSPack.Processor.CLI
{
    public class RuntimeInfo
    {
        public Dictionary<string, string> dependencies { get; set; }

        public Dictionary<string, AssemblyInfo> runtime { get; set; }

        public bool ShouldSerializedependencies()
        {
            return !ReferenceEquals(dependencies, null) && dependencies.Any();
        }

        public bool ShouldSerializeruntime()
        {
            return !ReferenceEquals(runtime, null) && runtime.Any();
        }
    }
}
