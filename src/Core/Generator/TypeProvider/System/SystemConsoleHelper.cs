// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemConsoleHelper
    {
        private readonly ModuleDefinition module;
        private TypeReference? console;
        private MethodReference? writeLine;

        public SystemConsoleHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference Console => console ??= new TypeReference("System", nameof(Console), module, module.TypeSystem.CoreLibrary, false);

        public MethodReference WriteLine => writeLine ??= new MethodReference(nameof(WriteLine), module.TypeSystem.String, Console)
        {
            HasThis = false,
            Parameters =
            {
                new ParameterDefinition("value", ParameterAttributes.None, module.TypeSystem.String),
            },
        };
    }
}
