// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace MSPack.Processor.Core.Definitions
{
    public static class StringKeyEnumerateHelper
    {
        public static StringKeySerializationInfoTuple[] Enumerate(FieldSerializationInfo[] fields, PropertySerializationInfo[] properties)
        {
            var length = fields.Length + properties.Length;
            if (length == 0)
            {
                return Array.Empty<StringKeySerializationInfoTuple>();
            }

            var answer = new StringKeySerializationInfoTuple[length];
            for (var i = 0; i < fields.Length; i++)
            {
                ref readonly var info = ref fields[i];
                answer[i] = new StringKeySerializationInfoTuple(info.StringKey, new FieldOrPropertyInfo(info));
            }

            for (var i = 0; i < properties.Length; i++)
            {
                ref readonly var info = ref properties[i];
                answer[i + fields.Length] = new StringKeySerializationInfoTuple(info.StringKey, new FieldOrPropertyInfo(info));
            }

            Array.Sort(answer);

            return answer;
        }
    }
}
