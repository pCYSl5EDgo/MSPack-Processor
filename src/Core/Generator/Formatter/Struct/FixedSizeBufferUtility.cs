// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace MSPack.Processor.Core.Formatter
{
    internal static class FixedSizeBufferUtility
    {
        private static int SizeOf(FixedSizeBufferElementType elementType) =>
            elementType switch
            {
                FixedSizeBufferElementType.Boolean => 1,
                FixedSizeBufferElementType.SByte => 1,
                FixedSizeBufferElementType.Byte => 1,
                FixedSizeBufferElementType.Char => 2,
                FixedSizeBufferElementType.Int16 => 2,
                FixedSizeBufferElementType.UInt16 => 2,
                FixedSizeBufferElementType.Int32 => 4,
                FixedSizeBufferElementType.UInt32 => 4,
                FixedSizeBufferElementType.Single => 4,
                FixedSizeBufferElementType.Int64 => 8,
                FixedSizeBufferElementType.UInt64 => 8,
                FixedSizeBufferElementType.Double => 8,
                FixedSizeBufferElementType.None => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null),
                _ => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null)
            };

        private static TypeReference GetType(ModuleDefinition module, FixedSizeBufferElementType elementType) =>
            elementType switch
            {
                FixedSizeBufferElementType.Boolean => module.TypeSystem.Boolean,
                FixedSizeBufferElementType.Char => module.TypeSystem.Char,
                FixedSizeBufferElementType.SByte => module.TypeSystem.SByte,
                FixedSizeBufferElementType.Int16 => module.TypeSystem.Int16,
                FixedSizeBufferElementType.Int32 => module.TypeSystem.Int32,
                FixedSizeBufferElementType.Int64 => module.TypeSystem.Int64,
                FixedSizeBufferElementType.Byte => module.TypeSystem.Byte,
                FixedSizeBufferElementType.UInt16 => module.TypeSystem.UInt16,
                FixedSizeBufferElementType.UInt32 => module.TypeSystem.UInt32,
                FixedSizeBufferElementType.UInt64 => module.TypeSystem.UInt64,
                FixedSizeBufferElementType.Single => module.TypeSystem.Single,
                FixedSizeBufferElementType.Double => module.TypeSystem.Double,
                FixedSizeBufferElementType.None => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null),
                _ => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null)
            };

        private static OpCode LdInd(FixedSizeBufferElementType elementType) =>
            elementType switch
            {
                FixedSizeBufferElementType.SByte => OpCodes.Ldind_I1,
                FixedSizeBufferElementType.Int16 => OpCodes.Ldind_I2,
                FixedSizeBufferElementType.Int32 => OpCodes.Ldind_I4,
                FixedSizeBufferElementType.Boolean => OpCodes.Ldind_U1,
                FixedSizeBufferElementType.Byte => OpCodes.Ldind_U1,
                FixedSizeBufferElementType.Char => OpCodes.Ldind_U2,
                FixedSizeBufferElementType.UInt16 => OpCodes.Ldind_U2,
                FixedSizeBufferElementType.UInt32 => OpCodes.Ldind_U4,
                FixedSizeBufferElementType.Int64 => OpCodes.Ldind_I8,
                FixedSizeBufferElementType.UInt64 => OpCodes.Ldind_I8,
                FixedSizeBufferElementType.Single => OpCodes.Ldind_R4,
                FixedSizeBufferElementType.Double => OpCodes.Ldind_R8,
                FixedSizeBufferElementType.None => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null),
                _ => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null)
            };

        private static OpCode StInd(FixedSizeBufferElementType elementType) =>
            elementType switch
            {
                FixedSizeBufferElementType.Boolean => OpCodes.Stind_I1,
                FixedSizeBufferElementType.SByte => OpCodes.Stind_I1,
                FixedSizeBufferElementType.Byte => OpCodes.Stind_I1,
                FixedSizeBufferElementType.Char => OpCodes.Stind_I2,
                FixedSizeBufferElementType.Int16 => OpCodes.Stind_I2,
                FixedSizeBufferElementType.UInt16 => OpCodes.Stind_I2,
                FixedSizeBufferElementType.Int32 => OpCodes.Stind_I4,
                FixedSizeBufferElementType.UInt32 => OpCodes.Stind_I4,
                FixedSizeBufferElementType.Int64 => OpCodes.Stind_I8,
                FixedSizeBufferElementType.UInt64 => OpCodes.Stind_I8,
                FixedSizeBufferElementType.Single => OpCodes.Stind_R4,
                FixedSizeBufferElementType.Double => OpCodes.Stind_R8,
                FixedSizeBufferElementType.None => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null),
                _ => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null)
            };

        public static void SerializeFixedSizeBuffer(ILProcessor processor, ParameterDefinition valueParam, FieldDefinition fixedField, ModuleDefinition module, MessagePackWriterHelper writer, ModuleImporter importer, FixedSizeBufferElementType elementType, int count, ref VariableDefinition? notPinnedVariable)
        {
            var writingElement = writer.WriteMessagePackPrimitive(GetType(module, elementType));
            var ldInd = LdInd(elementType);
            var stride = SizeOf(elementType);
            processor.Append(Instruction.Create(OpCodes.Ldarg_1)); // { writer }
            processor.Append(InstructionUtility.LdcI4(count)); // { writer, int32 }
            processor.Append(Instruction.Create(OpCodes.Call, writer.WriteArrayHeaderInt)); // { }

            processor.Append(Instruction.Create(OpCodes.Ldarg_1)); // { writer }
            processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam)); // { writer, value& }
            processor.Append(Instruction.Create(OpCodes.Ldflda, importer.Import(fixedField))); // { writer, field& }
            processor.Append(Instruction.Create(OpCodes.Conv_U)); // { writer, native uint }

            if (count == 1)
            {
                processor.Append(Instruction.Create(ldInd)); // { writer, binary }
                processor.Append(Instruction.Create(OpCodes.Call, writingElement)); // { }
            }
            else
            {
                if (notPinnedVariable is null)
                {
                    notPinnedVariable = new VariableDefinition(module.TypeSystem.IntPtr);
                    processor.Body.Variables.Add(notPinnedVariable);
                }

                processor.Append(Instruction.Create(OpCodes.Dup)); // { writer, native uint, native uint }
                processor.Append(InstructionUtility.StoreVariable(notPinnedVariable)); // { writer, native uint }
                processor.Append(Instruction.Create(ldInd)); // { writer, binary }
                processor.Append(Instruction.Create(OpCodes.Call, writingElement)); // { }

                for (var i = 1; i < count; i++)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1)); // { writer }
                    processor.Append(InstructionUtility.LoadVariable(notPinnedVariable)); // { writer, native uint }
                    processor.Append(InstructionUtility.LdcI4(stride)); // { writer, native uint, int32 }
                    processor.Append(Instruction.Create(OpCodes.Add)); // { writer, native uint }
                    if (i != count - 1)
                    {
                        processor.Append(Instruction.Create(OpCodes.Dup)); // { writer, native int, native int }
                        processor.Append(InstructionUtility.StoreVariable(notPinnedVariable)); // { writer, native int }
                    }

                    processor.Append(Instruction.Create(ldInd)); // { writer, binary }
                    processor.Append(Instruction.Create(OpCodes.Call, writingElement)); // { }
                }
            }
        }

        public static Instruction[] DeserializeFixedSizeBuffer(VariableDefinition targetVariable, FieldDefinition fixedField, ModuleDefinition module, MessagePackReaderHelper reader, ModuleImporter importer, SystemInvalidOperationExceptionHelper invalidOperationExceptionHelper, FixedSizeBufferElementType elementType, int count)
        {
            var same = Instruction.Create(OpCodes.Ldloca_S, targetVariable);
            var read = reader.ReadMessagePackPrimitive(GetType(module, elementType));
            var stride = SizeOf(elementType);
            switch (count)
            {
                case 1:
                    return new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_1),
                        Instruction.Create(OpCodes.Call, reader.ReadArrayHeader),
                        InstructionUtility.LdcI4(count),
                        Instruction.Create(OpCodes.Beq_S, same),
                        Instruction.Create(OpCodes.Ldstr, "Fixed size buffer field should have " + count.ToString(CultureInfo.InvariantCulture) + " element(s)."),
                        Instruction.Create(OpCodes.Newobj, invalidOperationExceptionHelper.Ctor),
                        Instruction.Create(OpCodes.Throw),
                        same,
                        Instruction.Create(OpCodes.Ldflda, importer.Import(fixedField)),
                        Instruction.Create(OpCodes.Ldarg_1),
                        Instruction.Create(OpCodes.Call, read),
                        Instruction.Create(StInd(elementType)),
                    };
                case 2:
                    return new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_1),
                        Instruction.Create(OpCodes.Call, reader.ReadArrayHeader),
                        InstructionUtility.LdcI4(count),
                        Instruction.Create(OpCodes.Beq_S, same),
                        Instruction.Create(OpCodes.Ldstr, "Fixed size buffer field should have " + count.ToString(CultureInfo.InvariantCulture) + " element(s)."),
                        Instruction.Create(OpCodes.Newobj, invalidOperationExceptionHelper.Ctor),
                        Instruction.Create(OpCodes.Throw),
                        same,
                        Instruction.Create(OpCodes.Ldflda, importer.Import(fixedField)),
                        Instruction.Create(OpCodes.Dup),
                        Instruction.Create(OpCodes.Ldarg_1),
                        Instruction.Create(OpCodes.Call, read),
                        Instruction.Create(StInd(elementType)),
                        InstructionUtility.LdcI4(stride),
                        Instruction.Create(OpCodes.Add),
                        Instruction.Create(OpCodes.Ldarg_1),
                        Instruction.Create(OpCodes.Call, read),
                        Instruction.Create(StInd(elementType)),
                    };
                default:
                    {
                        var answer = new Instruction[6 + (6 * count)];
                        answer[0] = Instruction.Create(OpCodes.Ldarg_1);
                        answer[1] = Instruction.Create(OpCodes.Call, reader.ReadArrayHeader);
                        answer[2] = InstructionUtility.LdcI4(count);
                        answer[3] = Instruction.Create(OpCodes.Beq_S, same);
                        answer[4] = Instruction.Create(OpCodes.Ldstr, "Fixed size buffer field should have " + count.ToString(CultureInfo.InvariantCulture) + " element(s). field : " + fixedField.FullName);
                        answer[5] = Instruction.Create(OpCodes.Newobj, invalidOperationExceptionHelper.Ctor);
                        answer[6] = Instruction.Create(OpCodes.Throw);
                        answer[7] = same;
                        answer[8] = Instruction.Create(OpCodes.Ldflda, importer.Import(fixedField));

                        answer[9] = Instruction.Create(OpCodes.Dup);
                        answer[10] = Instruction.Create(OpCodes.Ldarg_1);
                        answer[11] = Instruction.Create(OpCodes.Call, read);
                        answer[12] = Instruction.Create(StInd(elementType));

                        for (var i = 1; i < count - 1; i++)
                        {
                            var start = 7 + (i * 6);
                            answer[start++] = Instruction.Create(OpCodes.Dup);
                            answer[start++] = InstructionUtility.LdcI4(stride);
                            answer[start++] = Instruction.Create(OpCodes.Add);
                            answer[start++] = Instruction.Create(OpCodes.Ldarg_1);
                            answer[start++] = Instruction.Create(OpCodes.Call, read);
                            answer[start] = Instruction.Create(StInd(elementType));
                        }

                        answer[1 + (6 * count)] = InstructionUtility.LdcI4(stride);
                        answer[2 + (6 * count)] = Instruction.Create(OpCodes.Add);
                        answer[3 + (6 * count)] = Instruction.Create(OpCodes.Ldarg_1);
                        answer[4 + (6 * count)] = Instruction.Create(OpCodes.Call, read);
                        answer[5 + (6 * count)] = Instruction.Create(StInd(elementType));

                        return answer;
                    }
            }
        }
    }
}
