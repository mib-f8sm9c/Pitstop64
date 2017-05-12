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
using Cereal64.Common.DataElements;
using Cereal64.VisObj64.Data.OpenGL;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DEX;

namespace TrackShack
{
    public static class TrackShackFloor //Rename later?
    {
        public static TrackWrapper CurrentTrack;

        public static string CurrentTrackPath;

        public static VO64GraphicsCollection RenderGroupUnion = new VO64GraphicsCollection();
        public static List<SurfaceRenderGroup> RenderingGroups = new List<SurfaceRenderGroup>();
        public static List<Surface> Surfaces = new List<Surface>();

        public static ElementSelectionGroup SelectedGroup = null;

        public static VO64GraphicsCollection SelectedRenderingGroup
        {
            get { return _selectedRenderingGroup; }
            set 
            {
                VO64GraphicsCollection current = _selectedRenderingGroup;
                _selectedRenderingGroup = value;
                TrackShackAlerts.NewRenderingGroup(current, value);
            }
        }
        private static VO64GraphicsCollection _selectedRenderingGroup;

        public static void LoadCurrentTrackIntoRomProject()
        {
            //Clear out the RomProject
            while (RomProject.Instance.Files.Count > 0)
            {
                RomProject.Instance.RemoveRomFile(RomProject.Instance.Files.Last());
            }

            List<VO64GraphicsCollection> collections = new List<VO64GraphicsCollection>();
            RenderingGroups.Clear();
            Surfaces.Clear();
            _selectedRenderingGroup = null;

            byte[] decodedDLData = CurrentTrack.Track.TrackItems.Data;
            byte[] vertsData = CurrentTrack.Track.Vertices.RawData;
            byte[] commandsData = CurrentTrack.Track.F3DCommands.RawData;
            byte[] itemsData = CurrentTrack.Track.TrackItems.Data;
            byte[] extraRefData = null;

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

            RomProject.Instance.AddRomFile(new RomFile("Items", 3, new Cereal64.Common.DataElements.UnknownData(0x00, itemsData)));

            if (CurrentTrack.Track.CommandReferences.Count > 0)
            {
                extraRefData = ByteHelper.CombineIntoBytes(CurrentTrack.Track.CommandReferences);
                RomProject.Instance.AddRomFile(new RomFile("Extra Commands", 4, new Cereal64.Common.DataElements.UnknownData(0x00, extraRefData)));
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
                segment = new DmaSegment();
                segment.File = RomProject.Instance.Files[3];
                segment.RamSegment = 0x06;
                segment.RamStartOffset = 0x00;
                segment.FileStartOffset = 0x00;
                segment.FileEndOffset = segment.File.FileLength;
                segment.TagInfo = "Items";
                profile.AddDmaSegment(0x06, segment);
                if (RomProject.Instance.Files.Count > 4)
                {
                    segment = new DmaSegment();
                    segment.File = RomProject.Instance.Files[4];
                    segment.RamSegment = 0x09;
                    segment.RamStartOffset = (CurrentTrack.Track.TextureReferences.Count + 1) * 0x10;
                    segment.FileStartOffset = 0x00;
                    segment.FileEndOffset = segment.File.FileLength;
                    segment.TagInfo = "Extra Commands";
                    profile.AddDmaSegment(0x09, segment);
                }
                RomProject.Instance.AddDmaProfile(profile);
            }

            //Replace this with better code. Consider bringing in that one entry in the track table, it may be more important than you think
            //int finalDLOffset = CurrentTrack.Track.F3DCommands.Commands.Count - 1;
            //while ((CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_EndDL) ||
            //    (CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_MK64_EndDL))
            //        finalDLOffset--;

            ////Now we have a non-end. Let's find the start of it!
            //while (!(CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_EndDL) &&
            //    !(CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_MK64_EndDL))
            //    finalDLOffset--;

            //finalDLOffset = 5564;

            //F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[1], finalDLOffset * 0x8); //should point at the last dl

            //F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[1], 0x380); //should point at the last dl

            //DEBUG
            F3DEXReaderPackage p;
            int render, innerOffset;
            RomFile file, innerFile;
            RomProject.Instance.FindRamOffset(CurrentTrack.Track.RenderTableOffset, out file, out render);
            byte[] data = file.GetAsBytes();
            for (; render < file.FileLength; render += 4)
            {
                RomProject.Instance.FindRamOffset(new DmaAddress(ByteHelper.ReadInt(data, render)), out innerFile, out innerOffset);
                p = F3DEXReader.ReadF3DEXAt(innerFile, innerOffset); //Testing this out a bit!
                foreach (N64DataElement el in p.Elements[innerFile])
                    innerFile.AddElement(el);
            }

            int renderOffset, surfaceOffset;
            RomFile renderFile, surfaceFile;

            RomProject.Instance.FindRamOffset(CurrentTrack.Track.RenderTableOffset, out renderFile, out renderOffset);
            RomProject.Instance.FindRamOffset(CurrentTrack.Track.SurfaceTableOffset, out surfaceFile, out surfaceOffset);

            byte[] surfaceFileData = surfaceFile.GetAsBytes();

            List<VO64GraphicsCollection> colls = new List<VO64GraphicsCollection>();
            N64DataElement elem;
            int surfaceInfo = 0;

            //Render groups first
            while (renderOffset < data.Length)
            {
                colls.Clear();
                for (int i = 0; i < 4; i++)
                {
                    DmaAddress addr = new DmaAddress(ByteHelper.ReadInt(data, renderOffset));
                    RomProject.Instance.FindRamOffset(addr, out innerFile, out innerOffset);
                    if (innerFile.HasElementExactlyAt(innerOffset, out elem))
                    {
                        colls.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)elem, 0, addr));
                        collections.AddRange(colls.Last().Collections);//collections = collections.Union(colls.Last().Collections).ToList();
                    }
                    renderOffset += 4;
                }
                if (colls.Count == 4)
                {
                    RenderingGroups.Add(new SurfaceRenderGroup(colls[0], colls[1], colls[2], colls[3]));
                }
            }

            //And then surfaces themselves
            do
            {
                //Read it in, then read in the next 4 renders, make a new surface object from it!
                surfaceInfo = ByteHelper.ReadInt(surfaceFileData, surfaceOffset);

                if (surfaceInfo != 0)
                {
                    //RomProject.Instance.FindRamOffset(new DmaAddress(surfaceInfo), out innerFile, out innerOffset);
                    //Need to identify the associated VO64GraphicsCollection?? but do afterwards? Not sure : (

                    SurfaceType type = (SurfaceType)ByteHelper.ReadByte(surfaceFileData, surfaceOffset + 4);
                    byte id = ByteHelper.ReadByte(surfaceFileData, surfaceOffset + 5);
                    short flags = ByteHelper.ReadShort(surfaceFileData, surfaceOffset + 6);

                    Surfaces.Add(new Surface(null, type, id, flags));
                }

                surfaceOffset += 8;
            } while (surfaceInfo != 0 && surfaceOffset < surfaceFile.FileLength);

            //F3DEXReaderPackage p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x90); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);
            //p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x278); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);
            //p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x188); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);
            //p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x3C0); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);
            //p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x118); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);
            //p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x328); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);
            //p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x218); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);
            //p = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[3], 0x458); //Testing this out a bit!
            //foreach (N64DataElement el in p.Elements[RomProject.Instance.Files[3]])
            //    RomProject.Instance.Files[3].AddElement(el);

            //Make a union of the collections, but only on collections with elements with faces
            List<VO64GraphicsCollection> uniqueCollections = new List<VO64GraphicsCollection>();
            RenderGroupUnion = new VO64GraphicsCollection();
            foreach (VO64GraphicsCollection coll in collections)
            {
                if (!ContainsFaces(coll))
                    RenderGroupUnion.Add(coll);
                else
                {
                    if (!uniqueCollections.Contains(coll))
                    {
                        RenderGroupUnion.Add(coll);
                        uniqueCollections.Add(coll);
                    }
                }
            }

            SelectedRenderingGroup = RenderGroupUnion;
        }

        private static bool ContainsFaces(VO64GraphicsCollection collection)
        {
            if (collection.Elements.FirstOrDefault(e => !e.IsEmpty) != null)
                return true;

            foreach (VO64GraphicsCollection coll in collection.Collections)
            {
                if (ContainsFaces(coll))
                    return true;
            }

            return false;

        }
    }
}
