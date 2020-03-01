// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Definitions
{
    public interface ITypeSerializationInfo
    {
#if CSHARP_8_0_OR_NEWER
        MethodDefinition? SerializationConstructor { get; }

        TypeReference? FormatterDefinition { get; }
#else
        MethodDefinition SerializationConstructor { get; }

        TypeReference FormatterType { get; }
#endif

        bool IsIntKey { get; }

        bool IsStringKey { get; }

        bool IsClass { get; }

        bool IsStruct { get; }

        bool FormatterExists { get; }

        int KeyCount { get; }

        TypeDefinition Definition { get; }

        int MaxIntKey { get; }

        int MinIntKey { get; }

        CustomAttributeArgument[] FormatterConstructorArguments { get; }

        FieldSerializationInfo[] FieldInfos { get; }

        PropertySerializationInfo[] PropertyInfos { get; }

        bool AreAllMessagePackPrimitive { get; }

        bool PublicAccessible { get; }

        FieldOrPropertyInfo this[int key] { get; }

        IEnumerable<(string key, FieldOrPropertyInfo value)> EnumerateStringKeyValuePairs();
    }
}
