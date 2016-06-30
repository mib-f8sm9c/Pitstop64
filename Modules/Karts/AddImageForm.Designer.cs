namespace MK64Pitstop.Modules.Karts
{
    partial class AddImageForm
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
            this.btnAddNewImage = new System.Windows.Forms.Button();
            this.lbAdded = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlKartImage = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.openImagesDialog = new System.Windows.Forms.OpenFileDialog();
            this.cbMultiSelect = new System.Windows.Forms.CheckBox();
            this.pnlKartImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNewImage
            // 
            this.btnAddNewImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewImage.Enabled = false;
            this.btnAddNewImage.Location = new System.Drawing.Point(18, 200);
            this.btnAddNewImage.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddNewImage.Name = "btnAddNewImage";
            this.btnAddNewImage.Size = new System.Drawing.Size(409, 29);
            this.btnAddNewImage.TabIndex = 4;
            this.btnAddNewImage.Text = "New Image...";
            this.btnAddNewImage.UseVisualStyleBackColor = true;
            this.btnAddNewImage.Click += new System.EventHandler(this.btnAddNewImage_Click);
            // 
            // lbAdded
            // 
            this.lbAdded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAdded.FormattingEnabled = true;
            this.lbAdded.ItemHeight = 16;
            this.lbAdded.Location = new System.Drawing.Point(18, 12);
            this.lbAdded.Name = "lbAdded";
            this.lbAdded.ScrollAlwaysVisible = true;
            this.lbAdded.Size = new System.Drawing.Size(337, 180);
            this.lbAdded.TabIndex = 0;
            this.lbAdded.SelectedIndexChanged += new System.EventHandler(this.lbAdded_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(268, 324);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 48);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(48, 324);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(133, 48);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(18, 278);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(409, 29);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset From Base Set...";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlKartImage
            // 
            this.pnlKartImage.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlKartImage.Controls.Add(this.pbImage);
            this.pnlKartImage.Location = new System.Drawing.Point(361, 75);
            this.pnlKartImage.Name = "pnlKartImage";
            this.pnlKartImage.Size = new System.Drawing.Size(66, 66);
            this.pnlKartImage.TabIndex = 11;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.Transparent;
            this.pbImage.Location = new System.Drawing.Point(1, 1);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(64, 64);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 11;
            this.pbImage.TabStop = false;
            // 
            // openImageDialog
            // 
            this.openImageDialog.FileName = "openFileDialog";
            this.openImageDialog.Filter = "BMP/PNG files|*.bmp;*.png|All files|*.*";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(18, 239);
            this.btnExport.Margin = new System.Windows.Forms.Padding(5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(409, 29);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "Export Image...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "BMP/PNG files|*.bmp;*.png|All files|*.*";
            // 
            // openImagesDialog
            // 
            this.openImagesDialog.FileName = "openFileDialog";
            this.openImagesDialog.Filter = "BMP/PNG files|*.bmp;*.png|All files|*.*";
            this.openImagesDialog.Multiselect = true;
            // 
            // cbMultiSelect
            // 
            this.cbMultiSelect.AutoSize = true;
            this.cbMultiSelect.Location = new System.Drawing.Point(362, 21);
            this.cbMultiSelect.Name = "cbMultiSelect";
            this.cbMultiSelect.Size = new System.Drawing.Size(65, 36);
            this.cbMultiSelect.TabIndex = 13;
            this.cbMultiSelect.Text = "Multi-\r\nSelect";
            this.cbMultiSelect.UseVisualStyleBackColor = true;
            this.cbMultiSelect.CheckedChanged += new System.EventHandler(this.cbMultiSelect_CheckedChanged);
            // 
            // AddImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 388);
            this.Controls.Add(this.cbMultiSelect);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.pnlKartImage);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnAddNewImage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbAdded);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddImageForm";
            this.Text = "AddImageForm";
            this.pnlKartImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lbAdded;
        private System.Windows.Forms.Button btnAddNewImage;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlKartImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.OpenFileDialog openImagesDialog;
        private System.Windows.Forms.CheckBox cbMultiSelect;
    }
}