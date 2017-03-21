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
using Pitstop64.Data.Tracks;
using Pitstop64.Services.Hub;
using Pitstop64.Data.Tracks.Compressed;
using System.IO;
using Pitstop64.Modules.Textures;
using Cereal64.Common.Utils;
using System.Xml.Linq;
using Ionic.Zip;

namespace Pitstop64.Modules.Tracks
{
    public partial class TrackControl : UserControl
    {
        private bool SettingsChanged { get { return _settingsChanged; } set { _settingsChanged = value; btnTracksApply.Enabled = value; } }
        private bool _settingsChanged;

        private const string TRACK_SHACK_EXEC = @"TrackShack.exe";

        public TrackControl()
        {
            InitializeComponent();

            UpdateControl();

            btnTrackShack.Enabled = File.Exists(TRACK_SHACK_EXEC);
        }

        public void UpdateControl()
        {
            UpdateTrackList();
            UpdateTrackInfo();
            UpdateSelectedTrackList();
            UpdateTexture();

            UpdateEnableds();
            UpdateTextureEnabled();

            SettingsChanged = false;
        }

        private void SaveChanges()
        {
            SaveSelectedTrackChanges();

            SettingsChanged = false;
        }

        private void btnTracksApply_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void UpdateEnableds()
        {
            bool enabled = MarioKart64ElementHub.Instance.Tracks.Count > 0;
            gbTracks.Enabled = enabled;
            gbSelectedTracks.Enabled = enabled;
        }

        private void btnTracksCancel_Click(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void btnTrackShack_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(TRACK_SHACK_EXEC);
        }

        #region TrackInfo

        private void UpdateTrackList()
        {
            CompressedTrack selectedTrack = SelectedTrack;
            lbAllTracks.Items.Clear();

            foreach (CompressedTrack track in MarioKart64ElementHub.Instance.Tracks)
            {
                lbAllTracks.Items.Add(track);
            }

            if (selectedTrack != null && lbAllTracks.Items.Contains(selectedTrack))
                lbAllTracks.SelectedItem = selectedTrack;
            else if (lbAllTracks.Items.Count > 0)
                lbAllTracks.SelectedIndex = 0;
        }
        
        private CompressedTrack SelectedTrack
        {
            get
            {
                if (lbAllTracks.SelectedItems.Count > 1)
                    return null;

                return (CompressedTrack)lbAllTracks.SelectedItem;
            }
        }

        private void lbAllTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTrackInfo();
        }

        private void UpdateTrackInfo()
        {
            //Bring up level portrait & name?
            //Change export button enabled?
        }

        private void btnExportTrack_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Convert the track into a trackinfo, and save it
                TrackInfo track = SelectedTrack.GetAsExportableTrack();

                TrackInfo.SaveTrackInfo(saveFileDialog.FileName, track);

            }
        }

        private void btnImportTrack_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //open it up
                TrackInfo tr = TrackInfo.LoadFromFile(openFileDialog.FileName);


                //debug
                tr.TrackName = "TestLevel";


                bool replaceTrack = false;
                if (MarioKart64ElementHub.Instance.Tracks.FindIndex(t => t.TrackName == tr.TrackName) != -1)
                {
                    replaceTrack = MessageBox.Show("Track with same name already exists. Do you want to replace it?", "Warning", MessageBoxButtons.YesNo) == DialogResult.OK;
                }

                MarioKart64ElementHub.Instance.ConvertNewTrack(tr, replaceTrack);

                MessageBox.Show("Successfully loaded!");
            }
        }

        #endregion

        #region SelectedTracks


        private void btnTrackUp_Click(object sender, EventArgs e)
        {
            //Move the selected track up
            if (lbTracks.SelectedIndex != 0)
            {
                CompressedTrack tempTrack = (CompressedTrack)lbTracks.SelectedItem;
                lbTracks.Items[lbTracks.SelectedIndex] = lbTracks.Items[lbTracks.SelectedIndex - 1];
                lbTracks.Items[lbTracks.SelectedIndex - 1] = tempTrack;

                SettingsChanged = true;
                lbTracks.SelectedIndex--;
            }
        }

        private void btnTrackDown_Click(object sender, EventArgs e)
        {
            //Move the selected track down
            if (lbTracks.SelectedIndex != lbTracks.Items.Count - 1)
            {
                CompressedTrack tempTrack = (CompressedTrack)lbTracks.SelectedItem;
                lbTracks.Items[lbTracks.SelectedIndex] = lbTracks.Items[lbTracks.SelectedIndex + 1];
                lbTracks.Items[lbTracks.SelectedIndex + 1] = tempTrack;

                SettingsChanged = true;
                lbTracks.SelectedIndex++;
            }
        }

        private void btnInsertTrack_Click(object sender, EventArgs e)
        {
            //Replace the selected track with the track from the dropdown list
            if (cbTrackList.SelectedIndex != -1)
            {
                lbTracks.Items[lbTracks.SelectedIndex] = (CompressedTrack)cbTrackList.SelectedItem;

                SettingsChanged = true;
                UpdateSelectedTrackButtons();
            }

        }

        private void btnTracksReset_Click(object sender, EventArgs e)
        {
            //Reset the tracks to its original order
            for (int i = 0; i < 8; i++)
            {
                lbTracks.Items[i] = MarioKart64ElementHub.Instance.Tracks[i]; //Should be loaded in order, never changing order
            }

            SettingsChanged = false;
            UpdateSelectedTrackButtons();
        }

        private void UpdateSelectedTrackButtons()
        {
            if (MarioKart64ElementHub.Instance.Karts.Count == 0)
                btnTracksReset.Enabled = false;
            else
                btnTracksReset.Enabled = true;

            if (lbTracks.SelectedIndex != -1)
            {
                btnTrackUp.Enabled = true;
                btnTrackDown.Enabled = true;
                btnInsertTrack.Enabled = true;
            }
            else
            {
                btnTrackUp.Enabled = false;
                btnTrackDown.Enabled = false;
                btnInsertTrack.Enabled = false;
            }
        }

        private void lbTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedTrackButtons();
        }

        private void UpdateSelectedTrackList()
        {
            CompressedTrack selectedTrack = (CompressedTrack)lbTracks.SelectedItem;

            lbTracks.Items.Clear();
            cbTrackList.Items.Clear();

            if (MarioKart64ElementHub.Instance.Tracks.Count == 0)
                return;

            foreach (CompressedTrack track in MarioKart64ElementHub.Instance.SelectedTracks)
            {
                lbTracks.Items.Add(track);
            }

            foreach (CompressedTrack track in MarioKart64ElementHub.Instance.Tracks)
            {
                cbTrackList.Items.Add(track);
            }

            if (selectedTrack != null && cbTrackList.Items.Contains(selectedTrack))
                cbTrackList.SelectedItem = selectedTrack;
            else if (cbTrackList.Items.Count > 0)
                cbTrackList.SelectedIndex = 0;

            UpdateSelectedTrackButtons();
        }

        private void SaveSelectedTrackChanges()
        {
            //Set the selected karts ordering
            for (int i = 0; i < MarioKart64ElementHub.Instance.SelectedTracks.Length; i++)
            {
                MarioKart64ElementHub.Instance.SelectedTracks[i] = (CompressedTrack)lbTracks.Items[i];
            }

            UpdateSelectedTrackButtons();
        }


        #endregion

        #region Textures

        private void UpdateTexture()
        {
            CompressedTrack selectedTrack = SelectedTextureTrack;
            cbTrackList2.Items.Clear();

            foreach (CompressedTrack track in MarioKart64ElementHub.Instance.Tracks)
            {
                cbTrackList2.Items.Add(track);
            }

            if (selectedTrack != null && cbTrackList2.Items.Contains(selectedTrack))
                cbTrackList2.SelectedItem = selectedTrack;
            else if (lbAllTracks.Items.Count > 0)
                cbTrackList2.SelectedIndex = 0;
        }

        private void UpdateTextureEnabled()
        {
            btnReplaceImage.Enabled = (SelectedTextureTrack != null);
        }

        private CompressedTrack SelectedTextureTrack
        {
            get
            {
                if (cbTrackList2.SelectedItem == null)
                    return null;

                return (CompressedTrack)cbTrackList2.SelectedItem;
            }
        }

        private TrackTextureRef SelectedTexture
        {
            get
            {
                if (lbTrackImages.SelectedItem == null)
                    return null;

                return (TrackTextureRef)lbTrackImages.SelectedItem;
            }
        }

        private void cbTrackList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update the track texture ref lb
            lbTrackImages.Items.Clear();

            if (SelectedTextureTrack != null)
            {
                foreach (TrackTextureRef tRef in SelectedTextureTrack.TextureBlock.TextureReferences)
                {
                    lbTrackImages.Items.Add(tRef);
                }
            }

            btnTopSky.BackColor = SelectedTextureTrack.TopSkyColor;
            btnBottomSky.BackColor = SelectedTextureTrack.BottomSkyColor;
        }

        private void lbTrackImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update the image preview & track button enabled
            UpdateTextureDisplay();
            UpdateTextureEnabled();
        }

        private void UpdateTextureDisplay()
        {
            if (SelectedTexture != null && SelectedTexture.ImageReference != null)
                imagePreviewControl.Image = SelectedTexture.ImageReference.Image;
            else
                imagePreviewControl.Image = null;
        }

        private void btnReplaceImage_Click(object sender, EventArgs e)
        {
            //If another MK64Image of the same size is chosen, you can use it instead

            SelectMK64ImageForm form = new SelectMK64ImageForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                MK64Image newImage = form.SelectedImage;

                if (newImage == null)
                    return;

                if (newImage.Height != SelectedTexture.ImageReference.Height ||
                    newImage.Width != SelectedTexture.ImageReference.Width)
                {
                    MessageBox.Show("Image does not match the same size as old image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newImage.TextureEncoding != MK64Image.MK64ImageEncoding.MIO0 || newImage.TextureBlockOffset != 0)
                {
                    MessageBox.Show("Image must be MIO0 encoded in its own block!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newImage.Format != SelectedTexture.ImageReference.Format || newImage.PixelSize != SelectedTexture.ImageReference.PixelSize)
                {
                    MessageBox.Show("Image must match the old image's format and pixel size!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Looks good friend!
                SelectedTexture.ImageReference = newImage;

                UpdateTextureDisplay();
            }
        }

        #endregion

        private void btnDebugExport_Click(object sender, EventArgs e)
        {
            if (SelectedTrack != null)
            {
                if (!Directory.Exists(Path.Combine("Tracks", SelectedTrack.TrackName)))
                    Directory.CreateDirectory(Path.Combine("Tracks", SelectedTrack.TrackName));

                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-items.bin", SelectedTrack.TrackName)), SelectedTrack.ItemBlock.ItemData.DecodedData);
                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-textures.bin", SelectedTrack.TrackName)), SelectedTrack.TextureBlock.RawData);
                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-vertices.bin", SelectedTrack.TrackName)), ByteHelper.CombineIntoBytes(VertexPacker.BytesToVertices(SelectedTrack.VertexBlock.VertexData.DecodedData.ToList())));
                List<F3DEXCommand> commands = F3DEXPacker.BytesToCommands(SelectedTrack.VertexBlock.DLData.ToList());
                byte[] commandData = ByteHelper.CombineIntoBytes(commands);
                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-dl.bin", SelectedTrack.TrackName)), commandData);

                //debug stuff
                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-dl-pre.bin", SelectedTrack.TrackName)), SelectedTrack.VertexBlock.DLData);
                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-dl-post.bin", SelectedTrack.TrackName)),
                    F3DEXPacker.CommandsToBytes(commands).ToArray());

                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-v-pre.bin", SelectedTrack.TrackName)), SelectedTrack.VertexBlock.VertexData.DecodedData);
                File.WriteAllBytes(Path.Combine("Tracks", SelectedTrack.TrackName, string.Format("{0}-v-post.bin", SelectedTrack.TrackName)),
                    VertexPacker.VerticesToBytes(VertexPacker.BytesToVertices(SelectedTrack.VertexBlock.VertexData.DecodedData.ToList())).ToArray());
            }
        }

        private void btnTopSky_Click(object sender, EventArgs e)
        {
            colorDialog.Color = SelectedTrack.TopSkyColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedTrack.TopSkyColor = colorDialog.Color;
                btnTopSky.BackColor = colorDialog.Color;
            }
        }

        private void btnBottomSky_Click(object sender, EventArgs e)
        {
            colorDialog.Color = SelectedTrack.BottomSkyColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedTrack.BottomSkyColor = colorDialog.Color;
                btnBottomSky.BackColor = colorDialog.Color;
            }
        }


        #region oldCode (DELETE this)
        //private void btnLoad_Click(object sender, EventArgs e)
        //{
        //    if (cbTrack.SelectedIndex != -1)
        //    {
        //        //This is still in the old format, so we'll just 
        //        openGLControl.ClearGraphics();
        //        PortedLoadingCode();
        //        openGLControl.RefreshGraphics();
        //    }
        //}
        
        //private void PortedLoadingCode()
        //{
        //    byte[] _romData = RomProject.Instance.Files[0].GetAsBytes();
        //    TrackDataReferenceEntry SelectedTrack = MarioKart64ElementHub.Instance.TrackTable.Entries[cbTrack.SelectedIndex];

        //    while (RomProject.Instance.Files.Count > 1)
        //    {
        //        RomProject.Instance.RemoveRomFile(RomProject.Instance.Files.Last());
        //    }

        //    //Take the blocks, and export them
        //    byte[] displayListBlock = new byte[SelectedTrack.DisplayListBlockEnd - SelectedTrack.DisplayListBlockStart];
        //    Array.Copy(_romData, SelectedTrack.DisplayListBlockStart,
        //        displayListBlock, 0, displayListBlock.Length);
        //    int vertexEndPackedDLStartOffset = SelectedTrack.DisplayListOffset & 0x00FFFFFF;
        //    byte[] vertexBlock = new byte[vertexEndPackedDLStartOffset];
        //    Array.Copy(_romData, SelectedTrack.VertexBlockStart,
        //        vertexBlock, 0, vertexBlock.Length);
        //    byte[] packedBlock = new byte[(SelectedTrack.VertexBlockEnd - SelectedTrack.VertexBlockStart) - vertexEndPackedDLStartOffset];
        //    Array.Copy(_romData, SelectedTrack.VertexBlockStart + vertexEndPackedDLStartOffset,
        //        packedBlock, 0, packedBlock.Length);
        //    byte[] textureBlock = new byte[SelectedTrack.TextureBlockEnd - SelectedTrack.TextureBlockStart];
        //    Array.Copy(_romData, SelectedTrack.TextureBlockStart,
        //        textureBlock, 0, textureBlock.Length);

        //    byte[] decodedDLData = Cereal64.Common.Utils.Encoding.MIO0.Decode(displayListBlock);

        //    List<Vertex> vertices = VertexPacker.BytesToVertices(Cereal64.Common.Utils.Encoding.MIO0.Decode(vertexBlock).ToList());
        //    VertexCollection vertCollection = new VertexCollection(0x00, vertices);
        //    byte[] vertsData = vertCollection.RawData;

        //    List<F3DEXCommand> commands = F3DEXPacker.BytesToCommands(packedBlock.ToList());
        //    F3DEXCommandCollection commandColl = new F3DEXCommandCollection(0x00, commands);
        //    byte[] commandsData = commandColl.RawData;

        //    f3DEXEditor1.Commands = commandColl;

        //    List<TrackTextureRef> textureSegPointers = ReadTextureBank(textureBlock);

        //    byte[] textureSegData = new byte[textureSegPointers.Sum(t => t.DecompressedSize)];
        //    int bytePointer = 0;
        //    List<string> offsets = new List<string>();
        //    List<int> pointers = new List<int>();
        //    for (int i = 0; i < textureSegPointers.Count; i++)
        //    {
        //        int mioSize = textureSegPointers[i].CompressedSize;
        //        if (mioSize % 4 != 0)
        //            mioSize += 4 - (mioSize % 4);
        //        byte[] tempHolder = new byte[mioSize];
        //        Array.Copy(_romData, (textureSegPointers[i].RomOffset & 0x00FFFFFF) + MarioKartRomInfo.TextureBankOffset,
        //            tempHolder, 0, mioSize);
        //        byte[] decompressed = Cereal64.Common.Utils.Encoding.MIO0.Decode(tempHolder);
        //        Array.Copy(decompressed, 0, textureSegData, bytePointer, decompressed.Length);
        //        pointers.Add(bytePointer);
        //        bytePointer += decompressed.Length;
        //        offsets.Add(((textureSegPointers[i].RomOffset & 0x00FFFFFF) + MarioKartRomInfo.TextureBankOffset).ToString("X"));
        //    }

        //    //Use the F3DEXReader here
        //    RomProject.Instance.AddRomFile(new RomFile("Verts", 1, new Cereal64.Common.DataElements.UnknownData(0x00, vertsData)));
        //    //RomProject.Instance.Files[0].FileLength = vertsData.Length;
        //    RomProject.Instance.AddRomFile(new RomFile("PackedDLs", 2, new Cereal64.Common.DataElements.UnknownData(0x00, commandsData)));
        //    //RomProject.Instance.Files[1].FileLength = commandsData.Length;
        //    RomProject.Instance.AddRomFile(new RomFile("Textures", 3, new Cereal64.Common.DataElements.UnknownData(0x00, textureSegData)));
        //    //RomProject.Instance.Files[2].FileLength = textureSegData.Length;

        //    //Here we'll assume that there's only 1 file (the full rom) in the rom project
        //    if (RomProject.Instance.DMAProfiles.FirstOrDefault(dp => dp.ProfileName == "Levelviewer") != null)
        //    {
        //        RomProject.Instance.RemoveDmaProfile(RomProject.Instance.DMAProfiles.FirstOrDefault(dp => dp.ProfileName == "Levelviewer"));
        //    }

        //    DmaProfile profile = new DmaProfile("Levelviewer");
        //    DmaSegment segment = new DmaSegment();
        //    segment.File = RomProject.Instance.Files[1];
        //    segment.RamSegment = 0x04;
        //    segment.RamStartOffset = 0x00;
        //    segment.FileStartOffset = 0x00;
        //    segment.FileEndOffset = segment.File.FileLength;
        //    segment.TagInfo = "Vertices";
        //    profile.AddDmaSegment(0x04, segment);
        //    segment = new DmaSegment();
        //    segment.File = RomProject.Instance.Files[2];
        //    segment.RamSegment = 0x07;
        //    segment.RamStartOffset = 0x00;
        //    segment.FileStartOffset = 0x00;
        //    segment.FileEndOffset = segment.File.FileLength;
        //    segment.TagInfo = "PackedDLs";
        //    profile.AddDmaSegment(0x07, segment);
        //    segment = new DmaSegment();
        //    segment.File = RomProject.Instance.Files[3];
        //    segment.RamSegment = 0x05;
        //    segment.RamStartOffset = 0x00;
        //    segment.FileStartOffset = 0x00;
        //    segment.FileEndOffset = segment.File.FileLength;
        //    segment.TagInfo = "Textures";
        //    profile.AddDmaSegment(0x05, segment);
        //    RomProject.Instance.AddDmaProfile(profile);

        //    F3DEXReaderPackage package = new F3DEXReaderPackage();// F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[2], 0x00);
        //    F3DEXReaderPackage newPackage = package;
        //    newPackage = null;

        //    if (package.Elements[RomProject.Instance.Files[2]][0] is F3DEXCommandCollection)
        //    {
        //        openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)package.Elements[RomProject.Instance.Files[2]][0]));
        //    }
        //}


        //private List<TrackTextureRef> ReadTextureBank(byte[] texturePointers)
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
#endregion
    }
}
