using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class SpliceCommandTimeSignal : SpliceCommand
    {
        public SpliceTime SpliceTime { get; set; } 

        public SpliceCommandTimeSignal(byte[] rawData)
        {
            this.SpliceTime = new();

            this.SpliceTime.TimeSpecifiedFlag = (rawData[0] & 0b10000000) != 0;

            if (this.SpliceTime.TimeSpecifiedFlag)
            {
                this.SpliceTime.PTSTime = (ulong)(
                ((ulong)(rawData[0] & 0b00000001) << 32)
                +
                ((ulong)rawData[1] << 24)
                +
                ((ulong)rawData[2] << 16)
                +
                ((ulong)rawData[3] << 8)
                +
                ((ulong)rawData[4] << 0)
                );
            }
        }
    }
}
