using MessagePack;
using MessagePack.Formatters;

namespace Core.Test
{
    public sealed class Resolver : IFormatterResolver
    {
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}