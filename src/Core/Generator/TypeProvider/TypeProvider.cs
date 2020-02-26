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
        private SystemCollectionsGenericDictionaryHelper? systemCollectionsGenericDictionaryHelper;
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

        public TypeProvider(ModuleDefinition module, IMetadataScope messagePackScope, IReportHook reportHook)
        {
            this.Module = module;
            this.messagePackScope = messagePackScope;
            this.reportHook = reportHook;
            Importer = new ModuleImporter(module);

            DebugInjectorUtility.SetProvider(this);
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

        private IMetadataScope SystemRuntimeExtensionsScope()
        {
            if (!(systemRuntimeExtensionsScope is null))
            {
                return systemRuntimeExtensionsScope;
            }

            var scopeGenerator = default(AssemblyNameReferenceGenerator);
            systemRuntimeExtensionsScope =
                scopeGenerator.TryGetHashtableContainerNameReference(Module, out var hashtableNameReference)
                    ? AssemblyNameReferenceInjector.Inject(Module, hashtableNameReference, reportHook)
                    : Module.TypeSystem.CoreLibrary;

            return systemRuntimeExtensionsScope;
        }

        public SystemObjectHelper SystemObjectHelper => systemObjectHelper ??= new SystemObjectHelper(Module, SystemTypeHelper);

        public SystemValueTypeHelper SystemValueTypeHelper => systemValueTypeHelper ??= new SystemValueTypeHelper(Module);

        private SystemReadOnlySpanHelper SystemReadOnlySpanHelperFunc() => SystemReadOnlySpanHelper;

        public SystemReadOnlySpanHelper SystemReadOnlySpanHelper => systemReadOnlySpanHelper ??= new SystemReadOnlySpanHelper(Module, Importer, SpanScope);

        public MessagePackWriterHelper MessagePackWriterHelper => messagePackWriterHelper ??= new MessagePackWriterHelper(Module, messagePackScope, SystemReadOnlySpanHelperFunc);

        public MessagePackReaderHelper MessagePackReaderHelper => messagePackReaderHelper ??= new MessagePackReaderHelper(Module, messagePackScope);

        public CodeGenHelpersHelper CodeGenHelpersHelper => codeGenHelpersHelper ??= new CodeGenHelpersHelper(Module, messagePackScope, MessagePackReaderHelper, SystemReadOnlySpanHelperFunc);

        public MessagePackSecurityHelper MessagePackSecurityHelper => messagePackSecurityHelper ??= new MessagePackSecurityHelper(Module, messagePackScope, MessagePackReaderHelper);

        public InterfaceFormatterResolverHelper InterfaceFormatterResolverHelper => interfaceFormatterResolverHelper ??= new InterfaceFormatterResolverHelper(Module, messagePackScope);

        public MessagePackSerializerOptionsHelper MessagePackSerializerOptionsHelper => messagePackSerializerOptionsHelper ??= new MessagePackSerializerOptionsHelper(Module, messagePackScope, InterfaceFormatterResolverHelper, () => MessagePackSecurityHelper);

        public InterfaceMessagePackFormatterHelper InterfaceMessagePackFormatterHelper => interfaceMessagePackFormatterHelper ??= new InterfaceMessagePackFormatterHelper(Module, messagePackScope, MessagePackWriterHelper, MessagePackReaderHelper, MessagePackSerializerOptionsHelper, Importer);

        public FormatterResolverExtensionHelper FormatterResolverExtensionHelper => formatterResolverExtensionHelper ??= new FormatterResolverExtensionHelper(Module, messagePackScope, InterfaceFormatterResolverHelper, InterfaceMessagePackFormatterHelper, Importer);

        public SystemCollectionsGenericIEqualityComparerHelper SystemCollectionsGenericIEqualityComparerHelper => systemCollectionsGenericIEqualityComparerHelper ??= new SystemCollectionsGenericIEqualityComparerHelper(Module, Importer);

        public SystemCollectionsGenericDictionaryHelper SystemCollectionsGenericDictionaryHelper => systemCollectionsGenericDictionaryHelper ??= new SystemCollectionsGenericDictionaryHelper(Module, SystemCollectionsScope, SystemCollectionsGenericIEqualityComparerHelper, Importer);

        public AutomataDictionaryHelper AutomataDictionaryHelper => automataDictionaryHelper ??= new AutomataDictionaryHelper(Module, messagePackScope, SystemReadOnlySpanHelper);

        public SystemCollectionsHashtableHelper SystemCollectionsHashtableHelper => systemCollectionsHashtableHelper ??= new SystemCollectionsHashtableHelper(Module, SystemRuntimeExtensionsScope);

        public SystemRuntimeTypeHandleHelper SystemRuntimeTypeHandleHelper => systemRuntimeTypeHandleHelper ??= new SystemRuntimeTypeHandleHelper(Module);

        public SystemTypeHelper SystemTypeHelper => systemTypeHelper ??= new SystemTypeHelper(Module, SystemRuntimeTypeHandleHelper);

        public SystemConsoleHelper SystemConsoleHelper => systemConsoleHelper ??= new SystemConsoleHelper(Module);

        public SystemExceptionHelper SystemExceptionHelper => systemExceptionHelper ??= new SystemExceptionHelper(Module);

        public SystemInvalidOperationExceptionHelper SystemInvalidOperationExceptionHelper => systemInvalidOperationExceptionHelper ??= new SystemInvalidOperationExceptionHelper(Module);

        public SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper => systemRuntimeCompilerServicesIsReadOnlyAttributeHelper ??= new SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper(Module);

        public SystemArrayHelper SystemArrayHelper => systemArrayHelper ??= new SystemArrayHelper(Module, Importer);

        public ModuleImporter Importer { get; }
    }
}
