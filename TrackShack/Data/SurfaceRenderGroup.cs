using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.VisObj64.Data.OpenGL;

namespace TrackShack.Data
{
    public class SurfaceRenderGroup
    {
        public VO64GraphicsCollection Direction1 { get; set; }
        public VO64GraphicsCollection Direction2 { get; set; }
        public VO64GraphicsCollection Direction3 { get; set; }
        public VO64GraphicsCollection Direction4 { get; set; }

        public SurfaceRenderGroup(VO64GraphicsCollection d1, VO64GraphicsCollection d2, VO64GraphicsCollection d3, VO64GraphicsCollection d4)
        {
            Direction1 = d1;
            Direction2 = d2;
            Direction3 = d3;
            Direction4 = d4;
        }
    }
}
