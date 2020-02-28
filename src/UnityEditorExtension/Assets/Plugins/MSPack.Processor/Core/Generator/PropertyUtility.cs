// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace MSPack.Processor.Core
{
    public static class PropertyUtility
    {
        public static PropertyDefinition GenerateAutoProperty(string name, TypeReference propertyTypeReference, FieldReference backingField, bool implementGet, bool implementSet, MethodAttributes getAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, MethodAttributes setAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName)
        {
            var property = new PropertyDefinition(name, PropertyAttributes.None, propertyTypeReference);
            var module = propertyTypeReference.Module;
            if (implementGet)
            {
                var method = new MethodDefinition("get_" + name, getAttributes, propertyTypeReference)
                {
                    HasThis = true,
                };

                var processor = method.Body.GetILProcessor();
                processor.Append(Instruction.Create(OpCodes.Ldarg_0));
                processor.Append(Instruction.Create(OpCodes.Ldfld, backingField));
                processor.Append(Instruction.Create(OpCodes.Ret));

                property.GetMethod = method;
            }

            // ReSharper disable once InvertIf
            if (implementSet)
            {
                var method = new MethodDefinition("set_" + name, setAttributes, module.TypeSystem.Void)
                {
                    HasThis = true,
                    Parameters =
                    {
                        new ParameterDefinition("value", ParameterAttributes.None, propertyTypeReference),
                    },
                };

                var processor = method.Body.GetILProcessor();
                processor.Append(Instruction.Create(OpCodes.Ldarg_0));
                processor.Append(Instruction.Create(OpCodes.Ldarg_1));
                processor.Append(Instruction.Create(OpCodes.Stfld, backingField));
                processor.Append(Instruction.Create(OpCodes.Ret));

                property.SetMethod = method;
            }

            return property;
        }

        public static void RegisterProperty(TypeDefinition typeDefinition, PropertyDefinition propertyDefinition)
        {
            if (!(propertyDefinition.GetMethod is null))
            {
                typeDefinition.Methods.Add(propertyDefinition.GetMethod);
            }

            if (!(propertyDefinition.SetMethod is null))
            {
                typeDefinition.Methods.Add(propertyDefinition.SetMethod);
            }

            typeDefinition.Properties.Add(propertyDefinition);
        }
    }
}
