// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Formatter;
using MSPack.Processor.Core.Provider;
using System.Globalization;

namespace MSPack.Processor.Core
{
    public sealed class EnumFormatterGenerator
    {
        private readonly TypeDefinition resolver;
        private readonly EnumFormatterImplementor implementor;

        public EnumFormatterGenerator(TypeDefinition resolver, TypeProvider provider)
        {
            this.resolver = resolver;
            this.implementor = new EnumFormatterImplementor(provider);
        }

        public FormatterInfo[] Generate(EnumSerializationInfo[] serializationInfos)
        {
            var answer = new FormatterInfo[serializationInfos.Length];
            for (var index = 0; index < answer.Length; index++)
            {
                ref readonly var info = ref serializationInfos[index];
                answer[index] = new FormatterInfo(info.Type, GetOrAdd(in info, index));
            }

            return answer;
        }

        private TypeDefinition GetOrAdd(in EnumSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "EFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }
    }
}
