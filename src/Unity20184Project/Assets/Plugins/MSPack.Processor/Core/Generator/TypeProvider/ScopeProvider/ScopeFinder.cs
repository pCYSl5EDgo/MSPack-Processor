// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
#if CSHARP_8_0_OR_NEWER
using System.Diagnostics.CodeAnalysis;
#endif

namespace MSPack.Processor.Core.Provider
{
    public sealed class ScopeFinder
    {
        private readonly string[] findTargetTypeFullNames;

        public ScopeFinder(params string[] findTargetTypeFullNames)
        {
            this.findTargetTypeFullNames = findTargetTypeFullNames;
            Array.Sort(this.findTargetTypeFullNames, StringComparer.Ordinal);
        }

        private bool MatchFullName(string name)
        {
            var matchFullName = Array.IndexOf(findTargetTypeFullNames, name) != -1;
            return matchFullName;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(ModuleDefinition target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        public bool TryFind(ModuleDefinition target, out IMetadataScope scope)
#endif
        {
            return TryFindInCustomAttributes(target.CustomAttributes, out scope) || TryFindInTypes(target.Types, out scope);
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(CustomAttribute target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        public bool TryFind(CustomAttribute target, out IMetadataScope scope)
#endif
        {
            return TryFind(target.AttributeType, out scope)
                   || TryFindInCustomAttributeArguments(target.ConstructorArguments, out scope);
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(CustomAttributeArgument target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(CustomAttributeArgument target, out IMetadataScope scope)
#endif
        {
            var typeReference = target.Type;
            if (typeReference.IsArray)
            {
                return TryFindInCustomAttributeArguments((CustomAttributeArgument[])target.Value, out scope);
            }

            return typeReference.FullName == "System.Type" ? TryFind((TypeReference)target.Value, out scope) : TryFind(typeReference, out scope);
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInCustomAttributeArguments(IEnumerable<CustomAttributeArgument> arguments, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInCustomAttributeArguments(IEnumerable<CustomAttributeArgument> arguments, out IMetadataScope scope)
#endif
        {
            foreach (var argument in arguments)
            {
                if (TryFind(argument, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(TypeDefinition target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(TypeDefinition target, out IMetadataScope scope)
#endif
        {
            return TryMatch(target.FullName, target.Scope, out scope)
                   || TryFindInTypes(target.NestedTypes, out scope)
                   || (TryFindInMethods(target.Methods, out scope)
                   || TryFindInFields(target.Fields, out scope)
                   || TryFindInCustomAttributes(target.CustomAttributes, out scope)
                   || TryFindInProperties(target.Properties, out scope));
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryMatch(string matchFullName, IMetadataScope matchScope, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryMatch(string matchFullName, IMetadataScope matchScope, out IMetadataScope scope)
#endif
        {
            if (MatchFullName(matchFullName))
            {
                scope = matchScope;
                return true;
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInMethods(Collection<MethodDefinition> definitions, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInMethods(Collection<MethodDefinition> definitions, out IMetadataScope scope)
#endif
        {
            foreach (var definition in definitions)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInFields(Collection<FieldDefinition> definitions, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInFields(Collection<FieldDefinition> definitions, out IMetadataScope scope)
#endif
        {
            foreach (var definition in definitions)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInProperties(Collection<PropertyDefinition> definitions, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInProperties(Collection<PropertyDefinition> definitions, out IMetadataScope scope)
#endif
        {
            foreach (var definition in definitions)
            {
                if (TryFindInCustomAttributes(definition.CustomAttributes, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInTypes(Collection<TypeDefinition> definitions, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInTypes(Collection<TypeDefinition> definitions, out IMetadataScope scope)
#endif
        {
            foreach (var type in definitions)
            {
                if (TryFind(type, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInCustomAttributes(Collection<CustomAttribute> customAttributes, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInCustomAttributes(Collection<CustomAttribute> customAttributes, out IMetadataScope scope)
#endif
        {
            foreach (var definition in customAttributes)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(FieldDefinition target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(FieldDefinition target, out IMetadataScope scope)
#endif
        {
            return TryFind(target.FieldType, out scope) || TryFindInCustomAttributes(target.CustomAttributes, out scope);
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(MethodReference target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(MethodReference target, out IMetadataScope scope)
#endif
        {
            if (target is GenericInstanceMethod instanceMethod)
            {
                return TryFind(instanceMethod, out scope);
            }

            return TryFind(target.DeclaringType, out scope)
                   || TryFind(target.ReturnType, out scope)
                   || TryFindInParameterDefinitions(target.Parameters, out scope);
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(GenericInstanceMethod target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(GenericInstanceMethod target, out IMetadataScope scope)
#endif
        {
            if (TryFind(target.ElementMethod, out scope))
            {
                return true;
            }

            foreach (var argument in target.GenericArguments)
            {
                if (TryFind(argument, out scope))
                {
                    return true;
                }
            }

            return false;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(MethodDefinition target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        public bool TryFind(MethodDefinition target, out IMetadataScope scope)
#endif
        {
            return TryFind(target.ReturnType, out scope)
                   || (TryFindInParameterDefinitions(target.Parameters, out scope)
                   || TryFindInCustomAttributes(target.CustomAttributes, out scope)
                   || target.HasBody
                   && TryFindInVariableDefinitions(target.Body.Variables, out scope));
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInParameterDefinitions(Collection<ParameterDefinition> definitions, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInParameterDefinitions(Collection<ParameterDefinition> definitions, out IMetadataScope scope)
#endif
        {
            foreach (var definition in definitions)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFindInVariableDefinitions(Collection<VariableDefinition> definitions, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFindInVariableDefinitions(Collection<VariableDefinition> definitions, out IMetadataScope scope)
#endif
        {
            foreach (var definition in definitions)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(ParameterDefinition target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(ParameterDefinition target, out IMetadataScope scope)
#endif
        {
            return TryFind(target.ParameterType, out scope) || TryFindInCustomAttributes(target.CustomAttributes, out scope);
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(VariableDefinition target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(VariableDefinition target, out IMetadataScope scope)
#endif
        {
            return TryFind(target.VariableType, out scope);
        }

#if CSHARP_8_0_OR_NEWER
        private bool TryFind(TypeReference target, [NotNullWhen(true)] out IMetadataScope? scope)
#else
        private bool TryFind(TypeReference target, out IMetadataScope scope)
#endif
        {
            while (true)
            {
                if (TryMatch(target.FullName, target.Scope, out scope))
                {
                    return true;
                }

                if (target.IsGenericInstance)
                {
                    target = ((GenericInstanceType)target).ElementType;
                    continue;
                }

                if (target.IsArray)
                {
                    target = ((ArrayType)target).ElementType;
                    continue;
                }

                if (target.IsByReference)
                {
                    target = ((ByReferenceType)target).ElementType;
                    continue;
                }

                if (target.IsPointer)
                {
                    target = ((PointerType)target).ElementType;
                    continue;
                }

                scope = default;
                return false;
            }
        }
    }
}
