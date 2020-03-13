// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public class UnionInterfaceSerializationInfoFactory : ISerializationFactory<UnionInterfaceSerializationInfo>
    {
        public UnionInterfaceSerializationInfo Create(TypeDefinition definition)
        {
            if (UnionInterfaceSerializationInfo.TryParse(definition, out var answer))
            {
                return answer;
            }

            throw new MessagePackGeneratorResolveFailedException("invalid union interface. type : " + definition.FullName);
        }
    }
}