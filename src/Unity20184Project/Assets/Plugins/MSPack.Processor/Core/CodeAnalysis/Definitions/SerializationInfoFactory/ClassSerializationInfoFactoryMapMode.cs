// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public class ClassSerializationInfoFactoryMapMode : ISerializationFactory<ClassSerializationInfo>
    {
        public ClassSerializationInfo Create(TypeDefinition definition)
        {
            if (ClassSerializationInfo.TryParse(definition, true, out var answer))
            {
                return answer;
            }

            throw new MessagePackGeneratorResolveFailedException("invalid class info. type : " + definition.FullName);
        }
    }
}