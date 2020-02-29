// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace MSPack.Processor.CLI
{
    public class LibraryInfo
    {
        public string type { get; set; }

        public bool serviceable { get; set; }

        public string sha512 { get; set; }

        public string path { get; set; }

        public string hashPath { get; set; }

        public bool ShouldSerializepath() => !ReferenceEquals(path, null);

        public bool ShouldSerializehashPath() => !ReferenceEquals(hashPath, null);
    }
}
