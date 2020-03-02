// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Collections.Generic;

namespace MSPack.Processor.Core.Formatter
{
    public static class GenericsUtility
    {
        public static GenericInstanceType TransplantGenericParameters(TypeDefinition targetType, TypeDefinition baseTypeDefinition, ModuleImporter importer)
        {
            var baseGenericParameters = baseTypeDefinition.GenericParameters;
            FirstAddGenericParameters(targetType, baseGenericParameters);

            var targetTypeGenericParameters = targetType.GenericParameters;
            for (var index = 0; index < targetTypeGenericParameters.Count; index++)
            {
                var formatterGenericParameter = targetTypeGenericParameters[index];
                var baseGenericParameter = baseGenericParameters[index];
                TransplantCustomAttributes(formatterGenericParameter, baseGenericParameter, importer);
                TransplantConstraints(formatterGenericParameter, targetTypeGenericParameters, baseGenericParameter, importer);
            }

            var answer = new GenericInstanceType(importer.Import(baseTypeDefinition));
            foreach (var parameter in targetTypeGenericParameters)
            {
                answer.GenericArguments.Add(parameter);
            }

            return answer;
        }

        private static void TransplantConstraints(GenericParameter formatterGenericParameter, Collection<GenericParameter> formatterGenericParameters, GenericParameter baseGenericParameter, ModuleImporter importer)
        {
            for (var index = 0; index < baseGenericParameter.Constraints.Count; index++)
            {
                var baseConstraint = baseGenericParameter.Constraints[index];
                var baseConstraintType = baseConstraint.ConstraintType;
                if (baseConstraintType is GenericInstanceType genericInstanceType)
                {
                    var transplanted = TransplantInternal(genericInstanceType, formatterGenericParameters, importer);
                    formatterGenericParameter.Constraints.Add(new GenericParameterConstraint(transplanted));
                }
                else
                {
                    formatterGenericParameter.Constraints.Add(new GenericParameterConstraint(importer.Import(baseConstraintType)));
                }
            }
        }

        private static GenericInstanceType TransplantInternal(GenericInstanceType genericInstanceType, Collection<GenericParameter> formatterGenericParameters, ModuleImporter importer)
        {
            var element = importer.Import(genericInstanceType.ElementType);
            var answer = new GenericInstanceType(element);
            foreach (var argument in genericInstanceType.GenericArguments)
            {
                if (argument is GenericInstanceType genericInstanceArgument)
                {
                    answer.GenericArguments.Add(TransplantInternal(genericInstanceArgument, formatterGenericParameters, importer));
                }
                else if (argument is GenericParameter genericParameter)
                {
                    answer.GenericArguments.Add(formatterGenericParameters[genericParameter.Position]);
                }
                else
                {
                    answer.GenericArguments.Add(importer.Import(argument));
                }
            }

            return answer;
        }

        private static void TransplantCustomAttributes(GenericParameter formatterGenericParameter, GenericParameter baseGenericParameter, ModuleImporter importer)
        {
            for (var index = 0; index < baseGenericParameter.CustomAttributes.Count; index++)
            {
                var baseAttribute = baseGenericParameter.CustomAttributes[index];
                var transplant = importer.Import(baseAttribute);
                formatterGenericParameter.CustomAttributes.Add(transplant);
            }
        }

        private static void FirstAddGenericParameters(TypeDefinition formatter, Collection<GenericParameter> genericParameters)
        {
            foreach (var parameter in genericParameters)
            {
                var cloneParameter = new GenericParameter(parameter.Name + "Emulate", formatter)
                {
                    Attributes = parameter.Attributes,
                };
                formatter.GenericParameters.Add(cloneParameter);
            }
        }

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