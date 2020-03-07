// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using System;

namespace MSPack.Processor.Core.Embed
{
    public readonly struct BinaryFieldDestinationTuple
    {
        public readonly byte[] Binary;
        public readonly FieldDefinition DataStaticFieldDefinition;
        public readonly Instruction Destination;

        public int Length => Binary.Length;
        public int HeaderOffset => EmbeddedStringHelper.CalcHeaderCount(Length);

        public BinaryFieldDestinationTuple(byte[] binary, FieldDefinition dataStaticFieldDefinition, Instruction destination)
        {
            if (binary.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Binary = binary;
            DataStaticFieldDefinition = dataStaticFieldDefinition;
            if (!dataStaticFieldDefinition.IsStatic)
            {
                throw new ArgumentException();
            }

            Destination = destination;
        }
    }
}