// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class MessagePackSerializerOptionsHelper
    {
        private readonly InterfaceFormatterResolverHelper interfaceFormatterResolverHelper;
        private readonly Func<MessagePackSecurityHelper> messagePackSecurityHelper;
        public readonly TypeReference Options;
        private MethodReference? _get_Resolver;
        private MethodReference? _get_Security;

        public MessagePackSerializerOptionsHelper(ModuleDefinition module, IMetadataScope messagePackScope, InterfaceFormatterResolverHelper interfaceFormatterResolverHelper, Func<MessagePackSecurityHelper> messagePackSecurityHelper)
        {
            this.interfaceFormatterResolverHelper = interfaceFormatterResolverHelper;
            this.messagePackSecurityHelper = messagePackSecurityHelper;
            Options = new TypeReference("MessagePack", "MessagePackSerializerOptions", module, messagePackScope, false);
        }

        public MethodReference get_Resolver => _get_Resolver ??= new MethodReference(nameof(get_Resolver), interfaceFormatterResolverHelper.IFormatterResolver, Options)
        {
            HasThis = true,
        };

        public MethodReference get_Security => _get_Security ??= new MethodReference(nameof(get_Security), messagePackSecurityHelper.Invoke().MessagePackSecurity, Options)
        {
            HasThis = true,
        };
    }
}
