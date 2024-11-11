using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ScteThreeFiver;
using System.Buffers.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TestScteThreeFiver
{
    [TestClass]
    public class UnitTestScteSpecExamples
    {
        /// <summary>
        /// 14.1. time_signal – Placement Opportunity Start
        /// The programmer is also using the Segment number in a non-normative
        /// manner to indicate that there are Distributor Placement Opportunities within this Provider Placement
        /// Opportunity.A standardized method of doing this would be to use the MID format of the segmentation
        /// UPID type and insert a UPID type 0x0E (ADS) with this information.
        /// </summary>
        [TestMethod]
        public void Test1TimesignalPlacementOpportunityStart()
        {

            //why is this one failing? It seems to read too far

            string base64_1 = "/DA0AAAAAAAA///wBQb+cr0AUAAeAhxDVUVJSAAAjn/PAAGlmbAICAAAAAAsoKGKNAIAmsnRfg==";
            Scte35 res = Scte35.DecoderBase64(base64_1);

            /*

                https://tools.middleman.tv/scte35-parser?base64=%252FDAlAAAAAA4QAP%252FwFAUwLMaof%252B%252F%252Fz0uXAP4AUmXAwlQBAQAAm2zCrQ%253D%253D&format=json
                {
                    table_id: "0xfc",
                    section_syntax_indicator: false,
                    private_indicator: false,
                    section_length: "0x34",
                    protocol_version: "0x0",
                    encrypted_packet: false,
                    encryption_algorithm: "0x0",
                    pts_adjustment: "0x0",
                    cw_index: "0xff",
                    tier: "0xfff",
                    splice_command_length: "0x5",
                    splice_command_type: "TimeSignal",
                    SpliceCommand: {
                        SpliceTime: {
                            time_specified_flag: true,
                            pts_time: "0x72bd0050"
                        }
                    },
                    descriptor_loop_length: "0x1e",
                    SpliceDescriptors: [
                        {
                            splice_descriptor_tag: "SegmentationDescriptor",
                            descriptor_length: "0x1e",
                            identifier: "0x43554549",
                            segmentation_event_id: "0x4800008e",
                            segmentation_event_cancel_indicator: false,
                            program_segmentation_flag: true,
                            segmentation_duration_flag: true,
                            delivery_not_restricted_flag: false,
                            web_delivery_allowed_flag: false,
                            no_regional_blackout_flag: true,
                            archive_allowed_flag: true,
                            device_restrictions: "None",
                            component_count: "0x0",
                            ComponentTags: [],
                            ComponentPtsOffsets: [],
                            segmentation_duration: "0x1a599b0",
                            SegmentationUPIDs: [
                                {
                                    segmentation_upid_type: "TI",
                                    segmentation_upid_length: "0x8",
                                    Data: "AAAAACygoYo=",
                                    MPU: null,
                                    SegmentationUPIDFormat: "NotSpecified",
                                    Value: ""
                                }
                            ],
                            segmentation_type_id: "ProviderPlacementOpportunityStart",
                            segment_num: "0x2",
                            segments_expected: "0x0",
                            sub_segment_num: "0x0",
                            sub_segments_expected: "0x0"
                        }
                    ],
                    alignment_stuffing: null,
                    E_CRC_32: "0x0",
                    CRC_32: "0x9ac9d17e"
                }


                https://iodisco.com/cgi-bin/scte35decoder
                {
                    "info_section": {
                        "table_id": "0xfc",
                        "section_syntax_indicator": false,
                        "private": false,
                        "sap_type": "0x03",
                        "sap_details": "No Sap Type",
                        "section_length": 52,
                        "protocol_version": 0,
                        "encrypted_packet": false,
                        "encryption_algorithm": 0,
                        "pts_adjustment": 0.0,
                        "cw_index": "0xff",
                        "tier": "0x0fff",
                        "splice_command_length": 5,
                        "splice_command_type": 6,
                        "descriptor_loop_length": 30,
                        "crc": "0x9ac9d17e"
                    },
                    "command": {
                        "command_length": 5,
                        "command_type": 6,
                        "name": "Time Signal",
                        "time_specified_flag": true,
                        "pts_time": 21388.766756
                    },
                    "descriptors": [
                        {
                            "tag": 2,
                            "descriptor_length": 28,
                            "name": "Segmentation Descriptor",
                            "identifier": "CUEI",
                            "segmentation_event_id": "0x4800008e",
                            "segmentation_event_cancel_indicator": false,
                            "segmentation_event_id_compliance_indicator": true,
                            "program_segmentation_flag": true,
                            "segmentation_duration_flag": true,
                            "delivery_not_restricted_flag": false,
                            "web_delivery_allowed_flag": false,
                            "no_regional_blackout_flag": true,
                            "archive_allowed_flag": true,
                            "device_restrictions": "No Restrictions",
                            "segmentation_duration": 307.0,
                            "segmentation_upid_type": 8,
                            "segmentation_upid_type_name": "AiringID",
                            "segmentation_upid_length": 8,
                            "segmentation_upid": "0x2ca0a18a",
                            "segmentation_type_id": 52,
                            "segment_num": 2,
                            "segments_expected": 0,
                            "sub_segment_num": 0,
                            "sub_segments_expected": 0
                        }
                    ]
                }

            */
            #region
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x34, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);


            Assert.AreEqual(0x1e, res.SpliceInfoSection.DescriptorLoopLength);


            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x9ac9d17e, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);


            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());

            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0x72bd0050, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            #endregion
            Assert.AreEqual(1, res.SpliceInfoSection.SpliceDescriptors.Count);
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(28, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x4800008e, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(2, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);
            Assert.AreEqual("AAAAACygoYo=", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted());

            Assert.AreEqual(52, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Provider Placement Opportunity Start", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);

        }

        /// <summary>
        /// 14.2. splice_insert
        /// </summary>
        [TestMethod]
        public void Test2SpliceInsert()
        {
            string base64_1 = "/DAvAAAAAAAA///wFAVIAACPf+/+c2nALv4AUsz1AAAAAAAKAAhDVUVJAAABNWLbowo=";
            Scte35 res = Scte35.DecoderBase64(base64_1);
            /*
             * https://tools.middleman.tv/scte35-parser?base64=%252FDAvAAAAAAAA%252F%252F%252FwFAVIAACPf%252B%252F%252Bc2nALv4AUsz1AAAAAAAKAAhDVUVJAAABNWLbowo%253D&format=json
             
                {
                    table_id: "0xfc",
                    section_syntax_indicator: false,
                    private_indicator: false,
                    section_length: "0x2f",
                    protocol_version: "0x0",
                    encrypted_packet: false,
                    encryption_algorithm: "0x0",
                    pts_adjustment: "0x0",
                    cw_index: "0xff",
                    tier: "0xfff",
                    splice_command_length: "0x14",
                    splice_command_type: "SpliceInsert",
                    SpliceCommand: {
                    splice_event_id: "0x4800008f",
                    splice_event_cancel_indicator: false,
                    out_of_network_indicator: true,
                    program_splice_flag: true,
                    duration_flag: true,
                    splice_immediate_flag: false,
                    splice_time(): {
                        time_specified_flag: true,
                        pts_time: "0x7369c02e"
                    },
                    component_count: "0x0",
                    ComponentTags: null,
                    break_duration(): {
                        auto_return: true,
                        duration: "0x52ccf5"
                    },
                    unique_program_id: "0x0",
                    avail_num: "0x0",
                    avails_expected: "0x0"
                    },
                    descriptor_loop_length: "0xa",
                    SpliceDescriptors: [
                        {
                            splice_descriptor_tag: "AvailDescriptor",
                            descriptor_length: "0x8",
                            identifier: "0x43554549",
                            provider_avail_id: "0x135"
                        }
                    ],
                    alignment_stuffing: null,
                    E_CRC_32: "0x0",
                    CRC_32: "0x62dba30a"
                }
                
                https://iodisco.com/cgi-bin/scte35decoder
                {
                    "info_section": {
                        "table_id": "0xfc",
                        "section_syntax_indicator": false,
                        "private": false,
                        "sap_type": "0x03",
                        "sap_details": "No Sap Type",
                        "section_length": 47,
                        "protocol_version": 0,
                        "encrypted_packet": false,
                        "encryption_algorithm": 0,
                        "pts_adjustment": 0.0,
                        "cw_index": "0xff",
                        "tier": "0x0fff",
                        "splice_command_length": 20,
                        "splice_command_type": 5,
                        "descriptor_loop_length": 10,
                        "crc": "0x62dba30a"
                    },
                    "command": {
                        "command_length": 20,
                        "command_type": 5,
                        "name": "Splice Insert",
                        "time_specified_flag": true,
                        "pts_time": 21514.559089,
                        "break_auto_return": true,
                        "break_duration": 60.293567,
                        "splice_event_id": 1207959695,
                        "splice_event_cancel_indicator": false,
                        "out_of_network_indicator": true,
                        "program_splice_flag": true,
                        "duration_flag": true,
                        "splice_immediate_flag": false,
                        "event_id_compliance_flag": true,
                        "unique_program_id": 0,
                        "avail_num": 0,
                        "avails_expected": 0
                    },
                    "descriptors": [
                        {
                            "tag": 0,
                            "descriptor_length": 8,
                            "name": "Avail Descriptor",
                            "identifier": "CUEI",
                            "provider_avail_id": 309
                        }
                    ]
                }
             * 
             */
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)47, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x14, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.SpliceInsertType);


            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x62dba30a, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandInsert", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual((UInt32)0x4800008f, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventID);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).OutOfNetworkIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).ProgramSpliceFlag);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).DurationFlag);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceImmediateFlag);
            Assert.IsNotNull(((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceTime);
            #pragma warning disable CS8602
            Assert.AreEqual((ulong)0x7369c02e, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.AutoReturn);
            Assert.AreEqual((ulong)0x52ccf5, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.Duration);

            Assert.AreEqual(0x0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).UniqueProgramID);
            Assert.AreEqual(0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailNum);
            Assert.AreEqual(0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailsExpected);

            Assert.AreEqual(0xa, res.SpliceInfoSection.DescriptorLoopLength);
            Assert.AreEqual(1, res.SpliceInfoSection.SpliceDescriptors.Count);
            Assert.AreEqual(10, res.SpliceInfoSection.SpliceDescriptors[0].DataLength);
            Assert.AreEqual(0, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorAvailDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((UInt32)0x135, ((SpliceDescriptorAvailDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProviderAvailId);

        }


        /// <summary>
        /// 14.3. time_signal – Placement Opportunity End
        /// </summary>
        [TestMethod]
        public void Test3TimesignalPlacementOpportunityEnd()
        {
            string base64_1 = "/DAvAAAAAAAA///wBQb+dGKQoAAZAhdDVUVJSAAAjn+fCAgAAAAALKChijUCAKnMZ1g=";
            Scte35 res = Scte35.DecoderBase64(base64_1);

            /*
                    {
                        table_id: "0xfc",
                        section_syntax_indicator: false,
                        private_indicator: false,
                        section_length: "0x2f",
                        protocol_version: "0x0",
                        encrypted_packet: false,
                        encryption_algorithm: "0x0",
                        pts_adjustment: "0x0",
                        cw_index: "0xff",
                        tier: "0xfff",
                        splice_command_length: "0x5",
                        splice_command_type: "TimeSignal",
                        SpliceCommand: {
                            SpliceTime: {
                                time_specified_flag: true,
                                pts_time: "0x746290a0"
                            }
                        },
                        descriptor_loop_length: "0x19",
                        SpliceDescriptors: [
                            {
                                splice_descriptor_tag: "SegmentationDescriptor",
                                descriptor_length: "0x17",
                                identifier: "0x43554549",
                                segmentation_event_id: "0x4800008e",
                                segmentation_event_cancel_indicator: false,
                                program_segmentation_flag: true,
                                segmentation_duration_flag: false,
                                delivery_not_restricted_flag: false,
                                web_delivery_allowed_flag: true,
                                no_regional_blackout_flag: true,
                                archive_allowed_flag: true,
                                device_restrictions: "None",
                                component_count: "0x0",
                                ComponentTags: [],
                                ComponentPtsOffsets: [],
                                SegmentationUPIDs: [
                                    {
                                        segmentation_upid_type: "TI",
                                        segmentation_upid_length: "0x8",
                                        Data: "AAAAACygoYo=",
                                        MPU: null,
                                        SegmentationUPIDFormat: "NotSpecified",
                                        Value: ""
                                    }
                                ],
                                segmentation_type_id: "ProviderPlacementOpportunityEnd",
                                segment_num: "0x2",
                                segments_expected: "0x0",
                                sub_segment_num: "0x0",
                                sub_segments_expected: "0x0"
                            }
                        ],
                        alignment_stuffing: null,
                        E_CRC_32: "0x0",
                        CRC_32: "0xa9cc6758"
                    }


                    {
                        "info_section": {
                            "table_id": "0xfc",
                            "section_syntax_indicator": false,
                            "private": false,
                            "sap_type": "0x03",
                            "sap_details": "No Sap Type",
                            "section_length": 47,
                            "protocol_version": 0,
                            "encrypted_packet": false,
                            "encryption_algorithm": 0,
                            "pts_adjustment": 0.0,
                            "cw_index": "0xff",
                            "tier": "0x0fff",
                            "splice_command_length": 5,
                            "splice_command_type": 6,
                            "descriptor_loop_length": 25,
                            "crc": "0xa9cc6758"
                        },
                        "command": {
                            "command_length": 5,
                            "command_type": 6,
                            "name": "Time Signal",
                            "time_specified_flag": true,
                            "pts_time": 21695.740089
                        },
                        "descriptors": [
                            {
                                "tag": 2,
                                "descriptor_length": 23,
                                "name": "Segmentation Descriptor",
                                "identifier": "CUEI",
                                "segmentation_event_id": "0x4800008e",
                                "segmentation_event_cancel_indicator": false,
                                "segmentation_event_id_compliance_indicator": true,
                                "program_segmentation_flag": true,
                                "segmentation_duration_flag": false,
                                "delivery_not_restricted_flag": false,
                                "web_delivery_allowed_flag": true,
                                "no_regional_blackout_flag": true,
                                "archive_allowed_flag": true,
                                "device_restrictions": "No Restrictions",
                                "segmentation_upid_type": 8,
                                "segmentation_upid_type_name": "AiringID",
                                "segmentation_upid_length": 8,
                                "segmentation_upid": "0x2ca0a18a",
                                "segmentation_type_id": 53,
                                "segment_num": 2,
                                "segments_expected": 0
                            }
                        ]
                    }

             */
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)47, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0xa9cc6758, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0x746290a0, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual(1, res.SpliceInfoSection.SpliceDescriptors.Count);
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x4800008e, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(0x2, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);
            //Assert.AreEqual("0x2ca0a18a", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted());

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACygoYo=", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);

            Assert.AreEqual(0x35, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Provider Placement Opportunity End", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);
        }


        /// <summary>
        /// 14.4. time_signal – Program Start/End
        /// </summary>
        [TestMethod]
        public void Test4TimesignalProgramStartEnd()
        {
            string base64_1 = "/DBIAAAAAAAA///wBQb+ek2ItgAyAhdDVUVJSAAAGH+fCAgAAAAALMvDRBEAAAIXQ1VFSUgAABl/nwgIAAAAACyk26AQAACZcuND";
            Scte35 res = Scte35.DecoderBase64(base64_1);

            /*
                        {
                            "table_id": "0xfc",
                            "section_syntax_indicator": false,
                            "private_indicator": false,
                            "section_length": "0x48",
                            "protocol_version": "0x0",
                            "encrypted_packet": false,
                            "encryption_algorithm": "0x0",
                            "pts_adjustment": "0x0",
                            "cw_index": "0xff",
                            "tier": "0xfff",
                            "splice_command_length": "0x5",
                            "splice_command_type": "TimeSignal",
                            "SpliceCommand": {
                                "SpliceTime": {
                                    "time_specified_flag": true,
                                    "pts_time": "0x7a4d88b6"
                                }
                            },
                            "descriptor_loop_length": "0x32",
                            "SpliceDescriptors": [
                                {
                                    "splice_descriptor_tag": "SegmentationDescriptor",
                                    "descriptor_length": "0x17",
                                    "identifier": "0x43554549",
                                    "segmentation_event_id": "0x48000018",
                                    "segmentation_event_cancel_indicator": false,
                                    "program_segmentation_flag": true,
                                    "segmentation_duration_flag": false,
                                    "delivery_not_restricted_flag": false,
                                    "web_delivery_allowed_flag": true,
                                    "no_regional_blackout_flag": true,
                                    "archive_allowed_flag": true,
                                    "device_restrictions": "None",
                                    "component_count": "0x0",
                                    "ComponentTags": [
                                    ],
                                    "ComponentPtsOffsets": [
                                    ],
                                    "SegmentationUPIDs": [
                                        {
                                            "segmentation_upid_type": "TI",
                                            "segmentation_upid_length": "0x8",
                                            "Data": "AAAAACzLw0Q=",
                                            "MPU": null,
                                            "SegmentationUPIDFormat": "NotSpecified",
                                            "Value": ""
                                        }
                                    ],
                                    "segmentation_type_id": "ProgramEnd",
                                    "segment_num": "0x0",
                                    "segments_expected": "0x0",
                                    "sub_segment_num": "0x0",
                                    "sub_segments_expected": "0x0"
                                },
                                {
                                    "splice_descriptor_tag": "SegmentationDescriptor",
                                    "descriptor_length": "0x17",
                                    "identifier": "0x43554549",
                                    "segmentation_event_id": "0x48000019",
                                    "segmentation_event_cancel_indicator": false,
                                    "program_segmentation_flag": true,
                                    "segmentation_duration_flag": false,
                                    "delivery_not_restricted_flag": false,
                                    "web_delivery_allowed_flag": true,
                                    "no_regional_blackout_flag": true,
                                    "archive_allowed_flag": true,
                                    "device_restrictions": "None",
                                    "component_count": "0x0",
                                    "ComponentTags": [
                                    ],
                                    "ComponentPtsOffsets": [
                                    ],
                                    "SegmentationUPIDs": [
                                        {
                                            "segmentation_upid_type": "TI",
                                            "segmentation_upid_length": "0x8",
                                            "Data": "AAAAACyk26A=",
                                            "MPU": null,
                                            "SegmentationUPIDFormat": "NotSpecified",
                                            "Value": ""
                                        }
                                    ],
                                    "segmentation_type_id": "ProgramStart",
                                    "segment_num": "0x0",
                                    "segments_expected": "0x0",
                                    "sub_segment_num": "0x0",
                                    "sub_segments_expected": "0x0"
                                }
                            ],
                            "alignment_stuffing": null,
                            "E_CRC_32": "0x0",
                            "CRC_32": "0x9972e343"
                        }


                        {
                            "info_section": {
                                "table_id": "0xfc",
                                "section_syntax_indicator": false,
                                "private": false,
                                "sap_type": "0x03",
                                "sap_details": "No Sap Type",
                                "section_length": 72,
                                "protocol_version": 0,
                                "encrypted_packet": false,
                                "encryption_algorithm": 0,
                                "pts_adjustment": 0.0,
                                "cw_index": "0xff",
                                "tier": "0x0fff",
                                "splice_command_length": 5,
                                "splice_command_type": 6,
                                "descriptor_loop_length": 50,
                                "crc": "0x9972e343"
                            },
                            "command": {
                                "command_length": 5,
                                "command_type": 6,
                                "name": "Time Signal",
                                "time_specified_flag": true,
                                "pts_time": 22798.906911
                            },
                            "descriptors": [
                                {
                                    "tag": 2,
                                    "descriptor_length": 23,
                                    "name": "Segmentation Descriptor",
                                    "identifier": "CUEI",
                                    "segmentation_event_id": "0x48000018",
                                    "segmentation_event_cancel_indicator": false,
                                    "segmentation_event_id_compliance_indicator": true,
                                    "program_segmentation_flag": true,
                                    "segmentation_duration_flag": false,
                                    "delivery_not_restricted_flag": false,
                                    "web_delivery_allowed_flag": true,
                                    "no_regional_blackout_flag": true,
                                    "archive_allowed_flag": true,
                                    "device_restrictions": "No Restrictions",
                                    "segmentation_upid_type": 8,
                                    "segmentation_upid_type_name": "AiringID",
                                    "segmentation_upid_length": 8,
                                    "segmentation_upid": "0x2ccbc344",
                                    "segmentation_type_id": 17,
                                    "segment_num": 0,
                                    "segments_expected": 0
                                },
                                {
                                    "tag": 2,
                                    "descriptor_length": 23,
                                    "name": "Segmentation Descriptor",
                                    "identifier": "CUEI",
                                    "segmentation_event_id": "0x48000019",
                                    "segmentation_event_cancel_indicator": false,
                                    "segmentation_event_id_compliance_indicator": true,
                                    "program_segmentation_flag": true,
                                    "segmentation_duration_flag": false,
                                    "delivery_not_restricted_flag": false,
                                    "web_delivery_allowed_flag": true,
                                    "no_regional_blackout_flag": true,
                                    "archive_allowed_flag": true,
                                    "device_restrictions": "No Restrictions",
                                    "segmentation_upid_type": 8,
                                    "segmentation_upid_type_name": "AiringID",
                                    "segmentation_upid_length": 8,
                                    "segmentation_upid": "0x2ca4dba0",
                                    "segmentation_type_id": 16,
                                    "segment_num": 0,
                                    "segments_expected": 0
                                }
                            ]
                        }

             */
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x48, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x9972e343, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0x7a4d88b6, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors.Count);

            // descriptor 1
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x48000018, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);
            //Assert.AreEqual("0x2ca0a18a", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted());

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACzLw0Q=", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);
            Assert.AreEqual(0x11, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Program End", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);


            // descriptor 2
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[1].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[1].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[1].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).Identifier);
            Assert.AreEqual((uint)0x48000019, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeviceRestrictions);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationDuration);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentatonUpid#);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentsExpected);
            //Assert.AreEqual("0x2ca0a18a", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentatonUpid.GetDataFormatted());

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentatonUpid#);
            string upid_data2 = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACyk26A=", upid_data2);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentsExpected);
            Assert.AreEqual(0x10, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeId);
            Assert.AreEqual("Program Start", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeIdName.Message);

        }

        /// <summary>
        /// 14.5. time_signal – Program Overlap Start
        /// </summary>
        [TestMethod]
        public void Test5Test4TimesignalProgramOverlapStart()
        {
            string base64_1 = "/DAvAAAAAAAA///wBQb+rr//ZAAZAhdDVUVJSAAACH+fCAgAAAAALKVs9RcAAJUdsKg=";
            Scte35 res = Scte35.DecoderBase64(base64_1);
            /*
                {
                    "table_id": "0xfc",
                    "section_syntax_indicator": false,
                    "private_indicator": false,
                    "section_length": "0x2f",
                    "protocol_version": "0x0",
                    "encrypted_packet": false,
                    "encryption_algorithm": "0x0",
                    "pts_adjustment": "0x0",
                    "cw_index": "0xff",
                    "tier": "0xfff",
                    "splice_command_length": "0x5",
                    "splice_command_type": "TimeSignal",
                    "SpliceCommand": {
                        "SpliceTime": {
                            "time_specified_flag": true,
                            "pts_time": "0xaebfff64"
                        }
                    },
                    "descriptor_loop_length": "0x19",
                    "SpliceDescriptors": [
                        {
                            "splice_descriptor_tag": "SegmentationDescriptor",
                            "descriptor_length": "0x17",
                            "identifier": "0x43554549",
                            "segmentation_event_id": "0x48000008",
                            "segmentation_event_cancel_indicator": false,
                            "program_segmentation_flag": true,
                            "segmentation_duration_flag": false,
                            "delivery_not_restricted_flag": false,
                            "web_delivery_allowed_flag": true,
                            "no_regional_blackout_flag": true,
                            "archive_allowed_flag": true,
                            "device_restrictions": "None",
                            "component_count": "0x0",
                            "ComponentTags": [
                            ],
                            "ComponentPtsOffsets": [
                            ],
                            "SegmentationUPIDs": [
                                {
                                    "segmentation_upid_type": "TI",
                                    "segmentation_upid_length": "0x8",
                                    "Data": "AAAAACylbPU=",
                                    "MPU": null,
                                    "SegmentationUPIDFormat": "NotSpecified",
                                    "Value": ""
                                }
                            ],
                            "segmentation_type_id": "ProgramOverlapStart",
                            "segment_num": "0x0",
                            "segments_expected": "0x0",
                            "sub_segment_num": "0x0",
                            "sub_segments_expected": "0x0"
                        }
                    ],
                    "alignment_stuffing": null,
                    "E_CRC_32": "0x0",
                    "CRC_32": "0x951db0a8"
                }

                {
                    "info_section": {
                        "table_id": "0xfc",
                        "section_syntax_indicator": false,
                        "private": false,
                        "sap_type": "0x03",
                        "sap_details": "No Sap Type",
                        "section_length": 47,
                        "protocol_version": 0,
                        "encrypted_packet": false,
                        "encryption_algorithm": 0,
                        "pts_adjustment": 0.0,
                        "cw_index": "0xff",
                        "tier": "0x0fff",
                        "splice_command_length": 5,
                        "splice_command_type": 6,
                        "descriptor_loop_length": 25,
                        "crc": "0x951db0a8"
                    },
                    "command": {
                        "command_length": 5,
                        "command_type": 6,
                        "name": "Time Signal",
                        "time_specified_flag": true,
                        "pts_time": 32575.759333
                    },
                    "descriptors": [
                        {
                            "tag": 2,
                            "descriptor_length": 23,
                            "name": "Segmentation Descriptor",
                            "identifier": "CUEI",
                            "segmentation_event_id": "0x48000008",
                            "segmentation_event_cancel_indicator": false,
                            "segmentation_event_id_compliance_indicator": true,
                            "program_segmentation_flag": true,
                            "segmentation_duration_flag": false,
                            "delivery_not_restricted_flag": false,
                            "web_delivery_allowed_flag": true,
                            "no_regional_blackout_flag": true,
                            "archive_allowed_flag": true,
                            "device_restrictions": "No Restrictions",
                            "segmentation_upid_type": 8,
                            "segmentation_upid_type_name": "AiringID",
                            "segmentation_upid_length": 8,
                            "segmentation_upid": "0x2ca56cf5",
                            "segmentation_type_id": 23,
                            "segment_num": 0,
                            "segments_expected": 0
                        }
                    ]
                }

            */
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x2f, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x951db0a8, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0xaebfff64, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual(1, res.SpliceInfoSection.SpliceDescriptors.Count);

            // descriptor 1
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x48000008, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);
            //Assert.AreEqual("0x2ca0a18a", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted());

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACylbPU=", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);

            Assert.AreEqual(0x17, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Program Overlap Start", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);
        }

        /// <summary>
        /// 14.6. time_signal – Program Blackout Override / Program End
        /// </summary>
        [TestMethod]
        public void Test6TimeSignalProgramBlackoutOverrideProgramEnd()
        {
            string base64_1 = "/DBIAAAAAAAA///wBQb+ky44CwAyAhdDVUVJSAAACn+fCAgAAAAALKCh4xgAAAIXQ1VFSUgAAAl/nwgIAAAAACygoYoRAAC0IX6w";
            Scte35 res = Scte35.DecoderBase64(base64_1);
            /*
                    {
                        "info_section": {
                            "table_id": "0xfc",
                            "section_syntax_indicator": false,
                            "private": false,
                            "sap_type": "0x03",
                            "sap_details": "No Sap Type",
                            "section_length": 72,
                            "protocol_version": 0,
                            "encrypted_packet": false,
                            "encryption_algorithm": 0,
                            "pts_adjustment": 0.0,
                            "cw_index": "0xff",
                            "tier": "0x0fff",
                            "splice_command_length": 5,
                            "splice_command_type": 6,
                            "descriptor_loop_length": 50,
                            "crc": "0xb4217eb0"
                        },
                        "command": {
                            "command_length": 5,
                            "command_type": 6,
                            "name": "Time Signal",
                            "time_specified_flag": true,
                            "pts_time": 27436.441722
                        },
                        "descriptors": [
                            {
                                "tag": 2,
                                "descriptor_length": 23,
                                "name": "Segmentation Descriptor",
                                "identifier": "CUEI",
                                "segmentation_event_id": "0x4800000a",
                                "segmentation_event_cancel_indicator": false,
                                "segmentation_event_id_compliance_indicator": true,
                                "program_segmentation_flag": true,
                                "segmentation_duration_flag": false,
                                "delivery_not_restricted_flag": false,
                                "web_delivery_allowed_flag": true,
                                "no_regional_blackout_flag": true,
                                "archive_allowed_flag": true,
                                "device_restrictions": "No Restrictions",
                                "segmentation_upid_type": 8,
                                "segmentation_upid_type_name": "AiringID",
                                "segmentation_upid_length": 8,
                                "segmentation_upid": "0x2ca0a1e3",
                                "segmentation_type_id": 24,
                                "segment_num": 0,
                                "segments_expected": 0
                            },
                            {
                                "tag": 2,
                                "descriptor_length": 23,
                                "name": "Segmentation Descriptor",
                                "identifier": "CUEI",
                                "segmentation_event_id": "0x48000009",
                                "segmentation_event_cancel_indicator": false,
                                "segmentation_event_id_compliance_indicator": true,
                                "program_segmentation_flag": true,
                                "segmentation_duration_flag": false,
                                "delivery_not_restricted_flag": false,
                                "web_delivery_allowed_flag": true,
                                "no_regional_blackout_flag": true,
                                "archive_allowed_flag": true,
                                "device_restrictions": "No Restrictions",
                                "segmentation_upid_type": 8,
                                "segmentation_upid_type_name": "AiringID",
                                "segmentation_upid_length": 8,
                                "segmentation_upid": "0x2ca0a18a",
                                "segmentation_type_id": 17,
                                "segment_num": 0,
                                "segments_expected": 0
                            }
                        ]
                    }

            */
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x48, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0xb4217eb0, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0x932e380b, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors.Count);

            // descriptor 1
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x4800000a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACygoeM=", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);

            Assert.AreEqual(0x18, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Program Blackout Override", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);



            // descriptor 2
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[1].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[1].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[1].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).Identifier);
            Assert.AreEqual((uint)0x48000009, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeviceRestrictions);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationDuration);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentsExpected);

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidLength);
            string upid_data2 = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACygoYo=", upid_data2);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentsExpected);

            Assert.AreEqual(0x11, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeId);
            Assert.AreEqual("Program End", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeIdName.Message);


        }

        /// <summary>
        /// 14.7. time_signal – Program End
        /// </summary>
        [TestMethod]
        public void Test7TimeSignalProgramEnd()
        {
            string base64_1 = "/DAvAAAAAAAA///wBQb+rvF8TAAZAhdDVUVJSAAAB3+fCAgAAAAALKVslxEAAMSHai4=";
            Scte35 res = Scte35.DecoderBase64(base64_1);

            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x2f, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0xc4876a2e, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0xaef17c4c, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual(1, res.SpliceInfoSection.SpliceDescriptors.Count);

            // descriptor 1
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x48000007, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ComponentCount);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACylbJc=", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);

            Assert.AreEqual(0x11, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Program End", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);
        }

        /// <summary>
        /// This is a complex message, although one that can occur frequently as many ad Breaks are placed
        /// at the end of the Program.The implementer should take care though to find the length and
        /// current practice is to try and keep the message in a single transport packet.
        /// </summary>
        [TestMethod]
        public void Test8TimeSignalProgramStartEndPlacementOpportunityEnd()
        {
            string base64_1 = "/DBhAAAAAAAA///wBQb+qM1E7QBLAhdDVUVJSAAArX+fCAgAAAAALLLXnTUCAAIXQ1VFSUgAACZ/nwgIAAAAACyy150RAAACF0NVRUlIAAAnf58ICAAAAAAsstezEAAAihiGnw==";
            Scte35 res = Scte35.DecoderBase64(base64_1);
            /*
            

            */
            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x61, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);

            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x8a18869f, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0xa8cd44ed, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual(3, res.SpliceInfoSection.SpliceDescriptors.Count);

            // descriptor 1
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[0].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).Identifier);
            Assert.AreEqual((uint)0x480000ad, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ComponentCount);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(2, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACyy150=", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);

            Assert.AreEqual(0x35, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Provider Placement Opportunity End", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);

            // descriptor 2
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[1].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[1].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[1].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).Identifier);
            Assert.AreEqual((uint)0x48000026, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).DeviceRestrictions);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).ComponentCount);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentsExpected);

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidLength);
            string upid_data1 = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACyy150=", upid_data1);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SubSegmentsExpected);

            Assert.AreEqual(0x11, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeId);
            Assert.AreEqual("Program End", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[1])).SegmentationUpidTypeIdName.Message);

            // descriptor 3
            Assert.AreEqual(2, res.SpliceInfoSection.SpliceDescriptors[2].Tag);
            Assert.AreEqual(0x17, res.SpliceInfoSection.SpliceDescriptors[2].DescriptorLength);
            Assert.AreEqual("ScteThreeFiver.SpliceDescriptorSegmentationDescriptor", res.SpliceInfoSection.SpliceDescriptors[2].GetType().ToString());
            Assert.AreEqual(SpliceDescriptorsReader.CUEIdentifier, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).Identifier);
            Assert.AreEqual((uint)0x48000027, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).ProgramSegmentationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentationDurationFlag);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).DeliveryNotRestrictedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).WebDeliveryAllowedFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).NoRegionalBlackoutFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).ArchiveAllowedFlag);
            Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).DeviceRestrictions);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).ComponentCount);
            //Assert.AreEqual((ulong)0x1a599b0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);
            //Assert.AreEqual(0x2ca0a18a, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid#);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentsExpected);

            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentationUpidType);
            Assert.AreEqual(8, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentationUpidLength);
            string upid_data2 = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("AAAAACyy17M=", upid_data2);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SubSegmentsExpected);

            Assert.AreEqual(0x10, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentationUpidTypeId);
            Assert.AreEqual("Program Start", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[2])).SegmentationUpidTypeIdName.Message);

        }
    }
}