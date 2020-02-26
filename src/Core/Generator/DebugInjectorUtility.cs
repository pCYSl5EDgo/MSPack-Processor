// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using MSPack.Processor.Core.Provider;
using Mono.Cecil.Cil;

namespace MSPack.Processor.Core
{
    public static class DebugInjectorUtility
    {
        private static TypeProvider? provider;

        public static void SetProvider(TypeProvider provider)
        {
            DebugInjectorUtility.provider = provider;
        }

        public static void WriteLine(this ILProcessor processor, string value)
        {
            if (provider is null)
            {
                throw new NullReferenceException();
            }

            processor.Append(Instruction.Create(OpCodes.Ldstr, value));
            processor.Append(Instruction.Create(OpCodes.Call, provider.SystemConsoleHelper.WriteLine));
        }

        public static void Throw(this ILProcessor processor, string message)
        {
            if (provider is null)
            {
                throw new NullReferenceException();
            }

            processor.Append(Instruction.Create(OpCodes.Ldstr, message));
            processor.Append(Instruction.Create(OpCodes.Newobj, provider.SystemExceptionHelper.Ctor));
            processor.Append(Instruction.Create(OpCodes.Throw));
        }
    }
}
