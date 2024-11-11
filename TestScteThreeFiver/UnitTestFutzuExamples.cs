using ScteThreeFiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScteThreeFiver
{
    [TestClass]
    public class UnitTestFutzuExamples
    {
        [TestMethod]
        public void Test1()
        {
            // ref https://tools.middleman.tv/scte35-parser?base64=%252FDBZAAAAAAAA%252F%252F%252FwBQb%252BAAAAAABDAkFDVUVJAAAACn%252F%252FAAApMuAPLXVybjp1dWlkOmFhODViYmI2LTVjNDMtNGI2YS1iZWJiLWVlM2IxM2ViNzk5ORAAAFz7UQA%253D&format=json
            string base64_1 = "/DBZAAAAAAAA///wBQb+AAAAAABDAkFDVUVJAAAACn//AAApMuAPLXVybjp1dWlkOmFhODViYmI2LTVjNDMtNGI2YS1iZWJiLWVlM2IxM2ViNzk5ORAAAFz7UQA=";
            Scte35 res = Scte35.DecoderBase64(base64_1);


            Assert.AreEqual(0xFC, res.SpliceInfoSection.TableId);
            Assert.AreEqual(false, res.SpliceInfoSection.SectionSyntaxIndicator);
            Assert.AreEqual(false, res.SpliceInfoSection.PrivateIndicator);
            Assert.AreEqual((uint)3, res.SpliceInfoSection.SAPType);
            Assert.AreEqual((ushort)0x59, res.SpliceInfoSection.SectionLength);
            Assert.AreEqual(0, res.SpliceInfoSection.ProtocolVersion);
            Assert.AreEqual(false, res.SpliceInfoSection.EncryptedPacketFlag);
            Assert.AreEqual(0, res.SpliceInfoSection.EncryptionAlgorithm);
            Assert.AreEqual((ulong)0x0, res.SpliceInfoSection.PTSAdjustment);
            Assert.AreEqual(0xff, res.SpliceInfoSection.CWIndex);
            Assert.AreEqual(0x0fff, res.SpliceInfoSection.Tier);
            Assert.AreEqual(0x5, res.SpliceInfoSection.SpliceCommandLength);
            Assert.AreEqual(res.SpliceInfoSection.SpliceCommandType, SpliceCommand.TimeSignalType);


            Assert.AreEqual(0x43, res.SpliceInfoSection.DescriptorLoopLength);


            Assert.AreEqual((uint)0, res.SpliceInfoSection.Ecrc32);
            Assert.AreEqual((uint)0x5cfb5100, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(res.SpliceInfoSection.Crc32Calcutated, res.SpliceInfoSection.Crc32);
            Assert.AreEqual(true, res.SpliceInfoSection.Crcvalid);

            Assert.AreEqual("ScteThreeFiver.SpliceCommandTimeSignal", res.SpliceInfoSection.SpliceCommand.GetType().ToString());

            Assert.AreEqual(true, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.TimeSpecifiedFlag);
            Assert.AreEqual((ulong)0x0, ((SpliceCommandTimeSignal)(res.SpliceInfoSection.SpliceCommand)).SpliceTime.PTSTime);

            Assert.AreEqual((ulong)0x43, res.SpliceInfoSection.DescriptorLoopLength);
            Assert.AreEqual(1, res.SpliceInfoSection.SpliceDescriptors.Count);

            Assert.AreEqual(SpliceDescriptorsReader.SegmentationDescriptor, res.SpliceInfoSection.SpliceDescriptors[0].Tag);
            Assert.AreEqual(0x41, res.SpliceInfoSection.SpliceDescriptors[0].DescriptorLength);
            Assert.AreEqual((uint)0x43554549, res.SpliceInfoSection.SpliceDescriptors[0].Identifier);
            Assert.AreEqual((uint)0xa, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventId);
            Assert.AreEqual(false, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationEventCancelIndicator);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ProgramSegmentationFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDurationFlag);
            Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeliveryNotRestrictedFlag);

            //TODO CHECK - are these set and what are the detaults? (they are not set since DeliveryNotRestrictedFlag == true)
            //Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).WebDeliveryAllowedFlag);
            //Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).NoRegionalBlackoutFlag);
            //Assert.AreEqual(true, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ArchiveAllowedFlag);
            //Assert.AreEqual(3, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).DeviceRestrictions);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).ComponentCount);
            Assert.AreEqual((ulong)0x2932e0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationDuration);


            Assert.AreEqual(0x0F, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(0x2d, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentsExpected);
            Assert.AreEqual(15, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidType);
            Assert.AreEqual(0x2d, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidLength);
            string upid_data = ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentatonUpid.GetDataFormatted();
            Assert.AreEqual("dXJuOnV1aWQ6YWE4NWJiYjYtNWM0My00YjZhLWJlYmItZWUzYjEzZWI3OTk5", upid_data);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentNumber);
            Assert.AreEqual(0, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SubSegmentsExpected);
            Assert.AreEqual(16, ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeId);
            Assert.AreEqual("Program Start", ((SpliceDescriptorSegmentationDescriptor)(res.SpliceInfoSection.SpliceDescriptors[0])).SegmentationUpidTypeIdName.Message);
        }
    }
}
