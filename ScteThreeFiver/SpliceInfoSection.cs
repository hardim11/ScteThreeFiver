using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScteThreeFiver
{
    public class SpliceInfoSection
    {
        public byte TableId { get; set; }                   // This is an 8-bit field. Its value shall be 0xFC.
        public bool SectionSyntaxIndicator { get; set; }    // – The section_syntax_indicator is a 1-bit field that should always be
                                                            //   set to ‘0’, indicating that MPEG short sections are to be used.
        public bool PrivateIndicator { get; set; }          //  – This is a 1-bit flag that shall be set to 0.
        public UInt32 SAPType { get; set; }                 // A two-bit field that indicates if the content preparation system has created a Stream
                                                            // Access Point(SAP) at the signaled point in the stream
        public UInt16 SectionLength { get; set; }          // – This is a 12-bit field specifying the number of remaining bytes in the
                                                           //   splice_info_section, immediately following the section_length field up to the end of the
                                                           //   splice_info_section.The value in this field shall not exceed 4093.
        public byte ProtocolVersion { get; set; }           // An 8-bit unsigned integer field whose function is to allow, in the future, this
                                                            //   table type to carry parameters that may be structured differently than those defined in the current
                                                            //   protocol.At present, the only valid value for protocol_version protocolVersion[Optional;
                                                            //   xsd:unsignedByte] If is zero
        public bool EncryptedPacketFlag { get; set; }       // When this bit is set to ‘1’, it indicates that portions of the
                                                            //   splice_info_section, starting with splice_command_type and ending with and including
                                                            //   E_CRC_32, are encrypted. When this bit is set to ‘0’, no part of this message is encrypted.  
        public byte EncryptionAlgorithm { get; set; }      // This 6-bit unsigned integer specifies which encryption algorithm was
                                                           //   used to encrypt the current message.
        public UInt64 PTSAdjustment { get; set; }           // A 33-bit unsigned integer that appears in the clear and that shall be used by a
                                                            //   splicing device as an offset to be added to the(sometimes) encrypted pts_time field(s)
                                                            //   throughout this message, to obtain the intended splice time(s).
        public byte CWIndex { get; set; }                  // An 8-bit unsigned integer that conveys which control word (key) is to be used to
                                                           //   decrypt the message
        public UInt16 Tier { get; set; }                   // – A 12-bit value used by the SCTE 35 message provider to assign messages to authorization tiers.
        public UInt16 SpliceCommandLength { get; set; }     // - A 12-bit length of the splice command. The length shall represen
                                                            //   the number of bytes following the splice_command_type up to, but not including the
                                                            //   descriptor_loop_length.
        public byte SpliceCommandType { get; set; }        // An 8-bit unsigned integer which shall be assigned one of the values
                                                           //    shown in column labeled splice_command_type value in Table 7.
                                                           //        Command splice_command_type value XML Element
                                                           //        splice_null                 0x00   SpliceNull
                                                           //        Reserved                    0x01
                                                           //        Reserved                    0x02
                                                           //        Reserved                    0x03
                                                           //        splice_schedule             0x04   SpliceSchedule
                                                           //        splice_insert               0x05   SpliceInsert
                                                           //        time_signal                 0x06   TimeSignal
                                                           //        bandwidth_reservation       0x07   BandwidthReservation
                                                           //        Reserved                    0x08 - 0xfe
                                                           //        private_command             0xff    PrivateCommand

        public SpliceCommand SpliceCommand { get; set; }    // 
        public UInt16 DescriptorLoopLength { get; set; }   // A 16-bit unsigned integer specifying the number of bytes used in the
                                                           // splice descriptor loop immediately following.
        public List<SpliceDescriptor> SpliceDescriptors { get; set; }
        public byte AlignmentStuffing { get; set; }         // ???????????????
                                                            // When encryption is used, this field is a function of the particular encryption algorithm chosen

        public UInt32 Ecrc32 { get; set; }                  // This is a 32-bit field that contains the CRC value that gives a zero output of the
                                                            //    registers in the decoder defined in [MPEG Systems] after processing the entire decrypted portion
                                                            //    of the splice_info_section
        public UInt32 Crc32 { get; set; }                   // This is a 32-bit field that contains the CRC value that gives a zero output of the
                                                            //    registers in the decoder defined in [MPEG Systems] after processing the entire
                                                            //    splice_info_section, which includes the table_id field through the CRC_32 field.
        public UInt32 Crc32Calcutated { get; set; }
        public bool Crcvalid
        {
            get
            {
                return this.Crc32Calcutated == this.Crc32;
            }
        }
        public bool Ecrcvalid { get; set; }


        public SpliceInfoSection()
        {
            this.SpliceDescriptors = [];
            this.SpliceCommand = new SpliceCommandNull();
        }

        public static SpliceInfoSection Decode(byte[] bytes)
        {
            SpliceInfoSection section = new();

            section.TableId = bytes[0];
            section.SectionSyntaxIndicator = Helpers.GetBit(bytes[1], 7) != 0;
            section.PrivateIndicator = Helpers.GetBit(bytes[1], 6) != 0;
            // byte[1] xx11xxxx == && 0b00110000 >> 4
            section.SAPType = (byte)((bytes[1] & 0b00110000) >> 4);
            // this is the next 4 + 8 bits
            section.SectionLength = (ushort)(((bytes[1] & 0b00001111) << 8) + bytes[2]);
            section.ProtocolVersion = bytes[3];
            section.EncryptedPacketFlag = Helpers.GetBit(bytes[4], 7) != 0;
            section.EncryptionAlgorithm = (byte)((bytes[4] & 0b01111110) >> 1);
            //TODO actual encryption decrytion

            // PTS Adjustment = byte[4] lsb + next 4 bytes
            section.PTSAdjustment = (ulong)(
                ((ulong)(bytes[4] & 0b00000001) << 32)
                +
                (ulong)(bytes[5] << 24)
                +
                (ulong)(bytes[6] << 16)
                +
                (ulong)(bytes[7] << 8)
                +
                (ulong)(bytes[8] << 0)
                );
            section.CWIndex = bytes[9];
            section.Tier = (ushort)((bytes[10] << 4) + ((bytes[11] & 0b11110000) >> 4));
            section.SpliceCommandLength = (ushort)(((bytes[11] & 0b00001111) << 4) + (bytes[12]));
            section.SpliceCommandType = bytes[13];

            // we now need to read the command

            // copy the data from byte 14 -> SpliceCommandLength
            byte[] commandData = new byte[section.SpliceCommandLength];
            Buffer.BlockCopy(bytes, 14, commandData, 0, section.SpliceCommandLength);

            section.SpliceCommand = section.SpliceCommandType switch
            {
                SpliceCommand.SpliceNullType => new SpliceCommandNull(),
                SpliceCommand.SpliceScheduleType => new SpliceCommandSchedule(commandData),
                SpliceCommand.SpliceInsertType => new SpliceCommandInsert(commandData),
                SpliceCommand.TimeSignalType => new SpliceCommandTimeSignal(commandData),
                SpliceCommand.BandwidthReservationType => new SpliceCommandBandwidthReservation(),
                _ => new SpliceCommandPrivateCommand(commandData)
            };

            // now we read the descriptor length 
            ushort descriptorLengthPosition = (ushort)(14 + section.SpliceCommandLength);
            section.DescriptorLoopLength = (ushort)((bytes[descriptorLengthPosition] << 8) + bytes[descriptorLengthPosition + 1]);

            // copy the data from byte 14 -> SpliceCommandLength
            byte[] descriptorData = new byte[section.DescriptorLoopLength];
            Buffer.BlockCopy(bytes, (descriptorLengthPosition + 2), descriptorData, 0, section.DescriptorLoopLength);

            // we now need to read the descriptors
            section.SpliceDescriptors = SpliceDescriptorsReader.ReadSpliceDescriptors(section.DescriptorLoopLength, descriptorData);

            ushort endPartCursor = (ushort)(descriptorLengthPosition + 2 + section.DescriptorLoopLength);
            // next ECRC
            if (section.EncryptedPacketFlag)
            {
                // decode the ECRC
                section.Ecrc32 = (uint)((
                    bytes[endPartCursor] << 24)
                    +
                    (bytes[endPartCursor + 1] << 16)
                    +
                    (bytes[endPartCursor + 2] << 8)
                    +
                    (bytes[endPartCursor + 3] << 0)
                );


                //TODO ECRC
                //                 endPartCursor = (ushort)(endPartCursor + 4)

                // according to spec, ECRC is the CRC of the after processing the entire decrypted portion of the
                // splice_info_section.

                throw new NotImplementedException();
            }
            else
            {
                section.Ecrc32 = 0;
            }

            // finally CRC
            section.Crc32 = (uint)((
                bytes[endPartCursor] << 24)
                +
                (bytes[endPartCursor + 1] << 16)
                +
                (bytes[endPartCursor + 2] << 8)
                +
                (bytes[endPartCursor + 3] << 0)
            );


            byte[] crcData = new byte[bytes.Length - 4];
            Buffer.BlockCopy(bytes, 0, crcData, 0, bytes.Length - 4);
            section.Crc32Calcutated = CrcMpeg.CalculateChecksumComcast(crcData);

            return section;
        }
    }

}
