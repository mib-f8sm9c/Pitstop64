namespace ChompShop.Controls.KartControls
{
    partial class KartAnimsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KartAnimsForm));
            this.kartPreviewControl = new Pitstop64.Modules.Karts.KartPreviewControl();
            this.pnlKartAnimation = new System.Windows.Forms.Panel();
            this.lbAnimImages = new System.Windows.Forms.ListBox();
            this.gbAnimationType = new System.Windows.Forms.GroupBox();
            this.gbSpin = new System.Windows.Forms.GroupBox();
            this.cbAnimSpinM25 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpinM19 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpinM12 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpin25 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpinM6 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpin19 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpin0 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpin12 = new System.Windows.Forms.CheckBox();
            this.cbAnimSpin6 = new System.Windows.Forms.CheckBox();
            this.gbTurn = new System.Windows.Forms.GroupBox();
            this.cbAnimTurnM25 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurnM19 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurnM12 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurnM6 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurn0 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurn6 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurn12 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurn19 = new System.Windows.Forms.CheckBox();
            this.cbAnimTurn25 = new System.Windows.Forms.CheckBox();
            this.cbAnimCrash = new System.Windows.Forms.CheckBox();
            this.lbAnimations = new System.Windows.Forms.ListBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlLeftSide = new System.Windows.Forms.Panel();
            this.pnlReloadAnimations = new System.Windows.Forms.Panel();
            this.btnRecalculate = new System.Windows.Forms.Button();
            this.lblWarningText = new System.Windows.Forms.Label();
            this.lblReloadWarning = new System.Windows.Forms.Label();
            this.btnAnimImageDown = new System.Windows.Forms.Button();
            this.btnAnimImageUp = new System.Windows.Forms.Button();
            this.btnAnimImageDuplicate = new System.Windows.Forms.Button();
            this.btnAnimImageRemove = new System.Windows.Forms.Button();
            this.btnAnimImageAdd = new System.Windows.Forms.Button();
            this.btnDefaultAnims = new System.Windows.Forms.Button();
            this.btnAnimRename = new System.Windows.Forms.Button();
            this.btnPlayAnims = new System.Windows.Forms.Button();
            this.btnAnimationsDelete = new System.Windows.Forms.Button();
            this.btnAnimationsAdd = new System.Windows.Forms.Button();
            this.pnlKartAnimation.SuspendLayout();
            this.gbAnimationType.SuspendLayout();
            this.gbSpin.SuspendLayout();
            this.gbTurn.SuspendLayout();
            this.pnlLeftSide.SuspendLayout();
            this.pnlReloadAnimations.SuspendLayout();
            this.SuspendLayout();
            // 
            // kartPreviewControl
            // 
            this.kartPreviewControl.AnimIndex = 0;
            this.kartPreviewControl.CycleAnimations = false;
            this.kartPreviewControl.DisplayRefKartOption = true;
            this.kartPreviewControl.ExportButtonVisible = true;
            this.kartPreviewControl.FrameIndex = 0;
            this.kartPreviewControl.FramesPerSecond = 30;
            this.kartPreviewControl.Image = null;
            this.kartPreviewControl.ImageName = "";
            this.kartPreviewControl.ImageSize = new System.Drawing.Size(133, 140);
            this.kartPreviewControl.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.kartPreviewControl.Kart = null;
            this.kartPreviewControl.Location = new System.Drawing.Point(3, 4);
            this.kartPreviewControl.LockImageSize = true;
            this.kartPreviewControl.Mode = Pitstop64.Modules.Karts.KartPreviewControl.PreviewMode.Static;
            this.kartPreviewControl.Name = "kartPreviewControl";
            this.kartPreviewControl.OverlayImage = null;
            this.kartPreviewControl.ReferenceKart = null;
            this.kartPreviewControl.ShowReferenceKart = false;
            this.kartPreviewControl.Size = new System.Drawing.Size(175, 165);
            this.kartPreviewControl.TabIndex = 5;
            this.kartPreviewControl.UseAnimPalettes = false;
            // 
            // pnlKartAnimation
            // 
            this.pnlKartAnimation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlKartAnimation.Controls.Add(this.btnAnimImageDown);
            this.pnlKartAnimation.Controls.Add(this.btnAnimImageUp);
            this.pnlKartAnimation.Controls.Add(this.btnAnimImageDuplicate);
            this.pnlKartAnimation.Controls.Add(this.btnAnimImageRemove);
            this.pnlKartAnimation.Controls.Add(this.btnAnimImageAdd);
            this.pnlKartAnimation.Controls.Add(this.lbAnimImages);
            this.pnlKartAnimation.Controls.Add(this.gbAnimationType);
            this.pnlKartAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKartAnimation.Location = new System.Drawing.Point(186, 5);
            this.pnlKartAnimation.Name = "pnlKartAnimation";
            this.pnlKartAnimation.Size = new System.Drawing.Size(489, 309);
            this.pnlKartAnimation.TabIndex = 1;
            // 
            // lbAnimImages
            // 
            this.lbAnimImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbAnimImages.FormattingEnabled = true;
            this.lbAnimImages.ItemHeight = 16;
            this.lbAnimImages.Location = new System.Drawing.Point(14, 37);
            this.lbAnimImages.Name = "lbAnimImages";
            this.lbAnimImages.ScrollAlwaysVisible = true;
            this.lbAnimImages.Size = new System.Drawing.Size(166, 244);
            this.lbAnimImages.TabIndex = 0;
            this.lbAnimImages.SelectedIndexChanged += new System.EventHandler(this.lbAnimImages_SelectedIndexChanged);
            // 
            // gbAnimationType
            // 
            this.gbAnimationType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAnimationType.Controls.Add(this.gbSpin);
            this.gbAnimationType.Controls.Add(this.gbTurn);
            this.gbAnimationType.Controls.Add(this.cbAnimCrash);
            this.gbAnimationType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAnimationType.Location = new System.Drawing.Point(196, 8);
            this.gbAnimationType.Name = "gbAnimationType";
            this.gbAnimationType.Size = new System.Drawing.Size(273, 283);
            this.gbAnimationType.TabIndex = 6;
            this.gbAnimationType.TabStop = false;
            this.gbAnimationType.Text = "Animation Type";
            // 
            // gbSpin
            // 
            this.gbSpin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSpin.Controls.Add(this.cbAnimSpinM25);
            this.gbSpin.Controls.Add(this.cbAnimSpinM19);
            this.gbSpin.Controls.Add(this.cbAnimSpinM12);
            this.gbSpin.Controls.Add(this.cbAnimSpin25);
            this.gbSpin.Controls.Add(this.cbAnimSpinM6);
            this.gbSpin.Controls.Add(this.cbAnimSpin19);
            this.gbSpin.Controls.Add(this.cbAnimSpin0);
            this.gbSpin.Controls.Add(this.cbAnimSpin12);
            this.gbSpin.Controls.Add(this.cbAnimSpin6);
            this.gbSpin.Location = new System.Drawing.Point(6, 131);
            this.gbSpin.Name = "gbSpin";
            this.gbSpin.Size = new System.Drawing.Size(257, 102);
            this.gbSpin.TabIndex = 1;
            this.gbSpin.TabStop = false;
            this.gbSpin.Text = "Spin Animation";
            // 
            // cbAnimSpinM25
            // 
            this.cbAnimSpinM25.AutoSize = true;
            this.cbAnimSpinM25.Location = new System.Drawing.Point(6, 21);
            this.cbAnimSpinM25.Name = "cbAnimSpinM25";
            this.cbAnimSpinM25.Size = new System.Drawing.Size(79, 20);
            this.cbAnimSpinM25.TabIndex = 0;
            this.cbAnimSpinM25.Text = "Spin -25°";
            this.cbAnimSpinM25.UseVisualStyleBackColor = true;
            this.cbAnimSpinM25.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpinM19
            // 
            this.cbAnimSpinM19.AutoSize = true;
            this.cbAnimSpinM19.Location = new System.Drawing.Point(87, 21);
            this.cbAnimSpinM19.Name = "cbAnimSpinM19";
            this.cbAnimSpinM19.Size = new System.Drawing.Size(79, 20);
            this.cbAnimSpinM19.TabIndex = 1;
            this.cbAnimSpinM19.Text = "Spin -19°";
            this.cbAnimSpinM19.UseVisualStyleBackColor = true;
            this.cbAnimSpinM19.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpinM12
            // 
            this.cbAnimSpinM12.AutoSize = true;
            this.cbAnimSpinM12.Location = new System.Drawing.Point(167, 21);
            this.cbAnimSpinM12.Name = "cbAnimSpinM12";
            this.cbAnimSpinM12.Size = new System.Drawing.Size(79, 20);
            this.cbAnimSpinM12.TabIndex = 2;
            this.cbAnimSpinM12.Text = "Spin -12°";
            this.cbAnimSpinM12.UseVisualStyleBackColor = true;
            this.cbAnimSpinM12.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpin25
            // 
            this.cbAnimSpin25.AutoSize = true;
            this.cbAnimSpin25.Location = new System.Drawing.Point(167, 71);
            this.cbAnimSpin25.Name = "cbAnimSpin25";
            this.cbAnimSpin25.Size = new System.Drawing.Size(75, 20);
            this.cbAnimSpin25.TabIndex = 8;
            this.cbAnimSpin25.Text = "Spin 25°";
            this.cbAnimSpin25.UseVisualStyleBackColor = true;
            this.cbAnimSpin25.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpinM6
            // 
            this.cbAnimSpinM6.AutoSize = true;
            this.cbAnimSpinM6.Location = new System.Drawing.Point(6, 46);
            this.cbAnimSpinM6.Name = "cbAnimSpinM6";
            this.cbAnimSpinM6.Size = new System.Drawing.Size(72, 20);
            this.cbAnimSpinM6.TabIndex = 3;
            this.cbAnimSpinM6.Text = "Spin -6°";
            this.cbAnimSpinM6.UseVisualStyleBackColor = true;
            this.cbAnimSpinM6.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpin19
            // 
            this.cbAnimSpin19.AutoSize = true;
            this.cbAnimSpin19.Location = new System.Drawing.Point(87, 71);
            this.cbAnimSpin19.Name = "cbAnimSpin19";
            this.cbAnimSpin19.Size = new System.Drawing.Size(75, 20);
            this.cbAnimSpin19.TabIndex = 7;
            this.cbAnimSpin19.Text = "Spin 19°";
            this.cbAnimSpin19.UseVisualStyleBackColor = true;
            this.cbAnimSpin19.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpin0
            // 
            this.cbAnimSpin0.AutoSize = true;
            this.cbAnimSpin0.Location = new System.Drawing.Point(87, 46);
            this.cbAnimSpin0.Name = "cbAnimSpin0";
            this.cbAnimSpin0.Size = new System.Drawing.Size(68, 20);
            this.cbAnimSpin0.TabIndex = 4;
            this.cbAnimSpin0.Text = "Spin 0°";
            this.cbAnimSpin0.UseVisualStyleBackColor = true;
            this.cbAnimSpin0.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpin12
            // 
            this.cbAnimSpin12.AutoSize = true;
            this.cbAnimSpin12.Location = new System.Drawing.Point(6, 71);
            this.cbAnimSpin12.Name = "cbAnimSpin12";
            this.cbAnimSpin12.Size = new System.Drawing.Size(75, 20);
            this.cbAnimSpin12.TabIndex = 6;
            this.cbAnimSpin12.Text = "Spin 12°";
            this.cbAnimSpin12.UseVisualStyleBackColor = true;
            this.cbAnimSpin12.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimSpin6
            // 
            this.cbAnimSpin6.AutoSize = true;
            this.cbAnimSpin6.Location = new System.Drawing.Point(167, 46);
            this.cbAnimSpin6.Name = "cbAnimSpin6";
            this.cbAnimSpin6.Size = new System.Drawing.Size(68, 20);
            this.cbAnimSpin6.TabIndex = 5;
            this.cbAnimSpin6.Text = "Spin 6°";
            this.cbAnimSpin6.UseVisualStyleBackColor = true;
            this.cbAnimSpin6.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // gbTurn
            // 
            this.gbTurn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTurn.Controls.Add(this.cbAnimTurnM25);
            this.gbTurn.Controls.Add(this.cbAnimTurnM19);
            this.gbTurn.Controls.Add(this.cbAnimTurnM12);
            this.gbTurn.Controls.Add(this.cbAnimTurnM6);
            this.gbTurn.Controls.Add(this.cbAnimTurn0);
            this.gbTurn.Controls.Add(this.cbAnimTurn6);
            this.gbTurn.Controls.Add(this.cbAnimTurn12);
            this.gbTurn.Controls.Add(this.cbAnimTurn19);
            this.gbTurn.Controls.Add(this.cbAnimTurn25);
            this.gbTurn.Location = new System.Drawing.Point(6, 25);
            this.gbTurn.Name = "gbTurn";
            this.gbTurn.Size = new System.Drawing.Size(257, 92);
            this.gbTurn.TabIndex = 0;
            this.gbTurn.TabStop = false;
            this.gbTurn.Text = "Turn Animation";
            // 
            // cbAnimTurnM25
            // 
            this.cbAnimTurnM25.AutoSize = true;
            this.cbAnimTurnM25.Location = new System.Drawing.Point(6, 20);
            this.cbAnimTurnM25.Name = "cbAnimTurnM25";
            this.cbAnimTurnM25.Size = new System.Drawing.Size(79, 20);
            this.cbAnimTurnM25.TabIndex = 0;
            this.cbAnimTurnM25.Text = "Turn -25°";
            this.cbAnimTurnM25.UseVisualStyleBackColor = true;
            this.cbAnimTurnM25.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurnM19
            // 
            this.cbAnimTurnM19.AutoSize = true;
            this.cbAnimTurnM19.Location = new System.Drawing.Point(87, 20);
            this.cbAnimTurnM19.Name = "cbAnimTurnM19";
            this.cbAnimTurnM19.Size = new System.Drawing.Size(79, 20);
            this.cbAnimTurnM19.TabIndex = 1;
            this.cbAnimTurnM19.Text = "Turn -19°";
            this.cbAnimTurnM19.UseVisualStyleBackColor = true;
            this.cbAnimTurnM19.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurnM12
            // 
            this.cbAnimTurnM12.AutoSize = true;
            this.cbAnimTurnM12.Location = new System.Drawing.Point(167, 20);
            this.cbAnimTurnM12.Name = "cbAnimTurnM12";
            this.cbAnimTurnM12.Size = new System.Drawing.Size(79, 20);
            this.cbAnimTurnM12.TabIndex = 2;
            this.cbAnimTurnM12.Text = "Turn -12°";
            this.cbAnimTurnM12.UseVisualStyleBackColor = true;
            this.cbAnimTurnM12.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurnM6
            // 
            this.cbAnimTurnM6.AutoSize = true;
            this.cbAnimTurnM6.Location = new System.Drawing.Point(6, 45);
            this.cbAnimTurnM6.Name = "cbAnimTurnM6";
            this.cbAnimTurnM6.Size = new System.Drawing.Size(72, 20);
            this.cbAnimTurnM6.TabIndex = 3;
            this.cbAnimTurnM6.Text = "Turn -6°";
            this.cbAnimTurnM6.UseVisualStyleBackColor = true;
            this.cbAnimTurnM6.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurn0
            // 
            this.cbAnimTurn0.AutoSize = true;
            this.cbAnimTurn0.Location = new System.Drawing.Point(87, 45);
            this.cbAnimTurn0.Name = "cbAnimTurn0";
            this.cbAnimTurn0.Size = new System.Drawing.Size(68, 20);
            this.cbAnimTurn0.TabIndex = 4;
            this.cbAnimTurn0.Text = "Turn 0°";
            this.cbAnimTurn0.UseVisualStyleBackColor = true;
            this.cbAnimTurn0.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurn6
            // 
            this.cbAnimTurn6.AutoSize = true;
            this.cbAnimTurn6.Location = new System.Drawing.Point(167, 45);
            this.cbAnimTurn6.Name = "cbAnimTurn6";
            this.cbAnimTurn6.Size = new System.Drawing.Size(68, 20);
            this.cbAnimTurn6.TabIndex = 5;
            this.cbAnimTurn6.Text = "Turn 6°";
            this.cbAnimTurn6.UseVisualStyleBackColor = true;
            this.cbAnimTurn6.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurn12
            // 
            this.cbAnimTurn12.AutoSize = true;
            this.cbAnimTurn12.Location = new System.Drawing.Point(6, 70);
            this.cbAnimTurn12.Name = "cbAnimTurn12";
            this.cbAnimTurn12.Size = new System.Drawing.Size(75, 20);
            this.cbAnimTurn12.TabIndex = 6;
            this.cbAnimTurn12.Text = "Turn 12°";
            this.cbAnimTurn12.UseVisualStyleBackColor = true;
            this.cbAnimTurn12.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurn19
            // 
            this.cbAnimTurn19.AutoSize = true;
            this.cbAnimTurn19.Location = new System.Drawing.Point(87, 70);
            this.cbAnimTurn19.Name = "cbAnimTurn19";
            this.cbAnimTurn19.Size = new System.Drawing.Size(75, 20);
            this.cbAnimTurn19.TabIndex = 7;
            this.cbAnimTurn19.Text = "Turn 19°";
            this.cbAnimTurn19.UseVisualStyleBackColor = true;
            this.cbAnimTurn19.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimTurn25
            // 
            this.cbAnimTurn25.AutoSize = true;
            this.cbAnimTurn25.Location = new System.Drawing.Point(167, 70);
            this.cbAnimTurn25.Name = "cbAnimTurn25";
            this.cbAnimTurn25.Size = new System.Drawing.Size(75, 20);
            this.cbAnimTurn25.TabIndex = 8;
            this.cbAnimTurn25.Text = "Turn 25°";
            this.cbAnimTurn25.UseVisualStyleBackColor = true;
            this.cbAnimTurn25.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // cbAnimCrash
            // 
            this.cbAnimCrash.AutoSize = true;
            this.cbAnimCrash.Location = new System.Drawing.Point(62, 248);
            this.cbAnimCrash.Name = "cbAnimCrash";
            this.cbAnimCrash.Size = new System.Drawing.Size(124, 20);
            this.cbAnimCrash.TabIndex = 2;
            this.cbAnimCrash.Text = "Crash Animation";
            this.cbAnimCrash.UseVisualStyleBackColor = true;
            this.cbAnimCrash.CheckedChanged += new System.EventHandler(this.cbAnim_CheckedChanged);
            // 
            // lbAnimations
            // 
            this.lbAnimations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAnimations.FormattingEnabled = true;
            this.lbAnimations.ItemHeight = 16;
            this.lbAnimations.Location = new System.Drawing.Point(8, 184);
            this.lbAnimations.Name = "lbAnimations";
            this.lbAnimations.ScrollAlwaysVisible = true;
            this.lbAnimations.Size = new System.Drawing.Size(163, 100);
            this.lbAnimations.TabIndex = 0;
            this.lbAnimations.SelectedIndexChanged += new System.EventHandler(this.lbAnimations_SelectedIndexChanged);
            // 
            // pnlLeftSide
            // 
            this.pnlLeftSide.Controls.Add(this.btnDefaultAnims);
            this.pnlLeftSide.Controls.Add(this.btnAnimRename);
            this.pnlLeftSide.Controls.Add(this.btnPlayAnims);
            this.pnlLeftSide.Controls.Add(this.lbAnimations);
            this.pnlLeftSide.Controls.Add(this.btnAnimationsDelete);
            this.pnlLeftSide.Controls.Add(this.btnAnimationsAdd);
            this.pnlLeftSide.Controls.Add(this.kartPreviewControl);
            this.pnlLeftSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftSide.Location = new System.Drawing.Point(5, 5);
            this.pnlLeftSide.Name = "pnlLeftSide";
            this.pnlLeftSide.Size = new System.Drawing.Size(181, 309);
            this.pnlLeftSide.TabIndex = 0;
            // 
            // pnlReloadAnimations
            // 
            this.pnlReloadAnimations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReloadAnimations.Controls.Add(this.btnRecalculate);
            this.pnlReloadAnimations.Controls.Add(this.lblWarningText);
            this.pnlReloadAnimations.Controls.Add(this.lblReloadWarning);
            this.pnlReloadAnimations.Location = new System.Drawing.Point(5, 5);
            this.pnlReloadAnimations.Name = "pnlReloadAnimations";
            this.pnlReloadAnimations.Size = new System.Drawing.Size(670, 309);
            this.pnlReloadAnimations.TabIndex = 19;
            this.pnlReloadAnimations.Visible = false;
            // 
            // btnRecalculate
            // 
            this.btnRecalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecalculate.Location = new System.Drawing.Point(256, 227);
            this.btnRecalculate.Name = "btnRecalculate";
            this.btnRecalculate.Size = new System.Drawing.Size(155, 51);
            this.btnRecalculate.TabIndex = 1;
            this.btnRecalculate.Text = "Recalculate Animations";
            this.btnRecalculate.UseVisualStyleBackColor = true;
            this.btnRecalculate.Click += new System.EventHandler(this.btnRecalculate_Click);
            // 
            // lblWarningText
            // 
            this.lblWarningText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWarningText.Location = new System.Drawing.Point(105, 103);
            this.lblWarningText.Name = "lblWarningText";
            this.lblWarningText.Size = new System.Drawing.Size(458, 95);
            this.lblWarningText.TabIndex = 1;
            this.lblWarningText.Text = resources.GetString("lblWarningText.Text");
            this.lblWarningText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReloadWarning
            // 
            this.lblReloadWarning.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblReloadWarning.AutoSize = true;
            this.lblReloadWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReloadWarning.Location = new System.Drawing.Point(275, 39);
            this.lblReloadWarning.Name = "lblReloadWarning";
            this.lblReloadWarning.Size = new System.Drawing.Size(109, 29);
            this.lblReloadWarning.TabIndex = 0;
            this.lblReloadWarning.Text = "Warning";
            // 
            // btnAnimImageDown
            // 
            this.btnAnimImageDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimImageDown.Image = global::ChompShop.Properties.Resources.arrow_thick_bottom;
            this.btnAnimImageDown.Location = new System.Drawing.Point(160, 10);
            this.btnAnimImageDown.Name = "btnAnimImageDown";
            this.btnAnimImageDown.Size = new System.Drawing.Size(20, 20);
            this.btnAnimImageDown.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnAnimImageDown, "Move Kart Image down");
            this.btnAnimImageDown.UseVisualStyleBackColor = true;
            this.btnAnimImageDown.Click += new System.EventHandler(this.btnAnimImageDown_Click);
            // 
            // btnAnimImageUp
            // 
            this.btnAnimImageUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimImageUp.Image = global::ChompShop.Properties.Resources.arrow_thick_top;
            this.btnAnimImageUp.Location = new System.Drawing.Point(134, 10);
            this.btnAnimImageUp.Name = "btnAnimImageUp";
            this.btnAnimImageUp.Size = new System.Drawing.Size(20, 20);
            this.btnAnimImageUp.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnAnimImageUp, "Move Kart Image up");
            this.btnAnimImageUp.UseVisualStyleBackColor = true;
            this.btnAnimImageUp.Click += new System.EventHandler(this.btnAnimImageUp_Click);
            // 
            // btnAnimImageDuplicate
            // 
            this.btnAnimImageDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimImageDuplicate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnAnimImageDuplicate.Image = global::ChompShop.Properties.Resources.clipboard;
            this.btnAnimImageDuplicate.Location = new System.Drawing.Point(70, 10);
            this.btnAnimImageDuplicate.Name = "btnAnimImageDuplicate";
            this.btnAnimImageDuplicate.Size = new System.Drawing.Size(20, 20);
            this.btnAnimImageDuplicate.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnAnimImageDuplicate, "Duplicate selected Kart Image");
            this.btnAnimImageDuplicate.UseVisualStyleBackColor = true;
            this.btnAnimImageDuplicate.Click += new System.EventHandler(this.btnAnimImageDuplicate_Click);
            // 
            // btnAnimImageRemove
            // 
            this.btnAnimImageRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimImageRemove.Image = global::ChompShop.Properties.Resources.minus;
            this.btnAnimImageRemove.Location = new System.Drawing.Point(44, 10);
            this.btnAnimImageRemove.Name = "btnAnimImageRemove";
            this.btnAnimImageRemove.Size = new System.Drawing.Size(20, 20);
            this.btnAnimImageRemove.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnAnimImageRemove, "Remove selected Kart Image from the animation");
            this.btnAnimImageRemove.UseVisualStyleBackColor = true;
            this.btnAnimImageRemove.Click += new System.EventHandler(this.btnAnimImageRemove_Click);
            // 
            // btnAnimImageAdd
            // 
            this.btnAnimImageAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimImageAdd.Image = global::ChompShop.Properties.Resources.plus;
            this.btnAnimImageAdd.Location = new System.Drawing.Point(18, 10);
            this.btnAnimImageAdd.Name = "btnAnimImageAdd";
            this.btnAnimImageAdd.Size = new System.Drawing.Size(20, 20);
            this.btnAnimImageAdd.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnAnimImageAdd, "Add existing Kart Image to the animation");
            this.btnAnimImageAdd.UseVisualStyleBackColor = true;
            this.btnAnimImageAdd.Click += new System.EventHandler(this.btnAnimImageAdd_Click);
            // 
            // btnDefaultAnims
            // 
            this.btnDefaultAnims.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultAnims.Image = global::ChompShop.Properties.Resources.bolt;
            this.btnDefaultAnims.Location = new System.Drawing.Point(86, 158);
            this.btnDefaultAnims.Name = "btnDefaultAnims";
            this.btnDefaultAnims.Size = new System.Drawing.Size(20, 20);
            this.btnDefaultAnims.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnDefaultAnims, "Set Up Default Animations");
            this.btnDefaultAnims.UseVisualStyleBackColor = true;
            this.btnDefaultAnims.Click += new System.EventHandler(this.btnDefaultAnims_Click);
            // 
            // btnAnimRename
            // 
            this.btnAnimRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimRename.Image = global::ChompShop.Properties.Resources.pencil;
            this.btnAnimRename.Location = new System.Drawing.Point(60, 158);
            this.btnAnimRename.Name = "btnAnimRename";
            this.btnAnimRename.Size = new System.Drawing.Size(20, 20);
            this.btnAnimRename.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnAnimRename, "Rename selected animation");
            this.btnAnimRename.UseVisualStyleBackColor = true;
            this.btnAnimRename.Click += new System.EventHandler(this.btnAnimRename_Click);
            // 
            // btnPlayAnims
            // 
            this.btnPlayAnims.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayAnims.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayAnims.Image = global::ChompShop.Properties.Resources.play_circle;
            this.btnPlayAnims.Location = new System.Drawing.Point(152, 158);
            this.btnPlayAnims.Name = "btnPlayAnims";
            this.btnPlayAnims.Size = new System.Drawing.Size(20, 20);
            this.btnPlayAnims.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnPlayAnims, "Play all animations");
            this.btnPlayAnims.UseVisualStyleBackColor = true;
            this.btnPlayAnims.Click += new System.EventHandler(this.btnPlayAnims_Click);
            // 
            // btnAnimationsDelete
            // 
            this.btnAnimationsDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimationsDelete.Image = global::ChompShop.Properties.Resources.minus;
            this.btnAnimationsDelete.Location = new System.Drawing.Point(34, 158);
            this.btnAnimationsDelete.Name = "btnAnimationsDelete";
            this.btnAnimationsDelete.Size = new System.Drawing.Size(20, 20);
            this.btnAnimationsDelete.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnAnimationsDelete, "Remove selected animation");
            this.btnAnimationsDelete.UseVisualStyleBackColor = true;
            this.btnAnimationsDelete.Click += new System.EventHandler(this.btnAnimationsDelete_Click);
            // 
            // btnAnimationsAdd
            // 
            this.btnAnimationsAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimationsAdd.Image = global::ChompShop.Properties.Resources.plus;
            this.btnAnimationsAdd.Location = new System.Drawing.Point(8, 158);
            this.btnAnimationsAdd.Name = "btnAnimationsAdd";
            this.btnAnimationsAdd.Size = new System.Drawing.Size(20, 20);
            this.btnAnimationsAdd.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnAnimationsAdd, "Add new animation");
            this.btnAnimationsAdd.UseVisualStyleBackColor = true;
            this.btnAnimationsAdd.Click += new System.EventHandler(this.btnAnimationsAdd_Click);
            // 
            // KartAnimsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 319);
            this.Controls.Add(this.pnlKartAnimation);
            this.Controls.Add(this.pnlLeftSide);
            this.Controls.Add(this.pnlReloadAnimations);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartAnimsForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Kart Animations";
            this.pnlKartAnimation.ResumeLayout(false);
            this.gbAnimationType.ResumeLayout(false);
            this.gbAnimationType.PerformLayout();
            this.gbSpin.ResumeLayout(false);
            this.gbSpin.PerformLayout();
            this.gbTurn.ResumeLayout(false);
            this.gbTurn.PerformLayout();
            this.pnlLeftSide.ResumeLayout(false);
            this.pnlReloadAnimations.ResumeLayout(false);
            this.pnlReloadAnimations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Pitstop64.Modules.Karts.KartPreviewControl kartPreviewControl;
        private System.Windows.Forms.Panel pnlKartAnimation;
        private System.Windows.Forms.Button btnAnimImageDown;
        private System.Windows.Forms.Button btnAnimImageUp;
        private System.Windows.Forms.Button btnAnimImageDuplicate;
        private System.Windows.Forms.Button btnAnimImageRemove;
        private System.Windows.Forms.Button btnAnimImageAdd;
        private System.Windows.Forms.ListBox lbAnimImages;
        private System.Windows.Forms.GroupBox gbAnimationType;
        private System.Windows.Forms.CheckBox cbAnimCrash;
        private System.Windows.Forms.CheckBox cbAnimSpin25;
        private System.Windows.Forms.CheckBox cbAnimSpin19;
        private System.Windows.Forms.CheckBox cbAnimSpin12;
        private System.Windows.Forms.CheckBox cbAnimSpin6;
        private System.Windows.Forms.CheckBox cbAnimSpin0;
        private System.Windows.Forms.CheckBox cbAnimSpinM6;
        private System.Windows.Forms.CheckBox cbAnimSpinM12;
        private System.Windows.Forms.CheckBox cbAnimSpinM19;
        private System.Windows.Forms.CheckBox cbAnimSpinM25;
        private System.Windows.Forms.CheckBox cbAnimTurn25;
        private System.Windows.Forms.CheckBox cbAnimTurn19;
        private System.Windows.Forms.CheckBox cbAnimTurn12;
        private System.Windows.Forms.CheckBox cbAnimTurn6;
        private System.Windows.Forms.CheckBox cbAnimTurn0;
        private System.Windows.Forms.CheckBox cbAnimTurnM6;
        private System.Windows.Forms.CheckBox cbAnimTurnM12;
        private System.Windows.Forms.CheckBox cbAnimTurnM19;
        private System.Windows.Forms.CheckBox cbAnimTurnM25;
        private System.Windows.Forms.Button btnAnimationsDelete;
        private System.Windows.Forms.Button btnAnimationsAdd;
        private System.Windows.Forms.ListBox lbAnimations;
        private System.Windows.Forms.GroupBox gbTurn;
        private System.Windows.Forms.GroupBox gbSpin;
        private System.Windows.Forms.Button btnPlayAnims;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel pnlLeftSide;
        private System.Windows.Forms.Button btnAnimRename;
        private System.Windows.Forms.Panel pnlReloadAnimations;
        private System.Windows.Forms.Button btnRecalculate;
        private System.Windows.Forms.Label lblWarningText;
        private System.Windows.Forms.Label lblReloadWarning;
        private System.Windows.Forms.Button btnDefaultAnims;
    }
}