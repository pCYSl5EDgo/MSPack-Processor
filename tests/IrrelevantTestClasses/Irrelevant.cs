using MessagePack;

namespace IrrelevantTestClasses
{
    [MessagePackObject]
    public class IrrelevantType
    {
        [Key(0)] public string[] Names { get; } = new string[10];
    }
}
