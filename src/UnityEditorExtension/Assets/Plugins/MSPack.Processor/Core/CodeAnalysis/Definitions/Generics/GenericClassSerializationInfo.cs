// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct GenericClassSerializationInfo : IGenericTypeSerializationInfo
    {
        public GenericClassSerializationInfo(TypeDefinition definition, FieldSerializationInfo[] fieldInfos, PropertySerializationInfo[] propertyInfos, int minIntKey, int maxIntKey, GenericInstanceType[] definitionVariations)
        {
            Definition = definition;
            CustomFormatter = CustomFormatterTypeInfo.Default;
            FieldInfos = fieldInfos;
            PropertyInfos = propertyInfos;
            MinIntKey = minIntKey;
            MaxIntKey = maxIntKey;
            SerializationConstructor = default;
            DefinitionGenericInstanceVariations = definitionVariations;

            AreAllMessagePackPrimitive = this.FieldInfos.All(x => x.IsMessagePackPrimitive) && this.PropertyInfos.All(x => x.IsMessagePackPrimitive);
            PublicAccessible = PublicTypeTestUtility.IsPublicType(definition) && this.FieldInfos.All(x => x.PublicAccessible) && this.PropertyInfos.All(x => x.PublicAccessible);
        }

        public GenericClassSerializationInfo(TypeDefinition definition, CustomFormatterTypeInfo customFormatter, GenericInstanceType[] definitionVariations)
        {
            Definition = definition;
            CustomFormatter = customFormatter;
            FieldInfos = Array.Empty<FieldSerializationInfo>();
            PropertyInfos = Array.Empty<PropertySerializationInfo>();
            MinIntKey = 0;
            MaxIntKey = 0;
            SerializationConstructor = default;
            DefinitionGenericInstanceVariations = definitionVariations;

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

        public int Count => FieldInfos.Length + PropertyInfos.Length;

        public TypeDefinition Definition { get; }

        public int MinIntKey { get; }

        public int MaxIntKey { get; }

        public CustomFormatterTypeInfo CustomFormatter { get; }

        public FieldSerializationInfo[] FieldInfos { get; }

        public PropertySerializationInfo[] PropertyInfos { get; }

        public bool AreAllMessagePackPrimitive { get; }

        public bool PublicAccessible { get; }

        public GenericInstanceType[] DefinitionGenericInstanceVariations { get; }

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