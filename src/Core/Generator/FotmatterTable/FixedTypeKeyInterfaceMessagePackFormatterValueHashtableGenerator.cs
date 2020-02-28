// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core
{
    public sealed class FixedTypeKeyInterfaceMessagePackFormatterValueHashtableGenerator : IFormatterTableGenerator
    {
        private const string TypeName = "FixedTypeKeyInterfaceMessagePackFormatterValueHashtable";
        private readonly FixedTypeKeyGenerator<FormatterInfo, TypeKeyInterfaceMessagePackFormatterValuePairGenerator> generator;
        private readonly ModuleImporter importer;
        private readonly SystemTypeHelper typeHelper;

        public FixedTypeKeyInterfaceMessagePackFormatterValueHashtableGenerator(ModuleDefinition module, TypeKeyInterfaceMessagePackFormatterValuePairGenerator pairGenerator, InterfaceMessagePackFormatterHelper interfaceMessagePackFormatter, SystemObjectHelper systemObjectHelper, SystemTypeHelper typeHelper, ModuleImporter importer, SystemArrayHelper arrayHelper, double loadFactor)
        {
            this.importer = importer;
            this.typeHelper = typeHelper;
            generator = new FixedTypeKeyGenerator<FormatterInfo, TypeKeyInterfaceMessagePackFormatterValuePairGenerator>(
                TypeName,
                module,
                pairGenerator,
                systemObjectHelper,
                typeHelper,
                importer,
                arrayHelper,
                loadFactor,
                LoadAppropriateValueFromFormatterInfo,
                info => info.SerializeTypeDefinition,
                new[] { Instruction.Create(OpCodes.Ldnull), });
        }

        public (TypeDefinition tableType, MethodDefinition getFormatter) Generate(FormatterInfo[] infos)
        {
            return generator.Generate(infos);
        }

        private void LoadAppropriateValueFromFormatterInfo(ILProcessor processor, FormatterInfo formatterInfo)
        {
            foreach (var constructorArgument in formatterInfo.FormatterConstructorArguments)
            {
                void Load(CustomAttributeArgument argument)
                {
                    var value = argument.Value;
                    switch (argument.Type.FullName)
                    {
                        case "System.Char":
                            processor.Append(InstructionUtility.LdcI4((char)value));
                            break;
                        case "System.Boolean":
                            processor.Append(InstructionUtility.LdcBoolean((bool)value));
                            break;
                        case "System.Type":
                            processor.Append(Instruction.Create(OpCodes.Ldtoken, importer.Import((TypeReference)value)));
                            processor.Append(Instruction.Create(OpCodes.Call, typeHelper.GetTypeFromHandle));
                            break;
                        case "System.Object":
                            processor.Append(Instruction.Create(OpCodes.Ldnull));
                            break;
                        case "System.Byte":
                            processor.Append(InstructionUtility.LdcI4((byte)value));
                            break;
                        case "System.SByte":
                            processor.Append(InstructionUtility.LdcI4((sbyte)value));
                            break;
                        case "System.Int16":
                            processor.Append(InstructionUtility.LdcI4((short)value));
                            break;
                        case "System.Int32":
                            processor.Append(InstructionUtility.LdcI4((int)value));
                            break;
                        case "System.Int64":
                            {
                                var (instruction, instructions) = InstructionUtility.LdcI8((long)value);
                                if (instructions is null)
                                {
#if CSHARP_8_0_OR_NEWER
                                    processor.Append(instruction!);
#else
                                    processor.Append(instruction);
#endif
                                }
                                else
                                {
                                    foreach (var instruction1 in instructions)
                                    {
                                        processor.Append(instruction1);
                                    }
                                }
                            }

                            break;
                        case "System.UInt16":
                            processor.Append(InstructionUtility.LdcI4((ushort)value));
                            break;
                        case "System.UInt32":
                            processor.Append(InstructionUtility.LdcI4((int)(uint)value));
                            break;
                        case "System.UInt64":
                            {
                                var (instruction, instructions) = InstructionUtility.LdcU8((ulong)value);
                                if (instructions is null)
                                {
#if CSHARP_8_0_OR_NEWER
                                    processor.Append(instruction!);
#else
                                    processor.Append(instruction);
#endif
                                }
                                else
                                {
                                    foreach (var instruction1 in instructions)
                                    {
                                        processor.Append(instruction1);
                                    }
                                }
                            }

                            break;
                        case "System.Single":
                            processor.Append(InstructionUtility.LdcR4((float)value));
                            break;
                        case "System.Double":
                            processor.Append(InstructionUtility.LdcR8((double)value));
                            break;
                        case "System.String":
                            processor.Append(InstructionUtility.LdStr((string)value));
                            break;
                        default: throw new MessagePackGeneratorResolveFailedException(argument.Type.FullName + " is not supported.");
                    }
                }

                Load(constructorArgument);
            }

            processor.Append(Instruction.Create(OpCodes.Newobj, importer.Import(formatterInfo.FormatterTypeConstructor))); // { Pair&, formatter }
        }
    }
}
