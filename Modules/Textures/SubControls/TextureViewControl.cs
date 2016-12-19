using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Common.Rom;
using Cereal64.Common.Utils.Encoding;

namespace MK64Pitstop.Modules.Textures.SubControls
{
    public partial class TextureViewControl : UserControl, ITextureViewControl
    {
        public TextureViewControl()
        {
            InitializeComponent();
        }

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
                    btnEditPalette.Visible = false;

                    btnReplaceWith.Enabled = (_image.Format != Texture.ImageFormat.CI &&
                        _image.TextureEncoding != MK64Image.MK64ImageEncoding.TKMK00);
                }
                else
                {
                    lblName.Text = _image.ImageName;
                    lblSize.Text = string.Format("{0}x{1}", _image.Width, _image.Height);
                    btnEditPalette.Visible = false;
                   // btnEditPalette.Visible = _image.Format == Texture.ImageFormat.CI;
                }
            }
        }
        private MK64Image _image;

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
            if (_image.Format == Texture.ImageFormat.CI)
                ReplaceCI();
            else
                ReplaceTexture();
        }

        private void ReplaceCI()
        {

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

                if (bmp.Height != _image.Width ||
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

                byte[] newData = TextureConversion.ImageToBinary(_image.Format, _image.PixelSize, bmp);

                if(newData == null || newData.Length == 0)
                {
                    MessageBox.Show("Error: Couldn't convert image file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] oldData = _image.ImageReference.Texture.RawData;

                _image.ImageReference.Texture.RawData = newData;

                _image.ImageReference.UpdateImage();

                if(!_image.IsValidImage)
                {
                    _image.ImageReference.Texture.RawData = oldData;
                    _image.ImageReference.UpdateImage();

                    MessageBox.Show("Error: Couldn't set image file! File might be too large to load in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_image.TextureEncoding == MK64Image.MK64ImageEncoding.MIO0)
                {
                    MIO0Block block = (MIO0Block)RomProject.Instance.Files[0].GetElementAt(_image.TextureOffset);

                    byte[] oldMIO0Data = block.DecodedData;

                    Array.Copy(newData, 0, oldMIO0Data, _image.TextureBlockOffset, newData.Length);

                    byte[] compressedNewMIO0 = MIO0.Encode(oldMIO0Data);

                    if (compressedNewMIO0.Length > block.RawDataSize)
                    {
                        _image.ImageReference.Texture.RawData = oldData;
                        _image.ImageReference.UpdateImage();

                        MessageBox.Show("Error: Couldn't set image file! File might be too large to load in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    block.RawData = compressedNewMIO0;
                }

                Image = _image; //Reset it
            }
        }

        private void btnEditPalette_Click(object sender, EventArgs e)
        {
            //Bring up the palette editing form. Needs to have a preview screen. If it affects multiple images, think about
            // having a before/after preview for the changes
        }
    }
}
