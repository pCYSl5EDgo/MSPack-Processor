// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;
using MSPack.Processor.Annotation;
using System;

namespace CompoundTestClasses
{
    [MessagePackObject(true)]
    [MessagePackObjectGenericVariation(typeof(Generics0<int, int>))]
    [MessagePackObjectGenericVariation(typeof(Generics0<long, long>))]
    public class Generics0<T0, T1> : IEquatable<Generics0<T0, T1>>//, IMessagePackSerializationCallbackReceiver
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>
    {
        public Generics0() { }
        public Generics0(T0 今度の戦いの結果で世界の命運が決まるa, T1 今度の戦いの結果で世界の命運が決まるb)
        {
            今度の戦いの結果で世界の命運が決まるA = 今度の戦いの結果で世界の命運が決まるa;
            今度の戦いの結果で世界の命運が決まるB = 今度の戦いの結果で世界の命運が決まるb;
        }

        //[Key(0)] 
        public T0 今度の戦いの結果で世界の命運が決まるA { get; }
        public T0 今度の戦いの結果で世界の命運が決まるAC { get; }
        public T0 今度の戦いの結果で世界の命運が決まるDC { get; }
        public T0 今度の戦いの結果で世界の命運が決まるAV { get; }

        //[Key(1)] 
        public T1 今度の戦いの結果で世界の命運が決まるB { get; }

        public bool Equals(Generics0<T0, T1> other)
        {
            if (ReferenceEquals(this, other)) return true;
            return 今度の戦いの結果で世界の命運が決まるA.Equals(other.今度の戦いの結果で世界の命運が決まるA) && 今度の戦いの結果で世界の命運が決まるB.Equals(other.今度の戦いの結果で世界の命運が決まるB);
        }

        public override bool Equals(object obj) => obj is Generics0<T0, T1> other && Equals(other);

        public override int GetHashCode() => 今度の戦いの結果で世界の命運が決まるA.GetHashCode() ^ 今度の戦いの結果で世界の命運が決まるB.GetHashCode();
        public void OnBeforeSerialize()
        {
            Console.WriteLine("Serialize");
            Console.WriteLine("A:" + 今度の戦いの結果で世界の命運が決まるA.ToString());
            Console.WriteLine("B:" + 今度の戦いの結果で世界の命運が決まるB.ToString());
        }

        public void OnAfterDeserialize()
        {
            Console.WriteLine("Deserialize");
            Console.WriteLine("A:" + 今度の戦いの結果で世界の命運が決まるA.ToString());
            Console.WriteLine("B:" + 今度の戦いの結果で世界の命運が決まるB.ToString());
        }
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
    [MessagePackObjectGenericVariation(typeof(Generics1<IntValue, IntValue>))]
    public class Generics1<T0, T1> : IEquatable<Generics1<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>, IDouble<T0>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>, IDouble<T1>
    {
        private T0 halfA;
        private T1 halfB;

        //[Key(0)]
        public T0 この手の名前は本当適当に決めることであらを探すのが大事だと思うの
        {
            get => halfA.Double();
            set => halfA = value.Half();
        }

        //[Key(1)]
        public T1 この手の名前は微妙に異なるものにするべき
        {
            get => halfB.Double();
            set => halfB = value.Half();
        }

        public bool Equals(Generics1<T0, T1> other)
        {
            if (ReferenceEquals(this, other)) return true;
            return この手の名前は本当適当に決めることであらを探すのが大事だと思うの.Equals(other.この手の名前は本当適当に決めることであらを探すのが大事だと思うの) && この手の名前は微妙に異なるものにするべき.Equals(other.この手の名前は微妙に異なるものにするべき);
        }

        public override bool Equals(object obj) => obj is Generics0<T0, T1> other && Equals(other);

        public override int GetHashCode() => halfA.GetHashCode() ^ halfB.GetHashCode();
    }

    [MessagePackObject(true)]
    [MessagePackObjectGenericVariation(typeof(Generics2<int, int>))]
    [MessagePackObjectGenericVariation(typeof(Generics2<long, long>))]
    public struct Generics2<T0, T1> : IEquatable<Generics2<T0, T1>>
        where T0 : unmanaged, IEquatable<T0>, IComparable<T1>
        where T1 : unmanaged, IEquatable<T1>, IComparable<T0>
    {
        public Generics2(T0 沢山適当に名前つけるのも楽ではない, T1 ちょっとだけ異なる名前つけるのも楽ではない)
        {
            this.沢山適当に名前つけるのも楽ではない = 沢山適当に名前つけるのも楽ではない;
            this.ちょっとだけ異なる名前つけるのも楽ではない = ちょっとだけ異なる名前つけるのも楽ではない;
        }

        //[Key(0)] 
        public T0 沢山適当に名前つけるのも楽ではない { get; }
        //[Key(1)] 
        public T1 ちょっとだけ異なる名前つけるのも楽ではない { get; }

        public bool Equals(Generics2<T0, T1> other)
        {
            return 沢山適当に名前つけるのも楽ではない.Equals(other.沢山適当に名前つけるのも楽ではない) && ちょっとだけ異なる名前つけるのも楽ではない.Equals(other.ちょっとだけ異なる名前つけるのも楽ではない);
        }

        public override bool Equals(object obj) => obj is Generics2<T0, T1> other && Equals(other);

        public override int GetHashCode() => 沢山適当に名前つけるのも楽ではない.GetHashCode() ^ ちょっとだけ異なる名前つけるのも楽ではない.GetHashCode();
    }

    [MessagePackObject(true)]
    [MessagePackObjectGenericVariation(typeof(Generics3<IntValue, IntValue>))]
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
        public T0 AAA
        {
            get => halfA.Double();
            set => halfA = value.Half();
        }

        //[Key(1)]
        public T1 ABA
        {
            get => halfB.Double();
            set => halfB = value.Half();
        }

        public bool Equals(Generics3<T0, T1> other)
        {
            return AAA.Equals(other.AAA) && ABA.Equals(other.ABA);
        }

        public override bool Equals(object obj) => obj is Generics3<T0, T1> other && Equals(other);

        public override int GetHashCode() => halfA.GetHashCode() ^ halfB.GetHashCode();
    }
}