// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Embed;
using MSPack.Processor.Core.Formatter;
using MSPack.Processor.Core.Provider;
using System;
using System.Text;

namespace MSPack.Processor.Core
{
    public sealed class GenericFormatterBaseTypeDefinitionGenerator
    {
        private readonly ModuleDefinition module;
        private readonly TypeDefinition resolver;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;
        private readonly AutomataEmbeddingHelper automataHelper;

        public GenericFormatterBaseTypeDefinitionGenerator(TypeDefinition resolver, TypeProvider provider, DataHelper dataHelper, AutomataEmbeddingHelper automataHelper)
        {
            this.resolver = resolver;
            this.provider = provider;
            this.dataHelper = dataHelper;
            this.automataHelper = automataHelper;
            this.module = resolver.Module;
        }

#if CSHARP_8_0_OR_NEWER
        private IGenericClassFormatterImplementor? genericClassFormatterImplementor;
        private IGenericStructFormatterImplementor? genericStructFormatterImplementor;
#else
        private IGenericClassFormatterImplementor genericClassFormatterImplementor;
        private IGenericStructFormatterImplementor genericStructFormatterImplementor;
#endif

        private IGenericClassFormatterImplementor GenericClassImplementor
        {
            get
            {
                if (genericClassFormatterImplementor is null)
                {
                    genericClassFormatterImplementor = new GenericClassImplementorFacade(module, provider, dataHelper, automataHelper);
                }

                return genericClassFormatterImplementor;
            }
        }

        private IGenericStructFormatterImplementor GenericStructImplementor
        {
            get
            {
                if (genericStructFormatterImplementor is null)
                {
                    genericStructFormatterImplementor = new GenericStructImplementorFacade(module, provider, dataHelper, automataHelper);
                }

                return genericStructFormatterImplementor;
            }
        }

        private static int Count(CollectedInfo[] collectedInfos)
        {
            var count = 0;
            for (var index = 0; index < collectedInfos.Length; index++)
            {
                ref readonly var collectedInfo = ref collectedInfos[index];
                for (var i = 0; i < collectedInfo.GenericClassSerializationInfos.Length; i++)
                {
                    ref readonly var info = ref collectedInfo.GenericClassSerializationInfos[i];
                    count += info.DefinitionGenericInstanceVariations.Length;
                }

                for (var i = 0; i < collectedInfo.GenericStructSerializationInfos.Length; i++)
                {
                    ref readonly var info = ref collectedInfo.GenericStructSerializationInfos[i];
                    count += info.DefinitionGenericInstanceVariations.Length;
                }
            }

            return count;
        }

        public FormatterTableItemInfo[] Generate(CollectedInfo[] collectedInfos)
        {
            var count = Count(collectedInfos);
            if (count == 0)
            {
                return Array.Empty<FormatterTableItemInfo>();
            }

            var answer = new FormatterTableItemInfo[count];
            for (int i = 0, index = 0; i < collectedInfos.Length; i++)
            {
                ref readonly var collectedInfo = ref collectedInfos[i];
                Generate(answer, ref index, collectedInfo.GenericClassSerializationInfos);
                Generate(answer, ref index, collectedInfo.GenericStructSerializationInfos);
            }

            return answer;
        }

        private void Generate(FormatterTableItemInfo[] formatterInfos, ref int index, GenericStructSerializationInfo[] collectedInfoGenericStructSerializationInfos)
        {
            for (var i = 0; i < collectedInfoGenericStructSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoGenericStructSerializationInfos[i];
                FormatterTableItemInfo baseFormatter;
                if (info.CustomFormatter.FormatterType is null)
                {
                    baseFormatter = new FormatterTableItemInfo(info.Definition, Add(info, index), Array.Empty<CustomAttributeArgument>());
                }
                else
                {
                    baseFormatter = new FormatterTableItemInfo(info.Definition, info.CustomFormatter.FormatterType, info.CustomFormatter.FormatterConstructorArguments);
                }

                foreach (var serializableTypeVariation in info.DefinitionGenericInstanceVariations)
                {
                    var genericFormatter = new GenericInstanceType(baseFormatter.FormatterType);
                    foreach (var argument in serializableTypeVariation.GenericArguments)
                    {
                        genericFormatter.GenericArguments.Add(argument);
                    }
                    formatterInfos[index++] = new FormatterTableItemInfo(serializableTypeVariation, genericFormatter, baseFormatter.FormatterConstructorArguments);
                }
            }
        }

        private void Generate(FormatterTableItemInfo[] formatterInfos, ref int index, GenericClassSerializationInfo[] collectedInfoGenericClassSerializationInfos)
        {
            for (var i = 0; i < collectedInfoGenericClassSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoGenericClassSerializationInfos[i];
                FormatterTableItemInfo baseFormatter;
                if (info.CustomFormatter.FormatterType is null)
                {
                    baseFormatter = new FormatterTableItemInfo(info.Definition, Add(info, index), Array.Empty<CustomAttributeArgument>());
                }
                else
                {
                    baseFormatter = new FormatterTableItemInfo(info.Definition, info.CustomFormatter.FormatterType, info.CustomFormatter.FormatterConstructorArguments);
                }

                foreach (var serializableTypeVariation in info.DefinitionGenericInstanceVariations)
                {
                    var genericFormatter = new GenericInstanceType(baseFormatter.FormatterType);
                    foreach (var argument in serializableTypeVariation.GenericArguments)
                    {
                        genericFormatter.GenericArguments.Add(argument);
                    }
                    formatterInfos[index++] = new FormatterTableItemInfo(serializableTypeVariation, genericFormatter, baseFormatter.FormatterConstructorArguments);
                }
            }
        }

        private static string CalcName(string namePrefix, int index, TypeReference type)
        {
            var builder = new StringBuilder();
            builder.Append(namePrefix).Append(index).Append('_').Append(type.Name);
            return builder.ToString();
        }

        private TypeReference Add(in GenericClassSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(
                string.Empty,
                CalcName("GCFormatter", index, info.Definition),
                TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                resolver.Module.TypeSystem.Object);
            GenericClassImplementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference Add(in GenericStructSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(
                string.Empty,
                CalcName("GCFormatter", index, info.Definition),
                TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                resolver.Module.TypeSystem.Object);
            GenericStructImplementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }
    }
}