// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;

namespace MSPack.Processor.Core.Formatter
{
    public interface IGenericClassFormatterImplementor
    {
        void Implement(in GenericClassSerializationInfo info, TypeDefinition formatter);
    }
}