using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Utils;
using Pitstop64.Services.Hub;
using Pitstop64.Services;
using System.Drawing;

namespace Pitstop64.Data.Tracks.Compressed
{
    /// <summary>
    /// This class holds a track's data as it's stored inside the MK64 ROM. Do not edit this data,
    ///  export this as a TrackInfo and edit it there, and then re-import as a CompressedTrack
    /// </summary>
    public class CompressedTrack : RomItem
    {
        public string TrackName;

        public TrackItemBlock ItemBlock;
        public TrackVertexDLBlock VertexBlock;
        public TrackTextureRefBlock TextureBlock;

        public int Unknown1;
        public ushort Unknown2;

        public Color TopSkyColor;
        public Color BottomSkyColor;

        public DmaAddress SurfaceTableOffset;
        public DmaAddress RenderTableOffset;

        public CompressedTrack(string trackName, TrackItemBlock itemBlock, TrackVertexDLBlock vertexBlock,
            TrackTextureRefBlock textureBlock, int unknown1, ushort unknown2, Color topSkyColor, Color bottomSkyColor,
            DmaAddress surfaceTable, DmaAddress renderTable)
        {
            TrackName = trackName;
            ItemBlock = itemBlock;
            VertexBlock = vertexBlock;
            TextureBlock = textureBlock;
            Unknown1 = unknown1;
            Unknown2 = unknown2;
            TopSkyColor = topSkyColor;
            BottomSkyColor = bottomSkyColor;
            SurfaceTableOffset = surfaceTable;
            RenderTableOffset = renderTable;
        }

        public TrackInfo GetAsExportableTrack()
        {
            //convert & export your track data here!
            return new TrackInfo(TrackName, GetItemsObject(), GetVertices(), GetCommands(), GetImages(), GetCommandRefs(), (uint)Unknown1, Unknown2,
                TopSkyColor, BottomSkyColor, SurfaceTableOffset, RenderTableOffset);
        }

        private TrackItemsObject GetItemsObject()
        {
            TrackItemsObject obj = new TrackItemsObject(ItemBlock.ItemData.DecodedData);

            return obj;
        }

        private VertexCollection GetVertices()
        {
            List < Vertex > vertices = VertexPacker.BytesToVertices(VertexBlock.VertexData.DecodedData.ToList());
            
            return new VertexCollection(-1, vertices);
        }

        private F3DEXCommandCollection GetCommands()
        {
            return new F3DEXCommandCollection(-1, F3DEXPacker.BytesToCommands(this.VertexBlock.DLData.ToList()));
        }

        private List<MK64Image> GetImages()
        {
            List<MK64Image> images = new List<MK64Image>();

            foreach (TrackTextureRef ttr in TextureBlock.TextureReferences)
            {
                images.Add(ttr.ImageReference);
            }
            return images;
        }

        private List<DmaAddress> GetCommandRefs()
        {
            return new List<DmaAddress>(TextureBlock.DLReferences);
        }

        public override string ToString()
        {
            return TrackName;
        }
    }
}
