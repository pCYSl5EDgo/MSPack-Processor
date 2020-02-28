// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;

        public StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor(ModuleDefinition module, TypeProvider provider)
        {
            this.module = module;
            this.provider = provider;
        }

        /// <summary>
        /// Implement Serialize/Deserialize &amp; Constructor.
        /// </summary>
        /// <param name="info">information.</param>
        /// <param name="formatter">formatter type. Must be empty.</param>
        public void Implement(in StructSerializationInfo info, TypeDefinition formatter)
        {
            formatter.Methods.Add(ConstructorUtility.GenerateDefaultConstructor(module, provider.SystemObjectHelper));
            var iMessagePackFormatterGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(info.Definition);
            formatter.Interfaces.Add(new InterfaceImplementation(iMessagePackFormatterGeneric));
            formatter.Interfaces.Add(new InterfaceImplementation(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterNoGeneric));

            var shouldCallback = CallbackTestUtility.ShouldCallback(info.Definition);

            var serialize = GenerateSerialize(in info, shouldCallback);
            serialize.Body.Optimize();
            formatter.Methods.Add(serialize);

            if (info.SerializationConstructor is null)
            {
                throw new InvalidProgramException("in this situation, SerializationConstructor should not be null.");
            }

            var deserialize = GenerateDeserialize(in info, shouldCallback, info.SerializationConstructor);
            deserialize.Body.Optimize();
            formatter.Methods.Add(deserialize);
        }

        #region Serialize
        private MethodDefinition GenerateSerialize(in StructSerializationInfo info, bool shouldCallback)
        {
            var valueParam = new ParameterDefinition("value", ParameterAttributes.None, provider.Importer.Import(info.Definition));
            var serialize = new MethodDefinition("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, module.TypeSystem.Void)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("writer", ParameterAttributes.None, new ByReferenceType(provider.MessagePackWriterHelper.Writer)),
                    valueParam,
                    new ParameterDefinition("options", ParameterAttributes.None, provider.MessagePackSerializerOptionsHelper.Options),
                },
            };

            var processor = serialize.Body.GetILProcessor();
            SerializeWriteArrayHeader(processor, info);

            Preprocess(info, processor, valueParam, shouldCallback);

            var intPtrVariable = default(VariableDefinition);
            for (var i = 0; i <= info.MaxIntKey; i++)
            {
                var (result, field, property) = info[i];
                SerializeEach(processor, valueParam, field, property, result, ref intPtrVariable);
            }

            processor.Append(Instruction.Create(OpCodes.Ret));

            return serialize;
        }

#if CSHARP_8_0_OR_NEWER
        private void SerializeEach(ILProcessor processor, ParameterDefinition valueParam, in FieldSerializationInfo field, in PropertySerializationInfo property, IndexerAccessResult result, ref VariableDefinition? intPtrVariable)
#else
        private void SerializeEach(ILProcessor processor, ParameterDefinition valueParam, in FieldSerializationInfo field, in PropertySerializationInfo property, IndexerAccessResult result, ref VariableDefinition intPtrVariable)
#endif
        {
            switch (result)
            {
                case IndexerAccessResult.None:
                    processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                    processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
                    break;
                case IndexerAccessResult.Field:
                    SerializeField(field, processor, valueParam, ref intPtrVariable);
                    break;
                case IndexerAccessResult.Property:
                    SerializeProperty(property, processor, valueParam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

#if CSHARP_8_0_OR_NEWER
        private void SerializeField(in FieldSerializationInfo serializationInfo, ILProcessor processor, ParameterDefinition valueParam, ref VariableDefinition? intPtrVariable)
#else
        private void SerializeField(in FieldSerializationInfo serializationInfo, ILProcessor processor, ParameterDefinition valueParam, ref VariableDefinition intPtrVariable)
#endif
        {
            if (serializationInfo.IsFixedSizeBuffer)
            {
                FixedSizeBufferUtility.SerializeFixedSizeBuffer(
                    processor,
                    valueParam,
                    serializationInfo.Definition,
                    module,
                    provider.MessagePackWriterHelper,
                    provider.Importer,
                    serializationInfo.ElementType,
                    serializationInfo.FixedSizeBufferCount,
                    ref intPtrVariable);
            }
            else
            {
                var fieldReference = provider.Importer.Import(serializationInfo.Definition);
                var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(fieldTypeReference)));
            }
        }

        private void SerializeProperty(in PropertySerializationInfo property, ILProcessor processor, ParameterDefinition valueParam)
        {
            var backingField = property.BackingFieldReference;
            if (!(backingField is null))
            {
                var fieldReference = provider.Importer.Import(backingField);
                var fieldTypeReference = provider.Importer.Import(fieldReference.FieldType);
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Ldfld, fieldReference));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(fieldTypeReference)));
            }
            else if (property.IsReadable)
            {
                var propertyReference = provider.Importer.Import(property.Definition.GetMethod);
                var propertyTypeReference = provider.Importer.Import(property.Definition.PropertyType);
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
                processor.Append(Instruction.Create(OpCodes.Call, propertyReference));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteMessagePackPrimitive(propertyTypeReference)));
            }
            else
            {
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteNil));
            }
        }

        private void SerializeWriteArrayHeader(ILProcessor processor, StructSerializationInfo info)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(InstructionUtility.LdcI4(info.MaxIntKey + 1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackWriterHelper.WriteArrayHeaderInt));
        }

        private void Preprocess(in StructSerializationInfo info, ILProcessor processor, ParameterDefinition valueParam, bool shouldCallback)
        {
            if (!shouldCallback || CallbackTestUtility.NoOperationInBeforeSerializationCallback(info.Definition, out var callback))
            {
                return;
            }

            processor.Append(Instruction.Create(OpCodes.Ldarga_S, valueParam));
            processor.Append(Instruction.Create(OpCodes.Constrained, valueParam.ParameterType));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
        }
        #endregion

        #region Deserialize
        private MethodDefinition GenerateDeserialize(in StructSerializationInfo info, bool shouldCallback, MethodDefinition infoSerializationConstructor)
        {
            var target = provider.Importer.Import(info.Definition);
            var deserialize = new MethodDefinition("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, target)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(provider.MessagePackReaderHelper.Reader)),
                    new ParameterDefinition("options", ParameterAttributes.None, provider.MessagePackSerializerOptionsHelper.Options),
                },
            };

            if (infoSerializationConstructor.HasParameters)
            {
                if (shouldCallback)
                {
                    ImplementParamCallback(deserialize, in info, infoSerializationConstructor, target);
                }
                else
                {
                    ImplementParamNoCallback(deserialize, infoSerializationConstructor);
                }
            }
            else
            {
                if (shouldCallback)
                {
                    ImplementNoParamCallback(deserialize, in info, infoSerializationConstructor, target);
                }
                else
                {
                    ImplementNoParamNoCallback(deserialize, infoSerializationConstructor);
                }
            }

            return deserialize;
        }

        private void ImplementParamNoCallback(MethodDefinition deserialize, MethodDefinition infoSerializationConstructor)
        {
            deserialize.Body.InitLocals = true;
            var variables = deserialize.Body.Variables;
            variables.Add(new VariableDefinition(module.TypeSystem.Int32));
            variables.Add(new VariableDefinition(module.TypeSystem.Int32));
            var argumentVariableStartIndex = variables.Count;
            foreach (var parameter in infoSerializationConstructor.Parameters)
            {
                variables.Add(new VariableDefinition(provider.Importer.Import(parameter.ParameterType)));
            }

            var processor = deserialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.TryReadNil));

            var notNil = Instruction.Create(OpCodes.Ldarg_2);

            processor.Append(Instruction.Create(OpCodes.Brfalse_S, notNil));

            Throw(processor);

            EnsureDepthStep(processor, notNil);
            ReadArrayHeader(processor);

            Assignment(processor, argumentVariableStartIndex);

            for (var i = 0; i < infoSerializationConstructor.Parameters.Count; i++)
            {
                processor.Append(InstructionUtility.LoadVariable(variables[argumentVariableStartIndex + i]));
            }

            processor.Append(Instruction.Create(OpCodes.Newobj, provider.Importer.Import(infoSerializationConstructor)));

            DecrementDepth(processor);

            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private void ImplementParamCallback(MethodDefinition deserialize, in StructSerializationInfo info, MethodDefinition infoSerializationConstructor, TypeReference target)
        {
            var variables = deserialize.Body.Variables;
            deserialize.Body.InitLocals = true;
            variables.Add(new VariableDefinition(module.TypeSystem.Int32));
            variables.Add(new VariableDefinition(module.TypeSystem.Int32));
            var targetVariable = new VariableDefinition(target);
            variables.Add(targetVariable);

            var argumentVariableStartIndex = variables.Count;
            foreach (var parameter in infoSerializationConstructor.Parameters)
            {
                variables.Add(new VariableDefinition(provider.Importer.Import(parameter.ParameterType)));
            }

            var processor = deserialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.TryReadNil));

            var notNil = Instruction.Create(OpCodes.Ldarg_2);

            processor.Append(Instruction.Create(OpCodes.Brfalse_S, notNil));

            Throw(processor);

            EnsureDepthStep(processor, notNil);
            ReadArrayHeader(processor);

            Assignment(processor, argumentVariableStartIndex);

            processor.Append(Instruction.Create(OpCodes.Ldloca_S, targetVariable));

            for (var i = 0; i < infoSerializationConstructor.Parameters.Count; i++)
            {
                processor.Append(InstructionUtility.LoadVariable(variables[argumentVariableStartIndex + i]));
            }

            processor.Append(Instruction.Create(OpCodes.Call, provider.Importer.Import(infoSerializationConstructor)));
            processor.Append(InstructionUtility.StoreVariable(targetVariable));

            CallbackAfterDeserialization(info, processor, targetVariable);
            DecrementDepth(processor);

            processor.Append(InstructionUtility.LoadVariable(targetVariable));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private void Throw(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldstr, "typecode is null, struct not supported"));
            processor.Append(Instruction.Create(OpCodes.Newobj, provider.SystemInvalidOperationExceptionHelper.Ctor));
            processor.Append(Instruction.Create(OpCodes.Throw));
        }

        private void ImplementNoParamCallback(MethodDefinition deserialize, in StructSerializationInfo info, MethodDefinition infoSerializationConstructor, TypeReference target)
        {
            deserialize.Body.InitLocals = true;
            var variables = deserialize.Body.Variables;
            var targetVariable = new VariableDefinition(target);
            variables.Add(targetVariable);

            var processor = deserialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.TryReadNil));

            var notNil = Instruction.Create(OpCodes.Ldarg_2);

            processor.Append(Instruction.Create(OpCodes.Brfalse_S, notNil));

            Throw(processor);

            EnsureDepthStep(processor, notNil);

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip));

            processor.Append(Instruction.Create(OpCodes.Ldloca_S, targetVariable));
            processor.Append(Instruction.Create(OpCodes.Call, provider.Importer.Import(infoSerializationConstructor)));

            if (!CallbackTestUtility.NoOperationInAfterDeserializationCallback(info.Definition, out var callback))
            {
                processor.Append(Instruction.Create(OpCodes.Ldloca_S, targetVariable));
                processor.Append(Instruction.Create(OpCodes.Constrained, targetVariable.VariableType));
                processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
            }

            DecrementDepth(processor);

            processor.Append(InstructionUtility.LoadVariable(targetVariable));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private void ImplementNoParamNoCallback(MethodDefinition deserialize, MethodDefinition infoSerializationConstructor)
        {
            var processor = deserialize.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.TryReadNil));

            var notNil = Instruction.Create(OpCodes.Ldarg_2);

            processor.Append(Instruction.Create(OpCodes.Brfalse_S, notNil));

            Throw(processor);

            EnsureDepthStep(processor, notNil);

            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip));

            DecrementDepth(processor);

            processor.Append(Instruction.Create(OpCodes.Newobj, provider.Importer.Import(infoSerializationConstructor)));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private void Assignment(ILProcessor processor, int argumentVariableStartIndex)
        {
            var continuousCondition = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, continuousCondition));
            var loopStart = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(loopStart);
            var variables = processor.Body.Variables;
            var (switchInstructions, defaultInstructions, switchTable) = GenerateSwitchStatements(variables, argumentVariableStartIndex);
            processor.Append(Instruction.Create(OpCodes.Switch, switchTable));

            foreach (var instruction in defaultInstructions)
            {
                processor.Append(instruction);
            }

            var nextInstruction = Instruction.Create(OpCodes.Ldloc_1);
            processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));

            foreach (var instructions in switchInstructions)
            {
                foreach (var instruction in instructions)
                {
                    processor.Append(instruction);
                }

                processor.Append(Instruction.Create(OpCodes.Br, nextInstruction));
            }

            processor.Append(nextInstruction);
            processor.Append(InstructionUtility.LdcI4(1));
            processor.Append(Instruction.Create(OpCodes.Add));
            processor.Append(Instruction.Create(OpCodes.Stloc_1));
            processor.Append(continuousCondition);
            processor.Append(Instruction.Create(OpCodes.Ldloc_0));
            processor.Append(Instruction.Create(OpCodes.Blt, loopStart));
        }

        private (Instruction[][] switchInstructions, Instruction[] defaultInstructions, Instruction[] switchTable) GenerateSwitchStatements(Collection<VariableDefinition> variables, int argumentVariableStartIndex)
        {
            var answers = new Instruction[variables.Count - argumentVariableStartIndex][];
            var table = new Instruction[answers.Length];
            for (var i = 0; i < answers.Length; i++)
            {
                var variable = variables[i + argumentVariableStartIndex];
                answers[i] = new[]
                {
                    Instruction.Create(OpCodes.Ldarg_1),
                    Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadMessagePackPrimitive(variable.VariableType)),
                    InstructionUtility.StoreVariable(variable),
                };
                table[i] = answers[i][0];
            }

            var @default = new[]
            {
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.Skip),
            };
            return (answers, @default, table);
        }

        private void ReadArrayHeader(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.ReadArrayHeader));
            processor.Append(Instruction.Create(OpCodes.Stloc_0));
        }

        private void EnsureDepthStep(ILProcessor processor, Instruction notNil)
        {
            processor.Append(notNil);
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSerializerOptionsHelper.get_Security));
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Callvirt, provider.MessagePackSecurityHelper.DepthStep));
        }

        private void CallbackAfterDeserialization(in StructSerializationInfo info, ILProcessor processor, VariableDefinition targetVariable)
        {
            if (!CallbackTestUtility.NoOperationInAfterDeserializationCallback(info.Definition, out var callback))
            {
                processor.Append(Instruction.Create(OpCodes.Ldloca_S, targetVariable));
                processor.Append(Instruction.Create(OpCodes.Constrained, targetVariable.VariableType));
                processor.Append(Instruction.Create(OpCodes.Callvirt, provider.Importer.Import(callback)));
            }
        }

        private void DecrementDepth(ILProcessor processor)
        {
            processor.Append(Instruction.Create(OpCodes.Ldarg_1));
            processor.Append(Instruction.Create(OpCodes.Dup));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.get_Depth));
            processor.Append(InstructionUtility.LdcI4(1));
            processor.Append(Instruction.Create(OpCodes.Sub));
            processor.Append(Instruction.Create(OpCodes.Call, provider.MessagePackReaderHelper.set_Depth));
        }
        #endregion
    }
}
