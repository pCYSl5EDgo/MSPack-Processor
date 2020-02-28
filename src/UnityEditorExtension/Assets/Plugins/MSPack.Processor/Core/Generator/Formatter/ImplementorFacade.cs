﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using System.Linq;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class ImplementorFacade : IFormatterImplementor
    {
        private readonly ClassIntKeyFormatterImplementor classIntKeyImplementor;
        private readonly ClassIntKeyAllMessagePackPrimitiveFormatterImplementor classIntKeyAllMessagePackPrimitiveFormatterImplementor;
        private readonly ClassStringKeyImplementor classStringKeyImplementor;
        private readonly ClassStringKeyAllMessagePackPrimitiveImplementor classStringKeyAllMessagePackPrimitiveImplementor;

        private readonly StructIntKeyFormatterImplementor structIntKeyFormatterImplementor;
        private readonly StructIntKeyFormatterImplementorWithConstructor structIntKeyFormatterImplementorWithConstructor;
        private readonly StructIntKeyAllMessagePackPrimitiveFormatterImplementor structIntKeyAllMessagePackPrimitiveFormatterImplementor;
        private readonly StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor;
        private readonly StructStringKeyImplementor structStringKeyImplementor;
        private readonly StructStringKeyAllMessagePackPrimitiveImplementor structStringKeyAllMessagePackPrimitiveImplementor;

        private readonly UnionInterfaceFormatterAllConsequentImplementor unionInterfaceFormatterAllConsequentImplementor;
        private readonly UnionInterfaceFormatterImplementor unionInterfaceFormatterImplementor;

        private readonly UnionClassFormatterAllConsequentImplementor unionClassFormatterAllConsequentImplementor;
        private readonly UnionClassFormatterImplementor unionClassFormatterImplementor;

        public ImplementorFacade(TypeProvider provider, double loadFactor)
        {
            var module = provider.Module;
            var dataHelper = new DataHelper(module, provider.SystemValueTypeHelper.ValueType);

            classIntKeyImplementor = new ClassIntKeyFormatterImplementor(module, provider);
            classIntKeyAllMessagePackPrimitiveFormatterImplementor = new ClassIntKeyAllMessagePackPrimitiveFormatterImplementor(module, provider);
            structIntKeyFormatterImplementor = new StructIntKeyFormatterImplementor(module, provider);
            structIntKeyAllMessagePackPrimitiveFormatterImplementor = new StructIntKeyAllMessagePackPrimitiveFormatterImplementor(module, provider);
            structIntKeyFormatterImplementorWithConstructor = new StructIntKeyFormatterImplementorWithConstructor(module, provider);
            structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor = new StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor(module, provider);

            classStringKeyImplementor = new ClassStringKeyImplementor(module, provider, dataHelper);
            classStringKeyAllMessagePackPrimitiveImplementor = new ClassStringKeyAllMessagePackPrimitiveImplementor(module, provider, dataHelper);
            structStringKeyImplementor = new StructStringKeyImplementor(module, provider, dataHelper);
            structStringKeyAllMessagePackPrimitiveImplementor = new StructStringKeyAllMessagePackPrimitiveImplementor(module, provider, dataHelper);

            var typeKeyUInt64ValuePairGenerator = new TypeKeyUInt64ValuePairGenerator(module, provider.SystemValueTypeHelper, provider.SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper, provider.SystemTypeHelper);
            var fixedTypeKeyUInt64ValueHashtableGenerator = new FixedTypeKeyUInt64ValueHashtableGenerator(module, typeKeyUInt64ValuePairGenerator, provider.SystemObjectHelper, provider.SystemTypeHelper, provider.Importer, provider.SystemArrayHelper, loadFactor);
            unionInterfaceFormatterImplementor = new UnionInterfaceFormatterImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, fixedTypeKeyUInt64ValueHashtableGenerator);
            unionClassFormatterImplementor = new UnionClassFormatterImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, fixedTypeKeyUInt64ValueHashtableGenerator);

            var typeKeyInt32ValuePairGenerator = new TypeKeyInt32ValuePairGenerator(module, provider.SystemValueTypeHelper, provider.SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper, provider.SystemTypeHelper);
            var fixedTypeKeyInt32Generator = new FixedTypeKeyInt32ValueHashtableGenerator(module, typeKeyInt32ValuePairGenerator, provider.SystemObjectHelper, provider.SystemTypeHelper, provider.Importer, provider.SystemArrayHelper, loadFactor);
            unionInterfaceFormatterAllConsequentImplementor = new UnionInterfaceFormatterAllConsequentImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, fixedTypeKeyInt32Generator);
            unionClassFormatterAllConsequentImplementor = new UnionClassFormatterAllConsequentImplementor(module, provider.SystemObjectHelper, provider.InterfaceMessagePackFormatterHelper, provider.SystemInvalidOperationExceptionHelper, provider.Importer, provider.MessagePackSecurityHelper, provider.MessagePackSerializerOptionsHelper, provider.MessagePackWriterHelper, provider.MessagePackReaderHelper, provider.FormatterResolverExtensionHelper, fixedTypeKeyInt32Generator);
        }

        public void Implement(in ClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsIntKey)
            {
                if (info.AreAllMessagePackPrimitive)
                {
                    classIntKeyAllMessagePackPrimitiveFormatterImplementor.Implement(info, formatter);
                }
                else
                {
                    classIntKeyImplementor.Implement(info, formatter);
                }
            }
            else
            {
                if (info.AreAllMessagePackPrimitive)
                {
                    classStringKeyAllMessagePackPrimitiveImplementor.Implement(info, formatter);
                }
                else
                {
                    classStringKeyImplementor.Implement(info, formatter);
                }
            }
        }

        public void Implement(in StructSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsIntKey)
            {
                if (info.SerializationConstructor is null)
                {
                    if (info.AreAllMessagePackPrimitive)
                    {
                        structIntKeyAllMessagePackPrimitiveFormatterImplementor.Implement(info, formatter);
                    }
                    else
                    {
                        structIntKeyFormatterImplementor.Implement(info, formatter);
                    }
                }
                else
                {
                    if (info.SerializationConstructor.Parameters.All(x => x.ParameterType.IsMessagePackPrimitive()))
                    {
                        structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor.Implement(info, formatter);
                    }
                    else
                    {
                        structIntKeyFormatterImplementorWithConstructor.Implement(info, formatter);
                    }
                }
            }
            else
            {
                if (info.AreAllMessagePackPrimitive)
                {
                    structStringKeyAllMessagePackPrimitiveImplementor.Implement(info, formatter);
                }
                else
                {
                    structStringKeyImplementor.Implement(info, formatter);
                }
            }
        }

        public void Implement(in UnionInterfaceSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsPerfectKeyIndexMatch)
            {
                unionInterfaceFormatterAllConsequentImplementor.Implement(in info, formatter);
            }
            else
            {
                unionInterfaceFormatterImplementor.Implement(in info, formatter);
            }
        }

        public void Implement(in UnionClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsPerfectKeyIndexMatch)
            {
                unionClassFormatterAllConsequentImplementor.Implement(in info, formatter);
            }
            else
            {
                unionClassFormatterImplementor.Implement(in info, formatter);
            }
        }
    }
}
