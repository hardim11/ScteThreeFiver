using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class SpliceCommandSchedule : SpliceCommand
    {

        public byte SpliceCount {  get; set; }
        public List<SpliceScheduleEvent> Events { get; set; } = [];



        public SpliceCommandSchedule(byte[] rawData)
        {
            this.SpliceCount = rawData[0];

            int cursor = 1;
            for (int i = 1; i < this.SpliceCount; i++)
            {
                SpliceScheduleEvent newEvent = new();
                newEvent.SpliceEventID = (UInt32)
                (
                    (rawData[cursor] << 24)
                    + (rawData[cursor + 1] << 16)
                    + (rawData[cursor + 2] << 8)
                    + (rawData[cursor + 3] << 0)
                );
                cursor += 4;
                newEvent.SpliceEventCancelIndicator = Helpers.GetBit(rawData[cursor], 7) != 0;
                newEvent.EventIdComplianceflag = Helpers.GetBit(rawData[cursor], 6) != 0;
                cursor++;
                if (!newEvent.SpliceEventCancelIndicator)
                {
                    newEvent.OutOfNetworkIndicator = Helpers.GetBit(rawData[cursor], 7) != 0;
                    newEvent.ProgramSpliceFlag = Helpers.GetBit(rawData[cursor], 6) != 0;
                    newEvent.DurationFlag = Helpers.GetBit(rawData[cursor], 5) != 0;
                    cursor++;
                    if (newEvent.ProgramSpliceFlag)
                    {
                        newEvent.Program.UTCSpliceTime = (UInt32)
                        (
                            (rawData[cursor] << 24)
                            + (rawData[cursor + 1] << 16)
                            + (rawData[cursor + 2] << 8)
                            + (rawData[cursor + 3] << 0)
                        );
                        cursor += 4;
                    }
                    else
                    {
                        newEvent.ComponentCount = rawData[cursor];
                        cursor++;
                        for (int j = 0; j < newEvent.ComponentCount; j++)
                        {
                            SpliceScheduleComponent newComponent = new();
                            newComponent.ComponentTag = rawData[cursor++];
                            newComponent.UTCSpliceTime = (UInt32)
                            (
                                (rawData[cursor] << 24)
                                + (rawData[cursor + 1] << 16)
                                + (rawData[cursor + 2] << 8)
                                + (rawData[cursor + 3] << 0)
                            );
                            cursor += 4;
                            newEvent.Components.Add(newComponent);
                        }
                    }
                    newEvent.UniqueProgramId = (UInt16)(
                        (rawData[cursor] << 8)
                        + (rawData[cursor + 1] << 0)
                    );
                    cursor += 2;
                    newEvent.AvailNum = rawData[cursor++];
                    newEvent.AvailExpected = rawData[cursor++];
                }

                this.Events.Add(newEvent);
            }
        }
    }
}
