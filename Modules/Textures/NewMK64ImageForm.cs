using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace Pitstop64.Modules.Textures
{
    public partial class NewMK64ImageForm : Form
    {
        public List<string> FileNames { get; private set; }

        public NewMK64ImageForm()
        {
            InitializeComponent();

            UpdateValidPixelSizes();
        }

        public Texture.ImageFormat Format
        {
            get
            {
                if (radCI.Checked)
                    return Texture.ImageFormat.CI;
                else if (radIA.Checked)
                    return Texture.ImageFormat.IA;
                else if (radI.Checked)
                    return Texture.ImageFormat.I;
                else //if (radRGBA.Checked)
                    return Texture.ImageFormat.RGBA;
            }
        }

        public Texture.PixelInfo PixelSize
        {
            get
            {
                if (radHalfByte.Checked)
                    return Texture.PixelInfo.Size_4b;
                else if (radTwoByte.Checked)
                    return Texture.PixelInfo.Size_16b;
                else if (radFourByte.Checked)
                    return Texture.PixelInfo.Size_32b;
                else //if(radOneByte.Checked)
                    return Texture.PixelInfo.Size_8b;
            }
        }

        public bool EncodeTexture
        {
            get
            {
                return cbEncodeMIO0.Checked;
            }
        }

        public bool EncodePalette
        {
            get
            {
                return cbMioPalette.Checked;
            }
        }

        public int PaletteColorCount
        {
            get
            {
                if (radHalfByte.Checked)
                    return 16;
                else //if(radOneByte.Checked)
                    return 256;
            }
        }

        private void radFormatChanged(object sender, EventArgs e)
        {
            gbPalette.Enabled = radCI.Checked;

            UpdateValidPixelSizes();
        }

        private void UpdateValidPixelSizes()
        {
            switch (Format)
            {
                case Texture.ImageFormat.RGBA:
                    radHalfByte.Enabled = false;
                    radOneByte.Enabled = false;
                    radTwoByte.Enabled = true;
                    radFourByte.Enabled = true;
                    if (!radTwoByte.Checked && !radFourByte.Checked)
                        radTwoByte.Checked = true;
                    break;
                case Texture.ImageFormat.CI:
                    radHalfByte.Enabled = true;
                    radOneByte.Enabled = true;
                    radTwoByte.Enabled = false;
                    radFourByte.Enabled = false;
                    if (!radHalfByte.Checked && !radOneByte.Checked)
                        radOneByte.Checked = true;
                    break;
                case Texture.ImageFormat.IA:
                    radHalfByte.Enabled = true;
                    radOneByte.Enabled = true;
                    radTwoByte.Enabled = true;
                    radFourByte.Enabled = false;
                    if (!radHalfByte.Checked && !radOneByte.Checked && !radTwoByte.Checked)
                        radTwoByte.Checked = true;
                    break;
                case Texture.ImageFormat.I:
                    radHalfByte.Enabled = true;
                    radOneByte.Enabled = true;
                    radTwoByte.Enabled = false;
                    radFourByte.Enabled = false;
                    if (!radHalfByte.Checked && !radOneByte.Checked)
                        radOneByte.Checked = true;
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileNames = new List<string>(openFileDialog.FileNames);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
