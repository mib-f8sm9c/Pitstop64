using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;
using Pitstop64.Services;
using Pitstop64.Data;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DEX;
using Cereal64.Microcodes.F3DEX;
using Pitstop64.Data.Courses;
using Pitstop64.Services.Hub;

namespace Pitstop64.Modules.Courses
{
    public partial class TrackControl : UserControl
    {
        public TrackControl()
        {
            InitializeComponent();

            UpdateControl();
        }

        public void UpdateControl()
        {
            cbTrack.Items.Clear();

            if (RomProject.Instance.Files.Count > 0  && MarioKart64ElementHub.Instance.TrackDataBlock != null)
            {
                cbTrack.Enabled = true;

                for (int i = 0; i < MarioKartRomInfo.TrackCount; i++)
                {
                    //Add all the tracks here. But they're going to be loaded in the Mariokart hub
                    cbTrack.Items.Add(Enum.GetName(typeof(MarioKartRomInfo.OriginalTracks), (MarioKartRomInfo.OriginalTracks)i));
                }
                cbTrack.SelectedIndex = 0;
            }
            else
            {
                cbTrack.Enabled = false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cbTrack.SelectedIndex != -1)
            {
                //This is still in the old format, so we'll just 
                openGLControl.ClearGraphics();
                PortedLoadingCode();
                openGLControl.RefreshGraphics();
            }
        }


        private void PortedLoadingCode()
        {
            byte[] _romData = RomProject.Instance.Files[0].GetAsBytes();
            TrackDataReferenceEntry SelectedTrack = MarioKart64ElementHub.Instance.TrackDataBlock.Entries[cbTrack.SelectedIndex];

            while (RomProject.Instance.Files.Count > 1)
            {
                RomProject.Instance.RemoveRomFile(RomProject.Instance.Files.Last());
            }

            //Take the blocks, and export them
            byte[] displayListBlock = new byte[SelectedTrack.DisplayListBlockEnd - SelectedTrack.DisplayListBlockStart];
            Array.Copy(_romData, SelectedTrack.DisplayListBlockStart,
                displayListBlock, 0, displayListBlock.Length);
            int vertexEndPackedDLStartOffset = SelectedTrack.DisplayListOffset & 0x00FFFFFF;
            byte[] vertexBlock = new byte[vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedTrack.VertexBlockStart,
                vertexBlock, 0, vertexBlock.Length);
            byte[] packedBlock = new byte[(SelectedTrack.VertexBlockEnd - SelectedTrack.VertexBlockStart) - vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedTrack.VertexBlockStart + vertexEndPackedDLStartOffset,
                packedBlock, 0, packedBlock.Length);
            byte[] textureBlock = new byte[SelectedTrack.TextureBlockEnd - SelectedTrack.TextureBlockStart];
            Array.Copy(_romData, SelectedTrack.TextureBlockStart,
                textureBlock, 0, textureBlock.Length);

            byte[] decodedDLData = Cereal64.Common.Utils.Encoding.MIO0.Decode(displayListBlock);

            List<Vertex> vertices = VertexPacker.BytesToVertices(Cereal64.Common.Utils.Encoding.MIO0.Decode(vertexBlock).ToList());
            VertexCollection vertCollection = new VertexCollection(0x00, vertices);
            byte[] vertsData = vertCollection.RawData;

            List<F3DEXCommand> commands = F3DEXPacker.BytesToCommands(packedBlock.ToList());
            F3DEXCommandCollection commandColl = new F3DEXCommandCollection(0x00, commands);
            byte[] commandsData = commandColl.RawData;

            f3DEXEditor1.Commands = commandColl;

            List<TrackTextureRef> textureSegPointers = ReadTextureBank(textureBlock);

            byte[] textureSegData = new byte[textureSegPointers.Sum(t => t.DecompressedSize)];
            int bytePointer = 0;
            List<string> offsets = new List<string>();
            List<int> pointers = new List<int>();
            for (int i = 0; i < textureSegPointers.Count; i++)
            {
                int mioSize = textureSegPointers[i].CompressedSize;
                if (mioSize % 4 != 0)
                    mioSize += 4 - (mioSize % 4);
                byte[] tempHolder = new byte[mioSize];
                Array.Copy(_romData, (textureSegPointers[i].RomOffset & 0x00FFFFFF) + MarioKartRomInfo.TextureBankOffset,
                    tempHolder, 0, mioSize);
                byte[] decompressed = Cereal64.Common.Utils.Encoding.MIO0.Decode(tempHolder);
                Array.Copy(decompressed, 0, textureSegData, bytePointer, decompressed.Length);
                pointers.Add(bytePointer);
                bytePointer += decompressed.Length;
                offsets.Add(((textureSegPointers[i].RomOffset & 0x00FFFFFF) + MarioKartRomInfo.TextureBankOffset).ToString("X"));
            }

            //Use the F3DEXReader here
            RomProject.Instance.AddRomFile(new RomFile("Verts", 1, new Cereal64.Common.DataElements.UnknownData(0x00, vertsData)));
            //RomProject.Instance.Files[0].FileLength = vertsData.Length;
            RomProject.Instance.AddRomFile(new RomFile("PackedDLs", 2, new Cereal64.Common.DataElements.UnknownData(0x00, commandsData)));
            //RomProject.Instance.Files[1].FileLength = commandsData.Length;
            RomProject.Instance.AddRomFile(new RomFile("Textures", 3, new Cereal64.Common.DataElements.UnknownData(0x00, textureSegData)));
            //RomProject.Instance.Files[2].FileLength = textureSegData.Length;

            //Here we'll assume that there's only 1 file (the full rom) in the rom project
            if (RomProject.Instance.DMAProfiles.FirstOrDefault(dp => dp.ProfileName == "Levelviewer") != null)
            {
                RomProject.Instance.RemoveDmaProfile(RomProject.Instance.DMAProfiles.FirstOrDefault(dp => dp.ProfileName == "Levelviewer"));
            }

            DmaProfile profile = new DmaProfile("Levelviewer");
            DmaSegment segment = new DmaSegment();
            segment.File = RomProject.Instance.Files[1];
            segment.RamSegment = 0x04;
            segment.RamStartOffset = 0x00;
            segment.FileStartOffset = 0x00;
            segment.FileEndOffset = segment.File.FileLength;
            segment.TagInfo = "Vertices";
            profile.AddDmaSegment(0x04, segment);
            segment = new DmaSegment();
            segment.File = RomProject.Instance.Files[2];
            segment.RamSegment = 0x07;
            segment.RamStartOffset = 0x00;
            segment.FileStartOffset = 0x00;
            segment.FileEndOffset = segment.File.FileLength;
            segment.TagInfo = "PackedDLs";
            profile.AddDmaSegment(0x07, segment);
            segment = new DmaSegment();
            segment.File = RomProject.Instance.Files[3];
            segment.RamSegment = 0x05;
            segment.RamStartOffset = 0x00;
            segment.FileStartOffset = 0x00;
            segment.FileEndOffset = segment.File.FileLength;
            segment.TagInfo = "Textures";
            profile.AddDmaSegment(0x05, segment);
            RomProject.Instance.AddDmaProfile(profile);

            F3DEXReaderPackage package = new F3DEXReaderPackage();// F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[2], 0x00);
            F3DEXReaderPackage newPackage = package;
            newPackage = null;

            if (package.Elements[RomProject.Instance.Files[2]][0] is F3DEXCommandCollection)
            {
                openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)package.Elements[RomProject.Instance.Files[2]][0]));
            }
        }


        private List<TrackTextureRef> ReadTextureBank(byte[] texturePointers)
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

    }
}
