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
    public sealed class FormatterGenerator
    {
        private readonly TypeDefinition resolver;
        private readonly IFormatterImplementor implementor;

        private readonly Action<string> logger;

        public FormatterGenerator(TypeDefinition resolver, TypeProvider provider, double loadFactor, Action<string> logger)
        {
            this.logger = logger;
            this.resolver = resolver;
            implementor = new ImplementorFacade(provider, loadFactor);
        }

        public int Count(CollectedInfo[] infos)
        {
            var answer = 0;
            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var index = 0; index < infos.Length; index++)
            {
                ref readonly var collectedInfo = ref infos[index];
                logger(collectedInfo.ToString());
                answer += Count(collectedInfo);
            }

            return answer;
        }

        public static int Count(in CollectedInfo info)
        {
            return info.ClassSerializationInfos.Length
                   + info.StructSerializationInfos.Length
                   + info.InterfaceSerializationInfos.Length
                   + info.UnionClassSerializationInfos.Length;
        }

        public FormatterInfo[] Generate(CollectedInfo[] collectedReadOnlySpan)
        {
            if (collectedReadOnlySpan.Length == 0)
            {
                logger(nameof(FormatterGenerator) + " -> 0 length of " + nameof(collectedReadOnlySpan));
                return Array.Empty<FormatterInfo>();
            }

            var count = Count(collectedReadOnlySpan);
            if (count == 0)
            {
                logger(nameof(FormatterGenerator) + " -> total 0 length of " + nameof(collectedReadOnlySpan));
                return Array.Empty<FormatterInfo>();
            }

            logger(nameof(FormatterGenerator) + " -> answer length : " + count);

            var answer = new FormatterInfo[count];
            var index = 0;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < collectedReadOnlySpan.Length; i++)
            {
                Generate(answer, collectedReadOnlySpan[i], ref index);
            }

            return answer;
        }

        private void Generate(FormatterInfo[] formatterInfos, in CollectedInfo collectedInfo, ref int index)
        {
            var collectedInfoClassSerializationInfos = collectedInfo.ClassSerializationInfos;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < collectedInfoClassSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoClassSerializationInfos[i];
                formatterInfos[index] = new FormatterInfo(info.Definition, GetOrAdd(info, index), info.FormatterConstructorArguments);
                index++;
            }

            var collectedInfoStructSerializationInfos = collectedInfo.StructSerializationInfos;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < collectedInfoStructSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoStructSerializationInfos[i];
                formatterInfos[index] = new FormatterInfo(info.Definition, GetOrAdd(info, index), info.FormatterConstructorArguments);
                index++;
            }

            var collectedInfoInterfaceSerializationInfos = collectedInfo.InterfaceSerializationInfos;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < collectedInfoInterfaceSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoInterfaceSerializationInfos[i];
                formatterInfos[index] = new FormatterInfo(info.Definition, GetOrAdd(info, index), Array.Empty<CustomAttributeArgument>());
                index++;
            }

            var collectedInfoUnionClassSerializationInfos = collectedInfo.UnionClassSerializationInfos;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < collectedInfoUnionClassSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoUnionClassSerializationInfos[i];
                formatterInfos[index] = new FormatterInfo(info.Definition, GetOrAdd(info, index), Array.Empty<CustomAttributeArgument>());
                index++;
            }
        }

        private TypeDefinition GetOrAdd(in UnionClassSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "UFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeDefinition GetOrAdd(in UnionInterfaceSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "UFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference GetOrAdd(in ClassSerializationInfo info, int index)
        {
            if (info.FormatterExists)
            {
#if CSHARP_8_0_OR_NEWER
                return info.FormatterDefinition!;
#else
                return info.FormatterType;
#endif
            }

            var formatter = new TypeDefinition(string.Empty, "CFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference GetOrAdd(in StructSerializationInfo info, int index)
        {
            if (info.FormatterExists)
            {
#if CSHARP_8_0_OR_NEWER
                return info.FormatterDefinition!;
#else
                return info.FormatterType;
#endif
            }

            var formatter = new TypeDefinition(string.Empty, "SFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }
    }
}
