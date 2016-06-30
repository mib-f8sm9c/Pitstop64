using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;

namespace MK64Pitstop.Modules.About
{
    public partial class AboutControl : UserControl
    {
        private const string ABOUT_TEXT =
@"Mario Kart 64 Pitstop created by mib_f8sm9c
Major thanks to QueueRAM and Shygoo
More thanks to be included";

        public AboutControl()
        {
            InitializeComponent();

            txtAbout.Text = ABOUT_TEXT;

            UpdateControl();
        }

        public void UpdateControl()
        {
            if (RomProject.Instance.Files.Count > 0)
            {
                btnResizeRom.Enabled = true;
                txt.Enabled = true;
            }
            else
            {
                btnResizeRom.Enabled = false;
                txt.Enabled = false;
            }
        }

        private void btnResizeRom_Click(object sender, EventArgs e)
        {
            //SUPER debug, DONT ACTUALLY USE IN REALITY
            int mbSize = int.Parse(txt.Text);
            int byteCount = mbSize << 20;
            RomProject.Instance.Files[0].ExpandFileTo(byteCount, 0xFF);
        }
    }
}
