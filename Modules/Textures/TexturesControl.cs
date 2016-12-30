using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pitstop64.Data;
using MarioKartTestingTool;

namespace Pitstop64.Modules.Textures
{
    public partial class TexturesControl : UserControl
    {
        public TexturesControl()
        {
            InitializeComponent();
        }

        private List<TKMK00Block> _tkmkBlocks;

        public void AddTKMK00Textures(List<TKMK00Block> tkmks)
        {
            _tkmkBlocks = tkmks;

            cbImage.Items.Clear();

            foreach(TKMK00Block block in _tkmkBlocks)
            {
                cbImage.Items.Add(block);
            }

            if(cbImage.Items.Count > 0)
                cbImage.SelectedIndex = 0;
        }

        private void cbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbImage.SelectedIndex >= 0)
            {
                pbImage.Image = ((TKMK00Block)cbImage.Items[cbImage.SelectedIndex]).Image;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbImage.Image.Save(saveFileDialog.FileName);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);

                TKMK00Block tkmk = (TKMK00Block)cbImage.Items[cbImage.SelectedIndex];

                if (!tkmk.SetImage(bmp))
                {
                    MessageBox.Show("Overwrite failed (Image too big?)");
                }
                else
                {
                    pbImage.Image = tkmk.Image;
                }
            }
        }

        public void UpdateControl()
        {

        }
    }
}
