// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class MessagePackWriterHelper
    {
        private const string Write = nameof(Write);
        private const string WriteArrayHeader = nameof(WriteArrayHeader);
        private const string WriteMapHeader = nameof(WriteMapHeader);

        private readonly Func<SystemReadOnlySpanHelper> readOnlySpanHelperFunc;
        private readonly TypeSystem typeSystem;
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

        public MethodReference WriteNil => writeNil ??= CreateDefault(nameof(WriteNil));

        public MethodReference WriteByte => writeByte ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Byte);

        public MethodReference WriteUShort => writeUShort ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.UInt16);

        public MethodReference WriteUInt => writeUInt ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.UInt32);

        public MethodReference WriteULong => writeULong ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.UInt64);

        public MethodReference WriteSByte => writeSByte ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.SByte);

        public MethodReference WriteShort => writeShort ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Int16);

        public MethodReference WriteInt => writeInt ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Int32);

        public MethodReference WriteLong => writeLong ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Int64);

        public MethodReference WriteBoolean => writeBoolean ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Boolean);

        public MethodReference WriteChar => writeChar ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Char);

        public MethodReference WriteSingle => writeSingle ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Single);

        public MethodReference WriteDouble => writeDouble ??= CreateDefault(nameof(Write)).AddParamValue(typeSystem.Double);

        public MethodReference WriteArrayHeaderInt => writeArrayHeaderInt ??= CreateDefault(WriteArrayHeader).AddParamCount();

        public MethodReference WriteMapHeaderInt => writeMapHeaderInt ??= CreateDefault(WriteMapHeader).AddParamCount();

        public TypeReference Writer { get; }

        private MethodReference CreateDefault(string name) => new MethodReference(name, typeSystem.Void, Writer) { HasThis = true, };

        public MethodReference WriteMessagePackPrimitive(TypeReference typeReference)
        {
            return typeReference.FullName switch
            {
                "System.Byte" => WriteByte,
                "System.SByte" => WriteSByte,
                "System.UInt16" => WriteUShort,
                "System.UInt32" => WriteUInt,
                "System.UInt64" => WriteULong,
                "System.Int16" => WriteShort,
                "System.Int32" => WriteInt,
                "System.Int64" => WriteLong,
                "System.Boolean" => WriteBoolean,
                "System.Char" => WriteChar,
                "System.Single" => WriteSingle,
                "System.Double" => WriteDouble,
                _ => throw new NotSupportedException(typeReference.FullName + " is not messagepack primitive type.")
            };
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
