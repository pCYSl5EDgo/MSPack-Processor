// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System.Linq;

namespace MSPack.Processor.Core.Definitions
{
    public class GenericStructSerializationInfoFactory : ISerializationFactory<GenericStructSerializationInfo>
    {
        private readonly GenericInstanceVariationFinder finder;

        public GenericStructSerializationInfoFactory(GenericInstanceVariationFinder finder)
        {
            this.finder = finder;
        }

        public GenericStructSerializationInfo Create(TypeDefinition definition)
        {
            var messagePackAttribute = definition.CustomAttributes.SingleOrDefault(CustomAttributeHelper.IsMessagePackObjectAttribute);

            if (messagePackAttribute is null)
            {
                throw new MessagePackGeneratorResolveFailedException("invalid generic struct type. type : " + definition.FullName);
            }

            var serializationConstructor = SerializationConstructorUtility.Find(definition);
            var variations = finder.Find(definition).ToArray();
            var customFormatter = definition.CustomAttributes.SingleOrDefault(CustomAttributeHelper.IsMessagePackFormatterAttribute);
            if (!(customFormatter is null))
            {
                var customFormatterTypeInfo = CustomFormatterDetector.Detect(definition, customFormatter);
                return new GenericStructSerializationInfo(definition, customFormatterTypeInfo, serializationConstructor, variations);
            }

            CustomAttributeHelper.IsMessagePackObjectAttribute(messagePackAttribute, out var isKeyAsPropertyName);
            var fieldInfos = MessagePackObjectHelper.CollectFieldInfos(definition, false);
            var propertyInfos = MessagePackObjectHelper.CollectPropertyInfos(definition, isKeyAsPropertyName);
            var (minIntKey, maxIntKey) = MessagePackObjectHelper.FindMinMaxIntKey(fieldInfos, propertyInfos);

            return new GenericStructSerializationInfo(definition, fieldInfos, propertyInfos, minIntKey, maxIntKey, serializationConstructor, variations);
        }
    }
}