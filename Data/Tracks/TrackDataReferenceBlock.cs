using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Utils;
using System.Xml.Linq;

namespace Pitstop64.Data.Courses
{
    [AlternateXMLNames(new string[] { "CourseDataReferenceBlock" })]
    public class TrackDataReferenceBlock: N64DataElement
    {
        public TrackDataReferenceEntry[] Entries = new TrackDataReferenceEntry[MarioKartRomInfo.TrackCount];

        public TrackDataReferenceBlock(int offset, byte[] data)
            : base(offset, data)
        {

        }

        public TrackDataReferenceBlock(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {

        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(Entries);
            }
            set
            {
                if (value.Length != 0x30 * MarioKartRomInfo.TrackCount)
                    return;

                byte[] entryData = new byte[0x30];
                for (int i = 0; i < MarioKartRomInfo.TrackCount; i++)
                {
                    Array.Copy(value, 0x30 * i, entryData, 0, 0x30);
                    Entries[i] = new TrackDataReferenceEntry(FileOffset, entryData, i);
                }
            }
        }

        public override int RawDataSize
        {
            get { return Entries.Length * 0x30; } //Don't hardcode it in the future?
        }
    }
}
