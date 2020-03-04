// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;
using MSPack.Processor.Annotation;
using System;

namespace CompoundTestClasses
{
    [MessagePackObject(true)]
    [GenericArgument(typeof(Generics0<int, int>))]
    [GenericArgument(typeof(Generics0<long, long>))]
    public class Generics0<T0, T1> : IEquatable<Generics0<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>
    {
        public Generics0() { }
        public Generics0(T0 a, T1 b)
        {
            A = a;
            B = b;
        }

        //[Key(0)] 
        public T0 A { get; }
        //[Key(1)] 
        public T1 B { get; }

        public bool Equals(Generics0<T0, T1> other)
        {
            if (ReferenceEquals(this, other)) return true;
            return A.Equals(other.A) && B.Equals(other.B);
        }

        public override bool Equals(object obj) => obj is Generics0<T0, T1> other && Equals(other);

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
        [Key(0)]
        public int Value;

        public bool Equals(IntValue other) => Value == other.Value;

        public int CompareTo(IntValue other) => Value.CompareTo(other.Value);

        public IntValue Double() => new IntValue { Value = Value << 1 };

        public IntValue Half() => new IntValue { Value = Value >> 1 };

        public override int GetHashCode() => Value;
    }

    [MessagePackObject(true)]
    [GenericArgument(typeof(Generics1<IntValue, IntValue>))]
    public class Generics1<T0, T1> : IEquatable<Generics1<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>, IDouble<T0>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>, IDouble<T1>
    {
        private T0 halfA;
        private T1 halfB;

        //[Key(0)]
        public T0 A
        {
            get => halfA.Double();
            set => halfA = value.Half();
        }

        //[Key(1)]
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

        public override bool Equals(object obj) => obj is Generics0<T0, T1> other && Equals(other);

        public override int GetHashCode() => halfA.GetHashCode() ^ halfB.GetHashCode();
    }

    [MessagePackObject(true)]
    [GenericArgument(typeof(Generics2<int, int>))]
    [GenericArgument(typeof(Generics2<long, long>))]
    public struct Generics2<T0, T1> : IEquatable<Generics2<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>
    {
        public Generics2(T0 a, T1 b)
        {
            A = a;
            B = b;
        }

        //[Key(0)] 
        public T0 A { get; }
        //[Key(1)] 
        public T1 B { get; }

        public bool Equals(Generics2<T0, T1> other)
        {
            return A.Equals(other.A) && B.Equals(other.B);
        }

        public override bool Equals(object obj) => obj is Generics2<T0, T1> other && Equals(other);

        public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode();
    }

    [MessagePackObject(true)]
    [GenericArgument(typeof(Generics3<IntValue, IntValue>))]
    public struct Generics3<T0, T1> : IEquatable<Generics3<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>, IDouble<T0>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>, IDouble<T1>
    {
        private T0 halfA;
        private T1 halfB;

        public Generics3(T0 a, T1 b)
        {
            halfA = a;
            halfB = b;
        }

        //[Key(0)]
        public T0 A
        {
            get => halfA.Double();
            set => halfA = value.Half();
        }

        //[Key(1)]
        public T1 B
        {
            get => halfB.Double();
            set => halfB = value.Half();
        }

        public bool Equals(Generics3<T0, T1> other)
        {
            return A.Equals(other.A) && B.Equals(other.B);
        }

        public override bool Equals(object obj) => obj is Generics3<T0, T1> other && Equals(other);

        public override int GetHashCode() => halfA.GetHashCode() ^ halfB.GetHashCode();
    }
}