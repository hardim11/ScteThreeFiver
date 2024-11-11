using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class SpliceScheduleEvent
    {
        public UInt32 SpliceEventID { get; set; }
        public bool SpliceEventCancelIndicator { get; set; }
        public bool EventIdComplianceflag { get; set; }      
        public bool OutOfNetworkIndicator {  get; set; }
        public bool ProgramSpliceFlag{ get; set; }
        public bool DurationFlag { get; set; }

        public SpliceScheduleEventProgram Program { get; set; }

        public byte ComponentCount { get; set; }
        public List<SpliceScheduleComponent> Components { get; set; } = [];


        public UInt16 UniqueProgramId { get; set; }
        public byte AvailNum { get; set; }
        public byte AvailExpected { get; set; }

        public SpliceScheduleEvent()
        {
            this.Program = new SpliceScheduleEventProgram();

        }
    }
}
