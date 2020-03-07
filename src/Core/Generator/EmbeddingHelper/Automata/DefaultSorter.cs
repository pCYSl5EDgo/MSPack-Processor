// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public sealed class DefaultSorter : IComparer<BinaryFieldDestinationTuple>
    {
        public int Compare(BinaryFieldDestinationTuple x, BinaryFieldDestinationTuple y)
        {
            if (x.Length < y.Length)
            {
                return -1;
            }

            if (x.Length > y.Length)
            {
                return 1;
            }

            for (var i = 0; i < x.Binary.Length; i++)
            {
                var xVal = x.Binary[i];
                var yVal = y.Binary[i];
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