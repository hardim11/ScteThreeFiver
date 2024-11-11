using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Collections.Specialized.BitVector32;

namespace ScteThreeFiver
{
    public class SegmentationUpid
    {

        public int Length { get; set; }
        public byte Type { get; set; }
        public string TypeName { get { return GetTypeDetails(this.Type); } }
        public byte[]? Data { get; set; }
        public CompanySpecificSegmentationDescriptor CompanySpecificSegmentationDescriptor { get; set; }


        public SegmentationUpid() {

            this.CompanySpecificSegmentationDescriptor = new CompanySpecificSegmentationDescriptor();
        }

        public string GetDataFormatted()
        {
            if (Data == null)
            {
                return "";
            }

            // either send back HEX or string depending on the type ID
            byte[] stringTypes = [ 0x01, 0x03, 0x07, 0x09, 0x0E ];
            if (stringTypes.Contains(this.Type))
            {
                // return string
                return System.Text.Encoding.UTF8.GetString(Data);
            }
            else
            {
                //return base 64
                return Convert.ToBase64String(Data);
            }
        }

        static string GetTypeDetails(byte SegmentationUpidType)
        {
            return SegmentationUpidType switch
            {
                0x00 => "Not Used",
                0x01 => "User Defined",             // Deprecated: use type 0x0C; The
                                                    // segmentation_upid does not
                                                    // follow a standard naming scheme
                0x02 => "ISCI",                     // Deprecated: use type 0x03, 8
                                                    // characters; 4 alpha characters
                                                    // followed by 4 numbers
                                                    // ABCD1234
                0x03 => "Ad-ID",                    // Defined by the Advertising Digital Identification, LLC group. 12 characters; 4 alpha characters (company identification prefix) followed by 8 alphanumeric characters. (See[Ad - ID])
                                                    // e.g. ABCD0001000H [SMPTE 2092 - 1] 
                0x04 => "UMID",                     // 060A2B34.01010105.01010D20.13000000.D2C9036C.8F195343.AB7014D2.D718BFDA
                0x05 => "ISAN",                     // Deprecated: use type 0x06, ISO15706 binary encoding.
                0x06 => "ISAN",                     // Formerly known as V-ISAN. ISO 15706 - 2 binary encoding (“versioned” ISAN)
                                                    // e.g. 0000-0001-2C52-0000 - P - 0000 -0000 - 0
                0x07 => "TID",                      // Tribune Media Systems Program identifier. 12 characters; 2 alpha characters followed by 10 numbers
                                                    // e.g. MV0004146400
                0x08 => "TI",                       // AiringID (Formerly Turner ID), used to indicate a specific airing of a Program that is unique within a network
                                                    // e.g. 0x0A42235B81BC70FC (expressed as hexadecimal)
                0x09 => "ADI",                      // CableLabs metadata identifier as defined in Section 10.3.3.2.
                                                    // e.g. provider.com/MOVE1234567890123456
                0x0A => "EIDR",                     // An EIDR (see [EIDR]) represented in Compact Binary encoding as defined in Section 2.1.1 in EIDR ID Format(see [EIDR ID FORMAT])
                                                    // e.g. Content: 10.5240 / 0E4F892E-442F - 6BD4 -15B0 - 1 Video Service: 10.5239 / C370 - DCA5 [SMPTE 2079]
                0x0B => "ATSC Content Identifier",  // ATSC_content_identifier() structure as defined in [ATSCA/57B]
                0x0C => "MPU()",                    // Managed Private UPID structure as defined in Section 10.3.3.3.
                0x0D => "MID()",                    // Multiple UPID types structure as defined in Section 10.3.3.4.
                0x0E => "ADS Information",          // Advertising information as described in Section 10.3.3.5.
                                                    // e.g. type=LA&dur=60&pos=90&tier=1
                0x0F => "URI",                      // Universal Resource Identifier (see [RFC 3986]).
                0x10 => "UUID",                     // Universally Unique Identifier (see [RFC 4122]). This segmentation_upid_type can be used instead of an URI if it is desired to transfer the UUID payload only.
                                                    // e.g. 0xCB0350A948774CA7BB638730B37A98CF (expressed as hexadecimal)
                0x11 => "SCR",                      // Segment Content Reference as described in Section 10.3.3.6.
                                                    // e.g. type=PI&tier=1
                _ => "Reserved"                     // 0x12-0xFF
            };
        }

        public static SegmentationUpid ReadUpid(byte SegmentationUpidType, byte[] rawData)
        {
            SegmentationUpid res = new()
            {
                Length = rawData.Length,
                Type = SegmentationUpidType,
                Data = rawData
            };

            // is it my company?
            if (res.Type == 1)
            {
                res.CompanySpecificSegmentationDescriptor = new CompanySpecificSegmentationDescriptor(res.GetDataFormatted());
            }

            return res;
        }
    }
}
