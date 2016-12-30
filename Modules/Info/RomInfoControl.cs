using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;

namespace MK64Pitstop.Modules.Info
{
    public partial class RomInfoControl : UserControl
    {

        public RomInfoControl()
        {
            InitializeComponent();

            UpdateControl();
        }

        public void UpdateControl()
        {
            if (RomProject.Instance.Files.Count > 0)
            {
                btnResizeRom.Enabled = true;
                txtRomSize.Enabled = true;
                txtRomSize.Text = ((int)Math.Round(RomProject.Instance.Files[0].FileLength / 1048576.0)).ToString();
            }
            else
            {
                btnResizeRom.Enabled = false;
                txtRomSize.Enabled = false;
                txtRomSize.Text = string.Empty;
            }
        }

        private void btnResizeRom_Click(object sender, EventArgs e)
        {
            //SUPER debug, DONT ACTUALLY USE IN REALITY
            int mbSize = int.Parse(txtRomSize.Text);
            int byteCount = mbSize << 20;
            RomProject.Instance.Files[0].ExpandFileTo(byteCount, 0xFF);
            MessageBox.Show("File expanded to " + byteCount + " bytes!");
        }
    }
}
