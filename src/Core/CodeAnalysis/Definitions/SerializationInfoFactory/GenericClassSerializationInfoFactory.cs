// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Linq;

namespace MSPack.Processor.Core.Definitions
{
    public class GenericClassSerializationInfoFactory : ISerializationFactory<GenericClassSerializationInfo>
    {
        private readonly GenericInstanceVariationFinder finder;

        public GenericClassSerializationInfoFactory(GenericInstanceVariationFinder finder)
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

            CustomAttributeHelper.IsMessagePackObjectAttribute(messagePackAttribute, out var isKeyAsPropertyName);
            var fieldInfos = MessagePackObjectHelper.CollectFieldInfos(definition, false);
            var propertyInfos = MessagePackObjectHelper.CollectPropertyInfos(definition, isKeyAsPropertyName);
            var (minIntKey, maxIntKey) = MessagePackObjectHelper.FindMinMaxIntKey(fieldInfos, propertyInfos);

            return new GenericClassSerializationInfo(definition, fieldInfos, propertyInfos, minIntKey, maxIntKey, variations);
        }
    }
}