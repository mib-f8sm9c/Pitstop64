using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data;

namespace MK64Pitstop.Modules.Textures.SubControls
{
    public partial class TKMKViewControl : UserControl, ITextureViewControl
    {
        public TKMKViewControl()
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
                }
                else
                {
                    lblName.Text = _image.ImageName;
                    lblSize.Text = string.Format("{0}x{1}", _image.Width, _image.Height);
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

                if (!_image.TKMKReference.SetImage(bmp))
                {
                    MessageBox.Show("Error: Couldn't set image file! File might be too large to load in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Image = _image; //Reset it
            }
        }
    }
}
