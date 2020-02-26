// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace MSPack.Processor.Core
{
    public sealed class FixedTypeKeyInt32ValueHashtableGenerator
    {
        private const string TypeName = "FixedTypeKeyInt32ValueHashtable";
        private readonly FixedTypeKeyGenerator<UnionSerializationInfo, TypeKeyInt32ValuePairGenerator> generator;

        public FixedTypeKeyInt32ValueHashtableGenerator(ModuleDefinition module, TypeKeyInt32ValuePairGenerator pairGenerator, SystemObjectHelper systemObjectHelper, SystemTypeHelper typeHelper, ModuleImporter importer, SystemArrayHelper arrayHelper, double loadFactor)
        {
            generator = new FixedTypeKeyGenerator<UnionSerializationInfo, TypeKeyInt32ValuePairGenerator>(
                TypeName,
                module,
                pairGenerator,
                systemObjectHelper,
                typeHelper,
                importer,
                arrayHelper,
                loadFactor,
                (processor, unionInfo) =>
                {
                    processor.Append(InstructionUtility.LdcI4(unionInfo.Index));
                },
                unionInfo => unionInfo.Type,
                new[] { Instruction.Create(OpCodes.Ldc_I4_M1), });
        }

        public (TypeDefinition tableType, MethodDefinition getPair) Generate(UnionSerializationInfo[] unionInfos) => generator.Generate(unionInfos);
    }
}
