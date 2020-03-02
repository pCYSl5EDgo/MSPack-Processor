// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;
using MessagePack.Resolvers;
using NUnit.Framework;
using SimpleTestClasses;

namespace Core.Test
{
    [SetUpFixture]
    [TestFixture]
    public class EmptyTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            StaticCompositeResolver.Instance.Register(new IFormatterResolver[]
            {
                Resolver.Instance,
                BuiltinResolver.Instance,
            });
            var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);

            MessagePackSerializer.DefaultOptions = option;
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