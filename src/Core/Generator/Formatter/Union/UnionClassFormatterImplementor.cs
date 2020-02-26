// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class UnionClassFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly SystemObjectHelper objectHelper;
        private readonly InterfaceMessagePackFormatterHelper interfaceMessagePackFormatterHelper;
        private readonly SystemInvalidOperationExceptionHelper invalidOperationExceptionHelper;
        private readonly ModuleImporter importer;
        private readonly MessagePackSecurityHelper securityHelper;
        private readonly MessagePackSerializerOptionsHelper messagePackSerializerOptionsHelper;
        private readonly MessagePackWriterHelper writerHelper;
        private readonly MessagePackReaderHelper readerHelper;
        private readonly FormatterResolverExtensionHelper formatterResolverExtensionHelper;
        private readonly FixedTypeKeyUInt64ValueHashtableGenerator generator;

        public UnionClassFormatterImplementor(ModuleDefinition module, SystemObjectHelper objectHelper, InterfaceMessagePackFormatterHelper interfaceMessagePackFormatterHelper, SystemInvalidOperationExceptionHelper invalidOperationExceptionHelper, ModuleImporter importer, MessagePackSecurityHelper securityHelper, MessagePackSerializerOptionsHelper messagePackSerializerOptionsHelper, MessagePackWriterHelper writerHelper, MessagePackReaderHelper readerHelper, FormatterResolverExtensionHelper formatterResolverExtensionHelper, FixedTypeKeyUInt64ValueHashtableGenerator generator)
        {
            this.module = module;
            this.objectHelper = objectHelper;
            this.interfaceMessagePackFormatterHelper = interfaceMessagePackFormatterHelper;
            this.invalidOperationExceptionHelper = invalidOperationExceptionHelper;
            this.importer = importer;
            this.securityHelper = securityHelper;
            this.messagePackSerializerOptionsHelper = messagePackSerializerOptionsHelper;
            this.writerHelper = writerHelper;
            this.readerHelper = readerHelper;
            this.formatterResolverExtensionHelper = formatterResolverExtensionHelper;
            this.generator = generator;
        }

        public void Implement(in UnionClassSerializationInfo info, TypeDefinition formatter)
        {
            var iMessagePackFormatterGeneric = interfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(info.Definition);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric));
            formatter.Interfaces.Add(new InterfaceImplementation(interfaceMessagePackFormatterHelper.IMessagePackFormatterNoGeneric));

            formatter.Methods.Add(ConstructorUtility.GenerateDefaultConstructor(module, objectHelper));

            var (tableType, get) = generator.Generate(info.SerializationInfo);
            formatter.NestedTypes.Add(tableType);

            var serialize = GenerateSerialize(in info, get);
            serialize.Body.Optimize();
            formatter.Methods.Add(serialize);

            var deserialize = GenerateDeserialize(in info);
            deserialize.Body.Optimize();
            formatter.Methods.Add(deserialize);
        }

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in UnionClassSerializationInfo info)
        {
            var target = importer.Import(info.Definition);
            var targetVariable = new VariableDefinition(target);
            var deserialize = new MethodDefinition("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, target)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(readerHelper.Reader)),
                    new ParameterDefinition("options", ParameterAttributes.None, messagePackSerializerOptionsHelper.Options),
                },
                Body =
                {
                    InitLocals = true,
                    Variables =
                    {
                        targetVariable, // target
                    },
                },
            };

            var processor = deserialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, readerHelper.TryReadNil));

            var notNil = Instruction.Create(OpCodes.Ldarg_2);

            processor.Append(Instruction.Create(OpCodes.Brfalse_S, notNil));

            processor.Append(Instruction.Create(OpCodes.Ldnull));
            processor.Append(Instruction.Create(OpCodes.Ret));

            EnsureDepthStep(processor, notNil);
            var success = Instruction.Create(OpCodes.Ldarg_1);
            ReadArrayHeaderAndEnsureLength2(processor, success, info.Definition.FullName);

            processor.Append(success);
            processor.Append(Instruction.Create(OpCodes.Call, readerHelper.ReadInt32));
            if (info.MinKey != 0)
            {
                processor.Append(InstructionUtility.LdcI4(info.MinKey));
                processor.Append(Instruction.Create(OpCodes.Sub));
            }

            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(in info, targetVariable);
            processor.Append(Instruction.Create(OpCodes.Switch, switchTable));

            var nextInstruction = Instruction.Create(OpCodes.Ldarg_1);

            foreach (var instruction in defaultInstructions)
            {
                processor.Append(instruction);
            }

            processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));

            var @default = defaultInstructions[0];
            foreach (var instructions in switchInstructions)
            {
                if (ReferenceEquals(@default, instructions[0]))
                {
                    continue;
                }

                foreach (var instruction in instructions)
                {
                    processor.Append(instruction);
                }

                processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));
            }

            DecrementDepth(processor, nextInstruction);

            processor.Append(InstructionUtility.LoadVariable(targetVariable));
            processor.Append(Instruction.Create(OpCodes.Ret));

            return deserialize;
        }

        private (Instruction[][] switchInstrcutions, Instruction[] defaultInstructions, Instruction[] switchTable) GenerateSwitchStatements(in UnionClassSerializationInfo info, VariableDefinition targetVariable)
        {
            var defaultInstructions = new[] {
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, readerHelper.Skip),
            };
            var switchInstructions = new Instruction[info.MaxKey - info.MinKey + 1][];
            var switchTable = new Instruction[switchInstructions.Length];
            for (var index = 0; index < switchInstructions.Length; index++)
            {
                switchInstructions[index] = defaultInstructions;
                switchTable[index] = defaultInstructions[0];
            }

            for (var index = 0; index < info.SerializationInfo.Length; index++)
            {
                ref readonly var serializationInfo = ref info.SerializationInfo[index];
                var key = serializationInfo.Key - info.MinKey;
                switchTable[key] = (switchInstructions[key] = FillAnswer(serializationInfo, targetVariable))[0];
            }

            return (switchInstructions, defaultInstructions, switchTable);
        }

        private Instruction[] FillAnswer(in UnionSerializationInfo unionSerializationInfo, VariableDefinition targetVariable)
        {
            var formatterWithVerifyGeneric = formatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(unionSerializationInfo.Type);
            var iMessagePackFormatterGeneric = interfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(unionSerializationInfo.Type);
            var deserializeGeneric = interfaceMessagePackFormatterHelper.DeserializeGeneric(iMessagePackFormatterGeneric);
            if (unionSerializationInfo.Type.IsValueType)
            {
                return new[]
                {
                    Instruction.Create(OpCodes.Ldarg_2),
                    Instruction.Create(OpCodes.Callvirt, messagePackSerializerOptionsHelper.get_Resolver),
                    Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                    Instruction.Create(OpCodes.Ldarg_1),
                    Instruction.Create(OpCodes.Ldarg_2),
                    Instruction.Create(OpCodes.Callvirt, deserializeGeneric),
                    Instruction.Create(OpCodes.Box, importer.Import(unionSerializationInfo.Type)),
                    InstructionUtility.StoreVariable(targetVariable),
                };
            }
            else
            {
                return new[]
                {
                    Instruction.Create(OpCodes.Ldarg_2),
                    Instruction.Create(OpCodes.Callvirt, messagePackSerializerOptionsHelper.get_Resolver),
                    Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                    Instruction.Create(OpCodes.Ldarg_1),
                    Instruction.Create(OpCodes.Ldarg_2),
                    Instruction.Create(OpCodes.Callvirt, deserializeGeneric),
                    InstructionUtility.StoreVariable(targetVariable),
                };
            }
        }

        private void ReadArrayHeaderAndEnsureLength2(ILProcessor processor, Instruction success, string name)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, readerHelper.ReadArrayHeader));
            processor.Append(InstructionUtility.LdcI4(2));
            processor.Append(Instruction.Create(OpCodes.Beq_S, success));
            processor.Append(Instruction.Create(OpCodes.Ldstr, "Invalid Union data was detected. type : " + name));
            processor.Append(Instruction.Create(OpCodes.Newobj, invalidOperationExceptionHelper.Ctor));
            processor.Append(Instruction.Create(OpCodes.Throw));
        }

        private void EnsureDepthStep(ILProcessor processor, Instruction notNil)
        {
            processor.Append(notNil);
            processor.Append(Instruction.Create(OpCodes.Callvirt, messagePackSerializerOptionsHelper.get_Security));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Callvirt, securityHelper.DepthStep));
        }

        private void DecrementDepth(ILProcessor processor, Instruction nextInstruction)
        {
            processor.Append(nextInstruction);
            processor.Append(Instruction.Create(OpCodes.Dup));
            processor.Append(Instruction.Create(OpCodes.Call, readerHelper.get_Depth));
            processor.Append(InstructionUtility.LdcI4(1));
            processor.Append(Instruction.Create(OpCodes.Sub));
            processor.Append(Instruction.Create(OpCodes.Call, readerHelper.set_Depth));
        }
        #endregion

        #region Serialize
        private MethodDefinition GenerateSerialize(in UnionClassSerializationInfo info, MethodDefinition get)
        {
            var keyVariable = new VariableDefinition(module.TypeSystem.UInt64);
            var serialize = new MethodDefinition("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, module.TypeSystem.Void)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(writerHelper.Writer)),
                    new ParameterDefinition("value", ParameterAttributes.None, importer.Import(info.Definition)),
                    new ParameterDefinition("options", ParameterAttributes.None, messagePackSerializerOptionsHelper.Options),
                },
                Body =
                {
                    InitLocals = false,
                    Variables =
                    {
                        keyVariable,
                    },
                },
            };

            var processor = serialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_2));

            var success = Instruction.Create(OpCodes.Ldarg_2);
            processor.Append(Instruction.Create(OpCodes.Brtrue_S, success));

            var fail = Instruction.Create(OpCodes.Ldarg_1);
            processor.Append(Instruction.Create(OpCodes.Br_S, fail));

            processor.Append(success);
            processor.Append(Instruction.Create(OpCodes.Callvirt, objectHelper.GetType));
            processor.Append(Instruction.Create(OpCodes.Call, get));
            processor.Append(Instruction.Create(OpCodes.Dup));
            processor.Append(InstructionUtility.StoreVariable(keyVariable));
            processor.Append(InstructionUtility.LdcI4(-1));
            processor.Append(Instruction.Create(OpCodes.Conv_I8));
            var success1 = Instruction.Create(OpCodes.Ldarg_1);
            processor.Append(Instruction.Create(OpCodes.Bne_Un_S, success1));

            processor.Append(fail);
            processor.Append(Instruction.Create(OpCodes.Call, writerHelper.WriteNil));
            processor.Append(Instruction.Create(OpCodes.Ret));

            processor.Append(success1);
            processor.Append(InstructionUtility.LdcI4(2));
            processor.Append(Instruction.Create(OpCodes.Call, writerHelper.WriteArrayHeaderInt));

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(InstructionUtility.LoadVariable(keyVariable));
            processor.Append(InstructionUtility.LdcI4(32));
            processor.Append(Instruction.Create(OpCodes.Shr_Un));
            processor.Append(Instruction.Create(OpCodes.Conv_U4));
            processor.Append(Instruction.Create(OpCodes.Call, writerHelper.WriteInt));

            processor.Append(InstructionUtility.LoadVariable(keyVariable));
            processor.Append(Instruction.Create(OpCodes.Conv_U4));

            var (switchInstructions, switchTable) = GenerateSerializeSwitchTable(in info);
            processor.Append(Instruction.Create(OpCodes.Switch, switchTable));

            processor.Append(Instruction.Create(OpCodes.Ret));

            foreach (var instructions in switchInstructions)
            {
                foreach (var instruction in instructions)
                {
                    processor.Append(instruction);
                }

                processor.Append(Instruction.Create(OpCodes.Ret));
            }

            return serialize;
        }

        private (Instruction[][], Instruction[]) GenerateSerializeSwitchTable(in UnionClassSerializationInfo info)
        {
            var switchInstructions = new Instruction[info.SerializationInfo.Length][];
            var switchTable = new Instruction[switchInstructions.Length];

            for (var i = 0; i < switchInstructions.Length; i++)
            {
                ref readonly var unionSerializationInfo = ref info.SerializationInfo[i];
                var formatterWithVerifyGeneric = formatterResolverExtensionHelper.GetFormatterWithVerifyGeneric(unionSerializationInfo.Type);
                var iMessagePackFormatterGeneric = interfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(unionSerializationInfo.Type);
                var serializeGeneric = interfaceMessagePackFormatterHelper.SerializeGeneric(iMessagePackFormatterGeneric);
                switchInstructions[i] = new[]
                {
                    Instruction.Create(OpCodes.Ldarg_3),
                    Instruction.Create(OpCodes.Callvirt, messagePackSerializerOptionsHelper.get_Resolver),
                    Instruction.Create(OpCodes.Call, formatterWithVerifyGeneric),
                    Instruction.Create(OpCodes.Ldarg_1),
                    Instruction.Create(OpCodes.Ldarg_2),
                    Instruction.Create(OpCodes.Castclass, importer.Import(unionSerializationInfo.Type)),
                    Instruction.Create(OpCodes.Ldarg_3),
                    Instruction.Create(OpCodes.Callvirt, serializeGeneric),
                };

                switchTable[i] = switchInstructions[i][0];
            }

            return (switchInstructions, switchTable);
        }

        #endregion
    }
}
