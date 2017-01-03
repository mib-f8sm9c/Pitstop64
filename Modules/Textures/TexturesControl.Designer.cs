namespace Pitstop64.Modules.Textures
{
    partial class TexturesControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlChoose = new System.Windows.Forms.Panel();
            this.lblImageCount = new System.Windows.Forms.Label();
            this.txtSearchImages = new Cereal64.Common.Controls.WatermarkTextBox();
            this.cbImageType = new System.Windows.Forms.ComboBox();
            this.lblImageType = new System.Windows.Forms.Label();
            this.btnRemoveImage = new System.Windows.Forms.Button();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.lbImages = new System.Windows.Forms.ListBox();
            this.pnlView = new System.Windows.Forms.Panel();
            this.imagePreviewControl = new Cereal64.Common.Controls.ImagePreviewControl();
            this.pnlTools = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlChoose.SuspendLayout();
            this.pnlView.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlChoose);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlView);
            this.splitContainer1.Size = new System.Drawing.Size(679, 324);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlChoose
            // 
            this.pnlChoose.Controls.Add(this.lblImageCount);
            this.pnlChoose.Controls.Add(this.txtSearchImages);
            this.pnlChoose.Controls.Add(this.cbImageType);
            this.pnlChoose.Controls.Add(this.lblImageType);
            this.pnlChoose.Controls.Add(this.btnRemoveImage);
            this.pnlChoose.Controls.Add(this.btnAddImage);
            this.pnlChoose.Controls.Add(this.lbImages);
            this.pnlChoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChoose.Location = new System.Drawing.Point(0, 0);
            this.pnlChoose.Name = "pnlChoose";
            this.pnlChoose.Size = new System.Drawing.Size(226, 324);
            this.pnlChoose.TabIndex = 0;
            // 
            // lblImageCount
            // 
            this.lblImageCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Location = new System.Drawing.Point(69, 283);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(57, 17);
            this.lblImageCount.TabIndex = 6;
            this.lblImageCount.Text = "Images:";
            // 
            // txtSearchImages
            // 
            this.txtSearchImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchImages.Location = new System.Drawing.Point(20, 14);
            this.txtSearchImages.Name = "txtSearchImages";
            this.txtSearchImages.Size = new System.Drawing.Size(185, 23);
            this.txtSearchImages.TabIndex = 0;
            this.txtSearchImages.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtSearchImages.WaterMarkText = "Filter by name...";
            this.txtSearchImages.TextChanged += new System.EventHandler(this.txtSearchImages_TextChanged);
            // 
            // cbImageType
            // 
            this.cbImageType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageType.FormattingEnabled = true;
            this.cbImageType.Items.AddRange(new object[] {
            "All",
            "RGBA",
            "CI",
            "IA",
            "I",
            "MIO0",
            "TKMK00",
            "Raw"});
            this.cbImageType.Location = new System.Drawing.Point(72, 44);
            this.cbImageType.Name = "cbImageType";
            this.cbImageType.Size = new System.Drawing.Size(133, 24);
            this.cbImageType.TabIndex = 5;
            this.cbImageType.SelectedIndexChanged += new System.EventHandler(this.cbImageType_SelectedIndexChanged);
            // 
            // lblImageType
            // 
            this.lblImageType.AutoSize = true;
            this.lblImageType.Location = new System.Drawing.Point(22, 46);
            this.lblImageType.Name = "lblImageType";
            this.lblImageType.Size = new System.Drawing.Size(44, 17);
            this.lblImageType.TabIndex = 4;
            this.lblImageType.Text = "Type:";
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveImage.Image = global::MK64Pitstop.Properties.Resources.minus;
            this.btnRemoveImage.Location = new System.Drawing.Point(181, 276);
            this.btnRemoveImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveImage.TabIndex = 2;
            this.btnRemoveImage.UseVisualStyleBackColor = true;
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
            // 
            // btnAddImage
            // 
            this.btnAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddImage.Image = global::MK64Pitstop.Properties.Resources.plus;
            this.btnAddImage.Location = new System.Drawing.Point(20, 276);
            this.btnAddImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(24, 24);
            this.btnAddImage.TabIndex = 1;
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // lbImages
            // 
            this.lbImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbImages.FormattingEnabled = true;
            this.lbImages.ItemHeight = 16;
            this.lbImages.Location = new System.Drawing.Point(20, 73);
            this.lbImages.Name = "lbImages";
            this.lbImages.ScrollAlwaysVisible = true;
            this.lbImages.Size = new System.Drawing.Size(185, 196);
            this.lbImages.TabIndex = 0;
            this.lbImages.SelectedIndexChanged += new System.EventHandler(this.lbImages_SelectedIndexChanged);
            // 
            // pnlView
            // 
            this.pnlView.Controls.Add(this.imagePreviewControl);
            this.pnlView.Controls.Add(this.pnlTools);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(449, 324);
            this.pnlView.TabIndex = 0;
            // 
            // imagePreviewControl
            // 
            this.imagePreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePreviewControl.ExportButtonVisible = true;
            this.imagePreviewControl.Image = null;
            this.imagePreviewControl.ImageName = "";
            this.imagePreviewControl.ImageSize = new System.Drawing.Size(404, 220);
            this.imagePreviewControl.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imagePreviewControl.Location = new System.Drawing.Point(0, 0);
            this.imagePreviewControl.LockImageSize = false;
            this.imagePreviewControl.Margin = new System.Windows.Forms.Padding(4);
            this.imagePreviewControl.Name = "imagePreviewControl";
            this.imagePreviewControl.OverlayImage = null;
            this.imagePreviewControl.Size = new System.Drawing.Size(449, 245);
            this.imagePreviewControl.TabIndex = 6;
            // 
            // pnlTools
            // 
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTools.Location = new System.Drawing.Point(0, 245);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(449, 79);
            this.pnlTools.TabIndex = 7;
            // 
            // TexturesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TexturesControl";
            this.Size = new System.Drawing.Size(679, 324);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlChoose.ResumeLayout(false);
            this.pnlChoose.PerformLayout();
            this.pnlView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlView;
        private System.Windows.Forms.Panel pnlChoose;
        private System.Windows.Forms.ListBox lbImages;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.Button btnRemoveImage;
        private System.Windows.Forms.Label lblImageType;
        private System.Windows.Forms.ComboBox cbImageType;
        private Cereal64.Common.Controls.WatermarkTextBox txtSearchImages;
        private Cereal64.Common.Controls.ImagePreviewControl imagePreviewControl;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.Label lblImageCount;
    }
}
