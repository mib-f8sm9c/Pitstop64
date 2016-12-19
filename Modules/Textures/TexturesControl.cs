using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data;
using MK64Pitstop.Services.Hub;
using MK64Pitstop.Modules.Textures.SubControls;

namespace MK64Pitstop.Modules.Textures
{
    public partial class TexturesControl : UserControl
    {
        public TexturesControl()
        {
            InitializeComponent();

            _controls = new Dictionary<ActiveControlTypes, ITextureViewControl>();
            cbImageType.SelectedIndex = 0;
        }

        private ITextureViewControl ActiveControl;

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

            lblImageCount.Text = "Images: " + lbImages.Items.Count;
        }

        //private void btnExport_Click(object sender, EventArgs e)
        //{
        //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        pbImage.Image.Save(saveFileDialog.FileName);
        //    }
        //}

        //private void btnImport_Click(object sender, EventArgs e)
        //{
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        Bitmap bmp = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);

        //        TKMK00Block tkmk = (TKMK00Block)cbImage.Items[cbImage.SelectedIndex];

        //        if (!tkmk.SetImage(bmp))
        //        {
        //            MessageBox.Show("Overwrite failed (Image too big?)");
        //        }
        //        else
        //        {
        //            pbImage.Image = tkmk.Image;
        //        }
        //    }
        //}

        private void btnAddImage_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {

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
            if (lbImages.SelectedItem != null)
            {
                //Quick display here!
                //pictureBox1.Image = ((MK64Image)lbImages.SelectedItem).Image;
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
                    //case ActiveControlTypes.CI:
                    //    control = new CITextureViewControl();
                    //    break;
                    default:
                        control = new TextureViewControl();
                        break;
                }

                pnlTools.Controls.Add(control.GetAsControl());
                _controls.Add(_activeControlType, control);
                ActiveControl = control;
            }

            ActiveControl.Activate();
        }

        private void DeactivateControl()
        {
            if (_controls.ContainsKey(_activeControlType) && _controls[_activeControlType] != null)
                _controls[_activeControlType].Deactivate();

            ActiveControl = null;
        }
    }
}
