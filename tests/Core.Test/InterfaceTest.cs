// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ComplexTestClasses;
using MessagePack;
using NUnit.Framework;

namespace Core.Test
{
    [TestFixture]
    public class InterfaceTest
    {
        [TestCase(114)]
        [TestCase(514)]
        [TestCase(-1919)]
        [TestCase(-810)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase((int)sbyte.MinValue)]
        [TestCase((int)short.MinValue)]
        [TestCase(int.MinValue)]
        [TestCase((int)sbyte.MaxValue)]
        [TestCase((int)short.MaxValue)]
        [TestCase(int.MaxValue)]
        public void BasicTest(int arg)
        {
            var value0 = new ImplementorStruct(arg);
            var bytes0 = MessagePackSerializer.Serialize<IUnionBase>(value0);
            var other0 = MessagePackSerializer.Deserialize<IUnionBase>(bytes0);
            Assert.True(value0.Equals(other0));
            var value1 = new ImplementorGenericStruct<int>(arg);
            var bytes1 = MessagePackSerializer.Serialize<IUnionBase>(value1);
            var other1 = MessagePackSerializer.Deserialize<IUnionBase>(bytes1);
            Assert.True(value1.Equals(other1));
            var value2 = new ImplementorGenericStruct<ImplementorStruct>(value0);
            var bytes2 = MessagePackSerializer.Serialize<IUnionBase>(value2);
            var other2 = MessagePackSerializer.Deserialize<IUnionBase>(bytes2);
            Assert.True(value2.Equals(other2));
            var value3 = new ImplementorGenericStruct<string>(arg.ToString());
            var bytes3 = MessagePackSerializer.Serialize<IUnionBase>(value3);
            var other3 = MessagePackSerializer.Deserialize<IUnionBase>(bytes3);
            Assert.True(value3.Equals(other3));
        }
    }
}