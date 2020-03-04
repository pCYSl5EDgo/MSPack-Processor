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

        public FormatterGenerator(TypeDefinition resolver, TypeProvider provider, double loadFactor)
        {
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
                answer += collectedInfo.Count;
            }

            return answer;
        }

        public FormatterInfo[] Generate(CollectedInfo[] collectedReadOnlySpan)
        {
            if (collectedReadOnlySpan.Length == 0)
            {
                return Array.Empty<FormatterInfo>();
            }

            var count = Count(collectedReadOnlySpan);
            if (count == 0)
            {
                return Array.Empty<FormatterInfo>();
            }

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

            var collectedInfoGenericClassSerializationInfos = collectedInfo.GenericClassSerializationInfos;
            for (var i = 0; i < collectedInfoGenericClassSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoGenericClassSerializationInfos[i];
                formatterInfos[index] = new FormatterInfo(info.Definition, GetOrAdd(info, index), Array.Empty<CustomAttributeArgument>());
                index++;
            }

            var collectedInfoGenericStructSerializationInfos = collectedInfo.GenericStructSerializationInfos;
            for (var i = 0; i < collectedInfoGenericStructSerializationInfos.Length; i++)
            {
                ref readonly var info = ref collectedInfoGenericStructSerializationInfos[i];
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
            if (!(info.FormatterType is null))
            {
                return info.FormatterType;
            }

            var formatter = new TypeDefinition(string.Empty, "CFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference GetOrAdd(in GenericClassSerializationInfo info, int index)
        {
            if (!(info.FormatterType is null))
            {
                return info.FormatterType;
            }

            var formatter = new TypeDefinition(string.Empty, "GCFormatter" + index.ToString(CultureInfo.InvariantCulture) + "`" + info.Definition.GenericParameters.Count.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference GetOrAdd(in GenericStructSerializationInfo info, int index)
        {
            if (!(info.FormatterType is null))
            {
                return info.FormatterType;
            }

            var formatter = new TypeDefinition(string.Empty, "GSFormatter" + index.ToString(CultureInfo.InvariantCulture) + "`" + info.Definition.GenericParameters.Count.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }

        private TypeReference GetOrAdd(in StructSerializationInfo info, int index)
        {
            if (!(info.FormatterType is null))
            {
                return info.FormatterType;
            }

            var formatter = new TypeDefinition(string.Empty, "SFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }
    }
}
