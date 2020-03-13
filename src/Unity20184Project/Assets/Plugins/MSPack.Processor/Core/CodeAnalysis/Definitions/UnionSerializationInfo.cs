// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct UnionSerializationInfo : IComparable<UnionSerializationInfo>
    {
        public readonly int Key;
        public readonly TypeReference Type;
        public readonly int Index;

        public ulong Value
        {
            get
            {
                ulong key = (ulong)Key;
                ulong index = (ulong)Index;
                return key << 32 | index;
            }
        }

        public UnionSerializationInfo(int key, TypeReference type, int index)
        {
            Key = key;
            Type = type;
            Index = index;
        }

        private sealed class Comparer : IComparer<CustomAttribute>
        {
            public int Compare(CustomAttribute x, CustomAttribute y)
            {
                var xKey = (int)x.ConstructorArguments[0].Value;
                var yKey = (int)y.ConstructorArguments[0].Value;
                if (xKey == yKey)
                {
                    throw new MessagePackGeneratorResolveFailedException("Union should not have same key.");
                }

                return xKey < yKey ? -1 : 1;
            }
        }

        public static bool TryParse(CustomAttribute unionAttribute, int index, out UnionSerializationInfo value)
        {
            if (unionAttribute.AttributeType.FullName != "MessagePack.UnionAttribute")
            {
                value = default;
                return false;
            }

            var key = (int)unionAttribute.ConstructorArguments[0].Value;
            var type = (TypeReference)unionAttribute.ConstructorArguments[1].Value;
            value = new UnionSerializationInfo(key, type, index);
            return true;
        }

        public static UnionSerializationInfo[] Parse(Collection<CustomAttribute> attributes)
        {
            var array = attributes.Where(x => x.AttributeType.FullName == "MessagePack.UnionAttribute").ToArray();

            if (array.Length == 0)
            {
                return Array.Empty<UnionSerializationInfo>();
            }

            Array.Sort(array, new Comparer());

            var answer = new UnionSerializationInfo[array.Length];

            for (var index = 0; index < answer.Length; index++)
            {
                if (!TryParse(array[index], index, out answer[index]))
                {
                    throw new InvalidProgramException();
                }
            }

            return answer;
        }

        public int CompareTo(UnionSerializationInfo other)
        {
            var keyComparison = Key.CompareTo(other.Key);
            if (keyComparison != 0)
            {
                return keyComparison;
            }

            throw new MessagePackGeneratorResolveFailedException("Union should not have same key. Between type  0 : " + this.Type.FullName + " and type 1 : " + other.Type.FullName);
        }
    }
}
