using System;
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
