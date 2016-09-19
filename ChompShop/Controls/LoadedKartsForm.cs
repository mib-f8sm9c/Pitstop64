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

        private void SetButtonsEnabled()
        {
            bool enabled = lbKarts.SelectedIndex != -1;

            btnName.Enabled = enabled;
            btnPortraits.Enabled = enabled;
            btnImages.Enabled = enabled;
            btnAnims.Enabled = enabled;
            btnResetChanges.Enabled = enabled;
        }

        private void PopulateKartListBox()
        {
            KartWrapper selectedWrapper = (KartWrapper)lbKarts.SelectedItem;
            lbKarts.Items.Clear();

            lbKarts.Items.AddRange(ChompShopFloor.Karts.ToArray());

            if (lbKarts.Items.Count > 0)
            {
                if (selectedWrapper == null)
                    lbKarts.SelectedIndex = 0;
                else
                {
                    int index = ChompShopFloor.Karts.IndexOf(selectedWrapper);
                    if (index == -1)
                        index = 0;
                    lbKarts.SelectedIndex = index;
                }
            }
        }

        public override ChompShopWindowType WindowType
        {
            get { return ChompShopWindowType.LoadedKarts; }
        }

        private KartWrapper SelectedKart { get { if (lbKarts.SelectedIndex == -1) return null; return (KartWrapper)lbKarts.SelectedItem; } }

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
    }
}
