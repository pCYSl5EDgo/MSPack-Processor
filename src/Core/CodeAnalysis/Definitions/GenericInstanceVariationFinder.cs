// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace MSPack.Processor.Core.Definitions
{
    public class GenericInstanceVariationFinder
    {
        public IList<GenericInstanceType> Find(TypeDefinition definition)
        {
            if (!definition.HasCustomAttributes)
            {
                return Array.Empty<GenericInstanceType>();
            }

            var list = new List<GenericInstanceType>();
            foreach (var attribute in definition.CustomAttributes)
            {
                if (attribute.AttributeType.FullName != "MSPack.Processor.Annotation.GenericArgumentAttribute")
                {
                    continue;
                }

                if (!(attribute.ConstructorArguments[0].Value is GenericInstanceType variation))
                {
                    continue;
                }

                if (variation.ElementType.FullName != definition.FullName)
                {
                    throw new MessagePackGeneratorResolveFailedException("Generic instance variation must be ");
                }

                list.Add(variation);
            }

            return list;
        }
    }
}