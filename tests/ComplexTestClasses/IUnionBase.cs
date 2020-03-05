// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;

namespace ComplexTestClasses
{
    [Union(0, typeof(ImplementorStruct))]
    public interface IUnionBase
    {
        int Value { get; }
    }
}