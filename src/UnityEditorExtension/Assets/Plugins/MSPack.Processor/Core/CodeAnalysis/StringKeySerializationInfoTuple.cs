// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct StringKeySerializationInfoTuple : IComparable<StringKeySerializationInfoTuple>
    {
        public readonly string key;

        public readonly FieldOrPropertyInfo value;

        public StringKeySerializationInfoTuple(string key, FieldOrPropertyInfo value)
        {
            this.key = key;
            this.value = value;
        }

        public int CompareTo(StringKeySerializationInfoTuple other)
        {
            var xKey = key;
            var yKey = other.key;
            var c = xKey.Length.CompareTo(yKey.Length);
            if (c != 0)
            {
                return c;
            }

            return string.CompareOrdinal(xKey, yKey);
        }

        public void Deconstruct(out string key, out FieldOrPropertyInfo value)
        {
            key = this.key;
            value = this.value;
        }
    }
}