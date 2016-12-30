using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Utils;
using System.Xml.Linq;

namespace Pitstop64.Data.Courses
{
    public class CourseDataReferenceBlock: N64DataElement
    {
        public CourseDataReferenceEntry[] entries = new CourseDataReferenceEntry[0x13];

        public CourseDataReferenceBlock(int offset, byte[] data)
            : base(offset, data)
        {

        }

        public CourseDataReferenceBlock(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {

        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(entries);
            }
            set
            {
                if (value.Length != 0x30 * 0x13)
                    return;

                byte[] entryData = new byte[0x30];
                for (int i = 0; i < 0x13; i++)
                {
                    Array.Copy(value, 0x30 * i, entryData, 0, 0x30);
                    entries[i] = new CourseDataReferenceEntry(FileOffset, entryData, i);
                }
            }
        }

        public override int RawDataSize
        {
            get { return entries.Length * 0x30; } //Don't hardcode it in the future?
        }
    }
}
