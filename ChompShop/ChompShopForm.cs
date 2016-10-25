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

            ChompShopFloor.LoadReferenceKart();
        }

        private void newKartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CheckIfNeedToSave();

            //kartName = RequestKartName();

            //ChompShopFloor.NewKart(kartName);
            TextInputForm form = new TextInputForm("Kart Name:", "New Kart Name");
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Later on, do better name verifcation
                if (string.IsNullOrWhiteSpace(form.TextOutput))
                {
                    MessageBox.Show("Kart Name is invalid!");
                }
                else
                    ChompShopFloor.NewKart(form.TextOutput);
            }
        }

        private void loadKartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openKartDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(openKartDialog.FileNames.Length > 1)
                    ChompShopFloor.LoadKarts(openKartDialog.FileNames);
                else
                    ChompShopFloor.LoadKarts(openKartDialog.FileName);
            }
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

        private void referenceKartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controlController.ShowSingleForm(ChompShopWindowType.ReferenceKart);
        }
    }
}
