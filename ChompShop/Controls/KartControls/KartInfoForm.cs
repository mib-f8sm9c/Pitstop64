using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using Pitstop64.Data;
using System.IO;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace ChompShop.Controls.KartControls
{
    public partial class KartInfoForm : ChompShopWindow
    {
        public bool _initializing;

        public KartInfoForm(KartWrapper kart)
            : base(kart)
        {
            InitializeComponent();

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            this.ResetTitleText();

            ClearView();

            if (Kart == null)
                return;

            txtKartName.Text = Kart.Kart.KartName;

            if (Kart.Kart.KartNamePlate != null)
            {
                pbNamePlate.Image = Kart.Kart.KartNamePlate.Image;
                btnExportNamePlate.Enabled = true;
            }
            else
            {
                btnExportNamePlate.Enabled = false;
            }

            if (Kart.Kart.KartMiniIcon != null)
            {
                pbIcon.Image = Kart.Kart.KartMiniIcon.Image;
                btnExportIcon.Enabled = true;
            }
            else
            {
                btnExportIcon.Enabled = false;
            }

            if (Kart.Kart.KartMiniPortrait != null)
            {
                pbPortrait.Image = Kart.Kart.KartMiniPortrait.Image;
                btnExportPortrait.Enabled = true;
            }
            else
            {
                btnExportPortrait.Enabled = false;
            }

            _initializing = false;
        }

        private void ClearView()
        {
            txtKartName.Text = string.Empty;
            pbNamePlate.Image = null;
            pbIcon.Image = null;
            pbPortrait.Image = null;
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartName; } }

        protected override string TitleText { get { return "Kart Info - {0}"; } }

        private void txtKartName_TextChanged(object sender, EventArgs e)
        {
            if(_initializing)
                return;

            //Change the name and alert everywhere
            string oldName = Kart.Kart.KartName;
            string newName = ClearInvalidChars(txtKartName.Text);

            if(oldName == newName)
                return;

            //Here, double check for valid characters?

            foreach (KartWrapper wrapper in ChompShopFloor.Karts)
            {
                if (wrapper.Kart.KartName == newName)
                {
                    MessageBox.Show("Name already exists. Please make a new name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _initializing = true;
                    txtKartName.Text = oldName;
                    _initializing = false;

                    return;
                }
            }

            Kart.SetName(newName);
        }

        public string ClearInvalidChars(string input)
        {
            string output = string.Empty;
            foreach (char c in input)
            {
                if (_validChars.Contains(c))
                    output += c;
            }

            return output;
        }

        private void btnExportNamePlate_Click(object sender, EventArgs e)
        {
            if (saveNamePlateDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Blah blah blah
                Kart.Kart.KartNamePlate.Image.Save(saveNamePlateDialog.FileName);
            }
        }

        private void btnImportNamePlate_Click(object sender, EventArgs e)
        {
            if (openNamePlateDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Open the file, load into the image place if it works, update the kart
                Image img = Bitmap.FromFile(openNamePlateDialog.FileName);
                if (img.Width != 64 || img.Height != 12)
                {
                    MessageBox.Show("Image must be 64x12!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (img != null)
                {
                    //Copy the old name plate to make the new one
                    byte[] imgData = Cereal64.Microcodes.F3DEX.DataElements.TextureConversion.RGBA16ToBinary((Bitmap)img);
                    TKMK00Block tkmk = new TKMK00Block(-1, Pitstop64.Services.TKMK00.Encode(imgData, img.Width, img.Height, 0), 0);

                    MK64Image image = new MK64Image(tkmk, Path.GetFileNameWithoutExtension(openNamePlateDialog.FileName));

                    Kart.SetNamePlate(image);

                    pbNamePlate.Image = tkmk.Image;
                }
            }
        }

        private void btnExportIcon_Click(object sender, EventArgs e)
        {
            if (saveNamePlateDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Kart.Kart.KartMiniIcon.Image.Save(saveNamePlateDialog.FileName);
            }
        }

        private void btnImportIcon_Click(object sender, EventArgs e)
        {
            if (openNamePlateDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Open the file, load into the image place if it works, update the kart
                Image img = Bitmap.FromFile(openNamePlateDialog.FileName);
                if (img.Width != 8 || img.Height != 8)
                {
                    MessageBox.Show("Image must be 8x8!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (img != null)
                {
                    //Copy the old name plate to make the new one
                    byte[] imgData = Cereal64.Microcodes.F3DEX.DataElements.TextureConversion.RGBA16ToBinary((Bitmap)img);
                    Texture texture = new Texture(-1, imgData, Texture.ImageFormat.RGBA, Texture.PixelInfo.Size_16b, 8, 8);
                    F3DEXImage f3dImage = new F3DEXImage(texture);

                    MK64Image image = new MK64Image(f3dImage, Path.GetFileNameWithoutExtension(openNamePlateDialog.FileName));

                    Kart.SetMiniIcon(image);

                    pbIcon.Image = image.Image;
                }
            }
        }

        private void btnExportPortrait_Click(object sender, EventArgs e)
        {
            if (saveNamePlateDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Kart.Kart.KartMiniPortrait.Image.Save(saveNamePlateDialog.FileName);
            }
        }

        private void btnImportPortrait_Click(object sender, EventArgs e)
        {
            if (openNamePlateDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Open the file, load into the image place if it works, update the kart
                Image img = Bitmap.FromFile(openNamePlateDialog.FileName);
                if (img.Width != 32 || img.Height != 32)
                {
                    MessageBox.Show("Image must be 32x32!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (img != null)
                {
                    //Copy the old name plate to make the new one
                    Palette palette = new Palette(-1, new byte[512]);
                    int index = 0;
                    byte[] imgData = Cereal64.Microcodes.F3DEX.DataElements.TextureConversion.CI8ToBinary((Bitmap)img, palette, ref index, true);
                    Texture texture = new Texture(-1, imgData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 32, 32);
                    F3DEXImage f3dImage = new F3DEXImage(texture, palette);

                    MK64Image image = new MK64Image(f3dImage, Path.GetFileNameWithoutExtension(openNamePlateDialog.FileName));

                    Kart.SetMiniPortrait(image);

                    pbPortrait.Image = image.Image;
                }
            }
        }

        private char[] _validChars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                         'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                                         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
                                         't', 'u', 'v', 'w', 'x', 'y', 'z' };

    }
}
