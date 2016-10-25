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
            this.saveKartDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.kartValidityControl = new ChompShop.Controls.KartControls.KartValidityControl();
            this.clbKarts = new System.Windows.Forms.CheckedListBox();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveKartDialog
            // 
            this.saveKartDialog.Filter = "Karts|*.karts";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(35, 265);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(133, 49);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export Kart...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // gbInfo
            // 
            this.gbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfo.Controls.Add(this.kartValidityControl);
            this.gbInfo.Location = new System.Drawing.Point(200, 5);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(267, 312);
            this.gbInfo.TabIndex = 2;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Kart Info";
            // 
            // kartValidityControl
            // 
            this.kartValidityControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kartValidityControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.kartValidityControl.Location = new System.Drawing.Point(3, 19);
            this.kartValidityControl.Margin = new System.Windows.Forms.Padding(4);
            this.kartValidityControl.Name = "kartValidityControl";
            this.kartValidityControl.Size = new System.Drawing.Size(261, 290);
            this.kartValidityControl.TabIndex = 0;
            this.kartValidityControl.Visible = false;
            // 
            // clbKarts
            // 
            this.clbKarts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbKarts.FormattingEnabled = true;
            this.clbKarts.Location = new System.Drawing.Point(12, 17);
            this.clbKarts.Name = "clbKarts";
            this.clbKarts.ScrollAlwaysVisible = true;
            this.clbKarts.Size = new System.Drawing.Size(175, 238);
            this.clbKarts.TabIndex = 0;
            this.clbKarts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbKarts_ItemCheck);
            this.clbKarts.SelectedIndexChanged += new System.EventHandler(this.clbKarts_SelectedIndexChanged);
            // 
            // ExportKartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 322);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.clbKarts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ExportKartsForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Export Karts";
            this.gbInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbKarts;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveKartDialog;
        private KartControls.KartValidityControl kartValidityControl;
    }
}