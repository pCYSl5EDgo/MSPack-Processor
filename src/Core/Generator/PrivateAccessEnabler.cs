// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public static class PrivateAccessEnabler
    {
        public static void EnablePrivateAccess(ModuleDefinition module)
        {
            foreach (var attribute in module.CustomAttributes)
            {
                if (attribute.AttributeType.FullName == "System.Security.UnverifiableCodeAttribute")
                {
                    return;
                }
            }

            var unverifiable = new TypeReference("System.Security", "UnverifiableCodeAttribute", module, module.TypeSystem.CoreLibrary, false);
            var ctor = new MethodReference(".ctor", module.TypeSystem.Void, unverifiable);
            module.CustomAttributes.Add(new CustomAttribute(ctor));
        }
    }
}