using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScteThreeFiver
{
    public class CompanySpecificSegmentationDescriptor
    {
        // decode the message body according to ???
        /*

            data type identifier : this shall be set to 0x01010101

            primaryEventid  : The productionId in the format xx/xxx/xxx#00x 

            boundaryTags:	In the format xx/xx, where ‘xx’ is a hex value from one of the following values:
                        Bumper start:  	0x10
                        Bumper end    	0x11
                        Promo start     	0x20
                        Promo end      	0x21
                        Sponsorship start	0x30
                        Sponsorship end	0x31
                        Programme start	0x40
                        Programme end 	0x41
                        Chapter start	0x50
                        Chapter end	0x51
                        Break start	0x60
                        Break end	0x61
                        Spot start	0x70
                        Spot end	0x71

            The following example is for a content marker at a Bumper start and Sponsorship end

            Example:   0x01010101,urn:uuid:c7451a50-e053-4b2a-ca37-bae1df2cf1c8,10/31  

         * 
        */
        public string Identifier { get; set; }
        public string PrimaryEventId { get; set; }
        public byte BoundaryFromId { get; set; }
        public string BoundaryFromString { get; set; }
        public byte BoundaryToId { get; set; }
        public string BoundaryToString { get; set; }
        public bool DecodedOk {  get; set; }


        public CompanySpecificSegmentationDescriptor() {
            this.Identifier = "";
            this.PrimaryEventId = "";
            this.BoundaryFromString = "";
            this.BoundaryToString = "";
            this.DecodedOk = false;
        }

        public static string GetBoundaryName(byte Id)
        {
            return Id switch
            {
                0x10 => "Bumper start",
                0x11 => "Bumper end",
                0x20 => "Promo start",
                0x21 => "Promo end",
                0x30 => "Sponsorship start",
                0x31 => "Sponsorship end",
                0x40 => "Programme start",
                0x41 => "Programme end",
                0x50 => "Chapter start",
                0x51 => "Chapter end",
                0x60 => "Break start",
                0x61 => "Break end",
                0x70 => "Spot start",
                0x71 => "Promo end",
                _ => "Unknown"
            };
        }

        public CompanySpecificSegmentationDescriptor(string Data)
        {
            // assume input of the style
            this.Identifier = "Unset";
            this.PrimaryEventId = "Unset";
            this.BoundaryFromString = "Unset";
            this.BoundaryToString = "Unset";

            // 0x01010101,urn:uuid:2bdf2f5a-de86-40d9-8e17-f6a6785f4f70,61/10
            char[] delim = [ ',' ]; 
            string[] parts = Data.Split(delim);
            if (parts.Length != 3) {
                return;
                //throw new ArgumentException("unable to decode (not 3 parts)");
            }

            string BoundaryRegex = @"^\d\d\/\d\d$";
            if (!Regex.IsMatch(parts[2], BoundaryRegex)){
                return;
                //throw new ArgumentException("unable to decode (boundary format wrong)");
            }

            if (parts[0] != "0x01010101")
            {
                return;
                //throw new ArgumentException("Not my company's prefix (not 0x01010101)");
            }

            this.Identifier = parts[0];
            this.PrimaryEventId = parts[1];

            delim = [ '/' ];
            string[] tmp = parts[2].Split(delim);
            if (tmp.Length != 2)
            {
                return;
                //throw new ArgumentException("content boundary does not include 2 parts!");
            }
            this.BoundaryFromId = byte.Parse(tmp[0], NumberStyles.HexNumber);
            this.BoundaryToId = byte.Parse(tmp[1], NumberStyles.HexNumber);
            this.BoundaryFromString = GetBoundaryName(this.BoundaryFromId);
            this.BoundaryToString = GetBoundaryName(this.BoundaryToId);
            this.DecodedOk = true;
        }

        public override string ToString()
        {
            return "Event ID: " + this.PrimaryEventId + ", " + this.BoundaryFromString + "(" + this.BoundaryFromId.ToString() + ") > " + this.BoundaryToString + "(" + this.BoundaryToId.ToString() + ")";
        }
    }
}
