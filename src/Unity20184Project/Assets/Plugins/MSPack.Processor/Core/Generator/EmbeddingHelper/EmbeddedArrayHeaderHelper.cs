// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace MSPack.Processor.Core.Embed
{
    public static class EmbeddedArrayHeaderHelper
    {
        private static int CalcPlusCount(int length)
        {
            if (length < 16)
            {
                return 1;
            }

            return length < 65536 ? 3 : 5;
        }

        public static byte[] Encode(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, "Array Header length should not be less than zero.");
            }

            var plus = CalcPlusCount(length);
            var answer = new byte[plus];
            switch (plus)
            {
                case 1:
                    const byte mask = 0b10010000;
                    answer[0] = (byte)(mask | length);
                    break;
                case 3:
                    answer[0] = 220;
                    answer[1] = (byte)(length >> 8);
                    answer[2] = (byte)length;
                    break;
                case 5:
                    answer[0] = 221;
                    answer[1] = (byte)(length >> 24);
                    answer[2] = (byte)(length >> 16);
                    answer[3] = (byte)(length >> 8);
                    answer[4] = (byte)length;
                    break;
                default:
                    throw new InvalidProgramException();
            }

            return answer;
        }
    }
}
