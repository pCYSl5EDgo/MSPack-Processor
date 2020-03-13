// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public static class PublicTypeTestUtility
    {
        public static bool IsPublicType(TypeDefinition definition)
        {
            if (definition.IsNestedPublic)
            {
                return PublicTypeTestUtility.IsPublicType(definition.DeclaringType);
            }

            return definition.IsPublic;
        }
    }
}
