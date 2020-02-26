// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using ConsoleAppFramework;
using MSPack.Processor.Core;
using Microsoft.Extensions.Hosting;

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
            [Option("t", "Path of target dll or exe.")]string target,
            [Option("n", "Set resolver name with namespace.")]string resolverName = "MessagePack.GeneratedResolver",
            [Option("l", "Library dll or exe file paths that target file depends on. Split with ','.")] string libraryFiles = "",
            [Option("m", "Force use map mode serialization.")]bool useMapMode = false,
            [Option("load-factor", "Specific setting of dictionary load factor")]double loadFactor = 0.75)
        {
            var reportHook = new NopHook();
            var codeGenerator = new CodeGenerator(Console.WriteLine, reportHook);

            var libraryPaths = string.IsNullOrWhiteSpace(libraryFiles) ? Array.Empty<string>() : libraryFiles.Split(',', StringSplitOptions.RemoveEmptyEntries);

            codeGenerator
                .Generate(
                    target,
                    resolverName,
                    libraryPaths,
                    useMapMode,
                    loadFactor);
        }
    }
}
