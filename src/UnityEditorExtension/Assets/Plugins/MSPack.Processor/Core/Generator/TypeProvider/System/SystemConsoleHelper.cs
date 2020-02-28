// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemConsoleHelper
    {
        private readonly ModuleDefinition module;
#if CSHARP_8_0_OR_NEWER
        private TypeReference? console;
        private MethodReference? writeLine;
#else
        private TypeReference console;
        private MethodReference writeLine;
#endif

        public SystemConsoleHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference Console
        {
            get
            {
                if (console == null)
                {
                    console = new TypeReference("System", nameof(Console), module, module.TypeSystem.CoreLibrary, false);
                }

                return console;
            }
        }

        public MethodReference WriteLine
        {
            get
            {
                if (writeLine == null)
                {
                    writeLine = new MethodReference(nameof(WriteLine), module.TypeSystem.String, Console)
                    {
                        HasThis = false,
                        Parameters =
                        {
                            new ParameterDefinition("value", ParameterAttributes.None, module.TypeSystem.String),
                        },
                    };
                }

                return writeLine;
            }
        }
    }
}
