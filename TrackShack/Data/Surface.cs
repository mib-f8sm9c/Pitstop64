using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackShack.Data
{
    public enum SurfaceType
    {
        Invalid = 0x00,
        Solid = 0x01,
        DirtTrack = 0x02,
        SandyTrack = 0x03,
        Cement = 0x04,
        SnowTrack = 0x05,
        WoodenPaths = 0x06,
        DirtOffRoad = 0x07,
        Grass = 0x08,
        Ice = 0x09,
        SubmergedSand = 0x0A,
        SnowOffroad = 0x0B,
        RockWall = 0x0C,
        DirtOffRoad2 = 0x0D,
        TrainTracks = 0x0E,
        CaveInterior = 0x0F,
        RopeBridge = 0x10,
        SolidWoodBridge = 0x11,
        BoostRamp = 0xFC,
        OutOfBounds = 0xFD,
        BoostRampLowGrav = 0xFE,
        Solid2 = 0xFF
    }

    public class Surface
    {
        SurfaceRenderGroup RenderGroup;

        public SurfaceType Type;
        public byte ID;

        public bool Flag1, Flag2, Flag3;

        public Surface(SurfaceRenderGroup group, SurfaceType type, byte id, short flagCollection)
        {
            RenderGroup = group;

            Type = type;
            ID = id;
            Flag1 = (flagCollection & 0x8000) != 0;
            Flag2 = (flagCollection & 0x4000) != 0;
            Flag3 = (flagCollection & 0x2000) != 0;
        }
    }
}
