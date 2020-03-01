// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Collections.Generic;
using MSPack.Processor.Core.Provider;
using System;
using System.Linq;
using System.Text;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct FieldSerializationInfo : IMemberSerializeInfo, IComparable<FieldSerializationInfo>, IComparable<IMemberSerializeInfo>
    {
        public readonly FieldDefinition Definition;
        public readonly uint Index;
        public readonly bool IsIntKey;
        public readonly int IntKey;
        public readonly string StringKey;
        public readonly int FixedSizeBufferCount;
        public readonly FixedSizeBufferElementType ElementType;

        public bool IsFixedSizeBuffer => ElementType != FixedSizeBufferElementType.None;

        public bool IsStringKey => !IsIntKey;

        public bool IsReadable => true;

        public bool IsWritable { get; }

        public bool IsMessagePackPrimitive { get; }

        public bool PublicAccessible { get; }

        public bool IsValueType => Definition.FieldType.IsValueType;

        public TypeReference MemberTypeReference => Definition.FieldType;

        public Collection<CustomAttribute> CustomAttributes => Definition.CustomAttributes;

        public string FullName => Definition.FullName;

        public FieldSerializationInfo(FieldDefinition definition, uint index, int key, FixedSizeBufferElementType type, int fixedSizeBufferCount)
        {
            IsIntKey = true;
            Definition = definition;
            IntKey = key;
            Index = index;
            StringKey = string.Empty;
            IsWritable = !Definition.IsInitOnly;
            IsMessagePackPrimitive = definition.FieldType.IsMessagePackPrimitive();
            PublicAccessible = definition.IsPublic;
            ElementType = type;
            FixedSizeBufferCount = fixedSizeBufferCount;

            if (key < 0)
            {
                throw new ArgumentOutOfRangeException(definition.FullName, "Key Attribute value should not be less than 0.");
            }
        }

        public FieldSerializationInfo(FieldDefinition definition, uint index, string key, FixedSizeBufferElementType type, int fixedSizeBufferCount)
        {
            IsIntKey = false;
            IntKey = -1;
            Definition = definition;
            StringKey = key;
            Index = index;
            IsWritable = !Definition.IsInitOnly;
            IsMessagePackPrimitive = definition.FieldType.IsMessagePackPrimitive();
            PublicAccessible = definition.IsPublic;
            ElementType = type;
            FixedSizeBufferCount = fixedSizeBufferCount;
        }

        bool IMemberSerializeInfo.IsIntKey => IsIntKey;

        int IMemberSerializeInfo.IntKey => IntKey;

        string IMemberSerializeInfo.StringKey => StringKey;

        uint IMemberSerializeInfo.Index => Index;

        public static bool TryParse(FieldDefinition definition, uint index, bool useMapMode, out FieldSerializationInfo info)
        {
            if (definition.CustomAttributes.Any(CustomAttributeHelper.IsIgnoreMemberAttribute))
            {
                goto FAIL;
            }

            var keyAttribute = definition.CustomAttributes.FirstOrDefault(CustomAttributeHelper.IsKeyAttribute);
            if (keyAttribute is null)
            {
                if (definition.IsPublic)
                {
                    throw new MessagePackGeneratorResolveFailedException("all public members must mark KeyAttribute or IgnoreMemberAttribute : " + definition.FullName);
                }

                goto FAIL;
            }

            var (type, elementCount) = ParseFixedSizeBuffer(definition);

            if (CustomAttributeHelper.IsStringKeyAttribute(keyAttribute, out var stringKey))
            {
                info = new FieldSerializationInfo(definition, index, stringKey, type, elementCount);
                return true;
            }

            if (useMapMode)
            {
                info = new FieldSerializationInfo(definition, index, definition.Name, type, elementCount);
                return true;
            }

            if (CustomAttributeHelper.IsIntKeyAttribute(keyAttribute, out var intKey))
            {
                info = new FieldSerializationInfo(definition, index, intKey, type, elementCount);
                return true;
            }

        FAIL:
            info = default;
            return false;
        }

        private static (FixedSizeBufferElementType, int) ParseFixedSizeBuffer(FieldDefinition definition)
        {
            if (!definition.HasCustomAttributes)
            {
                return default;
            }

            foreach (var attribute in definition.CustomAttributes)
            {
                if (attribute.AttributeType.FullName != "System.Runtime.CompilerServices.FixedBufferAttribute")
                {
                    continue;
                }

                var typeStr = ((TypeReference)attribute.ConstructorArguments[0].Value).Name;
                if (!Enum.TryParse(typeStr, out FixedSizeBufferElementType type))
                {
                    continue;
                }

                var count = (int)attribute.ConstructorArguments[1].Value;
                return (type, count);
            }

            return default;
        }

        public int CompareTo(FieldSerializationInfo other)
        {
            if (IsIntKey ^ other.IsIntKey)
            {
                throw new MessagePackGeneratorResolveFailedException("all members key type must be same. type : " + Definition.FullName);
            }

            if (IsIntKey)
            {
                if (IntKey < other.IntKey)
                {
                    return -1;
                }

                if (IntKey > other.IntKey)
                {
                    return 1;
                }

                throw new MessagePackGeneratorResolveFailedException("key is duplicated, all members key must be unique. type : " + Definition.FullName);
            }

            if (Index < other.Index)
            {
                return -1;
            }

            return Index > other.Index ? 1 : 0;
        }

        public int CompareTo(IMemberSerializeInfo other)
        {
            if (IsIntKey ^ other.IsIntKey)
            {
                throw new MessagePackGeneratorResolveFailedException("all members key type must be same. type : " + Definition.FullName);
            }

            if (IsIntKey)
            {
                if (IntKey < other.IntKey)
                {
                    return -1;
                }

                if (IntKey > other.IntKey)
                {
                    return 1;
                }

                throw new MessagePackGeneratorResolveFailedException("key is duplicated, all members key must be unique. type : " + Definition.FullName);
            }

            if (Index < other.Index)
            {
                return -1;
            }

            return Index > other.Index ? 1 : 0;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            buffer.Append("Name : ").Append(FullName)
                .Append(" => ").Append(MemberTypeReference.FullName)
                .Append(" || Key : ");

            if (IsIntKey)
            {
                buffer.Append(IntKey);
            }
            else
            {
                buffer.Append(StringKey);
            }

            return buffer.ToString();
        }
    }
}
