// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public class UnionClassSerializationInfoFactory : ISerializationFactory<UnionClassSerializationInfo>
    {
        public UnionClassSerializationInfo Create(TypeDefinition definition)
        {
            if (UnionClassSerializationInfo.TryParse(definition, out var answer))
            {
                return answer;
            }

            throw new MessagePackGeneratorResolveFailedException("invalid union class. type : " + definition.FullName);
        }
    }
}