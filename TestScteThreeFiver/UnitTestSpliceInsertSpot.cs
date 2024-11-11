using ScteThreeFiver;

namespace TestScteThreeFiver
{
    [TestClass]
    public class UnitTestSpliceInsertSpot
    {
        [TestMethod]
        public void TestMethod1SpliceInsertSpot()
        {
            string base64_1 = "/DAlAAAAAA4QAP/wFAUEqw4Kf+//tqdiAP4AG3dACIsFBQAAcxHpRQ==";
            Scte35 res = Scte35.DecoderBase64(base64_1);

            /*

                https://tools.middleman.tv/scte35-parser?base64=%252FDAlAAAAAA4QAP%252FwFAUEqw4Kf%252B%252F%252FtqdiAP4AG3dACIsFBQAAcxHpRQ%253D%253D&format=json
                {
                    table_id: "0xfc",
                    section_syntax_indicator: false,
                    private_indicator: false,
                    section_length: "0x25",
                    protocol_version: "0x0",
                    encrypted_packet: false,
                    encryption_algorithm: "0x0",
                    pts_adjustment: "0xe10",
                    cw_index: "0x0",
                    tier: "0xfff",
                    splice_command_length: "0x14",
                    splice_command_type: "SpliceInsert",
                    SpliceCommand: {
                        splice_event_id: "0x4ab0e0a",
                        splice_event_cancel_indicator: false,
                        out_of_network_indicator: true,
                        program_splice_flag: true,
                        duration_flag: true,
                        splice_immediate_flag: false,
                        splice_time(): {
                            time_specified_flag: true,
                            pts_time: "0x1b6a76200"
                        },
                        component_count: "0x0",
                        ComponentTags: null,
                        break_duration(): {
                            auto_return: true,
                            duration: "0x1b7740"
                        },
                        unique_program_id: "0x88b",
                        avail_num: "0x5",
                        avails_expected: "0x5"
                    },
                    descriptor_loop_length: "0x0",
                    SpliceDescriptors: [],
                    alignment_stuffing: null,
                    E_CRC_32: "0x0",
                    CRC_32: "0x7311e945"
                }


                https://iodisco.com/cgi-bin/scte35decoder
                {
                    "info_section": {
                        "table_id": "0xfc",
                        "section_syntax_indicator": false,
                        "private": false,
                        "sap_type": "0x03",
                        "sap_details": "No Sap Type",
                        "section_length": 37,
                        "protocol_version": 0,
                        "encrypted_packet": false,
                        "encryption_algorithm": 0,
                        "pts_adjustment": 0.04,
                        "cw_index": "0x00",
                        "tier": "0x0fff",
                        "splice_command_length": 20,
                        "splice_command_type": 5,
                        "descriptor_loop_length": 0,
                        "crc": "0x7311e945"
                    },
                    "command": {
                        "command_length": 20,
                        "command_type": 5,
                        "name": "Splice Insert",
                        "time_specified_flag": true,
                        "pts_time": 81771.002311,
                        "break_auto_return": true,
                        "break_duration": 20.0,
                        "splice_event_id": 78319114,
                        "splice_event_cancel_indicator": false,
                        "out_of_network_indicator": true,
                        "program_splice_flag": true,
                        "duration_flag": true,
                        "splice_immediate_flag": false,
                        "event_id_compliance_flag": true,
                        "unique_program_id": 2187,
                        "avail_num": 5,
                        "avails_expected": 5
                    },
                    "descriptors": []
                }

            */
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
            Assert.AreEqual((uint)0x7311e945, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandInsert", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual((UInt32)78319114, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventID);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).OutOfNetworkIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).ProgramSpliceFlag);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).DurationFlag);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceImmediateFlag);
            #pragma warning disable CS8602
            Assert.AreEqual((ulong)0x1b6a76200, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.AutoReturn);
            Assert.AreEqual((ulong)0x1b7740, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.Duration);
            #pragma warning restore CS8602

            Assert.AreEqual(2187, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).UniqueProgramID);
            Assert.AreEqual(5, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailNum);
            Assert.AreEqual(5, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailsExpected);

            // company specific values
            Assert.AreEqual(34, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.BreakId);
            Assert.AreEqual(0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.TypeId);
            Assert.AreEqual(11, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.ScteRegionId);
            Assert.AreEqual("BroadcastAdSlot", ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.TypeName);
        }



    }
}