﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
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

        public TypeReference GetUnderlyingTypeReference(ModuleDefinition module)
        {
            switch (UnderlyingType)
            {
                case EnumUnderlyingType.Byte:
                    return module.TypeSystem.Byte;
                case EnumUnderlyingType.UInt16:
                    return module.TypeSystem.UInt16;
                case EnumUnderlyingType.UInt32:
                    return module.TypeSystem.UInt32;
                case EnumUnderlyingType.UInt64:
                    return module.TypeSystem.UInt64;
                case EnumUnderlyingType.SByte:
                    return module.TypeSystem.SByte;
                case EnumUnderlyingType.Int16:
                    return module.TypeSystem.Int16;
                case EnumUnderlyingType.Int32:
                    return module.TypeSystem.Int32;
                case EnumUnderlyingType.Int64:
                    return module.TypeSystem.Int64;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool Equals(EnumSerializationInfo other)
        {
            return (object)Definition == other.Definition;
        }
#if CSHARP_8_0_OR_NEWER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            return obj is EnumSerializationInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Definition.GetHashCode();
        }
    }
}
