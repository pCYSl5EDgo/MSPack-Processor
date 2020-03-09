using MessagePack;
using NUnit.Framework;
using SimpleTestClasses;
using System;

namespace Core.Test
{
    [TestFixture]
    public class SimpleTypeTests
    {
        [TestCase(unchecked((Byte)(0)))]
        [TestCase(unchecked((Byte)(1)))]
        [TestCase(unchecked((Byte)(-1)))]
        [TestCase(unchecked((Byte)sbyte.MinValue))]
        [TestCase(unchecked((Byte)sbyte.MaxValue))]
        [TestCase(unchecked((Byte)byte.MinValue))]
        [TestCase(unchecked((Byte)byte.MaxValue))]
        [TestCase(unchecked((Byte)short.MinValue))]
        [TestCase(unchecked((Byte)short.MaxValue))]
        [TestCase(unchecked((Byte)ushort.MinValue))]
        [TestCase(unchecked((Byte)ushort.MaxValue))]
        [TestCase(unchecked((Byte)int.MinValue))]
        [TestCase(unchecked((Byte)int.MaxValue))]
        [TestCase(unchecked((Byte)uint.MinValue))]
        [TestCase(unchecked((Byte)uint.MaxValue))]
        [TestCase(unchecked((Byte)long.MinValue))]
        [TestCase(unchecked((Byte)long.MaxValue))]
        [TestCase(unchecked((Byte)ulong.MinValue))]
        [TestCase(unchecked((Byte)ulong.MaxValue))]
        public void TestByte(Byte a)
        {
            var value = new SimpleTypeByte(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeByte>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((SByte)(0)))]
        [TestCase(unchecked((SByte)(1)))]
        [TestCase(unchecked((SByte)(-1)))]
        [TestCase(unchecked((SByte)sbyte.MinValue))]
        [TestCase(unchecked((SByte)sbyte.MaxValue))]
        [TestCase(unchecked((SByte)byte.MinValue))]
        [TestCase(unchecked((SByte)byte.MaxValue))]
        [TestCase(unchecked((SByte)short.MinValue))]
        [TestCase(unchecked((SByte)short.MaxValue))]
        [TestCase(unchecked((SByte)ushort.MinValue))]
        [TestCase(unchecked((SByte)ushort.MaxValue))]
        [TestCase(unchecked((SByte)int.MinValue))]
        [TestCase(unchecked((SByte)int.MaxValue))]
        [TestCase(unchecked((SByte)uint.MinValue))]
        [TestCase(unchecked((SByte)uint.MaxValue))]
        [TestCase(unchecked((SByte)long.MinValue))]
        [TestCase(unchecked((SByte)long.MaxValue))]
        [TestCase(unchecked((SByte)ulong.MinValue))]
        [TestCase(unchecked((SByte)ulong.MaxValue))]
        public void TestSByte(SByte a)
        {
            var value = new SimpleTypeSByte(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeSByte>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((Int16)(0)))]
        [TestCase(unchecked((Int16)(1)))]
        [TestCase(unchecked((Int16)(-1)))]
        [TestCase(unchecked((Int16)sbyte.MinValue))]
        [TestCase(unchecked((Int16)sbyte.MaxValue))]
        [TestCase(unchecked((Int16)byte.MinValue))]
        [TestCase(unchecked((Int16)byte.MaxValue))]
        [TestCase(unchecked((Int16)short.MinValue))]
        [TestCase(unchecked((Int16)short.MaxValue))]
        [TestCase(unchecked((Int16)ushort.MinValue))]
        [TestCase(unchecked((Int16)ushort.MaxValue))]
        [TestCase(unchecked((Int16)int.MinValue))]
        [TestCase(unchecked((Int16)int.MaxValue))]
        [TestCase(unchecked((Int16)uint.MinValue))]
        [TestCase(unchecked((Int16)uint.MaxValue))]
        [TestCase(unchecked((Int16)long.MinValue))]
        [TestCase(unchecked((Int16)long.MaxValue))]
        [TestCase(unchecked((Int16)ulong.MinValue))]
        [TestCase(unchecked((Int16)ulong.MaxValue))]
        public void TestInt16(Int16 a)
        {
            var value = new SimpleTypeInt16(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeInt16>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((Int32)(0)))]
        [TestCase(unchecked((Int32)(1)))]
        [TestCase(unchecked((Int32)(-1)))]
        [TestCase(unchecked((Int32)sbyte.MinValue))]
        [TestCase(unchecked((Int32)sbyte.MaxValue))]
        [TestCase(unchecked((Int32)byte.MinValue))]
        [TestCase(unchecked((Int32)byte.MaxValue))]
        [TestCase(unchecked((Int32)short.MinValue))]
        [TestCase(unchecked((Int32)short.MaxValue))]
        [TestCase(unchecked((Int32)ushort.MinValue))]
        [TestCase(unchecked((Int32)ushort.MaxValue))]
        [TestCase(unchecked((Int32)int.MinValue))]
        [TestCase(unchecked((Int32)int.MaxValue))]
        [TestCase(unchecked((Int32)uint.MinValue))]
        [TestCase(unchecked((Int32)uint.MaxValue))]
        [TestCase(unchecked((Int32)long.MinValue))]
        [TestCase(unchecked((Int32)long.MaxValue))]
        [TestCase(unchecked((Int32)ulong.MinValue))]
        [TestCase(unchecked((Int32)ulong.MaxValue))]
        public void TestInt32(Int32 a)
        {
            var value = new SimpleTypeInt32(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeInt32>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((Int64)(0)))]
        [TestCase(unchecked((Int64)(1)))]
        [TestCase(unchecked((Int64)(-1)))]
        [TestCase(unchecked((Int64)sbyte.MinValue))]
        [TestCase(unchecked((Int64)sbyte.MaxValue))]
        [TestCase(unchecked((Int64)byte.MinValue))]
        [TestCase(unchecked((Int64)byte.MaxValue))]
        [TestCase(unchecked((Int64)short.MinValue))]
        [TestCase(unchecked((Int64)short.MaxValue))]
        [TestCase(unchecked((Int64)ushort.MinValue))]
        [TestCase(unchecked((Int64)ushort.MaxValue))]
        [TestCase(unchecked((Int64)int.MinValue))]
        [TestCase(unchecked((Int64)int.MaxValue))]
        [TestCase(unchecked((Int64)uint.MinValue))]
        [TestCase(unchecked((Int64)uint.MaxValue))]
        [TestCase(unchecked((Int64)long.MinValue))]
        [TestCase(unchecked((Int64)long.MaxValue))]
        [TestCase(unchecked((Int64)ulong.MinValue))]
        [TestCase(unchecked((Int64)ulong.MaxValue))]
        public void TestInt64(Int64 a)
        {
            var value = new SimpleTypeInt64(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeInt64>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((UInt16)(0)))]
        [TestCase(unchecked((UInt16)(1)))]
        [TestCase(unchecked((UInt16)(-1)))]
        [TestCase(unchecked((UInt16)sbyte.MinValue))]
        [TestCase(unchecked((UInt16)sbyte.MaxValue))]
        [TestCase(unchecked((UInt16)byte.MinValue))]
        [TestCase(unchecked((UInt16)byte.MaxValue))]
        [TestCase(unchecked((UInt16)short.MinValue))]
        [TestCase(unchecked((UInt16)short.MaxValue))]
        [TestCase(unchecked((UInt16)ushort.MinValue))]
        [TestCase(unchecked((UInt16)ushort.MaxValue))]
        [TestCase(unchecked((UInt16)int.MinValue))]
        [TestCase(unchecked((UInt16)int.MaxValue))]
        [TestCase(unchecked((UInt16)uint.MinValue))]
        [TestCase(unchecked((UInt16)uint.MaxValue))]
        [TestCase(unchecked((UInt16)long.MinValue))]
        [TestCase(unchecked((UInt16)long.MaxValue))]
        [TestCase(unchecked((UInt16)ulong.MinValue))]
        [TestCase(unchecked((UInt16)ulong.MaxValue))]
        public void TestUInt16(UInt16 a)
        {
            var value = new SimpleTypeUInt16(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeUInt16>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((UInt32)(0)))]
        [TestCase(unchecked((UInt32)(1)))]
        [TestCase(unchecked((UInt32)(-1)))]
        [TestCase(unchecked((UInt32)sbyte.MinValue))]
        [TestCase(unchecked((UInt32)sbyte.MaxValue))]
        [TestCase(unchecked((UInt32)byte.MinValue))]
        [TestCase(unchecked((UInt32)byte.MaxValue))]
        [TestCase(unchecked((UInt32)short.MinValue))]
        [TestCase(unchecked((UInt32)short.MaxValue))]
        [TestCase(unchecked((UInt32)ushort.MinValue))]
        [TestCase(unchecked((UInt32)ushort.MaxValue))]
        [TestCase(unchecked((UInt32)int.MinValue))]
        [TestCase(unchecked((UInt32)int.MaxValue))]
        [TestCase(unchecked((UInt32)uint.MinValue))]
        [TestCase(unchecked((UInt32)uint.MaxValue))]
        [TestCase(unchecked((UInt32)long.MinValue))]
        [TestCase(unchecked((UInt32)long.MaxValue))]
        [TestCase(unchecked((UInt32)ulong.MinValue))]
        [TestCase(unchecked((UInt32)ulong.MaxValue))]
        public void TestUInt32(UInt32 a)
        {
            var value = new SimpleTypeUInt32(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeUInt32>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((UInt64)(0)))]
        [TestCase(unchecked((UInt64)(1)))]
        [TestCase(unchecked((UInt64)(-1)))]
        [TestCase(unchecked((UInt64)sbyte.MinValue))]
        [TestCase(unchecked((UInt64)sbyte.MaxValue))]
        [TestCase(unchecked((UInt64)byte.MinValue))]
        [TestCase(unchecked((UInt64)byte.MaxValue))]
        [TestCase(unchecked((UInt64)short.MinValue))]
        [TestCase(unchecked((UInt64)short.MaxValue))]
        [TestCase(unchecked((UInt64)ushort.MinValue))]
        [TestCase(unchecked((UInt64)ushort.MaxValue))]
        [TestCase(unchecked((UInt64)int.MinValue))]
        [TestCase(unchecked((UInt64)int.MaxValue))]
        [TestCase(unchecked((UInt64)uint.MinValue))]
        [TestCase(unchecked((UInt64)uint.MaxValue))]
        [TestCase(unchecked((UInt64)long.MinValue))]
        [TestCase(unchecked((UInt64)long.MaxValue))]
        [TestCase(unchecked((UInt64)ulong.MinValue))]
        [TestCase(unchecked((UInt64)ulong.MaxValue))]
        public void TestUInt64(UInt64 a)
        {
            var value = new SimpleTypeUInt64(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeUInt64>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((Single)(0)))]
        [TestCase(unchecked((Single)(1)))]
        [TestCase(unchecked((Single)(-1)))]
        [TestCase(unchecked((Single)sbyte.MinValue))]
        [TestCase(unchecked((Single)sbyte.MaxValue))]
        [TestCase(unchecked((Single)byte.MinValue))]
        [TestCase(unchecked((Single)byte.MaxValue))]
        [TestCase(unchecked((Single)short.MinValue))]
        [TestCase(unchecked((Single)short.MaxValue))]
        [TestCase(unchecked((Single)ushort.MinValue))]
        [TestCase(unchecked((Single)ushort.MaxValue))]
        [TestCase(unchecked((Single)int.MinValue))]
        [TestCase(unchecked((Single)int.MaxValue))]
        [TestCase(unchecked((Single)uint.MinValue))]
        [TestCase(unchecked((Single)uint.MaxValue))]
        [TestCase(unchecked((Single)long.MinValue))]
        [TestCase(unchecked((Single)long.MaxValue))]
        [TestCase(unchecked((Single)ulong.MinValue))]
        [TestCase(unchecked((Single)ulong.MaxValue))]
        public void TestSingle(Single a)
        {
            var value = new SimpleTypeSingle(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeSingle>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(unchecked((Double)(0)))]
        [TestCase(unchecked((Double)(1)))]
        [TestCase(unchecked((Double)(-1)))]
        [TestCase(unchecked((Double)sbyte.MinValue))]
        [TestCase(unchecked((Double)sbyte.MaxValue))]
        [TestCase(unchecked((Double)byte.MinValue))]
        [TestCase(unchecked((Double)byte.MaxValue))]
        [TestCase(unchecked((Double)short.MinValue))]
        [TestCase(unchecked((Double)short.MaxValue))]
        [TestCase(unchecked((Double)ushort.MinValue))]
        [TestCase(unchecked((Double)ushort.MaxValue))]
        [TestCase(unchecked((Double)int.MinValue))]
        [TestCase(unchecked((Double)int.MaxValue))]
        [TestCase(unchecked((Double)uint.MinValue))]
        [TestCase(unchecked((Double)uint.MaxValue))]
        [TestCase(unchecked((Double)long.MinValue))]
        [TestCase(unchecked((Double)long.MaxValue))]
        [TestCase(unchecked((Double)ulong.MinValue))]
        [TestCase(unchecked((Double)ulong.MaxValue))]
        public void TestDouble(Double a)
        {
            var value = new SimpleTypeDouble(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeDouble>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase((char)0)]
        [TestCase((char)byte.MaxValue)]
        [TestCase((char)short.MaxValue)]
        public void TestChar(Char a)
        {
            var value = new SimpleTypeChar(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeChar>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
        [TestCase(true)]
        [TestCase(false)]
        public void TestBoolean(Boolean a)
        {
            var value = new SimpleTypeBoolean(a);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<SimpleTypeBoolean>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(other.Value, a);
        }
    }
}