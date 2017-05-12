using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;

namespace Pitstop64.Data.Tracks
{
    public static class TrackConstants
    {
        public static DmaAddress[] RenderTableOffsets = new DmaAddress[]
        {
            new DmaAddress(0x090001F0),
            new DmaAddress(0x09000150),
            new DmaAddress(0x090001D0),
            new DmaAddress(0x09000170),
            new DmaAddress(0x060183F0),
            new DmaAddress(0x06007890),
            new DmaAddress(0x06019328),
            new DmaAddress(0x090002C0),
            new DmaAddress(0x09000290),
            new DmaAddress(0x090001D0),
            new DmaAddress(0x060239A0),
            new DmaAddress(0x090001A0),
            new DmaAddress(0x090000B0),
            new DmaAddress(0x060164B8),
            new DmaAddress(0x09000150),
            new DmaAddress(0x06000000),
            new DmaAddress(0x06000000),
            new DmaAddress(0x06000000),
            new DmaAddress(0x06013D20),
            new DmaAddress(0x06000000)
        };

        public static DmaAddress[] SurfaceTableOffsets = new DmaAddress[]
        {
            new DmaAddress(0x06009650),
            new DmaAddress(0x060072D0),
            new DmaAddress(0x060093D8),
            new DmaAddress(0x0600B458),
            new DmaAddress(0x06018240),
            new DmaAddress(0x060079A0),
            new DmaAddress(0x06018FD8),
            new DmaAddress(0x0600DC28),
            new DmaAddress(0x0600FF28),
            new DmaAddress(0x060144B8),
            new DmaAddress(0x06023B68),
            new DmaAddress(0x06023070),
            new DmaAddress(0x06009C20),
            new DmaAddress(0x06016440),
            new DmaAddress(0x0600CC38),
            null,
            null,
            null,
            new DmaAddress(0x06014338),
            null
        };
    }
}
