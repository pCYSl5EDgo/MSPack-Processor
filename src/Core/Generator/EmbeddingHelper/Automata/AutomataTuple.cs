// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;

namespace MSPack.Processor.Core.Embed
{
    public readonly struct AutomataTuple
    {
        public readonly int Index;
        private readonly byte[] binary;
        public readonly FieldDefinition DataStaticFieldDefinition;
        public readonly int HeaderCount;

        public byte this[int index] => binary[HeaderCount + index];

        public int Length => binary.Length - HeaderCount;

        public AutomataTuple(int index, byte[] binary, FieldDefinition dataStaticFieldDefinition)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Index = index;
            if (binary.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.binary = binary;
            DataStaticFieldDefinition = dataStaticFieldDefinition;
            if (!dataStaticFieldDefinition.IsStatic)
            {
                throw new ArgumentException();
            }

            int CalcHeader(int length)
            {
                if (length == 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (length < 32 + 1)
                {
                    return 1;
                }

                if (length < 256 + 2)
                {
                    return 2;
                }

                if (length < 65536 + 3)
                {
                    return 3;
                }

                return 5;
            }

            HeaderCount = CalcHeader(binary.Length);
        }
    }
}