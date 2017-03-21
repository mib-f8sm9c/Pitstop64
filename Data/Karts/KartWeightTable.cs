using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.Xml.Linq;
using Cereal64.Common.Utils;

namespace Pitstop64.Data.Karts
{
    public class KartWeightTable : N64DataElement
    {
        //ALL VALUES ARE Mario,Luigi,Yoshi,Toad,DK,Wario,Peach,Bowser

        public static int DefaultKartWeightBlockLocation = 0x121DA0;
        public float[] KartWeights;

        public KartWeightTable(int offset, byte[] data)
            : base(offset, data)
        {

        }

        public KartWeightTable(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {

        }

        public void InitDataContainers()
        {
            if (KartWeights == null)
            {
                KartWeights = new float[8];
            }
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    KartWeights[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
            }
            set
            {
                InitDataContainers();

                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Mario] = ByteHelper.ReadFloat(value, 0x0);
                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Luigi] = ByteHelper.ReadFloat(value, 0x4);
                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Yoshi] = ByteHelper.ReadFloat(value, 0x8);
                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Toad] = ByteHelper.ReadFloat(value, 0xC);
                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.DK] = ByteHelper.ReadFloat(value, 0x10);
                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Wario] = ByteHelper.ReadFloat(value, 0x14);
                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Peach] = ByteHelper.ReadFloat(value, 0x18);
                KartWeights[(int)MarioKartRomInfo.OriginalCharacters.Bowser] = ByteHelper.ReadFloat(value, 0x1C);
            }
        }

        public override int RawDataSize
        {
            get { return 0x20; }
        }
    }
}
