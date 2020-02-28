// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemInvalidOperationExceptionHelper
    {
        private readonly ModuleDefinition module;
#if CSHARP_8_0_OR_NEWER
        private TypeReference? exception;
        private MethodReference? ctor;
#else
        private TypeReference exception;
        private MethodReference ctor;
#endif

        public SystemInvalidOperationExceptionHelper(ModuleDefinition module)
        {
            this.module = module;
        }

        public TypeReference Exception
        {
            get
            {
                if (exception == null)
                {
                    exception = new TypeReference("System", "InvalidOperationException", module, module.TypeSystem.CoreLibrary, false);
                }

                return exception;
            }
        }

        public MethodReference Ctor
        {
            get
            {
                if (ctor == null)
                {
                    ctor = new MethodReference(".ctor", module.TypeSystem.Void, Exception)
                    {
                        HasThis = true,
                        Parameters =
                        {
                            new ParameterDefinition("message", ParameterAttributes.None, module.TypeSystem.String),
                        },
                    };
                }

                return ctor;
            }
        }
    }
}
