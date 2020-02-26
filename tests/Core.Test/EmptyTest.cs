using NUnit.Framework;
using SimpleTestClasses;
using MessagePack;

namespace Core.Test
{
    [TestFixture]
    public class EmptyTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private void Test<T>()
            where T : class, new()
        {
            var value = new T();
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<T>(bytes);
            Assert.False(other is null);
        }

        [Test]
        public void StructTest()
        {
            var value = new EmptyStruct();
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<EmptyStruct>(bytes);
            Assert.True(value.Equals(other));
        }

        [Test]
        public void ClassTest() => Test<EmptyClass>();

        [Test]
        public void SealedClassTest() => Test<EmptySealedClass>();
    }
}