#define INTERNAL_NULLABLE_ATTRIBUTES
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NET45 || NET451 || NET452 || NET6 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
namespace System
{
    public readonly struct Index
    {
        public Index(int value, bool fromEnd = false)
        {
            Value = value;
            IsFromEnd = fromEnd;
        }

        public bool IsFromEnd { get; }

        public int Value { get; }

        public int GetOffset(int length)
        {
            return IsFromEnd ? length - Value : Value;
        }

        public static implicit operator Index(int value) => new Index(value);
    }
}
#endif
