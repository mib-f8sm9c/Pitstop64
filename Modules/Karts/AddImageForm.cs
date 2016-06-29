using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Services;
using Cereal64.Common.Utils.Encoding;
using MK64Pitstop.Data;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Rom;
using System.IO;

namespace MK64Pitstop.Modules.Karts
{
    public partial class AddImageForm : Form
    {
        private KartInfo _kart;

        public AddImageForm(KartInfo kart)
        {
            InitializeComponent();

            _kart = kart;

            PopulateImages();

            UpdateEnabledButtons();
        }

        public void PopulateImages()
        {
            lbAdded.Items.Clear();

            foreach (string key in _kart.KartImages.Images.Keys)
            {
                lbAdded.Items.Add(_kart.KartImages.Images[key]);
            }
        }

        public void UpdateEnabledButtons()
        {
            if (_kart.KartImages.ImagePalette != null)
            {
                btnAddNewImage.Enabled = true;
                btnReset.Enabled = false;
            }
            else
            {
                btnAddNewImage.Enabled = false;
                btnReset.Enabled = true;
            }

            if (SelectedImage != null && SelectedImage.Image != null)
            {
                btnExport.Enabled = true;
                btnOK.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
                btnOK.Enabled = false;
            }
        }

        public KartImage SelectedImage
        {
            get
            {
                if (lbAdded.SelectedIndex != -1)
                    return (KartImage)lbAdded.SelectedItem;
                
                return null;
            }
        }

        private void lbAdded_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedImage != null)
                pbImage.Image = SelectedImage.Image;

            UpdateEnabledButtons();
        }

        private void CreateNewKartImage(string fileName, bool displayErrors = false)
        {
            string imageName = Path.GetFileNameWithoutExtension(fileName);

            if (_kart.KartImages.Images.ContainsKey(imageName))
            {
                MessageBox.Show("An image by that name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image img = Bitmap.FromFile(fileName);
            if (img.Width != 64 || img.Height != 64)
            {
                MessageBox.Show("Image must be 64x64!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (img != null)
            {
                //Create the new KartImage here
                byte[] imgData = TextureConversion.CI8ToBinary(new Bitmap(img), _kart.KartImages.ImagePalette);
                Texture texture = new Texture(-1, imgData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, _kart.KartImages.ImagePalette);
                ImageMIO0Block block = new ImageMIO0Block(MarioKart64ElementHub.Instance.NewElementOffset, imgData);
                block.ImageName = imageName;
                block.DecodedN64DataElement = texture;
                MarioKart64ElementHub.Instance.AdvanceNewElementOffset(block);
                RomProject.Instance.Files[0].AddElement(block);
                KartImage newImage = new KartImage(block);
                _kart.KartImages.Images.Add(imageName, newImage);

                lbAdded.Items.Add(newImage);
            }
        }

        private void btnAddNewImage_Click(object sender, EventArgs e)
        {
            //Import a new image
            if (openImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreateNewKartImage(openImageDialog.FileName, true);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //Reset _kart.KartImages to what you need. Only for new karts
            if (_kart.KartImages.ImagePalette != null)
            {
                UpdateEnabledButtons();
                return;
            }

            if (openImagesDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //From here, you need to make the palette, then one by one add in all the images
                PaletteMedianCutAnalyzer paletteMaker = new PaletteMedianCutAnalyzer();
                foreach (string file in openImagesDialog.FileNames)
                {
                    Bitmap img = new Bitmap(Bitmap.FromFile(file));
                    for (int i = 0; i < img.Width; i++)
                    {
                        for (int j = 0; j < img.Height; j++)
                        {
                            paletteMaker.AddColor(img.GetPixel(i, j));
                        }
                    }
                }
                Color[] colors = paletteMaker.GetPalette(0xC0);
                byte[] paletteData = TextureConversion.PaletteToBinary(colors);
                Palette palette = new Palette(MarioKart64ElementHub.Instance.NewElementOffset, paletteData);
                MarioKart64ElementHub.Instance.AdvanceNewElementOffset(palette);
                RomProject.Instance.Files[0].AddElement(palette);
                _kart.KartImages.SetPalette(palette);
                foreach (string file in openImagesDialog.FileNames)
                {
                    CreateNewKartImage(file);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedImage.Image.Save(saveImageDialog.FileName);
            }
        }
    }
}
