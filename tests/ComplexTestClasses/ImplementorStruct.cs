// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using MessagePack;
using MSPack.Processor.Annotation;

namespace ComplexTestClasses
{
    [MessagePackObject(true)]
    public struct ImplementorStruct : IUnionBase, IEquatable<ImplementorStruct>
    {
        public ImplementorStruct(int a)
        {
            Value = a;
        }

        public int Value { get; }

        public override int GetHashCode()
        {
            return Value;
        }

        public bool Equals(ImplementorStruct other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is ImplementorStruct other && Equals(other);
        }
    }

    [MessagePackObject]
    [MessagePackObjectGenericVariation(typeof(ImplementorGenericStruct<int>))]
    [MessagePackObjectGenericVariation(typeof(ImplementorGenericStruct<ImplementorStruct>))]
    [MessagePackObjectGenericVariation(typeof(ImplementorGenericStruct<string>))]
    public struct ImplementorGenericStruct<T> : IUnionBase, IEquatable<ImplementorGenericStruct<T>>
        where T : IEquatable<T>
    {
        [Key(0)]
        private readonly T value;

        public ImplementorGenericStruct(T value)
        {
            this.value = value;
        }

        [Key(1)]
        public int Value => value.GetHashCode();

        public bool Equals(ImplementorGenericStruct<T> other)
        {
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            return obj is ImplementorGenericStruct<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(value);
        }
    }
}