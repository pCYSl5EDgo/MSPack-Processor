// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Linq;
using System.Text;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct CollectedInfo
    {
        public readonly ModuleDefinition Module;
        public readonly ClassSerializationInfo[] ClassSerializationInfos;
        public readonly StructSerializationInfo[] StructSerializationInfos;
        public readonly UnionClassSerializationInfo[] UnionClassSerializationInfos;
        public readonly UnionInterfaceSerializationInfo[] InterfaceSerializationInfos;
        public readonly GenericClassSerializationInfo[] GenericClassSerializationInfos;
        public readonly bool PublicAccessible;

        public CollectedInfo(ModuleDefinition module, ClassSerializationInfo[] classSerializationInfos, StructSerializationInfo[] structSerializationInfos, UnionClassSerializationInfo[] unionClassSerializationInfos, UnionInterfaceSerializationInfo[] interfaceSerializationInfos, GenericClassSerializationInfo[] genericClassSerializationInfos)
        {
            this.Module = module;
            this.ClassSerializationInfos = classSerializationInfos;
            this.StructSerializationInfos = structSerializationInfos;
            this.UnionClassSerializationInfos = unionClassSerializationInfos;
            this.InterfaceSerializationInfos = interfaceSerializationInfos;
            this.GenericClassSerializationInfos = genericClassSerializationInfos;

            PublicAccessible = ClassSerializationInfos.All(x => x.PublicAccessible);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("Module : ").Append(Module.Name);
            builder.Append("\nClass : count -> ").Append(ClassSerializationInfos.Length);
            for (var i = 0; i < ClassSerializationInfos.Length; i++)
            {
                builder.Append("\nClass[").Append(i).Append("]").Append(ClassSerializationInfos[i].ToString());
            }

            builder.Append("\nStruct : count -> ").Append(StructSerializationInfos.Length);
            for (var i = 0; i < StructSerializationInfos.Length; i++)
            {
                builder.Append("\nClass[").Append(i).Append("]").Append(StructSerializationInfos[i].ToString());
            }

            return builder.ToString();
        }
    }
}
