using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.Xml.Linq;
using Cereal64.Common.Utils;

namespace Pitstop64.Data.Karts
{
    public class KartScaleTable : N64DataElement
    {
        //ALL VALUES ARE Mario,Luigi,Yoshi,Toad,DK,Wario,Peach,Bowser

        public static int DefaultKartScaleBlockLocation = 0x0DE7D4;
        public float[] KartScales;

        public KartScaleTable(int offset, byte[] data)
            : base(offset, data)
        {

        }

        public KartScaleTable(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {

        }

        public void InitDataContainers()
        {
            if (KartScales == null)
            {
                KartScales = new float[8];
            }
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(KartScales[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    KartScales[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    KartScales[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    KartScales[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    KartScales[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    KartScales[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    KartScales[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    KartScales[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
            }
            set
            {
                //ALL VALUES ARE Mario,Luigi,Yoshi,Toad,DK,Wario,Peach,Bowser
                InitDataContainers();

                KartScales[(int)MarioKartRomInfo.OriginalCharacters.Mario] = ByteHelper.ReadFloat(value, 0x0);
                KartScales[(int)MarioKartRomInfo.OriginalCharacters.Luigi] = ByteHelper.ReadFloat(value, 0x4);
                KartScales[(int)MarioKartRomInfo.OriginalCharacters.Yoshi] = ByteHelper.ReadFloat(value, 0x8);
                KartScales[(int)MarioKartRomInfo.OriginalCharacters.Toad] = ByteHelper.ReadFloat(value, 0xC);
                KartScales[(int)MarioKartRomInfo.OriginalCharacters.DK] = ByteHelper.ReadFloat(value, 0x10);
                KartScales[(int)MarioKartRomInfo.OriginalCharacters.Wario] = ByteHelper.ReadFloat(value, 0x14);
                KartScales[(int)MarioKartRomInfo.OriginalCharacters.Peach] = ByteHelper.ReadFloat(value, 0x18);
                KartScales[(int)MarioKartRomInfo.OriginalCharacters.Bowser] = ByteHelper.ReadFloat(value, 0x1C);
            }
        }

        public override int RawDataSize
        {
            get { return 0x20; }
        }
    }
}
