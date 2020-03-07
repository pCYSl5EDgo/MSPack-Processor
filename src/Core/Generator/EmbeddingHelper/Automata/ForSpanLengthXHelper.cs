// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Embed
{
    public static class ForSpanLength8Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter, IComparer<BinaryFieldDestinationTuple>
        {
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in options, sorter);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, sorter);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var number = options.UInt64VariableDefinition();
            var 探索結果 = BinarySearchHelper.BinarySearchEndLong(options, tuples, tuplesOffset, tuplesCount, sorter, number);
            return (
                new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_I8),
                    InstructionUtility.StoreVariable(number),
                },
                探索結果
            );
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            if (value0Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_I8),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_I8),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            var (value1Item1, value1Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple1));
            var number = options.UInt64VariableDefinition();

            if (value0Item2 is null)
            {
                if (value1Item2 is null)
                {
                    return new[]
                    {
                        InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                        Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                        Instruction.Create(OpCodes.Ldind_I8),
                        Instruction.Create(OpCodes.Dup),
                        InstructionUtility.StoreVariable(number),
                        value0Item1,
                        Instruction.Create(OpCodes.Beq, tuple0.Destination),
                        InstructionUtility.LoadVariable(number),
                        value1Item1,
                        Instruction.Create(OpCodes.Beq, tuple1.Destination),
                        Instruction.Create(OpCodes.Br, options.FailInstruction),
                    };
                }

                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_I8),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    value1Item2,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            if (value1Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_I8),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    value0Item2,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_I8),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                InstructionUtility.LoadVariable(number),
                value1Item1,
                value1Item2,
                Instruction.Create(OpCodes.Beq, tuple1.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
    public static class ForSpanLength7Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter, IComparer<BinaryFieldDestinationTuple>
        {
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in options, sorter);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, sorter);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var number = options.UInt64VariableDefinition();
            var 探索結果 = BinarySearchHelper.BinarySearchEndLong(options, tuples, tuplesOffset, tuplesCount, sorter, number);
            return (
                new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(6),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(48),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    InstructionUtility.StoreVariable(number),
                },
                探索結果
            );
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            if (value0Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(6),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(48),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(6),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(48),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            var (value1Item1, value1Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple1));
            var number = options.UInt64VariableDefinition();

            if (value0Item2 is null)
            {
                if (value1Item2 is null)
                {
                    return new[]
                    {
                        InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                        Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                        Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(6),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(48),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                        Instruction.Create(OpCodes.Dup),
                        InstructionUtility.StoreVariable(number),
                        value0Item1,
                        Instruction.Create(OpCodes.Beq, tuple0.Destination),
                        InstructionUtility.LoadVariable(number),
                        value1Item1,
                        Instruction.Create(OpCodes.Beq, tuple1.Destination),
                        Instruction.Create(OpCodes.Br, options.FailInstruction),
                    };
                }

                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(6),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(48),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    value1Item2,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            if (value1Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(6),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(48),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    value0Item2,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(6),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(48),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                InstructionUtility.LoadVariable(number),
                value1Item1,
                value1Item2,
                Instruction.Create(OpCodes.Beq, tuple1.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
    public static class ForSpanLength6Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter, IComparer<BinaryFieldDestinationTuple>
        {
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in options, sorter);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, sorter);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var number = options.UInt64VariableDefinition();
            var 探索結果 = BinarySearchHelper.BinarySearchEndLong(options, tuples, tuplesOffset, tuplesCount, sorter, number);
            return (
                new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    InstructionUtility.StoreVariable(number),
                },
                探索結果
            );
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            if (value0Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            var (value1Item1, value1Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple1));
            var number = options.UInt64VariableDefinition();

            if (value0Item2 is null)
            {
                if (value1Item2 is null)
                {
                    return new[]
                    {
                        InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                        Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                        Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                        Instruction.Create(OpCodes.Dup),
                        InstructionUtility.StoreVariable(number),
                        value0Item1,
                        Instruction.Create(OpCodes.Beq, tuple0.Destination),
                        InstructionUtility.LoadVariable(number),
                        value1Item1,
                        Instruction.Create(OpCodes.Beq, tuple1.Destination),
                        Instruction.Create(OpCodes.Br, options.FailInstruction),
                    };
                }

                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    value1Item2,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            if (value1Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    value0Item2,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U2),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                InstructionUtility.LoadVariable(number),
                value1Item1,
                value1Item2,
                Instruction.Create(OpCodes.Beq, tuple1.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
    public static class ForSpanLength5Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter, IComparer<BinaryFieldDestinationTuple>
        {
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in options, sorter);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, sorter);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var number = options.UInt64VariableDefinition();
            var 探索結果 = BinarySearchHelper.BinarySearchEndLong(options, tuples, tuplesOffset, tuplesCount, sorter, number);
            return (
                new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    InstructionUtility.StoreVariable(number),
                },
                探索結果
            );
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            if (value0Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var (value0Item1, value0Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple0));
            var (value1Item1, value1Item2) = InstructionUtility.LdcU8(sorter.GetValue(tuple1));
            var number = options.UInt64VariableDefinition();

            if (value0Item2 is null)
            {
                if (value1Item2 is null)
                {
                    return new[]
                    {
                        InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                        Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                        Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                        Instruction.Create(OpCodes.Dup),
                        InstructionUtility.StoreVariable(number),
                        value0Item1,
                        Instruction.Create(OpCodes.Beq, tuple0.Destination),
                        InstructionUtility.LoadVariable(number),
                        value1Item1,
                        Instruction.Create(OpCodes.Beq, tuple1.Destination),
                        Instruction.Create(OpCodes.Br, options.FailInstruction),
                    };
                }

                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    value1Item2,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            if (value1Item2 is null)
            {
                return new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.StoreVariable(number),
                    value0Item1,
                    value0Item2,
                    Instruction.Create(OpCodes.Beq, tuple0.Destination),
                    InstructionUtility.LoadVariable(number),
                    value1Item1,
                    Instruction.Create(OpCodes.Beq, tuple1.Destination),
                    Instruction.Create(OpCodes.Br, options.FailInstruction),
                };
            }

            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(4),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    Instruction.Create(OpCodes.Conv_U8),

                    InstructionUtility.LdcI4(32),
                    Instruction.Create(OpCodes.Shl),
                    Instruction.Create(OpCodes.Add),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                value0Item1,
                value0Item2,
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                InstructionUtility.LoadVariable(number),
                value1Item1,
                value1Item2,
                Instruction.Create(OpCodes.Beq, tuple1.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
    public static class ForSpanLength4Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter, IComparer<BinaryFieldDestinationTuple>
        {
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in options, sorter);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, sorter);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var number = options.UInt32VariableDefinition();
            var 探索結果 = BinarySearchHelper.BinarySearchEndInt(options, tuples, tuplesOffset, tuplesCount, sorter, number);
            return (
                new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U4),
                    InstructionUtility.StoreVariable(number),
                },
                探索結果
            );
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple0)),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var number = options.UInt32VariableDefinition();
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U4),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple0)),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                InstructionUtility.LoadVariable(number),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple1)),
                Instruction.Create(OpCodes.Beq, tuple1.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
    public static class ForSpanLength3Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter, IComparer<BinaryFieldDestinationTuple>
        {
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in options, sorter);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, sorter);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var number = options.UInt32VariableDefinition();
            var 探索結果 = BinarySearchHelper.BinarySearchEndInt(options, tuples, tuplesOffset, tuplesCount, sorter, number);
            return (
                new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U2),
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(2),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    InstructionUtility.LdcI4(16),
                    Instruction.Create(OpCodes.Shl),
                    InstructionUtility.StoreVariable(number),
                },
                探索結果
            );
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U2),
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(2),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    InstructionUtility.LdcI4(16),
                    Instruction.Create(OpCodes.Shl),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple0)),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var number = options.UInt32VariableDefinition();
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U2),
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    InstructionUtility.LdcI4(2),
                    Instruction.Create(OpCodes.Add),
                    Instruction.Create(OpCodes.Ldind_U1),
                    InstructionUtility.LdcI4(16),
                    Instruction.Create(OpCodes.Shl),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple0)),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                InstructionUtility.LoadVariable(number),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple1)),
                Instruction.Create(OpCodes.Beq, tuple1.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
    public static class ForSpanLength2Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter, IComparer<BinaryFieldDestinationTuple>
        {
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in options, sorter);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in options, sorter);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var number = options.UInt32VariableDefinition();
            var 探索結果 = BinarySearchHelper.BinarySearchEndInt(options, tuples, tuplesOffset, tuplesCount, sorter, number);
            return (
                new[]
                {
                    InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                    Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                    Instruction.Create(OpCodes.Ldind_U2),
                    InstructionUtility.StoreVariable(number),
                },
                探索結果
            );
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U2),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple0)),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var number = options.UInt32VariableDefinition();
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, options.ReadOnlySpanHelper.GetPinnableReferenceByte()),
                Instruction.Create(OpCodes.Ldind_U2),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple0)),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),
                InstructionUtility.LoadVariable(number),
                InstructionUtility.LdcI4((int)(uint)sorter.GetValue(tuple1)),
                Instruction.Create(OpCodes.Beq, tuple1.Destination),
                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
}