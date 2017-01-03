using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Rom;
using Cereal64.Common.Utils;
using System.Xml.Linq;

namespace Pitstop64.Data.Text
{
    public class TextReferenceBlock : N64DataElement
    {
        public static int TEXT_REFERENCE_SECTION_1 = 0x0E8100;
        public static int TEXT_REFERENCE_FILLER = 0xE8264;
        public static int TEXT_REFERENCE_SECTION_2 = 0xE8278;
        public static int TEXT_REFERENCE_TEXT_1 = 0xE8380;
        public static int TEXT_REFERENCE_SECTION_3 = 0xE83A0;
        public static int TEXT_REFERENCE_TEXT_2 = 0xE83B4;
        public static int TEXT_REFERENCE_SECTION_4 = 0xE83E4;
        public static int TEXT_REFERENCE_END = 0xE86C8; //End / Start of credits maybe?

        public static int TEXT_REFERENCE_SECTION_1_LENGTH { get { return TEXT_REFERENCE_FILLER - TEXT_REFERENCE_SECTION_1; } }
        public static int TEXT_REFERENCE_FILLER_LENGTH { get { return TEXT_REFERENCE_SECTION_2 - TEXT_REFERENCE_FILLER; } }
        public static int TEXT_REFERENCE_SECTION_2_LENGTH { get { return TEXT_REFERENCE_TEXT_1 - TEXT_REFERENCE_SECTION_2; } }
        public static int TEXT_REFERENCE_TEXT_1_LENGTH { get { return TEXT_REFERENCE_SECTION_3 - TEXT_REFERENCE_TEXT_1; } }
        public static int TEXT_REFERENCE_SECTION_3_LENGTH { get { return TEXT_REFERENCE_TEXT_2 - TEXT_REFERENCE_SECTION_3; } }
        public static int TEXT_REFERENCE_TEXT_2_LENGTH { get { return TEXT_REFERENCE_SECTION_4 - TEXT_REFERENCE_TEXT_2; } }
        public static int TEXT_REFERENCE_SECTION_4_LENGTH { get { return TEXT_REFERENCE_END - TEXT_REFERENCE_SECTION_4; } }
        
        public List<DmaAddress> TextReferences1;

        public List<byte> Filler1;

        public List<DmaAddress> TextReferences2;

        public List<byte> Text1;

        public List<DmaAddress> TextReferences3;

        public List<byte> Text2;

        public List<DmaAddress> TextReferences4;

        public TextReferenceBlock(int fileOffset, byte[] data)
            : base(fileOffset, data)
        {

        }

        public TextReferenceBlock(XElement xml, byte[] data)
            : base(xml, data)
        {

        }

        public void InitDataContainers()
        {
            if (TextReferences1 == null || Filler1 == null || TextReferences2 == null || Text1 == null || TextReferences3 == null || Text2 == null || TextReferences4 == null)
            {
                TextReferences1 = new List<DmaAddress>(TEXT_REFERENCE_SECTION_1_LENGTH / 4);
                Filler1 = new List<byte>(TEXT_REFERENCE_FILLER_LENGTH);
                TextReferences2 = new List<DmaAddress>(TEXT_REFERENCE_SECTION_2_LENGTH / 4);
                Text1 = new List<byte>(TEXT_REFERENCE_TEXT_1_LENGTH);
                TextReferences3 = new List<DmaAddress>(TEXT_REFERENCE_SECTION_3_LENGTH / 4);
                Text2 = new List<byte>(TEXT_REFERENCE_TEXT_2_LENGTH);
                TextReferences4 = new List<DmaAddress>(TEXT_REFERENCE_SECTION_4_LENGTH / 4);
            }
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(
                    TextReferences1,
                    Filler1,
                    TextReferences2,
                    Text1,
                    TextReferences3,
                    Text2,
                    TextReferences4);
            }
            set
            {
                InitDataContainers();

                int offset = 0; //0xE8100
                int address;

                //References 1
                for (; offset + TEXT_REFERENCE_SECTION_1 < TEXT_REFERENCE_FILLER; offset += 4)
                {
                    address = ByteHelper.ReadInt(value, offset);
                    TextReferences1.Add(new DmaAddress(address));
                }

                //Filler
                for (; offset + TEXT_REFERENCE_SECTION_1 < TEXT_REFERENCE_SECTION_2; offset++)
                {
                    Filler1.Add(value[offset]);
                }

                //References 2
                for (; offset + TEXT_REFERENCE_SECTION_1 < TEXT_REFERENCE_TEXT_1; offset += 4)
                {
                    address = ByteHelper.ReadInt(value, offset);
                    TextReferences2.Add(new DmaAddress(address));
                }

                //Text 1
                for (; offset + TEXT_REFERENCE_SECTION_1 < TEXT_REFERENCE_SECTION_3; offset++)
                {
                    Text1.Add(value[offset]);
                }

                //References 3
                for (; offset + TEXT_REFERENCE_SECTION_1 < TEXT_REFERENCE_TEXT_2; offset += 4)
                {
                    address = ByteHelper.ReadInt(value, offset);
                    TextReferences3.Add(new DmaAddress(address));
                }

                //Text 2
                for (; offset + TEXT_REFERENCE_SECTION_1 < TEXT_REFERENCE_SECTION_4; offset++)
                {
                    Text2.Add(value[offset]);
                }

                //References 4
                for (; offset + TEXT_REFERENCE_SECTION_1 < TEXT_REFERENCE_END; offset += 4)
                {
                    address = ByteHelper.ReadInt(value, offset);
                    TextReferences4.Add(new DmaAddress(address));
                }

            }
        }

        public override int RawDataSize
        {
            get { return TEXT_REFERENCE_END - TEXT_REFERENCE_SECTION_1; }
        }
    }
}
