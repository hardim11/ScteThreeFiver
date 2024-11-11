using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class SegmentationUpidTypeIds
    {
        public string Message { get; set; }
        public int SegmentNumber { get; set; }
        public int SegmentsExpected { get; set; }
        public int? SubSegmentNumber { get; set; }
        public int? SubSegmentsExpect {  get; set; }

        public SegmentationUpidTypeIds()
        {
            this.Message = string.Empty;
        }

        public static SegmentationUpidTypeIds GetSegmentationUpidTypeIdDetails(byte SegmentationUpidTypeId)
        {
            SegmentationUpidTypeIds res = new() { SegmentNumber = 0, SegmentsExpected = 0 };

            switch (SegmentationUpidTypeId)
            {
                case 0x00:
                    res.Message = "Not Indicated";
                    break;
                case 0x01:
                    res.Message = "Content Identification";
                    break;
                case 0x02:
                    res.Message = "Private";
                    break;
                case 0x10:
                    res.Message = "Program Start";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x11:
                    res.Message = "Program End";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x12:
                    res.Message = "Program Early Termination";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x13:
                    res.Message = "Program Breakaway";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x14:
                    res.Message = "Program Resumption";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x15:
                    res.Message = "Program Runover Planned";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x16:
                    res.Message = "Program Runover Unplanned";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x17:
                    res.Message = "Program Overlap Start";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x18:
                    res.Message = "Program Blackout Override";
                    break;
                case 0x19:
                    res.Message = "Program Join";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x1A:
                    res.Message = "Program Immediate Resumption";
                    break;
                case 0x20:
                    res.Message = "Chapter Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x21:
                    res.Message = "Chapter End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x22:
                    res.Message = "Break Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x23:
                    res.Message = "Break End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x24:
                    res.Message = "Opening Credit Start deprecated";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x25:
                    res.Message = "Opening Credit End deprecated";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x26:
                    res.Message = "Closing Credit Start deprecated";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x27:
                    res.Message = "Closing Credit End deprecated";
                    res.SegmentNumber = 1;
                    res.SegmentsExpected = 1;
                    break;
                case 0x30:
                    res.Message = "Provider Advertisement Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x31:
                    res.Message = "Provider Advertisement End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x32:
                    res.Message = "Distributor Advertisement Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x33:
                    res.Message = "Distributor Advertisement End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x34:
                    res.Message = "Provider Placement Opportunity Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break; 
                case 0x35:
                    res.Message = "Provider Placement Opportunity End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x36:
                    res.Message = "Distributor Placement Opportunity Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x37:
                    res.Message = "Distributor Placement Opportunity End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x38:
                    res.Message = "Provider Overlay Placement Opportunity Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x39:
                    res.Message = "Provider Overlay Placement Opportunity End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x3A:
                    res.Message = "Distributor Overlay Placement Opportunity Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x3B:
                    res.Message = "Distributor Overlay Placement Opportunity End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x3C:
                    res.Message = "Provider Promo Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x3D:
                    res.Message = "Provider Promo End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x3E:
                    res.Message = "Distributor Promo Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x3F:
                    res.Message = "Distributor Promo End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x40:
                    res.Message = "Unscheduled Event Start";
                    break;
                case 0x41:
                    res.Message = "Unscheduled Event End";
                    break;
                case 0x42:
                    res.Message = "Alternate Content Opportunity Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x43:
                    res.Message = "Alternate Content Opportunity End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x44:
                    res.Message = "Provider Ad Block Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x45:
                    res.Message = "Provider Ad Block End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x46:
                    res.Message = "Distributor Ad Block Start";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    res.SubSegmentNumber = 99;
                    res.SubSegmentsExpect = 99;
                    break;
                case 0x47:
                    res.Message = "Distributor Ad Block End";
                    res.SegmentNumber = 99;
                    res.SegmentsExpected = 99;
                    break;
                case 0x50:
                    res.Message = "Network Start";
                    break;
                case 0x51:
                    res.Message = "Network End";
                    break;
                default:
                    break;
            }

            return res;
        }
    }
}
