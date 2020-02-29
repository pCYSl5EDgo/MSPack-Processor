using System;
using System.Globalization;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text x;
    public Text y;
    public Text z;

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        StaticCompositeResolver.Instance.Register(new IFormatterResolver[]
        {
            Resolver.Instance,
            BuiltinResolver.Instance,
            StandardResolver.Instance,
        });
        var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);

        MessagePackSerializer.DefaultOptions = option;
    }

    [SerializeField] public A a;
    private byte[] serializedA;
    void Start()
    {
        serializedA = MessagePackSerializer.Serialize(a);
    }

    // Update is called once per frame
    void Update()
    {
        var a = MessagePackSerializer.Deserialize<A>(serializedA);
        x.text = a.X.ToString(CultureInfo.InvariantCulture);
        y.text = a.Y.ToString(CultureInfo.InvariantCulture);
        z.text = a.Z.ToString(CultureInfo.InvariantCulture);
    }
}

[Serializable]
[MessagePackObject]
public struct A
{
    [Key(0)]
    public int X;
    [Key(1)]
    public int Y;
    [Key(2)]
    public int Z;
}

public sealed class Resolver : IFormatterResolver
{
    public static readonly Resolver Instance = new Resolver();
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        return default;
    }
}
