using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Rom;
using MK64Pitstop.Services.Hub;
using MK64Pitstop.Data.Tracks;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;

namespace MK64Pitstop.Services.Readers
{
    public static class TrackReader
    {
        public static void ReadRom(BackgroundWorker worker, byte[] rawData, MarioKart64ReaderResults finalResults)
        {
            TrackReaderResults results = new TrackReaderResults();
            
            TrackDataReferenceBlock trackBlock;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.TrackReferenceDataTableLocation))
            {
                ProgressService.SetMessage("Loading Track Resources");
                byte[] refBlock = new byte[0x13 * 0x30];
                Array.Copy(rawData, MarioKartRomInfo.TrackReferenceDataTableLocation, refBlock, 0, 0x13 * 0x30);

                trackBlock = new TrackDataReferenceBlock(MarioKartRomInfo.TrackReferenceDataTableLocation, refBlock);
                results.NewElements.Add(trackBlock);
            }
            else
            {
                trackBlock = (TrackDataReferenceBlock)RomProject.Instance.Files[0].GetElementAt(MarioKartRomInfo.TrackReferenceDataTableLocation);
            }

            results.TrackDataBlock = trackBlock;

            if (MarioKart64ElementHub.Instance.Tracks.Count == 0) //Has not been initialized
                LoadTrackInfo(trackBlock, worker, rawData, results);

            finalResults.TrackResults = results;
        }

        public static void LoadTrackInfo(TrackDataReferenceBlock trackBlock, BackgroundWorker worker, byte[] rawData, TrackReaderResults results)
        {
            //Go through each track, load it up, etc.
            for (int i = 0; i < trackBlock.Entries.Length; i++)
            {
                //Load up the portait picture

                //Need to decide how to represent images first?

                TrackDataReferenceEntry SelectedTrackRef = trackBlock.Entries[i];

                //Load up the name
                string trackName = Enum.GetName(typeof(MarioKartRomInfo.OriginalTracks), i);

                TrackData track = new TrackData(trackName, true);
                //Now start loading up the raw data. Remember to add the MIO0 blocks, but send the actual data into the TrackData object!


                //Take the blocks, and export them
                byte[] displayListBlock = new byte[SelectedTrackRef.DisplayListBlockEnd - SelectedTrackRef.DisplayListBlockStart];
                Array.Copy(rawData, SelectedTrackRef.DisplayListBlockStart,
                    displayListBlock, 0, displayListBlock.Length);



                int vertexEndPackedDLStartOffset = SelectedTrackRef.DisplayListOffset & 0x00FFFFFF;
                byte[] vertexBlock = new byte[vertexEndPackedDLStartOffset];
                Array.Copy(rawData, SelectedTrackRef.VertexBlockStart,
                    vertexBlock, 0, vertexBlock.Length);
                byte[] packedBlock = new byte[(SelectedTrackRef.VertexBlockEnd - SelectedTrackRef.VertexBlockStart) - vertexEndPackedDLStartOffset];
                Array.Copy(rawData, SelectedTrackRef.VertexBlockStart + vertexEndPackedDLStartOffset,
                    packedBlock, 0, packedBlock.Length);
                byte[] textureBlock = new byte[SelectedTrackRef.TextureBlockEnd - SelectedTrackRef.TextureBlockStart];
                Array.Copy(rawData, SelectedTrackRef.TextureBlockStart,
                    textureBlock, 0, textureBlock.Length);

                byte[] decodedDLData = Cereal64.Common.Utils.Encoding.MIO0.Decode(displayListBlock);

                List<Vertex> vertices = VertexPacker.BytesToVertices(Cereal64.Common.Utils.Encoding.MIO0.Decode(vertexBlock).ToList());
                VertexCollection vertCollection = new VertexCollection(0x00, vertices);
                byte[] vertsData = vertCollection.RawData;

                List<F3DEXCommand> commands = F3DEXPacker.BytesToCommands(packedBlock.ToList());
                F3DEXCommandCollection commandColl = new F3DEXCommandCollection(0x00, commands);
                byte[] commandsData = commandColl.RawData;

                List<TrackTextureRef> textureSegPointers = ReadTextureBank(textureBlock);

                byte[] textureSegData = new byte[textureSegPointers.Sum(t => t.DecompressedSize)];
                int bytePointer = 0;
                List<string> offsets = new List<string>();
                List<int> pointers = new List<int>();
                for (int j = 0; j < textureSegPointers.Count; j++)
                {
                    int mioSize = textureSegPointers[j].CompressedSize;
                    if (mioSize % 4 != 0)
                        mioSize += 4 - (mioSize % 4);
                    byte[] tempHolder = new byte[mioSize];
                    Array.Copy(rawData, (textureSegPointers[j].RomOffset & 0x00FFFFFF) + MarioKartRomInfo.TextureBankOffset,
                        tempHolder, 0, mioSize);
                    byte[] decompressed = Cereal64.Common.Utils.Encoding.MIO0.Decode(tempHolder);
                    Array.Copy(decompressed, 0, textureSegData, bytePointer, decompressed.Length);
                    pointers.Add(bytePointer);
                    bytePointer += decompressed.Length;
                    offsets.Add(((textureSegPointers[j].RomOffset & 0x00FFFFFF) + MarioKartRomInfo.TextureBankOffset).ToString("X"));
                }

            }
        }

        private static List<TrackTextureRef> ReadTextureBank(byte[] texturePointers)
        {
            List<TrackTextureRef> output = new List<TrackTextureRef>();
            byte[] tempArray = new byte[0x10];

            for (int i = 0; i < texturePointers.Length / 0x10; i++)
            {
                Array.Copy(texturePointers, i * 0x10, tempArray, 0, 0x10);
                TrackTextureRef refText = new TrackTextureRef(i * 0x10, tempArray);
                if (refText.RomOffset == 0x00000000)
                    break;

                output.Add(refText);
            }

            return output;
        }


        public static void ApplyResults(TrackReaderResults results)
        {
            foreach (N64DataElement element in results.NewElements)
                RomProject.Instance.Files[0].AddElement(element);

            MarioKart64ElementHub.Instance.TrackDataBlock = results.TrackDataBlock;
        }
    }

    public class TrackReaderResults
    {
        public List<N64DataElement> NewElements;
        public TrackDataReferenceBlock TrackDataBlock;
        public List<TrackData> Tracks;

        public TrackReaderResults()
        {
            NewElements = new List<N64DataElement>();
            Tracks = new List<TrackData>();
        }
    }
}
