// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Collections.Generic;

namespace MSPack.Processor.Core.Formatter
{
    public static class GenericsUtility
    {
        /// <summary>
        /// Transplants generic parameters from serialize target type to formatter type.
        /// </summary>
        /// <param name="formatterTypeDefinition">Destination formatter type.</param>
        /// <param name="serializeTargetTypeDefinition">Source serializable type.</param>
        /// <param name="importer">Module importer.</param>
        /// <param name="genericInstanceFormatter">Generic instance of formatter type.</param>
        /// <param name="genericInstanceSerializeTarget">Generic instance of serializable type.</param>
        public static void TransplantGenericParameters(TypeDefinition formatterTypeDefinition, TypeDefinition serializeTargetTypeDefinition, ModuleImporter importer, out GenericInstanceType genericInstanceFormatter, out GenericInstanceType genericInstanceSerializeTarget)
        {
            var baseGenericParameters = serializeTargetTypeDefinition.GenericParameters;
            FirstAddGenericParameters(formatterTypeDefinition, baseGenericParameters);

            var targetTypeGenericParameters = formatterTypeDefinition.GenericParameters;
            for (var index = 0; index < targetTypeGenericParameters.Count; index++)
            {
                var formatterGenericParameter = targetTypeGenericParameters[index];
                var baseGenericParameter = baseGenericParameters[index];
                TransplantCustomAttributes(formatterGenericParameter, baseGenericParameter, importer);
                TransplantConstraints(formatterGenericParameter, targetTypeGenericParameters, baseGenericParameter, importer);
            }

            genericInstanceFormatter = new GenericInstanceType(formatterTypeDefinition);
            genericInstanceSerializeTarget = new GenericInstanceType(importer.Import(serializeTargetTypeDefinition).Reference);
            foreach (var parameter in formatterTypeDefinition.GenericParameters)
            {
                genericInstanceFormatter.GenericArguments.Add(parameter);
                genericInstanceSerializeTarget.GenericArguments.Add(parameter);
            }
        }

        private static void TransplantConstraints(GenericParameter formatterGenericParameter, Collection<GenericParameter> formatterGenericParameters, GenericParameter baseGenericParameter, ModuleImporter importer)
        {
            for (var index = 0; index < baseGenericParameter.Constraints.Count; index++)
            {
#if UNITY_2018_4_OR_NEWER
                var baseConstraintType = baseGenericParameter.Constraints[index];
                if (baseConstraintType is GenericInstanceType genericInstanceType)
                {
                    var transplanted = TransplantInternal(genericInstanceType, formatterGenericParameters, importer);
                    formatterGenericParameter.Constraints.Add(transplanted);
                }
                else
                {
                    formatterGenericParameter.Constraints.Add(importer.Import(baseConstraintType));
                }
#else
                var baseConstraint = baseGenericParameter.Constraints[index];
                var baseConstraintType = baseConstraint.ConstraintType;
                if (baseConstraintType is GenericInstanceType genericInstanceType)
                {
                    var transplanted = TransplantInternal(genericInstanceType, formatterGenericParameters, importer);
                    formatterGenericParameter.Constraints.Add(new GenericParameterConstraint(transplanted));
                }
                else
                {
                    formatterGenericParameter.Constraints.Add(new GenericParameterConstraint(importer.Import(baseConstraintType).Reference));
                }
#endif
            }
        }

        private static GenericInstanceType TransplantInternal(GenericInstanceType genericInstanceType, Collection<GenericParameter> formatterGenericParameters, ModuleImporter importer)
        {
            var element = importer.Import(genericInstanceType.ElementType);
            var answer = new GenericInstanceType(element.Reference);
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
                    answer.GenericArguments.Add(importer.Import(argument).Reference);
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
            var answer = new MethodReference(reference.Name, importer.Import(reference.ReturnType).Reference, targetType)
            {
                HasThis = reference.HasThis,
            };

            for (var index = 0; index < reference.Parameters.Count; index++)
            {
                var parameter = reference.Parameters[index];
                var item = new ParameterDefinition(parameter.Name, parameter.Attributes, importer.Import(parameter.ParameterType).Reference);
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
            var importedFieldType = importer.Import(reference.FieldType).Reference;
            var importedDeclaringType = importer.Import(targetType).Reference;
            var transplant = new FieldReference(reference.Name, importedFieldType, importedDeclaringType);
            return transplant;
        }
    }
}