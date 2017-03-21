using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data.Tracks;
using Cereal64.Common.Rom;
using Cereal64.Common.Utils;
using TrackShack.Data;
using Cereal64.Microcodes.F3DEX;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace TrackShack
{
    public static class TrackShackFloor //Rename later?
    {
        public static TrackWrapper CurrentTrack;

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

            byte[] decodedDLData = CurrentTrack.Track.TrackItems.Data;
            byte[] vertsData = CurrentTrack.Track.Vertices.RawData;
            byte[] commandsData = CurrentTrack.Track.F3DCommands.RawData;

            byte[] textureSegData = new byte[CurrentTrack.Track.TextureReferences.Sum(img => img.ImageReference.Texture.RawData.Length)];
            int bytePointer = 0;
            for (int i = 0; i < CurrentTrack.Track.TextureReferences.Count; i++)
            {
                int dataLength = CurrentTrack.Track.TextureReferences[i].ImageReference.Texture.RawDataSize;
                Array.Copy(CurrentTrack.Track.TextureReferences[i].ImageReference.Texture.RawData, 0, textureSegData, bytePointer, dataLength);
                bytePointer += dataLength;
            }

            //Use the F3DEXReader here : Consider using a not-unknowndata constructor
            RomProject.Instance.AddRomFile(new RomFile("Verts", 0, new Cereal64.Common.DataElements.UnknownData(0x00, vertsData)));
            CurrentTrack.Track.Vertices.FileOffset = 0;
            RomProject.Instance.Files[0].AddElement(CurrentTrack.Track.Vertices);
            

            RomProject.Instance.AddRomFile(new RomFile("PackedDLs", 1, new Cereal64.Common.DataElements.UnknownData(0x00, commandsData)));
            CurrentTrack.Track.F3DCommands.FileOffset = 0;
            RomProject.Instance.Files[1].AddElement(CurrentTrack.Track.F3DCommands);
            

            RomProject.Instance.AddRomFile(new RomFile("Textures", 2, new Cereal64.Common.DataElements.UnknownData(0x00, textureSegData)));
            bytePointer = 0;
            for (int i = 0; i < CurrentTrack.Track.TextureReferences.Count; i++)
            {
                Texture textureCopy = new Texture(bytePointer,
                    CurrentTrack.Track.TextureReferences[i].ImageReference.Texture.RawData,
                    CurrentTrack.Track.TextureReferences[i].ImageReference.Texture.Format,
                    CurrentTrack.Track.TextureReferences[i].ImageReference.Texture.PixelSize,
                    CurrentTrack.Track.TextureReferences[i].ImageReference.Texture.Width,
                    CurrentTrack.Track.TextureReferences[i].ImageReference.Texture.Height);
                bytePointer += textureCopy.RawDataSize;
                RomProject.Instance.Files[2].AddElement(textureCopy);
            }

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

            //Replace this with better code. Consider bringing in that one entry in the track table, it may be more important than you think
            int finalDLOffset = CurrentTrack.Track.F3DCommands.Commands.Count - 1;
            while ((CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_EndDL) ||
                (CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_MK64_EndDL))
                    finalDLOffset--;

            //Now we have a non-end. Let's find the start of it!
            while (!(CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_EndDL) &&
                !(CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_MK64_EndDL))
                finalDLOffset--;

            //finalDLOffset = 5564;

            F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[1], finalDLOffset * 0x8); //should point at the last dl
        }


    }
}
