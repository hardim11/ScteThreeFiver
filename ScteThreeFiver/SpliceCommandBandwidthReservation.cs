using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class SpliceCommandBandwidthReservation : SpliceCommand
    {
        // almost the same as null, nothing here to see
        /*
         * 
         * The bandwidth_reservation() command is provided for reserving bandwidth in a multiplex. A
        typical usage would be in a satellite delivery system that requires packets of a certain PID to
        always be present at the intended repetition rate to guarantee a certain bandwidth for that PID.
        This message differs from a splice_null() command so that it can easily be handled in a unique
        way by receiving equipment (i.e. removed from the multiplex by a satellite receiver). If a
        descriptor is sent with this command, it can not be expected that it will be carried through the
        entire transmission chain and it should be a private descriptor that is utilized only by the
        bandwidth reservation process.
        */
    }
}
