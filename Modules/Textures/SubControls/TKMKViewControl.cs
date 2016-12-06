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
                }
                else
                {
                    lblName.Text = _image.ImageName;
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
        }
    }
}
