using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using Pitstop64.Modules.Karts;
using Pitstop64.Data.Karts;
using Cereal64.Microcodes.F3DEX.DataElements;
using Pitstop64.Data;
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
                //byte[] imgData = TextureConversion.CI8ToBinary(bmp, Kart.Kart.KartImages.ImagePalette);
                //Texture texture = new Texture(-1, imgData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, Kart.Kart.KartImages.ImagePalette);
               // ImageMIO0Block block = new ImageMIO0Block(-1, imgData);
               // block.ImageName = imageName;
               // block.DecodedN64DataElement = texture;
                //byte[] blankPaletteData = new byte[0x40];
                //Palette blankPalette = new Palette(-1, blankPaletteData);
                //KartImage newImage = new KartImage(block, blankPalette);
                //return newImage;
                int tempPalOffset = 0;
                byte[] imgData = TextureConversion.CI8ToBinary(bmp, Kart.Kart.KartImages.ImagePalette, ref tempPalOffset);
                Texture texture = new Texture(-1, imgData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64);
                List<Palette> palettes = new List<Palette>();
                palettes.Add(Kart.Kart.KartImages.ImagePalette);
                byte[] blankPaletteData = new byte[0x40];
                palettes.Add(new Palette(-1, blankPaletteData));
                F3DEXImage image = new F3DEXImage(texture, palettes);
                MK64Image mkImg = new MK64Image(image, imageName, true);
                KartImage newImage = new KartImage(new List<MK64Image>() { mkImg });
                return newImage;
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

        private void SetBetaButtonState()
        {
            btnMassImport.Enabled = (lbNewImages.Items.Count == 321 && Kart.Kart.KartImages.ImagePalette == null);
        }

        private void ResetButtonStates()
        {
            SetPaletteButtonState();
            SetNewImagesButtonState();
            SetKartImagesButtonState();
            SetBetaButtonState();
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
                imagePreviewControl.Image = ((KartImage)lbKartImages.SelectedItem).Images[0].Image;
                imagePreviewControl.ImageName = ((KartImage)lbKartImages.SelectedItem).Images[0].ImageName;
            }
            else
            {
                imagePreviewControl.Image = null;
            }

            btnRemoveKartImage.Enabled = validKart;
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartImages; } }

        protected override string TitleText { get { return "Kart Images - {0}"; } }

        private void btnMassImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will follow MRKane's blender output style. Are you ready?", "Heads Up", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //Here, we'll load in all the images, and then automatically load in the animations.
                BetaMassImport();

                PopulateKartImagesListBox();
                PopulateNewImagesListBox();
                ResetButtonStates();
            }
        }

        private void BetaMassImport()
        {
            //MAKE SURE TO INCLUDE A METHOD TO USE EXTRA PALETTES, AND ONE TO NOT USE THEM!

            //First, make the new palette
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
            List<string> orderedKartNames = GetNewImageNames();
            CreateAllKartImages();

            BetaSetUpAnimations(orderedKartNames);
        }

        private void BetaSetUpAnimations(List<string> orderedKartNames)
        {
            //Clear animations
            while (Kart.Kart.KartAnimations.Count > 0)
                Kart.RemoveAnimation(Kart.Kart.KartAnimations[0]);

            //Okay, here's where we set up the new animations & images in them.
            //Image order:
            //1  1-21 : -25 turn
            //2  22-42 : -19 turn
            //3  43-63 : -12 turn
            //4  64-84 : -6 turn
            //5  85-105 : 0 turn
            //6  106-126 : 6 turn
            //7  127-147 : 12 turn
            //8  148-168 : 19 turn
            //9  169-189 : 25 turn
            //10 190-209 : -25 spin
            //11 210-229 : -12 spin
            //12 230-249 : 0 spin
            //13 250-269 : 12 spin
            //14 270-289 : 25 spin
            //15 290-321 : crash


            Kart.AddNewAnimation("Turn Down 25");
            for(int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[0], i, Kart.Kart.KartImages.Images[orderedKartNames[i]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[0], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown25);

            Kart.AddNewAnimation("Turn Down 19");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[1], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 21]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[1], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown19);

            Kart.AddNewAnimation("Turn Down 12");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[2], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 42]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[2], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12);

            Kart.AddNewAnimation("Turn Down 6");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[3], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 63]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[3], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown6);

            Kart.AddNewAnimation("Turn 0");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[4], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 84]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[4], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurn0);

            Kart.AddNewAnimation("Turn Up 6");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[5], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 105]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[5], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp6);

            Kart.AddNewAnimation("Turn Up 12");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[6], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 126]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[6], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12);

            Kart.AddNewAnimation("Turn Up 19");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[7], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 147]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[7], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp19);

            Kart.AddNewAnimation("Turn Up 25");
            for (int i = 0; i < 21; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[8], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 168]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[8], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp25);


            Kart.AddNewAnimation("Spin Down 25");
            for (int i = 0; i < 20; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[9], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 189]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[9],
                (int)(KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25 | KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19));

            Kart.AddNewAnimation("Spin Down 12");
            for (int i = 0; i < 20; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[10], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 209]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[10], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12);

            Kart.AddNewAnimation("Spin 0");
            for (int i = 0; i < 20; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[11], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 229]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[11],
                (int)(KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6 | KartAnimationSeries.KartAnimationTypeFlag.FullSpin0 | KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6));

            Kart.AddNewAnimation("Spin Up 12");
            for (int i = 0; i < 20; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[12], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 249]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[12], (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12);

            Kart.AddNewAnimation("Spin Up 25");
            for (int i = 0; i < 20; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[13], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 269]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[13],
                (int)(KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25 | KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19));


            Kart.AddNewAnimation("Crash");
            for (int i = 0; i < 32; i++)
                Kart.AddImagetoAnimation(Kart.Kart.KartAnimations[14], i, Kart.Kart.KartImages.Images[orderedKartNames[i + 289]]);
            Kart.SetAnimationType(Kart.Kart.KartAnimations[14], (int)KartAnimationSeries.KartAnimationTypeFlag.Crash);

            //What else to do here??
           

        }

        private List<string> GetNewImageNames()
        {
            List<string> imageNames = new List<string>(lbNewImages.Items.Count);

            foreach (object bmpObj in lbNewImages.Items)
                imageNames.Add(((BitmapWrapper)bmpObj).Name);
            imageNames.Sort();

            return imageNames;
        }
    }
}
