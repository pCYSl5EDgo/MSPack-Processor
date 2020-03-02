// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using MessagePack;
using MSPack.Processor.Annotation;

namespace CompoundTestClasses
{
    [MessagePackObject]
    [GenericArgument(typeof(int), typeof(int))]
    [GenericArgument(typeof(ulong), typeof(ulong))]
    public class Generics<T0, T1> : IEquatable<Generics<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>
    {
        [Key(0)] public T0 A;
        [Key(1)] public T1 B;

        public bool Equals(Generics<T0, T1> other)
        {
            if (ReferenceEquals(this, other)) return true;
            return A.Equals(other.A) && B.Equals(other.B);
        }

        public override bool Equals(object obj) => obj is Generics<T0, T1> other && Equals(other);

        public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode();
    }

    public interface IDouble<T>
    {
        T Double();
        T Half();
    }

    [MessagePackObject]
    public struct IntValue
        : IEquatable<IntValue>,
            IComparable<IntValue>,
            IDouble<IntValue>
    {
        [Key(0)] public int Value;

        public bool Equals(IntValue other) => Value == other.Value;

        public int CompareTo(IntValue other) => Value.CompareTo(other.Value);

        public IntValue Double() => new IntValue { Value = Value << 1 };

        public IntValue Half() => new IntValue { Value = Value >> 1 };

        public override int GetHashCode() => Value;
    }

    [MessagePackObject]
    [GenericArgument(typeof(int), typeof(int))]
    [GenericArgument(typeof(ulong), typeof(ulong))]
    public class Generics1<T0, T1> : IEquatable<Generics1<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>, IDouble<T0>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>, IDouble<T1>
    {
        private T0 halfA;
        private T1 halfB;

        [Key(0)]
        public T0 A
        {
            get => halfA.Double();
            set => halfA = value.Half();
        }

        [Key(1)]
        public T1 B
        {
            get => halfB.Double();
            set => halfB = value.Half();
        }

        public bool Equals(Generics1<T0, T1> other)
        {
            if (ReferenceEquals(this, other)) return true;
            return A.Equals(other.A) && B.Equals(other.B);
        }

        public override bool Equals(object obj) => obj is Generics<T0, T1> other && Equals(other);

        public override int GetHashCode() => halfA.GetHashCode() ^ halfB.GetHashCode();
    }
}