// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class MessagePackReaderHelper
    {
        private readonly TypeSystem typeSystem;
        public readonly TypeReference Reader;
        public readonly TypeReference Nil;

        private MethodReference? _get_Depth;
        private MethodReference? _set_Depth;

        private MethodReference? _ReadArrayHeader;
        private MethodReference? _ReadMapHeader;
        private MethodReference? _ReadNil;
        private MethodReference? _ReadBoolean;
        private MethodReference? _ReadByte;
        private MethodReference? _ReadChar;
        private MethodReference? _ReadSByte;
        private MethodReference? _ReadInt16;
        private MethodReference? _ReadInt32;
        private MethodReference? _ReadInt64;
        private MethodReference? _ReadUInt16;
        private MethodReference? _ReadUInt32;
        private MethodReference? _ReadUInt64;
        private MethodReference? _ReadSingle;
        private MethodReference? _ReadDouble;
        private MethodReference? _Skip;
        private MethodReference? _TryReadNil;

        public MessagePackReaderHelper(ModuleDefinition module, IMetadataScope messagePackScope)
        {
            this.typeSystem = module.TypeSystem;
            Nil = new TypeReference("MessagePack", "Nil", module, messagePackScope, true);
            Reader = new TypeReference("MessagePack", "MessagePackReader", module, messagePackScope, true);
        }

        public MethodReference get_Depth => _get_Depth ??= new MethodReference(nameof(get_Depth), typeSystem.Int32, Reader) { HasThis = true, };

        public MethodReference set_Depth => _set_Depth ??= new MethodReference(nameof(set_Depth), typeSystem.Void, Reader)
        {
            HasThis = true,
            Parameters =
            {
                new ParameterDefinition("value", ParameterAttributes.None, typeSystem.Int32),
            },
        };

        public MethodReference ReadArrayHeader => _ReadArrayHeader ??= new MethodReference(nameof(ReadArrayHeader), typeSystem.Int32, Reader) { HasThis = true, };

        public MethodReference ReadMapHeader => _ReadMapHeader ??= new MethodReference(nameof(ReadMapHeader), typeSystem.Int32, Reader) { HasThis = true, };

        public MethodReference ReadNil => _ReadNil ??= new MethodReference(nameof(ReadNil), Nil, Reader) { HasThis = true, };

        public MethodReference ReadBoolean => _ReadBoolean ??= new MethodReference(nameof(ReadBoolean), typeSystem.Boolean, Reader) { HasThis = true, };

        public MethodReference ReadByte => _ReadByte ??= new MethodReference(nameof(ReadByte), typeSystem.Byte, Reader) { HasThis = true, };

        public MethodReference ReadChar => _ReadChar ??= new MethodReference(nameof(ReadChar), typeSystem.Char, Reader) { HasThis = true, };

        public MethodReference ReadSByte => _ReadSByte ??= new MethodReference(nameof(ReadSByte), typeSystem.SByte, Reader) { HasThis = true, };

        public MethodReference ReadInt16 => _ReadInt16 ??= new MethodReference(nameof(ReadInt16), typeSystem.Int16, Reader) { HasThis = true, };

        public MethodReference ReadInt32 => _ReadInt32 ??= new MethodReference(nameof(ReadInt32), typeSystem.Int32, Reader) { HasThis = true, };

        public MethodReference ReadInt64 => _ReadInt64 ??= new MethodReference(nameof(ReadInt64), typeSystem.Int64, Reader) { HasThis = true, };

        public MethodReference ReadUInt16 => _ReadUInt16 ??= new MethodReference(nameof(ReadUInt16), typeSystem.UInt16, Reader) { HasThis = true, };

        public MethodReference ReadUInt32 => _ReadUInt32 ??= new MethodReference(nameof(ReadUInt32), typeSystem.UInt32, Reader) { HasThis = true, };

        public MethodReference ReadUInt64 => _ReadUInt64 ??= new MethodReference(nameof(ReadUInt64), typeSystem.UInt64, Reader) { HasThis = true, };

        public MethodReference ReadSingle => _ReadSingle ??= new MethodReference(nameof(ReadSingle), typeSystem.Single, Reader) { HasThis = true, };

        public MethodReference ReadDouble => _ReadDouble ??= new MethodReference(nameof(ReadDouble), typeSystem.Double, Reader) { HasThis = true, };

        public MethodReference Skip => _Skip ??= new MethodReference(nameof(Skip), typeSystem.Void, Reader) { HasThis = true, };

        public MethodReference TryReadNil => _TryReadNil ??= new MethodReference(nameof(TryReadNil), typeSystem.Boolean, Reader) { HasThis = true, };

        public MethodReference ReadMessagePackPrimitive(TypeReference typeReference)
        {
            return typeReference.FullName switch
            {
                "System.Byte" => ReadByte,
                "System.SByte" => ReadSByte,
                "System.UInt16" => ReadUInt16,
                "System.UInt32" => ReadUInt32,
                "System.UInt64" => ReadUInt64,
                "System.Int16" => ReadInt16,
                "System.Int32" => ReadInt32,
                "System.Int64" => ReadInt64,
                "System.Boolean" => ReadBoolean,
                "System.Char" => ReadChar,
                "System.Single" => ReadSingle,
                "System.Double" => ReadDouble,
                _ => throw new NotSupportedException(typeReference.FullName + " is not messagepack primitive type.")
            };
        }
    }
}
