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
    //To do: make this a pop-up window rather than a in-form window? Also have one to allow you to import only select karts?
    public partial class ReferenceKartForm : ChompShopWindow
    {
        private bool _initializing;

        public ReferenceKartForm()
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
            _initializing = true;

            //select appropriate rad
            radUseFile.Enabled = ChompShopFloor.HasReferenceKartFile;

            if (ChompShopFloor.UsingLoadedReferenceKart)
            {
                radUseFile.Checked = true;
                lbKarts.Enabled = false;
            }
            else if (ChompShopFloor.ReferenceKart != null)
            {
                radUseKart.Checked = true;
                lbKarts.Enabled = true;
            }
            else
            {
                radDontUse.Checked = true;
                lbKarts.Enabled = false;
            }

            PopulateKartListBox();

            _initializing = false;
        }

        private void PopulateKartListBox()
        {
            lbKarts.Items.Clear();

            foreach (KartWrapper kart in ChompShopFloor.Karts)
            {
                lbKarts.Items.Add(kart);
            }

            if (!ChompShopFloor.UsingLoadedReferenceKart)
            {
                if (ChompShopFloor.Karts.Contains(ChompShopFloor.ReferenceKart))
                {
                    lbKarts.SelectedItem = ChompShopFloor.ReferenceKart;
                }
                else
                {
                    radDontUse.Checked = true;
                }
            }
        }

        private void radCheckedChanged(object sender, EventArgs e)
        {
            if (_initializing)
                return;

            lbKarts.Enabled = radUseKart.Checked;
            btnSaveRef.Enabled = radUseKart.Checked;

            if (radUseFile.Checked)
                ChompShopFloor.LoadReferenceKart();
            else if (radDontUse.Checked)
                ChompShopFloor.ClearReferenceKart();
        }

        private void btnSaveRef_Click(object sender, EventArgs e)
        {
            //Save the reference kart, and select the rad
            if (ChompShopFloor.ReferenceKart != null)
            {
                ChompShopFloor.SaveReferenceKart();
                radUseFile.Checked = true;
                radUseFile.Enabled = true;
            }
        }

        private void lbKarts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSaveRef.Enabled = (lbKarts.SelectedIndex != -1);

            ChompShopFloor.LoadReferenceKart((KartWrapper)lbKarts.SelectedItem);
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

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.ReferenceKart; } }

        protected override string TitleText { get { return "Reference Kart"; } }

    }
}
