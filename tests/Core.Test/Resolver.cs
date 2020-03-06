// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;
using MessagePack.Formatters;
using System;

namespace Core.Test
{
    public sealed class Resolver : IFormatterResolver
    {
        private Span<byte> _() => Span<byte>.Empty;

        public static readonly Resolver Instance = new Resolver();

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}