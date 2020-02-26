// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class AutomataDictionaryHelper
    {
        private readonly ModuleDefinition module;
        private readonly IMetadataScope messagePackScope;
        private readonly SystemReadOnlySpanHelper readOnlySpanHelper;
        private MethodReference? ctor;
        private MethodReference? add;
        private MethodReference? tryGetValue;

        public TypeReference AutomataDictionary { get; }

        public AutomataDictionaryHelper(ModuleDefinition module, IMetadataScope messagePackScope, SystemReadOnlySpanHelper readOnlySpanHelper)
        {
            this.module = module;
            this.messagePackScope = messagePackScope;
            this.readOnlySpanHelper = readOnlySpanHelper;
            AutomataDictionary = new TypeReference("MessagePack.Internal", "AutomataDictionary", module, messagePackScope, false);
        }

        public MethodReference Ctor => ctor ??= new MethodReference(".ctor", module.TypeSystem.Void, AutomataDictionary)
        {
            HasThis = true,
        };

        public MethodReference Add => add ??= new MethodReference(nameof(Add), module.TypeSystem.Void, AutomataDictionary)
        {
            HasThis = true,
            Parameters =
            {
                new ParameterDefinition("str", ParameterAttributes.None, module.TypeSystem.String),
                new ParameterDefinition("value", ParameterAttributes.None, module.TypeSystem.Int32),
            },
        };

        public MethodReference TryGetValue => tryGetValue ??= new MethodReference(nameof(TryGetValue), module.TypeSystem.Boolean, AutomataDictionary)
        {
            HasThis = true,
            Parameters =
            {
                new ParameterDefinition("bytes", ParameterAttributes.None, readOnlySpanHelper.ReadOnlySpanGeneric(module.TypeSystem.Byte)),
                new ParameterDefinition("value", ParameterAttributes.Out, new ByReferenceType(module.TypeSystem.Int32)),
            },
        };
    }
}
