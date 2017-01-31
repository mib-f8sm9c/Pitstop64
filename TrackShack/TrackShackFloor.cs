using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data.Tracks;
using Cereal64.Common.Rom;
using Cereal64.Common.Utils;

namespace TrackShack
{
    public static class TrackShackFloor //Rename later?
    {
        public static TrackInfo CurrentTrack;

        public static string CurrentTrackPath;

        public static void LoadCurrentTrackIntoRomProject()
        {
            //Clear out the RomProject
            while (RomProject.Instance.Files.Count > 0)
            {
                RomProject.Instance.RemoveRomFile(RomProject.Instance.Files.Last());
            }


            //Clear out the RomProject
            while (RomProject.Instance.Files.Count > 0)
            {
                RomProject.Instance.RemoveRomFile(RomProject.Instance.Files.Last());
            }

            byte[] decodedDLData = CurrentTrack.TrackItems.Data;
            byte[] vertsData = CurrentTrack.Vertices.RawData;
            byte[] commandsData = CurrentTrack.F3DCommands.RawData;

            byte[] textureSegData = new byte[CurrentTrack.TextureReferences.Sum(img => img.ImageReference.Texture.RawData.Length)];
            int bytePointer = 0;
            for (int i = 0; i < CurrentTrack.TextureReferences.Count; i++)
            {
                int dataLength = CurrentTrack.TextureReferences[i].ImageReference.Texture.RawDataSize;
                Array.Copy(CurrentTrack.TextureReferences[i].ImageReference.Texture.RawData, 0, textureSegData, bytePointer, dataLength);
                bytePointer += dataLength;
            }

            //Use the F3DEXReader here
            RomProject.Instance.AddRomFile(new RomFile("Verts", 0, new Cereal64.Common.DataElements.UnknownData(0x00, vertsData)));
            //RomProject.Instance.Files[0].FileLength = vertsData.Length;
            RomProject.Instance.AddRomFile(new RomFile("PackedDLs", 1, new Cereal64.Common.DataElements.UnknownData(0x00, commandsData)));
            //RomProject.Instance.Files[1].FileLength = commandsData.Length;
            RomProject.Instance.AddRomFile(new RomFile("Textures", 2, new Cereal64.Common.DataElements.UnknownData(0x00, textureSegData)));
            //RomProject.Instance.Files[2].FileLength = textureSegData.Length;

            //Here we'll assume that there's only 1 file (the full rom) in the rom project
            if (RomProject.Instance.DMAProfiles.FirstOrDefault(dp => dp.ProfileName == "Levelviewer") == null)
            {
                //RomProject.Instance.RemoveDmaProfile(RomProject.Instance.DMAProfiles.FirstOrDefault(dp => dp.ProfileName == "Levelviewer"));

                DmaProfile profile = new DmaProfile("Levelviewer");
                DmaSegment segment = new DmaSegment();
                segment.File = RomProject.Instance.Files[0];
                segment.RamSegment = 0x04;
                segment.RamStartOffset = 0x00;
                segment.FileStartOffset = 0x00;
                segment.FileEndOffset = segment.File.FileLength;
                segment.TagInfo = "Vertices";
                profile.AddDmaSegment(0x04, segment);
                segment = new DmaSegment();
                segment.File = RomProject.Instance.Files[1];
                segment.RamSegment = 0x07;
                segment.RamStartOffset = 0x00;
                segment.FileStartOffset = 0x00;
                segment.FileEndOffset = segment.File.FileLength;
                segment.TagInfo = "PackedDLs";
                profile.AddDmaSegment(0x07, segment);
                segment = new DmaSegment();
                segment.File = RomProject.Instance.Files[2];
                segment.RamSegment = 0x05;
                segment.RamStartOffset = 0x00;
                segment.FileStartOffset = 0x00;
                segment.FileEndOffset = segment.File.FileLength;
                segment.TagInfo = "Textures";
                profile.AddDmaSegment(0x05, segment);
                RomProject.Instance.AddDmaProfile(profile);

            }
        }


    }
}
