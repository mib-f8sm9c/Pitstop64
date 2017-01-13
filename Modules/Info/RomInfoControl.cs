using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;
using Cereal64.Common.DataElements.Encoding;
using System.IO;

namespace Pitstop64.Modules.Info
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
            cb1MIO0Data.Items.Clear();

            if (RomProject.Instance.Files.Count > 0)
            {
                btnResizeRom.Enabled = true;
                txtRomSize.Enabled = true;
                txtRomSize.Text = ((int)Math.Round(RomProject.Instance.Files[0].FileLength / 1048576.0)).ToString();

                //Update the MIO0 stuff
                foreach (N64DataElement element in RomProject.Instance.Files[0].Elements)
                {
                    if (element is MIO0Block)
                    {
                        MIO0Block block = (MIO0Block)element;
                        if (block.Elements.Count > 1)
                        {
                            cb1MIO0Data.Items.Add(block);
                        }
                    }
                }

                bool mioEnabled = cb1MIO0Data.Items.Count > 0;

                btnExportMIO0.Enabled = mioEnabled;
                cb1MIO0Data.Enabled = mioEnabled;
                if (mioEnabled)
                    cb1MIO0Data.SelectedIndex = 0;
            }
            else
            {
                btnResizeRom.Enabled = false;
                txtRomSize.Enabled = false;
                txtRomSize.Text = string.Empty;
                btnExportMIO0.Enabled = false;
                cb1MIO0Data.Enabled = false;
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
