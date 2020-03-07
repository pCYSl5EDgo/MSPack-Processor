// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil.Cil;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public static class 残余長さが8より長いと確定したSpanに対する操作Helper
    {
        public static void Embed(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, int start, int length, List<Instruction> list)
        {
            switch (tuplesCount)
            {
                case 1:
                    Embed1(in tuples[tuplesOffset], in options, start, length, list);
                    return;
                case 2:
                    Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, start, length, list);
                    return;
                default:
                    var sorter = new Length8Sorter(start);
                    Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
                    var segments = Divide(tuples, tuplesOffset, tuplesCount, sorter);

#if CSHARP_8_0_OR_NEWER
                    var pairs = new (Instruction[], Instruction[], Instruction, Instruction?)[segments.Length];
#else
                    var pairs = new (Instruction[], Instruction[], Instruction, Instruction)[segments.Length];
#endif

                    for (var index = 0; index < segments.Length; index++)
                    {
                        var (offset, count, value) = segments[index];
                        ref var pair = ref pairs[index];
                        (pair.Item1, pair.Item2) = 残余長さが確定したSpanに対する操作Helper.Embed単一の長さであると既に確認されたSpanに対して結果を確定する操作(tuples, offset, count, options, start + 8);
                        (pair.Item3, pair.Item4) = InstructionUtility.LdcU8(value);
                    }

                    switch (pairs.Length)
                    {
                        case 1:
                            EmbedDefault1(list, options, ref pairs[0]);
                            break;
                        case 2:
                            EmbedDefault2(list, options, ref pairs[0], ref pairs[1], options.UInt64VariableDefinition());
                            return;
                    }

                    var number = options.UInt64VariableDefinition();
                    list.Add(InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition));
                    list.Add(Instruction.Create(OpCodes.Dup));
                    list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()));
                    list.Add(Instruction.Create(OpCodes.Ldind_I8));
                    list.Add(InstructionUtility.StoreVariable(number));

                    list.Add(InstructionUtility.LdcI4(8));
                    list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.SliceStartByte()));
                    list.Add(InstructionUtility.StoreVariable(options.SpanVariableDefinition));
                    var embedded = EmbedDefault(in options, number, pairs, 0, pairs.Length);
                    list.AddRange(embedded);
                    for (var index = 0; index < pairs.Length; index++)
                    {
                        ref var pair = ref pairs[index];
                        list.AddRange(pair.Item1);
                        list.AddRange(pair.Item2);
                    }
                    return;
            }
        }
#if CSHARP_8_0_OR_NEWER
        private static Instruction[] EmbedDefault(in AutomataOption options, VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction?)[] pairs, int offset, int count)
#else
        private static Instruction[] EmbedDefault(in AutomataOption options, VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction)[] pairs, int offset, int count)
#endif
        {
            ref var middle = ref pairs[offset + (count >> 1)];
            if (middle.Item4 is null)
            {
                switch (count)
                {
                    case 1:
                        return new[]
                        {
                            InstructionUtility.LoadVariable(number),
                            middle.Item3,
                            Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                            Instruction.Create(OpCodes.Br, options.FailInstruction),
                        };
                    case 2:
                        ref var other = ref pairs[offset];
                        if (other.Item4 is null)
                        {
                            return new[]
                            {
                                InstructionUtility.LoadVariable(number),
                                middle.Item3,
                                Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                                InstructionUtility.LoadVariable(number),
                                other.Item3,
                                Instruction.Create(OpCodes.Beq, other.Item1[0]),
                                Instruction.Create(OpCodes.Br, options.FailInstruction),
                            };
                        }
                        else
                        {
                            return new[]
                            {
                                InstructionUtility.LoadVariable(number),
                                middle.Item3,
                                Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                                InstructionUtility.LoadVariable(number),
                                other.Item3,
                                other.Item4,
                                Instruction.Create(OpCodes.Beq, other.Item1[0]),
                                Instruction.Create(OpCodes.Br, options.FailInstruction),
                            };
                        }
                }

                var lesserResult = EmbedDefault(options, number, pairs, offset, count >> 1);
                var greaterResult = EmbedDefault(options, number, pairs, offset + (count >> 1) + 1, count - 1 - (count >> 1));
                return InstructionConcatHelper.Concat(
                    new[]
                    {
                        InstructionUtility.LoadVariable(number),
                        middle.Item3,
                        Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                        InstructionUtility.LoadVariable(number),
                        middle.Item3.Clone(),
                        Instruction.Create(OpCodes.Bgt, greaterResult[0]),
                    },
                    lesserResult,
                    greaterResult
                );
            }
            else
            {
                switch (count)
                {
                    case 1:
                        return new[]
                        {
                            InstructionUtility.LoadVariable(number),
                            middle.Item3,
                            middle.Item4,
                            Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                            Instruction.Create(OpCodes.Br, options.FailInstruction),
                        };
                    case 2:
                        ref var other = ref pairs[offset];
                        if (other.Item4 is null)
                        {
                            return new[]
                            {
                                InstructionUtility.LoadVariable(number),
                                middle.Item3,
                                middle.Item4,
                                Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                                InstructionUtility.LoadVariable(number),
                                other.Item3,
                                Instruction.Create(OpCodes.Beq, other.Item1[0]),
                                Instruction.Create(OpCodes.Br, options.FailInstruction),
                            };
                        }
                        else
                        {
                            return new[]
                            {
                                InstructionUtility.LoadVariable(number),
                                middle.Item3,
                                middle.Item4,
                                Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                                InstructionUtility.LoadVariable(number),
                                other.Item3,
                                other.Item4,
                                Instruction.Create(OpCodes.Beq, other.Item1[0]),
                                Instruction.Create(OpCodes.Br, options.FailInstruction),
                            };
                        }
                }
                var lesserResult = EmbedDefault(options, number, pairs, offset, count >> 1);
                var greaterResult = EmbedDefault(options, number, pairs, offset + (count >> 1) + 1, count - 1 - (count >> 1));

                return InstructionConcatHelper.Concat(
                    new[]
                    {
                        InstructionUtility.LoadVariable(number),
                        middle.Item3,
                        middle.Item4,
                        Instruction.Create(OpCodes.Beq, middle.Item1[0]),
                        InstructionUtility.LoadVariable(number),
                        middle.Item3.Clone(),
                        middle.Item4.Clone(),
                        Instruction.Create(OpCodes.Bgt, greaterResult[0]),
                    },
                    lesserResult,
                    greaterResult
                );
            }
        }

#if CSHARP_8_0_OR_NEWER
        private static void EmbedDefault2(List<Instruction> list, in AutomataOption options, ref (Instruction[], Instruction[], Instruction, Instruction?) pair0, ref (Instruction[], Instruction[], Instruction, Instruction?) pair1, VariableDefinition number)
#else
        private static void EmbedDefault2(List<Instruction> list, in AutomataOption options, ref (Instruction[], Instruction[], Instruction, Instruction) pair0, ref (Instruction[], Instruction[], Instruction, Instruction) pair1, VariableDefinition number)
#endif
        {
            list.Add(InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition));
            list.Add(Instruction.Create(OpCodes.Dup));
            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()));
            list.Add(Instruction.Create(OpCodes.Ldind_I8));
            list.Add(InstructionUtility.StoreVariable(number));

            list.Add(InstructionUtility.LdcI4(8));
            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.SliceStartByte()));
            list.Add(InstructionUtility.StoreVariable(options.SpanVariableDefinition));

            list.Add(InstructionUtility.LoadVariable(number));
            list.Add(pair0.Item3);
            if (!(pair0.Item4 is null))
            {
                list.Add(pair0.Item4);
            }

            list.Add(Instruction.Create(OpCodes.Beq, pair0.Item1[0]));

            list.Add(InstructionUtility.LoadVariable(number));
            list.Add(pair1.Item3);
            if (!(pair1.Item4 is null))
            {
                list.Add(pair1.Item4);
            }

            list.Add(Instruction.Create(OpCodes.Bne_Un, options.FailInstruction));

            list.AddRange(pair1.Item1);
            list.AddRange(pair1.Item2);

            list.AddRange(pair0.Item1);
            list.AddRange(pair0.Item2);
        }

#if CSHARP_8_0_OR_NEWER
        private static void EmbedDefault1(List<Instruction> list, in AutomataOption options, ref (Instruction[], Instruction[], Instruction, Instruction?) pair0)
#else
        private static void EmbedDefault1(List<Instruction> list, in AutomataOption options, ref (Instruction[], Instruction[], Instruction, Instruction) pair0)
#endif
        {
            list.Add(InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition));
            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()));
            list.Add(Instruction.Create(OpCodes.Ldind_I8));
            list.Add(pair0.Item3);
            if (!(pair0.Item4 is null))
            {
                list.Add(pair0.Item4);
            }

            list.Add(Instruction.Create(OpCodes.Bne_Un, options.FailInstruction));

            list.Add(InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition));
            list.Add(InstructionUtility.LdcI4(8));
            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.SliceStartByte()));
            list.Add(InstructionUtility.StoreVariable(options.SpanVariableDefinition));

            list.AddRange(pair0.Item1);
            list.AddRange(pair0.Item2);
        }

        private static (int offset, int count, ulong value)[] Divide(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, Length8Sorter sorter)
        {
            var currentValue = sorter.GetValue(tuples[tuplesOffset]);
            var segments = new (int offset, int count, ulong value)[]
            {
                (tuplesOffset, 1, currentValue),
            };

            for (var i = 1; i < tuplesCount; i++)
            {
                var value = sorter.GetValue(tuples[tuplesOffset + i]);
                if (currentValue == value)
                {
                    segments[segments.Length - 1].count++;
                    continue;
                }

                currentValue = value;
                Array.Resize(ref segments, segments.Length + 1);
                segments[segments.Length - 1] = (tuplesOffset + i, 1, currentValue);
            }

            return segments;
        }

        private static void Embed1(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, int start, int length, List<Instruction> list)
        {
            var ctorPointerByte = options.ReadOnlySpanHelper.CtorPointerByte();

            list.Add(InstructionUtility.LoadVariable(options.SpanVariableDefinition));

            list.Add(Instruction.Create(OpCodes.Ldsflda, tuple0.DataStaticFieldDefinition));
            list.Add(InstructionUtility.LdcI4(start + tuple0.HeaderOffset));
            list.Add(Instruction.Create(OpCodes.Add));
            list.Add(InstructionUtility.LdcI4(length - start));
            list.Add(Instruction.Create(OpCodes.Newobj, ctorPointerByte));

            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.OpEqualityByte()));
            list.Add(Instruction.Create(OpCodes.Brtrue, tuple0.Destination));
            list.Add(Instruction.Create(OpCodes.Br, options.FailInstruction));
        }

        private static void Embed2(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, int start, int length, List<Instruction> list)
        {
            var sameLength = SameLength(tuple0.Binary, tuple1.Binary, start);
            if (sameLength == 0)
            {
                Embed2Different(tuple0, tuple1, options, start, length, list);
            }
            else
            {
                Embed2Same(tuple0, tuple1, options, start, length, sameLength, list);
            }
        }

        private static void Embed2Same(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, int start, int length, int sameLength, List<Instruction> list)
        {
            var ctorPointerByte = options.ReadOnlySpanHelper.CtorPointerByte();

            var spanVariableDefinition = options.SpanVariableDefinition;
            list.Add(InstructionUtility.LoadVariableAddress(spanVariableDefinition));
            list.Add(InstructionUtility.LdcI4(0));
            list.Add(InstructionUtility.LdcI4(sameLength));
            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.SliceStartLengthByte()));

            list.Add(Instruction.Create(OpCodes.Ldsflda, tuple0.DataStaticFieldDefinition));
            list.Add(InstructionUtility.LdcI4(start + tuple0.HeaderOffset));
            list.Add(Instruction.Create(OpCodes.Add));
            list.Add(InstructionUtility.LdcI4(sameLength));
            list.Add(Instruction.Create(OpCodes.Newobj, ctorPointerByte));

            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.OpEqualityByte()));
            list.Add(Instruction.Create(OpCodes.Brfalse, options.FailInstruction));

            list.Add(InstructionUtility.LoadVariableAddress(spanVariableDefinition));
            list.Add(InstructionUtility.LdcI4(sameLength));
            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.SliceStartByte()));
            list.Add(Instruction.Create(OpCodes.Dup));
            list.Add(InstructionUtility.StoreVariable(spanVariableDefinition));

            list.Add(Instruction.Create(OpCodes.Ldsflda, tuple0.DataStaticFieldDefinition));
            list.Add(InstructionUtility.LdcI4(start + sameLength + tuple0.HeaderOffset));
            list.Add(Instruction.Create(OpCodes.Add));
            list.Add(InstructionUtility.LdcI4(length - start - sameLength));
            list.Add(Instruction.Create(OpCodes.Newobj, ctorPointerByte));

            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.OpEqualityByte()));
            list.Add(Instruction.Create(OpCodes.Brtrue, tuple0.Destination));

            list.Add(InstructionUtility.LoadVariable(options.SpanVariableDefinition));

            list.Add(Instruction.Create(OpCodes.Ldsflda, tuple1.DataStaticFieldDefinition));
            list.Add(InstructionUtility.LdcI4(start + sameLength + tuple1.HeaderOffset));
            list.Add(Instruction.Create(OpCodes.Add));
            list.Add(InstructionUtility.LdcI4(length - start - sameLength));
            list.Add(Instruction.Create(OpCodes.Newobj, ctorPointerByte));

            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.OpEqualityByte()));
            list.Add(Instruction.Create(OpCodes.Brtrue, tuple1.Destination));

            list.Add(Instruction.Create(OpCodes.Br, options.FailInstruction));
        }

        private static void Embed2Different(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, int start, int length, List<Instruction> list)
        {
            var ctorPointerByte = options.ReadOnlySpanHelper.CtorPointerByte();

            list.Add(InstructionUtility.LoadVariable(options.SpanVariableDefinition));

            list.Add(Instruction.Create(OpCodes.Ldsflda, tuple0.DataStaticFieldDefinition));
            list.Add(InstructionUtility.LdcI4(start + tuple0.HeaderOffset));
            list.Add(Instruction.Create(OpCodes.Add));
            list.Add(InstructionUtility.LdcI4(length - start));
            list.Add(Instruction.Create(OpCodes.Newobj, ctorPointerByte));

            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.OpEqualityByte()));
            list.Add(Instruction.Create(OpCodes.Brtrue, tuple0.Destination));

            list.Add(InstructionUtility.LoadVariable(options.SpanVariableDefinition));

            list.Add(Instruction.Create(OpCodes.Ldsflda, tuple1.DataStaticFieldDefinition));
            list.Add(InstructionUtility.LdcI4(start + tuple1.HeaderOffset));
            list.Add(Instruction.Create(OpCodes.Add));
            list.Add(InstructionUtility.LdcI4(length - start));
            list.Add(Instruction.Create(OpCodes.Newobj, ctorPointerByte));

            list.Add(Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.OpEqualityByte()));
            list.Add(Instruction.Create(OpCodes.Brtrue, tuple1.Destination));

            list.Add(Instruction.Create(OpCodes.Br, options.FailInstruction));
        }

        private static int SameLength(byte[] array0, byte[] array1, int start)
        {
            for (var i = start; i < array0.Length; i++)
            {
                if (array0[i] != array1[i])
                {
                    return i - start;
                }
            }

            return array0.Length - start;
        }
    }
}
