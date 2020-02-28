// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public sealed class FormatterResolverFinder
    {
        public TypeDefinition Find(ModuleDefinition module, string defaultResolverName)
        {
            if (!string.IsNullOrWhiteSpace(defaultResolverName))
            {
                var resolver = module.GetType(defaultResolverName);
                if (!(resolver is null))
                {
                    return resolver;
                }

                throw new MessagePackGeneratorResolveFailedException("Resolver not found. Searched resolver name : " + defaultResolverName);
            }

            var found = FindInModule(module);
            if (found is null)
            {
                throw new MessagePackGeneratorResolveFailedException("Resolver not found.");
            }

            return found;
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
#if CSHARP_8_0_OR_NEWER
        private TypeDefinition? FindInModule(ModuleDefinition module)
#else
        private TypeDefinition FindInModule(ModuleDefinition module)
#endif
        {
            foreach (var type in module.Types)
            {
                var resolver = FindInType(type);
                if (resolver is null)
                {
                    continue;
                }

                return resolver;
            }

            return default;
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
#if CSHARP_8_0_OR_NEWER
        private TypeDefinition? FindInType(TypeDefinition type)
#else
        private TypeDefinition FindInType(TypeDefinition type)
#endif
        {
            foreach (var nestedType in type.NestedTypes)
            {
                var nestedResolver = FindInType(nestedType);
                if (nestedResolver is null)
                {
                    continue;
                }

                return nestedResolver;
            }

            if (type.IsInterface || type.IsEnum || !type.HasInterfaces)
            {
                return default;
            }

            foreach (var @interface in type.Interfaces)
            {
                if (@interface.InterfaceType.FullName != "MessagePack.IFormatterResolver")
                {
                    continue;
                }

                return type;
            }

            return default;
        }
    }
}