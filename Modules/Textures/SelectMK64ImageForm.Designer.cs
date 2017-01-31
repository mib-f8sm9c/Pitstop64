namespace Pitstop64.Modules.Textures
{
    partial class SelectMK64ImageForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlChoose = new System.Windows.Forms.Panel();
            this.txtSearchImages = new Cereal64.Common.Controls.WatermarkTextBox();
            this.cbImageType = new System.Windows.Forms.ComboBox();
            this.lblImageType = new System.Windows.Forms.Label();
            this.lbImages = new System.Windows.Forms.ListBox();
            this.pnlView = new System.Windows.Forms.Panel();
            this.imagePreviewControl = new Cereal64.Common.Controls.ImagePreviewControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlChoose.SuspendLayout();
            this.pnlView.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlChoose);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlView);
            this.splitContainer1.Size = new System.Drawing.Size(710, 353);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // pnlChoose
            // 
            this.pnlChoose.Controls.Add(this.txtSearchImages);
            this.pnlChoose.Controls.Add(this.cbImageType);
            this.pnlChoose.Controls.Add(this.lblImageType);
            this.pnlChoose.Controls.Add(this.lbImages);
            this.pnlChoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChoose.Location = new System.Drawing.Point(0, 0);
            this.pnlChoose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlChoose.Name = "pnlChoose";
            this.pnlChoose.Size = new System.Drawing.Size(236, 353);
            this.pnlChoose.TabIndex = 0;
            // 
            // txtSearchImages
            // 
            this.txtSearchImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchImages.Location = new System.Drawing.Point(27, 17);
            this.txtSearchImages.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearchImages.Name = "txtSearchImages";
            this.txtSearchImages.Size = new System.Drawing.Size(180, 23);
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
            this.cbImageType.Location = new System.Drawing.Point(96, 54);
            this.cbImageType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbImageType.Name = "cbImageType";
            this.cbImageType.Size = new System.Drawing.Size(111, 24);
            this.cbImageType.TabIndex = 5;
            this.cbImageType.SelectedIndexChanged += new System.EventHandler(this.cbImageType_SelectedIndexChanged);
            // 
            // lblImageType
            // 
            this.lblImageType.AutoSize = true;
            this.lblImageType.Location = new System.Drawing.Point(29, 57);
            this.lblImageType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageType.Name = "lblImageType";
            this.lblImageType.Size = new System.Drawing.Size(43, 16);
            this.lblImageType.TabIndex = 4;
            this.lblImageType.Text = "Type:";
            // 
            // lbImages
            // 
            this.lbImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbImages.FormattingEnabled = true;
            this.lbImages.ItemHeight = 16;
            this.lbImages.Location = new System.Drawing.Point(27, 90);
            this.lbImages.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbImages.Name = "lbImages";
            this.lbImages.ScrollAlwaysVisible = true;
            this.lbImages.Size = new System.Drawing.Size(180, 228);
            this.lbImages.TabIndex = 0;
            this.lbImages.SelectedIndexChanged += new System.EventHandler(this.lbImages_SelectedIndexChanged);
            // 
            // pnlView
            // 
            this.pnlView.Controls.Add(this.imagePreviewControl);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 0);
            this.pnlView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(469, 353);
            this.pnlView.TabIndex = 0;
            // 
            // imagePreviewControl
            // 
            this.imagePreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePreviewControl.ExportButtonVisible = true;
            this.imagePreviewControl.Image = null;
            this.imagePreviewControl.ImageName = "";
            this.imagePreviewControl.ImageSize = new System.Drawing.Size(409, 322);
            this.imagePreviewControl.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imagePreviewControl.Location = new System.Drawing.Point(0, 0);
            this.imagePreviewControl.LockImageSize = false;
            this.imagePreviewControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.imagePreviewControl.Name = "imagePreviewControl";
            this.imagePreviewControl.OverlayImage = null;
            this.imagePreviewControl.Size = new System.Drawing.Size(469, 353);
            this.imagePreviewControl.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 353);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 65);
            this.panel1.TabIndex = 8;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(96, 11);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(177, 43);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(415, 11);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(177, 43);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectMK64ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 418);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SelectMK64ImageForm";
            this.Text = "SelectMK64ImageForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlChoose.ResumeLayout(false);
            this.pnlChoose.PerformLayout();
            this.pnlView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlChoose;
        private Cereal64.Common.Controls.WatermarkTextBox txtSearchImages;
        private System.Windows.Forms.ComboBox cbImageType;
        private System.Windows.Forms.Label lblImageType;
        private System.Windows.Forms.ListBox lbImages;
        private System.Windows.Forms.Panel pnlView;
        private Cereal64.Common.Controls.ImagePreviewControl imagePreviewControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}