// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CompoundTestClasses;
using MessagePack;
using NUnit.Framework;

namespace Core.Test
{
    [TestFixture]
    public class CustomSerializerTest
    {
        [TestCase(114)]
        [TestCase(514)]
        [TestCase(1919)]
        [TestCase(810)]
        [TestCase(33)]
        [TestCase(-4)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase((int)sbyte.MaxValue)]
        [TestCase((int)sbyte.MaxValue)]
        [TestCase((int)short.MaxValue)]
        [TestCase((int)short.MaxValue)]
        public void SimpleCustomFormatterTest(int a)
        {
            var value = new CustomSerializerClass0(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<CustomSerializerClass0>(bytes);
            Assert.AreEqual(a, other.A);
            Assert.True(value.Equals(other));
        }

        [TestCase(114)]
        [TestCase(514)]
        [TestCase(1919)]
        [TestCase(810)]
        [TestCase(33)]
        [TestCase(-4)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase((int)sbyte.MaxValue)]
        [TestCase((int)sbyte.MaxValue)]
        [TestCase((int)short.MaxValue)]
        [TestCase((int)short.MaxValue)]
        public void CustomFormatterWithArgumentTest(int a)
        {
            var value = new CustomSerializerClass1(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<CustomSerializerClass1>(bytes);
            Assert.AreEqual(a, other.A);
            Assert.True(value.Equals(other));
        }
    }
}