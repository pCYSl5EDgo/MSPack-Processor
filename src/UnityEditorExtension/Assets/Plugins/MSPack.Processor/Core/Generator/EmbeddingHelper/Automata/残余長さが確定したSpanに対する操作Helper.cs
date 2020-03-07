// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;

namespace MSPack.Processor.Core.Embed
{
    public static class 残余長さが確定したSpanに対する操作Helper
    {
        public static (Instruction[], Instruction[]) Embed単一の長さであると既に確認されたSpanに対して結果を確定する操作(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, int start)
        {
            var length = tuples[tuplesOffset].Length;
            switch (length)
            {
                case 1:
                    return 残余長さが1であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length1Sorter(start));
                case 2:
                    return 残余長さが2であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length2Sorter(start));
                case 3:
                    return 残余長さが3であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length3Sorter(start));
                case 4:
                    return 残余長さが4であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length4Sorter(start));
                case 5:
                    return 残余長さが5であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length5Sorter(start));
                case 6:
                    return 残余長さが6であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length6Sorter(start));
                case 7:
                    return 残余長さが7であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length7Sorter(start));
                case 8:
                    return 残余長さが8であると確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, new Length8Sorter(start));
                default:
                    var list = new List<Instruction>();
                    残余長さが8より長いと確定したSpanに対する操作Helper.Embed(tuples, tuplesOffset, tuplesCount, in options, start, length, list);
                    return (list.ToArray(), Array.Empty<Instruction>());
            }
        }
    }
}