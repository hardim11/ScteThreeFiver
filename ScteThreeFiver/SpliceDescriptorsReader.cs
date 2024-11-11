using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    //TODO refactor this as I don't think this is very pretty, I don't think that this should be a separate class
    public class SpliceDescriptorsReader
    {


        public const byte AvailDescriptor = 0x00;
        public const byte DTMFDescriptor = 0x01;
        public const byte SegmentationDescriptor = 0x02;
        public const byte TimeDescriptor = 0x03;
        public const byte AudioDescriptor = 0x04;

        // CUEIdentifier is 32-bit number used to identify the owner of the
        // descriptor. The identifier shall have a value of 0x43554549 (ASCII “CUEI”). 
        public const UInt32 CUEIdentifier = 0x43554549;
        // CUEIASCII is the CUEIIdentifier ASCII value
        public const string CUEIASCII = "CUEI";

        public SpliceDescriptorsReader()
        {
                
        }

        public static List<SpliceDescriptor> ReadSpliceDescriptors(UInt16 DescriptorLoopLength, byte[] rawData)
        {
            List<SpliceDescriptor> res = [];

            if (rawData.Length != DescriptorLoopLength) {
                throw new ArgumentException("incorrect length data");
            }

            int cursor = 0;

            // loop
            while (cursor < rawData.Length)
            {
                // read the tag, 
                byte descriptorType = rawData[cursor];
                byte descriptorLength = rawData[cursor + 1];


                // route to the appropriate type
                SpliceDescriptor newDesriptor;

                // copy the desciptor data 
                byte[] blockRaw = new byte[descriptorLength + 2];
                Buffer.BlockCopy(rawData, cursor, blockRaw, 0, descriptorLength + 2);
                newDesriptor = descriptorType switch
                {
                    SpliceDescriptorsReader.AvailDescriptor => SpliceDescriptorAvailDescriptor.ReadRawData(blockRaw),
                    SpliceDescriptorsReader.DTMFDescriptor => SpliceDescriptorDtmfDescriptor.ReadRawData(blockRaw),
                    SpliceDescriptorsReader.SegmentationDescriptor => SpliceDescriptorSegmentationDescriptor.ReadRawData(blockRaw),
                    SpliceDescriptorsReader.TimeDescriptor => SpliceDescriptorTimeDescriptor.ReadRawData(blockRaw),
                    SpliceDescriptorsReader.AudioDescriptor => SpliceDescriptorAudioDescriptor.ReadRawData(blockRaw),
                    _ => SpliceDescriptorPrivate.ReadRawData(blockRaw)
                };                
                // add to list
                res.Add(newDesriptor);
                // increment cursor
                cursor = cursor + newDesriptor.DescriptorLength + 2;
            }

            return res;
        }
    }
}
