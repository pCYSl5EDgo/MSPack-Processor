// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using System;
using Mono.Collections.Generic;

namespace MSPack.Processor.Core.Formatter
{
    public static class GenericsUtility
    {
        public static MethodReference Transplant(MethodReference reference, GenericInstanceType targetType, ModuleImporter importer)
        {
            var answer = new MethodReference(reference.Name, importer.Import(reference.ReturnType), targetType)
            {
                HasThis = reference.HasThis,
            };

            for (var index = 0; index < reference.Parameters.Count; index++)
            {
                var parameter = reference.Parameters[index];
                var item = new ParameterDefinition(parameter.Name, parameter.Attributes, importer.Import(parameter.ParameterType));
                answer.Parameters.Add(item);
                foreach (var attribute in parameter.CustomAttributes)
                {
                    item.CustomAttributes.Add(new CustomAttribute(importer.Import(attribute.Constructor), attribute.GetBlob()));
                }
            }

            return answer;
        }

        public static FieldReference Transplant(FieldReference reference, GenericInstanceType targetType, ModuleImporter importer)
        {
            return new FieldReference(reference.Name, importer.Import(reference.FieldType), targetType);
        }
    }
}