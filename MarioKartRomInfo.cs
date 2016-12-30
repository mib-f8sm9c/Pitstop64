using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitstop64
{
    public static class MarioKartRomInfo
    {
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
        public enum OriginalCourses
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
      
        //Here, define the different pointers for the different regions. In the MarioKartHeader class (or MarioKart64Reader) we'll 
        // actually #def these values
#if EU

#elif EUA

#elif JP

#elif JPA

#else //USA or non-defined

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

        public static int CourseReferenceDataTableLocation { get { return 0x122390; } }
        public static int CourseCount { get { return 0x13; } }
        public static int TextureBankOffset { get { return 0x641F70; } } //Ends 966260?
        public static int KartTexturePaletteBank { get { return 0x145470; } }

        public static int CharacterFaceTableOffset { get { return 0x12FCA8; } }
        public static int CharacterFaceTableLength { get { return 0x1540; } }

        public static int CharacterFaceMIO0Offset { get { return 0x729A30; } }
        public static int TKMK00Block { get { return 0x7FA3C0; } }

#endif

    }
}
