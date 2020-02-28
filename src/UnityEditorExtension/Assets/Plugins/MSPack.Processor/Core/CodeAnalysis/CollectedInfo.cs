// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct CollectedInfo
    {
        public readonly ModuleDefinition Module;
        public readonly ClassSerializationInfo[] ClassSerializationInfos;
        public readonly StructSerializationInfo[] StructSerializationInfos;
        public readonly UnionClassSerializationInfo[] UnionClassSerializationInfos;
        public readonly UnionInterfaceSerializationInfo[] InterfaceSerializationInfos;
        public readonly bool PublicAccessible;

        public CollectedInfo(ModuleDefinition module, ClassSerializationInfo[] classSerializationInfos, StructSerializationInfo[] structSerializationInfos, UnionClassSerializationInfo[] unionClassSerializationInfos, UnionInterfaceSerializationInfo[] interfaceSerializationInfos)
        {
            this.Module = module;
            this.ClassSerializationInfos = classSerializationInfos;
            this.StructSerializationInfos = structSerializationInfos;
            this.UnionClassSerializationInfos = unionClassSerializationInfos;
            this.InterfaceSerializationInfos = interfaceSerializationInfos;

            PublicAccessible = ClassSerializationInfos.All(x => x.PublicAccessible);
        }
    }
}
