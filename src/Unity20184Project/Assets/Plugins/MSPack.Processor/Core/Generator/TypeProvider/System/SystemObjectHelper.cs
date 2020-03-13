// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemObjectHelper
    {
        private readonly ModuleDefinition module;

        public MethodReference Ctor { get; }

        public new MethodReference GetHashCode { get; }

        public new MethodReference GetType { get; }

        public SystemObjectHelper(ModuleDefinition module, SystemTypeHelper typeHelper)
        {
            this.module = module;
            Ctor = new MethodReference(".ctor", module.TypeSystem.Void, module.TypeSystem.Object)
            {
                HasThis = true,
            };
            GetHashCode = new MethodReference(nameof(GetHashCode), module.TypeSystem.Int32, module.TypeSystem.Object)
            {
                HasThis = true,
            };
            GetType = new MethodReference(nameof(GetType), typeHelper.Type, module.TypeSystem.Object)
            {
                HasThis = true,
            };
        }
    }
}
