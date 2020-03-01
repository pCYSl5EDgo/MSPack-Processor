// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;

namespace IrrelevantTestClasses
{
    [MessagePackObject]
    public class IrrelevantType
    {
        [Key(0)] public string[] Names { get; } = new string[10];
    }
}
