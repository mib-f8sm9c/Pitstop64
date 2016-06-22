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
                if (!RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.TKMK00TextureLocations[i].RomOffset))
                {
                    ushort alpha = MarioKartRomInfo.TKMK00TextureLocations[i].AlphaColor;
                    int offset = MarioKartRomInfo.TKMK00TextureLocations[i].RomOffset;
                    int length = MarioKartRomInfo.TKMK00TextureLocations[i].Length;

                    TKMK00Block tkmk;

                    byte[] bytes = new byte[length];
                    Array.Copy(data, offset, bytes, 0, length);

                    tkmk = new TKMK00Block(offset, bytes, alpha);

                    RomProject.Instance.Files[0].AddElement(tkmk);
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

            //Okay, let's test the MIO0 encoding!!
            //int mioOffset;
            //List<DmaAddress[]> mioReferences = new List<DmaAddress[]>();

            //mioReferences.Add(block.Mario1References);
            //mioReferences.Add(block.Mario2References);
            //mioReferences.Add(block.Luigi1References);
            //mioReferences.Add(block.Luigi2References);
            //mioReferences.Add(block.Bowser1References);
            //mioReferences.Add(block.Bowser2References);
            //mioReferences.Add(block.Toad1References);
            //mioReferences.Add(block.Toad2References);
            //mioReferences.Add(block.Yoshi1References);
            //mioReferences.Add(block.Yoshi2References);
            //mioReferences.Add(block.DK1References);
            //mioReferences.Add(block.DK2References);
            //mioReferences.Add(block.Peach1References);
            //mioReferences.Add(block.Peach2References);
            //mioReferences.Add(block.Wario1References);
            //mioReferences.Add(block.Wario2References);

            ////Palettes
            //for (int i = 0; i < block.PaletteReferences.Length; i++)
            //{
            //    if (block.PaletteReferences[i].ReferenceElement == null)
            //    {
            //        int paletteOffset = block.PaletteReferences[i].Offset + 0x145470;
            //        N64DataElement existingPalette;
            //        if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
            //            (existingPalette = RomProject.Instance.Files[0].GetElementAt(paletteOffset)) is Palette)
            //        {
            //            block.PaletteReferences[i].ReferenceElement = (Palette)existingPalette;
            //        }
            //        else
            //        {
            //            byte[] paletteData = new byte[0x200]; //256 2-byte color values
            //            Array.Copy(data, paletteOffset, paletteData, 0, paletteData.Length);
            //            Palette newPalette = new Palette(paletteOffset, paletteData);
            //            block.PaletteReferences[i].ReferenceElement = newPalette;
            //            RomProject.Instance.Files[0].AddElement(newPalette);
            //        }
            //    }
            //}
            
            ////MIO and the encoded image creation
            //for (int i = 0; i < mioReferences.Count; i++)
            //{
            //    for (int j = 0; j < mioReferences[i].Length; j++)
            //    {
            //        if (mioReferences[i][j].ReferenceElement == null)
            //        {
            //            mioOffset = mioReferences[i][j].Offset + 0x145470;
            //            N64DataElement existingMio;
            //            MIO0Block mio;
            //            if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset) && 
            //                (existingMio = RomProject.Instance.Files[0].GetElementAt(mioOffset)) is MIO0Block)
            //            {
            //                mio = (MIO0Block)existingMio;
            //                mioReferences[i][j].ReferenceElement = mio;
            //            }
            //            else
            //            {
            //                mio = MIO0Block.ReadMIO0BlockFrom(data, mioOffset);
            //                mioReferences[i][j].ReferenceElement = mio;
            //                RomProject.Instance.Files[0].AddElement(mio);
            //            }

            //            //Handle the encoded texture now
            //            if (mio.DecodedN64DataElement == null)
            //            {
            //                Palette selectedPalette = (Palette)block.PaletteReferences[i / 2].ReferenceElement;
            //                Texture newTexture = new Texture(0, mio.DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, selectedPalette);
            //                mio.DecodedN64DataElement = newTexture;
            //            }
            //        }
            //    }
            //}

            //for (int i = 0; i < mio0.DecodedData.Length; i++)
            //{
            //    if (mio0.DecodedData[i] != mio0Recoded.DecodedData[i])
            //        throw new Exception();
            //    if (mio0.DecodedData[i] != mio0Recoded2.DecodedData[i])
            //        throw new Exception();
            //}

            //int length1 = mio0.RawDataSize;
            //int length2 = mio0Recoded.RawDataSize;
            //int length3 = length1 + length2;
            //length3++;
        }
    }
}
