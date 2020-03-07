// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;

namespace MSPack.Processor.Core
{
    public static class InstructionUtility
    {
        public static Instruction Clone(this Instruction instruction)
        {
            var answer = Instruction.Create(OpCodes.Nop);
            answer.OpCode = instruction.OpCode;
            answer.Operand = instruction.Operand;
            return answer;
        }

        public static Instruction LdcI4(int value)
        {
            switch (value)
            {
                case -1: return Instruction.Create(OpCodes.Ldc_I4_M1);
                case 0: return Instruction.Create(OpCodes.Ldc_I4_0);
                case 1: return Instruction.Create(OpCodes.Ldc_I4_1);
                case 2: return Instruction.Create(OpCodes.Ldc_I4_2);
                case 3: return Instruction.Create(OpCodes.Ldc_I4_3);
                case 4: return Instruction.Create(OpCodes.Ldc_I4_4);
                case 5: return Instruction.Create(OpCodes.Ldc_I4_5);
                case 6: return Instruction.Create(OpCodes.Ldc_I4_6);
                case 7: return Instruction.Create(OpCodes.Ldc_I4_7);
                case 8: return Instruction.Create(OpCodes.Ldc_I4_8);
                default:
                    if (value >= sbyte.MinValue && value <= sbyte.MaxValue)
                    {
                        return Instruction.Create(OpCodes.Ldc_I4_S, (sbyte)value);
                    }
                    else
                    {
                        return Instruction.Create(OpCodes.Ldc_I4, value);
                    }
            }
        }
#if CSHARP_8_0_OR_NEWER
        public static (Instruction, Instruction?) LdcI8(long value)
#else
        public static (Instruction, Instruction) LdcI8(long value)
#endif
        {
            switch (value)
            {
                case -1: return (Instruction.Create(OpCodes.Ldc_I4_M1), Instruction.Create(OpCodes.Conv_I8));
                case 0: return (Instruction.Create(OpCodes.Ldc_I4_0), Instruction.Create(OpCodes.Conv_I8));
                case 1: return (Instruction.Create(OpCodes.Ldc_I4_1), Instruction.Create(OpCodes.Conv_I8));
                case 2: return (Instruction.Create(OpCodes.Ldc_I4_2), Instruction.Create(OpCodes.Conv_I8));
                case 3: return (Instruction.Create(OpCodes.Ldc_I4_3), Instruction.Create(OpCodes.Conv_I8));
                case 4: return (Instruction.Create(OpCodes.Ldc_I4_4), Instruction.Create(OpCodes.Conv_I8));
                case 5: return (Instruction.Create(OpCodes.Ldc_I4_5), Instruction.Create(OpCodes.Conv_I8));
                case 6: return (Instruction.Create(OpCodes.Ldc_I4_6), Instruction.Create(OpCodes.Conv_I8));
                case 7: return (Instruction.Create(OpCodes.Ldc_I4_7), Instruction.Create(OpCodes.Conv_I8));
                case 8: return (Instruction.Create(OpCodes.Ldc_I4_8), Instruction.Create(OpCodes.Conv_I8));
            }

            if (value >= sbyte.MinValue && value <= sbyte.MaxValue)
            {
                return (
                    Instruction.Create(OpCodes.Ldc_I4_S, (sbyte)value),
                    Instruction.Create(OpCodes.Conv_I8)
                );
            }

            if (value >= int.MinValue && value <= int.MaxValue)
            {
                return (
                    Instruction.Create(OpCodes.Ldc_I4, (int)value),
                    Instruction.Create(OpCodes.Conv_I8)
                );
            }

            return (Instruction.Create(OpCodes.Ldc_I8, value), default);
        }
#if CSHARP_8_0_OR_NEWER
        public static (Instruction, Instruction?) LdcU8(ulong value)
#else
        public static (Instruction, Instruction) LdcU8(ulong value)
#endif
        {
            switch (value)
            {
                case 0: return (Instruction.Create(OpCodes.Ldc_I4_0), Instruction.Create(OpCodes.Conv_U8));
                case 1: return (Instruction.Create(OpCodes.Ldc_I4_1), Instruction.Create(OpCodes.Conv_U8));
                case 2: return (Instruction.Create(OpCodes.Ldc_I4_2), Instruction.Create(OpCodes.Conv_U8));
                case 3: return (Instruction.Create(OpCodes.Ldc_I4_3), Instruction.Create(OpCodes.Conv_U8));
                case 4: return (Instruction.Create(OpCodes.Ldc_I4_4), Instruction.Create(OpCodes.Conv_U8));
                case 5: return (Instruction.Create(OpCodes.Ldc_I4_5), Instruction.Create(OpCodes.Conv_U8));
                case 6: return (Instruction.Create(OpCodes.Ldc_I4_6), Instruction.Create(OpCodes.Conv_U8));
                case 7: return (Instruction.Create(OpCodes.Ldc_I4_7), Instruction.Create(OpCodes.Conv_U8));
                case 8: return (Instruction.Create(OpCodes.Ldc_I4_8), Instruction.Create(OpCodes.Conv_U8));
            }

            if (value <= (ulong)sbyte.MaxValue)
            {
                return (
                    Instruction.Create(OpCodes.Ldc_I4_S, (sbyte)value),
                    Instruction.Create(OpCodes.Conv_U8)
                );
            }

            if (value <= (ulong)int.MaxValue)
            {
                return (
                    Instruction.Create(OpCodes.Ldc_I4, (int)value),
                    Instruction.Create(OpCodes.Conv_U8)
                );
            }

            return (Instruction.Create(OpCodes.Ldc_I8, (long)value), default);
        }

        public static Instruction LdcBoolean(bool value)
        {
            return Instruction.Create(value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
        }

        public static Instruction LdcR4(float value)
        {
            return Instruction.Create(OpCodes.Ldc_R4, value);
        }

        public static Instruction LdcR8(double value)
        {
            return Instruction.Create(OpCodes.Ldc_R8, value);
        }

        public static Instruction LdStr(string value)
        {
            return Instruction.Create(OpCodes.Ldstr, value);
        }

        public static Instruction LdNull() => Instruction.Create(OpCodes.Ldnull);

        public static Instruction LoadVariableAddress(VariableDefinition variableDefinition)
        {
            if (variableDefinition.Index < 256)
            {
                return Instruction.Create(OpCodes.Ldloca_S, variableDefinition);
            }

            return Instruction.Create(OpCodes.Ldloca, variableDefinition);
        }

        public static Instruction LoadVariable(VariableDefinition variableDefinition)
        {
            switch (variableDefinition.Index)
            {
                case 0:
                    return Instruction.Create(OpCodes.Ldloc_0);
                case 1:
                    return Instruction.Create(OpCodes.Ldloc_1);
                case 2:
                    return Instruction.Create(OpCodes.Ldloc_2);
                case 3:
                    return Instruction.Create(OpCodes.Ldloc_3);
                default:
                    return Instruction.Create(variableDefinition.Index < 256 ? OpCodes.Ldloc_S : OpCodes.Ldloc, variableDefinition);
            }
        }

        public static Instruction StoreVariable(VariableDefinition variableDefinition)
        {
            switch (variableDefinition.Index)
            {
                case 0:
                    return Instruction.Create(OpCodes.Stloc_0);
                case 1:
                    return Instruction.Create(OpCodes.Stloc_1);
                case 2:
                    return Instruction.Create(OpCodes.Stloc_2);
                case 3:
                    return Instruction.Create(OpCodes.Stloc_3);
                default:
                    return Instruction.Create(variableDefinition.Index < 256 ? OpCodes.Stloc_S : OpCodes.Stloc, variableDefinition);
            }
        }
    }
}
