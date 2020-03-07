// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public interface ILengthSorter
    {
        ulong GetValue(in BinaryFieldDestinationTuple destinationTuple);
    }

    public sealed class Length1Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length1Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

    public sealed class Length2Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length2Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            answer |= (ulong)binary[start + 1] << 8;
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start + 1];
            yVal = y.Binary[start + 1];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

    public sealed class Length3Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length3Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            answer |= (ulong)binary[start + 1] << 8;
            answer |= (ulong)binary[start + 2] << 16;
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start + 2];
            yVal = y.Binary[start + 2];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 1];
            yVal = y.Binary[start + 1];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

    public sealed class Length4Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length4Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            answer |= (ulong)binary[start + 1] << 8;
            answer |= (ulong)binary[start + 2] << 16;
            answer |= (ulong)binary[start + 3] << 24;
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start + 3];
            yVal = y.Binary[start + 3];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 2];
            yVal = y.Binary[start + 2];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 1];
            yVal = y.Binary[start + 1];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

    public sealed class Length5Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length5Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            answer |= (ulong)binary[start + 1] << 8;
            answer |= (ulong)binary[start + 2] << 16;
            answer |= (ulong)binary[start + 3] << 24;
            answer |= (ulong)binary[start + 4] << 32;
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start + 4];
            yVal = y.Binary[start + 4];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 3];
            yVal = y.Binary[start + 3];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 2];
            yVal = y.Binary[start + 2];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 1];
            yVal = y.Binary[start + 1];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

    public sealed class Length6Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length6Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            answer |= (ulong)binary[start + 1] << 8;
            answer |= (ulong)binary[start + 2] << 16;
            answer |= (ulong)binary[start + 3] << 24;
            answer |= (ulong)binary[start + 4] << 32;
            answer |= (ulong)binary[start + 5] << 40;
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start + 5];
            yVal = y.Binary[start + 5];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 4];
            yVal = y.Binary[start + 4];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 3];
            yVal = y.Binary[start + 3];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 2];
            yVal = y.Binary[start + 2];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 1];
            yVal = y.Binary[start + 1];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

    public sealed class Length7Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length7Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            answer |= (ulong)binary[start + 1] << 8;
            answer |= (ulong)binary[start + 2] << 16;
            answer |= (ulong)binary[start + 3] << 24;
            answer |= (ulong)binary[start + 4] << 32;
            answer |= (ulong)binary[start + 5] << 40;
            answer |= (ulong)binary[start + 6] << 48;
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start + 6];
            yVal = y.Binary[start + 6];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 5];
            yVal = y.Binary[start + 5];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 4];
            yVal = y.Binary[start + 4];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 3];
            yVal = y.Binary[start + 3];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 2];
            yVal = y.Binary[start + 2];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 1];
            yVal = y.Binary[start + 1];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

    public sealed class Length8Sorter : IComparer<BinaryFieldDestinationTuple>, ILengthSorter
    {
        private readonly int start;

        public Length8Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in BinaryFieldDestinationTuple destinationTuple)
        {
            var binary = destinationTuple.Binary;
            ulong answer = binary[start];
            answer |= (ulong)binary[start + 1] << 8;
            answer |= (ulong)binary[start + 2] << 16;
            answer |= (ulong)binary[start + 3] << 24;
            answer |= (ulong)binary[start + 4] << 32;
            answer |= (ulong)binary[start + 5] << 40;
            answer |= (ulong)binary[start + 6] << 48;
            answer |= (ulong)binary[start + 7] << 56;
            return answer;
        }

        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            byte xVal, yVal;
            xVal = x.Binary[start + 7];
            yVal = y.Binary[start + 7];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 6];
            yVal = y.Binary[start + 6];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 5];
            yVal = y.Binary[start + 5];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 4];
            yVal = y.Binary[start + 4];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 3];
            yVal = y.Binary[start + 3];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 2];
            yVal = y.Binary[start + 2];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start + 1];
            yVal = y.Binary[start + 1];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            xVal = x.Binary[start];
            yVal = y.Binary[start];
            if (xVal > yVal)
            {
                return 1;
            }

            if (xVal < yVal)
            {
                return -1;
            }

            return 0;
        }

        public ArraySegment<BinaryFieldDestinationTuple> Where(ArraySegment<BinaryFieldDestinationTuple> sortedArray, ulong value)
        {
            var startIndex = sortedArray.Offset;
            var array = sortedArray.Array;
            if (array is null)
            {
                throw new NullReferenceException();
            }

            for (; startIndex < array.Length; startIndex++)
            {
                if (GetValue(array[startIndex]) == value)
                {
                    break;
                }
            }

            var endExclude = startIndex + 1;
            for (; endExclude < array.Length; endExclude++)
            {
                if (GetValue(array[endExclude]) != value)
                {
                    break;
                }
            }

            return new ArraySegment<BinaryFieldDestinationTuple>(array, startIndex, endExclude - start);
        }
    }

}