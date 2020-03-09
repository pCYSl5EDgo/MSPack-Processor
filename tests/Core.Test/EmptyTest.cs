// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;
using MessagePack.Resolvers;
using NUnit.Framework;

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
                StandardResolver.Instance,
            });
            var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);

            MessagePackSerializer.DefaultOptions = option;
        }
    }
}