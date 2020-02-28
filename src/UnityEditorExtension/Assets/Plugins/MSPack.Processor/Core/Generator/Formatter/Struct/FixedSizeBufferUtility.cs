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
        private static int SizeOf(FixedSizeBufferElementType elementType)
        {
            switch (elementType)
            {
                case FixedSizeBufferElementType.Boolean:
                case FixedSizeBufferElementType.SByte:
                case FixedSizeBufferElementType.Byte:
                    return 1;
                case FixedSizeBufferElementType.Char:
                case FixedSizeBufferElementType.Int16:
                case FixedSizeBufferElementType.UInt16:
                    return 2;
                case FixedSizeBufferElementType.Int32:
                case FixedSizeBufferElementType.UInt32:
                case FixedSizeBufferElementType.Single:
                    return 4;
                case FixedSizeBufferElementType.Int64:
                case FixedSizeBufferElementType.UInt64:
                case FixedSizeBufferElementType.Double:
                    return 8;
                case FixedSizeBufferElementType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        private static TypeReference GetType(ModuleDefinition module, FixedSizeBufferElementType elementType)
        {
            switch (elementType)
            {
                case FixedSizeBufferElementType.Boolean:
                    return module.TypeSystem.Boolean;
                case FixedSizeBufferElementType.Char:
                    return module.TypeSystem.Char;
                case FixedSizeBufferElementType.SByte:
                    return module.TypeSystem.SByte;
                case FixedSizeBufferElementType.Int16:
                    return module.TypeSystem.Int16;
                case FixedSizeBufferElementType.Int32:
                    return module.TypeSystem.Int32;
                case FixedSizeBufferElementType.Int64:
                    return module.TypeSystem.Int64;
                case FixedSizeBufferElementType.Byte:
                    return module.TypeSystem.Byte;
                case FixedSizeBufferElementType.UInt16:
                    return module.TypeSystem.UInt16;
                case FixedSizeBufferElementType.UInt32:
                    return module.TypeSystem.UInt32;
                case FixedSizeBufferElementType.UInt64:
                    return module.TypeSystem.UInt64;
                case FixedSizeBufferElementType.Single:
                    return module.TypeSystem.Single;
                case FixedSizeBufferElementType.Double:
                    return module.TypeSystem.Double;
                case FixedSizeBufferElementType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        private static OpCode LdInd(FixedSizeBufferElementType elementType)
        {
            switch (elementType)
            {
                case FixedSizeBufferElementType.SByte:
                    return OpCodes.Ldind_I1;
                case FixedSizeBufferElementType.Int16:
                    return OpCodes.Ldind_I2;
                case FixedSizeBufferElementType.Int32:
                    return OpCodes.Ldind_I4;
                case FixedSizeBufferElementType.Boolean:
                case FixedSizeBufferElementType.Byte:
                    return OpCodes.Ldind_U1;
                case FixedSizeBufferElementType.Char:
                case FixedSizeBufferElementType.UInt16:
                    return OpCodes.Ldind_U2;
                case FixedSizeBufferElementType.UInt32:
                    return OpCodes.Ldind_U4;
                case FixedSizeBufferElementType.Int64:
                case FixedSizeBufferElementType.UInt64:
                    return OpCodes.Ldind_I8;
                case FixedSizeBufferElementType.Single:
                    return OpCodes.Ldind_R4;
                case FixedSizeBufferElementType.Double:
                    return OpCodes.Ldind_R8;
                case FixedSizeBufferElementType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        private static OpCode StInd(FixedSizeBufferElementType elementType)
        {
            switch (elementType)
            {
                case FixedSizeBufferElementType.Boolean:
                case FixedSizeBufferElementType.SByte:
                case FixedSizeBufferElementType.Byte:
                    return OpCodes.Stind_I1;
                case FixedSizeBufferElementType.Char:
                case FixedSizeBufferElementType.Int16:
                case FixedSizeBufferElementType.UInt16:
                    return OpCodes.Stind_I2;
                case FixedSizeBufferElementType.Int32:
                case FixedSizeBufferElementType.UInt32:
                    return OpCodes.Stind_I4;
                case FixedSizeBufferElementType.Int64:
                case FixedSizeBufferElementType.UInt64:
                    return OpCodes.Stind_I8;
                case FixedSizeBufferElementType.Single:
                    return OpCodes.Stind_R4;
                case FixedSizeBufferElementType.Double:
                    return OpCodes.Stind_R8;
                case FixedSizeBufferElementType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

#if CSHARP_8_0_OR_NEWER
        public static void SerializeFixedSizeBuffer(ILProcessor processor, ParameterDefinition valueParam, FieldDefinition fixedField, ModuleDefinition module, MessagePackWriterHelper writer, ModuleImporter importer, FixedSizeBufferElementType elementType, int count, ref VariableDefinition? notPinnedVariable)
#else
        public static void SerializeFixedSizeBuffer(ILProcessor processor, ParameterDefinition valueParam, FieldDefinition fixedField, ModuleDefinition module, MessagePackWriterHelper writer, ModuleImporter importer, FixedSizeBufferElementType elementType, int count, ref VariableDefinition notPinnedVariable)
#endif
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
