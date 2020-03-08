// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CompoundTestClasses;
using MessagePack;
using NUnit.Framework;
using System;

namespace Core.Test
{
    [TestFixture]
    public class GenericsTest
    {
        [TestCase(114, 514)]
        [TestCase(0, 0)]
        [TestCase((int)sbyte.MinValue, -1)]
        [TestCase((int)byte.MinValue, (int)sbyte.MaxValue)]
        [TestCase(int.MaxValue, int.MinValue)]
        [TestCase((int)short.MaxValue, (int)short.MinValue)]
        public void Generic0IntIntTest(int a, int b)
        {
            Console.WriteLine("A");
            var value = new Generics0<int, int>(a, b);
            Assert.AreEqual(value.今度の戦いの結果で世界の命運が決まるA, a);
            Assert.AreEqual(value.今度の戦いの結果で世界の命運が決まるB, b);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<Generics0<int, int>>(bytes);
            foreach (var b1 in bytes)
            {
                Console.WriteLine(b1.ToString("X"));
            }

            Console.WriteLine(value.今度の戦いの結果で世界の命運が決まるA + " , " + value.今度の戦いの結果で世界の命運が決まるB);
            Console.WriteLine(other.今度の戦いの結果で世界の命運が決まるA + " , " + other.今度の戦いの結果で世界の命運が決まるB);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.今度の戦いの結果で世界の命運が決まるA, a);
            Assert.AreEqual(other.今度の戦いの結果で世界の命運が決まるB, b);
        }

        [TestCase(114, 514)]
        [TestCase(0, 0)]
        [TestCase((int)sbyte.MinValue, -1)]
        [TestCase((int)byte.MinValue, (int)sbyte.MaxValue)]
        [TestCase(int.MaxValue, int.MinValue)]
        [TestCase((int)short.MaxValue, (int)short.MinValue)]
        public void Generic2IntIntTest(int a, int b)
        {
            var value = new Generics2<int, int>(a, b);
            Assert.AreEqual(value.A, a);
            Assert.AreEqual(value.B, b);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<Generics2<int, int>>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.A, a);
            Assert.AreEqual(other.B, b);
        }

        [TestCase(114, 514)]
        [TestCase(0, 0)]
        [TestCase((int)sbyte.MinValue, -1)]
        [TestCase((int)byte.MinValue, (int)sbyte.MaxValue)]
        [TestCase(int.MaxValue, int.MinValue)]
        [TestCase((int)short.MaxValue, (int)short.MinValue)]
        public void Generic3Test(int a, int b)
        {
            var value = new Generics3<IntValue, IntValue>(new IntValue()
            {
                Value = a,
            }, new IntValue()
            {
                Value = b,
            });
            Assert.AreEqual(value.A.Value, a * 2);
            Assert.AreEqual(value.B.Value, b * 2);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<Generics3<IntValue, IntValue>>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.A.Value, a * 2);
            Assert.AreEqual(other.B.Value, b * 2);
        }
    }
}