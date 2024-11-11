using ScteThreeFiver;

namespace TestScteThreeFiver
{
    [TestClass]
    public class UnitTestSpliceInsertBreak
    {
        [TestMethod]
        public void TestMethod1SpliceInsertBreak()
        {
            string base64_1 = "/DAlAAAAAA4QAP/wFAUwLMaof+//z0uXAP4AUmXAwlQBAQAAm2zCrQ==";
            Scte35 res = Scte35.DecoderBase64(base64_1);

            /*

                https://tools.middleman.tv/scte35-parser?base64=%252FDAlAAAAAA4QAP%252FwFAUwLMaof%252B%252F%252Fz0uXAP4AUmXAwlQBAQAAm2zCrQ%253D%253D&format=json
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
                        splice_event_id: "0x302cc6a8",
                        splice_event_cancel_indicator: false,
                        out_of_network_indicator: true,
                        program_splice_flag: true,
                        duration_flag: true,
                        splice_immediate_flag: false,
                        splice_time(): {
                            time_specified_flag: true,
                            pts_time: "0x1cf4b9700"
                        },
                        component_count: "0x0",
                        ComponentTags: null,
                        break_duration(): {
                            auto_return: true,
                            duration: "0x5265c0"
                        },
                        unique_program_id: "0xc254",
                        avail_num: "0x1",
                        avails_expected: "0x1"
                    },
                    descriptor_loop_length: "0x0",
                    SpliceDescriptors: [],
                    alignment_stuffing: null,
                    E_CRC_32: "0x0",
                    CRC_32: "0x9b6cc2ad"
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
                        "crc": "0x9b6cc2ad"
                    },
                    "command": {
                        "command_length": 20,
                        "command_type": 5,
                        "name": "Splice Insert",
                        "time_specified_flag": true,
                        "pts_time": 86364.498489,
                        "break_auto_return": true,
                        "break_duration": 60.0,
                        "splice_event_id": 808240808,
                        "splice_event_cancel_indicator": false,
                        "out_of_network_indicator": true,
                        "program_splice_flag": true,
                        "duration_flag": true,
                        "splice_immediate_flag": false,
                        "event_id_compliance_flag": true,
                        "unique_program_id": 49748,
                        "avail_num": 1,
                        "avails_expected": 1
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
            Assert.AreEqual((uint)0x9b6cc2ad, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandInsert", res.SpliceInfoSection.SpliceCommand.GetType().ToString());
            Assert.AreEqual((UInt32)0x302cc6a8, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventID);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).OutOfNetworkIndicator);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).ProgramSpliceFlag);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).DurationFlag);
            Assert.AreEqual(false, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceImmediateFlag);
            #pragma warning disable CS8602
            Assert.AreEqual((ulong)0x1cf4b9700, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);
            Assert.AreEqual(true, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.AutoReturn);
            Assert.AreEqual((ulong)0x5265c0, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.Duration);
            #pragma warning restore CS8602

            Assert.AreEqual(0xc254, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).UniqueProgramID);
            Assert.AreEqual(1, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailNum);
            Assert.AreEqual(1, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).AvailsExpected);

            // company specific values
            Assert.AreEqual(9, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.BreakId);
            Assert.AreEqual(3, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.TypeId);
            Assert.AreEqual(20, ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.ScteRegionId);
            Assert.AreEqual("CommercialBreak", ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.TypeName);

            // 1100001001010100
            // TTBBBBBBBBSSSSSS
            // 11                   = 3
            //   00001001           = 9
            //           010100     = 20
        }



    }
}