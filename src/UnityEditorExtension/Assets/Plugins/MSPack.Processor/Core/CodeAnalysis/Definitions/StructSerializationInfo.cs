﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Linq;
using System.Text;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct StructSerializationInfo : ITypeSerializationInfo
    {
#if CSHARP_8_0_OR_NEWER
        public StructSerializationInfo(TypeDefinition definition, FieldSerializationInfo[] fieldInfos, PropertySerializationInfo[] propertyInfos, int minIntKey, int maxIntKey, MethodDefinition? serializationConstructorDefinition)
#else
        public StructSerializationInfo(TypeDefinition definition, FieldSerializationInfo[] fieldInfos, PropertySerializationInfo[] propertyInfos, int minIntKey, int maxIntKey, MethodDefinition serializationConstructorDefinition)
#endif
        {
            Definition = definition;
            CustomFormatter = CustomFormatterTypeInfo.Default;
            FieldInfos = fieldInfos;
            PropertyInfos = propertyInfos;
            StringKeyValuePairs = StringKeyEnumerateHelper.Enumerate(fieldInfos, propertyInfos);
            MinIntKey = minIntKey;
            MaxIntKey = maxIntKey;
            SerializationConstructor = serializationConstructorDefinition;

            AreAllMessagePackPrimitive = this.FieldInfos.All(x => x.IsMessagePackPrimitive) && this.PropertyInfos.All(x => x.IsMessagePackPrimitive);
            PublicAccessible = PublicTypeTestUtility.IsPublicType(definition) && this.FieldInfos.All(x => x.PublicAccessible) && this.PropertyInfos.All(x => x.PublicAccessible);
        }

#if CSHARP_8_0_OR_NEWER
        public StructSerializationInfo(TypeDefinition definition, CustomFormatterTypeInfo customFormatter, MethodDefinition? serializationConstructorDefinition)
#else
        public StructSerializationInfo(TypeDefinition definition, CustomFormatterTypeInfo customFormatter, MethodDefinition serializationConstructorDefinition)
#endif
        {
            Definition = definition;
            CustomFormatter = customFormatter;
            FieldInfos = Array.Empty<FieldSerializationInfo>();
            PropertyInfos = Array.Empty<PropertySerializationInfo>();
            StringKeyValuePairs = Array.Empty<StringKeySerializationInfoTuple>();
            MinIntKey = 0;
            MaxIntKey = 0;
            SerializationConstructor = serializationConstructorDefinition;

            AreAllMessagePackPrimitive = FieldInfos.All(x => x.IsMessagePackPrimitive) && PropertyInfos.All(x => x.IsMessagePackPrimitive);
            PublicAccessible = PublicTypeTestUtility.IsPublicType(definition) && FieldInfos.All(x => x.PublicAccessible) && PropertyInfos.All(x => x.PublicAccessible);
        }

        public bool IsIntKey => MaxIntKey != -1;

        public bool IsStringKey => MaxIntKey == -1;

        public bool IsClass => false;

        public int Count => FieldInfos.Length + PropertyInfos.Length;

        public TypeDefinition Definition { get; }

        public int MinIntKey { get; }

        public int MaxIntKey { get; }

#if CSHARP_8_0_OR_NEWER
        public MethodDefinition? SerializationConstructor { get; }
#else
        public MethodDefinition SerializationConstructor { get; }
#endif

        public CustomFormatterTypeInfo CustomFormatter { get; }

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

        public StringKeySerializationInfoTuple[] StringKeyValuePairs { get; }

        public static bool TryParse(TypeDefinition type, bool useMapMode, out StructSerializationInfo info)
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

                info = new StructSerializationInfo(type, fieldInfos, propertyInfos, minIntKey, maxIntKey, serializationConstructor);
            }
            else
            {
                info = new StructSerializationInfo(type, CustomFormatterDetector.Detect(type, customFormatter), serializationConstructor);
            }

            return true;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            buffer.Append("FullName : ").Append(Definition.FullName)
                .Append("\nIs Public Accessible? : ").Append(PublicAccessible)
                .Append("\nIs Class? : ").Append(IsClass)
                .Append("\nIs Int Key? : ").Append(IsIntKey);

            for (var index = 0; index < this.FieldInfos.Length; index++)
            {
                ref readonly var info = ref FieldInfos[index];
                buffer.Append("\nField").Append(index).Append(" => ").Append(info.ToString());
            }

            for (var index = 0; index < this.PropertyInfos.Length; index++)
            {
                ref readonly var info = ref PropertyInfos[index];
                buffer.Append("\nProperty").Append(index).Append(" => ").Append(info.ToString());
            }

            return buffer.ToString();
        }
    }
}
