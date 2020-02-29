using MessagePack;
using System;
using System.Globalization;
using System.Linq;

namespace SimpleTestClasses
{
    [Union(0, typeof(PrivateMemberClass))]
    [Union(1, typeof(PrivateMemberStruct))]
    public interface IB : IEquatable<IB>
    {
        int PublicB { get; }
    }

    [MessagePackObject]
    public class PrivateMemberClass : IB
    {
        private PrivateMemberClass() { }
        public PrivateMemberClass(int privateA, int publicB)
        {
            this.privateA = privateA;
            PublicB = publicB;
        }

        [Key(0)]
        private int privateA { get; }

        [Key(1)]
        public int PublicB { get; set; }

        protected bool Equals(PrivateMemberClass other)
        {
            return privateA == other.privateA && PublicB == other.PublicB;
        }

        public bool Equals(IB other) => other is PrivateMemberClass c && this.Equals(c);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PrivateMemberClass)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (privateA * 397) ^ PublicB;
            }
        }
    }

    [MessagePackObject]
    public struct PrivateMemberStruct : IB
    {
        [Key(0)]
        public string name;

        [Key(1)]
        public char[][] array;

        public PrivateMemberStruct(string name, int publicB)
        {
            this.name = name;
            PublicB = publicB;

            if (publicB <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(publicB), "publicB is " + publicB.ToString(CultureInfo.InvariantCulture));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            array = new char[publicB][];
            for (var i = 0; i < array.Length; i++)
            {
                ref var chars = ref array[i];
                chars = new char[i + 1];
                for (var j = 0; j < chars.Length; j++)
                {
                    if (name.Length <= j)
                    {
                        break;
                    }
                    chars[j] = name[j];
                }
            }
        }

        [Key(2)]
        public int PublicB { get; }

        public bool Equals(PrivateMemberStruct other)
        {
            if (name != other.name || PublicB != other.PublicB || array.Length != other.array.Length)
            {
                return false;
            }

            for (var index = 0; index < array.Length; index++)
            {
                var bytes0 = array[index];
                var bytes1 = other.array[index];
                if (!bytes0.SequenceEqual(bytes1))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Equals(IB other)
        {
            return other is PrivateMemberStruct s && Equals(s);
        }

        public override bool Equals(object obj)
        {
            return obj is PrivateMemberStruct other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (name != null ? name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (array != null ? array.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PublicB;
                return hashCode;
            }
        }
    }
}