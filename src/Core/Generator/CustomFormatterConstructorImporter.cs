// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Globalization;
using System.Text;

namespace MSPack.Processor.Core
{
    public sealed class CustomFormatterConstructorImporter
    {
        private readonly TypeReference voidTypeReference;
        private readonly ModuleImporter importer;

        public CustomFormatterConstructorImporter(TypeReference voidTypeReference, ModuleImporter importer)
        {
            this.voidTypeReference = voidTypeReference;
            this.importer = importer;
        }

        public MethodReference Import(in FormatterInfo formatterInfo)
        {
            var formatterType = formatterInfo.FormatterType;

            if (formatterType is TypeDefinition definition)
            {
                return importer.Import(FindConstructor(definition, formatterInfo.FormatterConstructorArguments));
            }

            var answer = new MethodReference(".ctor", voidTypeReference, importer.Import(formatterType))
            {
                HasThis = true,
            };

            for (var index = 0; index < formatterInfo.FormatterConstructorArguments.Length; index++)
            {
                ref var argument = ref formatterInfo.FormatterConstructorArguments[index];
                answer.Parameters.Add(new ParameterDefinition(argument.Type));
            }

            return answer;
        }

        private static MethodDefinition FindConstructor(TypeDefinition typeDefinition, CustomAttributeArgument[] arguments)
        {
            foreach (var method in typeDefinition.Methods)
            {
                if (!method.IsSpecialName || !method.IsRuntimeSpecialName || !method.HasThis || method.Parameters.Count != arguments.Length || method.Name != ".ctor")
                {
                    continue;
                }

                for (var index = 0; index < arguments.Length; index++)
                {
                    ref var argument = ref arguments[index];
                    var param = method.Parameters[index];
                    if (argument.Type.FullName != param.ParameterType.FullName)
                    {
                        goto next;
                    }
                }

                return method;
            next:;
            }

            var buffer = new StringBuilder();
            buffer
                .Append("Formatter type : ")
                .Append(typeDefinition.FullName)
                .Append(" does not have corresponding constructor. It requires ")
                .Append(arguments.Length.ToString(CultureInfo.InvariantCulture));

            for (var index = 0; index < arguments.Length; index++)
            {
                ref var argument = ref arguments[index];
                buffer
                    .Append("\n\tType : ")
                    .Append(argument.Type.FullName)
                    .Append(", Value : ")
                    .Append(argument.Value.ToString());
            }

            throw new MessagePackGeneratorResolveFailedException(buffer.ToString());
        }

    }
}