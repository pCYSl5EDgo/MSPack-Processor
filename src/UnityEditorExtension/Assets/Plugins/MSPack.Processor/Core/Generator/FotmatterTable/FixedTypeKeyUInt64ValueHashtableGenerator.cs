// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core
{
    public sealed class FixedTypeKeyUInt64ValueHashtableGenerator
    {
        private const string TypeName = "FixedTypeKeyUInt64ValueHashtable";
        private readonly FixedTypeKeyGenerator<UnionSerializationInfo, TypeKeyUInt64ValuePairGenerator> generator;

        public FixedTypeKeyUInt64ValueHashtableGenerator(ModuleDefinition module, TypeKeyUInt64ValuePairGenerator pairGenerator, SystemObjectHelper systemObjectHelper, SystemTypeHelper typeHelper, ModuleImporter importer, SystemArrayHelper arrayHelper, double loadFactor)
        {
            generator = new FixedTypeKeyGenerator<UnionSerializationInfo, TypeKeyUInt64ValuePairGenerator>(
                TypeName,
                module,
                pairGenerator,
                systemObjectHelper,
                typeHelper,
                importer,
                arrayHelper,
                loadFactor,
                (ILProcessor processor, UnionSerializationInfo unionInfo) =>
                {
                    var (instruction, instructions) = InstructionUtility.LdcU8(unionInfo.Value);

                    if (instructions is null)
                    {
                        processor.Append(instruction);
                    }
                    else
                    {
                        foreach (var inst in instructions)
                        {
                            processor.Append(inst);
                        }
                    }
                }, (UnionSerializationInfo unionInfo) => unionInfo.Type, new[] { Instruction.Create(OpCodes.Ldc_I4_M1), Instruction.Create(OpCodes.Conv_I8), });
        }

        public (TypeDefinition tableType, MethodDefinition getPairULongValue) Generate(UnionSerializationInfo[] unionInfos) => generator.Generate(unionInfos);
    }
}
