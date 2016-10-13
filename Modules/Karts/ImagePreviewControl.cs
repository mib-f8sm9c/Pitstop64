using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data.Karts;

namespace MK64Pitstop.Modules.Karts
{
    public partial class ImagePreviewControl : UserControl
    {
        public ImagePreviewControl()
        {
            InitializeComponent();
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
