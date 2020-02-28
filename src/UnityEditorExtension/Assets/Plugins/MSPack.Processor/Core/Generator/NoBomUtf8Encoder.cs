using System.Text;

namespace MSPack.Processor.Core
{
    public static class NoBomUtf8Encoder
    {
        public static readonly Encoding Encoding = new UTF8Encoding(false);
    }
}
