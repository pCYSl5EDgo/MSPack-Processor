// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using MSPack.Processor.Core.Provider;
using System;

namespace MSPack.Processor.Core.Embed
{
    public static class CompareSequenceEqualityHelper
    {
        public static Instruction[] CompareSequentialEquality(in AutomataTuple tuple, VariableDefinition span, SystemReadOnlySpanHelper helper, int start, int length, Instruction failInstruction, Instruction[] successInstructions)
        {
            var times8 = length >> 3;

#if CSHARP_8_0_OR_NEWER
            var loaders = new (Instruction, Instruction?)[times8];
#else
            var loaders = new (Instruction, Instruction)[times8];
#endif
            for (var i = 0; i < loaders.Length; i++)
            {
                loaders[i] = InstructionUtility.LdcU8(new Length8Sorter(start + (i << 3)).GetValue(tuple));
            }

            var rest = length - (times8 << 3);
            if (rest <= 4)
            {
                return CompareSequentialEqualityLessThanRest5(tuple, span, helper, start, length, failInstruction, successInstructions, loaders, rest);
            }

            return CompareSequentialEqualityMoreThanRest4(tuple, span, helper, start, length, failInstruction, successInstructions, loaders, rest);
        }

#if CSHARP_8_0_OR_NEWER
        private static int Sum((Instruction, Instruction?)[] loaders)
#else
        private static int Sum((Instruction, Instruction)[] loaders)
#endif
        {
            var answer = loaders.Length << 1;
            foreach (var (_, instruction) in loaders)
            {
                if (instruction is null)
                {
                    answer--;
                }
            }

            return answer;
        }

        private static uint CalcV1(in AutomataTuple tuple, int start, int length)
        {
            return tuple[start + length - 1];
        }

        private static uint CalcV2(in AutomataTuple tuple, int start, int length)
        {
            return tuple[start + length - 2] | (CalcV1(tuple, start, length) << 8);
        }

        private static uint CalcV3(in AutomataTuple tuple, int start, int length)
        {
            return tuple[start + length - 3] | (CalcV2(tuple, start, length) << 8);
        }

        private static uint CalcV4(in AutomataTuple tuple, int start, int length)
        {
            return tuple[start + length - 4] | (CalcV3(tuple, start, length) << 8);
        }

        private static ulong CalcV5(in AutomataTuple tuple, int start, int length)
        {
            return tuple[start + length - 5] | ((ulong)CalcV4(tuple, start, length) << 8);
        }

        private static ulong CalcV6(in AutomataTuple tuple, int start, int length)
        {
            return tuple[start + length - 6] | (CalcV5(tuple, start, length) << 8);
        }

        private static ulong CalcV7(in AutomataTuple tuple, int start, int length)
        {
            return tuple[start + length - 7] | (CalcV6(tuple, start, length) << 8);
        }

#if CSHARP_8_0_OR_NEWER
        private static Instruction[] CompareSequentialEqualityLessThanRest5(in AutomataTuple tuple, VariableDefinition span, SystemReadOnlySpanHelper helper, int start, int length, Instruction failInstruction, Instruction[] successInstructions, (Instruction, Instruction?)[] loaders, int rest)
#else
        private static Instruction[] CompareSequentialEqualityLessThanRest5(in AutomataTuple tuple, VariableDefinition span, SystemReadOnlySpanHelper helper, int start, int length, Instruction failInstruction, Instruction[] successInstructions, (Instruction, Instruction)[] loaders, int rest)
#endif
        {
            Instruction[] answer;
            switch (rest)
            {
                case 0:
                    answer = new Instruction[5 * loaders.Length + Sum(loaders) + successInstructions.Length];
                    break;
                case 1:
                    var v1 = (int)CalcV1(tuple, start, length);
                    answer = new Instruction[5 * loaders.Length + Sum(loaders) + 6 + successInstructions.Length];
                    answer[answer.Length - 6 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                    answer[answer.Length - 5 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 1);
                    answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                    answer[answer.Length - 3 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U1);
                    answer[answer.Length - 2 - successInstructions.Length] = InstructionUtility.LdcI4(v1);
                    answer[answer.Length - 1 - successInstructions.Length] = Instruction.Create(OpCodes.Bne_Un, failInstruction);
                    break;
                case 2:
                    var v2 = (int)CalcV2(tuple, start, length);
                    answer = new Instruction[5 * loaders.Length + Sum(loaders) + 6 + successInstructions.Length];
                    answer[answer.Length - 6 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                    answer[answer.Length - 5 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 2);
                    answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                    answer[answer.Length - 3 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U2);
                    answer[answer.Length - 2 - successInstructions.Length] = InstructionUtility.LdcI4(v2);
                    answer[answer.Length - 1 - successInstructions.Length] = Instruction.Create(OpCodes.Bne_Un, failInstruction);
                    break;
                case 3:
                    var v3 = (int)CalcV3(tuple, start, length);
                    answer = new Instruction[5 * loaders.Length + Sum(loaders) + 13 + successInstructions.Length];
                    answer[answer.Length - 13 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                    answer[answer.Length - 12 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 3);
                    answer[answer.Length - 11 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                    answer[answer.Length - 10 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U1);
                    answer[answer.Length - 9 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                    answer[answer.Length - 8 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 2);
                    answer[answer.Length - 7 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                    answer[answer.Length - 6 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U2);
                    answer[answer.Length - 5 - successInstructions.Length] = InstructionUtility.LdcI4(8);
                    answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                    answer[answer.Length - 3 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                    answer[answer.Length - 2 - successInstructions.Length] = InstructionUtility.LdcI4(v3);
                    answer[answer.Length - 1 - successInstructions.Length] = Instruction.Create(OpCodes.Bne_Un, failInstruction);
                    break;
                case 4:
                    var v4 = (int)CalcV4(tuple, start, length);
                    answer = new Instruction[5 * loaders.Length + Sum(loaders) + 6 + successInstructions.Length];
                    answer[answer.Length - 6 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                    answer[answer.Length - 5 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 4);
                    answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                    answer[answer.Length - 3 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U4);
                    answer[answer.Length - 2 - successInstructions.Length] = InstructionUtility.LdcI4(v4);
                    answer[answer.Length - 1 - successInstructions.Length] = Instruction.Create(OpCodes.Bne_Un, failInstruction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            FillLastSuccess(answer, successInstructions);
            FillLongMatch(answer, loaders, span, failInstruction, helper);
            return answer;
        }

        private static void FillLastSuccess(Instruction[] answer, Instruction[] successInstructions)
        {
            for (var i = 0; i < successInstructions.Length; i++)
            {
                answer[answer.Length - 1 - i] = successInstructions[successInstructions.Length - 1 - i];
            }
        }

#if CSHARP_8_0_OR_NEWER
        private static void FillLongMatch(Instruction[] answer, (Instruction, Instruction?)[] loaders, VariableDefinition span, Instruction failInstruction, SystemReadOnlySpanHelper helper)
#else
        private static void FillLongMatch(Instruction[] answer, (Instruction, Instruction)[] loaders, VariableDefinition span, Instruction failInstruction, SystemReadOnlySpanHelper helper)
#endif
        {
            var index = 0;
            var getItemByte = helper.GetItemByte();

            for (var i = 0; i < loaders.Length; i++)
            {
                var (item1, item2) = loaders[i];
                answer[index++] = InstructionUtility.LoadAddress(span);
                answer[index++] = InstructionUtility.LdcI4(i << 3);
                answer[index++] = Instruction.Create(OpCodes.Call, getItemByte);
                answer[index++] = Instruction.Create(OpCodes.Ldind_I8);
                answer[index++] = item1;
                if (!(item2 is null))
                {
                    answer[index++] = item2;
                }

                answer[index++] = Instruction.Create(OpCodes.Bne_Un, failInstruction);
            }
        }

#if CSHARP_8_0_OR_NEWER
        private static Instruction[] CompareSequentialEqualityMoreThanRest4(in AutomataTuple tuple, VariableDefinition span, SystemReadOnlySpanHelper helper, int start, int length, Instruction failInstruction, Instruction[] successInstructions, (Instruction, Instruction?)[] loaders, int rest)
#else
        private static Instruction[] CompareSequentialEqualityMoreThanRest4(in AutomataTuple tuple, VariableDefinition span, SystemReadOnlySpanHelper helper, int start, int length, Instruction failInstruction, Instruction[] successInstructions, (Instruction, Instruction)[] loaders, int rest)
#endif
        {
            Instruction[] answer;
            switch (rest)
            {
                case 5:
                    var (item15, item25) = InstructionUtility.LdcU8(CalcV5(tuple, start, length));
                    if (item25 is null)
                    {
                        answer = new Instruction[5 * loaders.Length + Sum(loaders) + 15 + successInstructions.Length];
                        answer[answer.Length - 15 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 14 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 5);
                        answer[answer.Length - 13 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 12 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U1);
                        answer[answer.Length - 11 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 10 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 9 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 4);
                        answer[answer.Length - 8 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 7 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U4);
                        answer[answer.Length - 6 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 5 - successInstructions.Length] = InstructionUtility.LdcI4(8);
                        answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 3 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 2 - successInstructions.Length] = item15;
                    }
                    else
                    {
                        answer = new Instruction[5 * loaders.Length + Sum(loaders) + 16 + successInstructions.Length];
                        answer[answer.Length - 16 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 15 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 5);
                        answer[answer.Length - 14 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 13 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U1);
                        answer[answer.Length - 12 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 11 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 10 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 4);
                        answer[answer.Length - 9 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 8 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U4);
                        answer[answer.Length - 7 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 6 - successInstructions.Length] = InstructionUtility.LdcI4(8);
                        answer[answer.Length - 5 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 3 - successInstructions.Length] = item15;
                        answer[answer.Length - 2 - successInstructions.Length] = item25;
                    }

                    break;
                case 6:
                    var (item16, item26) = InstructionUtility.LdcU8(CalcV6(tuple, start, length));
                    if (item26 is null)
                    {
                        answer = new Instruction[5 * loaders.Length + Sum(loaders) + 15 + successInstructions.Length];
                        answer[answer.Length - 15 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 14 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 6);
                        answer[answer.Length - 13 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 12 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U2);
                        answer[answer.Length - 11 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 10 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 9 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 4);
                        answer[answer.Length - 8 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 7 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U4);
                        answer[answer.Length - 6 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 5 - successInstructions.Length] = InstructionUtility.LdcI4(16);
                        answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 3 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 2 - successInstructions.Length] = item16;
                    }
                    else
                    {
                        answer = new Instruction[5 * loaders.Length + Sum(loaders) + 16 + successInstructions.Length];
                        answer[answer.Length - 16 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 15 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 6);
                        answer[answer.Length - 14 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 13 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U2);
                        answer[answer.Length - 12 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 11 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 10 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 4);
                        answer[answer.Length - 9 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 8 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U4);
                        answer[answer.Length - 7 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 6 - successInstructions.Length] = InstructionUtility.LdcI4(16);
                        answer[answer.Length - 5 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 3 - successInstructions.Length] = item16;
                        answer[answer.Length - 2 - successInstructions.Length] = item26;
                    }

                    break;
                case 7:
                    var (item17, item27) = InstructionUtility.LdcU8(CalcV7(tuple, start, length));
                    if (item27 is null)
                    {
                        answer = new Instruction[5 * loaders.Length + Sum(loaders) + 23 + successInstructions.Length];
                        answer[answer.Length - 23 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 22 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 7);
                        answer[answer.Length - 21 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 20 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U1);
                        answer[answer.Length - 19 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 18 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 17 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 6);
                        answer[answer.Length - 16 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 15 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U2);
                        answer[answer.Length - 14 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 13 - successInstructions.Length] = InstructionUtility.LdcI4(8);
                        answer[answer.Length - 12 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 11 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 10 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 9 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 4);
                        answer[answer.Length - 8 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 7 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U4);
                        answer[answer.Length - 6 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 5 - successInstructions.Length] = InstructionUtility.LdcI4(24);
                        answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 3 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 2 - successInstructions.Length] = item17;
                    }
                    else
                    {
                        answer = new Instruction[5 * loaders.Length + Sum(loaders) + 24 + successInstructions.Length];
                        answer[answer.Length - 24 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 23 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 7);
                        answer[answer.Length - 22 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 21 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U1);
                        answer[answer.Length - 20 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 19 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 18 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 6);
                        answer[answer.Length - 17 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 16 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U2);
                        answer[answer.Length - 15 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 14 - successInstructions.Length] = InstructionUtility.LdcI4(8);
                        answer[answer.Length - 13 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 12 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 11 - successInstructions.Length] = InstructionUtility.LoadAddress(span);
                        answer[answer.Length - 10 - successInstructions.Length] = InstructionUtility.LdcI4(start + length - 4);
                        answer[answer.Length - 9 - successInstructions.Length] = Instruction.Create(OpCodes.Call, helper.GetItemByte());
                        answer[answer.Length - 8 - successInstructions.Length] = Instruction.Create(OpCodes.Ldind_U4);
                        answer[answer.Length - 7 - successInstructions.Length] = Instruction.Create(OpCodes.Conv_U8);
                        answer[answer.Length - 6 - successInstructions.Length] = InstructionUtility.LdcI4(24);
                        answer[answer.Length - 5 - successInstructions.Length] = Instruction.Create(OpCodes.Shl);
                        answer[answer.Length - 4 - successInstructions.Length] = Instruction.Create(OpCodes.Add);
                        answer[answer.Length - 3 - successInstructions.Length] = item17;
                        answer[answer.Length - 2 - successInstructions.Length] = item27;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            answer[answer.Length - 1 - successInstructions.Length] = Instruction.Create(OpCodes.Bne_Un, failInstruction);
            FillLastSuccess(answer, successInstructions);
            FillLongMatch(answer, loaders, span, failInstruction, helper);
            return answer;
        }
    }
}