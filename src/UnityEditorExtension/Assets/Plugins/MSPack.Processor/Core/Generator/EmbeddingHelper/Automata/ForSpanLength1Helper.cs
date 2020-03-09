// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using System;

namespace MSPack.Processor.Core.Embed
{
    public static class ForSpanLength1Helper
    {
        public static (Instruction[], Instruction[]) Embed<TSorter>(AutomataTuple[] tuples, int tuplesOffset, int tuplesCount, in AutomataOption option, TSorter sorter)
            where TSorter : ILengthSorter
        {
            var getPinnableItemReference = option.ReadOnlySpanHelper.GetPinnableReferenceByte();
            switch (tuplesCount)
            {
                case 1:
                    var embed1 = Embed1(in tuples[tuplesOffset], in option, sorter, getPinnableItemReference);
                    return (embed1, Array.Empty<Instruction>());
                case 2:
                    var embed2 = Embed2(in tuples[tuplesOffset], in tuples[tuplesOffset + 1], in option, sorter, getPinnableItemReference);
                    return (embed2, Array.Empty<Instruction>());
            }

            Array.Sort(tuples, tuplesOffset, tuplesCount, sorter);
            var failInstruction = InstructionUtility.LdcI4(-1);
            var smallestByte = (byte)sorter.GetValue(tuples[tuplesOffset]);
            var biggestByte = (byte)sorter.GetValue(tuples[tuplesOffset + tuplesCount - 1]);
            var switchTable = new Instruction[biggestByte - smallestByte + 1];
            for (var index = 1; index < switchTable.Length; index++)
            {
                switchTable[index] = failInstruction;
            }

            var loadingInstructions = new Instruction[tuplesCount * 2];

            try
            {
            for (int index = tuplesOffset, end = tuplesOffset + tuplesCount; index < end; index++)
            {
                ref readonly var tuple = ref tuples[index];
                var i = (byte)sorter.GetValue(tuple) - smallestByte;
                loadingInstructions[(index - tuplesOffset) * 2] = switchTable[i] = InstructionUtility.LdcI4(tuple.Index);
                loadingInstructions[(index - tuplesOffset) * 2 + 1] = Instruction.Create(OpCodes.Ret);
            }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return (new[]
            {
                InstructionUtility.LoadAddress(option.Span),
                Instruction.Create(OpCodes.Call, getPinnableItemReference),
                Instruction.Create(OpCodes.Ldind_U1),
                InstructionUtility.LdcI4(smallestByte),
                Instruction.Create(OpCodes.Sub),
                Instruction.Create(OpCodes.Switch, switchTable),

                failInstruction,
                Instruction.Create(OpCodes.Ret),
            }, loadingInstructions);
        }

        private static Instruction[] Embed2<TSorter>(in AutomataTuple tuple0, in AutomataTuple tuple1, in AutomataOption option, TSorter sorter, MethodReference getPinnableItemReference)
            where TSorter : ILengthSorter
        {
            var number = option.UInt32VariableDefinition();
            var value0 = (int)(byte)sorter.GetValue(tuple0);
            var value1 = (int)(byte)sorter.GetValue(tuple1);
            var whenNotEqualsToTuple0 = InstructionUtility.Load(number);
            var whenEqualsToTuple1 = InstructionUtility.LdcI4(tuple1.Index);

            return new[]
            {
                InstructionUtility.LoadAddress(option.Span),
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
                Instruction.Create(OpCodes.Beq_S, whenEqualsToTuple1),

                InstructionUtility.LdcI4(-1),
                Instruction.Create(OpCodes.Ret),

                whenEqualsToTuple1,
                Instruction.Create(OpCodes.Ret),
            };
        }

        private static Instruction[] Embed1<TSorter>(in AutomataTuple tuple0, in AutomataOption option, TSorter sorter, MethodReference getPinnableItemReference)
            where TSorter : ILengthSorter
        {
            var value0 = (int)(byte)sorter.GetValue(tuple0);
            var whenEquals = InstructionUtility.LdcI4(tuple0.Index);

            return new[]
            {
                InstructionUtility.LoadAddress(option.Span),
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