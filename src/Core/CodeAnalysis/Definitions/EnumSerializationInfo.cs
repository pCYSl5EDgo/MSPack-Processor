// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public enum EnumUnderlyingType
    {
        Byte,
        UInt16,
        UInt32,
        UInt64,
        SByte,
        Int16,
        Int32,
        Int64,
    }

    public readonly struct EnumSerializationInfo : IEquatable<EnumSerializationInfo>
    {
        public readonly TypeDefinition Definition;
        public readonly EnumUnderlyingType UnderlyingType;

        public EnumSerializationInfo(TypeDefinition definition)
        {
            Definition = definition;
            if (!Enum.TryParse(definition.Fields[0].FieldType.Name, out UnderlyingType))
            {
                throw new MessagePackGeneratorResolveFailedException(definition.FullName + " has not proper underlying type.");
            }
        }

        public TypeReference GetUnderlyingTypeReference(ModuleDefinition module) =>
            UnderlyingType switch
            {
                EnumUnderlyingType.Byte => module.TypeSystem.Byte,
                EnumUnderlyingType.UInt16 => module.TypeSystem.UInt16,
                EnumUnderlyingType.UInt32 => module.TypeSystem.UInt32,
                EnumUnderlyingType.UInt64 => module.TypeSystem.UInt64,
                EnumUnderlyingType.SByte => module.TypeSystem.SByte,
                EnumUnderlyingType.Int16 => module.TypeSystem.Int16,
                EnumUnderlyingType.Int32 => module.TypeSystem.Int32,
                EnumUnderlyingType.Int64 => module.TypeSystem.Int64,
                _ => throw new ArgumentOutOfRangeException()
            };

        public bool Equals(EnumSerializationInfo other)
        {
            return (object)Definition == other.Definition;
        }

        public override bool Equals(object? obj)
        {
            return obj is EnumSerializationInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Definition.GetHashCode();
        }
    }
}
