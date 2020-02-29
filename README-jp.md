# MSPack Processor - AOT compiler for [MessagePack-CSharp](https://github.com/neuecc/MessagePack-CSharp)

MSPack ProcessorはMessagePack-CSharpのAOTコード生成(mpc.exe)を置換する目的で作成されたライブラリとCLIツールである。

## Installation

### NuGet packages

Core library works with netstandard2.0 api.
Command Line Interface works on .net core 3.1.

## Supported Environment

### MSPack.Processor.Core

.NET Standard 2.0の上にC#7.3で記述されている。
依存するライブラリは`Mono.Cecil`(と`Mono.Cecil.Rocks`)のみである。

### MSPack.Processor.CLI

.NET Core 2.1以上で動作するようにC#7.2で記述されている。
.NET Global ToolsとしてNuget上に公開されている。
依存するライブラリは`ZString`, `Utf8Json`, `ConsoleAppFramework`の3種類である。

### <a name="unity"></a>Unity

For Unity, use new Unity Package Manger to install. The master branch is designed for UPM.

# Usage

```
dotnet tool install -g mspc
```

# LICENSE

MIT LICENSE

# AUTHOR

pCYSl5EDgo

## Special Thanks

- [mob-sakai](https://twitter.com/mob_sakai)
    - Without your help, I could not use IgnoresAccessChecksTo.