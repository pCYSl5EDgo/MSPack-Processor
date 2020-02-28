// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;

namespace MSPack.Processor.Core
{
    public static class PrivateAccessEnabler
    {
        public static void EnablePrivateAccess(ModuleDefinition module, Func<IMetadataScope> extensionsScopeFunc)
        {
            if (!HasUnverifiable(module))
            {
                AddUnverifiable(module);
            }

            if (!module.Assembly.HasSecurityDeclarations)
            {
                AddSecurity(module, module.Assembly, extensionsScopeFunc());
            }
        }

        private static void AddSecurity(ModuleDefinition module, AssemblyDefinition assembly, IMetadataScope extensionsScope)
        {
            var securityPermission = new TypeReference("System.Security.Permissions", "SecurityPermissionAttribute", module, extensionsScope, false);
            var declaration = new SecurityDeclaration(SecurityAction.RequestMinimum)
            {
                SecurityAttributes =
                {
                    new SecurityAttribute(securityPermission)
                    {
                        Properties =
                        {
                            new CustomAttributeNamedArgument("SkipVerification", new CustomAttributeArgument(module.TypeSystem.Boolean, true)),
                        },
                    },
                },
            };
            assembly.SecurityDeclarations.Add(declaration);
        }

        private static void AddUnverifiable(ModuleDefinition module)
        {
            var unverifiable = new TypeReference("System.Security", "UnverifiableCodeAttribute", module, module.TypeSystem.CoreLibrary, false);
            var ctor = new MethodReference(".ctor", module.TypeSystem.Void, unverifiable)
            {
                HasThis = true,
            };
            module.CustomAttributes.Add(new CustomAttribute(ctor));
        }

        private static bool HasUnverifiable(ModuleDefinition module)
        {
            foreach (var attribute in module.CustomAttributes)
            {
                if (attribute.AttributeType.FullName == "System.Security.UnverifiableCodeAttribute")
                {
                    return true;
                }
            }
            return false;
        }
    }
}