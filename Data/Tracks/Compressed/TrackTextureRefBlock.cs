using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Utils;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Common.Rom;

namespace Pitstop64.Data.Tracks.Compressed
{
    public class TrackTextureRefBlock : N64DataElement
    {
        public List<TrackTextureRef> TextureReferences;
        public List<DmaAddress> DLReferences;

        public TrackTextureRefBlock(int offset, byte[] rawData)
            : base(offset, rawData)
        {
        }

        public TrackTextureRefBlock(int offset, List<TrackTextureRef> textureRefs, List<DmaAddress> dlRefs)
            : base(offset, null)
        {
            TextureReferences = textureRefs;
            DLReferences = dlRefs;
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(TextureReferences,
                    (int)0, (int)0, (int)0, (int)0, DLReferences);
            }
            set
            {
                if (value == null)
                    return;

                int index = 0;
                byte[] tempByteHolder;
                bool midpointHit = false;
                TextureReferences = new List<TrackTextureRef>();
                DLReferences = new List<DmaAddress>();
                while (index < value.Length)
                {
                    if (!midpointHit) //look for texture refs & the midpoint
                    {
                        if (value[index] == 0x00) //if it's 0, then it's the midpoint
                        {
                            midpointHit = true;
                            index += TrackTextureRef.TRACK_TEXTURE_REF_SIZE;
                        }
                        else
                        {
                            tempByteHolder = new byte[TrackTextureRef.TRACK_TEXTURE_REF_SIZE];
                            Array.Copy(value, index, tempByteHolder, 0, TrackTextureRef.TRACK_TEXTURE_REF_SIZE);
                            TextureReferences.Add(new TrackTextureRef(FileOffset + index, tempByteHolder));
                            index += TrackTextureRef.TRACK_TEXTURE_REF_SIZE;
                        }
                    }
                    else //look for dl refs
                    {
                        DLReferences.Add(new DmaAddress(ByteHelper.ReadInt(value, index)));
                        index += DmaAddress.DMA_ADDRESS_SIZE;
                    }
                }
            }
        }

        public override int RawDataSize
        {
            get { return TextureReferences.Count * TrackTextureRef.TRACK_TEXTURE_REF_SIZE + DLReferences.Count * DmaAddress.DMA_ADDRESS_SIZE + 0x10; }
        }

    }
}
