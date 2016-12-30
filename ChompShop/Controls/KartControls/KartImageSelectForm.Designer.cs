namespace ChompShop.Controls.KartControls
{
    partial class KartImageSelectForm
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
            this.lbKartImages = new System.Windows.Forms.ListBox();
            this.txtSearchImages = new System.Windows.Forms.TextBox();
            this.imagePreviewControl = new Pitstop64.Modules.Karts.ImagePreviewControl();
            this.lblSelectCount = new System.Windows.Forms.Label();
            this.lblSelectedCountText = new System.Windows.Forms.Label();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lbKartImages
            // 
            this.lbKartImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKartImages.FormattingEnabled = true;
            this.lbKartImages.ItemHeight = 16;
            this.lbKartImages.Location = new System.Drawing.Point(16, 53);
            this.lbKartImages.Margin = new System.Windows.Forms.Padding(4);
            this.lbKartImages.Name = "lbKartImages";
            this.lbKartImages.ScrollAlwaysVisible = true;
            this.lbKartImages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbKartImages.Size = new System.Drawing.Size(240, 260);
            this.lbKartImages.TabIndex = 0;
            this.lbKartImages.SelectedIndexChanged += new System.EventHandler(this.lbKartImages_SelectedIndexChanged);
            // 
            // txtSearchImages
            // 
            this.txtSearchImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchImages.Location = new System.Drawing.Point(16, 15);
            this.txtSearchImages.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchImages.Name = "txtSearchImages";
            this.txtSearchImages.Size = new System.Drawing.Size(240, 23);
            this.txtSearchImages.TabIndex = 1;
            this.txtSearchImages.TextChanged += new System.EventHandler(this.txtSearchImages_TextChanged);
            // 
            // imagePreviewControl
            // 
            this.imagePreviewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePreviewControl.ExportButtonVisible = true;
            this.imagePreviewControl.Image = null;
            this.imagePreviewControl.Location = new System.Drawing.Point(264, 9);
            this.imagePreviewControl.Margin = new System.Windows.Forms.Padding(4);
            this.imagePreviewControl.Name = "imagePreviewControl";
            this.imagePreviewControl.OverlayImage = null;
            this.imagePreviewControl.Size = new System.Drawing.Size(197, 191);
            this.imagePreviewControl.TabIndex = 2;
            // 
            // lblSelectCount
            // 
            this.lblSelectCount.AutoSize = true;
            this.lblSelectCount.Location = new System.Drawing.Point(276, 203);
            this.lblSelectCount.Name = "lblSelectCount";
            this.lblSelectCount.Size = new System.Drawing.Size(116, 17);
            this.lblSelectCount.TabIndex = 3;
            this.lblSelectCount.Text = "Images Selected:";
            // 
            // lblSelectedCountText
            // 
            this.lblSelectedCountText.AutoSize = true;
            this.lblSelectedCountText.Location = new System.Drawing.Point(398, 203);
            this.lblSelectedCountText.Name = "lblSelectedCountText";
            this.lblSelectedCountText.Size = new System.Drawing.Size(16, 17);
            this.lblSelectedCountText.TabIndex = 4;
            this.lblSelectedCountText.Text = "0";
            // 
            // btnExportAll
            // 
            this.btnExportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportAll.Image = global::ChompShop.Properties.Resources.data_transfer_download_3x;
            this.btnExportAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportAll.Location = new System.Drawing.Point(264, 161);
            this.btnExportAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(128, 38);
            this.btnExportAll.TabIndex = 7;
            this.btnExportAll.Text = "Export All...";
            this.btnExportAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::ChompShop.Properties.Resources.circle_x_3x;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(264, 282);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(185, 39);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Image = global::ChompShop.Properties.Resources.circle_check_3x;
            this.btnSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelect.Location = new System.Drawing.Point(264, 235);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(185, 39);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Select";
            this.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // KartImageSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 336);
            this.Controls.Add(this.btnExportAll);
            this.Controls.Add(this.lblSelectedCountText);
            this.Controls.Add(this.lblSelectCount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtSearchImages);
            this.Controls.Add(this.lbKartImages);
            this.Controls.Add(this.imagePreviewControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartImageSelectForm";
            this.Text = "Kart Image Select";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbKartImages;
        private System.Windows.Forms.TextBox txtSearchImages;
        private Pitstop64.Modules.Karts.ImagePreviewControl imagePreviewControl;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSelectCount;
        private System.Windows.Forms.Label lblSelectedCountText;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}