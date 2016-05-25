using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MK64Pitstop
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

        public static int[] CharacterPaletteReference = new int[]
        {
            0x000E2F44,
            0x000E2F40,
            0x000E2F48,
            0x000E2F58,
            0x000E2F54,
            0x000E2F4C,
            0x000E2F50,
            0x000E2F5C
        };

        public enum OriginalCharacters
        {
            Luigi,
            Mario,
            Yoshi,
            Peach,
            Wario,
            Toad,
            DK,
            Bowser
        }

        public static int CourseReferenceDataTableLocation { get { return 0x122390; } }
        public static int CourseCount { get { return 0x13; } }
        public static int TextureBankOffset { get { return 0x641F70; } }
        public static int KartTexturePaletteBank { get { return 0x145470; } }

        public static int CharacterFaceMIO0Bank { get { return 0x729A30 ; } }
        public static int TKMK00Block { get { return 0x7FA3C0; } }
    }
}
