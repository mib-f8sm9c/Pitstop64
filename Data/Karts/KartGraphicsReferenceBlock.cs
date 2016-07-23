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
using MK64Pitstop.Services.Hub;

namespace MK64Pitstop.Data.Karts
{
    public class KartGraphicsReferenceBlock : N64DataElement
    {
        public static int DefaultKartGraphicsReferenceBlock1Location = 0x0DEAC0;
        public static int DefaultKartGraphicsReferenceBlock2Location = 0x0E2B20;
        public static int DefaultKartGraphicsReferenceBlock3Location = 0x0E2F40;
        public static int DefaultKartGraphicsReferenceLength = 0x44A0;

        public const int TURN_REF_TOTAL_COUNT = 0x1EF;
        public const int HALF_TURN_REF_COUNT = 0x23;
        public const int HALF_TURN_ANGLE_COUNT = 9;
        public const int FULL_SPIN_REF_COUNT = 0x14;
        public const int FULL_SPIN_ANGLE_COUNT = 9;
        public const int CRASH_REF_COUNT = 0x20;
        public const int CHARACTER_COUNT = 8;

        public const int DMA_SEGMENT_OFFSET = 0x145470;

        public const int FILLER_1_LENGTH = 0xA0;
        public const int FILLER_2_LENGTH = 0x8;

        #region hidethis
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
#endregion

        public List<DmaAddress[]> CharacterTurnReferences;

        public DmaAddress[] Filler1 = new DmaAddress[FILLER_1_LENGTH];

        public List<DmaAddress[]> CharacterCrashReferences;

        public DmaAddress[] Filler2 = new DmaAddress[FILLER_2_LENGTH];

        public DmaAddress[] CharacterPaletteReferences;

        public KartGraphicsReferenceBlock(int offset, byte[] data)
            : base(offset, data)
        {
            //InitDataContainers();

            //LoadDmaReferences();
            //LoadKartInfo();
        }


        public KartGraphicsReferenceBlock(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {
            //Here, we gotta load in the kart infos and load them that way, then read in the raw data here normally
            //InitDataContainers();
        }

        public void InitDataContainers()
        {
            if (CharacterCrashReferences == null || CharacterPaletteReferences == null || CharacterTurnReferences == null)
            {
                CharacterTurnReferences = new List<DmaAddress[]>(CHARACTER_COUNT);
                CharacterCrashReferences = new List<DmaAddress[]>(CHARACTER_COUNT);
                for (int i = 0; i < CHARACTER_COUNT; i++)
                {
                    CharacterTurnReferences.Add(new DmaAddress[TURN_REF_TOTAL_COUNT]);
                    CharacterCrashReferences.Add(new DmaAddress[CRASH_REF_COUNT]);
                }
                CharacterPaletteReferences = new DmaAddress[CHARACTER_COUNT];

            }
        }

        public override byte[] RawData
        {
            get
            {
                //SaveKartInfo();

                return ByteHelper.CombineIntoBytes(
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser],
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    Filler1,
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser],
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    Filler2,
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.DK],
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                    CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
            }
            set
            {
                InitDataContainers();

                int offset = 0; //0xDEAC0
                int address;

                //Block 1
                List<DmaAddress[]> blockReferencesInOrder = new List<DmaAddress[]>(8);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario]);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad]);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi]);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi]);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.DK]);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach]);
                blockReferencesInOrder.Add(CharacterTurnReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario]);

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
                for (int i = 0; i < Filler1.Length; i++)
                {
                    Filler1[i] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                    offset += 4;
                }

                //Block 2
                blockReferencesInOrder.Clear();
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario]);
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi]);
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad]);
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi]);
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.DK]);
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach]);
                blockReferencesInOrder.Add(CharacterCrashReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario]);

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
                for (int i = 0; i < Filler2.Length; i++)
                {
                    Filler2[i] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                    offset += 4;
                }

                //Palettes
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Mario] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Luigi] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Yoshi] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Toad] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.DK] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Wario] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Peach] = new DmaAddress(ByteHelper.ReadInt(value, offset));
                offset += 4;
                CharacterPaletteReferences[(int)MarioKartRomInfo.OriginalCharacters.Bowser] = new DmaAddress(ByteHelper.ReadInt(value, offset));
            }
        }

        public override int RawDataSize
        {
            get { return DefaultKartGraphicsReferenceLength; }
        }
    }
}
