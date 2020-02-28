// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;

namespace MSPack.Processor.Core.Provider
{
    public sealed class MessagePackWriterHelper
    {
        private const string Write = nameof(Write);
        private const string WriteArrayHeader = nameof(WriteArrayHeader);
        private const string WriteMapHeader = nameof(WriteMapHeader);

        private readonly Func<SystemReadOnlySpanHelper> readOnlySpanHelperFunc;
        private readonly TypeSystem typeSystem;
#if CSHARP_8_0_OR_NEWER
        private MethodReference? writeNil;
        private MethodReference? writeByte;
        private MethodReference? writeUShort;
        private MethodReference? writeUInt;
        private MethodReference? writeULong;
        private MethodReference? writeSByte;
        private MethodReference? writeShort;
        private MethodReference? writeInt;
        private MethodReference? writeLong;
        private MethodReference? writeBoolean;
        private MethodReference? writeChar;
        private MethodReference? writeSingle;
        private MethodReference? writeDouble;
        private MethodReference? writeMapHeaderInt;
        private MethodReference? writeArrayHeaderInt;
        private MethodReference? writeRaw;
#else
        private MethodReference writeNil;
        private MethodReference writeByte;
        private MethodReference writeUShort;
        private MethodReference writeUInt;
        private MethodReference writeULong;
        private MethodReference writeSByte;
        private MethodReference writeShort;
        private MethodReference writeInt;
        private MethodReference writeLong;
        private MethodReference writeBoolean;
        private MethodReference writeChar;
        private MethodReference writeSingle;
        private MethodReference writeDouble;
        private MethodReference writeMapHeaderInt;
        private MethodReference writeArrayHeaderInt;
        private MethodReference writeRaw;
#endif

        public MessagePackWriterHelper(ModuleDefinition module, IMetadataScope messagePackScope, Func<SystemReadOnlySpanHelper> readOnlySpanHelperFunc)
        {
            this.readOnlySpanHelperFunc = readOnlySpanHelperFunc;
            typeSystem = module.TypeSystem;
            Writer = new TypeReference("MessagePack", "MessagePackWriter", module, messagePackScope, true);
        }

        public MethodReference WriteRaw
        {
            get
            {
                if (writeRaw is null)
                {
                    var readOnlySpanByte = readOnlySpanHelperFunc.Invoke().ReadOnlySpanGeneric(typeSystem.Byte);
                    writeRaw = new MethodReference(nameof(WriteRaw), typeSystem.Void, Writer)
                    {
                        HasThis = true,
                        Parameters =
                        {
                            new ParameterDefinition("rawMessagePackBlock", ParameterAttributes.None, readOnlySpanByte),
                        },
                    };
                }

                return writeRaw;
            }
        }

        public MethodReference WriteNil
        {
            get
            {
                if (writeNil == null)
                {
                    writeNil = CreateDefault(nameof(WriteNil));
                }

                return writeNil;
            }
        }

        public MethodReference WriteByte
        {
            get
            {
                if (writeByte == null)
                {
                    writeByte = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Byte);
                }

                return writeByte;
            }
        }

        public MethodReference WriteUShort
        {
            get
            {
                if (writeUShort == null)
                {
                    writeUShort = CreateDefault(nameof(Write)).AddParamValue(typeSystem.UInt16);
                }

                return writeUShort;
            }
        }

        public MethodReference WriteUInt
        {
            get
            {
                if (writeUInt == null)
                {
                    writeUInt = CreateDefault(nameof(Write)).AddParamValue(typeSystem.UInt32);
                }

                return writeUInt;
            }
        }

        public MethodReference WriteULong
        {
            get
            {
                if (writeULong == null)
                {
                    writeULong = CreateDefault(nameof(Write)).AddParamValue(typeSystem.UInt64);
                }

                return writeULong;
            }
        }

        public MethodReference WriteSByte
        {
            get
            {
                if (writeSByte == null)
                {
                    writeSByte = CreateDefault(nameof(Write)).AddParamValue(typeSystem.SByte);
                }

                return writeSByte;
            }
        }

        public MethodReference WriteShort
        {
            get
            {
                if (writeShort == null)
                {
                    writeShort = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Int16);
                }

                return writeShort;
            }
        }

        public MethodReference WriteInt
        {
            get
            {
                if (writeInt == null)
                {
                    writeInt = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Int32);
                }

                return writeInt;
            }
        }

        public MethodReference WriteLong
        {
            get
            {
                if (writeLong == null)
                {
                    writeLong = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Int64);
                }

                return writeLong;
            }
        }

        public MethodReference WriteBoolean
        {
            get
            {
                if (writeBoolean == null)
                {
                    writeBoolean = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Boolean);
                }

                return writeBoolean;
            }
        }

        public MethodReference WriteChar
        {
            get
            {
                if (writeChar == null)
                {
                    writeChar = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Char);
                }

                return writeChar;
            }
        }

        public MethodReference WriteSingle
        {
            get
            {
                if (writeSingle == null)
                {
                    writeSingle = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Single);
                }

                return writeSingle;
            }
        }

        public MethodReference WriteDouble
        {
            get
            {
                if (writeDouble == null)
                {
                    writeDouble = CreateDefault(nameof(Write)).AddParamValue(typeSystem.Double);
                }

                return writeDouble;
            }
        }

        public MethodReference WriteArrayHeaderInt
        {
            get
            {
                if (writeArrayHeaderInt == null)
                {
                    writeArrayHeaderInt = CreateDefault(WriteArrayHeader).AddParamCount();
                }

                return writeArrayHeaderInt;
            }
        }

        public MethodReference WriteMapHeaderInt
        {
            get
            {
                if (writeMapHeaderInt == null)
                {
                    writeMapHeaderInt = CreateDefault(WriteMapHeader).AddParamCount();
                }

                return writeMapHeaderInt;
            }
        }

        public TypeReference Writer { get; }

        private MethodReference CreateDefault(string name) => new MethodReference(name, typeSystem.Void, Writer) { HasThis = true, };

        public MethodReference WriteMessagePackPrimitive(TypeReference typeReference)
        {
            switch (typeReference.FullName)
            {
                case "System.Byte":
                    return WriteByte;
                case "System.SByte":
                    return WriteSByte;
                case "System.UInt16":
                    return WriteUShort;
                case "System.UInt32":
                    return WriteUInt;
                case "System.UInt64":
                    return WriteULong;
                case "System.Int16":
                    return WriteShort;
                case "System.Int32":
                    return WriteInt;
                case "System.Int64":
                    return WriteLong;
                case "System.Boolean":
                    return WriteBoolean;
                case "System.Char":
                    return WriteChar;
                case "System.Single":
                    return WriteSingle;
                case "System.Double":
                    return WriteDouble;
                default:
                    throw new NotSupportedException(typeReference.FullName + " is not messagepack primitive type.");
            }
        }
    }

    internal static class AddParamHelper
    {
        public static MethodReference AddParamCount(this MethodReference methodReference)
        {
            methodReference.Parameters.Add(new ParameterDefinition("count", ParameterAttributes.None, methodReference.Module.TypeSystem.Int32));
            return methodReference;
        }

        public static MethodReference AddParamLength(this MethodReference methodReference)
        {
            methodReference.Parameters.Add(new ParameterDefinition("length", ParameterAttributes.None, methodReference.Module.TypeSystem.Int32));
            return methodReference;
        }

        public static MethodReference AddParamValue(this MethodReference methodReference, TypeReference typeReference)
        {
            methodReference.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, typeReference));
            return methodReference;
        }
    }
}
