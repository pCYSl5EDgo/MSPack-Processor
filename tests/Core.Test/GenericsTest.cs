// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CompoundTestClasses;
using MessagePack;
using NUnit.Framework;

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
            var value = new Generics0<int, int>(a, b);
            Assert.AreEqual(value.A, a);
            Assert.AreEqual(value.B, b);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<Generics0<int, int>>(bytes);
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