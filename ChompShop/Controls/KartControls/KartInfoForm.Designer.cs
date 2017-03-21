namespace ChompShop.Controls.KartControls
{
    partial class KartInfoForm
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
            this.openNamePlateDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveNamePlateDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbMiniIcon = new System.Windows.Forms.GroupBox();
            this.btnImportIcon = new System.Windows.Forms.Button();
            this.btnExportIcon = new System.Windows.Forms.Button();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.gbMiniPortrait = new System.Windows.Forms.GroupBox();
            this.btnImportPortrait = new System.Windows.Forms.Button();
            this.btnExportPortrait = new System.Windows.Forms.Button();
            this.pbPortrait = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbNamePlate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNamePlate)).BeginInit();
            this.gbMiniIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.gbMiniPortrait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortrait)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kart Name";
            // 
            // txtKartName
            // 
            this.txtKartName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtKartName.Location = new System.Drawing.Point(131, 28);
            this.txtKartName.Name = "txtKartName";
            this.txtKartName.Size = new System.Drawing.Size(124, 23);
            this.txtKartName.TabIndex = 0;
            this.txtKartName.TextChanged += new System.EventHandler(this.txtKartName_TextChanged);
            // 
            // gbNamePlate
            // 
            this.gbNamePlate.Controls.Add(this.btnImportNamePlate);
            this.gbNamePlate.Controls.Add(this.btnExportNamePlate);
            this.gbNamePlate.Controls.Add(this.pbNamePlate);
            this.gbNamePlate.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbNamePlate.Location = new System.Drawing.Point(0, 0);
            this.gbNamePlate.Name = "gbNamePlate";
            this.gbNamePlate.Size = new System.Drawing.Size(120, 145);
            this.gbNamePlate.TabIndex = 2;
            this.gbNamePlate.TabStop = false;
            this.gbNamePlate.Text = "Nameplate";
            // 
            // btnImportNamePlate
            // 
            this.btnImportNamePlate.Location = new System.Drawing.Point(25, 105);
            this.btnImportNamePlate.Name = "btnImportNamePlate";
            this.btnImportNamePlate.Size = new System.Drawing.Size(78, 34);
            this.btnImportNamePlate.TabIndex = 1;
            this.btnImportNamePlate.Text = "Import";
            this.btnImportNamePlate.UseVisualStyleBackColor = true;
            this.btnImportNamePlate.Click += new System.EventHandler(this.btnImportNamePlate_Click);
            // 
            // btnExportNamePlate
            // 
            this.btnExportNamePlate.Location = new System.Drawing.Point(25, 65);
            this.btnExportNamePlate.Name = "btnExportNamePlate";
            this.btnExportNamePlate.Size = new System.Drawing.Size(78, 34);
            this.btnExportNamePlate.TabIndex = 0;
            this.btnExportNamePlate.Text = "Export";
            this.btnExportNamePlate.UseVisualStyleBackColor = true;
            this.btnExportNamePlate.Click += new System.EventHandler(this.btnExportNamePlate_Click);
            // 
            // pbNamePlate
            // 
            this.pbNamePlate.Location = new System.Drawing.Point(28, 24);
            this.pbNamePlate.Name = "pbNamePlate";
            this.pbNamePlate.Size = new System.Drawing.Size(70, 20);
            this.pbNamePlate.TabIndex = 1;
            this.pbNamePlate.TabStop = false;
            // 
            // openNamePlateDialog
            // 
            this.openNamePlateDialog.Filter = "PNG files|*.png|All files|*.*";
            // 
            // saveNamePlateDialog
            // 
            this.saveNamePlateDialog.Filter = "PNG files|*.png|All files|*.*";
            // 
            // gbMiniIcon
            // 
            this.gbMiniIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbMiniIcon.Controls.Add(this.btnImportIcon);
            this.gbMiniIcon.Controls.Add(this.btnExportIcon);
            this.gbMiniIcon.Controls.Add(this.pbIcon);
            this.gbMiniIcon.Location = new System.Drawing.Point(128, 0);
            this.gbMiniIcon.Name = "gbMiniIcon";
            this.gbMiniIcon.Size = new System.Drawing.Size(120, 145);
            this.gbMiniIcon.TabIndex = 3;
            this.gbMiniIcon.TabStop = false;
            this.gbMiniIcon.Text = "Kart Icon";
            // 
            // btnImportIcon
            // 
            this.btnImportIcon.Location = new System.Drawing.Point(25, 105);
            this.btnImportIcon.Name = "btnImportIcon";
            this.btnImportIcon.Size = new System.Drawing.Size(78, 34);
            this.btnImportIcon.TabIndex = 1;
            this.btnImportIcon.Text = "Import";
            this.btnImportIcon.UseVisualStyleBackColor = true;
            this.btnImportIcon.Click += new System.EventHandler(this.btnImportIcon_Click);
            // 
            // btnExportIcon
            // 
            this.btnExportIcon.Location = new System.Drawing.Point(25, 65);
            this.btnExportIcon.Name = "btnExportIcon";
            this.btnExportIcon.Size = new System.Drawing.Size(78, 34);
            this.btnExportIcon.TabIndex = 0;
            this.btnExportIcon.Text = "Export";
            this.btnExportIcon.UseVisualStyleBackColor = true;
            this.btnExportIcon.Click += new System.EventHandler(this.btnExportIcon_Click);
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(53, 24);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(20, 20);
            this.pbIcon.TabIndex = 1;
            this.pbIcon.TabStop = false;
            // 
            // gbMiniPortrait
            // 
            this.gbMiniPortrait.Controls.Add(this.btnImportPortrait);
            this.gbMiniPortrait.Controls.Add(this.btnExportPortrait);
            this.gbMiniPortrait.Controls.Add(this.pbPortrait);
            this.gbMiniPortrait.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbMiniPortrait.Location = new System.Drawing.Point(259, 0);
            this.gbMiniPortrait.Name = "gbMiniPortrait";
            this.gbMiniPortrait.Size = new System.Drawing.Size(120, 145);
            this.gbMiniPortrait.TabIndex = 4;
            this.gbMiniPortrait.TabStop = false;
            this.gbMiniPortrait.Text = "Mini Portrait";
            // 
            // btnImportPortrait
            // 
            this.btnImportPortrait.Location = new System.Drawing.Point(24, 105);
            this.btnImportPortrait.Name = "btnImportPortrait";
            this.btnImportPortrait.Size = new System.Drawing.Size(78, 34);
            this.btnImportPortrait.TabIndex = 1;
            this.btnImportPortrait.Text = "Import";
            this.btnImportPortrait.UseVisualStyleBackColor = true;
            this.btnImportPortrait.Click += new System.EventHandler(this.btnImportPortrait_Click);
            // 
            // btnExportPortrait
            // 
            this.btnExportPortrait.Location = new System.Drawing.Point(24, 65);
            this.btnExportPortrait.Name = "btnExportPortrait";
            this.btnExportPortrait.Size = new System.Drawing.Size(78, 34);
            this.btnExportPortrait.TabIndex = 0;
            this.btnExportPortrait.Text = "Export";
            this.btnExportPortrait.UseVisualStyleBackColor = true;
            this.btnExportPortrait.Click += new System.EventHandler(this.btnExportPortrait_Click);
            // 
            // pbPortrait
            // 
            this.pbPortrait.Location = new System.Drawing.Point(42, 22);
            this.pbPortrait.Name = "pbPortrait";
            this.pbPortrait.Size = new System.Drawing.Size(40, 40);
            this.pbPortrait.TabIndex = 1;
            this.pbPortrait.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbMiniIcon);
            this.panel1.Controls.Add(this.gbNamePlate);
            this.panel1.Controls.Add(this.gbMiniPortrait);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(5, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 145);
            this.panel1.TabIndex = 5;
            // 
            // KartInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 229);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtKartName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartInfoForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Kart Info";
            this.gbNamePlate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbNamePlate)).EndInit();
            this.gbMiniIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.gbMiniPortrait.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPortrait)).EndInit();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.OpenFileDialog openNamePlateDialog;
        private System.Windows.Forms.SaveFileDialog saveNamePlateDialog;
        private System.Windows.Forms.GroupBox gbMiniIcon;
        private System.Windows.Forms.Button btnImportIcon;
        private System.Windows.Forms.Button btnExportIcon;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.GroupBox gbMiniPortrait;
        private System.Windows.Forms.Button btnImportPortrait;
        private System.Windows.Forms.Button btnExportPortrait;
        private System.Windows.Forms.PictureBox pbPortrait;
        private System.Windows.Forms.Panel panel1;
    }
}