using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Rom;
using Cereal64.Common.Utils;
using System.Collections.ObjectModel;
using Cereal64.Microcodes.F3DEX.DataElements;
using System.Xml.Linq;
using MK64Pitstop.Services;

namespace MK64Pitstop.Data
{
    public class KartGraphicsReferenceBlock : N64DataElement
    {
        public static int DefaultKartGraphicsReferenceBlock1Location = 0x0DEAC0;
        public static int DefaultKartGraphicsReferenceBlock2Location = 0x0E2B20;
        public static int DefaultKartGraphicsReferenceBlock3Location = 0x0E2F40;
        public static int DefaultKartGraphicsReferenceLength = 0x44A0;

        private const int TURN_REF_TOTAL_COUNT = 0x1EF;
        private const int HALF_TURN_REF_COUNT = 0x23;
        private const int HALF_TURN_ANGLE_COUNT = 9;
        private const int FULL_SPIN_REF_COUNT = 0x14;
        private const int FULL_SPIN_ANGLE_COUNT = 9;
        private const int CRASH_REF_COUNT = 0x20;
        private const int CHARACTER_COUNT = 8;

        private const int DMA_SEGMENT_OFFSET = 0x145470;

        //This is the order of the images saved to the ROM compared to how they should be stored/viewed
        //private short[] RomImageOrder = new short[321] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 35, 36, 37, 38, 39,
        //        40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 
        //        75, 76, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 126, 127, 128, 129, 130, 131, 132,
        //        133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160,
        //        161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188,
        //        203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 238, 239, 240, 241, 242, 243, 244,
        //        245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
        //        31, 32, 33, 34, 265, 266, 267, 268, 269, 270, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 271, 272, 273, 274, 275, 276, 112, 113,
        //        114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 277, 278, 279, 280, 281, 282, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198,
        //        199, 200, 201, 202, 283, 284, 285, 286, 287, 288, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 289, 290, 291, 292, 
        //        293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320 };
        //private short[] RomImageOrder = new short[321] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 195, 196, 197, 198, 199,
        //    200, 201, 202, 203, 204, 205, 206, 207, 208, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45,
        //    46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 63, 64,
        //    65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248,
        //    84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115,
        //    116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144,
        //    145, 146, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
        //    160, 161, 162, 163, 164, 165, 166, 167, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288, 168, 169, 170, 171, 172, 173, 174,
        //    175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 209, 210, 211, 212, 213, 214, 229, 230, 231,
        //    232, 233, 234, 249, 250, 251, 252, 253, 254, 269, 270, 271, 272, 273, 274, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 301, 302,
        //    303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, };

        private List<DmaAddress[]> _characterTurnReferences;

        private DmaAddress[] _filler1 = new DmaAddress[0xA0];

        private List<DmaAddress[]> _characterCrashReferences;

        private DmaAddress[] _filler2 = new DmaAddress[0x8];

        private DmaAddress[] _characterPaletteReferences;

        public KartGraphicsReferenceBlock(int offset, byte[] data)
            : base(offset, data)
        {
            InitDataContainers();

            LoadDmaReferences();
            LoadKartInfo();
        }


        public KartGraphicsReferenceBlock(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {
            //Here, we gotta load in the kart infos and load them that way, then read in the raw data here normally
            InitDataContainers();
        }

        public void InitDataContainers()
        {
            if (_characterCrashReferences == null || _characterPaletteReferences == null || _characterTurnReferences == null)
            {
                _characterTurnReferences = new List<DmaAddress[]>(CHARACTER_COUNT);
                _characterCrashReferences = new List<DmaAddress[]>(CHARACTER_COUNT);
                for (int i = 0; i < CHARACTER_COUNT; i++)
                {
                    _characterTurnReferences.Add(new DmaAddress[TURN_REF_TOTAL_COUNT]);
                    _characterCrashReferences.Add(new DmaAddress[CRASH_REF_COUNT]);
                }
                _characterPaletteReferences = new DmaAddress[CHARACTER_COUNT];

            }
        }

        public override byte[] RawData
        {
            get
            {
                SaveKartInfo();

                return ByteHelper.CombineIntoBytes(
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser],
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    _characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    _filler1,
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser],
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    _characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    _filler2,
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
            }
            set
            {
                InitDataContainers();

                int offset = 0; //0xDEAC0
                int address;

                //Block 1
                List<DmaAddress[]> blockReferencesInOrder = new List<DmaAddress[]>(8);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario]);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad]);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi]);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi]);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.DK]);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach]);
                blockReferencesInOrder.Add(_characterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario]);

                for (int character = 0; character < 8; character++)
                {
                    for (int i = 0; i < TURN_REF_TOTAL_COUNT; i++)
                    {
                        address = ByteHelper.ReadInt(value, offset);
                        blockReferencesInOrder[character][i] = new DmaAddress(address);
                        offset += 4;
                    }
                }

                //Filler
                for (int i = 0; i < _filler1.Length; i++)
                {
                    _filler1[i] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                    offset += 4;
                }

                //Block 2
                blockReferencesInOrder.Clear();
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario]);
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi]);
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad]);
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi]);
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.DK]);
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach]);
                blockReferencesInOrder.Add(_characterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario]);

                for (int character = 0; character < 8; character++)
                {
                    for (int i = 0; i < CRASH_REF_COUNT; i++)
                    {
                        address = ByteHelper.ReadInt(value, offset);
                        blockReferencesInOrder[character][i] = new DmaAddress(address);
                        offset += 4;
                    }
                }

                //Filler
                for (int i = 0; i < _filler2.Length; i++)
                {
                    _filler2[i] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                    offset += 4;
                }

                //Palettes
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.DK] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                _characterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser] = new DmaAddress(ByteHelper.ReadInt(value, offset));
            }
        }

        //Used ONLY to load the original 8 characters from the rom
        //NEED TO MOVE the rest of this code up to MarioKart64ElementHub!!
        //Also make sure all dataelements are properly added to the rom file!
        public void LoadDmaReferences()
        {
            byte[] data = RomProject.Instance.Files[0].GetAsBytes();

            for (int i = 0; i < CHARACTER_COUNT; i++)
            {
                string kartName = Enum.GetName(typeof(MarioKartRomInfo.OriginalCharacters), i);

                if (_characterPaletteReferences[i].ReferenceElement == null)
                {
                    int paletteOffset = _characterPaletteReferences[i].Offset + DMA_SEGMENT_OFFSET;
                    N64DataElement existingPalette;
                    if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
                        (existingPalette = RomProject.Instance.Files[0].GetElementAt(paletteOffset)) is Palette)
                    {
                        _characterPaletteReferences[i].ReferenceElement = (Palette)existingPalette;
                    }
                    else
                    {
                        byte[] paletteData = new byte[0x180]; //256 2-byte color values
                        Array.Copy(data, paletteOffset, paletteData, 0, paletteData.Length);
                        Palette newPalette = new Palette(paletteOffset, paletteData);
                        _characterPaletteReferences[i].ReferenceElement = newPalette;
                        RomProject.Instance.Files[0].AddElement(newPalette);
                    }
                }

                //OKAY, Instructions for the next step:
                // Use the RomImageOrder to split up images the way you need to, then whatever.
                int mioOffset;

                for (int j = 0; j < _characterTurnReferences[i].Length; j++)
                {
                    if (_characterTurnReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = _characterTurnReferences[i][j].Offset + DMA_SEGMENT_OFFSET;
                        N64DataElement existingMio;
                        ImageMIO0Block mio;
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset) &&
                            (existingMio = RomProject.Instance.Files[0].GetElementAt(mioOffset)) is ImageMIO0Block)
                        {
                            mio = (ImageMIO0Block)existingMio;
                            _characterTurnReferences[i][j].ReferenceElement = mio;
                        }
                        else
                        {
                            mio = ImageMIO0Block.ReadImageMIO0BlockFrom(data, mioOffset);
                            _characterTurnReferences[i][j].ReferenceElement = mio;
                            RomProject.Instance.Files[0].AddElement(mio);
                            MarioKart64ElementHub.Instance.OriginalMIO0Blocks.Add(mio);
                        }
                    }

                    //Handle the encoded texture now
                    ImageMIO0Block imageMio = (ImageMIO0Block)_characterTurnReferences[i][j].ReferenceElement;
                    if (imageMio.DecodedN64DataElement == null)
                    {
                        Palette selectedPalette = (Palette)_characterPaletteReferences[i].ReferenceElement;
                        Texture newTexture = new Texture(0, imageMio.DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, selectedPalette);
                        imageMio.DecodedN64DataElement = newTexture;
                    }
                }

                for (int j = 0; j < _characterCrashReferences[i].Length; j++)
                {
                    if (_characterCrashReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = _characterCrashReferences[i][j].Offset + DMA_SEGMENT_OFFSET;
                        N64DataElement existingMio;
                        ImageMIO0Block mio;
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset) &&
                            (existingMio = RomProject.Instance.Files[0].GetElementAt(mioOffset)) is ImageMIO0Block)
                        {
                            mio = (ImageMIO0Block)existingMio;
                            _characterCrashReferences[i][j].ReferenceElement = mio;
                        }
                        else
                        {
                            mio = ImageMIO0Block.ReadImageMIO0BlockFrom(data, mioOffset);
                            _characterCrashReferences[i][j].ReferenceElement = mio;
                            RomProject.Instance.Files[0].AddElement(mio);
                            MarioKart64ElementHub.Instance.OriginalMIO0Blocks.Add(mio);
                        }
                    }

                    //Handle the encoded texture now
                    ImageMIO0Block imageMio = (ImageMIO0Block)_characterCrashReferences[i][j].ReferenceElement;
                    if (imageMio.DecodedN64DataElement == null)
                    {
                        Palette selectedPalette = (Palette)_characterPaletteReferences[i].ReferenceElement;
                        Texture newTexture = new Texture(0, imageMio.DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, selectedPalette);
                        imageMio.DecodedN64DataElement = newTexture;
                    }
                }
            }
        }

        private void LoadKartInfo()
        {
            if (MarioKart64ElementHub.Instance.Karts.Count > 0) //Already has been initialized
                return;

            byte[] data = RomProject.Instance.Files[0].GetAsBytes();

            for (int i = 0; i < CHARACTER_COUNT; i++)
            {
                string kartName = Enum.GetName(typeof(MarioKartRomInfo.OriginalCharacters), i);

                KartInfo newKart = new KartInfo(kartName, (Palette)_characterPaletteReferences[i].ReferenceElement, true);

                KartAnimationSeries[] turnAnims = new KartAnimationSeries[HALF_TURN_ANGLE_COUNT];
                KartAnimationSeries[] spinAnims = new KartAnimationSeries[FULL_SPIN_ANGLE_COUNT];
                KartAnimationSeries crashAnim;
                
                ImageMIO0Block[][] turnBlocks = new ImageMIO0Block[HALF_TURN_ANGLE_COUNT][];
                for(int k = 0; k < HALF_TURN_ANGLE_COUNT; k++)
                {
                    turnBlocks[k] = new ImageMIO0Block[HALF_TURN_REF_COUNT];
                }

                ImageMIO0Block[][] spinBlocks = new ImageMIO0Block[FULL_SPIN_ANGLE_COUNT][];
                for(int k = 0; k < FULL_SPIN_ANGLE_COUNT; k++)
                {
                    spinBlocks[k] = new ImageMIO0Block[FULL_SPIN_REF_COUNT];
                }

                turnAnims[0] = new KartAnimationSeries(kartName + " Turn Down 25");
                turnAnims[0].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown25;
                turnAnims[1] = new KartAnimationSeries(kartName + " Turn Down 19");
                turnAnims[1].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown19;
                turnAnims[2] = new KartAnimationSeries(kartName + " Turn Down 12");
                turnAnims[2].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12;
                turnAnims[3] = new KartAnimationSeries(kartName + " Turn Down 6");
                turnAnims[3].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown6;
                turnAnims[4] = new KartAnimationSeries(kartName + " Turn 0");
                turnAnims[4].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurn0;
                turnAnims[5] = new KartAnimationSeries(kartName + " Turn Up 6");
                turnAnims[5].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp6;
                turnAnims[6] = new KartAnimationSeries(kartName + " Turn Up 12");
                turnAnims[6].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12;
                turnAnims[7] = new KartAnimationSeries(kartName + " Turn Up 19");
                turnAnims[7].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp19;
                turnAnims[8] = new KartAnimationSeries(kartName + " Turn Up 25");
                turnAnims[8].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp25;

                spinAnims[0] = new KartAnimationSeries(kartName + " Spin Down 25");
                spinAnims[0].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25;
                spinAnims[1] = new KartAnimationSeries(kartName + " Spin Down 19");
                spinAnims[1].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19;
                spinAnims[2] = new KartAnimationSeries(kartName + " Spin Down 12");
                spinAnims[2].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12;
                spinAnims[3] = new KartAnimationSeries(kartName + " Spin Down 6");
                spinAnims[3].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6;
                spinAnims[4] = new KartAnimationSeries(kartName + " Spin 0");
                spinAnims[4].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpin0;
                spinAnims[5] = new KartAnimationSeries(kartName + " Spin Up 6");
                spinAnims[5].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6;
                spinAnims[6] = new KartAnimationSeries(kartName + " Spin Up 12"); ;
                spinAnims[6].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12;
                spinAnims[7] = new KartAnimationSeries(kartName + " Spin Up 19"); ;
                spinAnims[7].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19;
                spinAnims[8] = new KartAnimationSeries(kartName + " Spin Up 25");
                spinAnims[8].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25;

                crashAnim = new KartAnimationSeries(kartName + " Crash");
                crashAnim.KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.Crash;

                //Work backwards, to help with image naming
                for (short j = 0; j < HALF_TURN_ANGLE_COUNT * HALF_TURN_REF_COUNT + FULL_SPIN_ANGLE_COUNT * FULL_SPIN_REF_COUNT; j++)
                {
                    ImageMIO0Block imageBlock = (ImageMIO0Block)_characterTurnReferences[i][j].ReferenceElement;

                    //Determine which animation block the current image belongs in
                    if (j >= HALF_TURN_ANGLE_COUNT * HALF_TURN_REF_COUNT)
                    {
                        //Full spin
                        int spinAnim = (j - HALF_TURN_ANGLE_COUNT * HALF_TURN_REF_COUNT) / FULL_SPIN_REF_COUNT;
                        int spinIndex = (j - HALF_TURN_ANGLE_COUNT * HALF_TURN_REF_COUNT) - spinAnim * FULL_SPIN_REF_COUNT;

                        imageBlock.ImageName = kartName[0] + " " + Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag), 
                            spinAnims[spinAnim].KartAnimationType) + "-" + spinIndex;

                        spinBlocks[spinAnim][spinIndex] = imageBlock;
                    }
                    else
                    {
                        //Half turn
                        int turnAnim = j / HALF_TURN_REF_COUNT;
                        int turnIndex = j - turnAnim * HALF_TURN_REF_COUNT;

                        imageBlock.ImageName = kartName[0] + " " + Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                            turnAnims[turnAnim].KartAnimationType) + "-" + turnIndex;

                        turnBlocks[turnAnim][turnIndex] = imageBlock;
                    }

                    if (!newKart.KartImages.Images.ContainsKey(imageBlock.ImageName))
                    {
                        newKart.KartImages.Images.Add(imageBlock.ImageName, new KartImage(imageBlock));
                    }
                }

                for (int j = 0; j < spinBlocks.Length; j++)
                {
                    for (int k = 0; k < spinBlocks[j].Length; k++)
                    {
                        if(spinBlocks[j][k] != null)
                            spinAnims[j].OrderedImageNames.Add(spinBlocks[j][k].ImageName);
                    }
                }

                for (int j = 0; j < turnBlocks.Length; j++)
                {
                    for (int k = 0; k < turnBlocks[j].Length; k++)
                    {
                        if (turnBlocks[j][k] != null)
                            turnAnims[j].OrderedImageNames.Add(turnBlocks[j][k].ImageName);
                    }
                }

                for (int j = 0; j < _characterCrashReferences[i].Length; j++)
                {
                    ImageMIO0Block imageBlock = (ImageMIO0Block)_characterCrashReferences[i][j].ReferenceElement;

                    imageBlock.ImageName = kartName[0] + " " + Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                        crashAnim.KartAnimationType) + "-" + j;

                    crashAnim.OrderedImageNames.Add(imageBlock.ImageName);

                    if (!newKart.KartImages.Images.ContainsKey(imageBlock.ImageName))
                    {
                        newKart.KartImages.Images.Add(imageBlock.ImageName, new KartImage(imageBlock));
                    }
                }

                for(int j = 0; j < turnAnims.Length; j++)
                    newKart.KartAnimations.Add(turnAnims[j]);

                for (int j = 0; j < spinAnims.Length; j++)
                    newKart.KartAnimations.Add(spinAnims[j]);

                newKart.KartAnimations.Add(crashAnim);

                MarioKart64ElementHub.Instance.Karts.Add(newKart);
                MarioKart64ElementHub.Instance.SelectedKarts[i] = newKart;
            }
        }

        private void SaveKartInfo()
        {
            for (int i = 0; i < MarioKart64ElementHub.Instance.SelectedKarts.Length; i++)
            {
                KartInfo kart = MarioKart64ElementHub.Instance.SelectedKarts[i];
                _characterPaletteReferences[i] = new DmaAddress(0x0F, kart.KartImages.ImagePalette.FileOffset - DMA_SEGMENT_OFFSET);
                _characterPaletteReferences[i].ReferenceElement = kart.KartImages.ImagePalette;

                for (int j = 0; j < _characterTurnReferences[i].Length; j++)
                {
                    int animFlag;
                    int animIndex;
                    if (j < HALF_TURN_ANGLE_COUNT * HALF_TURN_REF_COUNT)
                    {
                        animFlag = (int)Math.Round(Math.Pow(2, j / HALF_TURN_REF_COUNT));
                        animIndex = j - (j / HALF_TURN_REF_COUNT) * HALF_TURN_REF_COUNT;
                    }
                    else
                    {
                        animFlag = (int)Math.Round(Math.Pow(2, (j - HALF_TURN_ANGLE_COUNT * HALF_TURN_REF_COUNT) / FULL_SPIN_REF_COUNT + HALF_TURN_ANGLE_COUNT));
                        animIndex = j - (HALF_TURN_REF_COUNT * HALF_TURN_ANGLE_COUNT) - ((j - HALF_TURN_ANGLE_COUNT * HALF_TURN_REF_COUNT) / FULL_SPIN_REF_COUNT) * FULL_SPIN_REF_COUNT;
                    }

                    KartAnimationSeries anim = kart.KartAnimations.FirstOrDefault(f => (f.KartAnimationType & animFlag) != 0);
                    if (anim != null)
                    {
                        //Need to replace animIndex with GetIndexfor(animIndex), but we need a better spin/turn/crash test
                        string imageName;
                        if (anim.IsTurnAnim)
                            imageName = anim.OrderedImageNames[anim.GetImageIndexForTurnFrame(animIndex)];
                        else //if (anim.IsSpinAnim)
                            imageName = anim.OrderedImageNames[anim.GetImageIndexForSpinFrame(animIndex)];
                        ImageMIO0Block block = kart.KartImages.Images[imageName].GetEncodedData(kart.KartImages.ImagePalette);
                        DmaAddress address = new DmaAddress(0x0F, block.FileOffset - DMA_SEGMENT_OFFSET);
                        address.ReferenceElement = block;
                        _characterTurnReferences[i][j] = address;
                    }
                }

                for (int j = 0; j < _characterCrashReferences[i].Length; j++)
                {
                    KartAnimationSeries anim = kart.KartAnimations.FirstOrDefault(f => (f.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.Crash) != 0);
                    if (anim != null)
                    {
                        ImageMIO0Block block = kart.KartImages.Images[anim.OrderedImageNames[anim.GetImageIndexForCrashFrame(j)]].GetEncodedData(kart.KartImages.ImagePalette);
                        DmaAddress address = new DmaAddress(0x0F, block.FileOffset - DMA_SEGMENT_OFFSET);
                        address.ReferenceElement = block;
                        _characterCrashReferences[i][j] = address;
                    }
                }
            }
        }

        public override int RawDataSize
        {
            get { return DefaultKartGraphicsReferenceLength; }
        }
    }
}
