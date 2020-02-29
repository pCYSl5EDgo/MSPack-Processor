// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;

namespace MSPack.Processor.Core.Provider
{
    public sealed class MessagePackReaderHelper
    {
        private readonly TypeSystem typeSystem;
        public readonly TypeReference Reader;
        public readonly TypeReference Nil;

#if CSHARP_8_0_OR_NEWER
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
#else
        private MethodReference _get_Depth;
        private MethodReference _set_Depth;

        private MethodReference _ReadArrayHeader;
        private MethodReference _ReadMapHeader;
        private MethodReference _ReadNil;
        private MethodReference _ReadBoolean;
        private MethodReference _ReadByte;
        private MethodReference _ReadChar;
        private MethodReference _ReadSByte;
        private MethodReference _ReadInt16;
        private MethodReference _ReadInt32;
        private MethodReference _ReadInt64;
        private MethodReference _ReadUInt16;
        private MethodReference _ReadUInt32;
        private MethodReference _ReadUInt64;
        private MethodReference _ReadSingle;
        private MethodReference _ReadDouble;
        private MethodReference _Skip;
        private MethodReference _TryReadNil;
#endif


        public MessagePackReaderHelper(ModuleDefinition module, IMetadataScope messagePackScope)
        {
            this.typeSystem = module.TypeSystem;
            Nil = new TypeReference("MessagePack", "Nil", module, messagePackScope, true);
            Reader = new TypeReference("MessagePack", "MessagePackReader", module, messagePackScope, true);
        }

        public MethodReference get_Depth
        {
            get
            {
                if (_get_Depth == null)
                {
                    _get_Depth = new MethodReference(nameof(get_Depth), typeSystem.Int32, Reader) { HasThis = true, };
                }

                return _get_Depth;
            }
        }

        public MethodReference set_Depth
        {
            get
            {
                if (_set_Depth == null)
                {
                    _set_Depth = new MethodReference(nameof(set_Depth), typeSystem.Void, Reader)
                    {
                        HasThis = true,
                        Parameters =
                        {
                            new ParameterDefinition("value", ParameterAttributes.None, typeSystem.Int32),
                        },
                    };
                }

                return _set_Depth;
            }
        }

        public MethodReference ReadArrayHeader
        {
            get
            {
                if (_ReadArrayHeader == null)
                {
                    _ReadArrayHeader = new MethodReference(nameof(ReadArrayHeader), typeSystem.Int32, Reader) { HasThis = true, };
                }

                return _ReadArrayHeader;
            }
        }

        public MethodReference ReadMapHeader
        {
            get
            {
                if (_ReadMapHeader == null)
                {
                    _ReadMapHeader = new MethodReference(nameof(ReadMapHeader), typeSystem.Int32, Reader) { HasThis = true, };
                }

                return _ReadMapHeader;
            }
        }

        public MethodReference ReadNil
        {
            get
            {
                if (_ReadNil == null)
                {
                    _ReadNil = new MethodReference(nameof(ReadNil), Nil, Reader) { HasThis = true, };
                }

                return _ReadNil;
            }
        }

        public MethodReference ReadBoolean
        {
            get
            {
                if (_ReadBoolean == null)
                {
                    _ReadBoolean = new MethodReference(nameof(ReadBoolean), typeSystem.Boolean, Reader) { HasThis = true, };
                }

                return _ReadBoolean;
            }
        }

        public MethodReference ReadByte
        {
            get
            {
                if (_ReadByte == null)
                {
                    _ReadByte = new MethodReference(nameof(ReadByte), typeSystem.Byte, Reader) { HasThis = true, };
                }

                return _ReadByte;
            }
        }

        public MethodReference ReadChar
        {
            get
            {
                if (_ReadChar == null)
                {
                    _ReadChar = new MethodReference(nameof(ReadChar), typeSystem.Char, Reader) { HasThis = true, };
                }

                return _ReadChar;
            }
        }

        public MethodReference ReadSByte
        {
            get
            {
                if (_ReadSByte == null)
                {
                    _ReadSByte = new MethodReference(nameof(ReadSByte), typeSystem.SByte, Reader) { HasThis = true, };
                }

                return _ReadSByte;
            }
        }

        public MethodReference ReadInt16
        {
            get
            {
                if (_ReadInt16 == null)
                {
                    _ReadInt16 = new MethodReference(nameof(ReadInt16), typeSystem.Int16, Reader) { HasThis = true, };
                }

                return _ReadInt16;
            }
        }

        public MethodReference ReadInt32
        {
            get
            {
                if (_ReadInt32 == null)
                {
                    _ReadInt32 = new MethodReference(nameof(ReadInt32), typeSystem.Int32, Reader) { HasThis = true, };
                }

                return _ReadInt32;
            }
        }

        public MethodReference ReadInt64
        {
            get
            {
                if (_ReadInt64 == null)
                {
                    _ReadInt64 = new MethodReference(nameof(ReadInt64), typeSystem.Int64, Reader) { HasThis = true, };
                }

                return _ReadInt64;
            }
        }

        public MethodReference ReadUInt16
        {
            get
            {
                if (_ReadUInt16 == null)
                {
                    _ReadUInt16 = new MethodReference(nameof(ReadUInt16), typeSystem.UInt16, Reader) { HasThis = true, };
                }

                return _ReadUInt16;
            }
        }

        public MethodReference ReadUInt32
        {
            get
            {
                if (_ReadUInt32 == null)
                {
                    _ReadUInt32 = new MethodReference(nameof(ReadUInt32), typeSystem.UInt32, Reader) { HasThis = true, };
                }

                return _ReadUInt32;
            }
        }

        public MethodReference ReadUInt64
        {
            get
            {
                if (_ReadUInt64 == null)
                {
                    _ReadUInt64 = new MethodReference(nameof(ReadUInt64), typeSystem.UInt64, Reader) { HasThis = true, };
                }

                return _ReadUInt64;
            }
        }

        public MethodReference ReadSingle
        {
            get
            {
                if (_ReadSingle == null)
                {
                    _ReadSingle = new MethodReference(nameof(ReadSingle), typeSystem.Single, Reader) { HasThis = true, };
                }

                return _ReadSingle;
            }
        }

        public MethodReference ReadDouble
        {
            get
            {
                if (_ReadDouble == null)
                {
                    _ReadDouble = new MethodReference(nameof(ReadDouble), typeSystem.Double, Reader) { HasThis = true, };
                }

                return _ReadDouble;
            }
        }

        public MethodReference Skip
        {
            get
            {
                if (_Skip == null)
                {
                    _Skip = new MethodReference(nameof(Skip), typeSystem.Void, Reader) { HasThis = true, };
                }

                return _Skip;
            }
        }

        public MethodReference TryReadNil
        {
            get
            {
                if (_TryReadNil == null)
                {
                    _TryReadNil = new MethodReference(nameof(TryReadNil), typeSystem.Boolean, Reader) { HasThis = true, };
                }

                return _TryReadNil;
            }
        }

        public MethodReference ReadMessagePackPrimitive(TypeReference typeReference)
        {
            switch (typeReference.FullName)
            {
                case "System.Byte":
                    return ReadByte;
                case "System.SByte":
                    return ReadSByte;
                case "System.UInt16":
                    return ReadUInt16;
                case "System.UInt32":
                    return ReadUInt32;
                case "System.UInt64":
                    return ReadUInt64;
                case "System.Int16":
                    return ReadInt16;
                case "System.Int32":
                    return ReadInt32;
                case "System.Int64":
                    return ReadInt64;
                case "System.Boolean":
                    return ReadBoolean;
                case "System.Char":
                    return ReadChar;
                case "System.Single":
                    return ReadSingle;
                case "System.Double":
                    return ReadDouble;
                default:
                    throw new NotSupportedException(typeReference.FullName + " is not messagepack primitive type.");
            }
        }
    }
}
