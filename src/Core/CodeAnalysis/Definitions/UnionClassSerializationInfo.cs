// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public struct UnionClassSerializationInfo
    {
        public readonly TypeDefinition Definition;
        public readonly UnionSerializationInfo[] SerializationInfo;
        public readonly int MinKey;
        public readonly int MaxKey;

        public UnionClassSerializationInfo(TypeDefinition definition, UnionSerializationInfo[] unionSerializationInfo)
        {
            Definition = definition;
            SerializationInfo = unionSerializationInfo;
            MinKey = SerializationInfo[0].Key;
            MaxKey = SerializationInfo[SerializationInfo.Length - 1].Key;
        }

        public static bool TryParse(TypeDefinition unionClassDefinition, out UnionClassSerializationInfo info)
        {
            var array = UnionSerializationInfo.Parse(unionClassDefinition.CustomAttributes);
            info = new UnionClassSerializationInfo(unionClassDefinition, array);
            return true;
        }

        public bool IsPerfectKeyIndexMatch => MinKey == 0 && MaxKey == SerializationInfo.Length - 1;
    }
}
