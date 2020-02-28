// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct ClassSerializationInfo : ITypeSerializationInfo
    {
#if CSHARP_8_0_OR_NEWER
        public ClassSerializationInfo(TypeDefinition definition, string formatterName, FieldSerializationInfo[] fieldInfos, PropertySerializationInfo[] propertyInfos, int minIntKey, int maxIntKey, MethodDefinition? serializationConstructorDefinition)
#else
        public ClassSerializationInfo(TypeDefinition definition, string formatterName, FieldSerializationInfo[] fieldInfos, PropertySerializationInfo[] propertyInfos, int minIntKey, int maxIntKey, MethodDefinition serializationConstructorDefinition)
#endif
        {
            Definition = definition;
            FormatterName = formatterName;
            FormatterDefinition = default;
            FormatterConstructorArguments = Array.Empty<CustomAttributeArgument>();
            FieldInfos = fieldInfos;
            PropertyInfos = propertyInfos;
            MinIntKey = minIntKey;
            MaxIntKey = maxIntKey;
            SerializationConstructor = serializationConstructorDefinition;

            AreAllMessagePackPrimitive = this.FieldInfos.All(x => x.IsMessagePackPrimitive) && this.PropertyInfos.All(x => x.IsMessagePackPrimitive);
            PublicAccessible = PublicTypeTestUtility.IsPublicType(definition) && this.FieldInfos.All(x => x.PublicAccessible) && this.PropertyInfos.All(x => x.PublicAccessible);
        }
#if CSHARP_8_0_OR_NEWER
        public ClassSerializationInfo(TypeDefinition definition, TypeDefinition formatterDefinition, CustomAttributeArgument[] constructorArguments, MethodDefinition? serializationConstructorDefinition)
#else
        public ClassSerializationInfo(TypeDefinition definition, TypeDefinition formatterDefinition, CustomAttributeArgument[] constructorArguments, MethodDefinition serializationConstructorDefinition)
#endif
        {
            Definition = definition;
            FormatterName = string.Empty;
            FormatterDefinition = formatterDefinition;
            FormatterConstructorArguments = constructorArguments;
            FieldInfos = Array.Empty<FieldSerializationInfo>();
            PropertyInfos = Array.Empty<PropertySerializationInfo>();
            MinIntKey = 0;
            MaxIntKey = 0;
            SerializationConstructor = serializationConstructorDefinition;

            AreAllMessagePackPrimitive = FieldInfos.All(x => x.IsMessagePackPrimitive) && PropertyInfos.All(x => x.IsMessagePackPrimitive);
            PublicAccessible = PublicTypeTestUtility.IsPublicType(definition) && FieldInfos.All(x => x.PublicAccessible) && PropertyInfos.All(x => x.PublicAccessible);
        }

#if CSHARP_8_0_OR_NEWER
        public MethodDefinition? SerializationConstructor { get; }
#else
        public MethodDefinition SerializationConstructor { get; }
#endif

        public bool IsIntKey => MaxIntKey != -1;

        public bool IsStringKey => MaxIntKey == -1;

        public bool IsClass => true;

        public bool IsStruct => false;

        public bool FormatterExists => !ReferenceEquals(FormatterDefinition, null);

        public int KeyCount => FieldInfos.Length + PropertyInfos.Length;

        public TypeDefinition Definition { get; }

        public string FormatterName { get; }

        public int MinIntKey { get; }

        public int MaxIntKey { get; }

#if CSHARP_8_0_OR_NEWER
        public TypeDefinition? FormatterDefinition { get; }
#else
        public TypeDefinition FormatterDefinition { get; }

#endif

        public CustomAttributeArgument[] FormatterConstructorArguments { get; }

        public FieldSerializationInfo[] FieldInfos { get; }

        public PropertySerializationInfo[] PropertyInfos { get; }

        public bool AreAllMessagePackPrimitive { get; }

        public bool PublicAccessible { get; }

        public FieldOrPropertyInfo this[int key]
        {
            get
            {
                if (IsStringKey)
                {
                    throw new NotSupportedException(this.Definition.FullName + " does not support int key.");
                }

                foreach (var fieldSerializationInfo in FieldInfos)
                {
                    if (fieldSerializationInfo.IntKey == key)
                    {
                        return new FieldOrPropertyInfo(fieldSerializationInfo);
                    }
                }

                foreach (var propertySerializationInfo in PropertyInfos)
                {
                    if (propertySerializationInfo.IntKey == key)
                    {
                        return new FieldOrPropertyInfo(propertySerializationInfo);
                    }
                }

                return default;
            }
        }

        public IEnumerable<(string key, FieldOrPropertyInfo value)> EnumerateStringKeyValuePairs()
            => FieldInfos.Select(x => (x.StringKey, new FieldOrPropertyInfo(x)))
                .Concat(PropertyInfos.Select(x => (x.StringKey, new FieldOrPropertyInfo(x))));

        public static bool TryParse(TypeDefinition type, bool useMapMode, out ClassSerializationInfo info)
        {
            var messagePackAttribute = type.CustomAttributes.SingleOrDefault(CustomAttributeHelper.IsMessagePackObjectAttribute);

            if (messagePackAttribute is null)
            {
                info = default;
                return false;
            }

            var serializationConstructor = SerializationConstructorUtility.Find(type);

            var customFormatter = type.CustomAttributes.SingleOrDefault(CustomAttributeHelper.IsMessagePackFormatterAttribute);
            if (customFormatter is null)
            {
                CustomAttributeHelper.IsMessagePackObjectAttribute(messagePackAttribute, out var isKeyAsPropertyName);
                var fieldInfos = MessagePackObjectHelper.CollectFieldInfos(type, useMapMode);
                var propertyInfos = MessagePackObjectHelper.CollectPropertyInfos(type, isKeyAsPropertyName | useMapMode);
                var (minIntKey, maxIntKey) = MessagePackObjectHelper.FindMinMaxIntKey(fieldInfos, propertyInfos);

                info = new ClassSerializationInfo(type, MessagePackObjectHelper.FindFormatterName(type), fieldInfos, propertyInfos, minIntKey, maxIntKey, serializationConstructor);
            }
            else
            {
                var formatterType = ((TypeReference)customFormatter.ConstructorArguments[0].Value).Resolve();
                if (customFormatter.ConstructorArguments.Count == 2 && customFormatter.ConstructorArguments[1].Value is CustomAttributeArgument[] argumentArray)
                {
                    info = new ClassSerializationInfo(type, formatterType, argumentArray, serializationConstructor);
                }
                else
                {
                    info = new ClassSerializationInfo(type, formatterType, Array.Empty<CustomAttributeArgument>(), serializationConstructor);
                }
            }

            info.Validate();
            return true;
        }

        [Conditional("DEBUG")]
        public void Validate()
        {
            if (FieldInfos.All(x => !x.IsWritable) &&
                PropertyInfos.All(x => !x.IsWritable) &&
                !Definition.Methods.Any(x => x.IsPublic && x.Name == ".ctor" && x.Parameters.Count == 0))
            {
                throw new MessagePackGeneratorResolveFailedException("All key fields and properties are readonly and type does not have zero param constructor. type : " + Definition.FullName);
            }
        }
    }
}
