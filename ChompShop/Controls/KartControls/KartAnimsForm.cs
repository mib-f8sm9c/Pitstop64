using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using System.Runtime.InteropServices;
using MK64Pitstop.Data.Karts;

namespace ChompShop.Controls.KartControls
{
    public partial class KartAnimsForm : ChompShopWindow
    {
        public bool _initializing;

        public KartAnimsForm(KartWrapper kart)
            : base (kart)
        {
            InitializeComponent();

            //Handle treeView checkboxes
            //treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            //treeView.DrawNode += new DrawTreeNodeEventHandler(tree_DrawNode);

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            ResetTitleText();

            kartPreviewControl.Kart = Kart.Kart;
            kartPreviewControl.FramesPerSecond = 60;
            kartPreviewControl.DisplayRefKartOption = false;

            PopulateAnimations();
            PopulateAnimationImages();
            UpdateKartPreviewAnimation();
            UpdateAnimationEnableds();

            _initializing = false;
        }

        private KartAnimationSeries SelectedAnimation
        {
            get
            {
                return (KartAnimationSeries)lbAnimations.SelectedItem;
            }
        }

        private KartImage SelectedImage
        {
            get
            {
                return (KartImage)lbAnimImages.SelectedItem;
            }
        }

        private void PopulateAnimations()
        {
            KartAnimationSeries selected = (KartAnimationSeries)lbAnimations.SelectedItem;

            lbAnimations.Items.Clear();

            for (int i = 0; i < Kart.Kart.KartAnimations.Count; i++)
            {
                lbAnimations.Items.Add(Kart.Kart.KartAnimations[i]);
            }

            if (selected != null && Kart.Kart.KartAnimations.Contains(selected))
                lbAnimations.SelectedItem = selected;
        }

        private void PopulateAnimationImages()
        {
            KartImage selected = (KartImage)lbAnimImages.SelectedItem;

            lbAnimImages.Items.Clear();

            if (SelectedAnimation == null)
                return;

            for (int i = 0; i < SelectedAnimation.OrderedImageNames.Count; i++)
            {
                KartImage image = Kart.Kart.KartImages.Images[SelectedAnimation.OrderedImageNames[i]];
                lbAnimImages.Items.Add(image);
            }

            if (selected != null && SelectedAnimation.OrderedImageNames.Contains(selected.Name))
                lbAnimImages.SelectedItem = selected;

            UpdateAnimationImagesEnableds();
        }

        private void UpdateKartPreviewAnimation()
        {
            //Full animation
            if(SelectedAnimation == null)
            {
                //All animation cycle
                kartPreviewControl.FrameIndex = 0;
                kartPreviewControl.CycleAnimations = true;
                kartPreviewControl.Mode = MK64Pitstop.Modules.Karts.KartPreviewControl.PreviewMode.Animated;
                kartPreviewControl.AnimIndex = 0;
            }
            else
            {
                kartPreviewControl.FrameIndex = 0;
                kartPreviewControl.CycleAnimations = false;
                kartPreviewControl.Mode = MK64Pitstop.Modules.Karts.KartPreviewControl.PreviewMode.Animated;
                kartPreviewControl.AnimIndex = Kart.Kart.KartAnimations.IndexOf(SelectedAnimation);
            }
        }

        private void UpdateKartPreviewImage()
        {
            //Just a single image
            if (SelectedImage == null)
            {
                UpdateKartPreviewAnimation();
            }
            else
            {
                kartPreviewControl.Mode = MK64Pitstop.Modules.Karts.KartPreviewControl.PreviewMode.Static;
                kartPreviewControl.Image = SelectedImage.Image;
            }
        }

        private void UpdateAnimationEnableds()
        {
            bool animSelected = (SelectedAnimation != null);

            pnlKartAnimation.Enabled = animSelected;

            btnAnimationsDelete.Enabled = animSelected;

            gbAnimationType.Enabled = animSelected;
        }

        private void UpdateAnimationImagesEnableds()
        {
            bool imageSelected = (SelectedImage != null);
            bool hasImages = (lbAnimImages.Items.Count > 0);

            btnAnimImageUp.Enabled = imageSelected && lbAnimImages.SelectedIndex != 0;
            btnAnimImageDown.Enabled = imageSelected && lbAnimImages.SelectedIndex != lbAnimImages.SelectedIndex - 1;

            btnAnimImageRemove.Enabled = imageSelected;
            btnAnimImageDuplicate.Enabled = imageSelected;
        }

        private void UpdateAnimationChecks()
        {
            if (SelectedAnimation == null)
                SetAnimationCheckboxes(0);
            else
                SetAnimationCheckboxes(SelectedAnimation.KartAnimationType);
        }

        private void lbAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing)
                return;

            //Populate the animation box
            PopulateAnimationImages();
            UpdateAnimationChecks();

            //Throw the animation into the kart viewer
            UpdateKartPreviewAnimation();
            UpdateAnimationEnableds();
        }

        private void btnAnimationsAdd_Click(object sender, EventArgs e)
        {
            //Add anim
            TextInputForm form = new TextInputForm("New Animation Name:", "Add New Animation");
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Kart.Kart.KartAnimations.SingleOrDefault(k => k.Name == form.TextOutput) != null)
                {
                    MessageBox.Show("Animation with the given name already exists! Please use a unique name!", "Warning");
                    return;
                }

                Kart.AddNewAnimation(form.TextOutput);
                PopulateAnimations();
                //PopulateAnimationImages();
                //UpdateKartPreviewAnimation();
                //UpdateAnimationEnableds();
            }
        }

        private void btnAnimationsDelete_Click(object sender, EventArgs e)
        {
            //Remove anim
            if (SelectedAnimation != null)
            {
                Kart.RemoveAnimation(SelectedAnimation);
                PopulateAnimations();
                //PopulateAnimationImages();
                //UpdateKartPreviewAnimation();
                //UpdateAnimationEnableds();
            }
        }

        private void btnAnimRename_Click(object sender, EventArgs e)
        {
            //Rename anim
            TextInputForm form = new TextInputForm("New Animation Name:", "Rename Animation");
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                KartAnimationSeries existingAnim = Kart.Kart.KartAnimations.SingleOrDefault(k => k.Name == form.TextOutput);

                if (existingAnim == SelectedAnimation)
                    return;

                if (existingAnim != null)
                {
                    MessageBox.Show("Animation with the given name already exists! Please use a unique name!", "Warning");
                    return;
                }

                Kart.RenameAnimation(SelectedAnimation, form.TextOutput);

                PopulateAnimations();
                //PopulateAnimationImages();
                //UpdateKartPreviewAnimation();
                //UpdateAnimationEnableds();
            }
        }

        private void btnPlayAnims_Click(object sender, EventArgs e)
        {
            lbAnimations.SelectedIndex = -1;
            UpdateKartPreviewAnimation();
        }

        private void lbAnimImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing)
                return;

            UpdateAnimationImagesEnableds();

            UpdateKartPreviewImage();
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartAnimations; } }

        protected override string TitleText { get { return "Kart Animations - {0}"; } }

        private void btnAnimImageAdd_Click(object sender, EventArgs e)
        {
            //Need to load up the images, BLEGH
            KartImageSelectForm form = new KartImageSelectForm(Kart);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int selectedIndex = lbAnimImages.SelectedIndex;

                int insertIndex;
                if (lbAnimImages.Items.Count > 0 && lbAnimImages.SelectedIndex == -1)
                    insertIndex = lbAnimImages.Items.Count;
                else
                    insertIndex = lbAnimImages.SelectedIndex + 1;

                List<KartImage> images = form.SelectedImages;
                for (int i = images.Count - 1; i >= 0; i--)
                    Kart.AddImagetoAnimation(SelectedAnimation, insertIndex, images[i]);

                //update the stuff here
                PopulateAnimationImages();
                lbAnimImages.SelectedIndex = selectedIndex;
            }
        }

        private void btnAnimImageRemove_Click(object sender, EventArgs e)
        {
            if (SelectedImage != null && SelectedAnimation != null)
            {
                int selectedIndex = lbAnimImages.SelectedIndex;
                Kart.RemoveImageFromAnimation(SelectedAnimation, selectedIndex);

                PopulateAnimationImages();
                lbAnimImages.SelectedIndex = Math.Min(selectedIndex, lbAnimImages.Items.Count - 1);
                //more?
            }
        }

        private void btnAnimImageDuplicate_Click(object sender, EventArgs e)
        {
            if (SelectedImage != null && SelectedAnimation != null)
            {
                int selectedIndex = lbAnimImages.SelectedIndex;
                Kart.DuplicateImageInAnimation(SelectedAnimation, selectedIndex);

                PopulateAnimationImages();
                lbAnimImages.SelectedIndex = selectedIndex + 1;
                //more?
            }
        }

        private void btnAnimImageUp_Click(object sender, EventArgs e)
        {
            if (SelectedImage != null && SelectedAnimation != null)
            {
                int selectedIndex = lbAnimImages.SelectedIndex;
                Kart.MoveImageUpInAnimation(SelectedAnimation, selectedIndex);

                PopulateAnimationImages();

                lbAnimImages.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnAnimImageDown_Click(object sender, EventArgs e)
        {
            if (SelectedImage != null && SelectedAnimation != null)
            {
                int selectedIndex = lbAnimImages.SelectedIndex;
                Kart.MoveImageDownInAnimation(SelectedAnimation, selectedIndex);

                PopulateAnimationImages();
                lbAnimImages.SelectedIndex = selectedIndex + 1;
            }
        }


        #region CheckboxHell

        public bool IsTurnMinus25
        {
            get
            {
                return cbAnimTurnM25.Checked;
            }
            set
            {
                cbAnimTurnM25.Checked = value;
            }
        }

        public bool IsTurnMinus19
        {
            get
            {
                return cbAnimTurnM19.Checked;
            }
            set
            {
                cbAnimTurnM19.Checked = value;
            }
        }

        public bool IsTurnMinus12
        {
            get
            {
                return cbAnimTurnM12.Checked;
            }
            set
            {
                cbAnimTurnM12.Checked = value;
            }
        }

        public bool IsTurnMinus6
        {
            get
            {
                return cbAnimTurnM6.Checked;
            }
            set
            {
                cbAnimTurnM6.Checked = value;
            }
        }

        public bool IsTurn0
        {
            get
            {
                return cbAnimTurn0.Checked;
            }
            set
            {
                cbAnimTurn0.Checked = value;
            }
        }

        public bool IsTurn6
        {
            get
            {
                return cbAnimTurn6.Checked;
            }
            set
            {
                cbAnimTurn6.Checked = value;
            }
        }

        public bool IsTurn12
        {
            get
            {
                return cbAnimTurn12.Checked;
            }
            set
            {
                cbAnimTurn12.Checked = value;
            }
        }

        public bool IsTurn19
        {
            get
            {
                return cbAnimTurn19.Checked;
            }
            set
            {
                cbAnimTurn19.Checked = value;
            }
        }

        public bool IsTurn25
        {
            get
            {
                return cbAnimTurn25.Checked;
            }
            set
            {
                cbAnimTurn25.Checked = value;
            }
        }

        public bool IsSpinMinus25
        {
            get
            {
                return cbAnimSpinM25.Checked;
            }
            set
            {
                cbAnimSpinM25.Checked = value;
            }
        }

        public bool IsSpinMinus19
        {
            get
            {
                return cbAnimSpinM19.Checked;
            }
            set
            {
                cbAnimSpinM19.Checked = value;
            }
        }

        public bool IsSpinMinus12
        {
            get
            {
                return cbAnimSpinM12.Checked;
            }
            set
            {
                cbAnimSpinM12.Checked = value;
            }
        }

        public bool IsSpinMinus6
        {
            get
            {
                return cbAnimSpinM6.Checked;
            }
            set
            {
                cbAnimSpinM6.Checked = value;
            }
        }

        public bool IsSpin0
        {
            get
            {
                return cbAnimSpin0.Checked;
            }
            set
            {
                cbAnimSpin0.Checked = value;
            }
        }

        public bool IsSpin6
        {
            get
            {
                return cbAnimSpin6.Checked;
            }
            set
            {
                cbAnimSpin6.Checked = value;
            }
        }

        public bool IsSpin12
        {
            get
            {
                return cbAnimSpin12.Checked;
            }
            set
            {
                cbAnimSpin12.Checked = value;
            }
        }

        public bool IsSpin19
        {
            get
            {
                return cbAnimSpin19.Checked;
            }
            set
            {
                cbAnimSpin19.Checked = value;
            }
        }

        public bool IsSpin25
        {
            get
            {
                return cbAnimSpin25.Checked;
            }
            set
            {
                cbAnimSpin25.Checked = value;
            }
        }

        public bool IsCrash
        {
            get
            {
                return cbAnimCrash.Checked;
            }
            set
            {
                cbAnimCrash.Checked = value;
            }
        }

        private void SetAnimationCheckboxes(int state)
        {
            cbAnimTurnM25.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown25) != 0);
            cbAnimTurnM19.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown19) != 0);
            cbAnimTurnM12.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12) != 0);
            cbAnimTurnM6.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown6) != 0);
            cbAnimTurn0.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurn0) != 0);
            cbAnimTurn6.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp6) != 0);
            cbAnimTurn12.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12) != 0);
            cbAnimTurn19.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp19) != 0);
            cbAnimTurn25.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp25) != 0);

            cbAnimSpinM25.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25) != 0);
            cbAnimSpinM19.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19) != 0);
            cbAnimSpinM12.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12) != 0);
            cbAnimSpinM6.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6) != 0);
            cbAnimSpin0.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpin0) != 0);
            cbAnimSpin6.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6) != 0);
            cbAnimSpin12.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12) != 0);
            cbAnimSpin19.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19) != 0);
            cbAnimSpin25.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25) != 0);

            cbAnimCrash.Checked = ((state & (int)KartAnimationSeries.KartAnimationTypeFlag.Crash) != 0);
        }

        private int GetAnimationCheckboxes()
        {
            int animationFlag = 0;

            animationFlag |= (cbAnimTurnM25.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown25 : 0);
            animationFlag |= (cbAnimTurnM19.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown19 : 0);
            animationFlag |= (cbAnimTurnM12.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12 : 0);
            animationFlag |= (cbAnimTurnM6.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown6 : 0);
            animationFlag |= (cbAnimTurn0.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurn0 : 0);
            animationFlag |= (cbAnimTurn6.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp6 : 0);
            animationFlag |= (cbAnimTurn12.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12 : 0);
            animationFlag |= (cbAnimTurn19.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp19 : 0);
            animationFlag |= (cbAnimTurn25.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp25 : 0);

            animationFlag |= (cbAnimSpinM25.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25 : 0);
            animationFlag |= (cbAnimSpinM19.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19 : 0);
            animationFlag |= (cbAnimSpinM12.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12 : 0);
            animationFlag |= (cbAnimSpinM6.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6 : 0);
            animationFlag |= (cbAnimSpin0.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpin0 : 0);
            animationFlag |= (cbAnimSpin6.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6 : 0);
            animationFlag |= (cbAnimSpin12.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12 : 0);
            animationFlag |= (cbAnimSpin19.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19 : 0);
            animationFlag |= (cbAnimSpin25.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25 : 0);

            animationFlag |= (cbAnimCrash.Checked ? (int)KartAnimationSeries.KartAnimationTypeFlag.Crash : 0);

            return animationFlag;
        }

        private void cbAnim_CheckedChanged(object sender, EventArgs e)
        {
            //eh what?
            if (SelectedAnimation != null)
            {
                Kart.SetAnimationType(SelectedAnimation, GetAnimationCheckboxes());

                //Don't need to update??
            }
        }

        #endregion

        #region TreeViewStuff (no longer wanted)
        /*
        Code for mixed-checked box tree views, taken from http://stackoverflow.com/questions/698369/how-to-disable-a-winforms-treeview-node-checkbox

        // constants used to hide a checkbox
        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;
        public const int TV_FIRST = 0x1100;
        public const int TVM_SETITEM = TV_FIRST + 63;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam,
        IntPtr lParam);

        // struct used to set node properties
        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;

        }
        
        //More tree view stuff
        void tree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Tag != null && ((string)e.Node.Tag).StartsWith("N"))
            {
                HideCheckBox(e.Node);
                e.DrawDefault = true;
            }
            else
            {
                e.Graphics.DrawString(e.Node.Text, e.Node.TreeView.Font,
                   Brushes.Black, e.Node.Bounds.X, e.Node.Bounds.Y);
            }
        }

        private void HideCheckBox(TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvi));
            Marshal.StructureToPtr(tvi, lparam, false);
            SendMessage(node.TreeView.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
        }

        #region Checkbox Accessors
        
        public bool TurnMinus25Checked
        {
            get
            {
                return treeView.Nodes["nTm25"].Checked;
            }
            set
            {
                treeView.Nodes["nTm25"].Checked = value;
            }
        }

        public bool TurnMinus19Checked
        {
            get
            {
                return treeView.Nodes["nTm19"].Checked;
            }
            set
            {
                treeView.Nodes["nTm19"].Checked = value;
            }
        }

        public bool TurnMinus12Checked
        {
            get
            {
                return treeView.Nodes["nTm12"].Checked;
            }
            set
            {
                treeView.Nodes["nTm12"].Checked = value;
            }
        }

        public bool TurnMinus6Checked
        {
            get
            {
                return treeView.Nodes["nTm6"].Checked;
            }
            set
            {
                treeView.Nodes["nTm6"].Checked = value;
            }
        }

        public bool Turn0Checked
        {
            get
            {
                return treeView.Nodes["nT0"].Checked;
            }
            set
            {
                treeView.Nodes["nT0"].Checked = value;
            }
        }

        public bool Turn6Checked
        {
            get
            {
                return treeView.Nodes["nT6"].Checked;
            }
            set
            {
                treeView.Nodes["nT6"].Checked = value;
            }
        }

        public bool Turn12Checked
        {
            get
            {
                return treeView.Nodes["nT12"].Checked;
            }
            set
            {
                treeView.Nodes["nT12"].Checked = value;
            }
        }

        public bool Turn19Checked
        {
            get
            {
                return treeView.Nodes["nT19"].Checked;
            }
            set
            {
                treeView.Nodes["nT19"].Checked = value;
            }
        }

        public bool Turn25Checked
        {
            get
            {
                return treeView.Nodes["nT25"].Checked;
            }
            set
            {
                treeView.Nodes["nT25"].Checked = value;
            }
        }

        public bool SpinMinus25Checked
        {
            get
            {
                return treeView.Nodes["nSm25"].Checked;
            }
            set
            {
                treeView.Nodes["nSm25"].Checked = value;
            }
        }

        public bool SpinMinus19Checked
        {
            get
            {
                return treeView.Nodes["nSm19"].Checked;
            }
            set
            {
                treeView.Nodes["nSm19"].Checked = value;
            }
        }

        public bool SpinMinus12Checked
        {
            get
            {
                return treeView.Nodes["nSm12"].Checked;
            }
            set
            {
                treeView.Nodes["nSm12"].Checked = value;
            }
        }

        public bool SpinMinus6Checked
        {
            get
            {
                return treeView.Nodes["nSm6"].Checked;
            }
            set
            {
                treeView.Nodes["nSm6"].Checked = value;
            }
        }

        public bool Spin0Checked
        {
            get
            {
                return treeView.Nodes["nS0"].Checked;
            }
            set
            {
                treeView.Nodes["nS0"].Checked = value;
            }
        }

        public bool Spin6Checked
        {
            get
            {
                return treeView.Nodes["nS6"].Checked;
            }
            set
            {
                treeView.Nodes["nS6"].Checked = value;
            }
        }

        public bool Spin12Checked
        {
            get
            {
                return treeView.Nodes["nS12"].Checked;
            }
            set
            {
                treeView.Nodes["nS12"].Checked = value;
            }
        }

        public bool Spin19Checked
        {
            get
            {
                return treeView.Nodes["nS19"].Checked;
            }
            set
            {
                treeView.Nodes["nS19"].Checked = value;
            }
        }

        public bool Spin25Checked
        {
            get
            {
                return treeView.Nodes["nS25"].Checked;
            }
            set
            {
                treeView.Nodes["nS25"].Checked = value;
            }
        }

        public bool CrashChecked
        {
            get
            {
                return treeView.Nodes["nCrash"].Checked;
            }
            set
            {
                treeView.Nodes["nCrash"].Checked = value;
            }
        }

        #endregion

         */
        #endregion
    }
}
