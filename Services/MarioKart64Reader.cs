using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using MK64Pitstop.Data;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements;
using System.Drawing;

namespace MK64Pitstop.Services
{
    public static class MarioKart64Reader
    {
        public static void ReadRom()
        {
            //The rom should be loaded as the first file in the rom project
            byte[] data = RomProject.Instance.Files[0].GetAsBytes();

            //Now read the different data bits here, if they haven't been read in yet
            for (int i = 0; i < MarioKartRomInfo.TKMK00TextureLocations.Length; i++)
            {
                N64DataElement preExistingElement = RomProject.Instance.Files[0].GetElementAt(MarioKartRomInfo.TKMK00TextureLocations[i].RomOffset);
                if (preExistingElement != null && preExistingElement.GetType() == typeof(UnknownData))
                {
                    ushort alpha = MarioKartRomInfo.TKMK00TextureLocations[i].AlphaColor;
                    int offset = MarioKartRomInfo.TKMK00TextureLocations[i].RomOffset;
                    int length = MarioKartRomInfo.TKMK00TextureLocations[i].Length;

                    TKMK00Block tkmk;

                    byte[] bytes = new byte[length];
                    Array.Copy(data, offset, bytes, 0, length);

                    tkmk = new TKMK00Block(offset, bytes, alpha);

                    RomProject.Instance.Files[0].AddElement(tkmk);
                    MarioKart64ElementHub.Instance.OriginalTKMK00Blocks.Add(tkmk);
                }
            }

            //Load in the kart graphics MIO0 references
            //TO DO: MOVE THIS All TO EITHER A SEPARATE FUNCTION OR TO A SEPARATE CLASS
            KartGraphicsReferenceBlock block;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock1Location))
            {
                byte[] refBlock = new byte[KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceLength];
                Array.Copy(data, KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock1Location, refBlock, 0, KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceLength);

                block = new KartGraphicsReferenceBlock(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock1Location, refBlock);
                RomProject.Instance.Files[0].AddElement(block);
            }
            else
            {
                block = (KartGraphicsReferenceBlock)RomProject.Instance.Files[0].GetElementAt(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock1Location);
            }

            MarioKart64ElementHub.Instance.KartGraphicsBlock = block;

            RomProject.Instance.AddRomItem(MarioKart64ElementHub.Instance);
            
        }
    }
}
