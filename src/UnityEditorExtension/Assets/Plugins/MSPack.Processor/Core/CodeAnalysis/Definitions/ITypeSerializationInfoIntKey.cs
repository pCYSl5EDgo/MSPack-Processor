// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace MSPack.Processor.Core.Definitions
{
    public interface ITypeSerializationInfoIntKey : ITypeSerializationInfo
    {
        int KeyCount { get; }

        int MaxIntKey { get; }

        int MinIntKey { get; }

        FieldOrPropertyInfo this[int key] { get; }
    }
}