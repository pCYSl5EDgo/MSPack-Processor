# MSPack Processor - AOT compiler for [MessagePack-CSharp](https://github.com/neuecc/MessagePack-CSharp)

MSPack ProcessorはMessagePack-CSharpのAOTコード生成(mpc.exe)を置換する目的で作成されたライブラリとCLIツールである。

## Installation

.NET Core環境から使用する場合Nugetからインストールすることが可能である。

```
dotnet tool install -g MSPack.Processor.CLI
```

dotnet global toolsとしてのコマンド名は`mspc`である。

## How to use

MSBuildのTaskについてはどう実装すればよいのかよくわからないので用意していない。<br/>
dotnet global toolsをビルド後イベントから叩くのがいいのではないだろうか。

- `-i`
    - 編纂対象となるDLLのパス。
    - 複数DLLを指定することは出来ない。
    - 対象DLLを書き換えるので事前にバックアップを取るなりするべきかもしれない。
- `-n`
    - 諸々注入する先のIFormatterResolverを実装した具象型の名前空間込の名前。
    - `GetFormatter`メソッドの中身を書き換える。
    - Unity EditorではIL2CPPビルド時とMonoビルド時しか働かない予定なので、`throw new NotImplementedException();`よりは`return null;`の方が角が立たないと思われる。
- `-l`
    - `[MessagePackObjectAttribute]`が付与された型が所属するDLL群。
    - `,`区切りで複数のDLLを指定できる。
    - `[MessagePackObjectAttribute]`が付与された型のFormatterが編纂対象のResolverの内部型として生成される。
- `-d`
    - DLL群。
    - `,`区切りで複数のDLLを指定できる。
    - Enum型をシリアライズする時にその型の詳細な情報が必要になる。
    - 具体的には基底型がbyte, sbyte, ushort, short, uint, int, ulong, longのどれであるのかを知らねばならない。
    - これが不明だと実行時にFormatterが見つからないとエラーになる。
    - これを防ぐためにはシリアライズ対象のEnum型が含まれたDLLを`-l`または`-d`で指定せねばならない。
    - `[MessagePackObjectAttribute]`を付与された型が存在しないと事前にわかっている場合は、`-l`より`-d`の方が`[MessagePackObjectAttribute]`を検索しない分軽量なのでおすすめする。
- `-m`
    - 全ての型をmap modeとして扱う。
- `-load-factor`
    - 0より大きく1より小さい値を指定すること。
    - 性能特性チューニング用の値。
    - `System.Type`をKeyとした特化辞書を構築する際にハッシュテーブルをどれだけギチギチに埋めるかの割合。
    - 1に近いほどメモリ効率は良くなるが、ハッシュ値の衝突が大きくなり実行時にコストが嵩む。

### NuGet packages

[Core library](https://www.nuget.org/packages/MSPack.Processor.Core/) works with netstandard2.0 api.<br/>
[.Net Global Tools](https://www.nuget.org/packages/MSPack.Processor.CLI/) works on netcoreapp2.1 api.

## Supported Environment

### MSPack.Processor.Core

.NET Standard 2.0の上にC#7.3で記述されている。<br/>
依存するライブラリは`Mono.Cecil`(と`Mono.Cecil.Rocks`)のみである。

### MSPack.Processor.CLI

.NET Core 2.1以上で動作するようにC#7.2で記述し、.NET Global Toolsとして[Nuget上に公開されている](https://www.nuget.org/packages/MSPack.Processor.CLI/)。<br/>
依存するライブラリは`ZString`, `Utf8Json`, `ConsoleAppFramework`の3種類である。

### <a name="unity"></a>Unity

Unityからの利用法は現在作者であるpCYSl5EDgoのローカルマシンでIL2CPPビルドが出来ないため塩漬け中である。<br/>
テスト目的で利用する場合には、Unity2019.3においてUnity Package Managerを利用して`https://github.com/pCYSl5EDgo/MSPack-Processor.git#2019.3`をインストールする。


# LICENSE

MIT LICENSE

# AUTHOR

pCYSl5EDgo

## Special Thanks

- [mob-sakai](https://twitter.com/mob_sakai)
    - Without your help, I could not use `IgnoresAccessChecksToAttribute`.

# For Developers

- `dotnet tool install -g MSPack.Processor.CLI`でmspcをインストール。
- `tests/Core.Test/Core.Test.csproj`のビルド後コマンドでmspcを呼び出していることを確認されたし。
- `tests/Core.Test/Resolver.cs`にmspcを実行するために最低限必要な`IFormatterResolver`を実装したクラスを用意してある。
- `tests/Core.Test/EmptyTest.cs`に`[OneTimeSetUp]`属性が付与されたSetUp()メソッドが定義されている。
    - ここでResolverの登録を行っている。
- 後は`dotnet test`を実行してテストを走らせる。

## Unity

同梱されている[Unityプロジェクト](https://github.com/pCYSl5EDgo/MSPack-Processor/tree/dev/src/UnityEditorExtension)は2019.3.3f1で検証されている。<br/>
[メインコンポーネント](https://github.com/pCYSl5EDgo/MSPack-Processor/blob/dev/src/UnityEditorExtension/Assets/Scripts/Main.cs)とサンプルシーンをInspector Windowで見ればわかるが、正しくビルドすればx, y, zにそれぞれ&quot;5&quot;, &quot;10&quot;, &quot;-114&quot;が代入される。