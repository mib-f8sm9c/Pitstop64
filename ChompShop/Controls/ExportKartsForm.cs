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
    public partial class ExportKartsForm : ChompShopWindow
    {
        private List<KartWrapper> _checkedWrappers;

        private bool _initializing;

        public ExportKartsForm()
            : base (null)
        {
            InitializeComponent();
            ChompShopAlerts.LoadedKartsChanged += ChompShopAlerts_LoadedKartsChanged;

            _checkedWrappers = new List<KartWrapper>();
            InitData();
        }

        private void ChompShopAlerts_LoadedKartsChanged()
        {
            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            PopulateKartCheckedListBox();

            _initializing = false;
        }

        private void PopulateKartCheckedListBox()
        {
            List<KartWrapper> newCheckedWrappers = new List<KartWrapper>();

            Dictionary<KartWrapper, CheckState> oldCheckedStates = new Dictionary<KartWrapper, CheckState>();

            for (int i = 0; i < clbKarts.Items.Count; i++)
                oldCheckedStates.Add((KartWrapper)clbKarts.Items[i], clbKarts.GetItemCheckState(i));

            clbKarts.Items.Clear();

            foreach (KartWrapper kart in ChompShopFloor.Karts)
            {
                CheckState state = CheckState.Unchecked;
                if(oldCheckedStates.ContainsKey(kart))
                    state = oldCheckedStates[kart];
                if (!kart.ValidKart)
                    state = CheckState.Indeterminate;


                clbKarts.Items.Add(kart, state);
                if (state == CheckState.Checked)
                    newCheckedWrappers.Add(kart);
            }

            _checkedWrappers = newCheckedWrappers;
        }

        private void clbKarts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_initializing)
                return;

            if (e.CurrentValue == CheckState.Indeterminate)  //Disabled checkbox
            {
                e.NewValue = CheckState.Indeterminate;
                return;
            }

            if (e.NewValue == CheckState.Checked)
            {
                _checkedWrappers.Add((KartWrapper)clbKarts.Items[e.Index]);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                _checkedWrappers.Remove((KartWrapper)clbKarts.Items[e.Index]);
            }
            //Update stuff
            CheckedWrappersUpdated();
        }

        private void CheckedWrappersUpdated()
        {
            //Export button
            btnExport.Enabled = _checkedWrappers.Count > 0;
            btnExport.Text = (_checkedWrappers.Count > 1 ? "Export Karts..." : "Export Kart...");

            //Info form
            //Do something here : )
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_checkedWrappers.Count > 0)
            {
                if(saveKartDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ChompShopFloor.SaveKartToDisk(saveKartDialog.FileName, _checkedWrappers);
                }
            }
        }

        protected override void KartNameUpdated(KartWrapper wrapper)
        {
            for (int i = 0; i < clbKarts.Items.Count; i++)
            {
                if (clbKarts.Items[i] == wrapper)
                {
                    clbKarts.Items[i] = wrapper;
                }
            }
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.ExportKarts; } }

        protected override string TitleText { get { return "Export Karts"; } }

        private void clbKarts_SelectedIndexChanged(object sender, EventArgs e)
        {
            kartValidityControl.SetKart((KartWrapper)clbKarts.SelectedItem);
        }
    }
}
