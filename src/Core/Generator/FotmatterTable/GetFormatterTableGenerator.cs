// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;
using FieldAttributes = Mono.Cecil.FieldAttributes;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using ParameterAttributes = Mono.Cecil.ParameterAttributes;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace MSPack.Processor.Core
{
    public sealed class GetFormatterTableGenerator : IFormatterTableGenerator
    {
        private const string DefaultTableName = "ResolverGetFormatterHelper";
        private readonly TypeProvider provider;
        private readonly string tableName;

        public GetFormatterTableGenerator(TypeProvider provider)
        {
            string FindTableName()
            {
                if (provider.Module.GetType(string.Empty, DefaultTableName) is null)
                {
                    return DefaultTableName;
                }

                for (var i = 0; ; i++)
                {
                    var name = DefaultTableName + "<>" + i.ToString(CultureInfo.InvariantCulture);
                    if (provider.Module.GetType(string.Empty, name) is null)
                    {
                        return string.Intern(name);
                    }
                }
            }

            this.provider = provider;

            tableName = FindTableName();
        }

        /// <summary>
        /// Generate Helper Static Class and returns method IMessagePackFormatter GetFormatter(RuntimeTypeHandle t).
        /// </summary>
        /// <param name="infos">Formatter &amp; constructor information struct array.</param>
        /// <returns>GetFormatter Method Definition.</returns>
        public (TypeDefinition tableType, MethodDefinition getFormatter) Generate(FormatterInfo[] infos)
        {
            var table = new TypeDefinition(
                string.Empty,
                tableName,
                TypeAttributes.NestedAssembly | TypeAttributes.AutoLayout | TypeAttributes.AnsiClass | TypeAttributes.Abstract | TypeAttributes.Sealed,
                provider.Module.TypeSystem.Object);

            var lookupField = GenerateLookup();
            table.Fields.Add(lookupField);

            var cctor = GenerateCctor(lookupField, infos);
            table.Methods.Add(cctor);

            var getFormatter = GenerateGetFormatter(lookupField, infos);
            table.Methods.Add(getFormatter);
            return (table, getFormatter);
        }

        private FieldDefinition GenerateLookup()
        {
            var dictionary = provider.SystemCollectionsGenericDictionaryHelper.DictionaryGeneric(provider.SystemTypeHelper.Type, provider.Module.TypeSystem.Int32);
            var lookupField = new FieldDefinition("lookup", FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly, dictionary);
            return lookupField;
        }

        private MethodDefinition GenerateCctor(FieldReference lookupField, FormatterInfo[] formatters)
        {
            var cctor = new MethodDefinition(".cctor", MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName | MethodAttributes.Static, provider.Module.TypeSystem.Void)
            {
                HasThis = false,
            };

            var dictionaryType = (GenericInstanceType)lookupField.FieldType;

            var processor = cctor.Body.GetILProcessor();
            processor.Append(InstructionUtility.LdcI4(formatters.Length));
            var lookupCtor = provider.SystemCollectionsGenericDictionaryHelper.Ctor_CapacityGeneric(dictionaryType);
            processor.Append(Instruction.Create(OpCodes.Newobj, lookupCtor));

            var add = provider.SystemCollectionsGenericDictionaryHelper.Add(dictionaryType);

            for (var i = 0; i < formatters.Length; i++)
            {
                ref var formatter = ref formatters[i];
                processor.Append(Instruction.Create(OpCodes.Dup));
                processor.Append(Instruction.Create(OpCodes.Ldtoken, formatter.SerializeTypeDefinition));
                processor.Append(Instruction.Create(OpCodes.Call, provider.SystemTypeHelper.GetTypeFromHandle));
                processor.Append(InstructionUtility.LdcI4(i));
                processor.Append(Instruction.Create(OpCodes.Call, add));
            }

            processor.Append(Instruction.Create(OpCodes.Stsfld, lookupField));
            processor.Append(Instruction.Create(OpCodes.Ret));

            return cctor;
        }

        private MethodDefinition GenerateGetFormatter(FieldDefinition lookupField, FormatterInfo[] formatters)
        {
            var indexVariable = new VariableDefinition(provider.Module.TypeSystem.Int32);
            var getFormatter = new MethodDefinition("GetFormatter", MethodAttributes.Assembly | MethodAttributes.HideBySig | MethodAttributes.Static, provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterNoGeneric)
            {
                HasThis = false,
                Parameters =
                {
                    new ParameterDefinition("t", ParameterAttributes.None, provider.SystemTypeHelper.Type),
                },
                Body =
                {
                    InitLocals = false,
                    Variables = { indexVariable },
                },
            };

            var processor = getFormatter.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldsfld, lookupField));
            processor.Append(Instruction.Create(OpCodes.Ldarg_0));
            processor.Append(Instruction.Create(OpCodes.Ldloca_S, indexVariable));

            var dictionary = (GenericInstanceType)lookupField.FieldType;
            var tryGetValue = provider.SystemCollectionsGenericDictionaryHelper.TryGetValue(dictionary);

            processor.Append(Instruction.Create(OpCodes.Call, tryGetValue));

            var success = Instruction.Create(OpCodes.Ldloc_0);
            processor.Append(Instruction.Create(OpCodes.Brtrue_S, success));

            var fail = Instruction.Create(OpCodes.Ldnull);
            processor.Append(Instruction.Create(OpCodes.Br_S, fail));

            processor.Append(success);
            var switchDestinations = GenerateSwitchDestinations(formatters);
            processor.Append(Instruction.Create(OpCodes.Switch, switchDestinations));

            processor.Append(fail);
            processor.Append(Instruction.Create(OpCodes.Ret));

            foreach (var destinationInstruction in switchDestinations)
            {
                processor.Append(destinationInstruction);
                processor.Append(Instruction.Create(OpCodes.Ret));
            }

            return getFormatter;
        }

        private Instruction[] GenerateSwitchDestinations(FormatterInfo[] formatters)
        {
            var switchDestinations = new Instruction[formatters.Length];
            for (var i = 0; i < formatters.Length; i++)
            {
                var ctor = new MethodReference(".ctor", provider.Module.TypeSystem.Void, formatters[i].FormatterTypeDefinition)
                {
                    HasThis = true,
                };
                switchDestinations[i] = Instruction.Create(OpCodes.Newobj, ctor);
            }

            return switchDestinations;
        }
    }
}
