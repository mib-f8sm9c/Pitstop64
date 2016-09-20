namespace ChompShop.Controls
{
    partial class ExportKartsForm
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
            this.clbKarts = new System.Windows.Forms.CheckedListBox();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.kartExportInfoControl1 = new ChompShop.Controls.KartExportInfoControl();
            this.saveKartDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // clbKarts
            // 
            this.clbKarts.FormattingEnabled = true;
            this.clbKarts.Location = new System.Drawing.Point(12, 27);
            this.clbKarts.Name = "clbKarts";
            this.clbKarts.ScrollAlwaysVisible = true;
            this.clbKarts.Size = new System.Drawing.Size(175, 202);
            this.clbKarts.TabIndex = 0;
            this.clbKarts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbKarts_ItemCheck);
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.kartExportInfoControl1);
            this.gbInfo.Location = new System.Drawing.Point(193, 12);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(241, 298);
            this.gbInfo.TabIndex = 1;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Kart Info";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(25, 248);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(133, 49);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export Kart...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // kartExportInfoControl1
            // 
            this.kartExportInfoControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kartExportInfoControl1.Location = new System.Drawing.Point(3, 19);
            this.kartExportInfoControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kartExportInfoControl1.Name = "kartExportInfoControl1";
            this.kartExportInfoControl1.Size = new System.Drawing.Size(235, 276);
            this.kartExportInfoControl1.TabIndex = 0;
            // 
            // saveKartDialog
            // 
            this.saveKartDialog.Filter = "Karts|*.karts";
            // 
            // ExportKartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 322);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.clbKarts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ExportKartsForm";
            this.Text = "Export Karts";
            this.gbInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbKarts;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Button btnExport;
        private KartExportInfoControl kartExportInfoControl1;
        private System.Windows.Forms.SaveFileDialog saveKartDialog;
    }
}