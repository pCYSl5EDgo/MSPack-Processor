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
        public void GenericIntIntTest(int a, int b)
        {
            var value = new Generics<int, int>(a, b);
            Assert.AreEqual(value.A, a);
            Assert.AreEqual(value.B, b);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<Generics<int, int>>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.A, a);
            Assert.AreEqual(other.B, b);
        }
    }
}