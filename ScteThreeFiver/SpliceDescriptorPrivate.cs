namespace ScteThreeFiver
{
    public class SpliceDescriptorPrivate : SpliceDescriptor
    {
        //public byte Tag {  get; set; }
        //public int DataLength { get; set; }
        //public byte DescriptorLength { get; set; }
        //public UInt32 Identifier { get; set; }
        public byte[] Payload {  get; set; }

        public SpliceDescriptorPrivate()
        {
            this.Payload = [];
        }

        public static SpliceDescriptorPrivate ReadRawData(byte[] rawData)
        {
            SpliceDescriptorPrivate res = new();
            res.DataLength = rawData.Length;
            res.Tag = rawData[0];
            res.DescriptorLength = rawData[1];
            res.Identifier = (UInt32)(
                (rawData[2] << 24)
                + (rawData[3] << 16)
                + (rawData[4] << 8)
                + (rawData[5] << 0)
                );


            res.Payload = rawData;

            return res;
        }
    }

}