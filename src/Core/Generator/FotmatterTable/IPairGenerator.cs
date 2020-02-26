// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public interface IPairGenerator
    {
        TypeDefinition Pair { get; }

        FieldDefinition Key { get; }

        FieldDefinition Value { get; }
    }
}
