using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using Pitstop64.Data.Karts;
using System.IO;

namespace ChompShop.Controls.KartControls
{
    public partial class KartImageSelectForm : ChompShopWindow
    {
        public KartImageSelectForm(KartWrapper kart)
            : base(kart)
        {
            InitializeComponent();

            UpdateImages();
        }

        private void UpdateImages()
        {
            lbKartImages.Items.Clear();

            if(Kart == null || Kart.Kart == null)
                return;

            foreach (KartImage image in Kart.Kart.KartImages.Images.Values)
            {
                if(string.IsNullOrWhiteSpace(FilterText))
                    lbKartImages.Items.Add(image);
                else if (image.Name.ToLower().Contains(FilterText))
                    lbKartImages.Items.Add(image);
            }

            UpdateSelectedCount();
        }

        public List<KartImage> SelectedImages
        {
            get
            {
                List<KartImage> images = new List<KartImage>();
                foreach (object image in lbKartImages.SelectedItems)
                {
                    images.Add((KartImage)image);
                }
                return images;
            }
        }

        public List<KartImage> AllImages
        {
            get
            {
                List<KartImage> images = new List<KartImage>();
                foreach (object image in lbKartImages.Items)
                {
                    images.Add((KartImage)image);
                }
                return images;
            }
        }

        private void UpdateSelectedCount()
        {
            lblSelectedCountText.Text = lbKartImages.SelectedItems.Count.ToString();
        }

        private void lbKartImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedCount();

            if (lbKartImages.SelectedIndex == -1)
                imagePreviewControl.Image = null;
            else
            {
                imagePreviewControl.Image = ((KartImage)lbKartImages.Items[lbKartImages.SelectedIndex]).Images[0].Image;
                imagePreviewControl.ImageName = ((KartImage)lbKartImages.Items[lbKartImages.SelectedIndex]).Images[0].ImageName;
            }
        }

        public string FilterText
        {
            get
            {
                return txtSearchImages.Text.Trim().ToLower();
            }
        }

        private void txtSearchImages_TextChanged(object sender, EventArgs e)
        {
            //Need to preserve selected items?
            UpdateImages();
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (KartImage image in AllImages)
                {
                    image.Images[0].Image.Save(Path.Combine(folderBrowserDialog.SelectedPath, image.Name + ".png"));
                }
            }
        }
    }
}
