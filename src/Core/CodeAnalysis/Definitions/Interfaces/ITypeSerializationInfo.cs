// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public interface ITypeSerializationInfo
    {
        CustomFormatterTypeInfo CustomFormatter { get; }

#if CSHARP_8_0_OR_NEWER
        MethodDefinition? SerializationConstructor { get; }
#else
        MethodDefinition SerializationConstructor { get; }
#endif

        bool IsClass { get; }

        TypeDefinition Definition { get; }

        FieldSerializationInfo[] FieldInfos { get; }

        PropertySerializationInfo[] PropertyInfos { get; }

        bool AreAllMessagePackPrimitive { get; }

        bool PublicAccessible { get; }

        int Count { get; }
    }
}
