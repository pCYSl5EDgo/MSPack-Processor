// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MSPack.Processor.Core.Report;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class TypeProvider
    {
        private readonly IMetadataScope messagePackScope;
        private readonly IReportHook reportHook;
#if CSHARP_8_0_OR_NEWER
        private IMetadataScope? spanScope;
        private IMetadataScope? systemCollectionsScope;
        private IMetadataScope? systemRuntimeExtensionsScope;
        private AutomataDictionaryHelper? automataDictionaryHelper;
        private MessagePackWriterHelper? messagePackWriterHelper;
        private MessagePackReaderHelper? messagePackReaderHelper;
        private MessagePackSerializerOptionsHelper? messagePackSerializerOptionsHelper;
        private MessagePackSecurityHelper? messagePackSecurityHelper;
        private CodeGenHelpersHelper? codeGenHelpersHelper;
        private InterfaceMessagePackFormatterHelper? interfaceMessagePackFormatterHelper;
        private InterfaceFormatterResolverHelper? interfaceFormatterResolverHelper;
        private FormatterResolverExtensionHelper? formatterResolverExtensionHelper;
        private SystemCollectionsGenericIEqualityComparerHelper? systemCollectionsGenericIEqualityComparerHelper;
        private SystemCollectionsHashtableHelper? systemCollectionsHashtableHelper;
        private SystemRuntimeTypeHandleHelper? systemRuntimeTypeHandleHelper;
        private SystemReadOnlySpanHelper? systemReadOnlySpanHelper;
        private SystemObjectHelper? systemObjectHelper;
        private SystemValueTypeHelper? systemValueTypeHelper;
        private SystemTypeHelper? systemTypeHelper;
        private SystemConsoleHelper? systemConsoleHelper;
        private SystemExceptionHelper? systemExceptionHelper;
        private SystemInvalidOperationExceptionHelper? systemInvalidOperationExceptionHelper;
        private SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper? systemRuntimeCompilerServicesIsReadOnlyAttributeHelper;
        private SystemArrayHelper? systemArrayHelper;
#else
        private IMetadataScope spanScope;
        private IMetadataScope systemCollectionsScope;
        private IMetadataScope systemRuntimeExtensionsScope;
        private AutomataDictionaryHelper automataDictionaryHelper;
        private MessagePackWriterHelper messagePackWriterHelper;
        private MessagePackReaderHelper messagePackReaderHelper;
        private MessagePackSerializerOptionsHelper messagePackSerializerOptionsHelper;
        private MessagePackSecurityHelper messagePackSecurityHelper;
        private CodeGenHelpersHelper codeGenHelpersHelper;
        private InterfaceMessagePackFormatterHelper interfaceMessagePackFormatterHelper;
        private InterfaceFormatterResolverHelper interfaceFormatterResolverHelper;
        private FormatterResolverExtensionHelper formatterResolverExtensionHelper;
        private SystemCollectionsGenericIEqualityComparerHelper systemCollectionsGenericIEqualityComparerHelper;
        private SystemCollectionsHashtableHelper systemCollectionsHashtableHelper;
        private SystemRuntimeTypeHandleHelper systemRuntimeTypeHandleHelper;
        private SystemReadOnlySpanHelper systemReadOnlySpanHelper;
        private SystemObjectHelper systemObjectHelper;
        private SystemValueTypeHelper systemValueTypeHelper;
        private SystemTypeHelper systemTypeHelper;
        private SystemConsoleHelper systemConsoleHelper;
        private SystemExceptionHelper systemExceptionHelper;
        private SystemInvalidOperationExceptionHelper systemInvalidOperationExceptionHelper;
        private SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper systemRuntimeCompilerServicesIsReadOnlyAttributeHelper;
        private SystemArrayHelper systemArrayHelper;
#endif

        public TypeProvider(ModuleDefinition module, IMetadataScope messagePackScope, IReportHook reportHook)
        {
            this.Module = module;
            this.messagePackScope = messagePackScope;
            this.reportHook = reportHook;
            Importer = new ModuleImporter(module);
        }

        public ModuleDefinition Module { get; }

        private IMetadataScope SpanScope()
        {
            if (spanScope is null)
            {
                var spanFinder = new ScopeFinder(
                    "System.Span`1",
                    "System.ReadOnlySpan`1",
                    "System.Memory`1",
                    "System.ReadOnlyMemory`1");

                if (!spanFinder.TryFind(Module, out spanScope))
                {
                    throw new MessagePackGeneratorResolveFailedException("module should contain Span<T>, ReadOnlySpan<T>, Memory<T>, or ReadOnlyMemory<T>.");
                }
            }

            return spanScope;
        }

        private IMetadataScope SystemCollectionsScope()
        {
            if (!(systemCollectionsScope is null))
            {
                return systemCollectionsScope;
            }

            var scopeGenerator = default(AssemblyNameReferenceGenerator);
            systemCollectionsScope =
                scopeGenerator.TryGetDictionaryContainerNameReference(Module, out var dictionaryNameReference)
                    ? AssemblyNameReferenceInjector.Inject(Module, dictionaryNameReference, reportHook)
                    : Module.TypeSystem.CoreLibrary;

            return systemCollectionsScope;
        }

        public IMetadataScope SystemRuntimeExtensionsScope()
        {
            if (!(systemRuntimeExtensionsScope is null))
            {
                return systemRuntimeExtensionsScope;
            }

            var scopeGenerator = default(AssemblyNameReferenceGenerator);
            systemRuntimeExtensionsScope =
                scopeGenerator.TryGetSystemRuntimeExtensionDllNameReference(Module, out var hashtableNameReference)
                    ? AssemblyNameReferenceInjector.Inject(Module, hashtableNameReference, reportHook)
                    : Module.TypeSystem.CoreLibrary;

            return systemRuntimeExtensionsScope;
        }

        public SystemObjectHelper SystemObjectHelper
        {
            get
            {
                if (systemObjectHelper == null)
                {
                    systemObjectHelper = new SystemObjectHelper(Module, SystemTypeHelper);
                }

                return systemObjectHelper;
            }
        }

        public SystemValueTypeHelper SystemValueTypeHelper
        {
            get
            {
                if (systemValueTypeHelper == null)
                {
                    systemValueTypeHelper = new SystemValueTypeHelper(Module);
                }

                return systemValueTypeHelper;
            }
        }

        private SystemReadOnlySpanHelper SystemReadOnlySpanHelperFunc() => SystemReadOnlySpanHelper;

        public SystemReadOnlySpanHelper SystemReadOnlySpanHelper
        {
            get
            {
                if (systemReadOnlySpanHelper == null)
                {
                    systemReadOnlySpanHelper = new SystemReadOnlySpanHelper(Module, Importer, SpanScope);
                }

                return systemReadOnlySpanHelper;
            }
        }

        public MessagePackWriterHelper MessagePackWriterHelper
        {
            get
            {
                if (messagePackWriterHelper == null)
                {
                    messagePackWriterHelper = new MessagePackWriterHelper(Module, messagePackScope, SystemReadOnlySpanHelperFunc);
                }

                return messagePackWriterHelper;
            }
        }

        public MessagePackReaderHelper MessagePackReaderHelper
        {
            get
            {
                if (messagePackReaderHelper == null)
                {
                    messagePackReaderHelper = new MessagePackReaderHelper(Module, messagePackScope);
                }

                return messagePackReaderHelper;
            }
        }

        public CodeGenHelpersHelper CodeGenHelpersHelper
        {
            get
            {
                if (codeGenHelpersHelper == null)
                {
                    codeGenHelpersHelper = new CodeGenHelpersHelper(Module, messagePackScope, MessagePackReaderHelper, SystemReadOnlySpanHelperFunc);
                }

                return codeGenHelpersHelper;
            }
        }

        public MessagePackSecurityHelper MessagePackSecurityHelper
        {
            get
            {
                if (messagePackSecurityHelper == null)
                {
                    messagePackSecurityHelper = new MessagePackSecurityHelper(Module, messagePackScope, MessagePackReaderHelper);
                }

                return messagePackSecurityHelper;
            }
        }

        public InterfaceFormatterResolverHelper InterfaceFormatterResolverHelper
        {
            get
            {
                if (interfaceFormatterResolverHelper == null)
                {
                    interfaceFormatterResolverHelper = new InterfaceFormatterResolverHelper(Module, messagePackScope);
                }

                return interfaceFormatterResolverHelper;
            }
        }

        public MessagePackSerializerOptionsHelper MessagePackSerializerOptionsHelper
        {
            get
            {
                if (messagePackSerializerOptionsHelper == null)
                {
                    messagePackSerializerOptionsHelper = new MessagePackSerializerOptionsHelper(Module, messagePackScope, InterfaceFormatterResolverHelper, () => MessagePackSecurityHelper);
                }

                return messagePackSerializerOptionsHelper;
            }
        }

        public InterfaceMessagePackFormatterHelper InterfaceMessagePackFormatterHelper
        {
            get
            {
                if (interfaceMessagePackFormatterHelper == null)
                {
                    interfaceMessagePackFormatterHelper = new InterfaceMessagePackFormatterHelper(Module, messagePackScope, MessagePackWriterHelper, MessagePackReaderHelper, MessagePackSerializerOptionsHelper, Importer);
                }

                return interfaceMessagePackFormatterHelper;
            }
        }

        public FormatterResolverExtensionHelper FormatterResolverExtensionHelper
        {
            get
            {
                if (formatterResolverExtensionHelper == null)
                {
                    formatterResolverExtensionHelper = new FormatterResolverExtensionHelper(Module, messagePackScope, InterfaceFormatterResolverHelper, InterfaceMessagePackFormatterHelper, Importer);
                }

                return formatterResolverExtensionHelper;
            }
        }

        public SystemCollectionsGenericIEqualityComparerHelper SystemCollectionsGenericIEqualityComparerHelper
        {
            get
            {
                if (systemCollectionsGenericIEqualityComparerHelper == null)
                {
                    systemCollectionsGenericIEqualityComparerHelper = new SystemCollectionsGenericIEqualityComparerHelper(Module, Importer);
                }

                return systemCollectionsGenericIEqualityComparerHelper;
            }
        }

        public AutomataDictionaryHelper AutomataDictionaryHelper
        {
            get
            {
                if (automataDictionaryHelper == null)
                {
                    automataDictionaryHelper = new AutomataDictionaryHelper(Module, messagePackScope, SystemReadOnlySpanHelper);
                }

                return automataDictionaryHelper;
            }
        }

        public SystemCollectionsHashtableHelper SystemCollectionsHashtableHelper
        {
            get
            {
                if (systemCollectionsHashtableHelper == null)
                {
                    systemCollectionsHashtableHelper = new SystemCollectionsHashtableHelper(Module, SystemRuntimeExtensionsScope);
                }

                return systemCollectionsHashtableHelper;
            }
        }

        public SystemRuntimeTypeHandleHelper SystemRuntimeTypeHandleHelper
        {
            get
            {
                if (systemRuntimeTypeHandleHelper == null)
                {
                    systemRuntimeTypeHandleHelper = new SystemRuntimeTypeHandleHelper(Module);
                }

                return systemRuntimeTypeHandleHelper;
            }
        }

        public SystemTypeHelper SystemTypeHelper
        {
            get
            {
                if (systemTypeHelper == null)
                {
                    systemTypeHelper = new SystemTypeHelper(Module, SystemRuntimeTypeHandleHelper);
                }

                return systemTypeHelper;
            }
        }

        public SystemConsoleHelper SystemConsoleHelper
        {
            get
            {
                if (systemConsoleHelper == null)
                {
                    systemConsoleHelper = new SystemConsoleHelper(Module);
                }

                return systemConsoleHelper;
            }
        }

        public SystemExceptionHelper SystemExceptionHelper
        {
            get
            {
                if (systemExceptionHelper == null)
                {
                    systemExceptionHelper = new SystemExceptionHelper(Module);
                }

                return systemExceptionHelper;
            }
        }

        public SystemInvalidOperationExceptionHelper SystemInvalidOperationExceptionHelper
        {
            get
            {
                if (systemInvalidOperationExceptionHelper == null)
                {
                    systemInvalidOperationExceptionHelper = new SystemInvalidOperationExceptionHelper(Module);
                }

                return systemInvalidOperationExceptionHelper;
            }
        }

        public SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper
        {
            get
            {
                if (systemRuntimeCompilerServicesIsReadOnlyAttributeHelper == null)
                {
                    systemRuntimeCompilerServicesIsReadOnlyAttributeHelper = new SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper(Module);
                }

                return systemRuntimeCompilerServicesIsReadOnlyAttributeHelper;
            }
        }

        public SystemArrayHelper SystemArrayHelper
        {
            get
            {
                if (systemArrayHelper == null)
                {
                    systemArrayHelper = new SystemArrayHelper(Module, Importer);
                }

                return systemArrayHelper;
            }
        }

        public ModuleImporter Importer { get; }
    }
}
