// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using MSPack.Processor.Core.Report;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MSPack.Processor.Core
{
    public class CodeGenerator : IDisposable
    {
        private readonly Action<string> logger;
        private readonly IReportHook reportHook;
        private readonly ReaderParameters readerParam;
        private readonly ReaderParameters doNotWriteReaderParam;

        private bool disposed;

#if CSHARP_8_0_OR_NEWER
        private ModuleDefinition[]? moduleDefinitions;
        private ModuleDefinition[]? definitionModuleDefinitions;
#else
        private ModuleDefinition[] moduleDefinitions;
        private ModuleDefinition[] definitionModuleDefinitions;
#endif

        public CodeGenerator(Action<string> logger, IReportHook reportHook)
        {
            this.logger = logger;
            this.reportHook = reportHook;
            var assemblyResolver = new DefaultAssemblyResolver();
            this.readerParam = new ReaderParameters
            {
                AssemblyResolver = assemblyResolver,
                ReadWrite = true,
            };
            this.doNotWriteReaderParam = new ReaderParameters()
            {
                AssemblyResolver = assemblyResolver,
                ReadWrite = false,
            };
        }

        public void Generate(
            string inputPath,
            string resolverName,
            string[] libraryPaths,
            string[] definitionPaths,
            bool useMapMode,
            double loadFactor)
        {
            if (disposed)
            {
                throw new InvalidOperationException("Generate method can be called once.");
            }

            var sw = new Stopwatch();
            ModuleDefinition inputModule;
            TypeDefinition resolverTypeDefinition;
            TypeProvider provider;
            moduleDefinitions = new ModuleDefinition[1 + libraryPaths.Length];
            using (new Watcher(sw, logger, "Module Reading"))
            {
                moduleDefinitions[0] = inputModule = ModuleDefinition.ReadModule(inputPath, readerParam);

                logger("log inputModule\nassembly : " + inputModule.Assembly.FullName + "\nfile : " + inputModule.FileName);

                var resolverFinder = new FormatterResolverFinder();
                resolverTypeDefinition = resolverFinder.Find(inputModule, resolverName);

                logger("log resolver\nname : " + resolverTypeDefinition.FullName);

                var messagePackAssemblyNameReference = inputModule.AssemblyReferences.First(x => x.Name == "MessagePack");
                ReadModules(libraryPaths, moduleDefinitions);
                definitionModuleDefinitions = new ModuleDefinition[definitionPaths.Length];
                for (var index = 0; index < definitionPaths.Length; index++)
                {
                    var path = definitionPaths[index];
                    definitionModuleDefinitions[index] = ModuleDefinition.ReadModule(path, doNotWriteReaderParam);
                }

                var verifier = new ModuleRelationshipVerifier();
                verifier.Verify(inputModule, moduleDefinitions);

                provider = new TypeProvider(inputModule, messagePackAssemblyNameReference, reportHook);
            }

            CollectedInfo[] collectedInfos;
            EnumSerializationInfo[] enumSerializationInfos;
            using (new Watcher(sw, logger, "Method Collect"))
            {
                collectedInfos = CollectInfo(useMapMode, moduleDefinitions);
                var enumTypeCollector = new EnumTypeCollector();
                enumSerializationInfos = enumTypeCollector.Collect(collectedInfos);
            }

            using (new Watcher(sw, logger, "Ensure Internal Access"))
            {
                EnsureInternalAccessibility(inputModule, collectedInfos, provider.SystemObjectHelper);
            }

            FormatterInfo[] formatterInfos;
            using (new Watcher(sw, logger, "Formatter Generation"))
            {
                var generator = new FormatterGenerator(resolverTypeDefinition, provider, loadFactor);
                formatterInfos = generator.Generate(collectedInfos);
                var oldLength = formatterInfos.Length;
                var enumGenerator = new EnumFormatterGenerator(resolverTypeDefinition, provider);
                var enumFormatterInfos = enumGenerator.Generate(enumSerializationInfos);
                Array.Resize(ref formatterInfos, oldLength + enumFormatterInfos.Length);
                Array.Copy(enumFormatterInfos, 0, formatterInfos, oldLength, enumFormatterInfos.Length);
            }

            var pairGenerator = new TypeKeyInterfaceMessagePackFormatterValuePairGenerator(provider);
            var tableGenerator = new FixedTypeKeyInterfaceMessagePackFormatterValueHashtableGenerator(inputModule, pairGenerator, provider.InterfaceMessagePackFormatterHelper, provider.SystemObjectHelper, provider.SystemTypeHelper, provider.Importer, provider.SystemArrayHelper, loadFactor);
            var (tableType, getFormatterMethodInfo) = tableGenerator.Generate(formatterInfos);
            resolverTypeDefinition.NestedTypes.Add(tableType);

            var resolverInjector = new ResolverInjector(inputModule, resolverTypeDefinition, provider);
            resolverInjector.Implement(getFormatterMethodInfo);

            PrivateAccessEnabler.EnablePrivateAccess(inputModule, provider.SystemRuntimeExtensionsScope);

            try
            {
                inputModule.Write();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void EnsureInternalAccessibility(ModuleDefinition targetModule, CollectedInfo[] collectedInfos, SystemObjectHelper systemObjectHelper)
        {
            var ignoresAccessChecksToAttributeGenerator = new IgnoresAccessChecksToAttributeGenerator(targetModule, systemObjectHelper);

            for (var i = 0; i < collectedInfos.Length; i++)
            {
                ref readonly var collectedInfo = ref collectedInfos[i];
                if (collectedInfo.PublicAccessible)
                {
                    continue;
                }

                ignoresAccessChecksToAttributeGenerator.EnsureAccess(collectedInfo.Module.Assembly.Name);
            }
        }
#if CSHARP_8_0_OR_NEWER
        private ref struct Watcher
#else
        private struct Watcher : IDisposable
#endif
        {
            private readonly Stopwatch sw;
            private readonly Action<string> logger;
            private readonly string description;

            public Watcher(Stopwatch sw, Action<string> logger, string description)
            {
                this.sw = sw;
                this.description = description;
                this.logger = logger;
                sw.Restart();
                logger(description + " Start");
            }

            public void Dispose()
            {
                logger(description + " Complete:" + sw.Elapsed.ToString());
            }
        }

        private CollectedInfo[] CollectInfo(bool useMapMode, ModuleDefinition[] modules)
        {
            if (modules.Length == 0)
            {
                return Array.Empty<CollectedInfo>();
            }

            var collectors = new TypeCollector[modules.Length];
            for (var i = 0; i < modules.Length; i++)
            {
                collectors[i] = new TypeCollector(modules[i], useMapMode, logger);
            }

            var answers = new CollectedInfo[modules.Length];

            for (var i = 0; i < collectors.Length; i++)
            {
                answers[i] = collectors[i].Collect();
            }

            return answers;
        }

        private void ReadModules(string[] paths, ModuleDefinition[] modules)
        {
            for (var i = 0; i < paths.Length; i++)
            {
                var path = paths[i];
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Dll/Exe path does not exist. path : " + path);
                }

                modules[i + 1] = ModuleDefinition.ReadModule(path, doNotWriteReaderParam);
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            if (!(moduleDefinitions is null))
            {
                for (var index = 0; index < moduleDefinitions.Length; index++)
                {
                    moduleDefinitions[index].Dispose();
                }

                moduleDefinitions = default;
            }

            if (!(definitionModuleDefinitions is null))
            {
                for (var index = 0; index < definitionModuleDefinitions.Length; index++)
                {
                    definitionModuleDefinitions[index].Dispose();
                }

                definitionModuleDefinitions = default;
            }
        }
    }
}
