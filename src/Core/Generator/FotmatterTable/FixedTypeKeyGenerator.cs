// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Provider;
using System;

namespace MSPack.Processor.Core
{
    public sealed class FixedTypeKeyGenerator<TInfo, TValueGenerator>
        where TInfo : struct
        where TValueGenerator : class, IPairGenerator
    {
        private readonly string name;
        private readonly ModuleDefinition module;
        private readonly TValueGenerator pairGenerator;
        private readonly SystemObjectHelper systemObjectHelper;
        private readonly SystemTypeHelper typeHelper;
        private readonly ModuleImporter importer;
        private readonly SystemArrayHelper arrayHelper;
        private readonly double loadFactor;
        private readonly Action<ILProcessor, TInfo> loadValueFromInfoAction;
        private readonly Func<TInfo, TypeReference> ldTokenTypeReferenceFunc;
        private readonly Instruction[] loadDefaultReturnValue;

        public FixedTypeKeyGenerator(string name, ModuleDefinition module, TValueGenerator pairGenerator, SystemObjectHelper systemObjectHelper, SystemTypeHelper typeHelper, ModuleImporter importer, SystemArrayHelper arrayHelper, double loadFactor, Action<ILProcessor, TInfo> loadValueFromInfoAction, Func<TInfo, TypeReference> ldTokenTypeReferenceFunc, Instruction[] loadDefaultReturnValue)
        {
            this.name = name;
            this.module = module;
            this.pairGenerator = pairGenerator;
            this.loadValueFromInfoAction = loadValueFromInfoAction;
            this.ldTokenTypeReferenceFunc = ldTokenTypeReferenceFunc;
            this.systemObjectHelper = systemObjectHelper;
            this.typeHelper = typeHelper;
            this.importer = importer;
            this.arrayHelper = arrayHelper;
            this.loadFactor = loadFactor;
            this.loadDefaultReturnValue = loadDefaultReturnValue;
        }

        public (TypeDefinition nestedTableType, MethodDefinition getValue) Generate(TInfo[] infos)
        {
            var answer = new TypeDefinition(
                string.Empty,
                name,
                TypeAttributes.NestedAssembly | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.AutoLayout | TypeAttributes.AnsiClass,
                module.TypeSystem.Object);

            var pairArray = new ArrayType(pairGenerator.Pair);
            var pairArray2d = new ArrayType(pairArray);
            var tableField = new FieldDefinition("table", FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly, pairArray2d);
            answer.Fields.Add(tableField);

            var capacity = CalcCapacity((int)(infos.Length / loadFactor));
            var bitmask = capacity - 1;

            var cctor = GenerateConstructor(infos, capacity, bitmask, tableField, pairArray);
            cctor.Body.Optimize();
            answer.Methods.Add(cctor);

            var getPair = GenerateGetPair(bitmask, tableField, pairArray);
            getPair.Body.Optimize();
            answer.Methods.Add(getPair);

            return (answer, getPair);
        }

        private static int CalcCapacity(int length)
        {
            var answer = 1;
            while (answer < length)
            {
                answer <<= 1;
            }

            return answer;
        }

        private MethodDefinition GenerateGetPair(int bitmask, FieldDefinition tableField, ArrayType pairArray)
        {
            var getPair = new MethodDefinition(
                "GetPair",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Static,
                importer.Import(pairGenerator.Value.FieldType).Reference)
            {
                HasThis = false,
                Parameters =
                {
                    new ParameterDefinition("type", ParameterAttributes.None, typeHelper.Type),
                },
                Body =
                {
                    InitLocals = true,
                    Variables =
                    {
                        new VariableDefinition(pairArray),
                        new VariableDefinition(module.TypeSystem.IntPtr),
                    },
                },
            };
            var processor = getPair.Body.GetILProcessor();

            processor.Append(Instruction.Create(OpCodes.Ldsfld, tableField)); // { Pair[][] }
            processor.Append(InstructionUtility.LdcI4(bitmask)); // { Pair[][], int32 }
            processor.Append(Instruction.Create(OpCodes.Ldarg_0)); // { Pair[][], int32, Type }
            processor.Append(Instruction.Create(OpCodes.Callvirt, systemObjectHelper.GetHashCode)); // { Pair[][], int32, int32 }
            processor.Append(Instruction.Create(OpCodes.And)); // { Pair[][], int32 }
            processor.Append(Instruction.Create(OpCodes.Ldelem_Ref)); // { Pair[] }
            processor.Append(Instruction.Create(OpCodes.Dup)); // { Pair[], Pair[] }
            processor.Append(Instruction.Create(OpCodes.Stloc_0)); // { Pair[] }

            var condition = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Brtrue_S, condition)); // { }

            ReturnDefaultValue(processor);

            var loopStart = Instruction.Create(OpCodes.Ldloc_0);
            processor.Append(loopStart); // { Pair[] }
            processor.Append(Instruction.Create(OpCodes.Ldloc_1)); // { Pair[], native int }
            processor.Append(Instruction.Create(OpCodes.Ldelema, importer.Import(pairGenerator.Pair).Reference)); // { Pair& }
            processor.Append(Instruction.Create(OpCodes.Dup)); // { Pair&, Pair& }
            processor.Append(Instruction.Create(OpCodes.Ldfld, importer.Import(pairGenerator.Key))); // { Pair&, Type }
            processor.Append(Instruction.Create(OpCodes.Ldarg_0)); // { Pair&, Type, Type }
            var continuation = Instruction.Create(OpCodes.Pop);
            processor.Append(Instruction.Create(OpCodes.Bne_Un_S, continuation)); // { Pair& }

            processor.Append(Instruction.Create(OpCodes.Ldfld, importer.Import(pairGenerator.Value))); // { uint64 }
            processor.Append(Instruction.Create(OpCodes.Ret));

            processor.Append(continuation); // {  }
            processor.Append(Instruction.Create(OpCodes.Ldloc_1)); // { native int }
            processor.Append(InstructionUtility.LdcI4(1)); // { native int, int32 }
            processor.Append(Instruction.Create(OpCodes.Conv_I)); // { native int, native int }
            processor.Append(Instruction.Create(OpCodes.Add)); // { native int }
            processor.Append(Instruction.Create(OpCodes.Stloc_1)); // { }

            processor.Append(condition); // { native int }
            processor.Append(Instruction.Create(OpCodes.Ldloc_0)); // { native int, Pair[] }
            processor.Append(Instruction.Create(OpCodes.Ldlen)); // { native int, native int }
            processor.Append(Instruction.Create(OpCodes.Blt_S, loopStart)); // { }

            ReturnDefaultValue(processor);

            return getPair;
        }

        private void ReturnDefaultValue(ILProcessor processor)
        {
            foreach (var instruction in loadDefaultReturnValue)
            {
                processor.Append(instruction);
            }

            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private MethodDefinition GenerateConstructor(TInfo[] infos, int capacity, int bitmask, FieldDefinition tableField, ArrayType pairArray)
        {
            var cctor = new MethodDefinition(
                ".cctor",
                MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName | MethodAttributes.Static,
                module.TypeSystem.Void)
            {
                HasThis = false,
                Body =
                {
                    InitLocals = false,
                    Variables =
                    {
                        new VariableDefinition(typeHelper.Type),
                        new VariableDefinition(module.TypeSystem.Int32), // Pair[][] index
                        new VariableDefinition(module.TypeSystem.Int32), // Pair[] old length
                        new VariableDefinition(pairArray), // Pair[]
                    },
                },
            };

            var processor = cctor.Body.GetILProcessor();

            processor.Append(InstructionUtility.LdcI4(capacity)); // { int32 }
            processor.Append(Instruction.Create(OpCodes.Newarr, pairArray)); // { Pair[][] }
            processor.Append(Instruction.Create(OpCodes.Stsfld, tableField)); // { }

            var importedPairTypeReference = importer.Import(pairGenerator.Pair).Reference;
            for (var i = 0; i < infos.Length; i++)
            {
                ref var info = ref infos[i];

                processor.Append(Instruction.Create(OpCodes.Ldtoken, importer.Import(ldTokenTypeReferenceFunc(info)).Reference)); // { RuntimeTypeHandle }
                processor.Append(Instruction.Create(OpCodes.Call, typeHelper.GetTypeFromHandle)); // { Type }
                processor.Append(Instruction.Create(OpCodes.Stloc_0)); // { }
                processor.Append(Instruction.Create(OpCodes.Ldloc_0)); // { Type }
                processor.Append(Instruction.Create(OpCodes.Callvirt, systemObjectHelper.GetHashCode)); // { int32 }
                processor.Append(InstructionUtility.LdcI4(bitmask)); // { int32, int32 }
                processor.Append(Instruction.Create(OpCodes.And)); // { int32 }
                processor.Append(Instruction.Create(OpCodes.Stloc_1)); // { }
                processor.Append(Instruction.Create(OpCodes.Ldsfld, tableField)); // { Pair[][] }
                processor.Append(Instruction.Create(OpCodes.Ldloc_1)); // { Pair[][], int32 }
                processor.Append(Instruction.Create(OpCodes.Ldelem_Any, pairArray)); // { Pair[] }
                var resizeSectionStart = Instruction.Create(OpCodes.Ldsfld, tableField);
                processor.Append(Instruction.Create(OpCodes.Brtrue_S, resizeSectionStart)); // { }

                processor.Append(InstructionUtility.LdcI4(1)); // { int32 }
                processor.Append(Instruction.Create(OpCodes.Newarr, importedPairTypeReference)); // { Pair[] }
                processor.Append(Instruction.Create(OpCodes.Stloc_3)); // { }
                processor.Append(Instruction.Create(OpCodes.Ldsfld, tableField)); // { Pair[][] }
                processor.Append(Instruction.Create(OpCodes.Ldloc_1)); // { Pair[][], int32 }
                processor.Append(Instruction.Create(OpCodes.Ldloc_3)); // { Pair[][], int32, Pair[] }
                processor.Append(Instruction.Create(OpCodes.Stelem_Ref)); // { }
                processor.Append(InstructionUtility.LdcI4(0)); // { int32 }
                processor.Append(Instruction.Create(OpCodes.Stloc_2)); // { }
                var commonSectionStart = Instruction.Create(OpCodes.Ldloc_3);
                processor.Append(Instruction.Create(OpCodes.Br_S, commonSectionStart)); // { }

                processor.Append(resizeSectionStart); // { } -> { Pair[][] }
                processor.Append(Instruction.Create(OpCodes.Ldloc_1)); // { Pair[][], int32 }
                processor.Append(Instruction.Create(OpCodes.Ldelem_Ref)); // { Pair[] }
                processor.Append(Instruction.Create(OpCodes.Ldlen)); // { native int }
                processor.Append(Instruction.Create(OpCodes.Conv_I4)); // { int32 }
                processor.Append(Instruction.Create(OpCodes.Stloc_2)); // { }
                processor.Append(Instruction.Create(OpCodes.Ldsfld, tableField)); // { Pair[][] }
                processor.Append(Instruction.Create(OpCodes.Ldloc_1)); // { Pair[][], int32 }
                processor.Append(Instruction.Create(OpCodes.Ldelema, pairArray)); // { Pair[]& }
                processor.Append(Instruction.Create(OpCodes.Dup)); // { Pair[]&, Pair[]& }
                processor.Append(Instruction.Create(OpCodes.Ldloc_2)); // { Pair[]&, Pair[]&, int32 }
                processor.Append(InstructionUtility.LdcI4(1)); // { Pair[]&, Pair[]&, int32, int32 }
                processor.Append(Instruction.Create(OpCodes.Add)); // { Pair[]&, Pair[]&, int32 }
                processor.Append(Instruction.Create(OpCodes.Call, arrayHelper.Resize(pairGenerator.Pair))); // { Pair[]& }
                processor.Append(Instruction.Create(OpCodes.Ldind_Ref)); // { Pair[] }
                processor.Append(Instruction.Create(OpCodes.Stloc_3)); // { }

                processor.Append(commonSectionStart); // { } -> { Pair[] }
                processor.Append(Instruction.Create(OpCodes.Ldloc_2)); // { Pair[], int32 }
                processor.Append(Instruction.Create(OpCodes.Ldelema, importedPairTypeReference)); // { Pair& }
                processor.Append(Instruction.Create(OpCodes.Dup)); // { Pair&, Pair& }

                processor.Append(Instruction.Create(OpCodes.Ldloc_0)); // { Pair&, Pair&, Type }
                processor.Append(Instruction.Create(OpCodes.Stfld, importer.Import(pairGenerator.Key))); // { Pair& }

                loadValueFromInfoAction(processor, info);

                // { Pair&, uint64 }
                processor.Append(Instruction.Create(OpCodes.Stfld, importer.Import(pairGenerator.Value))); // { }
            }

            processor.Append(Instruction.Create(OpCodes.Ret));

            return cctor;
        }
    }
}
