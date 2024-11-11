namespace ScteThreeFiver
{
    public class SpliceDescriptorAudioDescriptor : SpliceDescriptor
    {

        public byte AudioCount { get; set; }
        public List<AudioComponent> AudioComponents { get; set; }  = [];

        public SpliceDescriptorAudioDescriptor()
        {

        }

        public static SpliceDescriptorAudioDescriptor ReadRawData(byte[] rawData)
        {
            SpliceDescriptorAudioDescriptor res = new();
            res.DataLength = rawData.Length;
            res.Tag = rawData[0];
            res.DescriptorLength = rawData[1];
            res.Identifier = (UInt32)(
                (rawData[2] << 24)
                + (rawData[3] << 16)
                + (rawData[4] << 8)
                + (rawData[5] << 0)
                );
            res.AudioCount = (byte)(rawData[6] >> 4);
            int cursor = 8; // position of first component
            for (int i = 0; i < res.AudioCount; i++)
            {
                AudioComponent audioComponent = new();
                audioComponent.ComponentTag = rawData[cursor++];

                audioComponent.IsoTag[0] = rawData[cursor++];
                audioComponent.IsoTag[1] = rawData[cursor++];
                audioComponent.IsoTag[2] = rawData[cursor++];

                audioComponent.BitStreamMode = (byte)((rawData[cursor] & 0b11100000) >> 5);
                audioComponent.NumChannels = (byte)((rawData[cursor] & 0b00011110) >> 1);
                audioComponent.BitStreamMode = (byte)(rawData[cursor] & 0b00000001);

                res.AudioComponents.Add(audioComponent);
            }

            return res;
        }
    }

}