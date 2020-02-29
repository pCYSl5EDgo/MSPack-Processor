// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSPack.Processor.Core
{
    public sealed class ModuleRelationshipVerifier
    {
        public void Verify<TCollection>(ModuleDefinition targetModule, TCollection modules)
            where TCollection : IEnumerable<ModuleDefinition>
        {
            var targetName = targetModule.Assembly.Name;
            if (ReferenceEquals(targetName.PublicKeyToken, Array.Empty<byte>()))
            {
                foreach (var moduleDefinition in modules)
                {
                    VerifyStrongNamed(targetName.Name, targetName.Version, targetName.Culture, targetName.PublicKeyToken, moduleDefinition);
                }
            }
            else
            {
                foreach (var moduleDefinition in modules)
                {
                    VerifyWeakNamed(targetName.Name, moduleDefinition);
                }
            }
        }

        private static void VerifyWeakNamed(string targetNameName, ModuleDefinition moduleDefinition)
        {
            foreach (var nameReference in moduleDefinition.AssemblyReferences)
            {
                if (string.Equals(targetNameName, nameReference.Name, StringComparison.Ordinal))
                {
                    throw new MessagePackGeneratorResolveFailedException("circular reference is not allowed. Between module : " + targetNameName + " and module : " + nameReference.FullName);
                }
            }
        }

        private static void VerifyStrongNamed(string targetNameName, Version targetNameVersion, string targetNameCulture, byte[] targetNamePublicKeyToken, ModuleDefinition moduleDefinition)
        {
            foreach (var nameReference in moduleDefinition.AssemblyReferences)
            {
                if (string.Equals(targetNameName, nameReference.Name, StringComparison.Ordinal) && string.Equals(targetNameCulture, nameReference.Culture, StringComparison.Ordinal) && targetNamePublicKeyToken.SequenceEqual(nameReference.PublicKeyToken) && targetNameVersion.Equals(nameReference.Version))
                {
                    throw new MessagePackGeneratorResolveFailedException("circular reference is not allowed. Between module : " + targetNameName + " and module : " + nameReference.FullName);
                }
            }
        }
    }
}
