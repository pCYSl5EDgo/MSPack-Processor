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
#if CSHARP_8_0_OR_NEWER
        private TypeReference? messagePackSecurity;
        private MethodReference? depthStep;
#else
        private TypeReference messagePackSecurity;
        private MethodReference depthStep;
#endif


        public MessagePackSecurityHelper(ModuleDefinition module, IMetadataScope messagePackScope, MessagePackReaderHelper readerHelper)
        {
            this.module = module;
            this.messagePackScope = messagePackScope;
            this.readerHelper = readerHelper;
        }

        public TypeReference MessagePackSecurity
        {
            get
            {
                if (messagePackSecurity == null)
                {
                    messagePackSecurity = new TypeReference("MessagePack", nameof(MessagePackSecurity), module, messagePackScope, false);
                }

                return messagePackSecurity;
            }
        }

        public MethodReference DepthStep
        {
            get
            {
                if (depthStep == null)
                {
                    depthStep = new MethodReference(nameof(DepthStep), module.TypeSystem.Void, MessagePackSecurity)
                    {
                        HasThis = true,
                        Parameters =
                        {
                            new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(readerHelper.Reader)),
                        },
                    };
                }

                return depthStep;
            }
        }
    }
}
