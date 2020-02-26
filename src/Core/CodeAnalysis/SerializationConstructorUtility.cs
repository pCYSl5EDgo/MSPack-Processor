// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Definitions
{
    public static class SerializationConstructorUtility
    {
        public static MethodDefinition? Find(TypeDefinition type)
        {
            foreach (var methodDefinition in type.Methods)
            {
                if (methodDefinition.IsRuntimeSpecialName && methodDefinition.IsSpecialName && methodDefinition.Name == ".ctor" && methodDefinition.HasCustomAttributes && Examine(methodDefinition))
                {
                    return methodDefinition;
                }
            }

            return default;
        }

        private static bool Examine(MethodDefinition methodDefinition)
        {
            var attributes = methodDefinition.CustomAttributes;
            foreach (var customAttribute in attributes)
            {
                if (customAttribute.AttributeType.FullName == "MessagePack.SerializationConstructorAttribute")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
