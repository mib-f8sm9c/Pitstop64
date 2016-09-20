using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data.Karts;
using MK64Pitstop.Modules.Karts;
using Cereal64.Microcodes.F3DEX.DataElements;
using ChompShop.Controls;
using ChompShop.Data;

namespace ChompShop
{
    public partial class ChompShopForm : Form
    {
        private bool unsavedChanges = false;

        public ControlController ControlController { get { return _controlController; } }
        private ControlController _controlController;

        public ChompShopForm()
        {
            InitializeComponent();

            _controlController = new ControlController(this);
        }

        private void newKartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CheckIfNeedToSave();

            //kartName = RequestKartName();

            //ChompShopFloor.NewKart(kartName);
            NewKartForm form = new NewKartForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ChompShopFloor.NewKart(form.KartName);
            }
        }

        private void loadKartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openKartDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ChompShopFloor.LoadKarts(openKartDialog.FileName);
                //pbPreview.Image = ((Texture)ChompShopFloor.CurrentKart.KartPortraits[0].DecodedN64DataElement).Image;
            }
        }

        private void saveKartToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveKartAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckIfNeedToSave();

            this.Close();
        }

        private void CheckIfNeedToSave()
        {
            if (unsavedChanges)
            {
                //Ask to save
            }
        }

        private void loadedKartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controlController.ShowSingleForm(ChompShopWindowType.LoadedKarts);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controlController.ShowSingleForm(ChompShopWindowType.ExportKarts);
        }
    }
}
