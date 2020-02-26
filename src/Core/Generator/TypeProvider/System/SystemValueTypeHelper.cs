// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemValueTypeHelper
    {
        public TypeReference ValueType { get; }

        public SystemValueTypeHelper(ModuleDefinition module)
        {
            ValueType = new TypeReference("System", "ValueType", module, module.TypeSystem.CoreLibrary, false);
        }
    }
}
