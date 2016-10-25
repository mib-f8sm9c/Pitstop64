using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data.Karts;
using System.Drawing.Imaging;

namespace MK64Pitstop.Modules.Karts
{
    public partial class ImagePreviewControl : UserControl
    {
        public ImagePreviewControl()
        {
            InitializeComponent();
            pbOverlay.Parent = pbPreview;
        }

        private void btnBGColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnBGColor.BackColor = colorDialog.Color;
                pnlPreview.BackColor = colorDialog.Color;
            }
        }

        [Browsable(true)]
        [Category("Button style")]
        public bool ExportButtonVisible
        {
            get
            {
                return btnExport.Visible;
            }
            set
            {
                btnExport.Visible = value;
            }
        }

        public Image Image
        {
            get
            {
                return pbPreview.Image;
            }
            set
            {
                pbPreview.Image = value;
                btnExport.Enabled = (value != null);
            }
        }

        public Image OverlayImage
        {
            get
            {
                return pbOverlay.Image;
            }
            set
            {
                if (value == null)
                {
                    pbOverlay.Image = null;
                    return;
                }

                //copied from https://raviranjankr.wordpress.com/2011/05/25/change-opacity-of-image-in-c/
                Bitmap bmp = new Bitmap(value.Width,value.Height); // Determining Width and Height of Source Image
                Graphics graphics = Graphics.FromImage(bmp);
                ColorMatrix colormatrix = new ColorMatrix();
                colormatrix.Matrix33 = 0.5f;
                ImageAttributes imgAttribute = new ImageAttributes();
                imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                graphics.DrawImage(value, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, value.Width, value.Height, GraphicsUnit.Pixel, imgAttribute);
                graphics.Dispose();   // Releasing all resource used by graphics
                
                pbOverlay.Image = bmp;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //export
                pbPreview.Image.Save(saveFileDialog.FileName);
            }
        }
    }
}
