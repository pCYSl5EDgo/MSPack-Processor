﻿// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using System.Linq;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class ImplementorFacade : IFormatterImplementor
    {
        private readonly ClassIntKeyFormatterImplementor classIntKeyFormatterImplementor;
        private readonly ClassIntKeyAllMessagePackPrimitiveFormatterImplementor classIntKeyAllMessagePackPrimitiveFormatterImplementor;
        private readonly ClassStringKeyFormatterImplementor classStringKeyFormatterImplementor;
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

        private readonly GenericClassIntKeyFormatterImplementor genericClassIntKeyFormatterImplementor;
        private readonly GenericClassStringKeyFormatterImplementor genericClassStringKeyFormatterImplementor;

        public ImplementorFacade(TypeProvider provider, double loadFactor)
        {
            var module = provider.Module;
            var dataHelper = new DataHelper(module, provider.SystemValueTypeHelper.ValueType);

            classIntKeyFormatterImplementor = new ClassIntKeyFormatterImplementor(module, provider);
            classIntKeyAllMessagePackPrimitiveFormatterImplementor = new ClassIntKeyAllMessagePackPrimitiveFormatterImplementor(module, provider);
            structIntKeyFormatterImplementor = new StructIntKeyFormatterImplementor(module, provider);
            structIntKeyAllMessagePackPrimitiveFormatterImplementor = new StructIntKeyAllMessagePackPrimitiveFormatterImplementor(module, provider);
            structIntKeyFormatterImplementorWithConstructor = new StructIntKeyFormatterImplementorWithConstructor(module, provider);
            structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor = new StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor(module, provider);

            classStringKeyFormatterImplementor = new ClassStringKeyFormatterImplementor(module, provider, dataHelper);
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

            genericClassIntKeyFormatterImplementor = new GenericClassIntKeyFormatterImplementor(module, provider);
            genericClassStringKeyFormatterImplementor = new GenericClassStringKeyFormatterImplementor(module, provider, dataHelper);
        }

        public void Implement(in ClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsIntKey)
            {
                ImplementIntKey(info, formatter);
            }
            else
            {
                ImplementStringKey(info, formatter);
            }
        }

        private void ImplementStringKey(in ClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.AreAllMessagePackPrimitive)
            {
                classStringKeyAllMessagePackPrimitiveImplementor.Implement(info, formatter);
                return;
            }

            classStringKeyFormatterImplementor.Implement(info, formatter);
        }

        private void ImplementIntKey(in ClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.AreAllMessagePackPrimitive)
            {
                classIntKeyAllMessagePackPrimitiveFormatterImplementor.Implement(info, formatter);
                return;
            }

            /*for (var index = 0; index < info.FieldInfos.Length; index++)
            {
                if (info.FieldInfos[index].IsFixedArray)
                {
                    return;
                }
            }

            for (var index = 0; index < info.PropertyInfos.Length; index++)
            {
                if (info.PropertyInfos[index].IsFixedArray)
                {
                    return;
                }
            }*/

            classIntKeyFormatterImplementor.Implement(info, formatter);
        }

        public void Implement(in StructSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsIntKey)
            {
                ImplementIntKey(info, formatter);
            }
            else
            {
                ImplementStringKey(info, formatter);
            }
        }

        private void ImplementStringKey(in StructSerializationInfo info, TypeDefinition formatter)
        {
            if (info.AreAllMessagePackPrimitive)
            {
                structStringKeyAllMessagePackPrimitiveImplementor.Implement(info, formatter);
                return;
            }

            structStringKeyImplementor.Implement(info, formatter);
        }

        private void ImplementIntKey(in StructSerializationInfo info, TypeDefinition formatter)
        {
            if (info.SerializationConstructor is null)
            {
                ImplementIntKeyWithoutSerializationConstructor(info, formatter);
            }
            else
            {
                ImplementIntKeyWithSerializationConstructor(info, formatter, info.SerializationConstructor);
            }
        }

        private void ImplementIntKeyWithoutSerializationConstructor(in StructSerializationInfo info, TypeDefinition formatter)
        {
            if (info.AreAllMessagePackPrimitive)
            {
                structIntKeyAllMessagePackPrimitiveFormatterImplementor.Implement(info, formatter);
                return;
            }

            structIntKeyFormatterImplementor.Implement(info, formatter);
        }

        private void ImplementIntKeyWithSerializationConstructor(in StructSerializationInfo info, TypeDefinition formatter, MethodDefinition serializationConstructor)
        {
            if (serializationConstructor.Parameters.All(x => x.ParameterType.IsMessagePackPrimitive()))
            {
                structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor.Implement(info, formatter);
            }
            else
            {
                structIntKeyFormatterImplementorWithConstructor.Implement(info, formatter);
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

        public void Implement(in GenericClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.IsIntKey)
            {
                genericClassIntKeyFormatterImplementor.Implement(in info, formatter);
            }
            else
            {
                genericClassStringKeyFormatterImplementor.Implement(in info, formatter);
            }
        }
    }
}
