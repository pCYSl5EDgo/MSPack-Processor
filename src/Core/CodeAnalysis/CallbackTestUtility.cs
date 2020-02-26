// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace MSPack.Processor.Core
{
    public static class CallbackTestUtility
    {
        public static bool ShouldCallback(TypeDefinition definition)
        {
            foreach (var implementation in definition.Interfaces)
            {
                if (string.Equals(implementation.InterfaceType.FullName, "MessagePack.IMessagePackSerializationCallbackReceiver", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool NoOperationInBeforeSerializationCallback(TypeDefinition definition, out MethodDefinition method)
        {
            method = definition.Methods.First(x => x.IsPublic && x.Name == "OnBeforeSerialize" && x.HasThis && x.Parameters.Count == 0 && x.ReturnType.FullName == "System.Void");
            return IsNopMethod(method);
        }

        public static bool NoOperationInAfterDeserializationCallback(TypeDefinition definition, out MethodDefinition method)
        {
            method = definition.Methods.First(x => x.IsPublic && x.Name == "OnAfterDeserialize" && x.HasThis && x.Parameters.Count == 0 && x.ReturnType.FullName == "System.Void");
            return IsNopMethod(method);
        }

        private static bool IsNopMethod(MethodDefinition method)
        {
            if (!method.HasBody)
            {
                return false;
            }

            var instructions = method.Body.Instructions;
            if (instructions[instructions.Count - 1].OpCode.Code != Code.Ret)
            {
                return false;
            }

            for (var i = instructions.Count - 2; i >= 0; i--)
            {
                var instruction = instructions[i];
                if (instruction.OpCode.Code != Code.Nop && instruction.OpCode.Code != Code.Ret)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
