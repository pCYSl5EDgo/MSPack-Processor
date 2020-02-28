// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using MSPack.Processor.Core;
using MSPack.Processor.Core.Report;
using System;
using System.Threading.Tasks;

namespace MSPack.Processor.CLI
{
    public class MessagepackCompiler : ConsoleAppBase
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureLogging(logging => logging.ReplaceToSimpleConsole())
                .RunConsoleAppFrameworkAsync<MessagepackCompiler>(args)
                .ConfigureAwait(false);
        }

        public void Run(
            [Option("i", "Path of input dll.")]string input,
            [Option("n", "Set resolver name with namespace.")]string resolverName = "",
            [Option("l", "Library dll file paths that target file depends on. Split with ','.")] string libraryFiles = "",
            [Option("d", "Definition dll file paths. Split with ','.")] string definitionFiles = "",
            [Option("m", "Force use map mode serialization.")]bool useMapMode = false,
            [Option("load-factor", "Specific setting of dictionary load factor")]double loadFactor = 0.75)
        {
            var reportHook = new NopReportHook();
            var codeGenerator = new CodeGenerator(Console.WriteLine, reportHook);

            var libraryPaths = Split(libraryFiles);
            var definitionPaths = Split(definitionFiles);

            codeGenerator
                .Generate(
                    input,
                    resolverName,
                    libraryPaths,
                    definitionPaths,
                    useMapMode,
                    loadFactor);
        }

        private static string[] Split(string paths)
        {
            return string.IsNullOrWhiteSpace(paths) ? Array.Empty<string>() : paths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
