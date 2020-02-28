// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace MSPack.Processor.Core.Embed
{
    public static class EmbeddedStringHelper
    {
        private static int CalcPlusCount(int length)
        {
            if (length < 32)
            {
                return 1;
            }

            if (length < 256)
            {
                return 2;
            }

            return length < 65536 ? 3 : 5;
        }

        public static byte[] Encode(string value)
        {
            var length = NoBomUtf8Encoder.Encoding.GetByteCount(value);

            var plus = CalcPlusCount(length);
            var answer = new byte[length + plus];
            NoBomUtf8Encoder.Encoding.GetBytes(value, 0, value.Length, answer, plus);

            switch (plus)
            {
                case 1:
                    answer[0] = (byte)(0xa0 | length);
                    break;
                case 2:
                    answer[0] = 217;
                    answer[1] = (byte)length;
                    break;
                case 3:
                    answer[0] = 218;
                    answer[1] = (byte)(length >> 8);
                    answer[2] = (byte)length;
                    break;
                case 5:
                    answer[0] = 219;
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
