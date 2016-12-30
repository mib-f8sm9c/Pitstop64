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
    public partial class CourseControl : UserControl
    {

        public CourseControl()
        {
            InitializeComponent();

            UpdateControl();
        }

        public void UpdateControl()
        {
            cbCourse.Items.Clear();

            if (RomProject.Instance.Files.Count > 0  && MarioKart64ElementHub.Instance.CourseDataBlock != null)
            {
                cbCourse.Enabled = true;

                for (int i = 0; i < MarioKartRomInfo.CourseCount; i++)
                {
                    //Add all the courses here. But they're going to be loaded in the Mariokart hub
                    cbCourse.Items.Add(Enum.GetName(typeof(MarioKartRomInfo.OriginalCourses), (MarioKartRomInfo.OriginalCourses)i));
                }
                cbCourse.SelectedIndex = 0;
            }
            else
            {
                cbCourse.Enabled = false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cbCourse.SelectedIndex != -1)
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
            CourseDataReferenceEntry SelectedCourse = MarioKart64ElementHub.Instance.CourseDataBlock.entries[cbCourse.SelectedIndex];

            while (RomProject.Instance.Files.Count > 1)
            {
                RomProject.Instance.RemoveRomFile(RomProject.Instance.Files.Last());
            }

            //Take the blocks, and export them
            byte[] displayListBlock = new byte[SelectedCourse.DisplayListBlockEnd - SelectedCourse.DisplayListBlockStart];
            Array.Copy(_romData, SelectedCourse.DisplayListBlockStart,
                displayListBlock, 0, displayListBlock.Length);
            int vertexEndPackedDLStartOffset = SelectedCourse.DisplayListOffset & 0x00FFFFFF;
            byte[] vertexBlock = new byte[vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedCourse.VertexBlockStart,
                vertexBlock, 0, vertexBlock.Length);
            byte[] packedBlock = new byte[(SelectedCourse.VertexBlockEnd - SelectedCourse.VertexBlockStart) - vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedCourse.VertexBlockStart + vertexEndPackedDLStartOffset,
                packedBlock, 0, packedBlock.Length);
            byte[] textureBlock = new byte[SelectedCourse.TextureBlockEnd - SelectedCourse.TextureBlockStart];
            Array.Copy(_romData, SelectedCourse.TextureBlockStart,
                textureBlock, 0, textureBlock.Length);

            byte[] decodedDLData = Cereal64.Common.Utils.Encoding.MIO0.Decode(displayListBlock);

            List<Vertex> vertices = VertexPacker.BytesToVertices(Cereal64.Common.Utils.Encoding.MIO0.Decode(vertexBlock).ToList());
            VertexCollection vertCollection = new VertexCollection(0x00, vertices);
            byte[] vertsData = vertCollection.RawData;

            List<F3DEXCommand> commands = F3DEXPacker.BytesToCommands(packedBlock.ToList());
            F3DEXCommandCollection commandColl = new F3DEXCommandCollection(0x00, commands);
            byte[] commandsData = commandColl.RawData;

            List<CourseTextureRef> textureSegPointers = ReadTextureBank(textureBlock);

            byte[] textureSegData = new byte[textureSegPointers.Sum(t => t.DecompressedSize)];
            int bytePointer = 0;
            for (int i = 0; i < textureSegPointers.Count; i++)
            {
                byte[] tempHolder = new byte[textureSegPointers[i].CompressedSize];
                Array.Copy(_romData, (textureSegPointers[i].RomOffset & 0x00FFFFFF) + MarioKartRomInfo.TextureBankOffset,
                    tempHolder, 0, textureSegPointers[i].CompressedSize);
                byte[] decompressed = Cereal64.Common.Utils.Encoding.MIO0.Decode(tempHolder);
                Array.Copy(decompressed, 0, textureSegData, bytePointer, decompressed.Length);
                bytePointer += decompressed.Length;
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

            F3DEXReaderPackage package = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[2], 0x00);
            F3DEXReaderPackage newPackage = package;
            newPackage = null;

            if (package.Elements[RomProject.Instance.Files[2]][0] is F3DEXCommandCollection)
            {
                openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)package.Elements[RomProject.Instance.Files[2]][0]));
            }
        }


        private List<CourseTextureRef> ReadTextureBank(byte[] texturePointers)
        {
            List<CourseTextureRef> output = new List<CourseTextureRef>();
            byte[] tempArray = new byte[0x10];

            for (int i = 0; i < texturePointers.Length / 0x10; i++)
            {
                Array.Copy(texturePointers, i * 0x10, tempArray, 0, 0x10);
                CourseTextureRef refText = new CourseTextureRef(i * 0x10, tempArray);
                if (refText.RomOffset == 0x00000000)
                    break;

                output.Add(refText);
            }

            return output;
        }

    }
}
