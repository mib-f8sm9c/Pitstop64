using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Utils;

namespace MK64Pitstop.Data.Tracks
{
    //Temp holder until we decode this class : )
    public class TrackItemsBlock : N64DataElement
    {
        private byte[] rawData;

        public TrackItemsBlock(int offset, byte[] rawData)
            : base(offset, rawData)
        {
        }

        public override byte[] RawData
        {
            get
            {
                return rawData;
            }
            set
            {
                rawData = value;
            }
        }

        public override int RawDataSize
        {
            get { return rawData.Length; }
        }

    }
}
