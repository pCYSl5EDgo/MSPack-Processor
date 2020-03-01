// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Linq;

namespace MSPack.Processor.Core
{
    public readonly struct FormatterInfo : IEquatable<FormatterInfo>
    {
        public readonly TypeReference SerializeTypeReference;
        public readonly TypeReference FormatterType;
        public readonly CustomAttributeArgument[] FormatterConstructorArguments;

        public FormatterInfo(TypeReference serializeTypeReference, TypeReference formatterType, CustomAttributeArgument[] formatterConstructorArguments)
        {
            SerializeTypeReference = serializeTypeReference;
            FormatterType = formatterType;
            FormatterConstructorArguments = formatterConstructorArguments;
        }

        public bool Equals(FormatterInfo other)
        {
            return ReferenceEquals(SerializeTypeReference, other.SerializeTypeReference) && ReferenceEquals(FormatterType, other.FormatterType) && FormatterConstructorArguments.SequenceEqual(other.FormatterConstructorArguments);
        }

        public override bool Equals(object obj)
        {
            return obj is FormatterInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SerializeTypeReference != null ? SerializeTypeReference.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FormatterType != null ? FormatterType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FormatterConstructorArguments != null ? FormatterConstructorArguments.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
