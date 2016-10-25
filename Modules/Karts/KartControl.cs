using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Microcodes.F3DEX.DataElements;
using MK64Pitstop.Services;
using MK64Pitstop.Data.Karts;
using MK64Pitstop.Services.Hub;
using Cereal64.Common.Rom;
using System.Xml.Linq;
using System.IO;
using Ionic.Zip;

namespace MK64Pitstop.Modules.Karts
{
    public partial class KartControl : UserControl
    {
        private bool SettingsChanged { get { return _settingsChanged; } set { _settingsChanged = value; btnKartsApply.Enabled = value; } }
        private bool _settingsChanged;

        private const string CHOMP_SHOP_EXEC = @"ChompShop.exe";

        public KartControl()
        {
            InitializeComponent();

            btnChompShop.Enabled = File.Exists(CHOMP_SHOP_EXEC);
        }

        public void UpdateReferences()
        {
            UpdateKartList();
            UpdateKartInfo();
            UpdateSelectedKartList();

            UpdateEnableds();

            SettingsChanged = false;
        }

        private void SaveChanges()
        {
            SaveSelectedKartsChanges();

            SettingsChanged = false;
        }

        private void btnKartsApply_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void UpdateEnableds()
        {
            bool enabled = MarioKart64ElementHub.Instance.Karts.Count > 0;
            gbKarts.Enabled = enabled;
            gbSelectedKarts.Enabled = enabled;
        }

        private void btnKartsCancel_Click(object sender, EventArgs e)
        {
            UpdateReferences();
        }

        private void btnChompShop_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(CHOMP_SHOP_EXEC);
        }

        #region KartInfo

        private void UpdateKartList()
        {
            KartInfo SelectedKart = SelectedKartInfo;
            lbAllKarts.Items.Clear();

            foreach (KartInfo kart in MarioKart64ElementHub.Instance.Karts)
            {
                lbAllKarts.Items.Add(kart);
            }

            if (SelectedKart != null && lbAllKarts.Items.Contains(SelectedKart))
                lbAllKarts.SelectedItem = SelectedKart;
            else if (lbAllKarts.Items.Count > 0)
                lbAllKarts.SelectedIndex = 0;
        }

        private KartInfo[] AllSelectedKartInfos
        {
            get
            {
                KartInfo[] karts = new KartInfo[lbAllKarts.SelectedItems.Count];

                for (int i = 0; i < lbAllKarts.SelectedItems.Count; i++)
                    karts[i] = (KartInfo)lbAllKarts.SelectedItems[i];

                return karts;
            }
        }

        private KartInfo SelectedKartInfo
        {
            get
            {
                if (lbAllKarts.SelectedItems.Count > 1)
                    return null;

                return (KartInfo)lbAllKarts.SelectedItem;
            }
        }
        
        private void lbAllKarts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateKartInfo();
        }

        private void UpdateKartInfo()
        {
            if (SelectedKartInfo == null)
            {
                //Clear portrait and kart viewer
                pbPortrait.Image = null;
                kartPreviewControl.Kart = null;
                lblKartName.Text = string.Empty;

                return;
            }

            //fill out information
            pbPortrait.Image = ((Texture)SelectedKartInfo.KartPortraits[0].DecodedN64DataElement).Image;
            kartPreviewControl.Kart = SelectedKartInfo;
            lblKartName.Text = SelectedKartInfo.KartName;
        }

        private void btnExportKart_Click(object sender, EventArgs e)
        {
            //Export the kart to an external file here!

            if (saveKartDialog.ShowDialog() == DialogResult.OK)
            {
                if(File.Exists(saveKartDialog.FileName))
                {
                    DialogResult result = MessageBox.Show("Karts file already exists. Overwrite file? (Yes overwrites, No appends)",
                        "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                    if (result == DialogResult.Yes)
                        File.Delete(saveKartDialog.FileName);
                    else if (result == DialogResult.Cancel)
                        return;
                }
                KartInfo.SaveKarts(saveKartDialog.FileName, AllSelectedKartInfos);

            }
        }
        
        private void btnImportKart_Click(object sender, EventArgs e)
        {
            if (openKartDialog.ShowDialog() == DialogResult.OK)
            {
                List<KartInfo> karts = KartInfo.LoadFromFile(openKartDialog.FileName);

                int addedKarts = 0;

                foreach (KartInfo kart in karts)
                {
                    if (MarioKart64ElementHub.Instance.Karts.SingleOrDefault(k => k.KartName == kart.KartName) == null)
                    {
                        MarioKart64ElementHub.Instance.Karts.Add(kart);
                        RomProject.Instance.AddRomItem(kart);
                        addedKarts++;
                    }
                }


                if (addedKarts > 0)
                {
                    if (karts.Count == 1)
                    {
                        //Single kart, success
                        MessageBox.Show("Kart loaded");
                    }
                    else
                    {
                        if (addedKarts != karts.Count)
                        {
                            //Some failure
                            MessageBox.Show("Failed to load some karts (kart name already exists)");
                        }
                        else
                        {
                            MessageBox.Show("Karts loaded");
                        }
                    }
                    UpdateReferences();
                }
                else
                {
                    if (karts.Count == 0)
                    {
                        //No karts from start
                        MessageBox.Show("No karts found!");
                    }
                    else if (karts.Count == 1)
                    {
                        //Single kart, name taken
                        MessageBox.Show("Kart failed to load (kart name already exists)");
                    }
                    else
                    {
                        //Multi karts, all names taken
                        MessageBox.Show("All karts failed to loaded (kart names already exist)");
                    }
                }

            }
        }

        private void btnRemoveKart_Click(object sender, EventArgs e)
        {
            //Not done
            throw new NotImplementedException();
        }

        #endregion

        #region SelectedKarts

        private void btnKartUp_Click(object sender, EventArgs e)
        {
            //Move the selected kart up
            if(lbKarts.SelectedIndex != 0)
            {
                KartInfo tempKart = (KartInfo)lbKarts.SelectedItem;
                lbKarts.Items[lbKarts.SelectedIndex] = lbKarts.Items[lbKarts.SelectedIndex - 1];
                lbKarts.Items[lbKarts.SelectedIndex - 1] = tempKart;

                SettingsChanged = true;
                lbKarts.SelectedIndex--;
            }
        }

        private void btnKartDown_Click(object sender, EventArgs e)
        {
            //Move the selected kart down
            if (lbKarts.SelectedIndex != lbKarts.Items.Count - 1)
            {
                KartInfo tempKart = (KartInfo)lbKarts.SelectedItem;
                lbKarts.Items[lbKarts.SelectedIndex] = lbKarts.Items[lbKarts.SelectedIndex + 1];
                lbKarts.Items[lbKarts.SelectedIndex + 1] = tempKart;

                SettingsChanged = true;
                lbKarts.SelectedIndex++;
            }
        }

        private void btnInsertKart_Click(object sender, EventArgs e)
        {
            //Replace the selected kart with the kart from the dropdown list
            if (cbKartList.SelectedIndex != -1)
            {
                lbKarts.Items[lbKarts.SelectedIndex] = (KartInfo)cbKartList.SelectedItem;

                SettingsChanged = true;
                UpdateSelectedKartButtons();
            }

        }

        private void btnKartsReset_Click(object sender, EventArgs e)
        {
            //Reset the karts to its original order
            for (int i = 0; i < 8; i++)
            {
                lbKarts.Items[i] = MarioKart64ElementHub.Instance.Karts[i]; //Should be loaded in order, never changing order
            }

            SettingsChanged = false;
            UpdateSelectedKartButtons();
        }

        private void UpdateSelectedKartButtons()
        {
            if (MarioKart64ElementHub.Instance.Karts.Count == 0)
                btnKartsReset.Enabled = false;
            else
                btnKartsReset.Enabled = true;

            if (lbKarts.SelectedIndex != -1)
            {
                btnKartUp.Enabled = true;
                btnKartDown.Enabled = true;
                btnInsertKart.Enabled = true;
            }
            else
            {
                btnKartUp.Enabled = false;
                btnKartDown.Enabled = false;
                btnInsertKart.Enabled = false;
            }
        }

        private void lbKarts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedKartButtons();
        }

        private void UpdateSelectedKartList()
        {
            KartInfo selectedKart = (KartInfo)lbKarts.SelectedItem;

            lbKarts.Items.Clear();
            cbKartList.Items.Clear();

            if (MarioKart64ElementHub.Instance.Karts.Count == 0)
                return;

            foreach (KartInfo kart in MarioKart64ElementHub.Instance.SelectedKarts)
            {
                lbKarts.Items.Add(kart);
            }

            foreach (KartInfo kart in MarioKart64ElementHub.Instance.Karts)
            {
                cbKartList.Items.Add(kart);
            }

            if (selectedKart != null && cbKartList.Items.Contains(selectedKart))
                cbKartList.SelectedItem = selectedKart;
            else if (cbKartList.Items.Count > 0)
                cbKartList.SelectedIndex = 0;

            UpdateSelectedKartButtons();
        }

        private void SaveSelectedKartsChanges()
        {
            //Set the selected karts ordering
            for (int i = 0; i < MarioKart64ElementHub.Instance.SelectedKarts.Length; i++)
            {
                MarioKart64ElementHub.Instance.SelectedKarts[i] = (KartInfo)lbKarts.Items[i];
            }

            UpdateSelectedKartButtons();
        }

        #endregion


    }
}
