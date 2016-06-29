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

namespace MK64Pitstop.Modules.Karts
{
    public partial class KartControl : UserControl
    {
        private bool SettingsChanged { get { return _settingsChanged; } set { _settingsChanged = value; btnKartsApply.Enabled = value; } }
        private bool _settingsChanged;

        public KartControl()
        {
            InitializeComponent();
        }

        public void UpdateCurrentTab()
        {
            if (tabKartModule.SelectedTab == tabKarts)
            {
                UpdateKartInfo();
            }
            else if (tabKartModule.SelectedTab == tabSelectedKarts)
            {
                UpdateSelectedKarts();
            }
            else if (tabKartModule.SelectedTab == tabKartAnim)
            {
                UpdateKartAnimations();
            }
        }

        public void UpdateReferences()
        {
            if (MarioKart64ElementHub.Instance.Karts.Count == 0)
                return;
            
            //UpdateKartInfo();
            UpdateCurrentTab();

            SettingsChanged = false;
        }

        private void tabKarts_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            //Handle saving before switching tabs
            if (SettingsChanged)
            {
                DialogResult result = MessageBox.Show("Save changes before switching tabs?", "Warning", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation);

                switch (result)
                {
                    case DialogResult.Yes:
                        SaveTabChanges();
                        break;
                    case DialogResult.No:
                        SettingsChanged = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void tabKartModule_Selected(object sender, TabControlEventArgs e)
        {
            UpdateCurrentTab();
        }

        private void SaveTabChanges()
        {
            if (tabKartModule.SelectedTab == tabKarts)
            {
                SaveKartInfoChanges();
            }
            else if (tabKartModule.SelectedTab == tabSelectedKarts)
            {
                SaveSelectedKartsChanges();
            }
            else if (tabKartModule.SelectedTab == tabKartAnim)
            {
                SaveKartAnimationsChanges();
            }

            SettingsChanged = false;
        }

        private void btnKartsApply_Click(object sender, EventArgs e)
        {
            SaveTabChanges();
        }

        #region SelectedKarts

        private void btnKartsCancel_Click(object sender, EventArgs e)
        {
            //Reset the selected karts ordering
            int selectedIndex = lbKarts.SelectedIndex;
            lbKarts.Items.Clear();

            foreach (KartInfo kart in MarioKart64ElementHub.Instance.SelectedKarts)
                lbKarts.Items.Add(kart);

            lbKarts.SelectedIndex = selectedIndex;

            SettingsChanged = false;
            UpdateSelectedKartButtons();
        }

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

        private void UpdateSelectedKarts()
        {
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

        #region AnimationSelect

        private KartInfo SelectedKart
        {
            get
            {
                if (cbCurrentKart.SelectedIndex == -1)
                    return null;
                return (KartInfo)cbCurrentKart.SelectedItem;
            }
        }

        private KartAnimationSeries SelectedAnim
        {
            get
            {
                if (lbAnimations.SelectedIndex == -1)
                    return null;
                return (KartAnimationSeries)lbAnimations.SelectedItem;
            }
        }

        private void cbCurrentKart_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Populate the animations list box
            lbAnimations.Items.Clear();

            if (SelectedKart == null)
                return;

            foreach (KartAnimationSeries anim in SelectedKart.KartAnimations)
            {
                lbAnimations.Items.Add(anim);
            }

            if (lbAnimations.Items.Count > 0)
                lbAnimations.SelectedIndex = 0;
            else
            {
                lbAnimImages.Items.Clear();
                SetImageButtonsEnabled();
            }
        }

        private void lbAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Populate the animation edit group
            SetImageButtonsEnabled();

            PopulateAnimationWindow();
        }

        private void btnAnimationsAdd_Click(object sender, EventArgs e)
        {
            //Add a new animation
            NewAnimForm form = new NewAnimForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                if (SelectedKart.KartAnimations.SingleOrDefault(k => k.Name == form.AnimationName) == null)
                {
                    SelectedKart.KartAnimations.Add(new KartAnimationSeries(form.AnimationName));
                    lbAnimations.Items.Add(SelectedKart.KartAnimations.Last());
                    lbAnimations.SelectedIndex = lbAnimations.Items.Count - 1;

                    SettingsChanged = true;
                }
                else
                {
                    MessageBox.Show("Animation with that name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAnimationsDelete_Click(object sender, EventArgs e)
        {
            //Delete an animation
            if (SelectedAnim != null)
            {
                int selectedIndex = lbAnimations.SelectedIndex;

                SelectedKart.KartAnimations.Remove(SelectedAnim);
                lbAnimations.Items.RemoveAt(selectedIndex);

                if (lbAnimations.Items.Count > selectedIndex)
                {
                    lbAnimations.SelectedIndex = selectedIndex;
                }

                SettingsChanged = true;
            }
        }

        private KartInfo MarioKart;

        private void UpdateKartAnimations()
        {
            cbCurrentKart.Items.Clear();
            KartInfoCopies.Clear();

            if (MarioKart64ElementHub.Instance.Karts.Count == 0)
                return;

            foreach (KartInfo kart in MarioKart64ElementHub.Instance.Karts)
            {
                //MarioKart used for overlay
                if (kart.KartName == "Mario")
                    MarioKart = kart;

                KartInfo newKart = new KartInfo(kart.KartName, kart.KartImages.ImagePalette, kart.OriginalKart);
                foreach (KartAnimationSeries anim in kart.KartAnimations)
                {
                    KartAnimationSeries newAnim = new KartAnimationSeries(anim.Name);
                    newAnim.KartAnimationType = anim.KartAnimationType;
                    newAnim.OrderedImageNames.AddRange(anim.OrderedImageNames);
                    newKart.KartAnimations.Add(newAnim);
                }
                foreach (string key in kart.KartImages.Images.Keys)
                    newKart.KartImages.Images.Add(key, kart.KartImages.Images[key]);
                cbCurrentKart.Items.Add(newKart);
                KartInfoCopies.Add(newKart, kart);
            }

            cbCurrentKart.SelectedIndex = 0;
        }
        
        private void SaveKartAnimationsChanges()
        {
            foreach (KartInfo newKart in KartInfoCopies.Keys)
            {
                KartInfoCopies[newKart].KartAnimations.Clear();
                KartInfoCopies[newKart].KartImages.Images.Clear();

                if (KartInfoCopies[newKart].KartImages.ImagePalette == null && newKart.KartImages.ImagePalette != null)
                    KartInfoCopies[newKart].KartImages.SetPalette(newKart.KartImages.ImagePalette);

                KartInfoCopies[newKart].KartAnimations.AddRange(newKart.KartAnimations);
                foreach (string key in newKart.KartImages.Images.Keys)
                    KartInfoCopies[newKart].KartImages.Images.Add(key, newKart.KartImages.Images[key]);

            }
        }

        #endregion

        #region AnimationWindow
        
        private void SetImageButtonsEnabled()
        {
            if(lbAnimations.SelectedIndex == -1)
            {
                pbImage.Image = null;
                pbOverlay.Image = null;
                btnAnimImageAdd.Enabled = false;
                btnAnimImageRemove.Enabled = false;
                btnAnimationsDelete.Enabled = false;
                btnAnimImageUp.Enabled = false;
                btnAnimImageDown.Enabled = false;
                btnAnimImageDuplicate.Enabled = false;
                cbOverlayKart.Enabled = false;
                gbAnimationType.Enabled = false;
            }
            else
            {
                btnAnimImageAdd.Enabled = true;
                btnAnimImageRemove.Enabled = true;
                btnAnimationsDelete.Enabled = true;
                btnAnimImageUp.Enabled = true;
                btnAnimImageDown.Enabled = true;
                btnAnimImageDuplicate.Enabled = true;
                cbOverlayKart.Enabled = true;
                gbAnimationType.Enabled = true;
            }
        }

        private KartImage SelectedImage
        {
            get
            {
                if (lbAnimImages.SelectedIndex == -1)
                    return null;
                return (KartImage)lbAnimImages.SelectedItem;
            }
        }

        private KartImage SelectedOverlay
        {
            get
            {
                if (lbAnimImages.SelectedIndex == -1)
                    return null;

                int overlayIndex;
                if (SelectedAnim.IsCrashAnim)
                    overlayIndex = SelectedAnim.GetCrashFrameForImageIndex(lbAnimImages.SelectedIndex);
                else if (SelectedAnim.IsTurnAnim)
                    overlayIndex = SelectedAnim.GetTurnFrameForImageIndex(lbAnimImages.SelectedIndex);
                else //if (SelectedAnim.IsSpinAnim)
                    overlayIndex = SelectedAnim.GetSpinFrameForImageIndex(lbAnimImages.SelectedIndex);
                KartAnimationSeries matchingAnim = null;
                foreach (KartAnimationSeries anim in MarioKart.KartAnimations)
                {
                    if ((anim.KartAnimationType & SelectedAnim.KartAnimationType) != 0) //types match
                    {
                        matchingAnim = anim;
                        break;
                    }
                }

                if (matchingAnim == null || overlayIndex >= matchingAnim.OrderedImageNames.Count)
                    return null;

                int marioIndex;
                if (matchingAnim.IsCrashAnim)
                    marioIndex = matchingAnim.GetImageIndexForCrashFrame(overlayIndex);
                else if (matchingAnim.IsTurnAnim)
                    marioIndex = matchingAnim.GetImageIndexForTurnFrame(overlayIndex);
                else //if (SelectedAnim.IsSpinAnim)
                    marioIndex = matchingAnim.GetImageIndexForSpinFrame(overlayIndex);

                return MarioKart.KartImages.Images[matchingAnim.OrderedImageNames[marioIndex]];
            }
        }

        private void PopulateAnimationWindow()
        {
            int lastSelectedIndex = lbAnimImages.SelectedIndex;
            DisableCheckboxEvents();
            ClearAnimCheckboxes();
            lbAnimImages.Items.Clear();

            if (SelectedAnim == null)
                return;

            //Handle the checkboxes
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown25) != 0)
                cbAnimTurnM25.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown19) != 0)
                cbAnimTurnM19.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12) != 0)
                cbAnimTurnM12.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown6) != 0)
                cbAnimTurnM6.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurn0) != 0)
                cbAnimTurn0.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp6) != 0)
                cbAnimTurn6.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12) != 0)
                cbAnimTurn12.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp19) != 0)
                cbAnimTurn19.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp25) != 0)
                cbAnimTurn25.Checked = true;


            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25) != 0)
                cbAnimSpinM25.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19) != 0)
                cbAnimSpinM19.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12) != 0)
                cbAnimSpinM12.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6) != 0)
                cbAnimSpinM6.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpin0) != 0)
                cbAnimSpin0.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6) != 0)
                cbAnimSpin6.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12) != 0)
                cbAnimSpin12.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19) != 0)
                cbAnimSpin19.Checked = true;
            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25) != 0)
                cbAnimSpin25.Checked = true;

            if ((SelectedAnim.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.Crash) != 0)
                cbAnimCrash.Checked = true;

            EnableCheckboxEvents();

            //Fill in the image list
            foreach (string imageName in SelectedAnim.OrderedImageNames)
            {
                if (SelectedKart.KartImages.Images.ContainsKey(imageName))
                {
                    lbAnimImages.Items.Add(SelectedKart.KartImages.Images[imageName]);
                }
            }

            if (lbAnimImages.Items.Count > 0)
            {
                if (lastSelectedIndex != -1 && lastSelectedIndex < lbAnimImages.Items.Count)
                    lbAnimImages.SelectedIndex = lastSelectedIndex;
                else
                    lbAnimImages.SelectedIndex = 0;
            }
            else
            {
                pbImage.Image = null;
                pbOverlay.Image = null;
            }
        }

        private void lbAnimImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update the image in the preview
            UpdateImage();
        }

        private void UpdateImage()
        {
            pbImage.Image = null;

            if (SelectedImage == null)
                return;

            pbImage.Image = SelectedImage.Image;

            if (cbOverlayKart.Checked)
                UpdateOverlay();
        }

        private void UpdateOverlay()
        {
            //Find the correct Mario texture, then update it to be half-transparent
            pbOverlay.Image = null;

            if (pbOverlay == null)
                return;

            if (SelectedOverlay == null)
            {
                pbOverlay.Image = null;
                return;
            }

            Bitmap transparentOverlay = new Bitmap(SelectedOverlay.Image);
            for (int i = 0; i < transparentOverlay.Height; i++)
            {
                for (int j = 0; j < transparentOverlay.Width; j++)
                {
                    Color pixel = transparentOverlay.GetPixel(j, i);
                    if (pixel.A == byte.MaxValue)
                        transparentOverlay.SetPixel(j, i, Color.FromArgb(byte.MaxValue / 2, pixel));
                }
            }
            pbOverlay.Image = transparentOverlay;
        }

        private void btnAnimImageUp_Click(object sender, EventArgs e)
        {
            //Move animation image up in the list
            if (lbAnimImages.SelectedIndex != 0)
            {
                KartImage tempImage = (KartImage)lbAnimImages.SelectedItem;
                lbAnimImages.Items[lbAnimImages.SelectedIndex] = lbAnimImages.Items[lbAnimImages.SelectedIndex - 1];
                lbAnimImages.Items[lbAnimImages.SelectedIndex - 1] = tempImage;

                string tempImageName = SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex];
                SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex] = SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex - 1];
                SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex - 1] = tempImageName;

                lbAnimImages.SelectedIndex--;

                SettingsChanged = true;
            }
        }

        private void btnAnimImageDown_Click(object sender, EventArgs e)
        {
            //Move animation image down in the list
            if (lbAnimImages.SelectedIndex < lbAnimImages.Items.Count - 1)
            {
                KartImage tempImage = (KartImage)lbAnimImages.SelectedItem;
                lbAnimImages.Items[lbAnimImages.SelectedIndex] = lbAnimImages.Items[lbAnimImages.SelectedIndex + 1];
                lbAnimImages.Items[lbAnimImages.SelectedIndex + 1] = tempImage;

                string tempImageName = SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex];
                SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex] = SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex + 1];
                SelectedAnim.OrderedImageNames[lbAnimImages.SelectedIndex + 1] = tempImageName;

                lbAnimImages.SelectedIndex++;

                SettingsChanged = true;
            }
        }

        private void btnAnimImageAdd_Click(object sender, EventArgs e)
        {
            //Add an existing image to the list
            AddImageForm form = new AddImageForm(SelectedKart);
            if (form.ShowDialog() == DialogResult.OK)
            {
                //Get the form.ImageName image
                int newIndexToAdd;
                if (lbAnimImages.SelectedIndex == -1)
                    newIndexToAdd = lbAnimImages.Items.Count;
                else
                    newIndexToAdd = lbAnimImages.SelectedIndex + 1;
                lbAnimImages.Items.Insert(newIndexToAdd, form.SelectedImage);
                SelectedAnim.OrderedImageNames.Insert(newIndexToAdd, form.SelectedImage.Name);

                SettingsChanged = true;
            }
        }

        private void btnAnimImageRemove_Click(object sender, EventArgs e)
        {
            if(SelectedAnim == null || SelectedImage == null)
                return;

            //Remove an image from the list
            int selectedIndex = lbAnimImages.SelectedIndex;
            SelectedAnim.OrderedImageNames.RemoveAt(selectedIndex);
            lbAnimImages.Items.RemoveAt(selectedIndex);
            if(lbAnimImages.Items.Count != 0)
            {
                if (selectedIndex >= lbAnimImages.Items.Count)
                    lbAnimImages.SelectedIndex = selectedIndex - 1;
                else
                    lbAnimImages.SelectedIndex = selectedIndex;
            }
            SettingsChanged = true;
        }

        private void btnAnimImageDuplicate_Click(object sender, EventArgs e)
        {
            if (SelectedAnim == null || SelectedImage == null)
                return;

            //Duplicate an image in the list
            SelectedAnim.OrderedImageNames.Insert(lbAnimImages.SelectedIndex, SelectedImage.Name);
            lbAnimImages.Items.Insert(lbAnimImages.SelectedIndex, SelectedImage);
            SettingsChanged = true;
        }

        private void cbOverlayKart_CheckedChanged(object sender, EventArgs e)
        {
            //Add the Mario overlay
            pbOverlay.Visible = cbOverlayKart.Checked;
            if (cbOverlayKart.Checked)
                UpdateOverlay();
        }

        private void btnBGColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnBGColor.BackColor = colorDialog.Color;
                pnlKartImage.BackColor = colorDialog.Color;
            }
        }

        #region AnimationType

        private void DisableCheckboxEvents()
        {
            cbAnimTurnM25.CheckedChanged -= cbAnimTurnM25_CheckedChanged;
            cbAnimTurnM19.CheckedChanged -= cbAnimTurnM19_CheckedChanged;
            cbAnimTurnM12.CheckedChanged -= cbAnimTurnM12_CheckedChanged;
            cbAnimTurnM6.CheckedChanged -= cbAnimTurnM6_CheckedChanged;
            cbAnimTurn0.CheckedChanged -= cbAnimTurn0_CheckedChanged;
            cbAnimTurn6.CheckedChanged -= cbAnimTurn6_CheckedChanged;
            cbAnimTurn12.CheckedChanged -= cbAnimTurn12_CheckedChanged;
            cbAnimTurn19.CheckedChanged -= cbAnimTurn19_CheckedChanged;
            cbAnimTurn25.CheckedChanged -= cbAnimTurn25_CheckedChanged;

            cbAnimSpinM25.CheckedChanged -= cbAnimSpinM25_CheckedChanged;
            cbAnimSpinM19.CheckedChanged -= cbAnimSpinM19_CheckedChanged;
            cbAnimSpinM12.CheckedChanged -= cbAnimSpinM12_CheckedChanged;
            cbAnimSpinM6.CheckedChanged -= cbAnimSpinM6_CheckedChanged;
            cbAnimSpin0.CheckedChanged -= cbAnimSpin0_CheckedChanged;
            cbAnimSpin6.CheckedChanged -= cbAnimSpin6_CheckedChanged;
            cbAnimSpin12.CheckedChanged -= cbAnimSpin12_CheckedChanged;
            cbAnimSpin19.CheckedChanged -= cbAnimSpin19_CheckedChanged;
            cbAnimSpin25.CheckedChanged -= cbAnimSpin25_CheckedChanged;

            cbAnimCrash.CheckedChanged -= cbAnimCrash_CheckedChanged;
        }

        private void EnableCheckboxEvents()
        {
            cbAnimTurnM25.CheckedChanged += cbAnimTurnM25_CheckedChanged;
            cbAnimTurnM19.CheckedChanged += cbAnimTurnM19_CheckedChanged;
            cbAnimTurnM12.CheckedChanged += cbAnimTurnM12_CheckedChanged;
            cbAnimTurnM6.CheckedChanged += cbAnimTurnM6_CheckedChanged;
            cbAnimTurn0.CheckedChanged += cbAnimTurn0_CheckedChanged;
            cbAnimTurn6.CheckedChanged += cbAnimTurn6_CheckedChanged;
            cbAnimTurn12.CheckedChanged += cbAnimTurn12_CheckedChanged;
            cbAnimTurn19.CheckedChanged += cbAnimTurn19_CheckedChanged;
            cbAnimTurn25.CheckedChanged += cbAnimTurn25_CheckedChanged;

            cbAnimSpinM25.CheckedChanged += cbAnimSpinM25_CheckedChanged;
            cbAnimSpinM19.CheckedChanged += cbAnimSpinM19_CheckedChanged;
            cbAnimSpinM12.CheckedChanged += cbAnimSpinM12_CheckedChanged;
            cbAnimSpinM6.CheckedChanged += cbAnimSpinM6_CheckedChanged;
            cbAnimSpin0.CheckedChanged += cbAnimSpin0_CheckedChanged;
            cbAnimSpin6.CheckedChanged += cbAnimSpin6_CheckedChanged;
            cbAnimSpin12.CheckedChanged += cbAnimSpin12_CheckedChanged;
            cbAnimSpin19.CheckedChanged += cbAnimSpin19_CheckedChanged;
            cbAnimSpin25.CheckedChanged += cbAnimSpin25_CheckedChanged;

            cbAnimCrash.CheckedChanged += cbAnimCrash_CheckedChanged;
        }

        private void ClearAnimCheckboxes()
        {
            cbAnimTurnM25.Checked = false;
            cbAnimTurnM19.Checked = false;
            cbAnimTurnM12.Checked = false;
            cbAnimTurnM6.Checked = false;
            cbAnimTurn0.Checked = false;
            cbAnimTurn6.Checked = false;
            cbAnimTurn12.Checked = false;
            cbAnimTurn19.Checked = false;
            cbAnimTurn25.Checked = false;

            cbAnimSpinM25.Checked = false;
            cbAnimSpinM19.Checked = false;
            cbAnimSpinM12.Checked = false;
            cbAnimSpinM6.Checked = false;
            cbAnimSpin0.Checked = false;
            cbAnimSpin6.Checked = false;
            cbAnimSpin12.Checked = false;
            cbAnimSpin19.Checked = false;
            cbAnimSpin25.Checked = false;

            cbAnimCrash.Checked = false;
        }

        private void cbAnimTurnM25_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown25;

            SettingsChanged = true;
        }

        private void cbAnimTurnM19_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown19;

            SettingsChanged = true;
        }

        private void cbAnimTurnM12_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12;

            SettingsChanged = true;
        }

        private void cbAnimTurnM6_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown6;

            SettingsChanged = true;
        }

        private void cbAnimTurn0_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurn0;

            SettingsChanged = true;
        }

        private void cbAnimTurn6_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp6;

            SettingsChanged = true;
        }

        private void cbAnimTurn12_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12;

            SettingsChanged = true;
        }

        private void cbAnimTurn19_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp19;

            SettingsChanged = true;
        }

        private void cbAnimTurn25_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp25;

            SettingsChanged = true;
        }

        private void cbAnimSpinM25_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25;

            SettingsChanged = true;
        }

        private void cbAnimSpinM19_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19;

            SettingsChanged = true;
        }

        private void cbAnimSpinM12_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12;

            SettingsChanged = true;
        }

        private void cbAnimSpinM6_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6;

            SettingsChanged = true;
        }

        private void cbAnimSpin0_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpin0;

            SettingsChanged = true;
        }

        private void cbAnimSpin6_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6;

            SettingsChanged = true;
        }

        private void cbAnimSpin12_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12;

            SettingsChanged = true;
        }

        private void cbAnimSpin19_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19;

            SettingsChanged = true;
        }

        private void cbAnimSpin25_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25;

            SettingsChanged = true;
        }

        private void cbAnimCrash_CheckedChanged(object sender, EventArgs e)
        {
            SelectedAnim.KartAnimationType ^= (int)KartAnimationSeries.KartAnimationTypeFlag.Crash;

            SettingsChanged = true;
        }

        #endregion

        #endregion

        #region KartInfo

        private Dictionary<KartInfo, KartInfo> KartInfoCopies = new Dictionary<KartInfo,KartInfo>();

        private void UpdateKartInfo()
        {
            lbAllKarts.Items.Clear();
            KartInfoCopies.Clear();

            foreach (KartInfo kart in MarioKart64ElementHub.Instance.Karts)
            {
                KartInfo newKart = new KartInfo(kart.KartName, kart.KartImages.ImagePalette, kart.OriginalKart);
                lbAllKarts.Items.Add(newKart);
                KartInfoCopies.Add(newKart, kart);
            }

            if (lbAllKarts.Items.Count > 0)
                lbAllKarts.SelectedIndex = 0;
        }

        private void SaveKartInfoChanges()
        {
            List<KartInfo> newKarts = new List<KartInfo>();

            for (int i = 0; i < lbAllKarts.Items.Count; i++)
            {
                KartInfo kart = (KartInfo)lbAllKarts.Items[i];

                if (KartInfoCopies.ContainsKey(kart))
                    newKarts.Add(KartInfoCopies[kart]);
                else
                    newKarts.Add(kart);
            }

            MarioKart64ElementHub.Instance.Karts.Clear();
            foreach (KartInfo kart in newKarts)
                MarioKart64ElementHub.Instance.Karts.Add(kart);

            UpdateKartInfo();
        }

        private KartInfo SelectedKartInfo
        {
            get
            {
                return (KartInfo)lbAllKarts.SelectedItem;
            }
        }

        private bool HasKartName(string newName)
        {
            foreach (object obj in lbAllKarts.Items)
            {
                KartInfo kart = (KartInfo)obj;
                if (kart.KartName == newName)
                    return true;
            }

            return false;
        }

        private void btnAddKart_Click(object sender, EventArgs e)
        {
            string newKartName = "NewKart";
            if(HasKartName(newKartName))
            {
                int newCount = 2;
                while(HasKartName(newKartName + newCount))
                    newCount++;

                newKartName += newCount;
            }
            KartInfo kart = new KartInfo(newKartName, null);
            //MarioKart64ElementHub.Instance.Karts.Add(kart);
            lbAllKarts.Items.Add(kart);
            lbAllKarts.SelectedIndex = lbAllKarts.Items.Count - 1;

            SettingsChanged = true;
        }

        private void lbAllKarts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedKartInfo == null)
                return;

            txtKartName.Text = SelectedKartInfo.KartName;

            //Disable changing name/removing for the original karts
            txtKartName.Enabled = !SelectedKartInfo.OriginalKart;
            btnRemove.Enabled = !SelectedKartInfo.OriginalKart;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (SelectedKartInfo != null && !SelectedKartInfo.OriginalKart)
            {
                if (MessageBox.Show("Are you sure you want to delete this kart?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    //MarioKart64ElementHub.Instance.Karts.Remove(SelectedKartInfo);
                    int selectedIndex = lbAllKarts.SelectedIndex;
                    lbAllKarts.Items.Remove(SelectedKartInfo);
                    if (selectedIndex < lbAllKarts.Items.Count)
                        lbAllKarts.SelectedIndex = selectedIndex;
                    else
                        lbAllKarts.SelectedIndex = selectedIndex - 1;

                    SettingsChanged = true;
                }
            }
        }

        private void txtKartName_Validating(object sender, CancelEventArgs e)
        {
            foreach (object obj in lbAllKarts.Items)
            {
                KartInfo kart = (KartInfo)obj;
                if (kart != SelectedKartInfo && txtKartName.Text == kart.KartName)
                {
                    MessageBox.Show("Kart name already exists!");
                    txtKartName.Text = SelectedKartInfo.KartName;
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void txtKartName_Validated(object sender, EventArgs e)
        {
            SelectedKartInfo.KartName = txtKartName.Text;
            lbAllKarts.Items[lbAllKarts.SelectedIndex] = SelectedKartInfo; //reset the name in the list box
            SettingsChanged = true;
        }

        private void txtKartName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                (sender as TextBox).Parent.Focus();
            }
        }

        #endregion

        #region oldCode
        /*
        private void DisplaySelectedImage()
        {
            int characterIndex = cbCharacter.SelectedIndex;
            int imageIndex = cbImageNum.SelectedIndex;

            KartAnimationSeries.KartAnimationTypeFlag animFlag = (KartAnimationSeries.KartAnimationTypeFlag)Math.Pow(2, cbAnimation.SelectedIndex);

            if (characterIndex < 0 || characterIndex >= MarioKart64ElementHub.Instance.SelectedKarts.Length)
            {
                pictureBox.Image = null;
                return;
            }

            KartAnimationSeries selectedAnim = MarioKart64ElementHub.Instance.SelectedKarts[characterIndex].KartAnimations.FirstOrDefault(f => f.KartAnimationType == (int)animFlag);

            if (selectedAnim == null)
            {
                pictureBox.Image = null;
                return;
            }

            if (imageIndex < 0 || imageIndex >= selectedAnim.OrderedImageNames.Count)
            {
                pictureBox.Image = null;
                return;
            }

            pictureBox.Image = MarioKart64ElementHub.Instance.SelectedKarts[characterIndex].KartImagePool[selectedAnim.OrderedImageNames[imageIndex]].Image;
        }

        private void cbImageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedImage();
        }

        private void cbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbImageNum.Items.Clear();

            int characterIndex = cbCharacter.SelectedIndex;
            KartAnimationSeries.KartAnimationTypeFlag animFlag = (KartAnimationSeries.KartAnimationTypeFlag)Math.Pow(2, cbAnimation.SelectedIndex);
            int animIndex = cbAnimation.SelectedIndex;

            if (characterIndex < 0 || characterIndex >= MarioKart64ElementHub.Instance.SelectedKarts.Length)
            {
                pictureBox.Image = null;
                return;
            }

            KartAnimationSeries selectedAnim = MarioKart64ElementHub.Instance.SelectedKarts[characterIndex].KartAnimations.FirstOrDefault(f => f.KartAnimationType == (int)animFlag);
            
            if(selectedAnim == null)
            {
                pictureBox.Image = null;
                return;
            }

            foreach (string str in selectedAnim.OrderedImageNames)
            {
                cbImageNum.Items.Add(str);
            }

            if(cbImageNum.Items.Count > 0)
                cbImageNum.SelectedIndex = 0;
        }

        private void cbCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Since animations are constant, no need to change it
            if (cbAnimation.SelectedIndex == 0)
                cbAnimation_SelectedIndexChanged(sender, e);
            else
                cbAnimation.SelectedIndex = 9;
        }
        */
        #endregion

        /*
        private void cbCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbImageNum.Items.Clear();

            MarioKartRomInfo.OriginalCharacters character = (MarioKartRomInfo.OriginalCharacters)cbCharacter.SelectedIndex;
            List<MIO0Block> blocks = new List<MIO0Block>();
            //switch (character)
            //{
            //    case MarioKartRomInfo.OriginalCharacters.Mario:
            //        for (int j = 0; j < _kartInfo.Mario1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Mario1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Mario2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Mario2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Luigi:
            //        for (int j = 0; j < _kartInfo.Luigi1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Luigi1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Luigi2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Luigi2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Bowser:
            //        for (int j = 0; j < _kartInfo.Bowser1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Bowser1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Bowser2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Bowser2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Toad:
            //        for (int j = 0; j < _kartInfo.Toad1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Toad1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Toad2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Toad2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Yoshi:
            //        for (int j = 0; j < _kartInfo.Yoshi1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Yoshi1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Yoshi2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Yoshi2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.DK:
            //        for (int j = 0; j < _kartInfo.DK1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.DK1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.DK2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.DK2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Peach:
            //        for (int j = 0; j < _kartInfo.Peach1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Peach1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Peach2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Peach2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Wario:
            //        for (int j = 0; j < _kartInfo.Wario1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Wario1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Wario2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Wario2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //}

            DisplaySelectedImage();



            switch (character)
            {
                case MarioKartRomInfo.OriginalCharacters.Mario:
                    for (int j = 0; j < _kartInfo.Mario1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Mario1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Mario1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Mario2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Mario2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Mario2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Luigi:
                    for (int j = 0; j < _kartInfo.Luigi1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Luigi1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Luigi1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Luigi2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Luigi2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Luigi2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Bowser:
                    for (int j = 0; j < _kartInfo.Bowser1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Bowser1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Bowser1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Bowser2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Bowser2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Bowser2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Toad:
                    for (int j = 0; j < _kartInfo.Toad1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Toad1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Toad1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Toad2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Toad2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Toad2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Yoshi:
                    for (int j = 0; j < _kartInfo.Yoshi1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Yoshi1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Yoshi1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Yoshi2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Yoshi2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Yoshi2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.DK:
                    for (int j = 0; j < _kartInfo.DK1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.DK1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.DK1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.DK2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.DK2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.DK2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Peach:
                    for (int j = 0; j < _kartInfo.Peach1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Peach1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Peach1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Peach2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Peach2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Peach2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Wario:
                    for (int j = 0; j < _kartInfo.Wario1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Wario1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Wario1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Wario2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Wario2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Wario2References[j].ReferenceElement);
                    }
                    break;
            }

            List<MIO0Block> unsortedBlocks = new List<MIO0Block>(blocks);

            blocks.Sort((b1, b2) => b1.FileOffset.CompareTo(b2.FileOffset));

            List<int> orderOfBlocks = new List<int>();

            foreach (MIO0Block block in blocks)
            {
                orderOfBlocks.Add(unsortedBlocks.IndexOf(block));
            }

            foreach (MIO0Block block in blocks)
                cbImageNum.Items.Add(((Texture)block.DecodedN64DataElement).Image);

            cbImageNum.SelectedIndex = 0;
        }
        */

    }
}
