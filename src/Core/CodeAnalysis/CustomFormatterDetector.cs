// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;

namespace MSPack.Processor.Core.Definitions
{
    public static class CustomFormatterDetector
    {
        public static CustomFormatterTypeInfo Detect(TypeReference ownerTypeReference, CustomAttribute messagePackFormatterAttribute)
        {
            var arguments = messagePackFormatterAttribute.ConstructorArguments;
            TypeReference customFormatterTypeReference;
            CustomAttributeArgument[] customAttributeArguments;
            switch (arguments.Count)
            {
                case 1:
                    customFormatterTypeReference = (TypeReference)arguments[0].Value;
                    customAttributeArguments = Array.Empty<CustomAttributeArgument>();
                    return new CustomFormatterTypeInfo(customFormatterTypeReference, customAttributeArguments);
                case 2:
                    customFormatterTypeReference = (TypeReference)arguments[0].Value;
                    var argumentObjectArray = (CustomAttributeArgument[])arguments[1].Value;
                    customAttributeArguments = new CustomAttributeArgument[argumentObjectArray.Length];
                    for (var index = 0; index < argumentObjectArray.Length; index++)
                    {
                        ref var argument = ref argumentObjectArray[index];
                        customAttributeArguments[index] = (CustomAttributeArgument)argument.Value;
                    }
                    return new CustomFormatterTypeInfo(customFormatterTypeReference, customAttributeArguments);
                default:
                    throw new MessagePackGeneratorResolveFailedException("MessagePackFormatterAttribute should have 1 or 2 constructor argument(s). type : " + ownerTypeReference.FullName);
            }
        }
    }
}