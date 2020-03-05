// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Formatter;
using MSPack.Processor.Core.Provider;
using System;
using System.Globalization;

namespace MSPack.Processor.Core
{
    public sealed class FormatterBaseTypeDefinitionGenerator
    {
        private readonly TypeDefinition resolver;
        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly DataHelper dataHelper;
        private readonly double loadFactor;

#if CSHARP_8_0_OR_NEWER
        private IClassFormatterImplementor? classFormatterImplementor;
        private IStructFormatterImplementor? structFormatterImplementor;
        private IUnionFormatterImplementor? unionFormatterImplementor;
#else
        private IClassFormatterImplementor classFormatterImplementor;
        private IStructFormatterImplementor structFormatterImplementor;
        private IUnionFormatterImplementor unionFormatterImplementor;
#endif

        public FormatterBaseTypeDefinitionGenerator(TypeDefinition resolver, TypeProvider provider, DataHelper dataHelper, double loadFactor)
        {
            this.resolver = resolver;
            module = resolver.Module;
            this.provider = provider;
            this.loadFactor = loadFactor;
            this.dataHelper = dataHelper;
        }

        private IClassFormatterImplementor ClassImplementor
        {
            get
            {
                if (classFormatterImplementor is null)
                {
                    classFormatterImplementor = new ClassImplementorFacade(module, provider, dataHelper);
                }

                return classFormatterImplementor;
            }
        }

        private IStructFormatterImplementor StructImplementor
        {
            get
            {
                if (structFormatterImplementor is null)
                {
                    structFormatterImplementor = new StructImplementorFacade(module, provider, dataHelper);
                }

                return structFormatterImplementor;
            }
        }

        private IUnionFormatterImplementor UnionImplementor
        {
            get
            {
                if (unionFormatterImplementor is null)
                {
                    unionFormatterImplementor = new UnionImplementorFacade(module, provider, loadFactor);
                }

                return unionFormatterImplementor;
            }
        }

        public int Count(CollectedInfo[] infos)
        {
            var answer = 0;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < infos.Length; index++)
            {
                ref readonly var collectedInfo = ref infos[index];
                answer += collectedInfo.ClassSerializationInfos.Length
                    + collectedInfo.InterfaceSerializationInfos.Length
                    + collectedInfo.StructSerializationInfos.Length
                    + collectedInfo.UnionClassSerializationInfos.Length;
            }

            return answer;
        }

        public FormatterTableItemInfo[] Generate(CollectedInfo[] collectedReadOnlySpan)
        {
            var count = Count(collectedReadOnlySpan);
            if (count == 0)
            {
                return Array.Empty<FormatterTableItemInfo>();
            }

            var answer = new FormatterTableItemInfo[count];
            for (int i = 0, index = 0; i < collectedReadOnlySpan.Length; i++)
            {
                Generate(answer, collectedReadOnlySpan[i], ref index);
            }

            return answer;
        }

        private void Generate(FormatterTableItemInfo[] formatterInfos, in CollectedInfo collectedInfo, ref int index)
        {
            var collectedInfoClassSerializationInfos = collectedInfo.ClassSerializationInfos;
            Generate(formatterInfos, ref index, collectedInfoClassSerializationInfos);

            var collectedInfoStructSerializationInfos = collectedInfo.StructSerializationInfos;
            Generate(formatterInfos, ref index, collectedInfoStructSerializationInfos);

            var collectedInfoInterfaceSerializationInfos = collectedInfo.InterfaceSerializationInfos;
            Generate(formatterInfos, ref index, collectedInfoInterfaceSerializationInfos);

            var collectedInfoUnionClassSerializationInfos = collectedInfo.UnionClassSerializationInfos;
            Generate(formatterInfos, ref index, collectedInfoUnionClassSerializationInfos);
        }

        private void Generate(FormatterTableItemInfo[] formatterInfos, ref int index, UnionClassSerializationInfo[] collectedInfoUnionClassSerializationInfos)
        {
            for (var i = 0; i < collectedInfoUnionClassSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoUnionClassSerializationInfos[i];
                formatterInfos[index] = new FormatterTableItemInfo(info.Definition, Add(info, index), Array.Empty<CustomAttributeArgument>());
                index++;
            }
        }

        private void Generate(FormatterTableItemInfo[] formatterInfos, ref int index, UnionInterfaceSerializationInfo[] collectedInfoInterfaceSerializationInfos)
        {
            for (var i = 0; i < collectedInfoInterfaceSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoInterfaceSerializationInfos[i];
                formatterInfos[index] = new FormatterTableItemInfo(info.Definition, Add(info, index), Array.Empty<CustomAttributeArgument>());
                index++;
            }
        }

        private void Generate(FormatterTableItemInfo[] formatterInfos, ref int index, StructSerializationInfo[] collectedInfoStructSerializationInfos)
        {
            for (var i = 0; i < collectedInfoStructSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoStructSerializationInfos[i];
                if (info.CustomFormatter.FormatterType is null)
                {
                    formatterInfos[index] = new FormatterTableItemInfo(info.Definition, Add(info, index), Array.Empty<CustomAttributeArgument>());
                }
                else
                {
                    formatterInfos[index] = new FormatterTableItemInfo(info.Definition, info.CustomFormatter.FormatterType, info.CustomFormatter.FormatterConstructorArguments);
                }
                index++;
            }
        }

        private void Generate(FormatterTableItemInfo[] formatterInfos, ref int index, ClassSerializationInfo[] collectedInfoClassSerializationInfos)
        {
            for (var i = 0; i < collectedInfoClassSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoClassSerializationInfos[i];
                if (info.CustomFormatter.FormatterType is null)
                {
                    formatterInfos[index] = new FormatterTableItemInfo(info.Definition, Add(info, index), Array.Empty<CustomAttributeArgument>());
                }
                else
                {
                    formatterInfos[index] = new FormatterTableItemInfo(info.Definition, info.CustomFormatter.FormatterType, info.CustomFormatter.FormatterConstructorArguments);
                }
                index++;
            }
        }

        private TypeDefinition Add(in UnionClassSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "UFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            UnionImplementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeDefinition Add(in UnionInterfaceSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "UFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            UnionImplementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference Add(in ClassSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "CFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            ClassImplementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference Add(in StructSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "SFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            StructImplementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }
    }
}
