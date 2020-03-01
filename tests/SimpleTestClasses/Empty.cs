// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;
using System;

namespace SimpleTestClasses
{
    [MessagePackObject]
    public struct EmptyStruct : IEquatable<EmptyStruct>
    {
        public override bool Equals(object obj) => obj is EmptyStruct;

        public bool Equals(EmptyStruct other) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "";
    }

    [MessagePackObject]
    public class EmptyClass : IEquatable<EmptyClass>
    {
        public override bool Equals(object obj) => obj is EmptyClass;

        public bool Equals(EmptyClass other) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "";
    }

    [MessagePackObject]
    public sealed class EmptySealedClass : IEquatable<EmptySealedClass>
    {
        public override bool Equals(object obj) => obj is EmptySealedClass;

        public bool Equals(EmptySealedClass other) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "";
    }
}
