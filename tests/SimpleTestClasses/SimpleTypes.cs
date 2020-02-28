using System;
using MessagePack;

namespace SimpleTestClasses
{
  [MessagePackObject]
  public readonly struct SimpleTypeByte : IEquatable<SimpleTypeByte>
  {
    [Key(0)]
    private readonly Byte value;

    public SimpleTypeByte(Byte value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeByte other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeByte other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeSByte : IEquatable<SimpleTypeSByte>
  {
    [Key(0)]
    private readonly SByte value;

    public SimpleTypeSByte(SByte value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeSByte other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeSByte other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeInt16 : IEquatable<SimpleTypeInt16>
  {
    [Key(0)]
    private readonly Int16 value;

    public SimpleTypeInt16(Int16 value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeInt16 other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeInt16 other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeInt32 : IEquatable<SimpleTypeInt32>
  {
    [Key(0)]
    private readonly Int32 value;

    public SimpleTypeInt32(Int32 value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeInt32 other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeInt32 other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeInt64 : IEquatable<SimpleTypeInt64>
  {
    [Key(0)]
    private readonly Int64 value;

    public SimpleTypeInt64(Int64 value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeInt64 other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeInt64 other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeUInt16 : IEquatable<SimpleTypeUInt16>
  {
    [Key(0)]
    private readonly UInt16 value;

    public SimpleTypeUInt16(UInt16 value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeUInt16 other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeUInt16 other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeUInt32 : IEquatable<SimpleTypeUInt32>
  {
    [Key(0)]
    private readonly UInt32 value;

    public SimpleTypeUInt32(UInt32 value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeUInt32 other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeUInt32 other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeUInt64 : IEquatable<SimpleTypeUInt64>
  {
    [Key(0)]
    private readonly UInt64 value;

    public SimpleTypeUInt64(UInt64 value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeUInt64 other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeUInt64 other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeSingle : IEquatable<SimpleTypeSingle>
  {
    [Key(0)]
    private readonly Single value;

    public SimpleTypeSingle(Single value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeSingle other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeSingle other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeDouble : IEquatable<SimpleTypeDouble>
  {
    [Key(0)]
    private readonly Double value;

    public SimpleTypeDouble(Double value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeDouble other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeDouble other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeChar : IEquatable<SimpleTypeChar>
  {
    [Key(0)]
    private readonly Char value;

    public SimpleTypeChar(Char value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeChar other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeChar other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

  [MessagePackObject]
  public readonly struct SimpleTypeBoolean : IEquatable<SimpleTypeBoolean>
  {
    [Key(0)]
    private readonly Boolean value;

    public SimpleTypeBoolean(Boolean value)
    {
      this.value = value;
    }

    public bool Equals(SimpleTypeBoolean other)
    {
      return this.value == other.value;
    }

    public override bool Equals(object obj) => obj is SimpleTypeBoolean other && this.Equals(other);

    public override int GetHashCode() => value.GetHashCode();
  }

}