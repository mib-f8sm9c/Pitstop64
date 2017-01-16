using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pitstop64.Data;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Common.Rom;
using Cereal64.Common.Utils.Encoding;
using Pitstop64.Services.Hub;
using Cereal64.Common.DataElements;

namespace Pitstop64.Modules.Textures.SubControls
{
    public partial class TextureViewControl : UserControl, ITextureViewControl
    {
        public TexturesControl.ImageUpdatedEvent ImageUpdated
        {
            get
            {
                return _imageUpdated;
            }
            set
            {
                _imageUpdated = value;
            }
        }
        private TexturesControl.ImageUpdatedEvent _imageUpdated = delegate { };

        public MK64Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                if (_image == null || _image.Image == null)
                {
                    lblName.Text = string.Empty;
                    lblSize.Text = string.Empty;
                    lblFormat.Text = string.Empty;
                    lblEncoding.Text = string.Empty;
                    btnEditPalette.Visible = false;

                    btnReplaceWith.Enabled = !ImageIsSpecialCaseCI();
                }
                else
                {
                    lblName.Text = _image.ImageName;
                    lblSize.Text = string.Format("{0}x{1}", _image.Width, _image.Height);
                    lblFormat.Text = _image.Format.ToString();
                    if (_image.TextureEncoding == MK64Image.MK64ImageEncoding.MIO0)
                        lblEncoding.Text = "MIO0 Encoded";
                    else
                        lblEncoding.Text = "No Encoding";
                    btnEditPalette.Visible = false;
                   // btnEditPalette.Visible = _image.Format == Texture.ImageFormat.CI;
                }
            }
        }
        private MK64Image _image;

        private int _specialMIO0Length = -1;

        public TextureViewControl()
        {
            InitializeComponent();
        }

        public void Activate()
        {
            this.Dock = DockStyle.Fill;
            this.Enabled = true;
            this.Visible = true;
        }

        public void Deactivate()
        {
            this.Visible = false;
            this.Enabled = false;
            this.Dock = DockStyle.None;
        }

        public UserControl GetAsControl()
        {
            return this;
        }

        private void btnReplaceWith_Click(object sender, EventArgs e)
        {
            if(ImageIsSpecialCaseCI())
                ReplaceSpecialCaseCI();
            else if (_image.TextureEncoding == MK64Image.MK64ImageEncoding.MIO0 || (_image.PaletteOffset.Count > 0 && _image.PaletteEncoding[0] == MK64Image.MK64ImageEncoding.MIO0))
                ReplaceMIO0();
            else
                ReplaceTexture();
        }

        private bool ImageIsSpecialCaseCI()
        {
            //Needs to be CI
            if (_image.Format != Texture.ImageFormat.CI)
                return false;

            //Needs to be registered in the Texture Hub & have a palette
            if (!MarioKart64ElementHub.Instance.TextureHub.HasImagesForTexture(_image.ImageReference.Texture) ||
                _image.ImageReference.BasePalettes.Count == 0 ||
                !MarioKart64ElementHub.Instance.TextureHub.HasImagesForPalette(_image.ImageReference.BasePalettes[0]))
                return false;

            //Needs to share the image or the palette among multiple images
            if (MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture(_image.ImageReference.Texture).Count <= 1 &&
                MarioKart64ElementHub.Instance.TextureHub.ImagesForPalette(_image.ImageReference.BasePalettes[0]).Count <= 1)
                return false;

            return true;
        }

        private void ReplaceSpecialCaseCI()
        {
            //Can't handle this right now
            //if (MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture(_image.ImageReference.Texture).Count > 1)
            //{
            //    MessageBox.Show("Error: Multi-palette editing currently not available!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            List<MK64Image> images = new List<MK64Image>();
            ComplexCIReplaceForm.SharedMode sharedMode;

            if (MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture(_image.ImageReference.Texture).Count > 1)
            {
                sharedMode = ComplexCIReplaceForm.SharedMode.Texture;
                images.AddRange(MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture(_image.ImageReference.Texture));
            }
            else
            {
                sharedMode = ComplexCIReplaceForm.SharedMode.Palette;
                images.AddRange(MarioKart64ElementHub.Instance.TextureHub.ImagesForPalette(_image.ImageReference.BasePalettes[0]));
            }

            ComplexCIReplaceForm form = new ComplexCIReplaceForm(images, sharedMode);
            form.ShowDialog();
        }

        private void ReplaceMIO0()
        {
            //Attempt to load in a new texture and replace. Will not work if the texture size is larger than the one it's replacing
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
                if (bmp == null)
                {
                    MessageBox.Show("Error: Couldn't load image file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (bmp.Height != _image.Height ||
                    bmp.Width != _image.Width)
                {
                    MessageBox.Show("Error: New image must be same size!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Think hard about this. If you're compressing the image and/or palette into MIO0, you need to test against that.
                // Also a form if there's more than one image being affected by this change? Before/afters?


                //So we need some way to change an F3DEXImage to a new bitmap. but it needs to change texture and palette appropriately,
                // and most importantly it needs to be able to stop halfway to see how it will affect other images. But if we want to change
                // the lakitu images, which will change the palette, you'd have to change all of the images at once, right? So how the hell
                // is this going to work out??

                //Also palette editing

                byte[] newData;
                byte[] newPaletteData = null;
                if (_image.Format == Texture.ImageFormat.CI)
                {
                    Palette palette = new Palette(-1, new byte[(_image.ImageReference.WorkingPalette.Colors.Length * 2) - 200]);
                    newData = TextureConversion.ImageToBinary(_image.Format, _image.PixelSize, bmp, ref palette);
                    newPaletteData = palette.RawData;
                    if (newPaletteData.Length < (_image.ImageReference.WorkingPalette.Colors.Length * 2))
                    {
                        newPaletteData = Cereal64.Common.Utils.ByteHelper.CombineIntoBytes(newPaletteData, new byte[(_image.ImageReference.WorkingPalette.Colors.Length * 2) - newPaletteData.Length]);
                    }
                }
                else
                    newData = TextureConversion.ImageToBinary(_image.Format, _image.PixelSize, bmp);

                if (newData == null || newData.Length == 0)
                {
                    MessageBox.Show("Error: Couldn't convert image file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] oldData = _image.ImageReference.Texture.RawData;
                byte[] oldPaletteData = null;
                if (_image.Format == Texture.ImageFormat.CI)
                    oldPaletteData = _image.ImageReference.BasePalettes[0].RawData;

                _image.ImageReference.Texture.RawData = newData;
                if (_image.Format == Texture.ImageFormat.CI)
                    _image.ImageReference.BasePalettes[0].RawData = newPaletteData;

                _image.ImageReference.UpdateImage();

                if (!_image.IsValidImage)
                {
                    _image.ImageReference.Texture.RawData = oldData;
                    _image.ImageReference.UpdateImage();

                    MessageBox.Show("Error: Couldn't set image file! File might be too large to load in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] oldMIO0RefData = null;

                N64DataElement el;

                if (_image.TextureEncoding == MK64Image.MK64ImageEncoding.MIO0)
                {
                    if (!RomProject.Instance.Files[0].HasElementAt(_image.TextureOffset, out el))
                    {
                        MessageBox.Show("Error: Couldn't set image file! Could not find MIO0 block.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MIO0Block block = (MIO0Block)el;

                    byte[] oldMIO0Data = block.DecodedData;
                    oldMIO0RefData = oldMIO0Data;

                    if (_specialMIO0Length == -1 && block.FileOffset == 0x132B50)
                    {
                        _specialMIO0Length = block.RawDataSize;
                    }

                    Array.Copy(newData, 0, oldMIO0Data, _image.TextureBlockOffset, newData.Length);

                    byte[] compressedNewMIO0 = MIO0.Encode(oldMIO0Data);

                    int sizeCompare = (_specialMIO0Length != -1 && block.FileOffset == 0x132B50 ? _specialMIO0Length : block.RawDataSize);

                    if (compressedNewMIO0.Length > sizeCompare)
                    {
                        _image.ImageReference.Texture.RawData = oldData;
                        if (_image.Format == Texture.ImageFormat.CI)
                            _image.ImageReference.BasePalettes[0].RawData = oldPaletteData;
                        _image.ImageReference.UpdateImage();

                        MessageBox.Show("Error: Couldn't set image file! File might be too large to load in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    block.RawData = compressedNewMIO0;
                }

                if (_image.Format == Texture.ImageFormat.CI && _image.PaletteEncoding[0] == MK64Image.MK64ImageEncoding.MIO0)
                {
                    if (!RomProject.Instance.Files[0].HasElementAt(_image.TextureOffset, out el))
                    {
                        MessageBox.Show("Error: Couldn't set image file! Could not find MIO0 block.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MIO0Block block = (MIO0Block)el;

                    byte[] oldMIO0Data = block.DecodedData;

                    Array.Copy(newPaletteData, 0, oldMIO0Data, _image.PaletteBlockOffset[0], newPaletteData.Length);

                    byte[] compressedNewMIO0 = MIO0.Encode(oldMIO0Data);

                    int sizeCompare = (_specialMIO0Length != -1 && block.FileOffset == 0x132B50 ? _specialMIO0Length : block.RawDataSize);

                    if (compressedNewMIO0.Length > sizeCompare)
                    {
                        _image.ImageReference.Texture.RawData = oldData;
                        if (_image.Format == Texture.ImageFormat.CI)
                            _image.ImageReference.BasePalettes[0].RawData = oldPaletteData;
                        _image.ImageReference.UpdateImage();

                        //Revert texture
                        if (_image.TextureEncoding == MK64Image.MK64ImageEncoding.MIO0)
                        {
                            N64DataElement element;
                            if (!RomProject.Instance.Files[0].HasElementExactlyAt(_image.TextureOffset, out element))
                                throw new Exception();
                            MIO0Block blockText = (MIO0Block)element;

                            blockText.RawData = oldMIO0RefData;
                        }

                        MessageBox.Show("Error: Couldn't set image file! File might be too large to load in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    block.RawData = compressedNewMIO0;
                }

                Image = _image; //Reset it
                ImageUpdated();
            }
        }

        private void ReplaceTexture()
        {
            //Attempt to load in a new texture and replace. Will not work if the texture size is larger than the one it's replacing
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
                if (bmp == null)
                {
                    MessageBox.Show("Error: Couldn't load image file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (bmp.Height != _image.Height ||
                    bmp.Width != _image.Width)
                {
                    MessageBox.Show("Error: New image must be same size!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] newData;
                byte[] newPaletteData = null;
                if (_image.Format == Texture.ImageFormat.CI)
                {
                    Palette palette = new Palette(-1, new byte[_image.ImageReference.WorkingPalette.Colors.Length * 2]);
                    newData = TextureConversion.ImageToBinary(_image.Format, _image.PixelSize, bmp, ref palette);
                    newPaletteData = palette.RawData;
                }
                else
                    newData = TextureConversion.ImageToBinary(_image.Format, _image.PixelSize, bmp);

                if(newData == null || newData.Length == 0)
                {
                    MessageBox.Show("Error: Couldn't convert image file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] oldData = _image.ImageReference.Texture.RawData;
                byte[] oldPaletteData = null;
                if(_image.Format == Texture.ImageFormat.CI)
                    oldPaletteData = _image.ImageReference.BasePalettes[0].RawData;

                _image.ImageReference.Texture.RawData = newData;
                if (_image.Format == Texture.ImageFormat.CI)
                    _image.ImageReference.BasePalettes[0].RawData = newPaletteData;

                _image.ImageReference.UpdateImage();

                if(!_image.IsValidImage)
                {
                    _image.ImageReference.Texture.RawData = oldData;
                    if (_image.Format == Texture.ImageFormat.CI)
                        _image.ImageReference.BasePalettes[0].RawData = oldPaletteData;
                    _image.ImageReference.UpdateImage();

                    MessageBox.Show("Error: Couldn't set image file! File might be too large to load in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Image = _image; //Reset it
                ImageUpdated();
            }
        }

        private void btnEditPalette_Click(object sender, EventArgs e)
        {
            //Bring up the palette editing form. Needs to have a preview screen. If it affects multiple images, think about
            // having a before/after preview for the changes
        }
    }
}
