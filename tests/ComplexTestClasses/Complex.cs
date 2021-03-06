﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using IrrelevantTestClasses;
using MessagePack;

namespace ComplexTestClasses
{
    [MessagePackObject]
    public sealed class ComplexType
    {
        [Key(0)] public IrrelevantType NameArray { get; set; }
    }
}
