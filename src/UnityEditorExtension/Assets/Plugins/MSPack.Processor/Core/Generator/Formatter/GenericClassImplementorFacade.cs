// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core.Formatter
{
    public class GenericClassImplementorFacade : IGenericClassFormatterImplementor
    {
        private readonly TypeDefinition resolverTypeDefinition;
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;

        public GenericClassImplementorFacade(TypeDefinition resolverTypeDefinition, TypeProvider provider, DataHelper dataHelper)
        {
            this.resolverTypeDefinition = resolverTypeDefinition;
            this.module = resolverTypeDefinition.Module;
            this.provider = provider;
            this.dataHelper = dataHelper;
        }

#if CSHARP_8_0_OR_NEWER
        private GenericClassIntKeyFormatterImplementor? genericClassIntKeyFormatterImplementor;
        private GenericClassStringKeyFormatterImplementor? genericClassStringKeyFormatterImplementor;
#else
        private GenericClassIntKeyFormatterImplementor genericClassIntKeyFormatterImplementor;
        private GenericClassStringKeyFormatterImplementor genericClassStringKeyFormatterImplementor;
#endif

        private GenericClassIntKeyFormatterImplementor GetIntKey()
        {
            if (genericClassIntKeyFormatterImplementor is null)
            {
                genericClassIntKeyFormatterImplementor = new GenericClassIntKeyFormatterImplementor(module, provider, provider.Importer);
            }

            return genericClassIntKeyFormatterImplementor;
        }

        private GenericClassStringKeyFormatterImplementor GetStringKey()
        {
            if (genericClassStringKeyFormatterImplementor is null)
            {
                genericClassStringKeyFormatterImplementor = new GenericClassStringKeyFormatterImplementor(module, provider, dataHelper, provider.Importer, resolverTypeDefinition);
            }

            return genericClassStringKeyFormatterImplementor;
        }

        public void Implement(in GenericClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsIntKey)
            {
                GetIntKey().Implement(in info, formatter);
            }
            else
            {
                GetStringKey().Implement(in info, formatter);
            }
        }
    }
}