using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ScteThreeFiver
{
    public class SpliceCommandInsert : SpliceCommand
    {

        public UInt32 SpliceEventID { get; set; }
        public bool SpliceEventCancelIndicator {  get; set; }           // A 1-bit flag that, when set to ‘1’, indicates that a previously
                                                                        //   sent splice event, identified by splice_event_id, has been cancelled.
        public bool OutOfNetworkIndicator { get; set; }                 // A 1-bit flag that, when set to ‘1’, indicates that the splice event is
                                                                        //   an opportunity to exit from the network feed and that the value of splice_time(), as modified by
                                                                        //   pts_adjustment, shall refer to an intended Out Point or Program Out Point
        public bool ProgramSpliceFlag { get; set; }                     // A 1-bit flag that, when set to ‘1’, indicates that the message refers to a
                                                                        //   Program Splice Point and that the mode is the Program Splice Mode whereby all
                                                                        //   PIDs/components of the program are to be spliced.
        public bool DurationFlag { get; set; }                          // A 1-bit flag that, when set to ‘1’, indicates the presence of the break_duration() field
        public bool SpliceImmediateFlag { get; set; }                   // When this flag is ‘1’, it indicates the absence of the splice_time() field
                                                                        //   and that the splice mode shall be the Splice Immediate Mode
        public bool EventIdComplianceflag { get; set; }                 //
        public SpliceTime? SpliceTime { get; set; }                     // 
        public byte ComponentCount { get; set; }                        // An 8-bit unsigned integer that specifies the number of instances of
                                                                        //   elementary PID stream data in the loop that follows.
        public List<SpliceInsertComponent> ComponentTags { get; set; } = []; // 

        public BreakDuration? BreakDuration { get; set; }                // 

        public UInt16 UniqueProgramID { get; set; }                     // – This value should provide a unique identification for a viewing event within the service.
        public byte AvailNum { get; set; }                              // This field provides an identification for a specific avail within one unique_program_id.
        public byte AvailsExpected {  get; set; }                       // This field provides a count of the expected number of individual avails within the current viewing event


        public CompanySpecificInserts CompanySpecificInserts { get; set; } // this is for my company only and not part of the SCTE spec

        public SpliceCommandInsert(byte[] rawData) 
        {
            int cursor;
            this.SpliceEventID = (UInt32)
                (
                
                (rawData[0] << 24)
                + (rawData[1] << 16)
                + (rawData[2] << 8)
                + (rawData[3] << 0)
                );
            this.SpliceEventCancelIndicator = Helpers.GetBit(rawData[4], 7) != 0;

            if (!this.SpliceEventCancelIndicator)
            {
                // not a cancel so get details
                this.OutOfNetworkIndicator = Helpers.GetBit(rawData[5], 7) != 0;
                this.ProgramSpliceFlag = Helpers.GetBit(rawData[5], 6) != 0;
                this.DurationFlag = Helpers.GetBit(rawData[5], 5) != 0;
                this.SpliceImmediateFlag = Helpers.GetBit(rawData[5], 4) != 0;
                this.EventIdComplianceflag = Helpers.GetBit(rawData[5], 3) != 0;
                cursor = 6;
                if (this.ProgramSpliceFlag && !this.SpliceImmediateFlag)
                {
                    //splice_time()
                    if (Helpers.GetBit(rawData[cursor], 7) != 0)
                    {
                        this.SpliceTime = new()
                        {
                            TimeSpecifiedFlag = true,
                            PTSTime = (ulong)(
                            ((ulong)(rawData[cursor] & 0b00000001) << 32)
                            +
                            ((ulong)rawData[cursor + 1] << 24)
                            +
                            ((ulong)rawData[cursor + 2] << 16)
                            +
                            ((ulong)rawData[cursor + 3] << 8)
                            +
                            ((ulong)rawData[cursor + 4] << 0)
                            )
                        };
                        cursor += 5;
                    }
                    else
                    {
                        // nothing
                        cursor++;
                    }
                    
                }
                
                if (!this.ProgramSpliceFlag)
                {
                    // components
                    this.ComponentCount = rawData[cursor++];
                    for (int i = 0; i < this.ComponentCount; i++)
                    {
                        SpliceInsertComponent newComponent = new() { Tag = rawData[cursor++] };
                        if (!this.SpliceImmediateFlag)
                        {
                            newComponent.SpliceTime = new()
                            {
                                TimeSpecifiedFlag = (rawData[cursor] & 0b10000000) != 0
                            };
                            if (newComponent.SpliceTime.TimeSpecifiedFlag)
                            {
                                newComponent.SpliceTime.PTSTime = (ulong)(
                                    ((ulong)(rawData[cursor] & 0b00000001) << 32)
                                    +
                                    ((ulong)rawData[cursor + 1] << 24)
                                    +
                                    ((ulong)rawData[cursor + 2] << 16)
                                    +
                                    ((ulong)rawData[cursor + 3] << 8)
                                    +
                                    ((ulong)rawData[cursor + 4] << 0)
                                    );
                                cursor += 5;
                            }
                            else
                            {
                                cursor++;
                            }
                        }
                        this.ComponentTags.Add(newComponent);
                    }
                }


                if (this.DurationFlag)
                {
                    // break_duration()
                    byte[] durationRaw = new byte[5];
                    Buffer.BlockCopy(rawData, (cursor), durationRaw, 0, 5);
                    this.BreakDuration = new BreakDuration(durationRaw);
                    cursor += 5;
                }

                this.UniqueProgramID = (UInt16)(
                        (rawData[cursor ] << 8)
                        + (rawData[cursor + 1])
                    );

                this.AvailNum = rawData[cursor + 2];
                this.AvailsExpected = rawData[cursor + 3];

            }
            this.CompanySpecificInserts = CompanySpecificInserts.DecodeUpid(this.UniqueProgramID);
        }
    }
}
