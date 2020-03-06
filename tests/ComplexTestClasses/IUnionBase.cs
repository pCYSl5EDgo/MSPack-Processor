// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;

namespace ComplexTestClasses
{
    [Union(0, typeof(ImplementorStruct))]
    [Union(1, typeof(ImplementorGenericStruct<int>))]
    [Union(2, typeof(ImplementorGenericStruct<string>))]
    [Union(3, typeof(ImplementorGenericStruct<ImplementorStruct>))]
    public interface IUnionBase
    {
        int Value { get; }
    }
}