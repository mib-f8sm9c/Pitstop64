using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pitstop64.Data;
using Pitstop64.Services.Hub;

namespace Pitstop64.Modules.Textures
{
    //Note: A LOT of this was lifted from TexturesControl. Maybe we should reuse code rather than copy/paste?
    public partial class SelectMK64ImageForm : Form
    {
        public SelectMK64ImageForm(bool multiSelect = false)
        {
            InitializeComponent();

            if(multiSelect)
                lbImages.SelectionMode = SelectionMode.MultiExtended;
            else
                lbImages.SelectionMode = SelectionMode.One;

            SetImages(MarioKart64ElementHub.Instance.TextureHub.Images);
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

        public List<MK64Image> SelectedImages
        {
            get
            {
                return lbImages.SelectedItems.Cast<MK64Image>().ToList();
            }
        }

        public MK64Image SelectedImage
        {
            get
            {
                return (MK64Image)lbImages.SelectedItem;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
        }

        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedImage();
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
        
        private void DisplaySelectedImage()
        {
            SetPreviewImage(SelectedImage);
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
        }

    }
}
