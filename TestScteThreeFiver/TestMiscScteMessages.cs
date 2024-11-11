using ScteThreeFiver;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScteThreeFiver
{

    /// <summary>
    /// random SCTE commands I found on the interweb
    /// </summary>

    [TestClass]
    public class TestMiscScteMessages
    {
        [TestMethod]
        public void TestBitMovin()
        {
            // from https://bitmovin.com/blog/scte-35-guide/
            string base64 = "/DBcAAAAAAAAAP/wBQb//ciI8QBGAh1DVUVJXQk9EX+fAQ5FUDAxODAzODQwMDY2NiEEZAIZQ1VFSV0JPRF/3wABLit7AQVDMTQ2NDABAQEKQ1VFSQCAMTUwKnPhdcU=";
            Scte35 res = Scte35.DecoderBase64(base64);

            /*
            {
                "info_section": {
                    "table_id": "0xfc",
                    "section_syntax_indicator": false,
                    "private": false,
                    "sap_type": "0x03",
                    "sap_details": "No Sap Type",
                    "section_length": 92,
                    "protocol_version": 0,
                    "encrypted_packet": false,
                    "encryption_algorithm": 0,
                    "pts_adjustment": 0.0,
                    "cw_index": "0x00",
                    "tier": "0x0fff",
                    "splice_command_length": 5,
                    "splice_command_type": 6,
                    "descriptor_loop_length": 70,
                    "crc": "0x73e175c5"
                },
                "command": {
                    "command_length": 5,
                    "command_type": 6,
                    "name": "Time Signal",
                    "time_specified_flag": true,
                    "pts_time": 95030.502233
                },
                "descriptors": [
                    {
                        "tag": 2,
                        "descriptor_length": 29,
                        "name": "Segmentation Descriptor",
                        "identifier": "CUEI",
                        "segmentation_event_id": "0x5d093d11",
                        "segmentation_event_cancel_indicator": false,
                        "segmentation_event_id_compliance_indicator": true,
                        "program_segmentation_flag": true,
                        "segmentation_duration_flag": false,
                        "delivery_not_restricted_flag": false,
                        "web_delivery_allowed_flag": true,
                        "no_regional_blackout_flag": true,
                        "archive_allowed_flag": true,
                        "device_restrictions": "No Restrictions",
                        "segmentation_upid_type": 1,
                        "segmentation_upid_type_name": "Deprecated",
                        "segmentation_upid_length": 14,
                        "segmentation_upid": "EP018038400666",
                        "segmentation_type_id": 33,
                        "segment_num": 4,
                        "segments_expected": 100
                    },
                    {
                        "tag": 2,
                        "descriptor_length": 25,
                        "name": "Segmentation Descriptor",
                        "identifier": "CUEI",
                        "segmentation_event_id": "0x5d093d11",
                        "segmentation_event_cancel_indicator": false,
                        "segmentation_event_id_compliance_indicator": true,
                        "program_segmentation_flag": true,
                        "segmentation_duration_flag": true,
                        "delivery_not_restricted_flag": false,
                        "web_delivery_allowed_flag": true,
                        "no_regional_blackout_flag": true,
                        "archive_allowed_flag": true,
                        "device_restrictions": "No Restrictions",
                        "segmentation_duration": 220.033367,
                        "segmentation_upid_type": 1,
                        "segmentation_upid_type_name": "Deprecated",
                        "segmentation_upid_length": 5,
                        "segmentation_upid": "C1464",
                        "segmentation_type_id": 48,
                        "segment_num": 1,
                        "segments_expected": 1,
                        "sub_segment_num": 1,
                        "sub_segments_expected": 10
                    },
                    {
                        "tag": 1,
                        "descriptor_length": 10,
                        "name": "DTMF Descriptor",
                        "identifier": "CUEI",
                        "preroll": 0,
                        "dtmf_count": 4,
                        "dtmf_chars": [
                            "1",
                            "5",
                            "0",
                            "*"
                        ]
                    }
                ]
            }
             */


            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)92, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0x00, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);
            Assert.AreEqual(70, res.SpliceInfoSection.DescriptorLoopLength);


            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x73e175c5, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());

            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0x1fdc888f1, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual((ulong)70, res.SpliceInfoSection.DescriptorLoopLength);
            Assert.AreEqual(3, res.SpliceInfoSection.SpliceDescriptors.Count);

            // descriptor 0
            Assert.AreEqual(SpliceDescriptorsReader.SegmentationDescriptor, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(29, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual((uint)0x43554549, res.SpliceInfoSection.SpliceDescriptors[0].Identifier);
            Assert.AreEqual((uint)0x5d093d11, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);

            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ComponentCount);
            Assert.AreEqual((ulong)0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);


            Assert.AreEqual(1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(14, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            Assert.AreEqual(0x4, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0x64, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);
            Assert.AreEqual(1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(14, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("EP018038400666", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);
            Assert.AreEqual(33, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Chapter End", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);

            // descriptor 1
            Assert.AreEqual(SpliceDescriptorsReader.SegmentationDescriptor, res.SpliceInfoSection.SpliceDescriptors[1].Tag);
            Assert.AreEqual(25, res.SpliceInfoSection.SpliceDescriptors[1].DescriptorLength);
            Assert.AreEqual((uint)0x43554549, res.SpliceInfoSection.SpliceDescriptors[1].Identifier);
            Assert.AreEqual((uint)0x5d093d11, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeliveryNotRestrictedFlag);

            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeviceRestrictions);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ComponentCount);
            Assert.AreEqual((ulong)0x12e2b7b, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationDuration);


            Assert.AreEqual(1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidType);
            Assert.AreEqual(0x5, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidLength);
            Assert.AreEqual(1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentNumber);
            Assert.AreEqual(1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentsExpected);
            Assert.AreEqual(1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidType);
            Assert.AreEqual(0x5, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidLength);
            string upid_data1 = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("C1464", upid_data1);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentsExpected);
            Assert.AreEqual(48, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeId);
            Assert.AreEqual("Provider Advertisement Start", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeIdName.Message);

            // descriptor 2
            Assert.AreEqual(SpliceDescriptorsReader.DTMFDescriptor, res.SpliceInfoSection.SpliceDescriptors[2].Tag);
            Assert.AreEqual(10, res.SpliceInfoSection.SpliceDescriptors[2].DescriptorLength);
            Assert.AreEqual((uint)0x43554549, res.SpliceInfoSection.SpliceDescriptors[2].Identifier);
            Assert.AreEqual(0, ((SpliceDescriptorDtmfDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).PreRoll);
            Assert.AreEqual(4, ((SpliceDescriptorDtmfDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).DtmfCount);
            Assert.AreEqual(Convert.ToByte('1'), ((SpliceDescriptorDtmfDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).DtmfChars[0]);
            Assert.AreEqual(Convert.ToByte('5'), ((SpliceDescriptorDtmfDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).DtmfChars[1]);
            Assert.AreEqual(Convert.ToByte('0'), ((SpliceDescriptorDtmfDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).DtmfChars[2]);
            Assert.AreEqual(Convert.ToByte('*'), ((SpliceDescriptorDtmfDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).DtmfChars[3]);
        }


        [TestMethod]
        public void TestUsp1()
        {
            // from https://docs.unified-streaming.com/documentation/live/scte-35.html
            string base64 = "/DAlAAAAAA4QAP/wFAUAAAACf+/+ABjGMP4ADbugAAEBAQAAbRyXaw==";
            Scte35 res = Scte35.DecoderBase64(base64);

            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x25, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0xe10, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x14, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.SpliceInsertType);

            Assert.AreEqual(0, res.SpliceInfoSection.DescriptorLoopLength);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x6d1c976b, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandInsert", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual((UInt32)2, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventID);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).OutOfNetworkIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).ProgramSpliceFlag);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).DurationFlag);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceImmediateFlag);

            Assert.AreEqual(1, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).UniqueProgramID);
            Assert.AreEqual(1, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailNum);
            Assert.AreEqual(1, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailsExpected);
        }

        [TestMethod]
        public void TestUsp2()
        {
            // from https://docs.unified-streaming.com/documentation/live/scte-35.html
            string base64 = "/DAhAAAAAAAAAP/wEAUAAAfPf+9/fgAg9YDAAAAAAAA/APOv";
            Scte35 res = Scte35.DecoderBase64(base64);

            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x21, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x10, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.SpliceInsertType);

            Assert.AreEqual(0, res.SpliceInfoSection.DescriptorLoopLength);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x3f00f3af, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandInsert", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual((UInt32)0x7cf, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventID);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).OutOfNetworkIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).ProgramSpliceFlag);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).DurationFlag);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceImmediateFlag);

            Assert.AreEqual(0xc000, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).UniqueProgramID);
            Assert.AreEqual(0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailNum);
            Assert.AreEqual(0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailsExpected);
        }

        [TestMethod]
        public void TestUsp3()
        {
            // from https://docs.unified-streaming.com/documentation/live/scte-35.html
            string base64 = "/DAhAAAAAAAAAP/wEAUAAAfQf+9/fgAg9YDAAAAAAAA2Z7lO";
            Scte35 res = Scte35.DecoderBase64(base64);

            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)33, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x10, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.SpliceInsertType);

            Assert.AreEqual(0, res.SpliceInfoSection.DescriptorLoopLength);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x3667b94e, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandInsert", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual((UInt32)2000, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventID);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).OutOfNetworkIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).ProgramSpliceFlag);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).DurationFlag);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceImmediateFlag);

            Assert.AreEqual(0xc000, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).UniqueProgramID);
            Assert.AreEqual(0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailNum);
            Assert.AreEqual(0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailsExpected);
        }
    }
}
