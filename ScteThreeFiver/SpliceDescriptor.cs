namespace ScteThreeFiver
{
    public class SpliceDescriptor
    {
        public byte Tag {  get; set; }
        public int DataLength { get; set; }
        public byte DescriptorLength { get; set; }
        public UInt32 Identifier { get; set; }

        public SpliceDescriptor()
        {
                
        }
    }

}