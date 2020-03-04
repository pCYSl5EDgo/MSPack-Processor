// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core.Formatter
{
    public sealed class ClassImplementorFacade : IClassFormatterImplementor
    {
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;

        public ClassImplementorFacade(ModuleDefinition module, TypeProvider provider, DataHelper dataHelper)
        {
            this.module = module;
            this.provider = provider;
            this.dataHelper = dataHelper;
        }

#if CSHARP_8_0_OR_NEWER
        private ClassIntKeyFormatterImplementor? intKeyFormatterImplementor;
        private ClassIntKeyAllMessagePackPrimitiveFormatterImplementor? intKeyAllMessagePackPrimitiveFormatterImplementor;
        private ClassStringKeyFormatterImplementor? stringKeyFormatterImplementor;
        private ClassStringKeyAllMessagePackPrimitiveImplementor? stringKeyAllMessagePackPrimitiveImplementor;
#else
        private ClassIntKeyFormatterImplementor classIntKeyFormatterImplementor;
        private ClassIntKeyAllMessagePackPrimitiveFormatterImplementor classIntKeyAllMessagePackPrimitiveFormatterImplementor;
        private ClassStringKeyFormatterImplementor classStringKeyFormatterImplementor;
        private ClassStringKeyAllMessagePackPrimitiveImplementor classStringKeyAllMessagePackPrimitiveImplementor;
#endif

        private ClassIntKeyFormatterImplementor GetIntKey()
        {
            if (intKeyFormatterImplementor is null)
            {
                intKeyFormatterImplementor = new ClassIntKeyFormatterImplementor(module, provider);
            }

            return intKeyFormatterImplementor;
        }

        private ClassIntKeyAllMessagePackPrimitiveFormatterImplementor GetIntKeyAllPrimitive()
        {
            if (intKeyAllMessagePackPrimitiveFormatterImplementor is null)
            {
                intKeyAllMessagePackPrimitiveFormatterImplementor = new ClassIntKeyAllMessagePackPrimitiveFormatterImplementor(module, provider);
            }

            return intKeyAllMessagePackPrimitiveFormatterImplementor;
        }

        private ClassStringKeyFormatterImplementor GetStringKey()
        {
            if (stringKeyFormatterImplementor is null)
            {
                stringKeyFormatterImplementor = new ClassStringKeyFormatterImplementor(module, provider, dataHelper);
            }

            return stringKeyFormatterImplementor;
        }

        private ClassStringKeyAllMessagePackPrimitiveImplementor GetStringKeyAllPrimitive()
        {
            if (stringKeyAllMessagePackPrimitiveImplementor is null)
            {
                stringKeyAllMessagePackPrimitiveImplementor = new ClassStringKeyAllMessagePackPrimitiveImplementor(module, provider, dataHelper);
            }

            return stringKeyAllMessagePackPrimitiveImplementor;
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
                GetStringKeyAllPrimitive().Implement(info, formatter);
                return;
            }

            GetStringKey().Implement(info, formatter);
        }

        private void ImplementIntKey(in ClassSerializationInfo info, TypeDefinition formatter)
        {
            if (info.AreAllMessagePackPrimitive)
            {
                GetIntKeyAllPrimitive().Implement(info, formatter);
                return;
            }

            GetIntKey().Implement(info, formatter);
        }
    }
}