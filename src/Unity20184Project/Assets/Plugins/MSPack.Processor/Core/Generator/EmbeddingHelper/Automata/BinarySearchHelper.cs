// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;

namespace MSPack.Processor.Core.Embed
{
    public static class BinarySearchHelper
    {
        public static Instruction[] BinarySearchEndLong<TSorter>(in AutomataOption option, AutomataTuple[] tuples, int tuplesOffset, int tuplesCount, TSorter sorter, VariableDefinition number)
            where TSorter : ILengthSorter
        {
            ref readonly var middleTuple = ref tuples[tuplesOffset + (tuplesCount >> 1)];
            var middleValue = sorter.GetValue(middleTuple);
            var (middleItem1, middleItem2) = InstructionUtility.LdcU8(middleValue);
            var whenEqualsToMiddle = InstructionUtility.LdcI4(middleTuple.Index);
            switch (tuplesCount)
            {
                case 1:
                    if (middleItem2 is null)
                    {
                        return new[]
                        {
                            InstructionUtility.Load(number),
                            middleItem1,
                            Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),

                            InstructionUtility.LdcI4(-1),
                            Instruction.Create(OpCodes.Ret),

                            whenEqualsToMiddle,
                            Instruction.Create(OpCodes.Ret),
                        };
                    }

                    return new[]
                    {
                        InstructionUtility.Load(number),
                        middleItem1,
                        middleItem2,
                        Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),

                        InstructionUtility.LdcI4(-1),
                        Instruction.Create(OpCodes.Ret),

                        whenEqualsToMiddle,
                        Instruction.Create(OpCodes.Ret),
                    };
                case 2:
                    ref readonly var otherTuple = ref tuples[tuplesOffset];
                    var otherValue = sorter.GetValue(otherTuple);
                    var (otherItem1, otherItem2) = InstructionUtility.LdcU8(otherValue);

                    var whenEqualsToOther = InstructionUtility.LdcI4(otherTuple.Index);

                    if (middleItem2 is null)
                    {
                        if (otherItem2 is null)
                        {
                            return new[]
                            {
                                InstructionUtility.Load(number),
                                middleItem1,
                                Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),
                                InstructionUtility.Load(number),
                                otherItem1,
                                Instruction.Create(OpCodes.Beq_S, whenEqualsToOther),

                                InstructionUtility.LdcI4(-1),
                                Instruction.Create(OpCodes.Ret),

                                whenEqualsToMiddle,
                                Instruction.Create(OpCodes.Ret),

                                whenEqualsToOther,
                                Instruction.Create(OpCodes.Ret),
                            };
                        }

                        return new[]
                        {
                            InstructionUtility.Load(number),
                            middleItem1,
                            Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),

                            InstructionUtility.Load(number),
                            otherItem1,
                            otherItem2,
                            Instruction.Create(OpCodes.Beq_S, whenEqualsToOther),

                            InstructionUtility.LdcI4(-1),
                            Instruction.Create(OpCodes.Ret),

                            whenEqualsToMiddle,
                            Instruction.Create(OpCodes.Ret),

                            whenEqualsToOther,
                            Instruction.Create(OpCodes.Ret),
                        };
                    }

                    if (otherItem2 is null)
                    {
                        return new[]
                        {
                            InstructionUtility.Load(number),
                            middleItem1,
                            middleItem2,
                            Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),

                            InstructionUtility.Load(number),
                            otherItem1,
                            Instruction.Create(OpCodes.Beq_S, whenEqualsToOther),

                            InstructionUtility.LdcI4(-1),
                            Instruction.Create(OpCodes.Ret),

                            whenEqualsToMiddle,
                            Instruction.Create(OpCodes.Ret),

                            whenEqualsToOther,
                            Instruction.Create(OpCodes.Ret),
                        };
                    }
                    return new[]
                    {
                        InstructionUtility.Load(number),
                        middleItem1,
                        middleItem2,
                        Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),

                        InstructionUtility.Load(number),
                        otherItem1,
                        otherItem2,
                        Instruction.Create(OpCodes.Beq_S, whenEqualsToOther),

                        InstructionUtility.LdcI4(-1),
                        Instruction.Create(OpCodes.Ret),

                        whenEqualsToMiddle,
                        Instruction.Create(OpCodes.Ret),

                        whenEqualsToOther,
                        Instruction.Create(OpCodes.Ret),
                    };

                default:
                    var 小さい方でのtuplesの残存長さ = tuplesCount >> 1;
                    var 小さい方での二分探索結果 = BinarySearchEndLong(option, tuples, tuplesOffset, 小さい方でのtuplesの残存長さ, sorter, number);
                    var 大きい方でのtuplesの残存長さ = tuplesCount - (tuplesCount >> 1) - 1;
                    var 大きい方でのtuplesのoffset = tuplesOffset + 1 + (tuplesCount >> 1);
                    var 大きい方での二分探索結果 = BinarySearchEndLong(option, tuples, 大きい方でのtuplesのoffset, 大きい方でのtuplesの残存長さ, sorter, number);

                    var whenNotEqualsToMiddle = InstructionUtility.Load(number);
                    var 中央探索結果 = middleItem2 is null
                        ? new[]{
                            InstructionUtility.Load(number),
                            middleItem1,
                            Instruction.Create(OpCodes.Bne_Un_S, whenNotEqualsToMiddle),

                            whenEqualsToMiddle,
                            Instruction.Create(OpCodes.Ret),

                            whenNotEqualsToMiddle,
                            middleItem1.Clone(),
                            Instruction.Create(OpCodes.Blt_Un, 小さい方での二分探索結果[0]),
                        }
                        : new[]{
                            InstructionUtility.Load(number),
                            middleItem1,
                            middleItem2,
                            Instruction.Create(OpCodes.Bne_Un_S, whenNotEqualsToMiddle),

                            whenEqualsToMiddle,
                            Instruction.Create(OpCodes.Ret),

                            whenNotEqualsToMiddle,
                            middleItem1.Clone(),
                            middleItem2.Clone(),
                            Instruction.Create(OpCodes.Blt_Un, 小さい方での二分探索結果[0]),
                        };

                    return InstructionConcatHelper.Concat(
                        中央探索結果,
                        大きい方での二分探索結果,
                        小さい方での二分探索結果
                    );
            }
        }

        public static Instruction[] BinarySearchEndInt<TSorter>(in AutomataOption option, AutomataTuple[] tuples, int tuplesOffset, int tuplesCount, TSorter sorter, VariableDefinition number)
            where TSorter : ILengthSorter
        {
            ref readonly var middleTuple = ref tuples[tuplesOffset + (tuplesCount >> 1)];
            var middleValue = (int)sorter.GetValue(middleTuple);
            var whenEqualsToMiddle = InstructionUtility.LdcI4(middleTuple.Index);

            switch (tuplesCount)
            {
                case 1:
                    return new[]
                    {
                        InstructionUtility.Load(number),
                        InstructionUtility.LdcI4(middleValue),
                        Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),

                        InstructionUtility.LdcI4(-1),
                        Instruction.Create(OpCodes.Ret),

                        whenEqualsToMiddle,
                        Instruction.Create(OpCodes.Ret),
                    };

                case 2:
                    ref readonly var otherTuple = ref tuples[tuplesOffset];
                    var otherValue = (int)sorter.GetValue(otherTuple);
                    var whenEqualsToOther = InstructionUtility.LdcI4(otherTuple.Index);

                    return new[]
                    {
                        InstructionUtility.Load(number),
                        InstructionUtility.LdcI4(middleValue),
                        Instruction.Create(OpCodes.Beq_S, whenEqualsToMiddle),
                        InstructionUtility.Load(number),
                        InstructionUtility.LdcI4(otherValue),
                        Instruction.Create(OpCodes.Beq_S, whenEqualsToOther),

                        InstructionUtility.LdcI4(-1),
                        Instruction.Create(OpCodes.Ret),

                        whenEqualsToMiddle,
                        Instruction.Create(OpCodes.Ret),

                        whenEqualsToOther,
                        Instruction.Create(OpCodes.Ret),
                    };

                default:
                    var 小さい方でのtuplesの残存長さ = tuplesCount >> 1;
                    var 小さい方での二分探索結果 = BinarySearchEndInt(option, tuples, tuplesOffset, 小さい方でのtuplesの残存長さ, sorter, number);
                    var 大きい方でのtuplesの残存長さ = tuplesCount - (tuplesCount >> 1) - 1;
                    var 大きい方でのtuplesのoffset = tuplesOffset + 1 + (tuplesCount >> 1);
                    var 大きい方での二分探索結果 = BinarySearchEndInt(option, tuples, 大きい方でのtuplesのoffset, 大きい方でのtuplesの残存長さ, sorter, number);
                    var whenNotEqualsToMiddle = InstructionUtility.Load(number);
                    var 中央探索結果 = new[]
                    {
                        InstructionUtility.Load(number),
                        InstructionUtility.LdcI4(middleValue),
                        Instruction.Create(OpCodes.Bne_Un_S, whenNotEqualsToMiddle),

                        whenEqualsToMiddle,
                        Instruction.Create(OpCodes.Ret),

                        whenNotEqualsToMiddle,
                        InstructionUtility.LdcI4(middleValue),
                        Instruction.Create(OpCodes.Blt_Un, 小さい方での二分探索結果[0]),
                    };
                    return InstructionConcatHelper.Concat(
                        中央探索結果,
                        大きい方での二分探索結果,
                        小さい方での二分探索結果
                    );
            }
        }
    }
}