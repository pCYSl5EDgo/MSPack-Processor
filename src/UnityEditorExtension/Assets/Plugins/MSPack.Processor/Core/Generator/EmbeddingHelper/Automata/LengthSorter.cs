// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public interface ILengthSorter
    {
        ulong GetValue(in AutomataTuple tuple);
    }

    public sealed class Length1Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length1Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

    public sealed class Length2Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length2Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            answer |= (ulong)tuple[start + 1] << 8;
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 1];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 1];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

    public sealed class Length3Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length3Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            answer |= (ulong)tuple[start + 1] << 8;
            answer |= (ulong)tuple[start + 2] << 16;
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 2];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 2];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 1];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 1];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

    public sealed class Length4Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length4Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            answer |= (ulong)tuple[start + 1] << 8;
            answer |= (ulong)tuple[start + 2] << 16;
            answer |= (ulong)tuple[start + 3] << 24;
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 3];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 3];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 2];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 2];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 1];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 1];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

    public sealed class Length5Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length5Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            answer |= (ulong)tuple[start + 1] << 8;
            answer |= (ulong)tuple[start + 2] << 16;
            answer |= (ulong)tuple[start + 3] << 24;
            answer |= (ulong)tuple[start + 4] << 32;
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 4];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 4];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 3];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 3];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 2];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 2];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 1];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 1];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

    public sealed class Length6Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length6Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            answer |= (ulong)tuple[start + 1] << 8;
            answer |= (ulong)tuple[start + 2] << 16;
            answer |= (ulong)tuple[start + 3] << 24;
            answer |= (ulong)tuple[start + 4] << 32;
            answer |= (ulong)tuple[start + 5] << 40;
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 5];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 5];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 4];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 4];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 3];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 3];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 2];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 2];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 1];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 1];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

    public sealed class Length7Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length7Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            answer |= (ulong)tuple[start + 1] << 8;
            answer |= (ulong)tuple[start + 2] << 16;
            answer |= (ulong)tuple[start + 3] << 24;
            answer |= (ulong)tuple[start + 4] << 32;
            answer |= (ulong)tuple[start + 5] << 40;
            answer |= (ulong)tuple[start + 6] << 48;
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 6];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 6];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 5];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 5];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 4];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 4];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 3];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 3];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 2];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 2];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 1];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 1];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

    public sealed class Length8Sorter : IComparer<AutomataTuple>, ILengthSorter
    {
        private readonly int start;

        public Length8Sorter(int start)
        {
            this.start = start;
        }

        public ulong GetValue(in AutomataTuple tuple)
        {
            ulong answer = tuple[start];
            answer |= (ulong)tuple[start + 1] << 8;
            answer |= (ulong)tuple[start + 2] << 16;
            answer |= (ulong)tuple[start + 3] << 24;
            answer |= (ulong)tuple[start + 4] << 32;
            answer |= (ulong)tuple[start + 5] << 40;
            answer |= (ulong)tuple[start + 6] << 48;
            answer |= (ulong)tuple[start + 7] << 56;
            return answer;
        }

        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            var c0 = CompareImpl(x, y);
            var c1 = CompareImpl(y, x);
            if (c0 == 0)
            {
                if (c1 != 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }
            else
            {
                if (c0 * c1 >= 0)
                {
                    throw new System.Exception(x.Index + " , " + y.Index);
                }
            }

            return c0;
        }

        private int CompareImpl(in AutomataTuple x, in AutomataTuple y)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            byte xVal;
            // ReSharper disable once JoinDeclarationAndInitializer
            byte yVal;
            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 7];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 7];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 6];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 6];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 5];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 5];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 4];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 4];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 3];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 3];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 2];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 2];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start + 1];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start + 1];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            // ReSharper disable once PossibleNullReferenceException
            xVal = x[start];
            // ReSharper disable once PossibleNullReferenceException
            yVal = y[start];
            if(xVal > yVal)
            {
                return 1;
            }

            if(xVal < yVal)
            {
                return -1;
            }

            return 0;
        }
    }

}