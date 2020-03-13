// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        public ImportedTypeReference Import(TypeReference reference)
        {
            if (ReferenceEquals(module, reference.Module))
            {
                if (reference is GenericInstanceType genericInstanceType)
                {
                    return new ImportedTypeReference(Import(genericInstanceType));
                }

                return new ImportedTypeReference(reference);
            }

            switch (reference)
            {
                case ByReferenceType byReference:
                    return new ImportedTypeReference(Import(byReference));
                case ArrayType arrayType:
                    return new ImportedTypeReference(Import(arrayType));
                case GenericInstanceType genericInstanceType:
                    return new ImportedTypeReference(Import(genericInstanceType));
                case PointerType pointerType:
                    return new ImportedTypeReference(Import(pointerType));
                case GenericParameter genericParameter:
                    return new ImportedTypeReference(genericParameter);
                case RequiredModifierType requiredModifierType:
                    return new ImportedTypeReference(new RequiredModifierType(Import(requiredModifierType.ModifierType).Reference, Import(requiredModifierType.ElementType).Reference));
                default:
                    return new ImportedTypeReference(module.ImportReference(reference));
            }
        }

        public CustomAttribute Import(CustomAttribute attribute)
        {
            return new CustomAttribute(Import(attribute.Constructor), attribute.GetBlob());
        }

        public FieldReference Import(FieldReference reference)
        {
            return ReferenceEquals(module, reference.Module) ? reference : module.ImportReference(reference);
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

        private GenericInstanceType Import(GenericInstanceType type)
        {
            if (ShouldImport(type))
            {
                return CreateImport(type);
            }

            return type;
        }

        private bool ShouldImport(GenericInstanceType type)
        {
            if (!ReferenceEquals(type.ElementType.Module, module))
            {
                return true;
            }

            foreach (var argument in type.GenericArguments)
            {
                if (argument is GenericInstanceType genericInstanceType && ShouldImport(genericInstanceType))
                {
                    return true;
                }

                if (!ReferenceEquals(argument.Module, module))
                {
                    return true;
                }
            }

            return false;
        }

        private GenericInstanceType CreateImport(GenericInstanceType type)
        {
            var answer = new GenericInstanceType(Import(type.ElementType).Reference);
            foreach (var argument in type.GenericArguments)
            {
                answer.GenericArguments.Add(Import(argument).Reference);
            }

            return answer;
        }

        private ByReferenceType Import(ByReferenceType type) => new ByReferenceType(Import(type.ElementType).Reference);

        private ArrayType Import(ArrayType type) => new ArrayType(Import(type.ElementType).Reference, type.Rank);

        private PointerType Import(PointerType type) => new PointerType(Import(type.ElementType).Reference);

        private GenericInstanceMethod Import(GenericInstanceMethod method)
        {
            var answer = new GenericInstanceMethod(Import(method.ElementMethod));
            foreach (var argument in method.GenericArguments)
            {
                answer.GenericArguments.Add(Import(argument).Reference);
            }

            return answer;
        }
    }
}
