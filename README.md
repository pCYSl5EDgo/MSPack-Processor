# MSPack Processor - AOT compiler for [MessagePack-CSharp](https://github.com/neuecc/MessagePack-CSharp)

MessagePack-CSharp need ahead-of-time source code generation in Unity, Xamarin iOS and UWP environment.<br/>
The source code generator(mpc.exe) analyzes csproj file(s) and emits C# file(s).

## Installation

This library is distributed via NuGet package and with special [support for Unity](#unity).

### NuGet packages

Core library works with netstandard2.0 api.
Command Line Interface works on .net core 3.1.

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