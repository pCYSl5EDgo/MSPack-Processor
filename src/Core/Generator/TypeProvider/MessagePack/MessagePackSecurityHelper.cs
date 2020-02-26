// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class MessagePackSecurityHelper
    {
        private readonly ModuleDefinition module;
        private readonly IMetadataScope messagePackScope;
        private readonly MessagePackReaderHelper readerHelper;
        private TypeReference? messagePackSecurity;
        private MethodReference? depthStep;

        public MessagePackSecurityHelper(ModuleDefinition module, IMetadataScope messagePackScope, MessagePackReaderHelper readerHelper)
        {
            this.module = module;
            this.messagePackScope = messagePackScope;
            this.readerHelper = readerHelper;
        }

        public TypeReference MessagePackSecurity => messagePackSecurity ??= new TypeReference("MessagePack", nameof(MessagePackSecurity), module, messagePackScope, false);

        public MethodReference DepthStep => depthStep ??= new MethodReference(nameof(DepthStep), module.TypeSystem.Void, MessagePackSecurity)
        {
            HasThis = true,
            Parameters =
            {
                new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(readerHelper.Reader)),
            },
        };
    }
}
