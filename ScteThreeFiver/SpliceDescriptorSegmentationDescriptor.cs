namespace ScteThreeFiver
{
    public class SpliceDescriptorSegmentationDescriptor : SpliceDescriptor
    {

        public UInt32 SegmentationEventId { get; set; }
        public bool SegmentationEventCancelIndicator { get; set; }

        public bool ProgramSegmentationFlag { get; set; }
        public bool SegmentationDurationFlag { get; set; }
        public bool DeliveryNotRestrictedFlag { get; set; }
        public bool WebDeliveryAllowedFlag { get; set; }
        public bool NoRegionalBlackoutFlag { get; set; }
        public bool ArchiveAllowedFlag { get; set; }
        public byte DeviceRestrictions { get; set; }

        public byte ComponentCount { get; set; }
        public List<SegmentationComponent> Components { get; set; } = [];

        public UInt64 SegmentationDuration { get; set; }

        public byte SegmentationUpidType { get; set; }
        public byte SegmentationUpidLength { get; set; }
        public SegmentationUpid SegmentatonUpid { get; set; } = new SegmentationUpid();
        public byte SegmentationUpidTypeId { get; set; }
        public SegmentationUpidTypeIds SegmentationUpidTypeIdName { get; set;}
        public byte SegmentNumber { get; set; }
        public byte SegmentsExpected { get; set; }

        public byte SubSegmentNumber { get; set; }
        public byte SubSegmentsExpected { get; set; }



        public SpliceDescriptorSegmentationDescriptor()
        {
            this.SegmentationUpidTypeIdName = new SegmentationUpidTypeIds();
        }

        public static SpliceDescriptorSegmentationDescriptor ReadRawData(byte[] rawData)
        {
            SpliceDescriptorSegmentationDescriptor res = new();
            res.DataLength = rawData.Length;
            res.Tag = rawData[0];
            res.DescriptorLength = rawData[1];
            res.Identifier = (UInt32)(
                (rawData[2] << 24)
                + (rawData[3] << 16)
                + (rawData[4] << 8)
                + (rawData[5] << 0)
                );
            res.SegmentationEventId = (UInt32)(
                (rawData[6] << 24)
                + (rawData[7] << 16)
                + (rawData[8] << 8)
                + (rawData[9] << 0)
                );
            res.SegmentationEventCancelIndicator = Helpers.GetBit(rawData[10], 7) != 0;
            if (!res.SegmentationEventCancelIndicator)
            {

                res.ProgramSegmentationFlag = Helpers.GetBit(rawData[11], 7) != 0;
                res.SegmentationDurationFlag = Helpers.GetBit(rawData[11], 6) != 0;
                res.DeliveryNotRestrictedFlag = Helpers.GetBit(rawData[11], 5) != 0;
                if (!res.DeliveryNotRestrictedFlag)
                {
                    res.WebDeliveryAllowedFlag = Helpers.GetBit(rawData[11], 4) != 0;
                    res.NoRegionalBlackoutFlag = Helpers.GetBit(rawData[11], 3) != 0;
                    res.ArchiveAllowedFlag = Helpers.GetBit(rawData[11], 2) != 0;
                    res.DeviceRestrictions = (byte)(rawData[11] & 0b00000011);
                }
                int cursor = 12; // new start position
                if (!res.ProgramSegmentationFlag)
                {
                    res.ComponentCount = rawData[cursor]; // << this could actually be rawData[counter++] but do I feel brave?
                    cursor++;
                    for (int i = 0; i < res.ComponentCount; i++)
                    {
                        //
                        SegmentationComponent newComponent = new();
                        newComponent.ComponentType = rawData[cursor];
                        cursor++;
                        newComponent.PtsOffset = (ulong)(
                            ((ulong)(rawData[cursor] & 0b00000001) << 32)
                            +
                            ((ulong)rawData[cursor + 1] << 24)
                            +
                            ((ulong)rawData[cursor + 2] << 16)
                            +
                            ((ulong)rawData[cursor + 3] << 8)
                            +
                            ((ulong)rawData[cursor + 4] << 0)
                            );
                        cursor += 5;
                        res.Components.Add(newComponent);
                    }
                }

                // note we are now using the cursor
                if (res.SegmentationDurationFlag)
                {
                    res.SegmentationDuration = (ulong)(
                        ((ulong)(rawData[cursor]) << 32)
                        +
                        ((ulong)rawData[cursor + 1] << 24)
                        +
                        ((ulong)rawData[cursor + 2] << 16)
                        +
                        ((ulong)rawData[cursor + 3] << 8)
                        +
                        ((ulong)rawData[cursor + 4] << 0)
                        );
                    cursor += 5;
                }
                res.SegmentationUpidType = rawData[cursor++];
                res.SegmentationUpidLength = rawData[cursor++];

                // uggh need to parse the segmentation itself which needs lookups
                byte[] upidData = new byte[res.SegmentationUpidLength];
                Buffer.BlockCopy(rawData, (cursor), upidData, 0, res.SegmentationUpidLength);
                res.SegmentatonUpid = SegmentationUpid.ReadUpid(res.SegmentationUpidType, upidData);
                cursor += res.SegmentatonUpid.Length;


                res.SegmentationUpidTypeId = rawData[cursor++];
                res.SegmentationUpidTypeIdName = SegmentationUpidTypeIds.GetSegmentationUpidTypeIdDetails(res.SegmentationUpidTypeId);
                res.SegmentNumber = rawData[cursor++];
                res.SegmentsExpected = rawData[cursor++];

                if (
                    (res.SegmentationUpidTypeId == 0x34)
                    ||
                    (res.SegmentationUpidTypeId == 0x30)
                    ||
                    (res.SegmentationUpidTypeId == 0x32)
                    ||
                    (res.SegmentationUpidTypeId == 0x36)
                    ||
                    (res.SegmentationUpidTypeId == 0x38)
                    ||
                    (res.SegmentationUpidTypeId == 0x3A)
                    ||
                    (res.SegmentationUpidTypeId == 0x44)
                    ||
                    (res.SegmentationUpidTypeId == 0x46)
                    )
                {
                    //TODO this condition was added due to the SCTE tests failing, can it be removed? How to fix this to spec?
                    if ((cursor + 2) <= rawData.Length)
                    {
                        res.SubSegmentNumber = rawData[cursor++];
                        res.SubSegmentsExpected = rawData[cursor++];
                    }
                    else
                    {
                        Console.WriteLine(" this is weird, fails on the SCTE example... :-( ");
                    }
                }

            }


            return res;
        }
    }

}