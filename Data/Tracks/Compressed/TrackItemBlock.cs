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
    public class TrackItemBlock : N64DataElement
    {
        public MIO0Block ItemData { get; private set; }

        public TrackItemBlock(int offset, byte[] rawData)
            : base(offset, rawData)
        {
        }

        public TrackItemBlock(int offset, MIO0Block itemData)
            : base(offset, null)
        {
            ItemData = itemData;
        }

        public override byte[] RawData
        {
            get
            {
                return ItemData.RawData;
            }
            set
            {
                if (value != null)
                {
                    ItemData = MIO0Block.ReadMIO0BlockFrom(value, 0);
                }
            }
        }

        public override int RawDataSize
        {
            get { return ItemData.RawDataSize; }
        }

    }
}
