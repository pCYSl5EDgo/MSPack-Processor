// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public static class RuntimeDetector
    {
        public static RuntimeTarget DetectRuntimeTarget(ModuleDefinition module) =>
            module.TypeSystem.CoreLibrary.Name switch
            {
                "netstandard" => RuntimeTarget.NetStandard,
                "System.Runtime" => RuntimeTarget.Runtime,
                "mscorlib" => RuntimeTarget.MicrosoftCoreLibrary,
                _ => throw new MessagePackGeneratorResolveFailedException("not supported runtime. module core library assembly name : " + module.TypeSystem.CoreLibrary.Name)
            };
    }

    public enum RuntimeTarget
    {
        NetStandard,
        Runtime,
        MicrosoftCoreLibrary,
    }
}
