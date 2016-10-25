using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;

namespace ChompShop.Controls
{
    public partial class LoadedKartsForm : ChompShopWindow
    {
        public LoadedKartsForm()
            : base (null)
        {
            InitializeComponent();
            ChompShopAlerts.LoadedKartsChanged += ChompShopAlerts_LoadedKartsChanged;

            InitData();
        }

        private void ChompShopAlerts_LoadedKartsChanged()
        {
            InitData();
        }

        public override void InitData()
        {
            PopulateKartListBox();
            SetButtonsEnabled();
        }

        private KartWrapper SelectedKart { get { return (KartWrapper)lbKarts.SelectedItem; } }
        
        private void SetButtonsEnabled()
        {
            bool enabled = (SelectedKart != null);
            bool hasChanges = (enabled ? SelectedKart.IsModified : false);

            btnName.Enabled = enabled;
            btnPortraits.Enabled = enabled;
            btnImages.Enabled = enabled;
            btnAnims.Enabled = enabled;
            btnResetChanges.Enabled = enabled;
            btnCopy.Enabled = enabled;
            btnRemove.Enabled = enabled;

            btnResetChanges.Enabled = hasChanges;
            btnSaveChanges.Enabled = hasChanges;
        }

        private void PopulateKartListBox()
        {
            lbKarts.Items.Clear();

            lbKarts.Items.AddRange(ChompShopFloor.Karts.ToArray());

            if (lbKarts.Items.Count > 0)
            {
                if (SelectedKart == null)
                    lbKarts.SelectedIndex = 0;
                else
                {
                    int index = ChompShopFloor.Karts.IndexOf(SelectedKart);
                    if (index == -1)
                        index = 0;
                    lbKarts.SelectedIndex = index;
                }
            }
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            if (SelectedKart != null)
                this.ChompShopForm.ControlController.ShowKartForm(SelectedKart, ChompShopWindowType.KartName);
        }

        private void btnPortraits_Click(object sender, EventArgs e)
        {
            if (SelectedKart != null)
                this.ChompShopForm.ControlController.ShowKartForm(SelectedKart, ChompShopWindowType.KartPortraits);
        }

        private void btnImages_Click(object sender, EventArgs e)
        {
            if (SelectedKart != null)
                this.ChompShopForm.ControlController.ShowKartForm(SelectedKart, ChompShopWindowType.KartImages);
        }

        private void btnAnims_Click(object sender, EventArgs e)
        {
            if (SelectedKart != null)
                this.ChompShopForm.ControlController.ShowKartForm(SelectedKart, ChompShopWindowType.KartAnimations);
        }

        private void btnAddKart_Click(object sender, EventArgs e)
        {
            //Add a kart
            if (openKartDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ChompShopFloor.LoadKarts(openKartDialog.FileName);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Remove a kart
            if (SelectedKart != null)
            {
                KartWrapper kart = SelectedKart;
                ChompShopFloor.RemoveKart(kart);
                ChompShopForm.ControlController.ClearKartForms(kart); //Move to an alert if we use this line anywhere else?
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            //Copy a kart
            if (SelectedKart != null)
            {
                ChompShopFloor.CopyKart(SelectedKart);
            }
        }

        protected override void KartNameUpdated(KartWrapper wrapper)
        {
            for (int i = 0; i < lbKarts.Items.Count; i++)
            {
                if (lbKarts.Items[i] == wrapper)
                {
                    lbKarts.Items[i] = wrapper;
                }
            }
        }

        private void btnResetChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All changes since last save will be lost. Continue?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.OK)
            {
                SelectedKart.RevertChanges();
                InitData();
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SelectedKart.SaveChanges();
            SetButtonsEnabled();
        }

        private void lbKarts_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonsEnabled();
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.LoadedKarts; } }

        protected override string TitleText { get { return "Loaded Karts"; } }
    }
}
