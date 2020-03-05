// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Linq;

namespace MSPack.Processor.Core.Definitions
{
    public class GenericClassSerializationInfoFactoryMapMode : ISerializationFactory<GenericClassSerializationInfo>
    {
        private readonly GenericInstanceVariationFinder finder;

        public GenericClassSerializationInfoFactoryMapMode(GenericInstanceVariationFinder finder)
        {
            this.finder = finder;
        }

        public GenericClassSerializationInfo Create(TypeDefinition definition)
        {
            var messagePackAttribute = definition.CustomAttributes.SingleOrDefault(CustomAttributeHelper.IsMessagePackObjectAttribute);
            if (messagePackAttribute is null)
            {
                throw new MessagePackGeneratorResolveFailedException("invalid generic class type. type : " + definition.FullName);
            }

            var variations = finder.Find(definition).ToArray();
            var customFormatter = definition.CustomAttributes.SingleOrDefault(CustomAttributeHelper.IsMessagePackFormatterAttribute);
            if (!(customFormatter is null))
            {
                return new GenericClassSerializationInfo(definition, CustomFormatterDetector.Detect(definition, customFormatter), variations);
            }

            var fieldInfos = MessagePackObjectHelper.CollectFieldInfos(definition, true);
            var propertyInfos = MessagePackObjectHelper.CollectPropertyInfos(definition, true);
            var (minIntKey, maxIntKey) = MessagePackObjectHelper.FindMinMaxIntKey(fieldInfos, propertyInfos);

            return new GenericClassSerializationInfo(definition, fieldInfos, propertyInfos, minIntKey, maxIntKey, variations);
        }
    }
}