// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public static class CustomAttributeHelper
    {
        public static bool IsFromAnnotations(TypeReference attributeType) => attributeType.Scope.Name == "MessagePack.Annotations";

        public static bool IsUnionAttribute(CustomAttribute attribute)
        {
            var attributeType = attribute.AttributeType;
            return attributeType.FullName == "MessagePack.UnionAttribute" && IsFromAnnotations(attributeType);
        }

        public static bool IsMessagePackFormatterAttribute(CustomAttribute attribute)
        {
            var attributeType = attribute.AttributeType;
            return attributeType.FullName == "MessagePack.MessagePackFormatterAttribute" && IsFromAnnotations(attributeType);
        }

        public static bool IsMessagePackObjectAttribute(CustomAttribute attribute) => IsMessagePackObjectAttribute(attribute, out _);

        public static bool IsMessagePackObjectAttribute(CustomAttribute attribute, out bool keyAsPropertyName)
        {
            var attributeType = attribute.AttributeType;
            var isMessagePackObjectAttribute = attributeType.FullName == "MessagePack.MessagePackObjectAttribute" && IsFromAnnotations(attributeType);
            if (isMessagePackObjectAttribute)
            {
                keyAsPropertyName = (bool)attribute.ConstructorArguments[0].Value;
                return true;
            }

            keyAsPropertyName = false;
            return false;
        }

        public static bool IsIgnoreMemberAttribute(CustomAttribute attribute)
        {
            var attributeType = attribute.AttributeType;
            return attributeType.FullName == "MessagePack.IgnoreMemberAttribute" && IsFromAnnotations(attributeType);
        }

        public static bool IsKeyAttribute(CustomAttribute attribute)
        {
            var attributeType = attribute.AttributeType;
            return attributeType.FullName == "MessagePack.KeyAttribute" && IsFromAnnotations(attributeType);
        }

        public static bool IsIntKeyAttribute(CustomAttribute attribute, out int value)
        {
            if (!IsKeyAttribute(attribute))
            {
                goto FAIL;
            }

            var argument = attribute.ConstructorArguments[0];
            if (argument.Type.Name == "Int32")
            {
                value = (int)argument.Value;
                return true;
            }

        FAIL:
            value = default;
            return false;
        }

        public static bool IsStringKeyAttribute(CustomAttribute attribute, out string value)
        {
            if (!IsKeyAttribute(attribute))
            {
                goto FAIL;
            }

            var argument = attribute.ConstructorArguments[0];
            if (argument.Type.Name == "String")
            {
                value = (string)argument.Value;
                return true;
            }

        FAIL:
            value = string.Empty;
            return false;
        }
    }
}
