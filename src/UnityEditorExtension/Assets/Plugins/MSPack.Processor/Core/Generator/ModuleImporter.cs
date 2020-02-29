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

        public TypeReference Import(TypeReference reference)
        {
            return ReferenceEquals(module, reference.Module) ? reference : module.ImportReference(reference);
        }

        public FieldReference Import(FieldReference reference)
        {
            return ReferenceEquals(module, reference.Module) ? reference : module.ImportReference(reference);
        }

        public MethodReference Import(MethodReference reference)
        {
            return ReferenceEquals(module, reference.Module) ? reference : module.ImportReference(reference);
        }
    }
}
