// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using MessagePack;
using MSPack.Processor.Annotation;

namespace CompoundTestClasses
{
    [MessagePackObject]
    [GenericArgument(typeof(int), typeof(int))]
    [GenericArgument(typeof(int), typeof(ulong))]
    public class Generics<T0, T1> : IEquatable<Generics<T0, T1>>
        where T0 : IEquatable<T0>
        where T1 : IEquatable<T1>
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
}