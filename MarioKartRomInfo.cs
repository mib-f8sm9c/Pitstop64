using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Data.Textures;

namespace MK64Pitstop
{
    public static class MarioKartRomInfo
    {
        public struct MK64ImageInfo
        {
            public int TextureOffset;
            public string TextureEncoding;
            public int TextureBlockOffset;
            public string Format;
            public string PixelSize;
            public int Width;
            public int Height;
            public bool IsOriginal;
            public int PaletteCount;
            public List<int> PaletteOffset;
            public List<string> PaletteEncoding;
            public List<int> PaletteBlockOffset;
            public List<int> PaletteColorCount;
            public List<int> PaletteColorOffset;
            public int TkmkLength;
            public ushort TkmkAlpha;
            public string Name;
            
            public MK64ImageInfo(string inputString)
            {
                //Set defaults
                TextureOffset = -1;
                TextureEncoding = "Raw";
                TextureBlockOffset = -1;
                Format = "RGBA";
                PixelSize = "Size_8b";
                Width = 0;
                Height = 0;
                IsOriginal = false;
                PaletteCount = 0;
                PaletteOffset = new List<int>();
                PaletteEncoding = new List<string>();
                PaletteBlockOffset = new List<int>();
                PaletteColorCount = new List<int>();
                PaletteColorOffset = new List<int>();
                TkmkLength = 0;
                TkmkAlpha = 0;
                Name = string.Empty;

                //parse the string
                string[] parts = inputString.Split(',');

                switch (parts[0])
                {
                    case "TKMK00":
                        //Need to load up the tkmk specifics
                        TextureOffset = Convert.ToInt32(parts[1], 16);
                        TextureEncoding = parts[0];
                        PixelSize = "Size_16b";
                        Width = int.Parse(parts[2]);
                        Height = int.Parse(parts[3]);
                        IsOriginal = bool.Parse(parts[4]);
                        TkmkLength = Convert.ToInt32(parts[5], 16);
                        TkmkAlpha = Convert.ToUInt16(parts[6], 16);
                        break;
                    default:
                        TextureOffset = Convert.ToInt32(parts[1], 16);
                        TextureEncoding = parts[0];
                        TextureBlockOffset = Convert.ToInt32(parts[2], 16);
                        Format = parts[3];
                        PixelSize = parts[4];
                        Width = int.Parse(parts[5]);
                        Height = int.Parse(parts[6]);
                        IsOriginal = bool.Parse(parts[7]);
                        if (Format == "CI")
                        {
                            PaletteCount = Convert.ToInt32(parts[8], 16);
                            for (int i = 0; i < PaletteCount; i++)
                            {
                                PaletteOffset.Add(Convert.ToInt32(parts[9 + 5 * i], 16));
                                PaletteEncoding.Add(parts[10 + 5 * i]);
                                PaletteBlockOffset.Add(Convert.ToInt32(parts[11 + 5 * i], 16));
                                PaletteColorCount.Add(int.Parse(parts[12 + 5 * i]));
                                PaletteColorOffset.Add(int.Parse(parts[13 + 5 * i]));
                            }
                        }
                        break;
                }
                Name = parts.Last();
            }

        }

        public struct TKMK00RomLocation
        {
            public int RomOffset;
            public int Length;
            public ushort AlphaColor;

            public TKMK00RomLocation(int offset, int length, ushort alpha)
            {
                RomOffset = offset;
                Length = length;
                AlphaColor = alpha;
            }
        }

        public enum OriginalCharacters
        {
            Mario = 0,
            Luigi,
            Peach,
            Toad,
            Yoshi,
            DK,
            Wario,
            Bowser
        }

        //NOTE: THIS IS THE WRONG ORDER, but it's how the game stores it in order
        public enum OriginalTracks
        {
            MarioRaceway = 0x0,
            ChocoMountain,
            BowsersCastle,
            BansheeBoardwalk,
            YoshiValley,
            FrappeSnowland,
            KoopaTroopaBeach,
            RoyalRaceway,
            LuigiRaceway,
            MooMooFarm,
            ToadsTurnpike,
            KalimariDesert,
            SherbertLand,
            RainbowRoad,
            WarioStadium,
            BlockFort,
            Skyscraper,
            DoubleDeck,
            DKsJungleParkway,
            BigDonut
        }

        public const int CoursePreviewImageWidth = 128;
        public const int CoursePreviewImageHeight = 72;
        public static int[] CoursePreviewImageOffsets = new int[]
        {
            0x007A1418,
            0x007A4570,
            0x007A6F94,
            0x007AA074,
            0x007AC720,
            0x007B0160,
            0x007B2568,
            0x007B488C,
            0x007B70AC,
            0x007B9E20,
            0x007BC3E8,
            0x007BED84,
            0x007C1590,
            0x007C45A0,
            0x007C6DC4,
            0x007CA098,
            0x007CC5C0,
            0x007CECB0,
            0x007D15A8,
            0x007D548C
        };

        public static int[] OriginalTrackDLOffsets = new int[] //Can use the DL end index values instead maybe?
        {
            0x6928,
            0x5AE0,
            0x9910,
            0x7338,
            0x8150,
            0x6638,
            0xB2B0,
            0xB120,
            0xC730,
            0x6730,
            0x6B08,
            0xA670,
            0x3848,
            0x20F8,
            0xA4A8,
            0x15C0,
            0x1110,
            0x738,
            0x9C18
        };

        //Here, define the different pointers for the different regions. In the MarioKartHeader class (or MarioKart64Reader) we'll 
        // actually #def these values
#if EU

#elif EUA

#elif JP

#elif JPA

#else //USA or non-defined

        public static MK64ImageInfo[] ImageLocations = new MK64ImageInfo[]
        {
            //TKMK images
            new MK64ImageInfo("TKMK00,0x7FA3C0,220,32,True,0xC00,0x01,PlayerSelectImage"), //TKMK00, texture offset, width, height, isOrig, Length, Alpha, Name
            new MK64ImageInfo("TKMK00,0x7FB8C0,64,12,True,0x200,0x01," + TextureNames.KART_6_PORTRAIT),

            new MK64ImageInfo("TKMK00,0x7FBAC0,64,12,True,0x200,0x01," + TextureNames.KART_4_PORTRAIT),
            new MK64ImageInfo("TKMK00,0x7FBCC0,64,12,True,0x200,0x01," + TextureNames.KART_8_PORTRAIT),
            new MK64ImageInfo("TKMK00,0x7FBEC0,64,12,True,0x200,0x01," + TextureNames.KART_2_PORTRAIT),
            new MK64ImageInfo("TKMK00,0x7FC0C0,64,12,True,0x200,0x01," + TextureNames.KART_1_PORTRAIT),
            new MK64ImageInfo("TKMK00,0x7FC2C0,64,12,True,0x200,0x01," + TextureNames.KART_3_PORTRAIT),
            new MK64ImageInfo("TKMK00,0x7FC4C0,64,12,True,0x200,0x01," + TextureNames.KART_7_PORTRAIT),
            new MK64ImageInfo("TKMK00,0x7FC6C0,64,12,True,0x200,0x01," + TextureNames.KART_5_PORTRAIT),
            

            //Others
            new MK64ImageInfo("MIO0,0x7A6F94,0,RGBA,Size_16b,128,72,True,BowsersCastlePreview"), //Format, texture offset, texture block offset, image format, pixel size, width, height, isOrig, name
            new MK64ImageInfo("Raw,0x7DD63C,0,RGBA,Size_16b,64,64,True,Player1KartSelectBorder"),
            new MK64ImageInfo("MIO0,0x693BC4,0,CI,Size_8b,32,64,True,1,0x852E20,MIO0,0x13870,256,0,Cow1Front") //Format, texture offset, texture block offset, image format, pixel size, width, height, isOrig, paletteOffset, paletteFormat, paletteBlockOffset, colorCount, colorOffset, name
        };

        //Pretty sure this info is held elsewhere, once we decipher that table we can ignore this : )
        public static TKMK00RomLocation[] TKMK00TextureLocations = new TKMK00RomLocation[]
        {
            new TKMK00RomLocation(0x7FA3C0, 0xC00, 0x01), //0
            new TKMK00RomLocation(0x7FAFC0, 0x900, 0x01),
            new TKMK00RomLocation(0x7FB8C0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FBAC0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FBCC0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FBEC0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FC0C0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FC2C0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FC4C0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FC6C0, 0x200, 0x01),
            new TKMK00RomLocation(0x7FC8C0, 0x500, 0xBE), //10
            new TKMK00RomLocation(0x7FCDC0, 0x500, 0xBE),
            new TKMK00RomLocation(0x7FD2C0, 0xB00, 0xBE),
            new TKMK00RomLocation(0x7FDDC0, 0x400, 0xBE),
            new TKMK00RomLocation(0x7FE1C0, 0x500, 0xBE),
            new TKMK00RomLocation(0x7FE6C0, 0x500, 0xBE),
            new TKMK00RomLocation(0x7FEBC0, 0x400, 0xBE),
            new TKMK00RomLocation(0x7FEFC0, 0x400, 0xBE),
            new TKMK00RomLocation(0x7FF3C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x7FF7C0, 0x500, 0xBE),
            new TKMK00RomLocation(0x7FFCC0, 0x400, 0xBE), //20
            new TKMK00RomLocation(0x8000C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x8004C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x8008C0, 0x500, 0xBE),
            new TKMK00RomLocation(0x800DC0, 0x300, 0xBE),
            new TKMK00RomLocation(0x8010C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x8014C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x8018C0, 0x600, 0xBE),
            new TKMK00RomLocation(0x801EC0, 0x300, 0xBE),
            new TKMK00RomLocation(0x8021C0, 0xC00, 0x01),
            new TKMK00RomLocation(0x802DC0, 0x400, 0xBE), //30
            new TKMK00RomLocation(0x8031C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x8035C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x8039C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x803DC0, 0xC00, 0xBE),
            new TKMK00RomLocation(0x8049C0, 0x500, 0xBE),
            new TKMK00RomLocation(0x804EC0, 0x700, 0xBE),
            new TKMK00RomLocation(0x8055C0, 0xA00, 0xBE),
            new TKMK00RomLocation(0x805FC0, 0xB00, 0xBE),
            new TKMK00RomLocation(0x806AC0, 0x300, 0xBE),
            new TKMK00RomLocation(0x806DC0, 0x400, 0xBE), //40
            new TKMK00RomLocation(0x8071C0, 0x400, 0xBE),
            new TKMK00RomLocation(0x8075C0, 0x300, 0xBE),
            new TKMK00RomLocation(0x8078C0, 0x300, 0xBE),
            new TKMK00RomLocation(0x807BC0, 0x300, 0xBE),
            new TKMK00RomLocation(0x807EC0, 0x200, 0xBE),
            new TKMK00RomLocation(0x8080C0, 0x200, 0xBE),
            new TKMK00RomLocation(0x8082C0, 0x200, 0xBE),
            new TKMK00RomLocation(0x8084C0, 0x200, 0xBE),
            new TKMK00RomLocation(0x8086C0, 0x300, 0xBE),
            new TKMK00RomLocation(0x8089C0, 0x300, 0xBE), //50
            new TKMK00RomLocation(0x808CC0, 0x200, 0xBE),
            new TKMK00RomLocation(0x808EC0, 0x200, 0xBE),
            new TKMK00RomLocation(0x8090C0, 0x200, 0xBE),
            new TKMK00RomLocation(0x8092C0, 0x200, 0xBE),
            new TKMK00RomLocation(0x8094C0, 0xCE00, 0x01),
            new TKMK00RomLocation(0x8162C0, 0x9400, 0x01),
            new TKMK00RomLocation(0x81F6C0, 0xC00, 0x01),
            new TKMK00RomLocation(0x8202C0, 0x700, 0x01),
            new TKMK00RomLocation(0x8209C0, 0x100,0x01),
            new TKMK00RomLocation(0x820AC0, 0x500, 0x01),
            new TKMK00RomLocation(0x820FC0, 0xD50, 0x01) //WHERE DOES THIS ONE END???
        };

        public static int[] CharacterNameplateReference = new int[]
        {
            0x007FC0C0,
            0x007FBEC0,
            0x007FC2C0,
            0x007FBAC0,
            0x007FC6C0,
            0x007FB8C0,
            0x007FC4C0,
            0x007FBCC0,
        };

        public static int TrackReferenceDataTableLocation { get { return 0x122390; } }
        public static int TrackCount { get { return 0x14; } }
        public static int TextureBankOffset { get { return 0x641F70; } } //Ends 966260?
        public static int KartTexturePaletteBank { get { return 0x145470; } }

        public static int CharacterFaceTableOffset { get { return 0x12FCA8; } }
        public static int CharacterFaceTableLength { get { return 0x1540; } }

        public static int CharacterFaceMIO0Offset { get { return 0x729A30; } }
        public static int TKMK00Block { get { return 0x7FA3C0; } }

#endif

    }
}
