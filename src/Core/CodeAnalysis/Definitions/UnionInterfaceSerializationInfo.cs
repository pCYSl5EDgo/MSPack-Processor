// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct UnionInterfaceSerializationInfo
    {
        public readonly TypeDefinition Definition;
        public readonly UnionSerializationInfo[] SerializationInfo;
        public readonly int MinKey;
        public readonly int MaxKey;

        public UnionInterfaceSerializationInfo(TypeDefinition definition, UnionSerializationInfo[] unionSerializationInfo)
        {
            Definition = definition;
            SerializationInfo = unionSerializationInfo;
            MinKey = SerializationInfo[0].Key;
            MaxKey = SerializationInfo[^1].Key;
        }

        public static bool TryParse(TypeDefinition unionInterfaceDefinition, out UnionInterfaceSerializationInfo info)
        {
            var array = UnionSerializationInfo.Parse(unionInterfaceDefinition.CustomAttributes);
            info = new UnionInterfaceSerializationInfo(unionInterfaceDefinition, array);
            return true;
        }

        public bool IsPerfectKeyIndexMatch => MinKey == 0 && MaxKey == SerializationInfo.Length - 1;
    }
}
