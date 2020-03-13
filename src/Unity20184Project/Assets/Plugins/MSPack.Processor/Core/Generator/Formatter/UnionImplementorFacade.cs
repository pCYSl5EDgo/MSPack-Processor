// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core.Formatter
{
    public class UnionImplementorFacade : IUnionFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly double loadFactor;

        public UnionImplementorFacade(ModuleDefinition module, TypeProvider provider, double loadFactor)
        {
            this.module = module;
            this.provider = provider;
            this.loadFactor = loadFactor;
        }

#if CSHARP_8_0_OR_NEWER
        private UnionInterfaceFormatterAllConsequentImplementor? unionInterfaceFormatterAllConsequentImplementor;
        private UnionInterfaceFormatterImplementor? unionInterfaceFormatterImplementor;

        private UnionClassFormatterAllConsequentImplementor? unionClassFormatterAllConsequentImplementor;
        private UnionClassFormatterImplementor? unionClassFormatterImplementor;

        TypeKeyUInt64ValuePairGenerator? typeKeyUInt64ValuePairGenerator;
        FixedTypeKeyUInt64ValueHashtableGenerator? fixedTypeKeyUInt64ValueHashtableGenerator;
        TypeKeyInt32ValuePairGenerator? typeKeyInt32ValuePairGenerator;
        FixedTypeKeyInt32ValueHashtableGenerator? fixedTypeKeyInt32ValueHashtableGenerator;
#else
        private UnionInterfaceFormatterAllConsequentImplementor unionInterfaceFormatterAllConsequentImplementor;
        private UnionInterfaceFormatterImplementor unionInterfaceFormatterImplementor;

        private UnionClassFormatterAllConsequentImplementor unionClassFormatterAllConsequentImplementor;
        private UnionClassFormatterImplementor unionClassFormatterImplementor;

        TypeKeyUInt64ValuePairGenerator typeKeyUInt64ValuePairGenerator;
        FixedTypeKeyUInt64ValueHashtableGenerator fixedTypeKeyUInt64ValueHashtableGenerator;
        TypeKeyInt32ValuePairGenerator typeKeyInt32ValuePairGenerator;
        FixedTypeKeyInt32ValueHashtableGenerator fixedTypeKeyInt32ValueHashtableGenerator;
#endif

        private TypeKeyUInt64ValuePairGenerator GetUlongPair()
        {
            if (typeKeyUInt64ValuePairGenerator is null)
            {
                typeKeyUInt64ValuePairGenerator = new TypeKeyUInt64ValuePairGenerator(module, provider.SystemValueTypeHelper, provider.SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper, provider.SystemTypeHelper);
            }

            return typeKeyUInt64ValuePairGenerator;
        }

        private TypeKeyInt32ValuePairGenerator GetIntPair()
        {
            if (typeKeyInt32ValuePairGenerator is null)
            {
                typeKeyInt32ValuePairGenerator = new TypeKeyInt32ValuePairGenerator(module, provider.SystemValueTypeHelper, provider.SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper, provider.SystemTypeHelper);
            }

            return typeKeyInt32ValuePairGenerator;
        }

        private FixedTypeKeyUInt64ValueHashtableGenerator GetUlongTable()
        {
            if (fixedTypeKeyUInt64ValueHashtableGenerator is null)
            {
                fixedTypeKeyUInt64ValueHashtableGenerator = new FixedTypeKeyUInt64ValueHashtableGenerator(module, GetUlongPair(), provider.SystemObjectHelper, provider.SystemTypeHelper, provider.Importer, provider.SystemArrayHelper, loadFactor);
            }

            return fixedTypeKeyUInt64ValueHashtableGenerator;
        }

        private FixedTypeKeyInt32ValueHashtableGenerator GetIntTable()
        {
            if (fixedTypeKeyInt32ValueHashtableGenerator is null)
            {
                fixedTypeKeyInt32ValueHashtableGenerator = new FixedTypeKeyInt32ValueHashtableGenerator(module, GetIntPair(), provider.SystemObjectHelper, provider.SystemTypeHelper, provider.Importer, provider.SystemArrayHelper, loadFactor);
            }

            return fixedTypeKeyInt32ValueHashtableGenerator;
        }

        private UnionInterfaceFormatterImplementor GetInterface()
        {
            if (unionInterfaceFormatterImplementor is null)
            {
                unionInterfaceFormatterImplementor = new UnionInterfaceFormatterImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, GetUlongTable());
            }

            return unionInterfaceFormatterImplementor;
        }

        private UnionClassFormatterImplementor GetClass()
        {
            if (unionClassFormatterImplementor is null)
            {
                unionClassFormatterImplementor = new UnionClassFormatterImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, GetUlongTable());
            }

            return unionClassFormatterImplementor;
        }

        private UnionInterfaceFormatterAllConsequentImplementor GetInterfaceAllConsequent()
        {
            if (unionInterfaceFormatterAllConsequentImplementor is null)
            {
                unionInterfaceFormatterAllConsequentImplementor = new UnionInterfaceFormatterAllConsequentImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, GetIntTable());
            }

            return unionInterfaceFormatterAllConsequentImplementor;
        }
        private UnionClassFormatterAllConsequentImplementor GetClassAllConsequent()
        {
            if (unionClassFormatterAllConsequentImplementor is null)
            {
                unionClassFormatterAllConsequentImplementor = new UnionClassFormatterAllConsequentImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, GetIntTable());
            }

            return unionClassFormatterAllConsequentImplementor;
        }

        public void Implement(in UnionInterfaceSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsPerfectKeyIndexMatch)
            {
                GetInterfaceAllConsequent().Implement(info, formatter);
            }
            else
            {
                GetInterface().Implement(info, formatter);
            }
        }

        public void Implement(in UnionClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsPerfectKeyIndexMatch)
            {
                GetClassAllConsequent().Implement(info, formatter);
            }
            else
            {
                GetClass().Implement(info, formatter);
            }
        }
    }
}
