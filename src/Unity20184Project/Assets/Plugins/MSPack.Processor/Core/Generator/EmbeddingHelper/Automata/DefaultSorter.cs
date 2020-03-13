// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public sealed class DefaultSorter : IComparer<AutomataTuple>
    {
        public int Compare(AutomataTuple x, AutomataTuple y)
        {
            if (x.Length < y.Length)
            {
                return -1;
            }

            if (x.Length > y.Length)
            {
                return 1;
            }

            for (var i = 0; i < x.Length; i++)
            {
                // ReSharper disable once PossibleNullReferenceException
                var xVal = x[i];
                // ReSharper disable once PossibleNullReferenceException
                var yVal = y[i];
                if (xVal < yVal)
                {
                    return -1;
                }

                if (xVal > yVal)
                {
                    return 1;
                }
            }

            throw new InvalidOperationException();
        }
    }
}