// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public readonly struct FormatterInfo
    {
        public readonly TypeDefinition SerializeTypeDefinition;
        public readonly TypeDefinition FormatterTypeDefinition;
        public readonly MethodDefinition FormatterTypeConstructor;
        public readonly CustomAttributeArgument[] FormatterConstructorArguments;

        public FormatterInfo(TypeDefinition serializeTypeDefinition, TypeDefinition formatterTypeDefinition, CustomAttributeArgument[] formatterConstructorArguments)
        {
            SerializeTypeDefinition = serializeTypeDefinition;
            FormatterTypeDefinition = formatterTypeDefinition;
            FormatterConstructorArguments = formatterConstructorArguments;
            foreach (var methodDefinition in FormatterTypeDefinition.Methods)
            {
                if (methodDefinition.Name != ".ctor" || !methodDefinition.IsRuntimeSpecialName || !methodDefinition.IsSpecialName)
                {
                    continue;
                }

                if (methodDefinition.Parameters.Count != formatterConstructorArguments.Length)
                {
                    continue;
                }

                for (var index = 0; index < methodDefinition.Parameters.Count; index++)
                {
                    var methodDefinitionParameter = methodDefinition.Parameters[index];
                    var argument = FormatterConstructorArguments[index];
                    if (!string.Equals(methodDefinitionParameter.ParameterType.FullName, argument.Type.FullName))
                    {
                        goto CONTINUATION;
                    }
                }

                goto SUCCESS;
CONTINUATION:
                continue;
SUCCESS:
                FormatterTypeConstructor = methodDefinition;
                return;
            }

            throw new MessagePackGeneratorResolveFailedException("serialization target type : " + SerializeTypeDefinition.FullName + " , serializer type : " + FormatterTypeDefinition.FullName + " does not have correct constructor.");
        }

        public FormatterInfo(TypeDefinition serializeTypeDefinition, TypeDefinition formatterTypeDefinition)
        {
            SerializeTypeDefinition = serializeTypeDefinition;
            FormatterTypeDefinition = formatterTypeDefinition;
            FormatterConstructorArguments = Array.Empty<CustomAttributeArgument>();

            foreach (var methodDefinition in FormatterTypeDefinition.Methods)
            {
                if (methodDefinition.Name != ".ctor" || !methodDefinition.IsRuntimeSpecialName || !methodDefinition.IsSpecialName)
                {
                    continue;
                }

                if (methodDefinition.Parameters.Count != 0)
                {
                    continue;
                }

                for (var index = 0; index < methodDefinition.Parameters.Count; index++)
                {
                    var methodDefinitionParameter = methodDefinition.Parameters[index];
                    var argument = FormatterConstructorArguments[index];
                    if (!string.Equals(methodDefinitionParameter.ParameterType.FullName, argument.Type.FullName))
                    {
                        goto CONTINUATION;
                    }
                }

                goto SUCCESS;
CONTINUATION:
                continue;
SUCCESS:
                FormatterTypeConstructor = methodDefinition;
                return;
            }

            throw new MessagePackGeneratorResolveFailedException("serialization target type : " + SerializeTypeDefinition.FullName + " , serializer type : " + FormatterTypeDefinition.FullName + " does not have correct constructor.");
        }
    }
}
