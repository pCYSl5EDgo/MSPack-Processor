// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using System;

namespace MSPack.Processor.Core.Embed
{
    public static class ForSpanLength1Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(AutomataTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption options, TSorter sorter)
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

            var failInstruction = InstructionUtility.LdcI4(-1);
            var smallestByte = (byte)sorter.GetValue(tuples[tuplesOffset]);
            var biggestByte = (byte)sorter.GetValue(tuples[tuplesOffset + tuplesCount - 1]);
            var switchTable = new Instruction[biggestByte - smallestByte + 1];
            for (var index = 1; index < switchTable.Length; index++)
            {
                switchTable[index] = failInstruction;
            }

            var loadingInstructions = new Instruction[tuplesCount * 2];

            for (int index = tuplesOffset, end = tuplesOffset + tuplesCount; index < end; index++)
            {
                ref readonly var tuple = ref tuples[index];
                var i = (byte)sorter.GetValue(tuple) - smallestByte;
                loadingInstructions[i * 2] = switchTable[i] = InstructionUtility.LdcI4(tuple.Index);
                loadingInstructions[i * 2 + 1] = Instruction.Create(OpCodes.Ret);
            }

            var getPinnableItemReference = options.ReadOnlySpanHelper.GetPinnableReferenceByte();

            return (new[]
            {
                InstructionUtility.LoadAddress(options.Span),
                Instruction.Create(OpCodes.Call, getPinnableItemReference),
                Instruction.Create(OpCodes.Ldind_U1),
                InstructionUtility.LdcI4(smallestByte),
                Instruction.Create(OpCodes.Sub),
                Instruction.Create(OpCodes.Switch, switchTable),

                failInstruction,
                Instruction.Create(OpCodes.Ret),
            }, loadingInstructions);
        }

        private static Instruction[] Embed2<TSorter>(in AutomataTuple tuple0, in AutomataTuple tuple1, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var getPinnableItemReference = options.ReadOnlySpanHelper.GetPinnableReferenceByte();
            var number = options.UInt32VariableDefinition();
            var value0 = (int)(byte)sorter.GetValue(tuple0);
            var value1 = (int)(byte)sorter.GetValue(tuple1);
            var whenNotEqualsToTuple0 = InstructionUtility.Load(number);
            var whenEqualsToTuple1 = InstructionUtility.LdcI4(tuple1.Index);

            return new[]
            {
                InstructionUtility.LoadAddress(options.Span),
                Instruction.Create(OpCodes.Call, getPinnableItemReference),
                Instruction.Create(OpCodes.Ldind_U1),
                Instruction.Create(OpCodes.Dup),
                InstructionUtility.Store(number),
                InstructionUtility.LdcI4(value0),
                Instruction.Create(OpCodes.Bne_Un_S, whenNotEqualsToTuple0),

                InstructionUtility.LdcI4(tuple0.Index),
                Instruction.Create(OpCodes.Ret),

                whenNotEqualsToTuple0,
                InstructionUtility.LdcI4(value1),
                Instruction.Create(OpCodes.Beq_S, whenNotEqualsToTuple0),

                InstructionUtility.LdcI4(-1),
                Instruction.Create(OpCodes.Ret),

                whenEqualsToTuple1,
                Instruction.Create(OpCodes.Ret),
            };
        }

        private static Instruction[] Embed1<TSorter>(in AutomataTuple tuple0, in AutomataOption options, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var getPinnableItemReference = options.ReadOnlySpanHelper.GetPinnableReferenceByte();
            var value0 = (int)(byte)sorter.GetValue(tuple0);
            var whenEquals = InstructionUtility.LdcI4(tuple0.Index);

            return new[]
            {
                InstructionUtility.LoadAddress(options.Span),
                Instruction.Create(OpCodes.Call, getPinnableItemReference),
                Instruction.Create(OpCodes.Ldind_U1),
                InstructionUtility.LdcI4(value0),
                Instruction.Create(OpCodes.Beq_S, whenEquals),

                InstructionUtility.LdcI4(-1),
                Instruction.Create(OpCodes.Ret),

                whenEquals,
                Instruction.Create(OpCodes.Ret),
            };
        }
    }
}