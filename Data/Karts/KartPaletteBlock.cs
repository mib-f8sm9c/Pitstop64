using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.DataElements;
using System.Xml.Linq;
using Cereal64.Common.Utils;

namespace MK64Pitstop.Data.Karts
{
    public class KartPaletteBlock : N64DataElement
    {
        public List<Palette> Palettes;

        private const int PALETTE_BYTE_LENGTH = 0x40 * 2; //64 colors, 2 bytes per color

        public KartPaletteBlock(int fileOffset, List<Palette> palettes)
            : base(fileOffset, null)
        {
            Palettes.AddRange(palettes);
        }

        public KartPaletteBlock(int fileOffset, byte[] rawData)
            : base(fileOffset, rawData)
        {
        }

        public KartPaletteBlock(XElement xml, byte[] rawData)
            : base(xml, rawData)
        {
            //This should never be saved as an xml
            throw new Exception();
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(Palettes);
            }
            set
            {
                if(Palettes == null)
                    Palettes = new List<Palette>();

                if (value != null)
                {
                    int paletteCount = (value.Length / PALETTE_BYTE_LENGTH);

                    byte[] paletteBytes = new byte[PALETTE_BYTE_LENGTH];

                    bool overwriteExistingPalettes = Palettes.Count == paletteCount;

                    for (int i = 0; i < paletteCount; i++)
                    {
                        Array.Copy(value, i * PALETTE_BYTE_LENGTH, paletteBytes, 0, PALETTE_BYTE_LENGTH);

                        //Either overwrite existing palettes or create new ones
                        if (overwriteExistingPalettes)
                        {
                            Palettes[i].RawData = paletteBytes;
                        }
                        else
                        {
                            Palettes.Add(new Palette(FileOffset + i * PALETTE_BYTE_LENGTH, paletteBytes));
                        }
                    }
                }
            }
        }

        public override int RawDataSize
        {
            get { return Palettes.Count * PALETTE_BYTE_LENGTH; }
        }
    }
}
