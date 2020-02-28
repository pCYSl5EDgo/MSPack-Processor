// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public static class RuntimeDetector
    {
        public static RuntimeTarget DetectRuntimeTarget(ModuleDefinition module)
        {
            switch (module.TypeSystem.CoreLibrary.Name)
            {
                case "netstandard":
                    return RuntimeTarget.NetStandard;
                case "System.Runtime":
                    return RuntimeTarget.Runtime;
                case "mscorlib":
                    return RuntimeTarget.MicrosoftCoreLibrary;
                default:
                    throw new MessagePackGeneratorResolveFailedException("not supported runtime. module core library assembly name : " + module.TypeSystem.CoreLibrary.Name);
            }
        }
    }

    public enum RuntimeTarget
    {
        NetStandard,
        Runtime,
        MicrosoftCoreLibrary,
    }
}
