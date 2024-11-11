using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ScteThreeFiver
{
    public class Scte35
    {
        public SpliceInfoSection SpliceInfoSection { get; set; }
        public byte[] RawData { get; set; }

        public Scte35()
        {
            this.SpliceInfoSection = new SpliceInfoSection();
            this.RawData = [];
        }

        public static Scte35 DecoderBase64(string base64string)
        {
            byte[] bytes = Convert.FromBase64String(base64string);
            return DecoderByteArray(bytes);
        }

        public static Scte35 DecoderByteArray(byte[] bytes)
        {
            Scte35 result = new()
            {
                RawData = bytes,
                SpliceInfoSection = SpliceInfoSection.Decode(bytes)
            };

            return result;
        }
    }
}
