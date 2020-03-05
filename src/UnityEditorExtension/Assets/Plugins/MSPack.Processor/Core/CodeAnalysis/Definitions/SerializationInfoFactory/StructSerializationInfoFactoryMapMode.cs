// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public class StructSerializationInfoFactoryMapMode : ISerializationFactory<StructSerializationInfo>
    {
        public StructSerializationInfo Create(TypeDefinition definition)
        {
            if (StructSerializationInfo.TryParse(definition, true, out var answer))
            {
                return answer;
            }

            throw new MessagePackGeneratorResolveFailedException("invalid struct info. type : " + definition.FullName);
        }
    }
}