using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Rom;
using MK64Pitstop.Services.Hub;
using MK64Pitstop.Data.Tracks;

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

            //if (MarioKart64ElementHub.Instance.Tracks.Count == 0) //Has not been initialized
            //    LoadTrackInfo(block, portraits, worker, rawData, finalResults.OriginalTKMK00Blocks, results);

            finalResults.TrackResults = results;
        }

        //public static 

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

        public TrackReaderResults()
        {
            NewElements = new List<N64DataElement>();
        }
    }
}
