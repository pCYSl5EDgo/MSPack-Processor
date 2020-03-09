// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSPack.Processor.Core.Embed
{
    public sealed class AutomataEmbeddingHelper
    {
        private readonly TypeDefinition resolver;
        private readonly SystemReadOnlySpanHelper spanHelper;
        private readonly DataHelper dataHelper;
        private readonly ModuleDefinition module;
        private readonly List<(string[], MethodDefinition)> stringKeys = new List<(string[], MethodDefinition)>();

#if CSHARP_8_0_OR_NEWER
        private TypeDefinition? deserializeHelperType;
#else
        private TypeDefinition deserializeHelperType;
#endif

        public AutomataEmbeddingHelper(TypeDefinition resolver, SystemReadOnlySpanHelper spanHelper, DataHelper dataHelper)
        {
            this.resolver = resolver;
            this.spanHelper = spanHelper;
            this.dataHelper = dataHelper;
            module = resolver.Module;
        }

        public MethodDefinition GetIndex(FieldSerializationInfo[] fields, PropertySerializationInfo[] properties)
        {
            var array = new string[fields.Length + properties.Length];
            for (var i = 0; i < fields.Length; i++)
            {
                array[i] = fields[i].StringKey;
            }

            for (var i = 0; i < properties.Length; i++)
            {
                array[i + fields.Length] = properties[i].StringKey;
            }

            Array.Sort(array, StringComparer.InvariantCulture);
            var foundPair = stringKeys.FirstOrDefault(x => x.Item1.SequenceEqual(array));

            return foundPair.Item2 ?? CreateMethod(array, fields, properties);
        }

        private string DefineGetIndexName(string[] array)
        {
            var buffer = new StringBuilder()
                .Append("GetIndex_")
                .Append(GetDeserializeHelperType().Methods.Count.ToString("D4"))
                .Append("_")
                .Append(array.Length.ToString("D4"));
            return buffer.ToString();
        }

        private MethodDefinition CreateMethod(string[] array, FieldSerializationInfo[] fields, PropertySerializationInfo[] properties)
        {
            var spanVariable = new VariableDefinition(spanHelper.ReadOnlySpanByte());
            var spanParam = new ParameterDefinition("span", ParameterAttributes.None, spanHelper.ReadOnlySpanByte());
            var answer = new MethodDefinition(
                DefineGetIndexName(array),
                MethodAttributes.Static | MethodAttributes.Public | MethodAttributes.HideBySig,
                module.TypeSystem.Int32)
            {
                HasThis = false,
                Parameters =
                {
                    spanParam,
                },
                Body =
                {
                    InitLocals = true,
                    Variables =
                    {
                        spanVariable,
                    },
                }
            };
            GetDeserializeHelperType().Methods.Add(answer);
            stringKeys.Add((array, answer));

            ImplementMethod(array, fields, properties, answer, spanVariable, spanParam);
            return answer;
        }

        private void ImplementMethod(string[] array, FieldSerializationInfo[] fields, PropertySerializationInfo[] properties, MethodDefinition answer, VariableDefinition spanVariable, ParameterDefinition spanParam)
        {
            var processor = answer.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_0));
            processor.Append(InstructionUtility.Store(spanVariable));

            var uint32 = default(VariableDefinition);

            VariableDefinition UInt32VariableDefinitionGenerator()
            {
                if (uint32 is null)
                {
                    uint32 = new VariableDefinition(module.TypeSystem.UInt32);
                    answer.Body.Variables.Add(uint32);
                }

                return uint32;
            }

            var uint64 = default(VariableDefinition);

            VariableDefinition UInt64VariableDefinitionGenerator()
            {
                if (uint64 is null)
                {
                    uint64 = new VariableDefinition(module.TypeSystem.UInt64);
                    answer.Body.Variables.Add(uint64);
                }

                return uint64;
            }

            var int32 = default(VariableDefinition);

            VariableDefinition Int32VariableDefinitionGenerator()
            {
                if (int32 is null)
                {
                    int32 = new VariableDefinition(module.TypeSystem.Int32);
                    answer.Body.Variables.Add(int32);
                }

                return int32;
            }

            var option = new AutomataOption(spanVariable, UInt32VariableDefinitionGenerator, UInt64VariableDefinitionGenerator, Int32VariableDefinitionGenerator, spanParam, spanHelper);
            var tuples = new AutomataTuple[array.Length];
            var index = 0;
            for (var i = 0; i < fields.Length; i++, index++)
            {
                ref readonly var serializationInfo = ref fields[i];
                var embedBytes = EmbeddedStringHelper.Encode(serializationInfo.StringKey);
                if (!dataHelper.TryGetOrAdd(embedBytes, out var field))
                {
                    throw new MessagePackGeneratorResolveFailedException("invalid string key. type : " + serializationInfo.Definition.DeclaringType.FullName);
                }

                tuples[index] = new AutomataTuple(index, embedBytes, field);
            }

            for (var i = 0; i < properties.Length; i++, index++)
            {
                ref readonly var serializationInfo = ref properties[i];
                var embedBytes = EmbeddedStringHelper.Encode(serializationInfo.StringKey);
                if (!dataHelper.TryGetOrAdd(embedBytes, out var field))
                {
                    throw new MessagePackGeneratorResolveFailedException("invalid string key. type : " + serializationInfo.Definition.DeclaringType.FullName);
                }

                tuples[index] = new AutomataTuple(index, embedBytes, field);
            }

            var embeddedInstructions = Embed(tuples, option);
            foreach (var embeddedInstruction in embeddedInstructions)
            {
                processor.Append(embeddedInstruction);
            }

            answer.Body.Optimize();
        }

        public TypeDefinition GetDeserializeHelperType()
        {
            if (deserializeHelperType is null)
            {
                deserializeHelperType = new TypeDefinition(
                    string.Empty,
                    "AutomataDeserializeHelper",
                    TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.NestedPublic | TypeAttributes.BeforeFieldInit,
                    module.TypeSystem.Object
                );
                resolver.NestedTypes.Add(deserializeHelperType);
            }

            return deserializeHelperType;
        }

        private Instruction[] Embed(AutomataTuple[] tuples, in AutomataOption option)
        {
            Array.Sort(tuples, new DefaultSorter());
            switch (tuples.Length)
            {
                case 0: throw new ArgumentException();
                case 1:
                    return EmbedOne(in tuples[0], in option);
                case 2:
                    return tuples[0].Length == tuples[1].Length
                        ? Embed2SameLength(in tuples[0], in tuples[1], in option)
                        : Embed2Different(in tuples[0], in tuples[1], in option);
                default:
                    var variationCount = 1;
                    var oldLength = tuples[0].Length;
                    for (var index = 1; index < tuples.Length; index++)
                    {
                        ref readonly var tuple = ref tuples[index];
                        if (tuple.Length == oldLength)
                        {
                            continue;
                        }

                        oldLength = tuple.Length;
                        variationCount++;
                    }

                    switch (variationCount)
                    {
                        case 1:
                            var (item1, item2) = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, 0, tuples.Length, in option, 0);
                            var spanの長さが間違いなく指定した数値であることを確認する = Spanの長さが間違いなく指定した数値であることを確認するまたはマイナス壱を返す(option, tuples[0].Length, item1[0]);
                            return InstructionConcatHelper.Concat(
                                spanの長さが間違いなく指定した数値であることを確認する,
                                item1,
                                item2
                            );
                        case 2:
                            return InstructionConcatHelper.Concat(EmbedMulti2Lengths(tuples, in option));
                        default:
                            return EmbedMultiSwitch(tuples, in option);
                    }
            }
        }

        private (Instruction[], Instruction[], Instruction[], Instruction[], Instruction[], Instruction[]) EmbedMulti2Lengths(AutomataTuple[] tuples, in AutomataOption option)
        {
            var lesserLength = tuples[0].Length;
            var greaterLength = tuples[tuples.Length - 1].Length;
            var lengthVariable = option.Int32VariableDefinition();
            var lesserCount = 1;
            for (var i = 1; i < tuples.Length; i++)
            {
                if (lesserLength != tuples[i].Length)
                {
                    lesserCount = i;
                    break;
                }
            }

            var 短い方の探索結果 = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, 0, lesserCount, option, 0);
            var 長い方の探索結果 = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, lesserCount, tuples.Length - lesserCount, option, 0);
            var 初期探索結果 = new[]
            {
                InstructionUtility.Load(lengthVariable),
                InstructionUtility.LdcI4(lesserLength),
                Instruction.Create(OpCodes.Beq_S, 短い方の探索結果.Item1[0]),
                InstructionUtility.Load(lengthVariable),
                InstructionUtility.LdcI4(greaterLength),
                Instruction.Create(OpCodes.Beq, 長い方の探索結果.Item1[0]),
                InstructionUtility.LdcI4(-1),
                Instruction.Create(OpCodes.Ret),
            };
            var spanの長さを格納する = Spanの長さを格納する(option);
            return (spanの長さを格納する, 初期探索結果, 短い方の探索結果.Item1, 短い方の探索結果.Item2, 長い方の探索結果.Item1, 長い方の探索結果.Item2);
        }

        private Instruction[] EmbedMultiSwitch(AutomataTuple[] tuples, in AutomataOption option)
        {
            var shortestLength = tuples[0].Length;
            var switchTable = new Instruction[tuples[tuples.Length - 1].Length - shortestLength + 1];
            var failInstruction = InstructionUtility.LdcI4(-1);
            for (var i = 0; i < switchTable.Length; i++)
            {
                switchTable[i] = failInstruction;
            }

            var list = new List<Instruction>
            {
                InstructionUtility.LoadAddress(option.Span),
                Instruction.Create(OpCodes.Call, spanHelper.GetLengthByte()),
                InstructionUtility.LdcI4(shortestLength),
                Instruction.Create(OpCodes.Sub),
                Instruction.Create(OpCodes.Switch, switchTable),
                failInstruction,
                Instruction.Create(OpCodes.Ret),
            };
            List追加(tuples, option, shortestLength, switchTable, list);
            return list.ToArray();
        }

        private static void List追加(AutomataTuple[] tuples, in AutomataOption option, int shortestLength, Instruction[] switchTable, List<Instruction> list)
        {
            var tuplesOffset = 0;
            var tuplesCount = 1;
            var oldLength = shortestLength;
            while (tuplesOffset + tuplesCount != tuples.Length)
            {
                ref readonly var nextTuple = ref tuples[tuplesOffset + tuplesCount];
                if (nextTuple.Length == oldLength)
                {
                    tuplesCount++;
                }
                else
                {
                    List追加(option, switchTable, tuples, list, nextTuple, shortestLength, ref tuplesOffset, ref tuplesCount, ref oldLength);
                }
            }

            List追加(option, switchTable, tuples, list, shortestLength, tuplesOffset, tuplesCount, oldLength);
        }

        private static void List追加(in AutomataOption option, Instruction[] switchTable, AutomataTuple[] tuples, List<Instruction> list, in AutomataTuple nextTuple, int shortestLength, ref int tuplesOffset, ref int tuplesCount, ref int oldLength)
        {
            List追加(option, switchTable, tuples, list, shortestLength, tuplesOffset, tuplesCount, oldLength);
            oldLength = nextTuple.Length;
            tuplesOffset += tuplesCount;
            tuplesCount = 1;
        }

        private static void List追加(in AutomataOption option, Instruction[] switchTable, in AutomataTuple[] tuples, List<Instruction> list, int shortestLength, int tuplesOffset, int tuplesCount, int oldLength)
        {
            var (item1, item2) = ForSpanLengthDefinedHelper.EmbedForSpanLengthDefined(tuples, tuplesOffset, tuplesCount, option, 0);
            switchTable[oldLength - shortestLength] = item1[0];
            list.AddRange(item1);
            list.AddRange(item2);
        }

        private Instruction[] Spanの長さを格納する(in AutomataOption option)
        {
            var getLengthByte = spanHelper.GetLengthByte();
            var int32Variable = option.Int32VariableDefinition();
            return new[]
            {
                InstructionUtility.LoadAddress(option.Span),
                Instruction.Create(OpCodes.Call, getLengthByte),
                InstructionUtility.Store(int32Variable),
            };
        }

        private Instruction[] Spanの長さが間違いなく指定した数値であることを確認するまたはマイナス壱を返す(in AutomataOption option, int length, Instruction whenSuccess)
        {
            var getLengthByte = spanHelper.GetLengthByte();
            return new[]
            {
                InstructionUtility.LoadAddress(option.Span),
                Instruction.Create(OpCodes.Call, getLengthByte),
                InstructionUtility.LdcI4(length),
                Instruction.Create(OpCodes.Beq_S, whenSuccess),
                InstructionUtility.LdcI4(-1),
                Instruction.Create(OpCodes.Ret),
            };
        }

        private Instruction[] Embed2Different(in AutomataTuple first, in AutomataTuple second, in AutomataOption option)
        {
            var length = option.Int32VariableDefinition();

            var fail = InstructionUtility.LdcI4(-1);
            var firstLengthSame = CompareSequenceEqualityHelper.CompareSequentialEquality(first, option.Span, spanHelper, 0, first.Length, fail, new[]
            {
                InstructionUtility.LdcI4(first.Index),
                Instruction.Create(OpCodes.Ret),
            });
            var secondLengthSame = CompareSequenceEqualityHelper.CompareSequentialEquality(second, option.Span, spanHelper, 0, second.Length, fail, new[]
            {
                InstructionUtility.LdcI4(second.Index),
                Instruction.Create(OpCodes.Ret),
            });

            return InstructionConcatHelper.Concat(
                new[]
                {
                    InstructionUtility.LoadAddress(option.Span),
                    Instruction.Create(OpCodes.Call, spanHelper.GetLengthByte()),
                    Instruction.Create(OpCodes.Dup),
                    InstructionUtility.Store(length),
                    InstructionUtility.LdcI4(first.Length),
                    Instruction.Create(OpCodes.Beq_S, firstLengthSame[0]),
                    InstructionUtility.Load(length),
                    InstructionUtility.LdcI4(second.Length),
                    Instruction.Create(OpCodes.Beq, secondLengthSame[0]),
                    fail,
                    Instruction.Create(OpCodes.Ret),
                },
                firstLengthSame,
                secondLengthSame);
        }

        private Instruction[] Embed2SameLength(in AutomataTuple first, in AutomataTuple second, in AutomataOption option)
        {
            var sameLength = CalcSameLength(first, second);

            var fail = InstructionUtility.LdcI4(-1);
            var secondMatch = CompareSequenceEqualityHelper.CompareSequentialEquality(second, option.Span, spanHelper, sameLength, second.Length - sameLength, fail, new[]
            {
                InstructionUtility.LdcI4(second.Index),
                Instruction.Create(OpCodes.Ret),
            });
            var firstMatch = CompareSequenceEqualityHelper.CompareSequentialEquality(first, option.Span, spanHelper, sameLength, first.Length - sameLength, secondMatch[0], new[]
            {
                InstructionUtility.LdcI4(first.Index),
                Instruction.Create(OpCodes.Ret),
            });

            if (sameLength == 0)
            {
                return InstructionConcatHelper.Concat(
                    new[]
                    {
                        InstructionUtility.LoadAddress(option.Span),
                        Instruction.Create(OpCodes.Call, spanHelper.GetLengthByte()),
                        InstructionUtility.LdcI4(first.Length),
                        Instruction.Create(OpCodes.Beq_S, firstMatch[0]),

                        fail,
                        Instruction.Create(OpCodes.Ret),
                    },
                    firstMatch,
                    secondMatch);
            }

            var sameMatch = CompareSequenceEqualityHelper.CompareSequentialEquality(first, option.Span, spanHelper, 0, sameLength, fail, Array.Empty<Instruction>());
            return InstructionConcatHelper.Concat(new[]
                {
                    InstructionUtility.LoadAddress(option.Span),
                    Instruction.Create(OpCodes.Call, spanHelper.GetLengthByte()),
                    InstructionUtility.LdcI4(first.Length),
                    Instruction.Create(OpCodes.Beq_S, sameMatch[0]),

                    fail,
                    Instruction.Create(OpCodes.Ret),
                },
                sameMatch,
                firstMatch,
                secondMatch);
        }

        private Instruction[] EmbedOne(in AutomataTuple tuple, in AutomataOption option)
        {
            var fail = InstructionUtility.LdcI4(-1);
            var match = CompareSequenceEqualityHelper.CompareSequentialEquality(tuple, option.Span, spanHelper, 0, tuple.Length, fail, new[]
            {
                InstructionUtility.LdcI4(tuple.Index),
                Instruction.Create(OpCodes.Ret),
            });
            return InstructionConcatHelper.Concat(new[]
                {
                    InstructionUtility.LoadAddress(option.Span),
                    Instruction.Create(OpCodes.Call, spanHelper.GetLengthByte()),
                    InstructionUtility.LdcI4(tuple.Length),
                    Instruction.Create(OpCodes.Beq_S, match[0]),

                    fail,
                    Instruction.Create(OpCodes.Ret),
                },
                match);
        }

        private static int CalcSameLength(in AutomataTuple tuple0, in AutomataTuple tuple1)
        {
            for (var i = 0; i < tuple0.Length; i++)
            {
                if (tuple0[i] != tuple1[i])
                {
                    return i;
                }
            }

            return tuple0.Length;
        }
    }
}