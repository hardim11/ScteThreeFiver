namespace ScteThreeFiver
{
    public class SpliceDescriptorAvailDescriptor : SpliceDescriptor
    {
        public UInt32 ProviderAvailId { get; set; }

        public SpliceDescriptorAvailDescriptor()
        {

        }

        public static SpliceDescriptorAvailDescriptor ReadRawData(byte[] rawData)
        {
            SpliceDescriptorAvailDescriptor res = new();
            res.DataLength = rawData.Length;
            res.Tag = rawData[0];
            res.DescriptorLength = rawData[1];
            res.Identifier = (UInt32)(
                (rawData[2] << 24)
                + (rawData[3] << 16)
                + (rawData[4] << 8)
                + (rawData[5] << 0)
                );
            res.ProviderAvailId = (UInt32)(
                (rawData[6] << 24)
                + (rawData[7] << 16)
                + (rawData[8] << 8)
                + (rawData[9] << 0)
                );
            return res;
        }
    }

}