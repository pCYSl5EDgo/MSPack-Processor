// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct CustomFormatterTypeInfo : IEquatable<CustomFormatterTypeInfo>
    {
        public CustomFormatterTypeInfo(TypeReference formatterType, CustomAttributeArgument[] formatterConstructorArguments)
        {
            FormatterType = formatterType;
            FormatterConstructorArguments = formatterConstructorArguments;
        }

        private CustomFormatterTypeInfo(int _)
        {
            FormatterType = default;
            FormatterConstructorArguments = Array.Empty<CustomAttributeArgument>();
        }

        public static CustomFormatterTypeInfo Default => new CustomFormatterTypeInfo(0);

#if CSHARP_8_0_OR_NEWER
        public TypeReference? FormatterType { get; }
#else
        public TypeReference FormatterType { get; }
#endif

        public CustomAttributeArgument[] FormatterConstructorArguments { get; }

        public bool Equals(CustomFormatterTypeInfo other)
        {
            return Equals(FormatterType, other.FormatterType) && FormatterConstructorArguments.SequenceEqual(other.FormatterConstructorArguments);
        }

        public override bool Equals(object? obj)
        {
            return obj is CustomFormatterTypeInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            if (ReferenceEquals(FormatterType, default))
            {
                return default;
            }

            unchecked
            {
                return ((FormatterType != null ? FormatterType.GetHashCode() : 0) * 397) ^ FormatterConstructorArguments.GetHashCode();
            }
        }
    }
}