using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data.Karts;

namespace ChompShop
{
    public partial class ChompShopForm : Form
    {
        private bool unsavedChanges = false;

        public ChompShopForm()
        {
            InitializeComponent();

            ChompShopFloor.KartChanged += KartUpdated;
        }


        private void KartUpdated()
        {
            unsavedChanges = true;
        }

        private void newKartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckIfNeedToSave();

            //kartName = RequestKartName();

            //ChompShopFloor.NewKart(kartName);
        }

        private void loadKartToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
    }
}
