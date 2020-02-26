// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public interface ITypeSerializationInfo
    {
        MethodDefinition? SerializationConstructor { get; }

        bool IsIntKey { get; }

        bool IsStringKey { get; }

        bool IsClass { get; }

        bool IsStruct { get; }

        bool FormatterExists { get; }

        int KeyCount { get; }

        TypeDefinition Definition { get; }

        string FormatterName { get; }

        int MaxIntKey { get; }

        int MinIntKey { get; }

        TypeDefinition? FormatterDefinition { get; }

        CustomAttributeArgument[] FormatterConstructorArguments { get; }

        FieldSerializationInfo[] FieldInfos { get; }

        PropertySerializationInfo[] PropertyInfos { get; }

        bool AreAllMessagePackPrimitive { get; }

        bool PublicAccessible { get; }

        FieldOrPropertyInfo this[int key] { get; }

        IEnumerable<(string key, FieldOrPropertyInfo value)> EnumerateStringKeyValuePairs();
    }
}
