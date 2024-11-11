using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class CompanySpecificInserts
    {
        public ushort TypeId { get; set; }
        public ushort BreakId { get; set; }
        public ushort ScteRegionId { get; set; }
        public ushort Upid { get; set; }


        public string TypeName
        {
            get
            {
                return TypeId switch
                {
                    0 => "BroadcastAdSlot",
                    1 => "PromotionalSlot",
                    2 => "SponsorSlot",
                    3 => "CommercialBreak",
                    _ => "unknown"
                };
            }
        }

        public override string ToString()
        {
            string res = "For UPID " + Upid.ToString() + "\n";
            res = res + "TypeId = " + TypeId.ToString() + " (" + TypeName + ")\n";
            res = res + "BreakId = " + BreakId.ToString() + "\n";
            res = res + "ScteRegionId = " + ScteRegionId.ToString() + "\n";

            return res;
        }

        public static CompanySpecificInserts DecodeUpid(string upid)
        {
            ushort tmp = ushort.Parse(upid);
            return DecodeUpid(tmp);
        }

        public static CompanySpecificInserts DecodeUpid(ushort upid)
        {
            // UPID => uimsbf == ushort
            // TTBBBBBBBBSSSSSS
            //   11111111000000
            // TT == type
            // BBBBBBBB == BREAK ID for Breaks
            // SSSSSS == SCTE REGION ID for Breaks
            ushort maskTT = (ushort)0b1100000000000000;
            ushort maskBB = (ushort)0b0011111111000000;
            ushort maskSS = (ushort)0b0000000000111111;

            ushort TT = (ushort)((upid & maskTT) >> 14);
            ushort BB = (ushort)((upid & maskBB) >> 6);
            ushort SS = (ushort)(upid & maskSS);

            return new CompanySpecificInserts
            {
                Upid = upid,
                TypeId = TT,
                ScteRegionId = SS,
                BreakId = BB
            };
        }

        public static ushort EncodeBreakId(ushort TypeId, ushort BreakId, ushort ScteRegionId)
        {
            // UPID => uimsbf == ushort
            // TTBBBBBBBBSSSSSS
            //   11111111000000
            // TT == type
            // BBBBBBBB == BREAK ID for Breaks
            // SSSSSS == SCTE REGION ID for Breaks

            // make sure is valid
            ushort tmpTT = (ushort)((TypeId & 0b11) << 14);
            ushort tmpBB = (ushort)((BreakId & 0b11111111) << 6);
            ushort tmpSS = (ushort)(ScteRegionId & 0b111111);

            return (ushort)(tmpTT + tmpBB + tmpSS);
        }
    }
}
