namespace MK64Pitstop.Modules.Textures
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
            this.cbImage = new System.Windows.Forms.ComboBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lblImageSelect = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // cbImage
            // 
            this.cbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImage.FormattingEnabled = true;
            this.cbImage.Location = new System.Drawing.Point(15, 75);
            this.cbImage.Margin = new System.Windows.Forms.Padding(4);
            this.cbImage.Name = "cbImage";
            this.cbImage.Size = new System.Drawing.Size(151, 24);
            this.cbImage.TabIndex = 0;
            this.cbImage.SelectedIndexChanged += new System.EventHandler(this.cbImage_SelectedIndexChanged);
            // 
            // pbImage
            // 
            this.pbImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImage.Location = new System.Drawing.Point(186, 3);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(375, 318);
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            // 
            // lblImageSelect
            // 
            this.lblImageSelect.AutoSize = true;
            this.lblImageSelect.Location = new System.Drawing.Point(37, 54);
            this.lblImageSelect.Name = "lblImageSelect";
            this.lblImageSelect.Size = new System.Drawing.Size(92, 17);
            this.lblImageSelect.TabIndex = 2;
            this.lblImageSelect.Text = "Image Offset:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(54, 149);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 40);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(54, 214);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 40);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "BMP files|*.bmp";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "BMP files|*.bmp";
            // 
            // TexturesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblImageSelect);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.cbImage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TexturesControl";
            this.Size = new System.Drawing.Size(564, 324);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblImageSelect;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
