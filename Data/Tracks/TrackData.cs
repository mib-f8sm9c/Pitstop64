using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.Rom;

namespace MK64Pitstop.Data.Tracks
{
    //NOT BEING USED CURRENTLY!!!
    public class TrackData : RomItem
    {
        //In the future, store outside of the MIOBlocks, then move into them when re
        public MIO0Block ItemsBlock { get; private set; }
        public MIO0Block VertexBlock { get; private set; }
        public DmaAddressBlock TextureReferences { get; private set; }
        public uint VertexBank { get; private set; }
        public uint Unknown1 { get; private set; }
        public DmaAddress PackedDL { get; private set; }
        public uint Seg7Length { get; private set; }
        public uint TableSeg { get; private set; }
        public uint Unknown2 { get; private set; }

        public string TrackName { get; set; }
        public bool IsOriginalTrack { get; private set; }

        public TrackData()
        {

        }

        public TrackDataReferenceEntry GetTrackDataReference()
        {
            throw new NotImplementedException();
            return null;
        }

    }
}
