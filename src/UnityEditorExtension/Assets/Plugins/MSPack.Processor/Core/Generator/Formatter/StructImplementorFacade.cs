// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core.Formatter
{
    public class StructImplementorFacade : IStructFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;

        public StructImplementorFacade(ModuleDefinition module, TypeProvider provider, DataHelper dataHelper)
        {
            this.module = module;
            this.provider = provider;
            this.dataHelper = dataHelper;
        }

#if CSHARP_8_0_OR_NEWER
        private StructIntKeyFormatterImplementor? structIntKeyFormatterImplementor;
        private StructIntKeyFormatterImplementorWithConstructor? structIntKeyFormatterImplementorWithConstructor;
        private StructIntKeyAllMessagePackPrimitiveFormatterImplementor? structIntKeyAllMessagePackPrimitiveFormatterImplementor;
        private StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor? structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor;
        private StructStringKeyFormatterImplementor? structStringKeyFormatterImplementor;
        private StructStringKeyAllMessagePackPrimitiveFormatterImplementor? structStringKeyAllMessagePackPrimitiveFormatterImplementor;
#else
        private StructIntKeyFormatterImplementor structIntKeyFormatterImplementor;
        private StructIntKeyFormatterImplementorWithConstructor structIntKeyFormatterImplementorWithConstructor;
        private StructIntKeyAllMessagePackPrimitiveFormatterImplementor structIntKeyAllMessagePackPrimitiveFormatterImplementor;
        private StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor;
        private StructStringKeyFormatterImplementor structStringKeyFormatterImplementor;
        private StructStringKeyAllMessagePackPrimitiveFormatterImplementor structStringKeyAllMessagePackPrimitiveFormatterImplementor;
#endif

        private StructIntKeyFormatterImplementor GetIntKeyFormatterImplementor()
        {
            if (structIntKeyFormatterImplementor is null)
            {
                structIntKeyFormatterImplementor = new StructIntKeyFormatterImplementor(module, provider);
            }

            return structIntKeyFormatterImplementor;
        }

        private StructIntKeyFormatterImplementorWithConstructor GetIntKeyFormatterImplementorWithConstructor()
        {
            if (structIntKeyFormatterImplementorWithConstructor is null)
            {
                structIntKeyFormatterImplementorWithConstructor = new StructIntKeyFormatterImplementorWithConstructor(module, provider);
            }

            return structIntKeyFormatterImplementorWithConstructor;
        }

        private StructIntKeyAllMessagePackPrimitiveFormatterImplementor GetIntKeyAllMessagePackPrimitiveFormatterImplementor()
        {
            if (structIntKeyAllMessagePackPrimitiveFormatterImplementor is null)
            {
                structIntKeyAllMessagePackPrimitiveFormatterImplementor = new StructIntKeyAllMessagePackPrimitiveFormatterImplementor(module, provider);
            }

            return structIntKeyAllMessagePackPrimitiveFormatterImplementor;
        }

        private StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor GetIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor()
        {
            if (structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor is null)
            {
                structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor = new StructIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor(module, provider);
            }

            return structIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor;
        }

        private StructStringKeyFormatterImplementor GetStringKeyFormatterImplementor()
        {
            if (structStringKeyFormatterImplementor is null)
            {
                structStringKeyFormatterImplementor = new StructStringKeyFormatterImplementor(module, provider, dataHelper);
            }

            return structStringKeyFormatterImplementor;
        }

        private StructStringKeyAllMessagePackPrimitiveFormatterImplementor GetStringKeyAllMessagePackPrimitiveFormatterImplementor()
        {
            if (structStringKeyAllMessagePackPrimitiveFormatterImplementor is null)
            {
                structStringKeyAllMessagePackPrimitiveFormatterImplementor = new StructStringKeyAllMessagePackPrimitiveFormatterImplementor(module, provider, dataHelper);
            }

            return structStringKeyAllMessagePackPrimitiveFormatterImplementor;
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
                GetStringKeyAllMessagePackPrimitiveFormatterImplementor().Implement(info, formatter);
                return;
            }

            GetStringKeyFormatterImplementor().Implement(info, formatter);
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
                GetIntKeyAllMessagePackPrimitiveFormatterImplementor().Implement(info, formatter);
                return;
            }

            GetIntKeyFormatterImplementor().Implement(info, formatter);
        }

        private void ImplementIntKeyWithSerializationConstructor(in StructSerializationInfo info, TypeDefinition formatter, MethodDefinition serializationConstructor)
        {
            if (serializationConstructor.Parameters.All(x => x.ParameterType.IsMessagePackPrimitive()))
            {
                GetIntKeyAllMessagePackPrimitiveFormatterImplementorWithConstructor().Implement(info, formatter);
            }
            else
            {
                GetIntKeyFormatterImplementorWithConstructor().Implement(info, formatter);
            }
        }
    }
}