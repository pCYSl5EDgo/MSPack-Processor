// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MSPack.Processor.Core.Definitions;
using MSPack.Processor.Core.Provider;
using MSPack.Processor.Core.Report;
using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public class CodeGenerator
    {
        private readonly Action<string> logger;
        private readonly IReportHook reportHook;
        private readonly ReaderParameters readerParam;
        private readonly ReaderParameters doNotWriteReaderParam;

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
            string targetPath,
            string resolverName,
            string[] libraryPaths,
            bool useMapMode,
            double loadFactor)
        {
            var sw = new Stopwatch();
            ModuleDefinition targetModule;
            TypeDefinition resolverTypeDefinition;
            TypeProvider provider;
            var modules = new ModuleDefinition[1 + libraryPaths.Length];
            using (new Watcher(sw, logger, "Module Reading"))
            {
                modules[0] = targetModule = ModuleDefinition.ReadModule(targetPath, readerParam);

                resolverTypeDefinition = targetModule.GetType(resolverName);
                if (resolverTypeDefinition is null)
                {
                    throw new MessagePackGeneratorResolveFailedException("Resolver not found. type : " + resolverName);
                }

                var messagePackAssemblyNameReference = targetModule.AssemblyReferences.First(x => x.Name == "MessagePack");
                ReadModules(libraryPaths, modules);

                var verifier = new ModuleRelationshipVerifier();
                verifier.Verify(targetModule, modules);

                provider = new TypeProvider(targetModule, messagePackAssemblyNameReference, reportHook);
            }

            CollectedInfo[] collectedInfos;
            EnumSerializationInfo[] enumSerializationInfos;
            using (new Watcher(sw, logger, "Method Collect"))
            {
                collectedInfos = CollectInfo(useMapMode, modules);
                var enumTypeCollector = new EnumTypeCollector();
                enumSerializationInfos = enumTypeCollector.Collect(collectedInfos);
            }

            using (new Watcher(sw, logger, "Ensure Internal Access"))
            {
                EnsureInternalAccessibility(targetModule, collectedInfos, provider.SystemObjectHelper);
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
            var tableGenerator = new FixedTypeKeyInterfaceMessagePackFormatterValueHashtableGenerator(targetModule, pairGenerator, provider.InterfaceMessagePackFormatterHelper, provider.SystemObjectHelper, provider.SystemTypeHelper, provider.Importer, provider.SystemArrayHelper, loadFactor);
            var (tableType, getFormatterMethodInfo) = tableGenerator.Generate(formatterInfos);
            resolverTypeDefinition.NestedTypes.Add(tableType);

            var resolverInjector = new ResolverInjector(targetModule, resolverName, provider);
            resolverInjector.Implement(getFormatterMethodInfo);

            PrivateAccessEnabler.EnablePrivateAccess(targetModule, provider.SystemRuntimeExtensionsScope);

            try
            {
                targetModule.Write();
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
    }
}
