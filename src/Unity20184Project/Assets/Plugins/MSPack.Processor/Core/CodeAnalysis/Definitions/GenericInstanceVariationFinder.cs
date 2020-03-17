// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Definitions
{
    public class GenericInstanceVariationFinder
    {
        public IList<GenericInstanceType> Find(TypeDefinition definition)
        {
            if (!definition.HasCustomAttributes)
            {
                return Array.Empty<GenericInstanceType>();
            }

            var list = new List<GenericInstanceType>();
            foreach (var attribute in definition.CustomAttributes)
            {
                if (attribute.AttributeType.Name != "MessagePackObjectGenericVariationAttribute")
                {
                    continue;
                }

                if (!(attribute.ConstructorArguments[0].Value is GenericInstanceType variation))
                {
                    continue;
                }

                if (variation.ElementType.FullName != definition.FullName)
                {
                    throw new MessagePackGeneratorResolveFailedException("Generic instance variation must be ");
                }

                RefillValueTypeInfo(attribute, variation);

                list.Add(variation);
            }

            return list;
        }

        private static void RefillValueTypeInfo<TCustomAttribute, TVariation>(TCustomAttribute attribute, TVariation variation)
            where TCustomAttribute : ICustomAttribute
            where TVariation : IGenericInstance
        {
            foreach (var property in attribute.Properties)
            {
                if (property.Name != "NotPrimitiveValueTypeIndices")
                {
                    continue;
                }

                MakeValueType(variation, property);
            }

            foreach (var argument in variation.GenericArguments)
            {
                MakeValueType(argument);
            }
        }

        private static void MakeValueType<T>(T variation, CustomAttributeNamedArgument property)
            where T : IGenericInstance
        {
            var valueTypeIndices = (CustomAttributeArgument[])property.Argument.Value;
            foreach (var valueTypeIndex in valueTypeIndices)
            {
                var argument = variation.GenericArguments[(int)(uint)valueTypeIndex.Value];
                argument.IsValueType = true;
            }
        }

        private static void MakeValueType(TypeReference argument)
        {
            while (true)
            {
                if (argument is ArrayType arrayType)
                {
                    argument = arrayType.ElementType;
                    continue;
                }

                if (argument is PinnedType pinnedType)
                {
                    argument = pinnedType.ElementType;
                    continue;
                }

                if (argument is ByReferenceType byReferenceType)
                {
                    argument = byReferenceType.ElementType;
                    continue;
                }

                if (argument is OptionalModifierType optionalModifierType)
                {
                    MakeValueType(optionalModifierType.ElementType);
                    argument = optionalModifierType.ModifierType;
                    continue;
                }

                if (argument is GenericInstanceType genericInstanceType)
                {
                    foreach (var genericArgument in genericInstanceType.GenericArguments)
                    {
                        MakeValueType(genericArgument);
                    }

                    MakeValueType(genericInstanceType.ElementType);
                    if (genericInstanceType.ElementType.IsValueType)
                    {
                        argument.IsValueType = true;
                    }
                }
                else if (argument is PointerType pointerType)
                {
                    argument.IsValueType = true;
                    var elementType = pointerType.ElementType;
                    if (!elementType.IsValueType)
                    {
                        elementType.IsValueType = true;
                    }
                }
                else if (!argument.IsValueType && !argument.IsGenericParameter)
                {
                    try
                    {
                        var resolved = argument.Resolve();
                        if (resolved.IsValueType)
                        {
                            argument.IsValueType = true;
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
                break;
            }
        }
    }
}
