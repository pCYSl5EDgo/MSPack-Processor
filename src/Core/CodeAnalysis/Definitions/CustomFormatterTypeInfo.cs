// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct CustomFormatterTypeInfo
    {
        public CustomFormatterTypeInfo(TypeReference formatterType, CustomAttributeArgument[] formatterConstructorArguments)
        {
            FormatterType = formatterType;
            FormatterConstructorArguments = formatterConstructorArguments;
        }

        private CustomFormatterTypeInfo(int _)
        {
            FormatterType = default;
            FormatterConstructorArguments = Array.Empty<CustomAttributeArgument>();
        }

        public static CustomFormatterTypeInfo Default => new CustomFormatterTypeInfo(0);

#if CSHARP_8_0_OR_NEWER
        public TypeReference? FormatterType { get; }
#else
        public TypeReference FormatterType { get; }
#endif

        public CustomAttributeArgument[] FormatterConstructorArguments { get; }
    }
}