// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;
using MessagePack.Formatters;
using System;

namespace CompoundTestClasses
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(Formatter))]
    public class CustomSerializerClass0 : IEquatable<CustomSerializerClass0>
    {
        private int a;

        [IgnoreMember] public int A => a;

        public CustomSerializerClass0(int a)
        {
            this.a = a;
        }

        public sealed class Formatter : IMessagePackFormatter<CustomSerializerClass0>
        {
            public void Serialize(ref MessagePackWriter writer, CustomSerializerClass0 value, MessagePackSerializerOptions options)
            {
                if ((object)value == null)
                {
                    writer.WriteNil();
                    return;
                }

                writer.WriteArrayHeader(1U);
                writer.Write(value.a);
            }

            public CustomSerializerClass0 Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
            {
                if (reader.TryReadNil())
                {
                    return default;
                }

                var answer = default(CustomSerializerClass0);
                for (int index = 0, count = reader.ReadArrayHeader(); index < count; index++)
                {
                    switch (index)
                    {
                        case 0:
                            answer = new CustomSerializerClass0(reader.ReadInt32());
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }

                return answer;
            }
        }

        public bool Equals(CustomSerializerClass0 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return a == other.a;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomSerializerClass0)obj);
        }

        public override int GetHashCode()
        {
            return a;
        }
    }

    [MessagePackObject]
    [MessagePackFormatter(typeof(Formatter), 1, (byte)114, typeof(short))]
    public class CustomSerializerClass1 : IEquatable<CustomSerializerClass1>
    {
        private int a;

        [IgnoreMember] public int A => a;

        private CustomSerializerClass1() { }

        public CustomSerializerClass1(int a)
        {
            this.a = a;
        }

        public sealed class Formatter : IMessagePackFormatter<CustomSerializerClass1>
        {
            private readonly int a;
            private readonly byte b;
            private readonly Type type;

            public Formatter(int a, byte b, Type type)
            {
                this.a = a;
                this.b = b;
                this.type = type;
            }

            public void Serialize(ref MessagePackWriter writer, CustomSerializerClass1 value, MessagePackSerializerOptions options)
            {
                if ((object)value == null)
                {
                    writer.WriteNil();
                    return;
                }

                writer.WriteArrayHeader(1U);
                writer.Write(value.a);
            }

            public CustomSerializerClass1 Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
            {
                if (reader.TryReadNil())
                {
                    return default;
                }

                var answer = default(CustomSerializerClass1);
                for (int index = 0, count = reader.ReadArrayHeader(); index < count; index++)
                {
                    switch (index)
                    {
                        case 0:
                            answer = new CustomSerializerClass1(reader.ReadInt32());
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }

                return answer;
            }
        }

        public bool Equals(CustomSerializerClass1 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return a == other.a;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomSerializerClass1)obj);
        }

        public override int GetHashCode()
        {
            return a;
        }
    }
}