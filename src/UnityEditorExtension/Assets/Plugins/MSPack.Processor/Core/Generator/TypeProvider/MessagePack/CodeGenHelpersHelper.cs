// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;

namespace MSPack.Processor.Core.Provider
{
    public sealed class CodeGenHelpersHelper
    {
        private readonly ModuleDefinition module;
        private readonly IMetadataScope messagePackScope;
        private readonly MessagePackReaderHelper readerHelper;
        private readonly Func<SystemReadOnlySpanHelper> readOnlySpanHelper;
#if CSHARP_8_0_OR_NEWER
        private MethodReference? readStringSpan;
        private TypeReference? codeGenHelpers;
#else
        private MethodReference readStringSpan;
        private TypeReference codeGenHelpers;
#endif


        public CodeGenHelpersHelper(ModuleDefinition module, IMetadataScope messagePackScope, MessagePackReaderHelper readerHelper, Func<SystemReadOnlySpanHelper> readOnlySpanHelper)
        {
            this.module = module;
            this.messagePackScope = messagePackScope;
            this.readerHelper = readerHelper;
            this.readOnlySpanHelper = readOnlySpanHelper;
        }

        public TypeReference CodeGenHelpers
        {
            get
            {
                if (codeGenHelpers == null)
                {
                    codeGenHelpers = new TypeReference("MessagePack.Internal", nameof(CodeGenHelpers), module, messagePackScope, false);
                }

                return codeGenHelpers;
            }
        }

        public MethodReference ReadStringSpan
        {
            get
            {
                if (readStringSpan == null)
                {
                    readStringSpan = new MethodReference(nameof(ReadStringSpan), readOnlySpanHelper.Invoke().ReadOnlySpanGeneric(module.TypeSystem.Byte), CodeGenHelpers)
                    {
                        HasThis = false,
                        Parameters =
                        {
                            new ParameterDefinition("reader", ParameterAttributes.None, new ByReferenceType(readerHelper.Reader)),
                        },
                    };
                }

                return readStringSpan;
            }
        }
    }
}
