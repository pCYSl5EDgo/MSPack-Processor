// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core
{
    public static class ConstructorUtility
    {
        public static MethodDefinition GenerateDefaultConstructor(ModuleDefinition module, SystemObjectHelper helper)
        {
            var ctor = new MethodDefinition(
                ".ctor",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                module.TypeSystem.Void)
            {
                HasThis = true,
                Body =
                {
                    InitLocals = true,
                },
            };

            var processor = ctor.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldarg_0));
            processor.Append(Instruction.Create(OpCodes.Call, helper.Ctor));
            processor.Append(Instruction.Create(OpCodes.Ret));

            return ctor;
        }
    }
}
