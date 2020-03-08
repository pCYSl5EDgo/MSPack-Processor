// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public static class EmbeddedAutomata
    {
        public static Instruction[] Embed(AutomataTuple[] tuples, in AutomataOption options)
        {
            Array.Sort(tuples, new DefaultSorter());
            switch (tuples.Length)
            {
                case 0: throw new ArgumentException();
                case 1:
                    return EmbedOne(in tuples[0], in options);
                case 2:
                    return tuples[0].Length == tuples[1].Length
                        ? Embed2SameLength(in tuples[0], in tuples[1], in options)
                        : Embed2Different(in tuples[0], in tuples[1], in options);
                default:
                    var variationCount = 1;
                    var oldLength = tuples[0].Length;
                    for (var index = 1; index < tuples.Length; index++)
                    {
                        ref readonly var tuple = ref tuples[index];
                        if (tuple.Length == oldLength)
                        {
                            continue;
                        }

                        oldLength = tuple.Length;
                        variationCount++;
                    }

                    switch (variationCount)
                    {
                        case 1:
                            var spanの長さが間違いなく指定した数値であることを確認する = Spanの長さが間違いなく指定した数値であることを確認する(options, tuples[0].Length);
                            var (item1, item2) = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, 0, tuples.Length, in options, 0);
                            return InstructionConcatHelper.Concat(
                                spanの長さが間違いなく指定した数値であることを確認する,
                                item1,
                                item2
                            );
                        case 2:
                            return InstructionConcatHelper.Concat(EmbedMulti2Lengths(tuples, in options));
                        default:
                            return EmbedMultiSwitch(tuples, in options);
                    }
            }
        }

        private static (Instruction[], Instruction[], Instruction[], Instruction[], Instruction[], Instruction[]) EmbedMulti2Lengths(AutomataTuple[] tuples, in AutomataOption options)
        {
            var lesserLength = tuples[0].Length;
            var greaterLength = tuples[tuples.Length - 1].Length;
            var lengthVariable = options.Int32VariableDefinition();
            var lesserCount = 1;
            for (var i = 1; i < tuples.Length; i++)
            {
                if (lesserLength != tuples[i].Length)
                {
                    lesserCount = i;
                }
            }
            var 短い方の探索結果 = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, 0, lesserCount, options, 0);
            var 長い方の探索結果 = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, lesserCount, tuples.Length - lesserCount, options, 0);
            var 初期探索結果 = new[]
            {
                InstructionUtility.LoadVariable(lengthVariable),
                InstructionUtility.LdcI4(lesserLength),
                Instruction.Create(OpCodes.Beq_S, 短い方の探索結果.Item1[0]),
                InstructionUtility.LoadVariable(lengthVariable),
                InstructionUtility.LdcI4(greaterLength),
                Instruction.Create(OpCodes.Beq, 長い方の探索結果.Item1[0]),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
            var spanの長さを格納する = Spanの長さを格納する(options);
            return (spanの長さを格納する, 初期探索結果, 短い方の探索結果.Item1, 短い方の探索結果.Item2, 長い方の探索結果.Item1, 長い方の探索結果.Item2);
        }

        private static Instruction[] EmbedMultiSwitch(AutomataTuple[] tuples, in AutomataOption options)
        {
            var shortestLength = tuples[0].Length;
            var switchTable = new Instruction[tuples[tuples.Length - 1].Length - shortestLength + 1];
            for (var i = 0; i < switchTable.Length; i++)
            {
                switchTable[i] = options.FailInstruction;
            }

            var list = new List<Instruction>
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetLengthByte()),
                InstructionUtility.LdcI4(shortestLength),
                Instruction.Create(OpCodes.Sub),
                Instruction.Create(OpCodes.Switch, switchTable),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
            List追加(tuples, options, shortestLength, switchTable, list);
            return list.ToArray();
        }

        private static void List追加(AutomataTuple[] tuples, in AutomataOption options, int shortestLength, Instruction[] switchTable, List<Instruction> list)
        {
            var tuplesOffset = 0;
            var tuplesCount = 1;
            var oldLength = shortestLength;
            while (tuplesOffset + tuplesCount != tuples.Length)
            {
                ref readonly var nextTuple = ref tuples[tuplesOffset + tuplesCount];
                if (nextTuple.Length == oldLength)
                {
                    tuplesCount++;
                }
                else
                {
                    List追加(options, switchTable, tuples, list, nextTuple, shortestLength, ref tuplesOffset, ref tuplesCount, ref oldLength);
                }
            }

            List追加(options, switchTable, tuples, list, shortestLength, tuplesOffset, tuplesCount, oldLength);
        }

        private static void List追加(in AutomataOption options, Instruction[] switchTable, AutomataTuple[] tuples, List<Instruction> list, in AutomataTuple nextTuple, int shortestLength, ref int tuplesOffset, ref int tuplesCount, ref int oldLength)
        {
            List追加(options, switchTable, tuples, list, shortestLength, tuplesOffset, tuplesCount, oldLength);
            oldLength = nextTuple.Length;
            tuplesOffset += tuplesCount;
            tuplesCount = 1;
        }

        private static void List追加(in AutomataOption options, Instruction[] switchTable, in AutomataTuple[] tuples, List<Instruction> list, int shortestLength, int tuplesOffset, int tuplesCount, int oldLength)
        {
            var (item1, item2) = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, tuplesOffset, tuplesCount, options, 0);
            switchTable[oldLength - shortestLength] = item1[0];
            list.AddRange(item1);
            list.AddRange(item2);
        }

        private static Instruction[] Spanの長さを格納する(in this AutomataOption options)
        {
            var getLengthByte = options.ReadOnlySpanHelper.GetLengthByte();
            var int32Variable = options.Int32VariableDefinition();
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, getLengthByte),
                InstructionUtility.StoreVariable(int32Variable),
            };
        }

        private static Instruction[] Spanの長さが間違いなく指定した数値であることを確認する(in this AutomataOption options, int length)
        {
            var getLengthByte = options.ReadOnlySpanHelper.GetLengthByte();
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, getLengthByte),
                InstructionUtility.LdcI4(length),
                Instruction.Create(OpCodes.Bne_Un, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2Different(in AutomataTuple first, in AutomataTuple second, in AutomataOption options)
        {
            var ctor = options.ReadOnlySpanHelper.CtorPointerByte();
            var op = options.ReadOnlySpanHelper.OpEqualityByte();

            return new[]
            {
                InstructionUtility.LoadVariable(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Ldsflda, first.DataStaticFieldDefinition),
                InstructionUtility.LdcI4(first.Length),
                Instruction.Create(OpCodes.Newobj, ctor),
                Instruction.Create(OpCodes.Call, op),
                Instruction.Create(OpCodes.Brtrue, first.Destination),

                InstructionUtility.LoadVariable(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Ldsflda, second.DataStaticFieldDefinition),
                InstructionUtility.LdcI4(second.Length),
                Instruction.Create(OpCodes.Newobj, ctor),
                Instruction.Create(OpCodes.Call, op),
                Instruction.Create(OpCodes.Brtrue, second.Destination),

                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2SameLength(in AutomataTuple first, in AutomataTuple second, in AutomataOption options)
        {
            var sameLength = SameLength(first.Binary, second.Binary);
            if (sameLength == 0)
            {
                return Embed2Different(in first, in second, in options);
            }

            var ctor = options.ReadOnlySpanHelper.CtorPointerByte();
            var op = options.ReadOnlySpanHelper.OpEqualityByte();
            var sliceStartLength = options.ReadOnlySpanHelper.SliceStartLengthByte();
            var sliceStart = options.ReadOnlySpanHelper.SliceStartByte();

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                InstructionUtility.LdcI4(0),
                InstructionUtility.LdcI4(sameLength),
                Instruction.Create(OpCodes.Call, sliceStartLength),
                Instruction.Create(OpCodes.Ldsflda, first.DataStaticFieldDefinition),
                InstructionUtility.LdcI4(sameLength),
                Instruction.Create(OpCodes.Newobj, ctor),
                Instruction.Create(OpCodes.Call, op),
                Instruction.Create(OpCodes.Brfalse, options.FailInstruction),

                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                InstructionUtility.LdcI4(sameLength),
                Instruction.Create(OpCodes.Call, sliceStart),
                Instruction.Create(OpCodes.Ldsflda, first.DataStaticFieldDefinition),
                InstructionUtility.LdcI4(sameLength),
                Instruction.Create(OpCodes.Add),
                InstructionUtility.LdcI4(first.Length - sameLength),
                Instruction.Create(OpCodes.Newobj, ctor),
                Instruction.Create(OpCodes.Call, op),
                Instruction.Create(OpCodes.Brtrue, first.Destination),

                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                InstructionUtility.LdcI4(sameLength),
                Instruction.Create(OpCodes.Call, sliceStart),
                Instruction.Create(OpCodes.Ldsflda, second.DataStaticFieldDefinition),
                InstructionUtility.LdcI4(sameLength),
                Instruction.Create(OpCodes.Add),
                InstructionUtility.LdcI4(second.Length - sameLength),
                Instruction.Create(OpCodes.Newobj, ctor),
                Instruction.Create(OpCodes.Call, op),
                Instruction.Create(OpCodes.Brtrue, second.Destination),

                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] EmbedOne(in AutomataTuple tuple, in AutomataOption options)
        {
            return new[]
            {
                InstructionUtility.LoadVariable(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Ldsflda, tuple.DataStaticFieldDefinition),
                InstructionUtility.LdcI4(tuple.Length),
                Instruction.Create(OpCodes.Newobj, options.ReadOnlySpanHelper.CtorPointerByte()),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.OpEqualityByte()),
                Instruction.Create(OpCodes.Brtrue, tuple.Destination),

                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static int SameLength(byte[] array0, byte[] array1)
        {
            for (var i = 0; i < array0.Length; i++)
            {
                if (array0[i] != array1[i])
                {
                    return i;
                }
            }

            return array0.Length;
        }
    }
}