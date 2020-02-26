using MessagePack;
using MessagePack.Formatters;

namespace Core.Test
{
    public sealed class Resolver : IFormatterResolver
    {
        public static readonly Resolver Instance = new Resolver();

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}