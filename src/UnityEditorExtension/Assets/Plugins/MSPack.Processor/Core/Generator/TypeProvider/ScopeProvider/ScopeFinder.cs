using System;
using Mono.Cecil;
using Mono.Cecil.Cil;

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

        public bool MatchFullName(string name)
        {
            var matchFullName = Array.IndexOf(findTargetTypeFullNames, name) != -1;
            return matchFullName;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(ModuleDefinition target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(ModuleDefinition target, out IMetadataScope scope)
#endif
        {
            foreach (var targetCustomAttribute in target.CustomAttributes)
            {
                if (TryFind(targetCustomAttribute, out scope))
                {
                    return true;
                }
            }

            foreach (var typeDefinition in target.Types)
            {
                if (TryFind(typeDefinition, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(CustomAttribute target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(CustomAttribute target, out IMetadataScope scope)
#endif
        {
            if (TryFind(target.AttributeType, out scope))
            {
                return true;
            }

            foreach (var constructorArgument in target.ConstructorArguments)
            {
                if (TryFind(constructorArgument, out scope))
                {
                    return true;
                }
            }

            scope = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(CustomAttributeArgument target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out IMetadataScope? scope)
#else
        public bool TryFind(CustomAttributeArgument target, out IMetadataScope scope)
#endif
        {
            var typeReference = target.Type;
            if (!typeReference.IsArray)
            {
                return typeReference.FullName == "System.Type" ? TryFind((TypeReference)target.Value, out scope) : TryFind(typeReference, out scope);
            }

            var values = (CustomAttributeArgument[])target.Value;
            foreach (var argument in values)
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
        public bool TryFind(TypeDefinition target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(TypeDefinition target, out IMetadataScope scope)
#endif
        {
            if (MatchFullName(target.FullName))
            {
                scope = target.Scope;
                return true;
            }

            foreach (var definition in target.NestedTypes)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            foreach (var definition in target.Methods)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            foreach (var definition in target.Fields)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            foreach (var definition in target.CustomAttributes)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            foreach (var definition in target.Properties)
            {
                foreach (var attribute in definition.CustomAttributes)
                {
                    if (TryFind(attribute, out scope))
                    {
                        return true;
                    }
                }
            }

            scope = default;
            return false;
        }
#if CSHARP_8_0_OR_NEWER
        public bool TryFind(FieldDefinition target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(FieldDefinition target, out IMetadataScope scope)
#endif
        {
            if (TryFind(target.FieldType, out scope))
            {
                return true;
            }

            foreach (var definition in target.CustomAttributes)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            return false;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(MethodReference target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out IMetadataScope? scope)
#else
        public bool TryFind(MethodReference target, out IMetadataScope scope)
#endif
        {
            if (target is GenericInstanceMethod instanceMethod)
            {
                return TryFind(instanceMethod, out scope);
            }

            if (TryFind(target.DeclaringType, out scope) || TryFind(target.ReturnType, out scope))
            {
                return true;
            }

            foreach (var parameter in target.Parameters)
            {
                if (TryFind(parameter, out scope))
                {
                    return true;
                }
            }

            return false;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(GenericInstanceMethod target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out IMetadataScope? scope)
#else
        public bool TryFind(GenericInstanceMethod target, out IMetadataScope scope)
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
        public bool TryFind(MethodDefinition target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(MethodDefinition target, out IMetadataScope scope)
#endif
        {
            if (TryFind(target.ReturnType, out scope))
            {
                return true;
            }

            foreach (var definition in target.Parameters)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            foreach (var definition in target.CustomAttributes)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            if (!target.HasBody)
            {
                scope = default;
                return false;
            }

            foreach (var definition in target.Body.Variables)
            {
                if (TryFind(definition, out scope))
                {
                    return true;
                }
            }

            return false;
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryFind(ParameterDefinition target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(ParameterDefinition target, out IMetadataScope scope)
#endif
        {
            if (TryFind(target.ParameterType, out scope))
            {
                return true;
            }

            foreach (var attribute in target.CustomAttributes)
            {
                if (TryFind(attribute, out scope))
                {
                    return true;
                }
            }

            return false;
        }
#if CSHARP_8_0_OR_NEWER
        public bool TryFind(VariableDefinition target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(VariableDefinition target, out IMetadataScope scope)
#endif
        {
            return TryFind(target.VariableType, out scope);
        }
#if CSHARP_8_0_OR_NEWER
        public bool TryFind(GenericInstanceType target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(GenericInstanceType target, out IMetadataScope scope)
#endif
        {
            return TryFind(target.ElementType, out scope);
        }
#if CSHARP_8_0_OR_NEWER
        public bool TryFind(TypeReference target, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out IMetadataScope? scope)
#else
        public bool TryFind(TypeReference target, out IMetadataScope scope)
#endif
        {
            if (MatchFullName(target.FullName))
            {
                scope = target.Scope;
                return true;
            }

            if (target.IsArray)
            {
                return TryFind(((ArrayType)target).ElementType, out scope);
            }

            if (target.IsByReference)
            {
                return TryFind(((ByReferenceType)target).ElementType, out scope);
            }

            if (target.IsPointer)
            {
                return TryFind(((PointerType)target).ElementType, out scope);
            }

            if (target.IsGenericInstance)
            {
                return TryFind((GenericInstanceType)target, out scope);
            }

            scope = default;
            return false;
        }
    }
}
