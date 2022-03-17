using SevenZip.Compression.LZMA;

namespace UsefulScripts.NetScripts.Data
{
    public static class CompressionS
    {
        public static byte[] CompressData<T>(T obj) where T : class
        {
            var input = ByteConvert.SerializeObject<T>(obj);
            return SevenZipHelper.Compress(input);
        }

        public static T RecompressData<T>(byte[] bytes) where T : class
        {
            var input = SevenZipHelper.Decompress(bytes);
            return ByteConvert.DeserializeObject<T>(input);
        }
    }
}