// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MSPack.Processor.Core.Definitions;
using Mono.Cecil;

namespace MSPack.Processor.Core.Formatter
{
    public interface IFormatterImplementor
    {
        void Implement(in ClassSerializationInfo info, TypeDefinition formatter);

        void Implement(in StructSerializationInfo info, TypeDefinition formatter);

        void Implement(in UnionInterfaceSerializationInfo info, TypeDefinition formatter);

        void Implement(in UnionClassSerializationInfo info, TypeDefinition formatter);
    }
}
