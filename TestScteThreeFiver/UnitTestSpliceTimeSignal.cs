using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using ScteThreeFiver;

namespace TestScteThreeFiver
{
    [TestClass]
    public class UnitTestSpliceTimeSignal
    {
        [TestMethod]
        public void TestMethod1Timesignal()
        {
            string base64_1 = "/DBqAAAAAA4QAP/wBQb/z0mAoABUAlJDVUVJAazhQn//AAACFmABPjB4MDEwMTAxMDEsdXJuOnV1aWQ6MmJkZjJmNWEtZGU4Ni00MGQ5LThlMTctZjZhNjc4NWY0ZjcwLDYxLzEwAQAAELpGQg==";
            Scte35 res = Scte35.DecoderBase64(base64_1);

            /*
                https://tools.middleman.tv/scte35-parser?base64=%252FDBqAAAAAA4QAP%252FwBQb%252Fz0mAoABUAlJDVUVJAazhQn%252F%252FAAACFmABPjB4MDEwMTAxMDEsdXJuOnV1aWQ6MmJkZjJmNWEtZGU4Ni00MGQ5LThlMTctZjZhNjc4NWY0ZjcwLDYxLzEwAQAAELpGQg%253D%253D&format=json
                {
                    table_id: "0xfc",
                    section_syntax_indicator: false,
                    private_indicator: false,
                    section_length: "0x6a",
                    protocol_version: "0x0",
                    encrypted_packet: false,
                    encryption_algorithm: "0x0",
                    pts_adjustment: "0xe10",
                    cw_index: "0x0",
                    tier: "0xfff",
                    splice_command_length: "0x5",
                    splice_command_type: "TimeSignal",
                    SpliceCommand: {
                        SpliceTime: {
                            time_specified_flag: true,
                            pts_time: "0x1cf4980a0"
                        }
                    },
                    descriptor_loop_length: "0x54",
                    SpliceDescriptors: [
                        {
                            splice_descriptor_tag: "SegmentationDescriptor",
                            descriptor_length: "0x52",
                            identifier: "0x43554549",
                            segmentation_event_id: "0x1ace142",
                            segmentation_event_cancel_indicator: false,
                            program_segmentation_flag: true,
                            segmentation_duration_flag: true,
                            delivery_not_restricted_flag: true,
                            web_delivery_allowed_flag: true,
                            no_regional_blackout_flag: true,
                            archive_allowed_flag: true,
                            device_restrictions: "None",
                            component_count: "0x0",
                            ComponentTags: [],
                            ComponentPtsOffsets: [],
                            segmentation_duration: "0x21660",
                            SegmentationUPIDs: [
                                {
                                segmentation_upid_type: "UserDefined",
                                segmentation_upid_length: "0x3e",
                                Data: "MHgwMTAxMDEwMSx1cm46dXVpZDoyYmRmMmY1YS1kZTg2LTQwZDktOGUxNy1mNmE2Nzg1ZjRmNzAsNjEvMTA=",
                                MPU: null,
                                SegmentationUPIDFormat: "NotSpecified",
                                Value: ""
                                }
                            ],
                            segmentation_type_id: "ContentIdentification",
                            segment_num: "0x0",
                            segments_expected: "0x0",
                            sub_segment_num: "0x0",
                            sub_segments_expected: "0x0"
                        }
                    ],
                    alignment_stuffing: null,
                    E_CRC_32: "0x0",
                    CRC_32: "0x10ba4642"
                }

                https://iodisco.com/cgi-bin/scte35decoder
                {
                "info_section": {
                    "table_id": "0xfc",
                    "section_syntax_indicator": false,
                    "private": false,
                    "sap_type": "0x03",
                    "sap_details": "No Sap Type",
                    "section_length": 106,
                    "protocol_version": 0,
                    "encrypted_packet": false,
                    "encryption_algorithm": 0,
                    "pts_adjustment": 0.04,
                    "cw_index": "0x00",
                    "tier": "0x0fff",
                    "splice_command_length": 5,
                    "splice_command_type": 6,
                    "descriptor_loop_length": 84,
                    "crc": "0x10ba4642"
                },
                "command": {
                    "command_length": 5,
                    "command_type": 6,
                    "name": "Time Signal",
                    "time_specified_flag": true,
                    "pts_time": 86362.978489
                },
                "descriptors": [
                    {
                        "tag": 2,
                        "descriptor_length": 82,
                        "name": "Segmentation Descriptor",
                        "identifier": "CUEI",
                        "segmentation_event_id": "0x01ace142",
                        "segmentation_event_cancel_indicator": false,
                        "segmentation_event_id_compliance_indicator": true,
                        "program_segmentation_flag": true,
                        "segmentation_duration_flag": true,
                        "delivery_not_restricted_flag": true,
                        "segmentation_duration": 1.52,
                        "segmentation_upid_type": 1,
                        "segmentation_upid_type_name": "Deprecated",
                        "segmentation_upid_length": 62,
                        "segmentation_upid": "0x01010101,urn:uuid:2bdf2f5a-de86-40d9-8e17-f6a6785f4f70,61/10",
                        "segmentation_type_id": 1,
                        "segment_num": 0,
                        "segments_expected": 0
                    }
                ]
            }

            */

            #region
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x6a, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0xe10, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);


            Assert.AreEqual(0x54, res.SpliceInfoSection.DescriptorLoopLength);


            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x10ba4642, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());

            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0x1cf4980a0, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);
            #endregion
            Assert.AreEqual((ulong)0x1cf4980a0, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);
            Assert.AreEqual(1, res.SpliceInfoSection.SpliceDescriptors.Count);
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(82, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x01ace142, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            //Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            //Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            //Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            //Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            Assert.AreEqual((ulong)0x21660, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            Assert.AreEqual(1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(0x3e, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("0x01010101,urn:uuid:2bdf2f5a-de86-40d9-8e17-f6a6785f4f70,61/10", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);

            CompanySpecificSegmentationDescriptor foo = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.CompanySpecificSegmentationDescriptor;
            Assert.AreEqual("0x01010101", foo.Identifier);
            Assert.AreEqual("urn:uuid:2bdf2f5a-de86-40d9-8e17-f6a6785f4f70", foo.PrimaryEventId);
            Assert.AreEqual(0x61, foo.BoundaryFromId);
            Assert.AreEqual(0x10, foo.BoundaryToId);
            Assert.AreEqual("Break end", foo.BoundaryFromString);
            Assert.AreEqual("Bumper start", foo.BoundaryToString);

            Assert.AreEqual(0x1, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Content Identification", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);
        }

    }
}