namespace ScteThreeFiver
{
    public class SpliceDescriptorTimeDescriptor : SpliceDescriptor
    {


        public UInt64 TAI_seconds {  get; set; } // 48 bits
        public UInt32 TAI_ns { get; set; }
        public UInt16 UtcOffset {  get; set; }

        public SpliceDescriptorTimeDescriptor()
        {

        }

        public static SpliceDescriptorTimeDescriptor ReadRawData(byte[] rawData)
        {
            SpliceDescriptorTimeDescriptor res = new();
            res.DataLength = rawData.Length;
            res.Tag = rawData[0];
            res.DescriptorLength = rawData[1];
            res.Identifier = (UInt32)(
                (rawData[2] << 24)
                + (rawData[3] << 16)
                + (rawData[4] << 8)
                + (rawData[5] << 0)
                );

            res.TAI_seconds = (UInt64)(
                (rawData[6] << 40)
                + (rawData[7] << 32)
                + (rawData[8] << 24)
                + (rawData[9] << 16)
                + (rawData[10] << 8)
                + (rawData[11] << 0)
                );
            
            res.TAI_ns = (UInt16)(
                (rawData[12] << 24)
                + (rawData[13] << 16)
                + (rawData[14] << 8)
                + (rawData[15] << 0)
            );

            res.UtcOffset = (UInt16)(
                (rawData[16] << 8)
                + (rawData[17] << 0)
            );

            return res;
        }
    }

}