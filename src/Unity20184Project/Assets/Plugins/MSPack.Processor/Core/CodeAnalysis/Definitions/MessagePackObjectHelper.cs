// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MSPack.Processor.Core.Definitions
{
    public static class MessagePackObjectHelper
    {
        public const string DefaultFormatterName = "Formatter";

        public static string FindFormatterName(TypeDefinition type)
        {
            if (!type.HasNestedTypes)
            {
                return DefaultFormatterName;
            }

            var all = true;
            foreach (var nestedTypeDefinition in type.NestedTypes)
            {
                if (string.Equals(nestedTypeDefinition.Name, DefaultFormatterName, StringComparison.Ordinal))
                {
                    all = false;
                    break;
                }
            }

            if (all)
            {
                return DefaultFormatterName;
            }

            for (var i = 0; ; i++)
            {
                var name = DefaultFormatterName + "<" + i.ToString(CultureInfo.InvariantCulture) + ">";
                var allNameDifferent = true;
                foreach (var x in type.NestedTypes)
                {
                    if (!string.Equals(x.Name, name, StringComparison.Ordinal))
                    {
                        allNameDifferent = false;
                        break;
                    }
                }

                if (allNameDifferent)
                {
                    return string.Intern(name);
                }
            }
        }

        private static void CollectFieldInfosRecursiveBaseType(TypeReference type, bool useMapMode, List<FieldSerializationInfo> list)
        {
            while (true)
            {
                if (type.FullName == "System.Object")
                {
                    return;
                }

                if (!(type is TypeDefinition definition))
                {
                    try
                    {
                        definition = type.Resolve();
                    }
                    catch
                    {
                        throw new MessagePackGeneratorResolveFailedException("type : " + type.FullName + " is not defined.");
                    }
                }

                if (!definition.CustomAttributes.Any(CustomAttributeHelper.IsMessagePackObjectAttribute))
                {
                    return;
                }

                list.AddRange(CollectFieldInfosInSpecificType(definition, useMapMode));

                var baseType = definition.BaseType;
                if (!(baseType is null))
                {
                    type = baseType;
                    continue;
                }

                break;
            }
        }

        public static FieldSerializationInfo[] CollectFieldInfos(TypeDefinition type, bool useMapMode)
        {
            var list = new List<FieldSerializationInfo>(CollectFieldInfosInSpecificType(type, useMapMode));
            if (!(type.BaseType is null))
            {
                CollectFieldInfosRecursiveBaseType(type.BaseType, useMapMode, list);
            }

            return list.ToArray();
        }

        private static FieldSerializationInfo[] CollectFieldInfosInSpecificType(TypeDefinition type, bool useMapMode)
        {
            if (!type.HasFields)
            {
                return Array.Empty<FieldSerializationInfo>();
            }

            var sourceCount = type.Fields.Count;
            var destination = new List<FieldSerializationInfo>(sourceCount);

            for (var sourceIndex = 0; sourceIndex < sourceCount; sourceIndex++)
            {
                if (FieldSerializationInfo.TryParse(type.Fields[sourceIndex], (uint)sourceIndex, useMapMode, out var info))
                {
                    destination.Add(info);
                }
            }

            destination.Sort();

            return destination.Count == 0 ? Array.Empty<FieldSerializationInfo>() : destination.ToArray();
        }

        public static PropertySerializationInfo[] CollectPropertyInfos(TypeDefinition type, bool useMapMode)
        {
            var list = new List<PropertySerializationInfo>(CollectPropertyInfosInSpecificType(type, useMapMode));
            if (!(type.BaseType is null))
            {
                CollectPropertyInfosRecursiveBaseType(type.BaseType, useMapMode, list);
            }

            return list.ToArray();
        }

        private static void CollectPropertyInfosRecursiveBaseType(TypeReference type, bool useMapMode, List<PropertySerializationInfo> list)
        {
            while (true)
            {
                if (type.FullName == "System.Object")
                {
                    return;
                }

                if (!(type is TypeDefinition definition))
                {
                    try
                    {
                        definition = type.Resolve();
                    }
                    catch
                    {
                        throw new MessagePackGeneratorResolveFailedException("type : " + type.FullName + " is not defined.");
                    }
                }

                if (!definition.CustomAttributes.Any(CustomAttributeHelper.IsMessagePackObjectAttribute))
                {
                    return;
                }

                list.AddRange(CollectPropertyInfosInSpecificType(definition, useMapMode));

                var baseType = definition.BaseType;
                if (!(baseType is null))
                {
                    type = baseType;
                    continue;
                }

                break;
            }
        }

        private static PropertySerializationInfo[] CollectPropertyInfosInSpecificType(TypeDefinition type, bool useMapMode)
        {
            if (!type.HasProperties)
            {
                return Array.Empty<PropertySerializationInfo>();
            }

            var source = type.Properties;
            var sourceCount = source.Count;
            var destination = new List<PropertySerializationInfo>(sourceCount);

            for (var sourceIndex = 0; sourceIndex < sourceCount; sourceIndex++)
            {
                if (PropertySerializationInfo.TryParse(source[sourceIndex], (uint)sourceIndex, useMapMode, out var info))
                {
                    destination.Add(info);
                }
            }

            destination.Sort();

            return destination.Count == 0 ? Array.Empty<PropertySerializationInfo>() : destination.ToArray();
        }

        public static (int min, int max) FindMinMaxIntKey(FieldSerializationInfo[] fieldInfos, PropertySerializationInfo[] propertyInfos)
        {
            if (fieldInfos.Length == 0)
            {
                return propertyInfos.Length == 0 ? default : FindMinMaxIntKeyFromProperties(propertyInfos);
            }

            return propertyInfos.Length == 0 ? FindMinMaxIntKeyFromFields(fieldInfos) : FindMinMaxIntKeyBoth(fieldInfos, propertyInfos);
        }

        private static (int min, int max) FindMinMaxIntKeyBoth(FieldSerializationInfo[] fieldInfos, PropertySerializationInfo[] propertyInfos)
        {
            (int min, int max) answer;
            var fieldIndex = 0;
            var propertyIndex = 0;

            var field = fieldInfos[0].IntKey;
            var property = propertyInfos[0].IntKey;

            if (field == -1 || property == -1)
            {
                if (field != property)
                {
                    throw new MessagePackGeneratorResolveFailedException("all key type must be same. property : " + propertyInfos[0].Definition.FullName);
                }

                for (var index = 1; index < fieldInfos.Length; index++)
                {
                    if (fieldInfos[index].IntKey != -1)
                    {
                        throw new MessagePackGeneratorResolveFailedException("all key type must be same. property : " + propertyInfos[0].Definition.FullName);
                    }
                }

                for (var index = 1; index < propertyInfos.Length; index++)
                {
                    if (propertyInfos[index].IntKey != -1)
                    {
                        throw new MessagePackGeneratorResolveFailedException("all key type must be same. property : " + propertyInfos[0].Definition.FullName);
                    }
                }

                return (-1, -1);
            }

            if (field < property)
            {
                answer.min = answer.max = fieldInfos[fieldIndex++].IntKey;
            }
            else if (field > property)
            {
                answer.min = answer.max = propertyInfos[propertyIndex++].IntKey;
            }
            else
            {
                throw new MessagePackGeneratorResolveFailedException("all int key should be different from each other. property : " + propertyInfos[0].Definition.FullName);
            }

            while (fieldIndex < fieldInfos.Length && propertyIndex < propertyInfos.Length)
            {
                field = fieldInfos[fieldIndex].IntKey;
                property = propertyInfos[propertyIndex].IntKey;

                if (answer.max == field || answer.min == field || answer.max == property || answer.min == property || field == property)
                {
                    throw new MessagePackGeneratorResolveFailedException("all int key should be different from each other. property : " + propertyInfos[fieldIndex].Definition.FullName);
                }

                if (field < property)
                {
                    if (answer.max < field)
                    {
                        answer.max = field;
                    }
                    else if (answer.min > field)
                    {
                        answer.min = field;
                    }

                    fieldIndex++;
                }
                else if (field > property)
                {
                    if (answer.max < property)
                    {
                        answer.max = property;
                    }
                    else if (answer.min > property)
                    {
                        answer.min = property;
                    }

                    propertyIndex++;
                }
            }

            while (fieldIndex < fieldInfos.Length)
            {
                field = fieldInfos[fieldIndex].IntKey;
                if (answer.max == field || answer.min == field)
                {
                    throw new MessagePackGeneratorResolveFailedException("all int key should be different from each other. property : " + propertyInfos[fieldIndex].Definition.FullName);
                }

                if (answer.max < field)
                {
                    answer.max = field;
                }
                else if (answer.min > field)
                {
                    answer.min = field;
                }

                fieldIndex++;
            }

            while (propertyIndex < propertyInfos.Length)
            {
                property = propertyInfos[propertyIndex].IntKey;

                if (answer.max == property || answer.min == property)
                {
                    throw new MessagePackGeneratorResolveFailedException("all int key should be different from each other. property : " + propertyInfos[fieldIndex].Definition.FullName);
                }

                if (answer.max < property)
                {
                    answer.max = property;
                }
                else if (answer.min > property)
                {
                    answer.min = property;
                }

                propertyIndex++;
            }

            return answer;
        }

        private static (int min, int max) FindMinMaxIntKeyFromFields(FieldSerializationInfo[] infos)
        {
            int maxIntKey;
            var minIntKey = maxIntKey = infos[0].IntKey;

            if (maxIntKey == -1)
            {
                return (-1, -1);
            }

            for (var index = 1; index < infos.Length; index++)
            {
                var info = infos[index];
                if (!info.IsIntKey)
                {
                    throw new MessagePackGeneratorResolveFailedException("all members key type must be same. field : " + info.Definition.FullName);
                }

                if (info.IntKey > maxIntKey)
                {
                    maxIntKey = info.IntKey;
                }
                else if (info.IntKey < minIntKey)
                {
                    minIntKey = info.IntKey;
                }
                else
                {
                    throw new MessagePackGeneratorResolveFailedException("all members int key must be different. field : " + info.Definition.FullName);
                }
            }

            return (minIntKey, maxIntKey);
        }

        private static (int min, int max) FindMinMaxIntKeyFromProperties(PropertySerializationInfo[] infos)
        {
            int maxIntKey;
            var minIntKey = maxIntKey = infos[0].IntKey;

            if (maxIntKey == -1)
            {
                return (-1, -1);
            }

            for (var index = 1; index < infos.Length; index++)
            {
                var info = infos[index];
                if (!info.IsIntKey)
                {
                    throw new MessagePackGeneratorResolveFailedException("all members key type must be same. property : " + info.Definition.FullName);
                }

                if (info.IntKey > maxIntKey)
                {
                    maxIntKey = info.IntKey;
                }
                else if (info.IntKey < minIntKey)
                {
                    minIntKey = info.IntKey;
                }
                else
                {
                    throw new MessagePackGeneratorResolveFailedException("all members int key must be different. property : " + info.Definition.FullName);
                }
            }

            return (minIntKey, maxIntKey);
        }
    }
}
