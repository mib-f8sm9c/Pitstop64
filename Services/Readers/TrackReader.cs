using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Rom;
using Pitstop64.Services.Hub;
using Pitstop64.Data.Tracks;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.DataElements.Encoding;
using Pitstop64.Data;
using Pitstop64.Data.Tracks.Compressed;

namespace Pitstop64.Services.Readers
{
    public static class TrackReader
    {
        public static void ReadRom(BackgroundWorker worker, byte[] rawData, MarioKart64ReaderResults finalResults)
        {
            TrackReaderResults results = new TrackReaderResults();

            TrackDataReferenceBlock trackBlock;
            N64DataElement element;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.TrackReferenceDataTableLocation, out element))
            {
                ProgressService.SetMessage("Loading Track Resources");
                byte[] refBlock = new byte[MarioKartRomInfo.TrackCount * 0x30];
                Array.Copy(rawData, MarioKartRomInfo.TrackReferenceDataTableLocation, refBlock, 0,
                    MarioKartRomInfo.TrackCount * TrackDataReferenceEntry.TRACK_DATA_REFERENCE_ENTRY_SIZE);

                trackBlock = new TrackDataReferenceBlock(MarioKartRomInfo.TrackReferenceDataTableLocation, refBlock);
                results.NewElements.Add(trackBlock);
            }
            else
            {
                trackBlock = (TrackDataReferenceBlock)element;
            }

            results.TrackTable = trackBlock;

            TrackSkyTable skyTable;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.SkyTableOffset, out element))
            {
                byte[] refBlock = new byte[MarioKartRomInfo.TrackCount * 12];
                Array.Copy(rawData, MarioKartRomInfo.SkyTableOffset, refBlock, 0, refBlock.Length);

                skyTable = new TrackSkyTable(MarioKartRomInfo.SkyTableOffset, refBlock);
                results.NewElements.Add(skyTable);
            }
            else
            {
                skyTable = (TrackSkyTable)element;
            }

            results.SkyTable = skyTable;

            foreach(TrackDataReferenceEntry trackEntry in trackBlock.Entries)
            {
                LoadTrackInfo(trackEntry, worker, rawData, results);
            }

            finalResults.TrackResults = results;
        }

        public static void LoadTrackInfo(TrackDataReferenceEntry trackEntry, BackgroundWorker worker, byte[] rawData, TrackReaderResults results)
        {
            //Load up the name
            string trackName = Enum.GetName(typeof(MarioKartRomInfo.OriginalTracks), Array.IndexOf(results.TrackTable.Entries, trackEntry));

            //Load the portrait?

            //Now start loading up the raw data. Remember to add the MIO0 blocks, but send the actual data into the TrackData object!

            //Only need to create the blocks and add those to the new elements!
            //Item block
            MIO0Block itemData = MIO0Block.ReadMIO0BlockFrom(rawData, trackEntry.DisplayListBlockStart, trackEntry.DisplayListBlockEnd - trackEntry.DisplayListBlockStart);
            TrackItemBlock itemBlock = new TrackItemBlock(trackEntry.DisplayListBlockStart, itemData);
            results.NewElements.Add(itemBlock);

            //Vertex block
            byte[] vertexDLData = new byte[trackEntry.VertexBlockEnd - trackEntry.VertexBlockStart];
            Array.Copy(rawData, trackEntry.VertexBlockStart, vertexDLData, 0, vertexDLData.Length);
            TrackVertexDLBlock vertexBlock = new TrackVertexDLBlock(trackEntry.VertexBlockStart, vertexDLData, trackEntry.FinalDLCommandOffset);
            results.NewElements.Add(vertexBlock);

            //Texture Block
            byte[] textureData = new byte[trackEntry.TextureBlockEnd - trackEntry.TextureBlockStart];
            Array.Copy(rawData, trackEntry.TextureBlockStart, textureData, 0, textureData.Length);
            TrackTextureRefBlock textureBlock = new TrackTextureRefBlock(trackEntry.TextureBlockStart, textureData);
            results.NewElements.Add(textureBlock);

            CompressedTrack track = new CompressedTrack(trackName, itemBlock, vertexBlock, textureBlock, trackEntry.VertexCount, trackEntry.Unknown2,
                results.SkyTable.TopColors[results.Tracks.Count], results.SkyTable.BottomColors[results.Tracks.Count]);
            results.Tracks.Add(track);
        }

        //private static List<TrackTextureRef> ReadTextureBank(byte[] texturePointers)
        //{
        //    List<TrackTextureRef> output = new List<TrackTextureRef>();
        //    byte[] tempArray = new byte[0x10];

        //    for (int i = 0; i < texturePointers.Length / 0x10; i++)
        //    {
        //        Array.Copy(texturePointers, i * 0x10, tempArray, 0, 0x10);
        //        TrackTextureRef refText = new TrackTextureRef(i * 0x10, tempArray);
        //        if (refText.RomOffset == 0x00000000)
        //            break;

        //        output.Add(refText);
        //    }

        //    return output;
        //}


        public static void ApplyResults(TrackReaderResults results)
        {
            MarioKart64ElementHub.Instance.TrackTable = results.TrackTable;
            MarioKart64ElementHub.Instance.TrackSkyColorTable = results.SkyTable;

            for (int i = 0; i < results.Tracks.Count; i++)
            {
                CompressedTrack track = results.Tracks[i];
                //If a track by the same name doesn't exist
                if (MarioKart64ElementHub.Instance.Tracks.SingleOrDefault(t => t.TrackName == track.TrackName) == null)
                    MarioKart64ElementHub.Instance.Tracks.Add(track);

                if (MarioKart64ElementHub.Instance.SelectedTracks[i] == null)
                    MarioKart64ElementHub.Instance.SelectedTracks[i] = track;
            }

            foreach (N64DataElement element in results.NewElements)
                RomProject.Instance.Files[0].AddElement(element);

        }
    }

    public class TrackReaderResults
    {
        public List<N64DataElement> NewElements;
        public TrackDataReferenceBlock TrackTable;
        public TrackSkyTable SkyTable;
        public List<CompressedTrack> Tracks;

        public TrackReaderResults()
        {
            NewElements = new List<N64DataElement>();
            Tracks = new List<CompressedTrack>();
        }
    }
}
