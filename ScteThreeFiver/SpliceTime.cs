using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    // SpliceTime specifies the time of the splice event.
    public class SpliceTime
    {
        // based upon Comcast's https://github.com/Comcast/scte35-go/blob/main/pkg/scte35/splice_time.go#L25
        public bool TimeSpecifiedFlag { get; set; }
        public UInt64? PTSTime {  get; set; }
    }
}
