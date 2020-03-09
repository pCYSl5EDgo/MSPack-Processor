// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public static class ForSpanLengthMoreThan8Helper
    {
        public static void Embed(AutomataTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption option, int start, List<Instruction> list)
        {
            switch (tuplesCount)
            {
                case 1:
                    Embed1(in tuples[tuplesOffset], in option, start, list);
                    return;
                case 2:
                    Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in option, start, list);
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
                        (pair.Item1, pair.Item2) = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, offset, count, option, start + 8);
                        (pair.Item3, pair.Item4) = InstructionUtility.LdcU8(value);
                    }

                    switch (pairs.Length)
                    {
                        case 1:
                            EmbedDefault1(list, option, ref pairs[0]);
                            return;
                        case 2:
                            EmbedDefault2(list, option, ref pairs[0], ref pairs[1], option.UInt64VariableDefinition());
                            return;
                        default:
                            var number = option.UInt64VariableDefinition();
                            list.Add(InstructionUtility.LoadAddress(option.Span));
                            list.Add(Instruction.Create(OpCodes.Dup));
                            list.Add(Instruction.Create(OpCodes.Call, option.ReadOnlySpanHelper.GetPinnableReferenceByte()));
                            list.Add(Instruction.Create(OpCodes.Ldind_I8));
                            list.Add(InstructionUtility.Store(number));

                            list.Add(InstructionUtility.LdcI4(8));
                            list.Add(Instruction.Create(OpCodes.Call, option.ReadOnlySpanHelper.SliceStartByte()));
                            list.Add(InstructionUtility.Store(option.Span));
                            var embedded = EmbedDefault(number, pairs, 0, pairs.Length);
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
        }

#if CSHARP_8_0_OR_NEWER
        private static Instruction[] EmbedDefault(VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction?)[] pairs, int offset, int count)
#else
        private static Instruction[] EmbedDefault(VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction)[] pairs, int offset, int count)
#endif
        {
            var (middleProcess, _, middleLoad, middleConv) = pairs[offset + (count >> 1)];
            var middleProcessStart = middleProcess[0];

            if (middleConv is null)
            {
                return EmbedDefaultDefaultMiddleConvNull(number, pairs, offset, count, middleLoad, middleProcessStart);
            }

            return EmbedDefaultDefault(number, pairs, offset, count, middleLoad, middleConv, middleProcessStart);
        }

#if CSHARP_8_0_OR_NEWER
        private static Instruction[] EmbedDefaultDefault(VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction?)[] pairs, int offset, int count, Instruction middleLoad, Instruction middleConv, Instruction middleProcessStart)
#else
        private static Instruction[] EmbedDefaultDefault(VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction)[] pairs, int offset, int count, Instruction middleLoad, Instruction middleConv, Instruction middleProcessStart)
#endif
        {
            switch (count)
            {
                case 1:
                    return new[]
                    {
                    InstructionUtility.Load(number),
                    middleLoad,
                    middleConv,
                    Instruction.Create(OpCodes.Beq, middleProcessStart),
                    InstructionUtility.LdcI4(-1),
                    Instruction.Create(OpCodes.Ret),
                };
                case 2:
                    ref var other = ref pairs[offset];
                    if (other.Item4 is null)
                    {
                        return new[]
                        {
                        InstructionUtility.Load(number),
                        middleLoad,
                        middleConv,
                        Instruction.Create(OpCodes.Beq, middleProcessStart),
                        InstructionUtility.Load(number),
                        other.Item3,
                        Instruction.Create(OpCodes.Beq, other.Item1[0]),
                        InstructionUtility.LdcI4(-1),
                        Instruction.Create(OpCodes.Ret),
                    };
                    }
                    else
                    {
                        return new[]
                        {
                        InstructionUtility.Load(number),
                        middleLoad,
                        middleConv,
                        Instruction.Create(OpCodes.Beq, middleProcessStart),
                        InstructionUtility.Load(number),
                        other.Item3,
                        other.Item4,
                        Instruction.Create(OpCodes.Beq, other.Item1[0]),
                        InstructionUtility.LdcI4(-1),
                        Instruction.Create(OpCodes.Ret),
                    };
                    }
                default:
                    var lesserResult = EmbedDefault(number, pairs, offset, count >> 1);
                    var greaterResult = EmbedDefault(number, pairs, offset + (count >> 1) + 1, count - 1 - (count >> 1));

                    return InstructionConcatHelper.Concat(
                        new[]
                        {
                        InstructionUtility.Load(number),
                        middleLoad,
                        middleConv,
                        Instruction.Create(OpCodes.Beq, middleProcessStart),
                        InstructionUtility.Load(number),
                        middleLoad.Clone(),
                        middleConv.Clone(),
                        Instruction.Create(OpCodes.Bgt, greaterResult[0]),
                        },
                        lesserResult,
                        greaterResult
                    );
            }
        }

#if CSHARP_8_0_OR_NEWER
        private static Instruction[] EmbedDefaultDefaultMiddleConvNull(VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction?)[] pairs, int offset, int count, Instruction middleLoad, Instruction middleProcessStart)
#else
        private static Instruction[] EmbedDefaultDefaultMiddleConvNull(VariableDefinition number, (Instruction[], Instruction[], Instruction, Instruction)[] pairs, int offset, int count, Instruction middleLoad, Instruction middleProcessStart)
#endif
        {
            switch (count)
            {
                case 1:
                    return EmbedDefaultDefault1(number, middleLoad, middleProcessStart);
                case 2:
                    ref var other = ref pairs[offset];
                    var otherLoad = other.Item3;
                    var otherProcessStart = other.Item1[0];
                    var otherConv = other.Item4;
                    return EmbedDefaultDefault2(number, otherConv, middleLoad, middleProcessStart, otherLoad, otherProcessStart);
                default:
                    var lesserResult = EmbedDefault(number, pairs, offset, count >> 1);
                    var greaterResult = EmbedDefault(number, pairs, offset + (count >> 1) + 1, count - 1 - (count >> 1));
                    return InstructionConcatHelper.Concat(
                        new[]
                        {
                        InstructionUtility.Load(number),
                        middleLoad,
                        Instruction.Create(OpCodes.Beq, middleProcessStart),
                        InstructionUtility.Load(number),
                        middleLoad.Clone(),
                        Instruction.Create(OpCodes.Bgt, greaterResult[0]),
                        },
                        lesserResult,
                        greaterResult
                    );
            }
        }

#if CSHARP_8_0_OR_NEWER
        private static Instruction[] EmbedDefaultDefault2(VariableDefinition number, Instruction? otherConv, Instruction middleLoad, Instruction middleProcessStart, Instruction otherLoad, Instruction otherProcessStart)
#else
        private static Instruction[] EmbedDefaultDefault2(VariableDefinition number, Instruction otherConv, Instruction middleLoad, Instruction middleProcessStart, Instruction otherLoad, Instruction otherProcessStart)
#endif
        {
            if (otherConv is null)
            {
                return new[]
                {
                    InstructionUtility.Load(number),
                    middleLoad,
                    Instruction.Create(OpCodes.Beq, middleProcessStart),
                    InstructionUtility.Load(number),
                    otherLoad,
                    Instruction.Create(OpCodes.Beq, otherProcessStart),
                    InstructionUtility.LdcI4(-1),
                    Instruction.Create(OpCodes.Ret),
                };
            }

            return new[]
            {
                InstructionUtility.Load(number),
                middleLoad,
                Instruction.Create(OpCodes.Beq, middleProcessStart),
                InstructionUtility.Load(number),
                otherLoad,
                otherConv,
                Instruction.Create(OpCodes.Beq, otherProcessStart),
                InstructionUtility.LdcI4(-1),
                Instruction.Create(OpCodes.Ret),
            };
        }

        private static Instruction[] EmbedDefaultDefault1(VariableDefinition number, Instruction middleLoad, Instruction middleProcessStart)
        {
            return new[]
            {
                InstructionUtility.Load(number),
                middleLoad,
                Instruction.Create(OpCodes.Beq, middleProcessStart),
                InstructionUtility.LdcI4(-1),
                Instruction.Create(OpCodes.Ret),
            };
        }

#if CSHARP_8_0_OR_NEWER
        private static void EmbedDefault2(List<Instruction> list, in AutomataOption option, ref (Instruction[], Instruction[], Instruction, Instruction?) pair0, ref (Instruction[], Instruction[], Instruction, Instruction?) pair1, VariableDefinition number)
#else
        private static void EmbedDefault2(List<Instruction> list, in AutomataOption option, ref (Instruction[], Instruction[], Instruction, Instruction) pair0, ref (Instruction[], Instruction[], Instruction, Instruction) pair1, VariableDefinition number)
#endif
        {
            list.Add(InstructionUtility.LoadAddress(option.Span));
            list.Add(Instruction.Create(OpCodes.Dup));
            list.Add(Instruction.Create(OpCodes.Call, option.ReadOnlySpanHelper.GetPinnableReferenceByte()));
            list.Add(Instruction.Create(OpCodes.Ldind_I8));
            list.Add(InstructionUtility.Store(number));

            list.Add(InstructionUtility.LdcI4(8));
            list.Add(Instruction.Create(OpCodes.Call, option.ReadOnlySpanHelper.SliceStartByte()));
            list.Add(InstructionUtility.Store(option.Span));

            list.Add(InstructionUtility.Load(number));
            list.Add(pair0.Item3);
            if (!(pair0.Item4 is null))
            {
                list.Add(pair0.Item4);
            }

            list.Add(Instruction.Create(OpCodes.Beq, pair0.Item1[0]));

            list.Add(InstructionUtility.Load(number));
            list.Add(pair1.Item3);
            if (!(pair1.Item4 is null))
            {
                list.Add(pair1.Item4);
            }

            list.Add(Instruction.Create(OpCodes.Beq_S, pair1.Item1[0]));

            list.Add(InstructionUtility.LdcI4(-1));
            list.Add(Instruction.Create(OpCodes.Ret));

            list.AddRange(pair1.Item1);
            list.AddRange(pair1.Item2);

            list.AddRange(pair0.Item1);
            list.AddRange(pair0.Item2);
        }

#if CSHARP_8_0_OR_NEWER
        private static void EmbedDefault1(List<Instruction> list, in AutomataOption option, ref (Instruction[], Instruction[], Instruction, Instruction?) pair0)
#else
        private static void EmbedDefault1(List<Instruction> list, in AutomataOption option, ref (Instruction[], Instruction[], Instruction, Instruction) pair0)
#endif
        {
            list.Add(InstructionUtility.LoadAddress(option.Span));
            list.Add(Instruction.Create(OpCodes.Call, option.ReadOnlySpanHelper.GetPinnableReferenceByte()));
            list.Add(Instruction.Create(OpCodes.Ldind_I8));
            list.Add(pair0.Item3);
            if (!(pair0.Item4 is null))
            {
                list.Add(pair0.Item4);
            }

            var whenEqualsToPair0 = InstructionUtility.LoadAddress(option.Span);
            list.Add(Instruction.Create(OpCodes.Beq_S, whenEqualsToPair0));

            list.Add(InstructionUtility.LdcI4(-1));
            list.Add(Instruction.Create(OpCodes.Ret));

            list.Add(whenEqualsToPair0);
            list.Add(InstructionUtility.LdcI4(8));
            list.Add(Instruction.Create(OpCodes.Call, option.ReadOnlySpanHelper.SliceStartByte()));
            list.Add(InstructionUtility.Store(option.Span));

            list.AddRange(pair0.Item1);
            list.AddRange(pair0.Item2);
        }

        private static (int offset, int count, ulong value)[] Divide(AutomataTuple[] tuples, int tuplesOffset, int tuplesCount, Length8Sorter sorter)
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

        private static void Embed1(in AutomataTuple tuple0, in AutomataOption option, int start, List<Instruction> list)
        {
            var fail = InstructionUtility.LdcI4(-1);
            var length = tuple0.Length - start;
            var match = CompareSequenceEqualityHelper.CompareSequentialEquality(tuple0, option.Span, option.ReadOnlySpanHelper, start, length, fail, new[]
            {
                InstructionUtility.LdcI4(tuple0.Index),
                Instruction.Create(OpCodes.Ret),
            });

            list.Add(InstructionUtility.Load(option.ParamSpan));
            list.Add(InstructionUtility.Store(option.Span));

            list.AddRange(match);
            list.Add(fail);
            list.Add(Instruction.Create(OpCodes.Ret));
        }

        private static void Embed2(in AutomataTuple tuple0, in AutomataTuple tuple1, in AutomataOption option, int start, List<Instruction> list)
        {
            var sameLength = SameLength(tuple0, tuple1, start);
            var fail = InstructionUtility.LdcI4(-1);
            var span = option.Span;
            var helper = option.ReadOnlySpanHelper;
            var secondMatch = CompareSequenceEqualityHelper.CompareSequentialEquality(tuple1, span, helper, start + sameLength, tuple1.Length - start - sameLength, fail, new[]
            {
                InstructionUtility.LdcI4(tuple1.Index),
                Instruction.Create(OpCodes.Ret),
            });
            var firstMatch = CompareSequenceEqualityHelper.CompareSequentialEquality(tuple0, span, helper, start + sameLength, tuple0.Length - start - sameLength, secondMatch[0], new[]
            {
                InstructionUtility.LdcI4(tuple0.Index),
                Instruction.Create(OpCodes.Ret),
            });

            list.Add(InstructionUtility.Load(option.ParamSpan));
            list.Add(InstructionUtility.Store(option.Span));

            if (sameLength != 0)
            {
                var sameMatch = CompareSequenceEqualityHelper.CompareSequentialEquality(tuple0, span, helper, start, sameLength, fail, Array.Empty<Instruction>());
                list.AddRange(sameMatch);
            }

            list.AddRange(firstMatch);
            list.AddRange(secondMatch);
            list.Add(fail);
            list.Add(Instruction.Create(OpCodes.Ret));
        }

        private static int SameLength(in AutomataTuple tuple0, in AutomataTuple tuple1, int start)
        {
            for (var i = start; i < tuple0.Length; i++)
            {
                if (tuple0[i] != tuple1[i])
                {
                    return i - start;
                }
            }

            return tuple0.Length - start;
        }
    }
}
