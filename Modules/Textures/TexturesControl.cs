using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pitstop64.Data;
using Pitstop64.Services.Hub;
using Pitstop64.Modules.Textures.SubControls;
using Cereal64.Common.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Utils;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Common.Utils.Encoding;
using System.IO;

namespace Pitstop64.Modules.Textures
{
    public partial class TexturesControl : UserControl
    {
        public delegate void ImageUpdatedEvent();

        public TexturesControl()
        {
            InitializeComponent();

            _controls = new Dictionary<ActiveControlTypes, ITextureViewControl>();
            cbImageType.SelectedIndex = 0;
        }

        private ITextureViewControl ActiveControl;
        private NewMK64ImageForm _newImageForm;

        private enum ActiveControlTypes
        {
            TKMK,
            CI,
            Others
        }

        public enum SortTypes
        {
            All,
            RGBA,
            CI,
            IA,
            I,
            MIO0,
            TKMK00,
            Raw
        }

        private ActiveControlTypes _activeControlType;

        private Dictionary<ActiveControlTypes, ITextureViewControl> _controls;

        public void UpdateControl()
        {
            SetImages(MarioKart64ElementHub.Instance.TextureHub.Images);
        }

        public void SetImages(IList<MK64Image> images, SortTypes sortType = SortTypes.All, string filterText = "")
        {
            lbImages.Items.Clear();

            string cleanFilterText = filterText.Trim().ToLower();

            foreach (MK64Image image in images)
            {
                switch (sortType)
                {
                    case SortTypes.CI:
                        if (image.Format != Cereal64.Microcodes.F3DEX.DataElements.Texture.ImageFormat.CI)
                            continue;
                        break;
                    case SortTypes.I:
                        if (image.Format != Cereal64.Microcodes.F3DEX.DataElements.Texture.ImageFormat.I)
                            continue;
                        break;
                    case SortTypes.IA:
                        if (image.Format != Cereal64.Microcodes.F3DEX.DataElements.Texture.ImageFormat.IA)
                            continue;
                        break;
                    case SortTypes.RGBA:
                        if (image.Format != Cereal64.Microcodes.F3DEX.DataElements.Texture.ImageFormat.RGBA)
                            continue;
                        break;
                    case SortTypes.MIO0:
                        if (image.TextureEncoding != MK64Image.MK64ImageEncoding.MIO0)
                            continue;
                        break;
                    case SortTypes.TKMK00:
                        if (image.TextureEncoding != MK64Image.MK64ImageEncoding.TKMK00)
                            continue;
                        break;
                    case SortTypes.Raw:
                        if (image.TextureEncoding != MK64Image.MK64ImageEncoding.Raw)
                            continue;
                        break;
                }

                if (!string.IsNullOrWhiteSpace(cleanFilterText))
                {
                    if (!image.ImageName.ToLower().Contains(cleanFilterText))
                        continue;
                }

                lbImages.Items.Add(image);
            }
            UpdateImageCount();
        }

        private void UpdateImageCount()
        {
            lblImageCount.Text = "Images: " + lbImages.Items.Count;
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            if(_newImageForm == null)
                _newImageForm = new NewMK64ImageForm();

            if (_newImageForm.ShowDialog() == DialogResult.OK)
            {
                List<MK64Image> newImages = new List<MK64Image>();
                foreach (string FileName in _newImageForm.FileNames)
                {
                    //To do: make it load multiple at a time!
                    Bitmap bmp = (Bitmap)Bitmap.FromFile(FileName);

                    if (bmp == null)
                    {
                        MessageBox.Show("Could not load image:\n" + FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    byte[] imageData;

                    Texture texture;
                    Palette palette = null;

                    if (_newImageForm.Format == Texture.ImageFormat.CI)
                    {
                        byte[] paletteData = new byte[2 * _newImageForm.PaletteColorCount];
                        palette = new Palette(-1, paletteData);
                        imageData = TextureConversion.ImageToBinary(_newImageForm.Format, _newImageForm.PixelSize, bmp,
                            ref palette, true);
                    }
                    else
                    {
                        imageData = TextureConversion.ImageToBinary(_newImageForm.Format, _newImageForm.PixelSize, bmp);
                    }

                    texture = new Texture(-1, imageData, _newImageForm.Format, _newImageForm.PixelSize,
                        bmp.Width, bmp.Height);

                    //Now, if necessary, encode the texture!!
                    MIO0Block textureBlock = null, paletteBlock = null;
                    if (_newImageForm.EncodeTexture)
                    {
                        byte[] encodedData = MIO0.Encode(texture.RawData);
                        textureBlock = new MIO0Block(-1, encodedData);
                        textureBlock.AddElement(texture);
                        texture.FileOffset = 0;
                    }

                    if (palette != null && _newImageForm.EncodePalette)
                    {
                        byte[] encodedData = MIO0.Encode(palette.RawData);
                        paletteBlock = new MIO0Block(-1, encodedData);
                        paletteBlock.AddElement(palette);
                        palette.FileOffset = 0;
                    }

                    //Add in the texture/palette to the new data location
                    if (textureBlock != null)
                    {
                        textureBlock.FileOffset = MarioKart64ElementHub.Instance.NewElementOffset;
                        RomProject.Instance.Files[0].AddElement(textureBlock);
                        MarioKart64ElementHub.Instance.AdvanceNewElementOffset(textureBlock);
                    }
                    else
                    {
                        texture.FileOffset = MarioKart64ElementHub.Instance.NewElementOffset;
                        RomProject.Instance.Files[0].AddElement(texture);
                        MarioKart64ElementHub.Instance.AdvanceNewElementOffset(texture);
                    }

                    if (palette != null)
                    {
                        if (paletteBlock != null)
                        {
                            paletteBlock.FileOffset = MarioKart64ElementHub.Instance.NewElementOffset;
                            RomProject.Instance.Files[0].AddElement(paletteBlock);
                            MarioKart64ElementHub.Instance.AdvanceNewElementOffset(paletteBlock);
                        }
                        else
                        {
                            palette.FileOffset = MarioKart64ElementHub.Instance.NewElementOffset;
                            RomProject.Instance.Files[0].AddElement(palette);
                            MarioKart64ElementHub.Instance.AdvanceNewElementOffset(palette);
                        }
                    }

                    //Add in the new MK64Image
                    int tFileOffset = (textureBlock == null ? texture.FileOffset : textureBlock.FileOffset);
                    MK64Image.MK64ImageEncoding tEncoding = (textureBlock == null ? MK64Image.MK64ImageEncoding.Raw : MK64Image.MK64ImageEncoding.MIO0);
                    int tBlockOffset = (textureBlock == null ? -1 : texture.FileOffset);
                    List<int> PaletteOffset = new List<int>();
                    List<MK64Image.MK64ImageEncoding> PaletteEncodings = new List<MK64Image.MK64ImageEncoding>();
                    List<int> PaletteBlockOffset = new List<int>();
                    List<int> PaletteColorCount = new List<int>();
                    List<int> PaletteColorOffset = new List<int>();
                    if (palette != null)
                    {
                        PaletteOffset.Add(paletteBlock == null ? palette.FileOffset : paletteBlock.FileOffset);
                        PaletteEncodings.Add(paletteBlock == null ? MK64Image.MK64ImageEncoding.Raw : MK64Image.MK64ImageEncoding.MIO0);
                        PaletteBlockOffset.Add(paletteBlock == null ? -1 : palette.FileOffset);
                        PaletteColorCount.Add(palette.Colors.Length);
                        PaletteColorOffset.Add(0);
                    }

                    MK64Image newImage = new MK64Image(tFileOffset,
                        tEncoding,
                        tBlockOffset,
                        _newImageForm.Format,
                        _newImageForm.PixelSize,
                        bmp.Width,
                        bmp.Height,
                        false,
                        PaletteOffset,
                        PaletteEncodings,
                        PaletteBlockOffset,
                        PaletteColorCount,
                        PaletteColorOffset,
                        0, 0, Path.GetFileNameWithoutExtension(FileName));

                    MarioKart64ElementHub.Instance.TextureHub.AddImage(newImage);
                    newImages.Add(newImage);
                }

                if (newImages.Count > 1)
                    MessageBox.Show(string.Format("{0} images loaded!", newImages.Count), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (newImages.Count > 0)
                {
                    int newSelectedIndex = lbImages.Items.Count;
                    lbImages.Items.AddRange(newImages.ToArray());

                    //Update the image count & selected
                    lbImages.SelectedIndex = newSelectedIndex;
                    UpdateImageCount();
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            //Don't allow deleting original images!!
            if (SelectedImage == null)
            {
                MessageBox.Show("Error: No image selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SelectedImage.IsOriginalImage)
            {
                MessageBox.Show("Error: Image is not custom, cannot delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MK64Image image = SelectedImage;

            lbImages.SelectedIndex = lbImages.SelectedIndex - 1;
            MarioKart64ElementHub.Instance.TextureHub.RemoveImage(image);
            lbImages.Items.Remove(image);

            UpdateImageCount();

            //Code here to remove the texture/palette if not used??
        }

        private void cbImageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateImageFiltering();
        }

        private void txtSearchImages_TextChanged(object sender, EventArgs e)
        {
            UpdateImageFiltering();
        }

        private void UpdateImageFiltering()
        {
            if (cbImageType.SelectedIndex == -1)
                cbImageType.SelectedIndex = 0;

            SortTypes sortType = (SortTypes)cbImageType.SelectedIndex;
            SetImages(MarioKart64ElementHub.Instance.TextureHub.Images, sortType, txtSearchImages.Text);
        }

        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedImage();
        }

        private MK64Image SelectedImage
        {
            get
            {
                return (MK64Image)lbImages.SelectedItem;
            }
        }

        private void DisplaySelectedImage()
        {
            if (lbImages.SelectedItem != null)
            {
                MK64Image selectedImage = (MK64Image)lbImages.SelectedItem;
                ActiveControlTypes newType;
                if (selectedImage.TKMKReference != null)
                    newType = ActiveControlTypes.TKMK;
                else if (selectedImage.Format == Cereal64.Microcodes.F3DEX.DataElements.Texture.ImageFormat.CI)
                    newType = ActiveControlTypes.CI;
                else
                    newType = ActiveControlTypes.Others;

                if (newType != _activeControlType || ActiveControl == null)
                    SetActiveType(newType);

                SetPreviewImage(selectedImage);
            }
            else
            {
                if (ActiveControl != null)
                    ActiveControl.Image = null;
            }
        }

        private void SetPreviewImage(MK64Image selectedImage)
        {
            if (selectedImage == null || selectedImage.Image == null)
            {
                imagePreviewControl.Image = null;
            }
            else
            {
                imagePreviewControl.Image = selectedImage.Image;
                imagePreviewControl.ImageName = selectedImage.ImageName;
            }

            ActiveControl.Image = selectedImage;
        }

        private void SetActiveType(ActiveControlTypes type)
        {
            DeactivateControl();
            _activeControlType = type;
            ActivateControl();
        }

        private void ActivateControl()
        {
            if (_controls.ContainsKey(_activeControlType) && _controls[_activeControlType] != null)
            {
                ActiveControl = _controls[_activeControlType];
            }
            else
            {
                ITextureViewControl control = null;
                switch (_activeControlType)
                {
                    case ActiveControlTypes.TKMK:
                        control = new TKMKViewControl();
                        break;
                    default:
                        control = new TextureViewControl();
                        break;
                }

                pnlTools.Controls.Add(control.GetAsControl());
                _controls.Add(_activeControlType, control);
                ActiveControl = control;
            }

            ActiveControl.ImageUpdated += ImageUpdated;
            ActiveControl.Activate();
        }

        private void DeactivateControl()
        {
            if (_controls.ContainsKey(_activeControlType) && _controls[_activeControlType] != null)
                _controls[_activeControlType].Deactivate();

            if (ActiveControl != null)
                ActiveControl.ImageUpdated -= ImageUpdated;

            ActiveControl = null;
        }

        private void ImageUpdated()
        {
            DisplaySelectedImage();
        }
    }
}
