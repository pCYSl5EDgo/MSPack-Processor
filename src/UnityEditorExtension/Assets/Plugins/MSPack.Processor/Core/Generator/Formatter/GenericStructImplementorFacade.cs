// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core.Formatter
{
    public class GenericStructImplementorFacade : IGenericStructFormatterImplementor
    {
        private readonly TypeDefinition resolverTypeDefinition;
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;

        public GenericStructImplementorFacade(TypeDefinition resolverTypeDefinition, TypeProvider provider, DataHelper dataHelper)
        {
            this.resolverTypeDefinition = resolverTypeDefinition;
            this.module = resolverTypeDefinition.Module;
            this.provider = provider;
            this.dataHelper = dataHelper;
        }

#if CSHARP_8_0_OR_NEWER
        private GenericStructIntKeyFormatterImplementor? genericStructIntKeyFormatterImplementor;
        private GenericStructStringKeyFormatterImplementor? genericStructStringKeyFormatterImplementor;
#else
        private GenericStructIntKeyFormatterImplementor genericStructIntKeyFormatterImplementor;
        private GenericStructStringKeyFormatterImplementor genericStructStringKeyFormatterImplementor;
#endif

        private GenericStructIntKeyFormatterImplementor GetIntKey()
        {
            if (genericStructIntKeyFormatterImplementor is null)
            {
                genericStructIntKeyFormatterImplementor = new GenericStructIntKeyFormatterImplementor(module, provider, provider.Importer);
            }

            return genericStructIntKeyFormatterImplementor;
        }

        private GenericStructStringKeyFormatterImplementor GetStringKey()
        {
            if (genericStructStringKeyFormatterImplementor is null)
            {
                genericStructStringKeyFormatterImplementor = new GenericStructStringKeyFormatterImplementor(module, provider, dataHelper, provider.Importer, resolverTypeDefinition);
            }

            return genericStructStringKeyFormatterImplementor;
        }

        public void Implement(in GenericStructSerializationInfo info, TypeDefinition formatter)
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