using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using MK64Pitstop.Modules.Karts;
using MK64Pitstop.Data.Karts;
using Cereal64.Microcodes.F3DEX.DataElements;
using MK64Pitstop.Data;
using System.IO;

namespace ChompShop.Controls.KartControls
{
    public partial class KartImagesForm : ChompShopWindow
    {
        public bool _initializing;

        private bool _advancedSettingsOpen = false;

        public KartImagesForm(KartWrapper kart)
            : base (kart)
        {
            InitializeComponent();

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            ResetTitleText();

            ResetButtonStates();

            PopulateKartImagesListBox();
            PopulateNewImagesListBox();

            UpdateSplitContainer();

            _initializing = false;
        }

        private void PopulateKartImagesListBox()
        {
            object lastSelectedItem = lbKartImages.SelectedItem;
            int lastSelectedIndex = lbKartImages.SelectedIndex;

            lbKartImages.Items.Clear();

            if (Kart == null || Kart.Kart == null)
                return;

            foreach (KartImage img in Kart.Kart.KartImages.Images.Values)
            {
                lbKartImages.Items.Add(img);
            }

            if (lastSelectedItem != null && lbKartImages.Items.Contains(lastSelectedItem))
                lbKartImages.SelectedItem = lastSelectedItem;
            else if (lbKartImages.Items.Count > 0)
            {
                if (lastSelectedIndex != -1)
                    lbKartImages.SelectedIndex = Math.Min(lastSelectedIndex, lbKartImages.Items.Count - 1);
                else
                    lbKartImages.SelectedIndex = 0;
            }
            else
            {
                lbKartImages.SelectedIndex = -1;
            }

        }

        private void PopulateNewImagesListBox()
        {
            object lastSelectedItem = lbNewImages.SelectedItem;
            int lastSelectedIndex = lbNewImages.SelectedIndex;

            lbNewImages.Items.Clear();

            if (Kart == null || Kart.Kart == null)
                return;

            foreach (BitmapWrapper img in Kart.NewImages)
            {
                lbNewImages.Items.Add(img);
            }

            if (lastSelectedItem != null && lbNewImages.Items.Contains(lastSelectedItem))
                lbNewImages.SelectedItem = lastSelectedItem;
            else if (lbNewImages.Items.Count > 0)
            {
                if (lastSelectedIndex != -1)
                    lbNewImages.SelectedIndex = Math.Min(lastSelectedIndex, lbNewImages.Items.Count - 1);
                else
                    lbNewImages.SelectedIndex = 0;
            }
            else
            {
                lbNewImages.SelectedIndex = -1;
            }

        }

        private void UpdateSplitContainer()
        {
            if(_advancedSettingsOpen)
            {
                this.splitContainer.Panel2Collapsed = false;
                this.Width = 723;
                btnAdvanced.Image = ChompShop.Properties.Resources.collapse_left_2x;
                toolTip.SetToolTip(btnAdvanced, "Collapse more options");
            }
            else
            {
                this.splitContainer.Panel2Collapsed = true;
                this.Width = 408;
                btnAdvanced.Image = ChompShop.Properties.Resources.expand_left_2x;
                toolTip.SetToolTip(btnAdvanced, "Expand more options");
            }
        }

        private KartImage SelectedKartImage { get { return (KartImage)lbKartImages.SelectedItem; } }

        private List<KartImage> SelectedKartImages 
        { 
            get 
            {
                List<KartImage> images = new List<KartImage>();
                foreach(KartImage image in lbKartImages.SelectedItems)
                    images.Add(image);
                return images; 
            } 
        }

        private BitmapWrapper SelectedNewImage { get { return (BitmapWrapper)lbNewImages.SelectedItem; } }

        private List<BitmapWrapper> SelectedNewImages
        {
            get
            {
                List<BitmapWrapper> images = new List<BitmapWrapper>();
                foreach (BitmapWrapper image in lbNewImages.SelectedItems)
                    images.Add(image);
                return images;
            }
        }
        private void lbKartImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetKartImagesButtonState();
        }

        private void lbNewImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetNewImagesButtonState();
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            _advancedSettingsOpen = !_advancedSettingsOpen;
            UpdateSplitContainer();
        }

        private void btnRemoveKartImage_Click(object sender, EventArgs e)
        {
            Kart.RemoveKartImages(SelectedKartImages);

            PopulateKartImagesListBox();

            //Need to update or remove by hand??
        }

        private void btnEditKartImage_Click(object sender, EventArgs e)
        {
            //Bring up the kart image view to allow renaming/palette tweaking. Will need work in the future when
            // we bring in the animated stuff
        }

        private void btnAddNewImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<BitmapWrapper> wrappers = new List<BitmapWrapper>();
                foreach (string file in openFileDialog.FileNames)
                {
                    //Load in the file
                    wrappers.Add(new BitmapWrapper(Path.GetFileNameWithoutExtension(file), new Bitmap(Bitmap.FromFile(file))));
                }

                Kart.NewImages.AddRange(wrappers.ToArray());

                PopulateNewImagesListBox();
                SetPaletteButtonState();
                //Set off event for this??
            }
        }

        private void btnRemoveNewImage_Click(object sender, EventArgs e)
        {
            if (SelectedNewImages.Count > 0)
            {
                foreach(BitmapWrapper image in SelectedNewImages)
                    Kart.NewImages.Remove(image);

                PopulateNewImagesListBox();
                SetPaletteButtonState();
            }
        }

        private void btnConvertNewToKart_Click(object sender, EventArgs e)
        {
            List<BitmapWrapper> bmps = new List<BitmapWrapper>();
            foreach (object bmpObj in lbNewImages.SelectedItems)
                bmps.Add((BitmapWrapper)bmpObj);

            if(bmps.Count > 0)
                CreateKartImages(bmps);
        }

        private void btnBasePaletteManip_Click(object sender, EventArgs e)
        {
            if (Kart.Kart.KartImages.ImagePalette != null)
            {
                if (MessageBox.Show("Clear main Kart palette? All Kart Images will be removed & converted to bitmaps. Proceed?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    ClearCurrentKartPalette();
                    SetPaletteButtonState();
                }
            }
            else
            {
                if (MessageBox.Show("New Kart palette will be generated from all images, and all will be converted to Kart Images. Proceed?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    CreateNewKartPalette();
                    SetPaletteButtonState();
                }
            }
        }

        private void CreateAllKartImages()
        {
            List<BitmapWrapper> bmps = new List<BitmapWrapper>();

            foreach (object bmpObj in lbNewImages.Items)
                bmps.Add((BitmapWrapper)bmpObj);

            CreateKartImages(bmps);
        }

        private void CreateKartImages(List<BitmapWrapper> bmps)
        {
            //Take each image, conform to the palette, and add as a new image
            List<KartImage> karts = new List<KartImage>();

            foreach (BitmapWrapper bmp in bmps)
            {
                KartImage img = ConvertToKartImage(bmp.Image, bmp.Name);
                if (img != null)
                    karts.Add(img);
            }

            if (karts.Count > 0)
            {
                Kart.AddKartImages(karts);

                foreach (BitmapWrapper bmp in bmps)
                    Kart.NewImages.Remove(bmp);

                PopulateKartImagesListBox();
                PopulateNewImagesListBox();
            }
            else
            {
                MessageBox.Show("Error! Couldn't convert new images to Kart Images!");
            }
        }

        private KartImage ConvertToKartImage(Bitmap bmp, string imageName)
        {
            if (bmp != null && Kart != null && Kart.Kart != null && Kart.Kart.KartImages.ImagePalette != null)
            {
                //Create the new KartImage here
                int tempPalOffset = 0;
                byte[] imgData = TextureConversion.CI8ToBinary(bmp, Kart.Kart.KartImages.ImagePalette, ref tempPalOffset);
                Texture texture = new Texture(0, imgData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64);
                //KartImage newImage = new KartImage(new List<MK64Image>(new MK64Image(texture, Kart.Kart.KartImages.ImagePalette)));
                //return newImage;
                throw new NotImplementedException();
            }

            return null;
        }

        private void ClearCurrentKartPalette()
        {
            //We need to basically cull all the kart palettes, add them to the new images and then clear the image palette
            Kart.ClearMainPalette();

            PopulateKartImagesListBox();
            PopulateNewImagesListBox();
        }

        private void CreateNewKartPalette()
        {
            //MAKE SURE TO INCLUDE A METHOD TO USE EXTRA PALETTES, AND ONE TO NOT USE THEM!

            PaletteMedianCutAnalyzer paletteMaker = new PaletteMedianCutAnalyzer();
            foreach (BitmapWrapper wrapper in lbNewImages.Items)
            {
                for (int i = 0; i < wrapper.Image.Width; i++)
                {
                    for (int j = 0; j < wrapper.Image.Height; j++)
                    {
                        paletteMaker.AddColor(wrapper.Image.GetPixel(i, j));
                    }
                }
            }

            Color[] colors = paletteMaker.GetPalette(0xC0);
            byte[] paletteData = TextureConversion.PaletteToBinary(colors);
            Palette palette = new Palette(-1, paletteData);
            Kart.SetMainPalette(palette);

            //Convert all existing images
            CreateAllKartImages();
        }

        private void SetPaletteButtonState()
        {
            if (Kart.Kart.KartImages.ImagePalette == null)
            {
                //No palette yet
                btnBasePaletteManip.Image = ChompShop.Properties.Resources.circle_check_3x;
                btnBasePaletteManip.Text = "Create New Base Palette";
                toolTip.SetToolTip(btnBasePaletteManip, "Generate a new Base Palette from the new images");
            }
            else
            {
                //Palette exists
                btnBasePaletteManip.Image = ChompShop.Properties.Resources.circle_x_3x;
                btnBasePaletteManip.Text = "Clear Base Palette";
                toolTip.SetToolTip(btnBasePaletteManip, "Clear the Kart's Base Palette");
            }

            btnBasePaletteManip.Enabled = Kart.NewImages.Count > 0;
        }

        private void ResetButtonStates()
        {
            SetPaletteButtonState();
            SetNewImagesButtonState();
            SetKartImagesButtonState();
        }

        private void SetNewImagesButtonState()
        {
            bool validImage = (SelectedNewImage != null);
            if (validImage)
            {
                imagePreviewControl.Image = SelectedNewImage.Image;
                imagePreviewControl.ImageName = SelectedNewImage.Name;
            }
            else
            {
                imagePreviewControl.Image = null;
            }

            bool hasBasePalette = Kart.Kart.KartImages.ImagePalette != null;

            btnRemoveNewImage.Enabled = validImage;
            btnConvertNewToKart.Enabled = validImage && hasBasePalette;
        }

        private void SetKartImagesButtonState()
        {
            bool validKart = (SelectedKartImage != null);
            if (validKart)
            {
                imagePreviewControl.Image = ((MK64Image)lbKartImages.SelectedItem).Image;
                imagePreviewControl.ImageName = ((MK64Image)lbKartImages.SelectedItem).ImageName;
            }
            else
            {
                imagePreviewControl.Image = null;
            }

            btnRemoveKartImage.Enabled = validKart;
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartImages; } }

        protected override string TitleText { get { return "Kart Images - {0}"; } }
    }
}
