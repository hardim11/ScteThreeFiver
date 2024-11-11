using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class Helpers
    {
        // https://stackoverflow.com/questions/40541433/how-to-get-bit-values-from-byte
        // gets n'th bit from a byte, returns as an int BITNUMBER == zero based!
        public static int GetBit(byte b, int bitNumber)
        {
            return ((b >> bitNumber) & 0x01);
        }
    }
}
