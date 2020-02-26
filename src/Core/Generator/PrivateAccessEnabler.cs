// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public static class PrivateAccessEnabler
    {
        public static void EnablePrivateAccess(ModuleDefinition module)
        {
            if (!HasUnverifiable(module))
            {
                AddUnverifiable(module);
            }

            /*if (!HasSecurityPermission(module, out bool shouldRewrite, out CustomAttribute? securityPermissionAttribute))
            {
                AddSecurityPermission(module, extensionsScopeFunc());
            }
            else if (shouldRewrite)
            {
                RewriteSecurityPermission(securityPermissionAttribute!);
            }*/
        }

        /*
        private static void RewriteSecurityPermission(CustomAttribute securityPermissionAttribute)
        {
            var customAttributeArguments = securityPermissionAttribute.ConstructorArguments;
            customAttributeArguments[0] = new CustomAttributeArgument(customAttributeArguments[0].Type, (int)8);
            if (securityPermissionAttribute.HasProperties)
            {
                var properties = securityPermissionAttribute.Properties;
                for (var index = 0; index < properties.Count; index++)
                {
                    var property = properties[index];
                    if (property.Name != "SkipVerification")
                    {
                        continue;
                    }

                    properties[index] = new CustomAttributeNamedArgument("SkipVerification", new CustomAttributeArgument(property.Argument.Type, true));
                    return;
                }
            }

            securityPermissionAttribute.Properties.Add(new CustomAttributeNamedArgument("SkipVerification", new CustomAttributeArgument(securityPermissionAttribute.AttributeType.Module.TypeSystem.Boolean, true)));
        }

        private static void AddSecurityPermission(ModuleDefinition module, IMetadataScope extensionsScopeFunc)
        {
            var attribute = new TypeReference("System.Security.Permissions", "SecurityPermissionAttribute", module, extensionsScopeFunc, false);
            var flag = new TypeReference("System.Security.Permissions", "SecurityAction", module, extensionsScopeFunc, true);
            var ctor = new MethodReference(".ctor", module.TypeSystem.Void, attribute)
            {
                HasThis = true,
            };
            module.Assembly.CustomAttributes.Add(new CustomAttribute());
        }

        private static bool HasSecurityPermission(ModuleDefinition module, out bool shouldRewrite, out CustomAttribute? securityPermissionAttribute)
        {
            foreach (var attribute in module.Assembly.CustomAttributes)
            {
                if (attribute.AttributeType.FullName != "System.Security.Permissions.SecurityPermissionAttribute")
                {
                    continue;
                }

                securityPermissionAttribute = attribute;

                if ((int)attribute.ConstructorArguments[0].Value != 8 || !attribute.HasProperties)
                {
                    shouldRewrite = true;
                    return true;
                }

                foreach (var property in attribute.Properties)
                {
                    if (property.Name != "SkipVerification")
                    {
                        continue;
                    }

                    shouldRewrite = !(bool)property.Argument.Value;
                    return true;
                }

                shouldRewrite = true;
                return true;
            }

            securityPermissionAttribute = default;
            shouldRewrite = default;
            return false;
        }*/

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