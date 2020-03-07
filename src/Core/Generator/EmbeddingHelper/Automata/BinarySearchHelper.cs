// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;

namespace MSPack.Processor.Core.Embed
{
    public static class BinarySearchHelper
    {
        public static Instruction[] BinarySearchEndLong<TSorter>(in AutomataOption options, BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, TSorter sorter, VariableDefinition number)
            where TSorter : ILengthSorter
        {
            var failInstruction = options.FailInstruction;
            ref readonly var middleTuple = ref tuples[tuplesOffset + (tuplesCount >> 1)];
            var middleValue = sorter.GetValue(middleTuple);
            var (middleItem1, middleItem2) = InstructionUtility.LdcU8(middleValue);
            switch (tuplesCount)
            {
                case 1:
                    if (middleItem2 is null)
                    {
                        return new[]
                        {
                            InstructionUtility.LoadVariable(number),
                            middleItem1,
                            Instruction.Create(OpCodes.Bne_Un, failInstruction),
                            Instruction.Create(OpCodes.Br, middleTuple.Destination),
                        };
                    }

                    return new[]
                    {
                        InstructionUtility.LoadVariable(number),
                        middleItem1,
                        middleItem2,
                        Instruction.Create(OpCodes.Bne_Un, failInstruction),
                        Instruction.Create(OpCodes.Br, middleTuple.Destination),
                    };
                case 2:
                    ref readonly var otherTuple = ref tuples[tuplesOffset];
                    var otherValue = sorter.GetValue(otherTuple);
                    var (otherItem1, otherItem2) = InstructionUtility.LdcU8(otherValue);

                    if (middleItem2 is null)
                    {
                        if (otherItem2 is null)
                        {
                            return new[]
                            {
                                InstructionUtility.LoadVariable(number),
                                middleItem1,
                                Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                                InstructionUtility.LoadVariable(number),
                                otherItem1,
                                Instruction.Create(OpCodes.Beq, otherTuple.Destination),
                                Instruction.Create(OpCodes.Br, failInstruction),
                            };
                        }

                        return new[]
                        {
                            InstructionUtility.LoadVariable(number),
                            middleItem1,
                            Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                            InstructionUtility.LoadVariable(number),
                            otherItem1,
                            otherItem2,
                            Instruction.Create(OpCodes.Beq, otherTuple.Destination),
                            Instruction.Create(OpCodes.Br, failInstruction),
                        };
                    }

                    if (otherItem2 is null)
                    {
                        return new[]
                        {
                            InstructionUtility.LoadVariable(number),
                            middleItem1,
                            middleItem2,
                            Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                            InstructionUtility.LoadVariable(number),
                            otherItem1,
                            Instruction.Create(OpCodes.Beq, otherTuple.Destination),
                            Instruction.Create(OpCodes.Br, failInstruction),
                        };
                    }
                    return new[]
                    {
                        InstructionUtility.LoadVariable(number),
                        middleItem1,
                        middleItem2,
                        Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                        InstructionUtility.LoadVariable(number),
                        otherItem1,
                        otherItem2,
                        Instruction.Create(OpCodes.Beq, otherTuple.Destination),
                        Instruction.Create(OpCodes.Br, failInstruction),
                    };

                default:
                    var 小さい方でのtuplesの残存長さ = tuplesCount >> 1;
                    var 小さい方での二分探索結果 = BinarySearchEndLong(options, tuples, tuplesOffset, 小さい方でのtuplesの残存長さ, sorter, number);
                    var 大きい方でのtuplesの残存長さ = tuplesCount - (tuplesCount >> 1) - 1;
                    var 大きい方でのtuplesのoffset = tuplesOffset + 1 + (tuplesCount >> 1);
                    var 大きい方での二分探索結果 = BinarySearchEndLong(options, tuples, 大きい方でのtuplesのoffset, 大きい方でのtuplesの残存長さ, sorter, number);
                    var (middleItem1Another, middleItem2Another) = InstructionUtility.LdcU8(middleValue);

                    var 中央探索結果 = middleItem2 is null
                        ? new[]{
                            InstructionUtility.LoadVariable(number),
                            middleItem1,
                            Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                            InstructionUtility.LoadVariable(number),
                            middleItem1Another,
                            Instruction.Create(OpCodes.Blt_Un, 小さい方での二分探索結果[0]),
                        }
                        : new[]{
                            InstructionUtility.LoadVariable(number),
                            middleItem1,
                            middleItem2,
                            Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                            InstructionUtility.LoadVariable(number),
                            middleItem1Another,
#if CSHARP_8_0_OR_NEWER
                            middleItem2Another!,
#else
                            middleItem2Another,
#endif
                            Instruction.Create(OpCodes.Blt_Un, 小さい方での二分探索結果[0]),
                        };

                    return InstructionConcatHelper.Concat(
                        中央探索結果,
                        大きい方での二分探索結果,
                        小さい方での二分探索結果
                    );
            }
        }

        public static Instruction[] BinarySearchEndInt<TSorter>(in AutomataOption options, BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, TSorter sorter, VariableDefinition number)
            where TSorter : ILengthSorter
        {
            var failInstruction = options.FailInstruction;
            ref readonly var middleTuple = ref tuples[tuplesOffset + (tuplesCount >> 1)];
            var middleValue = (int)sorter.GetValue(middleTuple);

            switch (tuplesCount)
            {
                case 1:
                    return new[]
                    {
                        InstructionUtility.LoadVariable(number),
                        InstructionUtility.LdcI4(middleValue),
                        Instruction.Create(OpCodes.Bne_Un, failInstruction),
                        Instruction.Create(OpCodes.Br, middleTuple.Destination),
                    };
                case 2:
                    ref readonly var otherTuple = ref tuples[tuplesOffset];
                    var otherValue = (int)sorter.GetValue(otherTuple);
                    return new[]
                    {
                        InstructionUtility.LoadVariable(number),
                        InstructionUtility.LdcI4(middleValue),
                        Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                        InstructionUtility.LoadVariable(number),
                        InstructionUtility.LdcI4(otherValue),
                        Instruction.Create(OpCodes.Beq, otherTuple.Destination),
                        Instruction.Create(OpCodes.Br, failInstruction),
                    };
                default:
                    var 小さい方でのtuplesの残存長さ = tuplesCount >> 1;
                    var 小さい方での二分探索結果 = BinarySearchEndInt(options, tuples, tuplesOffset, 小さい方でのtuplesの残存長さ, sorter, number);
                    var 大きい方でのtuplesの残存長さ = tuplesCount - (tuplesCount >> 1) - 1;
                    var 大きい方でのtuplesのoffset = tuplesOffset + 1 + (tuplesCount >> 1);
                    var 大きい方での二分探索結果 = BinarySearchEndInt(options, tuples, 大きい方でのtuplesのoffset, 大きい方でのtuplesの残存長さ, sorter, number);
                    var 中央探索結果 = new[]
                    {
                        InstructionUtility.LoadVariable(number),
                        InstructionUtility.LdcI4(middleValue),
                        Instruction.Create(OpCodes.Beq, middleTuple.Destination),
                        InstructionUtility.LoadVariable(number),
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