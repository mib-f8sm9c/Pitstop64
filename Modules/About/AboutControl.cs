using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;
using MK64Pitstop.Services.Hub;
using Cereal64.Common.DataElements;
using Cereal64.Common.DataElements.Encoding;
using System.IO;

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
                txtRomSize.Enabled = true;
                txtRomSize.Text = ((int)Math.Round(RomProject.Instance.Files[0].FileLength / 1048576.0)).ToString();
            }
            else
            {
                btnResizeRom.Enabled = false;
                txtRomSize.Enabled = false;
                txtRomSize.Text = string.Empty;
            }

            //Update the MIO0 stuff
            cb1MIO0Data.Items.Clear();
            if (RomProject.Instance != null && RomProject.Instance.Files.Count > 0)
            {
                foreach (N64DataElement element in RomProject.Instance.Files[0].Elements)
                {
                    if (element is MIO0Block)
                    {
                        if (element.RawDataSize > 0x1000)
                        {
                            cb1MIO0Data.Items.Add(element);
                        }
                    }
                }
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

        private void btnExportMIO0_Click(object sender, EventArgs e)
        {
            if (cb1MIO0Data.SelectedItem != null && cb1MIO0Data.SelectedItem is MIO0Block)
            {
                File.WriteAllBytes(((MIO0Block)cb1MIO0Data.SelectedItem).ToString() + ".bin", ((MIO0Block)cb1MIO0Data.SelectedItem).DecodedData);
            }
        }
    }
}
