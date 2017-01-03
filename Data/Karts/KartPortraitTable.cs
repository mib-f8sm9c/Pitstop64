using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Utils;
using System.Xml.Linq;

namespace Pitstop64.Data.Karts
{
    public class KartPortraitTable : N64DataElement
    {
        public List<List<KartPortraitTableEntry>> Entries { get; private set; }

        private const int IMAGES_PER_KART = 17;

        public KartPortraitTable(int fileOffset, byte[] data)
            : base(fileOffset, data)
        {
        }

        public KartPortraitTable(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {
        }

        private void SwapToadAndPeachPortraits()
        {
            List<KartPortraitTableEntry> toadPortraits = Entries[(int)MarioKartRomInfo.OriginalCharacters.Toad];
            Entries[(int)MarioKartRomInfo.OriginalCharacters.Toad] = Entries[(int)MarioKartRomInfo.OriginalCharacters.Peach];
            Entries[(int)MarioKartRomInfo.OriginalCharacters.Peach] = toadPortraits;
        }

        public override byte[] RawData
        {
            get
            {
                SwapToadAndPeachPortraits();
                return ByteHelper.CombineIntoBytes(Entries.ToArray());
            }
            set
            {
                if (value.Length != RawDataSize)
                    return;

                if (Entries == null)
                    Entries = new List<List<KartPortraitTableEntry>>();

                Entries.Clear();

                Entries.Add(new List<KartPortraitTableEntry>());

                byte[] entryData = new byte[0x28];
                for (int i = 0; i < RawDataSize; i += 0x28) //Hardcode please!
                {
                    if (Entries.Last().Count >= IMAGES_PER_KART)
                        Entries.Add(new List<KartPortraitTableEntry>());

                    Array.Copy(value, i, entryData, 0, 0x28);

                    Entries.Last().Add(new KartPortraitTableEntry(FileOffset + i, entryData));
                }

                SwapToadAndPeachPortraits();
            }
        }

        public override int RawDataSize
        {
            get { return MarioKartRomInfo.CharacterFaceTableLength; }
        }
    }
}
