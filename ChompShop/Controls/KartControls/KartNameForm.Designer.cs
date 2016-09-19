namespace ChompShop.Controls.KartControls
{
    partial class KartNameForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtKartName = new System.Windows.Forms.TextBox();
            this.gbNamePlate = new System.Windows.Forms.GroupBox();
            this.btnImportNamePlate = new System.Windows.Forms.Button();
            this.btnExportNamePlate = new System.Windows.Forms.Button();
            this.pbNamePlate = new System.Windows.Forms.PictureBox();
            this.gbNamePlate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNamePlate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kart Name";
            // 
            // txtKartName
            // 
            this.txtKartName.Location = new System.Drawing.Point(12, 92);
            this.txtKartName.Name = "txtKartName";
            this.txtKartName.Size = new System.Drawing.Size(124, 23);
            this.txtKartName.TabIndex = 1;
            // 
            // gbNamePlate
            // 
            this.gbNamePlate.Controls.Add(this.btnImportNamePlate);
            this.gbNamePlate.Controls.Add(this.btnExportNamePlate);
            this.gbNamePlate.Controls.Add(this.pbNamePlate);
            this.gbNamePlate.Location = new System.Drawing.Point(142, 12);
            this.gbNamePlate.Name = "gbNamePlate";
            this.gbNamePlate.Size = new System.Drawing.Size(120, 191);
            this.gbNamePlate.TabIndex = 6;
            this.gbNamePlate.TabStop = false;
            this.gbNamePlate.Text = "Nameplate";
            // 
            // btnImportNamePlate
            // 
            this.btnImportNamePlate.Location = new System.Drawing.Point(24, 129);
            this.btnImportNamePlate.Name = "btnImportNamePlate";
            this.btnImportNamePlate.Size = new System.Drawing.Size(78, 34);
            this.btnImportNamePlate.TabIndex = 23;
            this.btnImportNamePlate.Text = "Import";
            this.btnImportNamePlate.UseVisualStyleBackColor = true;
            // 
            // btnExportNamePlate
            // 
            this.btnExportNamePlate.Location = new System.Drawing.Point(24, 80);
            this.btnExportNamePlate.Name = "btnExportNamePlate";
            this.btnExportNamePlate.Size = new System.Drawing.Size(78, 34);
            this.btnExportNamePlate.TabIndex = 23;
            this.btnExportNamePlate.Text = "Export";
            this.btnExportNamePlate.UseVisualStyleBackColor = true;
            // 
            // pbNamePlate
            // 
            this.pbNamePlate.Location = new System.Drawing.Point(28, 31);
            this.pbNamePlate.Name = "pbNamePlate";
            this.pbNamePlate.Size = new System.Drawing.Size(70, 20);
            this.pbNamePlate.TabIndex = 1;
            this.pbNamePlate.TabStop = false;
            // 
            // KartNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 213);
            this.Controls.Add(this.gbNamePlate);
            this.Controls.Add(this.txtKartName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartNameForm";
            this.Text = "Kart Name";
            this.gbNamePlate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbNamePlate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKartName;
        private System.Windows.Forms.GroupBox gbNamePlate;
        private System.Windows.Forms.Button btnImportNamePlate;
        private System.Windows.Forms.Button btnExportNamePlate;
        private System.Windows.Forms.PictureBox pbNamePlate;
    }
}