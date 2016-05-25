using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Utils;

namespace MarioKartTestingTool
{
    public static class VertexPacker
    {
        public static List<Vertex> BytesToVertices(List<byte> bytes)
        {
            List<Vertex> vertices = new List<Vertex>();

            //Read in the packed format, output the new shit
            for (int i = 0; i + 13 < bytes.Count; i += 14)
            {
                short X = (short)(bytes[i] << 8 | bytes[i+1]);
                short Y = (short)(bytes[i+2] << 8 | bytes[i+3]);
                short Z = (short)(bytes[i+4] << 8 | bytes[i+5]);
                short S = (short)(bytes[i+6] << 8 | bytes[i+7]);
                short T = (short)(bytes[i+8] << 8 | bytes[i+9]);
                byte R = bytes[i+10];
                byte G = bytes[i+11];
                byte B = bytes[i+12];
                byte A = bytes[i+13];

                vertices.Add(new Vertex((i / 14) * 16, ByteHelper.CombineIntoBytes(X, Y, Z, (short)0x0000,
                    S, T, R, G, B, A)));
            }

            return vertices;
        }

    }
}
