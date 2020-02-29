using MessagePack;
using System;

namespace SimpleTestClasses
{
    [MessagePackObject]
    public struct EmptyStruct : IEquatable<EmptyStruct>
    {
        public override bool Equals(object obj) => obj is EmptyStruct;

        public bool Equals(EmptyStruct other) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "";
    }

    [MessagePackObject]
    public class EmptyClass : IEquatable<EmptyClass>
    {
        public override bool Equals(object obj) => obj is EmptyClass;

        public bool Equals(EmptyClass other) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "";
    }

    [MessagePackObject]
    public sealed class EmptySealedClass : IEquatable<EmptySealedClass>
    {
        public override bool Equals(object obj) => obj is EmptySealedClass;

        public bool Equals(EmptySealedClass other) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "";
    }
}
