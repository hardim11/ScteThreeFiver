namespace ScteThreeFiver
{
    public class SpliceDescriptorDtmfDescriptor : SpliceDescriptor
    {

        public byte PreRoll {  get; set; }
        public byte DtmfCount { get; set; }
        public List<byte> DtmfChars { get; set; }  = [];

        public SpliceDescriptorDtmfDescriptor()
        {

        }

        public static SpliceDescriptorDtmfDescriptor ReadRawData(byte[] rawData)
        {
            SpliceDescriptorDtmfDescriptor res = new();
            res.DataLength = rawData.Length;
            res.Tag = rawData[0];
            res.DescriptorLength = rawData[1];
            res.Identifier = (UInt32)(
                (rawData[2] << 24)
                + (rawData[3] << 16)
                + (rawData[4] << 8)
                + (rawData[5] << 0)
                );
            res.PreRoll = rawData[6];
            res.DtmfCount = (byte)((rawData[7] & 0b11100000) >> 5);

            for (int i = 0; i < res.DtmfCount; i++)
            {
                byte aChar = rawData[i+8];
                res.DtmfChars.Add(aChar);
            }

            return res;
        }
    }

}