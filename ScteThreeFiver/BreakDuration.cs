using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class BreakDuration
    {
        /*
         *  The break_duration() structure specifies the duration of the commercial Break(s). It may be used 
         *  to give the splicer an indication of when the Break will be over and when the network In Point
         *   will occur. 
        */

        public bool AutoReturn { get; set; }
        public UInt64 Duration { get; set; }

        public BreakDuration(byte[] rawData) 
        { 
            this.AutoReturn = (rawData[0] & 0b10000000) != 0;
            this.Duration = (ulong)(
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
    
        public double DurationSeconds { get
            {
                return (this.Duration / 90000.0);
            }

        }
    }
}
