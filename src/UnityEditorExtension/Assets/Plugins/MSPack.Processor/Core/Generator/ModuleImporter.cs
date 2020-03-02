// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public sealed class ModuleImporter
    {
        private readonly ModuleDefinition module;

        public ModuleImporter(ModuleDefinition module)
        {
            this.module = module;
        }

        private GenericInstanceType Import(GenericInstanceType type)
        {
            var answer = new GenericInstanceType(Import(type.ElementType));
            foreach (var argument in type.GenericArguments)
            {
                answer.GenericArguments.Add(Import(argument));
            }

            return answer;
        }

        private ByReferenceType Import(ByReferenceType type) => new ByReferenceType(Import(type.ElementType));

        private ArrayType Import(ArrayType type) => new ArrayType(Import(type.ElementType), type.Rank);

        private PointerType Import(PointerType type) => new PointerType(Import(type.ElementType));

        public TypeReference Import(TypeReference reference)
        {
            if (ReferenceEquals(module, reference.Module))
                return reference;

            switch (reference)
            {
                case ByReferenceType byReference:
                    return Import(byReference);
                case ArrayType arrayType:
                    return Import(arrayType);
                case GenericInstanceType genericInstanceType:
                    return Import(genericInstanceType);
                case PointerType pointerType:
                    return Import(pointerType);
                case GenericParameter genericParameter:
                    return genericParameter;
                case RequiredModifierType requiredModifierType:
                    return new RequiredModifierType(Import(requiredModifierType.ModifierType), Import(requiredModifierType.ElementType));
                default:
                    return module.ImportReference(reference);
            }
        }

        public FieldReference Import(FieldReference reference)
        {
            return ReferenceEquals(module, reference.Module) ? reference : module.ImportReference(reference);
        }

        private GenericInstanceMethod Import(GenericInstanceMethod method)
        {
            var answer = new GenericInstanceMethod(Import(method.ElementMethod));
            foreach (var argument in method.GenericArguments)
            {
                answer.GenericArguments.Add(Import(argument));
            }

            return answer;
        }

        public MethodReference Import(MethodReference reference)
        {
            if (ReferenceEquals(module, reference.Module))
            {
                return reference;
            }

            if (reference is GenericInstanceMethod genericInstanceMethod)
            {
                return Import(genericInstanceMethod);
            }

            return module.ImportReference(reference);
        }
    }
}
