using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class SpliceCommandPrivateCommand : SpliceCommand
    {
        public UInt32 Identifier { get; set; }
        public byte[] PrivateBytes {  get; set; }


        public SpliceCommandPrivateCommand(byte[] rawData)
        {
            this.Identifier = (UInt32)(
                (rawData[0] << 24)
                + (rawData[1] << 16)
                + (rawData[2] << 8)
                + (rawData[3] << 0)
                );
            this.PrivateBytes = new byte[rawData.Length - 1];
            Buffer.BlockCopy(rawData, 1, this.PrivateBytes, 0, (rawData.Length - 1));
        }
    }
}
