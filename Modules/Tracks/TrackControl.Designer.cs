namespace Pitstop64.Modules.Tracks
{
    partial class TrackControl
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
            this.btnTrackShack = new System.Windows.Forms.Button();
            this.pnlSharedControls = new System.Windows.Forms.Panel();
            this.btnTracksCancel = new System.Windows.Forms.Button();
            this.btnTracksApply = new System.Windows.Forms.Button();
            this.gbSelectedTracks = new System.Windows.Forms.GroupBox();
            this.btnTracksReset = new System.Windows.Forms.Button();
            this.cbTrackList = new System.Windows.Forms.ComboBox();
            this.btnInsertTrack = new System.Windows.Forms.Button();
            this.btnTrackDown = new System.Windows.Forms.Button();
            this.btnTrackUp = new System.Windows.Forms.Button();
            this.lbTracks = new System.Windows.Forms.ListBox();
            this.gbTracks = new System.Windows.Forms.GroupBox();
            this.btnDebugExport = new System.Windows.Forms.Button();
            this.btnExportTrack = new System.Windows.Forms.Button();
            this.btnImportTrack = new System.Windows.Forms.Button();
            this.lbAllTracks = new System.Windows.Forms.ListBox();
            this.tabTrackTextures = new System.Windows.Forms.TabControl();
            this.tabTracks = new System.Windows.Forms.TabPage();
            this.tabTextures = new System.Windows.Forms.TabPage();
            this.pnlImageView = new System.Windows.Forms.Panel();
            this.btnReplaceImage = new System.Windows.Forms.Button();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.imagePreviewControl = new Cereal64.Common.Controls.ImagePreviewControl();
            this.pnlImageSelect = new System.Windows.Forms.Panel();
            this.lblTracksHeader = new System.Windows.Forms.Label();
            this.cbTrackList2 = new System.Windows.Forms.ComboBox();
            this.lbTrackImages = new System.Windows.Forms.ListBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnTopSky = new System.Windows.Forms.Button();
            this.btnBottomSky = new System.Windows.Forms.Button();
            this.lblTopSky = new System.Windows.Forms.Label();
            this.lblBottomSky = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pnlSharedControls.SuspendLayout();
            this.gbSelectedTracks.SuspendLayout();
            this.gbTracks.SuspendLayout();
            this.tabTrackTextures.SuspendLayout();
            this.tabTracks.SuspendLayout();
            this.tabTextures.SuspendLayout();
            this.pnlImageView.SuspendLayout();
            this.pnlImageSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTrackShack
            // 
            this.btnTrackShack.Location = new System.Drawing.Point(250, 270);
            this.btnTrackShack.Name = "btnTrackShack";
            this.btnTrackShack.Size = new System.Drawing.Size(143, 54);
            this.btnTrackShack.TabIndex = 16;
            this.btnTrackShack.Text = "Launch Track Editor...";
            this.btnTrackShack.UseVisualStyleBackColor = true;
            this.btnTrackShack.Click += new System.EventHandler(this.btnTrackShack_Click);
            // 
            // pnlSharedControls
            // 
            this.pnlSharedControls.Controls.Add(this.btnTracksCancel);
            this.pnlSharedControls.Controls.Add(this.btnTracksApply);
            this.pnlSharedControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSharedControls.Location = new System.Drawing.Point(4, 379);
            this.pnlSharedControls.Name = "pnlSharedControls";
            this.pnlSharedControls.Size = new System.Drawing.Size(620, 56);
            this.pnlSharedControls.TabIndex = 15;
            // 
            // btnTracksCancel
            // 
            this.btnTracksCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTracksCancel.Location = new System.Drawing.Point(505, 4);
            this.btnTracksCancel.Name = "btnTracksCancel";
            this.btnTracksCancel.Size = new System.Drawing.Size(112, 49);
            this.btnTracksCancel.TabIndex = 7;
            this.btnTracksCancel.Text = "Cancel";
            this.btnTracksCancel.UseVisualStyleBackColor = true;
            this.btnTracksCancel.Click += new System.EventHandler(this.btnTracksCancel_Click);
            // 
            // btnTracksApply
            // 
            this.btnTracksApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTracksApply.Enabled = false;
            this.btnTracksApply.Location = new System.Drawing.Point(387, 4);
            this.btnTracksApply.Name = "btnTracksApply";
            this.btnTracksApply.Size = new System.Drawing.Size(112, 49);
            this.btnTracksApply.TabIndex = 6;
            this.btnTracksApply.Text = "Apply";
            this.btnTracksApply.UseVisualStyleBackColor = true;
            this.btnTracksApply.Click += new System.EventHandler(this.btnTracksApply_Click);
            // 
            // gbSelectedTracks
            // 
            this.gbSelectedTracks.Controls.Add(this.btnTracksReset);
            this.gbSelectedTracks.Controls.Add(this.cbTrackList);
            this.gbSelectedTracks.Controls.Add(this.btnInsertTrack);
            this.gbSelectedTracks.Controls.Add(this.btnTrackDown);
            this.gbSelectedTracks.Controls.Add(this.btnTrackUp);
            this.gbSelectedTracks.Controls.Add(this.lbTracks);
            this.gbSelectedTracks.Location = new System.Drawing.Point(250, 6);
            this.gbSelectedTracks.Name = "gbSelectedTracks";
            this.gbSelectedTracks.Size = new System.Drawing.Size(349, 252);
            this.gbSelectedTracks.TabIndex = 14;
            this.gbSelectedTracks.TabStop = false;
            this.gbSelectedTracks.Text = "Selected Tracks";
            // 
            // btnTracksReset
            // 
            this.btnTracksReset.Enabled = false;
            this.btnTracksReset.Location = new System.Drawing.Point(186, 170);
            this.btnTracksReset.Name = "btnTracksReset";
            this.btnTracksReset.Size = new System.Drawing.Size(75, 23);
            this.btnTracksReset.TabIndex = 5;
            this.btnTracksReset.Text = "Reset";
            this.btnTracksReset.UseVisualStyleBackColor = true;
            this.btnTracksReset.Click += new System.EventHandler(this.btnTracksReset_Click);
            // 
            // cbTrackList
            // 
            this.cbTrackList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrackList.FormattingEnabled = true;
            this.cbTrackList.Location = new System.Drawing.Point(202, 114);
            this.cbTrackList.Name = "cbTrackList";
            this.cbTrackList.Size = new System.Drawing.Size(141, 24);
            this.cbTrackList.TabIndex = 4;
            // 
            // btnInsertTrack
            // 
            this.btnInsertTrack.Enabled = false;
            this.btnInsertTrack.Image = global::Pitstop64.Properties.Resources.arrow_thick_left;
            this.btnInsertTrack.Location = new System.Drawing.Point(171, 114);
            this.btnInsertTrack.Name = "btnInsertTrack";
            this.btnInsertTrack.Size = new System.Drawing.Size(20, 20);
            this.btnInsertTrack.TabIndex = 3;
            this.btnInsertTrack.UseVisualStyleBackColor = true;
            this.btnInsertTrack.Click += new System.EventHandler(this.btnInsertTrack_Click);
            // 
            // btnTrackDown
            // 
            this.btnTrackDown.Enabled = false;
            this.btnTrackDown.Image = global::Pitstop64.Properties.Resources.arrow_thick_bottom;
            this.btnTrackDown.Location = new System.Drawing.Point(171, 61);
            this.btnTrackDown.Name = "btnTrackDown";
            this.btnTrackDown.Size = new System.Drawing.Size(20, 20);
            this.btnTrackDown.TabIndex = 2;
            this.btnTrackDown.UseVisualStyleBackColor = true;
            this.btnTrackDown.Click += new System.EventHandler(this.btnTrackDown_Click);
            // 
            // btnTrackUp
            // 
            this.btnTrackUp.Enabled = false;
            this.btnTrackUp.Image = global::Pitstop64.Properties.Resources.arrow_thick_top;
            this.btnTrackUp.Location = new System.Drawing.Point(171, 32);
            this.btnTrackUp.Name = "btnTrackUp";
            this.btnTrackUp.Size = new System.Drawing.Size(20, 20);
            this.btnTrackUp.TabIndex = 1;
            this.btnTrackUp.UseVisualStyleBackColor = true;
            this.btnTrackUp.Click += new System.EventHandler(this.btnTrackUp_Click);
            // 
            // lbTracks
            // 
            this.lbTracks.FormattingEnabled = true;
            this.lbTracks.ItemHeight = 16;
            this.lbTracks.Location = new System.Drawing.Point(15, 31);
            this.lbTracks.Name = "lbTracks";
            this.lbTracks.ScrollAlwaysVisible = true;
            this.lbTracks.Size = new System.Drawing.Size(150, 180);
            this.lbTracks.TabIndex = 0;
            this.lbTracks.SelectedIndexChanged += new System.EventHandler(this.lbTracks_SelectedIndexChanged);
            // 
            // gbTracks
            // 
            this.gbTracks.Controls.Add(this.btnDebugExport);
            this.gbTracks.Controls.Add(this.btnExportTrack);
            this.gbTracks.Controls.Add(this.btnImportTrack);
            this.gbTracks.Controls.Add(this.lbAllTracks);
            this.gbTracks.Location = new System.Drawing.Point(6, 6);
            this.gbTracks.Name = "gbTracks";
            this.gbTracks.Size = new System.Drawing.Size(213, 334);
            this.gbTracks.TabIndex = 13;
            this.gbTracks.TabStop = false;
            this.gbTracks.Text = "Tracks";
            // 
            // btnDebugExport
            // 
            this.btnDebugExport.Location = new System.Drawing.Point(26, 287);
            this.btnDebugExport.Name = "btnDebugExport";
            this.btnDebugExport.Size = new System.Drawing.Size(151, 33);
            this.btnDebugExport.TabIndex = 25;
            this.btnDebugExport.Text = "Export Decoded Data";
            this.btnDebugExport.UseVisualStyleBackColor = true;
            this.btnDebugExport.Click += new System.EventHandler(this.btnDebugExport_Click);
            // 
            // btnExportTrack
            // 
            this.btnExportTrack.Location = new System.Drawing.Point(53, 247);
            this.btnExportTrack.Name = "btnExportTrack";
            this.btnExportTrack.Size = new System.Drawing.Size(93, 34);
            this.btnExportTrack.TabIndex = 24;
            this.btnExportTrack.Text = "Export";
            this.btnExportTrack.UseVisualStyleBackColor = true;
            this.btnExportTrack.Click += new System.EventHandler(this.btnExportTrack_Click);
            // 
            // btnImportTrack
            // 
            this.btnImportTrack.Location = new System.Drawing.Point(53, 208);
            this.btnImportTrack.Name = "btnImportTrack";
            this.btnImportTrack.Size = new System.Drawing.Size(93, 33);
            this.btnImportTrack.TabIndex = 5;
            this.btnImportTrack.Text = "Import";
            this.btnImportTrack.UseVisualStyleBackColor = true;
            this.btnImportTrack.Click += new System.EventHandler(this.btnImportTrack_Click);
            // 
            // lbAllTracks
            // 
            this.lbAllTracks.FormattingEnabled = true;
            this.lbAllTracks.ItemHeight = 16;
            this.lbAllTracks.Location = new System.Drawing.Point(6, 22);
            this.lbAllTracks.Name = "lbAllTracks";
            this.lbAllTracks.ScrollAlwaysVisible = true;
            this.lbAllTracks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAllTracks.Size = new System.Drawing.Size(184, 180);
            this.lbAllTracks.TabIndex = 1;
            this.lbAllTracks.SelectedIndexChanged += new System.EventHandler(this.lbAllTracks_SelectedIndexChanged);
            // 
            // tabTrackTextures
            // 
            this.tabTrackTextures.Controls.Add(this.tabTracks);
            this.tabTrackTextures.Controls.Add(this.tabTextures);
            this.tabTrackTextures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTrackTextures.Location = new System.Drawing.Point(4, 4);
            this.tabTrackTextures.Name = "tabTrackTextures";
            this.tabTrackTextures.SelectedIndex = 0;
            this.tabTrackTextures.Size = new System.Drawing.Size(620, 375);
            this.tabTrackTextures.TabIndex = 17;
            // 
            // tabTracks
            // 
            this.tabTracks.BackColor = System.Drawing.SystemColors.Control;
            this.tabTracks.Controls.Add(this.gbTracks);
            this.tabTracks.Controls.Add(this.btnTrackShack);
            this.tabTracks.Controls.Add(this.gbSelectedTracks);
            this.tabTracks.Location = new System.Drawing.Point(4, 25);
            this.tabTracks.Name = "tabTracks";
            this.tabTracks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTracks.Size = new System.Drawing.Size(612, 346);
            this.tabTracks.TabIndex = 0;
            this.tabTracks.Text = "Tracks";
            // 
            // tabTextures
            // 
            this.tabTextures.BackColor = System.Drawing.SystemColors.Control;
            this.tabTextures.Controls.Add(this.pnlImageView);
            this.tabTextures.Controls.Add(this.pnlImageSelect);
            this.tabTextures.Location = new System.Drawing.Point(4, 25);
            this.tabTextures.Name = "tabTextures";
            this.tabTextures.Padding = new System.Windows.Forms.Padding(3);
            this.tabTextures.Size = new System.Drawing.Size(612, 346);
            this.tabTextures.TabIndex = 1;
            this.tabTextures.Text = "Track Textures";
            // 
            // pnlImageView
            // 
            this.pnlImageView.Controls.Add(this.btnReplaceImage);
            this.pnlImageView.Controls.Add(this.lblImageSize);
            this.pnlImageView.Controls.Add(this.imagePreviewControl);
            this.pnlImageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImageView.Location = new System.Drawing.Point(216, 3);
            this.pnlImageView.Name = "pnlImageView";
            this.pnlImageView.Size = new System.Drawing.Size(393, 340);
            this.pnlImageView.TabIndex = 6;
            // 
            // btnReplaceImage
            // 
            this.btnReplaceImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplaceImage.Location = new System.Drawing.Point(128, 300);
            this.btnReplaceImage.Name = "btnReplaceImage";
            this.btnReplaceImage.Size = new System.Drawing.Size(246, 35);
            this.btnReplaceImage.TabIndex = 9;
            this.btnReplaceImage.Text = "Replace Texture with...";
            this.btnReplaceImage.UseVisualStyleBackColor = true;
            this.btnReplaceImage.Click += new System.EventHandler(this.btnReplaceImage_Click);
            // 
            // lblImageSize
            // 
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.Location = new System.Drawing.Point(19, 307);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(0, 17);
            this.lblImageSize.TabIndex = 8;
            // 
            // imagePreviewControl
            // 
            this.imagePreviewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePreviewControl.ExportButtonVisible = true;
            this.imagePreviewControl.Image = null;
            this.imagePreviewControl.ImageName = "";
            this.imagePreviewControl.ImageSize = new System.Drawing.Size(342, 268);
            this.imagePreviewControl.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imagePreviewControl.Location = new System.Drawing.Point(2, 1);
            this.imagePreviewControl.LockImageSize = false;
            this.imagePreviewControl.Margin = new System.Windows.Forms.Padding(4);
            this.imagePreviewControl.Name = "imagePreviewControl";
            this.imagePreviewControl.OverlayImage = null;
            this.imagePreviewControl.Size = new System.Drawing.Size(387, 293);
            this.imagePreviewControl.TabIndex = 7;
            // 
            // pnlImageSelect
            // 
            this.pnlImageSelect.Controls.Add(this.lblBottomSky);
            this.pnlImageSelect.Controls.Add(this.lblTopSky);
            this.pnlImageSelect.Controls.Add(this.btnBottomSky);
            this.pnlImageSelect.Controls.Add(this.btnTopSky);
            this.pnlImageSelect.Controls.Add(this.lblTracksHeader);
            this.pnlImageSelect.Controls.Add(this.cbTrackList2);
            this.pnlImageSelect.Controls.Add(this.lbTrackImages);
            this.pnlImageSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlImageSelect.Location = new System.Drawing.Point(3, 3);
            this.pnlImageSelect.Name = "pnlImageSelect";
            this.pnlImageSelect.Size = new System.Drawing.Size(213, 340);
            this.pnlImageSelect.TabIndex = 7;
            // 
            // lblTracksHeader
            // 
            this.lblTracksHeader.AutoSize = true;
            this.lblTracksHeader.Location = new System.Drawing.Point(80, 7);
            this.lblTracksHeader.Name = "lblTracksHeader";
            this.lblTracksHeader.Size = new System.Drawing.Size(51, 17);
            this.lblTracksHeader.TabIndex = 27;
            this.lblTracksHeader.Text = "Tracks";
            // 
            // cbTrackList2
            // 
            this.cbTrackList2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTrackList2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrackList2.FormattingEnabled = true;
            this.cbTrackList2.Location = new System.Drawing.Point(22, 30);
            this.cbTrackList2.Name = "cbTrackList2";
            this.cbTrackList2.Size = new System.Drawing.Size(167, 24);
            this.cbTrackList2.TabIndex = 5;
            this.cbTrackList2.SelectedIndexChanged += new System.EventHandler(this.cbTrackList2_SelectedIndexChanged);
            // 
            // lbTrackImages
            // 
            this.lbTrackImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTrackImages.FormattingEnabled = true;
            this.lbTrackImages.ItemHeight = 16;
            this.lbTrackImages.Location = new System.Drawing.Point(22, 60);
            this.lbTrackImages.Name = "lbTrackImages";
            this.lbTrackImages.ScrollAlwaysVisible = true;
            this.lbTrackImages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTrackImages.Size = new System.Drawing.Size(167, 196);
            this.lbTrackImages.TabIndex = 26;
            this.lbTrackImages.SelectedIndexChanged += new System.EventHandler(this.lbTrackImages_SelectedIndexChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Track file|*.track";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Track file|*.track";
            // 
            // btnTopSky
            // 
            this.btnTopSky.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopSky.Location = new System.Drawing.Point(144, 268);
            this.btnTopSky.Name = "btnTopSky";
            this.btnTopSky.Size = new System.Drawing.Size(30, 26);
            this.btnTopSky.TabIndex = 28;
            this.btnTopSky.UseVisualStyleBackColor = true;
            this.btnTopSky.Click += new System.EventHandler(this.btnTopSky_Click);
            // 
            // btnBottomSky
            // 
            this.btnBottomSky.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBottomSky.Location = new System.Drawing.Point(144, 304);
            this.btnBottomSky.Name = "btnBottomSky";
            this.btnBottomSky.Size = new System.Drawing.Size(30, 26);
            this.btnBottomSky.TabIndex = 29;
            this.btnBottomSky.UseVisualStyleBackColor = true;
            this.btnBottomSky.Click += new System.EventHandler(this.btnBottomSky_Click);
            // 
            // lblTopSky
            // 
            this.lblTopSky.AutoSize = true;
            this.lblTopSky.Location = new System.Drawing.Point(15, 273);
            this.lblTopSky.Name = "lblTopSky";
            this.lblTopSky.Size = new System.Drawing.Size(97, 17);
            this.lblTopSky.TabIndex = 30;
            this.lblTopSky.Text = "Top Sky Color";
            // 
            // lblBottomSky
            // 
            this.lblBottomSky.AutoSize = true;
            this.lblBottomSky.Location = new System.Drawing.Point(15, 309);
            this.lblBottomSky.Name = "lblBottomSky";
            this.lblBottomSky.Size = new System.Drawing.Size(116, 17);
            this.lblBottomSky.TabIndex = 31;
            this.lblBottomSky.Text = "Bottom Sky Color";
            // 
            // TrackControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabTrackTextures);
            this.Controls.Add(this.pnlSharedControls);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrackControl";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(628, 439);
            this.pnlSharedControls.ResumeLayout(false);
            this.gbSelectedTracks.ResumeLayout(false);
            this.gbTracks.ResumeLayout(false);
            this.tabTrackTextures.ResumeLayout(false);
            this.tabTracks.ResumeLayout(false);
            this.tabTextures.ResumeLayout(false);
            this.pnlImageView.ResumeLayout(false);
            this.pnlImageView.PerformLayout();
            this.pnlImageSelect.ResumeLayout(false);
            this.pnlImageSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTrackShack;
        private System.Windows.Forms.Panel pnlSharedControls;
        private System.Windows.Forms.Button btnTracksCancel;
        private System.Windows.Forms.Button btnTracksApply;
        private System.Windows.Forms.GroupBox gbSelectedTracks;
        private System.Windows.Forms.Button btnTracksReset;
        private System.Windows.Forms.ComboBox cbTrackList;
        private System.Windows.Forms.Button btnInsertTrack;
        private System.Windows.Forms.Button btnTrackDown;
        private System.Windows.Forms.Button btnTrackUp;
        private System.Windows.Forms.ListBox lbTracks;
        private System.Windows.Forms.GroupBox gbTracks;
        private System.Windows.Forms.Button btnExportTrack;
        private System.Windows.Forms.Button btnImportTrack;
        private System.Windows.Forms.ListBox lbAllTracks;
        private System.Windows.Forms.TabControl tabTrackTextures;
        private System.Windows.Forms.TabPage tabTracks;
        private System.Windows.Forms.TabPage tabTextures;
        private System.Windows.Forms.ComboBox cbTrackList2;
        private System.Windows.Forms.Panel pnlImageView;
        private System.Windows.Forms.Panel pnlImageSelect;
        private System.Windows.Forms.ListBox lbTrackImages;
        private Cereal64.Common.Controls.ImagePreviewControl imagePreviewControl;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.Button btnReplaceImage;
        private System.Windows.Forms.Label lblTracksHeader;
        private System.Windows.Forms.Button btnDebugExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnTopSky;
        private System.Windows.Forms.Button btnBottomSky;
        private System.Windows.Forms.Label lblBottomSky;
        private System.Windows.Forms.Label lblTopSky;
        private System.Windows.Forms.ColorDialog colorDialog;



    }
}
