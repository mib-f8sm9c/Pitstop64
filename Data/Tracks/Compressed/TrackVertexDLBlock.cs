using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Utils;
using Cereal64.Common.DataElements.Encoding;

namespace Pitstop64.Data.Tracks.Compressed
{
    public class TrackVertexDLBlock : N64DataElement
    {
        public MIO0Block VertexData { get; private set; }
        public byte[] DLData { get; private set; }

        public int FinalDLCommand;

        public TrackVertexDLBlock(int offset, byte[] rawData, int finalDL)
            : base(offset, rawData)
        {
            FinalDLCommand = finalDL;
        }

        public TrackVertexDLBlock(int offset, MIO0Block vertexData, byte[] dlData, int finalDL)
            : base(offset, null)
        {
            VertexData = vertexData;
            DLData = dlData;
            FinalDLCommand = finalDL;
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(VertexData.RawData, DLData);
            }
            set
            {
                if (value != null)
                {
                    VertexData = MIO0Block.ReadMIO0BlockFrom(value, 0);
                    DLData = new byte[value.Length - VertexData.RawDataSize];
                    Array.Copy(value, VertexData.RawDataSize, DLData, 0, DLData.Length);
                }
            }
        }

        public override int RawDataSize
        {
            get { return VertexData.RawDataSize + DLData.Length; }
        }

    }
}
