// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemExceptionHelper
    {
        private readonly ModuleDefinition module;
        private TypeReference? exception;
        private MethodReference? ctor;

        public SystemExceptionHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference Exception => exception ??= new TypeReference("System", nameof(Exception), module, module.TypeSystem.CoreLibrary, false);

        public MethodReference Ctor => ctor ??= new MethodReference(".ctor", module.TypeSystem.Void, Exception)
        {
            HasThis = true,
            Parameters =
            {
                new ParameterDefinition("message", ParameterAttributes.None, module.TypeSystem.String),
            },
        };
    }
}
