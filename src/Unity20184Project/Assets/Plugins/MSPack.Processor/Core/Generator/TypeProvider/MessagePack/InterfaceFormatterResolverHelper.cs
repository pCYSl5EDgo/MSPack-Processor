// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class InterfaceFormatterResolverHelper
    {
        public readonly TypeReference IFormatterResolver;

        public InterfaceFormatterResolverHelper(ModuleDefinition module, IMetadataScope messagePackScope)
        {
            IFormatterResolver = new TypeReference("MessagePack", "IFormatterResolver", module, messagePackScope, false);
        }
    }
}
