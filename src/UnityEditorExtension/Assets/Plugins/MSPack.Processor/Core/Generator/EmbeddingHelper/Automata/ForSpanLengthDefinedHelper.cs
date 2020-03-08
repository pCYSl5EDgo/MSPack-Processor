// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public static class ForSpanLengthDefinedHelper
    {
        public static (Instruction[], Instruction[]) EmbedForSpanLengthDefined(AutomataTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, int start)
        {
            switch (tuples[tuplesOffset].Length - start)
            {
                case 1:
                    return ForSpanLength1Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length1Sorter(start));
                case 2:
                    return ForSpanLength2Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length2Sorter(start));
                case 3:
                    return ForSpanLength3Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length3Sorter(start));
                case 4:
                    return ForSpanLength4Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length4Sorter(start));
                case 5:
                    return ForSpanLength5Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length5Sorter(start));
                case 6:
                    return ForSpanLength6Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length6Sorter(start));
                case 7:
                    return ForSpanLength7Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length7Sorter(start));
                case 8:
                    return ForSpanLength8Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length8Sorter(start));
                default:
                    var list = new List<Instruction>();
                    ForSpanLengthMoreThan8Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, start, list);
                    return (list.ToArray(), Array.Empty<Instruction>());
            }
        }
    }
}