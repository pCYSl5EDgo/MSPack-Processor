// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using System;

namespace MSPack.Processor.Core.Embed
{
    public static class 残余長さが1であると確定したSpanに対する操作Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(BinaryFieldDestinationTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
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

            var failInstruction = options.FailInstruction;
            var smallestByte = (byte)sorter.GetValue(tuples[tuplesOffset]);
            var biggestByte = (byte)sorter.GetValue(tuples[tuplesOffset + tuplesCount - 1]);
            var switchTable = new Instruction[biggestByte - smallestByte + 1];
            for (var index = 1; index < switchTable.Length; index++)
            {
                switchTable[index] = failInstruction;
            }

            for (int index = tuplesOffset, end = tuplesOffset + tuplesCount; index < end; index++)
            {
                ref readonly var tuple = ref tuples[index];
                var i = (byte)sorter.GetValue(tuple) - smallestByte;
                switchTable[i] = tuple.Destination;
            }

            var getPinnableItemReference = options.ReadOnlySpanHelper.GetPinnableReferenceByte();

            return (new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, getPinnableItemReference),
                Instruction.Create(OpCodes.Ldind_U1),
                InstructionUtility.LdcI4(smallestByte),
                Instruction.Create(OpCodes.Sub),
                Instruction.Create(OpCodes.Switch, switchTable),

                Instruction.Create(OpCodes.Br, failInstruction),
            }, Array.Empty<Instruction>());
        }

        private static Instruction[] Embed2<TSorter>(in BinaryFieldDestinationTuple tuple0, in BinaryFieldDestinationTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var getPinnableItemReference = options.ReadOnlySpanHelper.GetPinnableReferenceByte();
            var number = options.UInt32VariableDefinition();
            var value0 = (int)(byte)sorter.GetValue(tuple0);
            var value1 = (int)(byte)sorter.GetValue(tuple1);
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, getPinnableItemReference),
                Instruction.Create(OpCodes.Ldind_U1),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.StoreVariable(number),
                InstructionUtility.LdcI4(value0),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),

                InstructionUtility.LoadVariable(number),
                InstructionUtility.LdcI4(value1),
                Instruction.Create(OpCodes.Beq, tuple1.Destination),

                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }

        private static Instruction[] Embed1<TSorter>(in BinaryFieldDestinationTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var getPinnableItemReference = options.ReadOnlySpanHelper.GetPinnableReferenceByte();
            var value0 = (int)(byte)sorter.GetValue(tuple0);
            return new[]
            {
                InstructionUtility.LoadVariableAddress(options.SpanVariableDefinition),
                Instruction.Create(OpCodes.Call, getPinnableItemReference),
                Instruction.Create(OpCodes.Ldind_U1),
                InstructionUtility.LdcI4(value0),
                Instruction.Create(OpCodes.Beq, tuple0.Destination),

                Instruction.Create(OpCodes.Br, options.FailInstruction),
            };
        }
    }
}