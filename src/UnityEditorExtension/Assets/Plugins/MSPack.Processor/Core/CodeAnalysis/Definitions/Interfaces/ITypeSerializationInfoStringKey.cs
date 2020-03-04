// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace MSPack.Processor.Core.Definitions
{
    public interface ITypeSerializationInfoStringKey : ITypeSerializationInfo
    {
        IEnumerable<(string key, FieldOrPropertyInfo value)> EnumerateStringKeyValuePairs();
    }
}