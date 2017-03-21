namespace Pitstop64.Modules.Karts
{
    partial class KartControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnKartsCancel = new System.Windows.Forms.Button();
            this.btnKartsApply = new System.Windows.Forms.Button();
            this.pnlSharedControls = new System.Windows.Forms.Panel();
            this.saveKartDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbSelectedKarts = new System.Windows.Forms.GroupBox();
            this.btnKartsReset = new System.Windows.Forms.Button();
            this.cbKartList = new System.Windows.Forms.ComboBox();
            this.lbKarts = new System.Windows.Forms.ListBox();
            this.gbKarts = new System.Windows.Forms.GroupBox();
            this.kartPreviewControl = new Pitstop64.Modules.Karts.KartPreviewControl();
            this.btnExportKart = new System.Windows.Forms.Button();
            this.lblKartName = new System.Windows.Forms.Label();
            this.btnImportKart = new System.Windows.Forms.Button();
            this.pnlPortrait = new System.Windows.Forms.Panel();
            this.lbAllKarts = new System.Windows.Forms.ListBox();
            this.openKartDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnChompShop = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnInsertKart = new System.Windows.Forms.Button();
            this.btnKartDown = new System.Windows.Forms.Button();
            this.btnKartUp = new System.Windows.Forms.Button();
            this.pbPortrait = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlSharedControls.SuspendLayout();
            this.gbSelectedKarts.SuspendLayout();
            this.gbKarts.SuspendLayout();
            this.pnlPortrait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKartsCancel
            // 
            this.btnKartsCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKartsCancel.Location = new System.Drawing.Point(490, 4);
            this.btnKartsCancel.Name = "btnKartsCancel";
            this.btnKartsCancel.Size = new System.Drawing.Size(112, 49);
            this.btnKartsCancel.TabIndex = 7;
            this.btnKartsCancel.Text = "Cancel";
            this.btnKartsCancel.UseVisualStyleBackColor = true;
            this.btnKartsCancel.Click += new System.EventHandler(this.btnKartsCancel_Click);
            // 
            // btnKartsApply
            // 
            this.btnKartsApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKartsApply.Enabled = false;
            this.btnKartsApply.Location = new System.Drawing.Point(372, 4);
            this.btnKartsApply.Name = "btnKartsApply";
            this.btnKartsApply.Size = new System.Drawing.Size(112, 49);
            this.btnKartsApply.TabIndex = 6;
            this.btnKartsApply.Text = "Apply";
            this.btnKartsApply.UseVisualStyleBackColor = true;
            this.btnKartsApply.Click += new System.EventHandler(this.btnKartsApply_Click);
            // 
            // pnlSharedControls
            // 
            this.pnlSharedControls.Controls.Add(this.btnKartsCancel);
            this.pnlSharedControls.Controls.Add(this.btnKartsApply);
            this.pnlSharedControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSharedControls.Location = new System.Drawing.Point(0, 365);
            this.pnlSharedControls.Name = "pnlSharedControls";
            this.pnlSharedControls.Size = new System.Drawing.Size(605, 56);
            this.pnlSharedControls.TabIndex = 11;
            // 
            // saveKartDialog
            // 
            this.saveKartDialog.DefaultExt = "kart";
            this.saveKartDialog.Filter = "Karts|*.karts";
            this.saveKartDialog.OverwritePrompt = false;
            // 
            // gbSelectedKarts
            // 
            this.gbSelectedKarts.Controls.Add(this.btnKartsReset);
            this.gbSelectedKarts.Controls.Add(this.cbKartList);
            this.gbSelectedKarts.Controls.Add(this.btnInsertKart);
            this.gbSelectedKarts.Controls.Add(this.btnKartDown);
            this.gbSelectedKarts.Controls.Add(this.btnKartUp);
            this.gbSelectedKarts.Controls.Add(this.lbKarts);
            this.gbSelectedKarts.Location = new System.Drawing.Point(318, 8);
            this.gbSelectedKarts.Name = "gbSelectedKarts";
            this.gbSelectedKarts.Size = new System.Drawing.Size(276, 252);
            this.gbSelectedKarts.TabIndex = 11;
            this.gbSelectedKarts.TabStop = false;
            this.gbSelectedKarts.Text = "Selected Karts";
            // 
            // btnKartsReset
            // 
            this.btnKartsReset.Enabled = false;
            this.btnKartsReset.Location = new System.Drawing.Point(156, 170);
            this.btnKartsReset.Name = "btnKartsReset";
            this.btnKartsReset.Size = new System.Drawing.Size(75, 23);
            this.btnKartsReset.TabIndex = 5;
            this.btnKartsReset.Text = "Reset";
            this.btnKartsReset.UseVisualStyleBackColor = true;
            this.btnKartsReset.Click += new System.EventHandler(this.btnKartsReset_Click);
            // 
            // cbKartList
            // 
            this.cbKartList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKartList.FormattingEnabled = true;
            this.cbKartList.Location = new System.Drawing.Point(167, 113);
            this.cbKartList.Name = "cbKartList";
            this.cbKartList.Size = new System.Drawing.Size(97, 24);
            this.cbKartList.TabIndex = 4;
            // 
            // lbKarts
            // 
            this.lbKarts.FormattingEnabled = true;
            this.lbKarts.ItemHeight = 16;
            this.lbKarts.Location = new System.Drawing.Point(15, 31);
            this.lbKarts.Name = "lbKarts";
            this.lbKarts.ScrollAlwaysVisible = true;
            this.lbKarts.Size = new System.Drawing.Size(120, 180);
            this.lbKarts.TabIndex = 0;
            this.lbKarts.SelectedIndexChanged += new System.EventHandler(this.lbKarts_SelectedIndexChanged);
            // 
            // gbKarts
            // 
            this.gbKarts.Controls.Add(this.kartPreviewControl);
            this.gbKarts.Controls.Add(this.btnExportKart);
            this.gbKarts.Controls.Add(this.lblKartName);
            this.gbKarts.Controls.Add(this.btnImportKart);
            this.gbKarts.Controls.Add(this.pnlPortrait);
            this.gbKarts.Controls.Add(this.lbAllKarts);
            this.gbKarts.Location = new System.Drawing.Point(3, 3);
            this.gbKarts.Name = "gbKarts";
            this.gbKarts.Size = new System.Drawing.Size(309, 356);
            this.gbKarts.TabIndex = 10;
            this.gbKarts.TabStop = false;
            this.gbKarts.Text = "Karts";
            // 
            // kartPreviewControl
            // 
            this.kartPreviewControl.AnimIndex = 0;
            this.kartPreviewControl.CycleAnimations = true;
            this.kartPreviewControl.DisplayRefKartOption = false;
            this.kartPreviewControl.ExportButtonVisible = true;
            this.kartPreviewControl.FrameIndex = 0;
            this.kartPreviewControl.FramesPerSecond = 60;
            this.kartPreviewControl.Image = null;
            this.kartPreviewControl.ImageName = "";
            this.kartPreviewControl.ImageSize = new System.Drawing.Size(132, 139);
            this.kartPreviewControl.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.kartPreviewControl.Kart = null;
            this.kartPreviewControl.Location = new System.Drawing.Point(132, 186);
            this.kartPreviewControl.LockImageSize = true;
            this.kartPreviewControl.Mode = Pitstop64.Modules.Karts.KartPreviewControl.PreviewMode.Animated;
            this.kartPreviewControl.Name = "kartPreviewControl";
            this.kartPreviewControl.OverlayImage = null;
            this.kartPreviewControl.ReferenceKart = null;
            this.kartPreviewControl.ShowReferenceKart = false;
            this.kartPreviewControl.Size = new System.Drawing.Size(174, 164);
            this.kartPreviewControl.TabIndex = 25;
            this.kartPreviewControl.UseAnimPalettes = true;
            // 
            // btnExportKart
            // 
            this.btnExportKart.Location = new System.Drawing.Point(19, 263);
            this.btnExportKart.Name = "btnExportKart";
            this.btnExportKart.Size = new System.Drawing.Size(93, 34);
            this.btnExportKart.TabIndex = 24;
            this.btnExportKart.Text = "Export";
            this.btnExportKart.UseVisualStyleBackColor = true;
            this.btnExportKart.Click += new System.EventHandler(this.btnExportKart_Click);
            // 
            // lblKartName
            // 
            this.lblKartName.AutoSize = true;
            this.lblKartName.Location = new System.Drawing.Point(184, 155);
            this.lblKartName.Name = "lblKartName";
            this.lblKartName.Size = new System.Drawing.Size(75, 17);
            this.lblKartName.TabIndex = 0;
            this.lblKartName.Text = "Kart Name";
            // 
            // btnImportKart
            // 
            this.btnImportKart.Location = new System.Drawing.Point(19, 224);
            this.btnImportKart.Name = "btnImportKart";
            this.btnImportKart.Size = new System.Drawing.Size(93, 33);
            this.btnImportKart.TabIndex = 5;
            this.btnImportKart.Text = "Import";
            this.btnImportKart.UseVisualStyleBackColor = true;
            this.btnImportKart.Click += new System.EventHandler(this.btnImportKart_Click);
            // 
            // pnlPortrait
            // 
            this.pnlPortrait.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlPortrait.Controls.Add(this.pbPortrait);
            this.pnlPortrait.Location = new System.Drawing.Point(157, 22);
            this.pnlPortrait.Name = "pnlPortrait";
            this.pnlPortrait.Size = new System.Drawing.Size(130, 130);
            this.pnlPortrait.TabIndex = 20;
            // 
            // lbAllKarts
            // 
            this.lbAllKarts.FormattingEnabled = true;
            this.lbAllKarts.ItemHeight = 16;
            this.lbAllKarts.Location = new System.Drawing.Point(6, 22);
            this.lbAllKarts.Name = "lbAllKarts";
            this.lbAllKarts.ScrollAlwaysVisible = true;
            this.lbAllKarts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAllKarts.Size = new System.Drawing.Size(120, 180);
            this.lbAllKarts.TabIndex = 1;
            this.lbAllKarts.SelectedIndexChanged += new System.EventHandler(this.lbAllKarts_SelectedIndexChanged);
            // 
            // openKartDialog
            // 
            this.openKartDialog.DefaultExt = "kart";
            this.openKartDialog.Filter = "Karts|*.karts";
            // 
            // btnChompShop
            // 
            this.btnChompShop.Location = new System.Drawing.Point(439, 280);
            this.btnChompShop.Name = "btnChompShop";
            this.btnChompShop.Size = new System.Drawing.Size(143, 54);
            this.btnChompShop.TabIndex = 12;
            this.btnChompShop.Text = "Launch Kart Editor...";
            this.btnChompShop.UseVisualStyleBackColor = true;
            this.btnChompShop.Click += new System.EventHandler(this.btnChompShop_Click);
            // 
            // btnInsertKart
            // 
            this.btnInsertKart.Enabled = false;
            this.btnInsertKart.Image = global::Pitstop64.Properties.Resources.arrow_thick_left;
            this.btnInsertKart.Location = new System.Drawing.Point(141, 114);
            this.btnInsertKart.Name = "btnInsertKart";
            this.btnInsertKart.Size = new System.Drawing.Size(20, 20);
            this.btnInsertKart.TabIndex = 3;
            this.btnInsertKart.UseVisualStyleBackColor = true;
            this.btnInsertKart.Click += new System.EventHandler(this.btnInsertKart_Click);
            // 
            // btnKartDown
            // 
            this.btnKartDown.Enabled = false;
            this.btnKartDown.Image = global::Pitstop64.Properties.Resources.arrow_thick_bottom;
            this.btnKartDown.Location = new System.Drawing.Point(141, 61);
            this.btnKartDown.Name = "btnKartDown";
            this.btnKartDown.Size = new System.Drawing.Size(20, 20);
            this.btnKartDown.TabIndex = 2;
            this.btnKartDown.UseVisualStyleBackColor = true;
            this.btnKartDown.Click += new System.EventHandler(this.btnKartDown_Click);
            // 
            // btnKartUp
            // 
            this.btnKartUp.Enabled = false;
            this.btnKartUp.Image = global::Pitstop64.Properties.Resources.arrow_thick_top;
            this.btnKartUp.Location = new System.Drawing.Point(141, 32);
            this.btnKartUp.Name = "btnKartUp";
            this.btnKartUp.Size = new System.Drawing.Size(20, 20);
            this.btnKartUp.TabIndex = 1;
            this.btnKartUp.UseVisualStyleBackColor = true;
            this.btnKartUp.Click += new System.EventHandler(this.btnKartUp_Click);
            // 
            // pbPortrait
            // 
            this.pbPortrait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPortrait.Location = new System.Drawing.Point(0, 0);
            this.pbPortrait.Name = "pbPortrait";
            this.pbPortrait.Size = new System.Drawing.Size(130, 130);
            this.pbPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPortrait.TabIndex = 0;
            this.pbPortrait.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(331, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 48);
            this.button1.TabIndex = 13;
            this.button1.Text = "Debug Stats";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // KartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnChompShop);
            this.Controls.Add(this.pnlSharedControls);
            this.Controls.Add(this.gbSelectedKarts);
            this.Controls.Add(this.gbKarts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartControl";
            this.Size = new System.Drawing.Size(605, 421);
            this.VisibleChanged += new System.EventHandler(this.KartControl_VisibleChanged);
            this.pnlSharedControls.ResumeLayout(false);
            this.gbSelectedKarts.ResumeLayout(false);
            this.gbKarts.ResumeLayout(false);
            this.gbKarts.PerformLayout();
            this.pnlPortrait.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPortrait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKartsCancel;
        private System.Windows.Forms.Button btnKartsApply;
        private System.Windows.Forms.Panel pnlSharedControls;
        private System.Windows.Forms.SaveFileDialog saveKartDialog;
        private System.Windows.Forms.GroupBox gbSelectedKarts;
        private System.Windows.Forms.Button btnKartsReset;
        private System.Windows.Forms.ComboBox cbKartList;
        private System.Windows.Forms.Button btnInsertKart;
        private System.Windows.Forms.Button btnKartDown;
        private System.Windows.Forms.Button btnKartUp;
        private System.Windows.Forms.ListBox lbKarts;
        private System.Windows.Forms.GroupBox gbKarts;
        private KartPreviewControl kartPreviewControl;
        private System.Windows.Forms.Button btnExportKart;
        private System.Windows.Forms.Label lblKartName;
        private System.Windows.Forms.Button btnImportKart;
        private System.Windows.Forms.Panel pnlPortrait;
        private System.Windows.Forms.PictureBox pbPortrait;
        private System.Windows.Forms.ListBox lbAllKarts;
        private System.Windows.Forms.OpenFileDialog openKartDialog;
        private System.Windows.Forms.Button btnChompShop;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button1;
    }
}
