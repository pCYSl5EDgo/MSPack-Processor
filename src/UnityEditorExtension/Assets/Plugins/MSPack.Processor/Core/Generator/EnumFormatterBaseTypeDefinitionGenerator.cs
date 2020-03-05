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
    public sealed class EnumFormatterBaseTypeDefinitionGenerator
    {
        private readonly TypeDefinition resolver;
        private readonly EnumFormatterImplementor implementor;

        public EnumFormatterBaseTypeDefinitionGenerator(TypeDefinition resolver, TypeProvider provider)
        {
            this.resolver = resolver;
            this.implementor = new EnumFormatterImplementor(provider);
        }

        public FormatterTableItemInfo[] Generate(EnumSerializationInfo[] serializationInfos)
        {
            var answer = new FormatterTableItemInfo[serializationInfos.Length];
            for (var index = 0; index < answer.Length; index++)
            {
                ref readonly var info = ref serializationInfos[index];
                answer[index] = new FormatterTableItemInfo(info.Type, Add(in info, index), Array.Empty<CustomAttributeArgument>());
            }

            return answer;
        }

        private TypeDefinition Add(in EnumSerializationInfo info, int index)
        {
            var formatter = new TypeDefinition(string.Empty, "EFormatter" + index.ToString(CultureInfo.InvariantCulture), TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, resolver.Module.TypeSystem.Object);
            implementor.Implement(info, formatter);
            resolver.NestedTypes.Add(formatter);
            return formatter;
        }
    }
}
