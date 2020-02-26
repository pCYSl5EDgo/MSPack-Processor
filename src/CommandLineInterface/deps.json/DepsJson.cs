// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace MSPack.Processor.CLI
{
    public class DepsJson
    {
        public RuntimeTarget runtimeTarget { get; set; }

        public CompilationOptions compilationOptions { get; set; }

        public Dictionary<string, Dictionary<string, RuntimeInfo>> targets { get; set; }

        public Dictionary<string, LibraryInfo> libraries { get; set; }
    }
}
