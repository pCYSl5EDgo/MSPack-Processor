﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Collections.Generic;

namespace MSPack.Processor.Core.Definitions
{
    public interface IMemberSerializeInfo
    {
        string FullName { get; }

        bool IsIntKey { get; }

        bool IsStringKey { get; }

        uint Index { get; }

        int IntKey { get; }

        string StringKey { get; }

        bool IsReadable { get; }

        bool IsWritable { get; }

        bool IsMessagePackPrimitive { get; }

        bool PublicAccessible { get; }

        bool IsValueType { get; }

        bool IsFixedArray { get; }

        uint FixedArrayLength { get; }

        TypeReference MemberTypeReference { get; }

        Collection<CustomAttribute> CustomAttributes { get; }
    }
}
