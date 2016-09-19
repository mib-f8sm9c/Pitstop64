namespace ChompShop.Controls.KartControls
{
    partial class KartPortraitsForm
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
            this.gbKartPortraits = new System.Windows.Forms.GroupBox();
            this.btnImportPortrait = new System.Windows.Forms.Button();
            this.btnExportPortrait = new System.Windows.Forms.Button();
            this.pnlPortrait = new System.Windows.Forms.Panel();
            this.pbPortrait = new System.Windows.Forms.PictureBox();
            this.btnBGColor2 = new System.Windows.Forms.Button();
            this.txtPortraitNum = new System.Windows.Forms.TextBox();
            this.btnNextPortrait = new System.Windows.Forms.Button();
            this.btnPrevPortrait = new System.Windows.Forms.Button();
            this.gbKartPortraits.SuspendLayout();
            this.pnlPortrait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // gbKartPortraits
            // 
            this.gbKartPortraits.Controls.Add(this.btnImportPortrait);
            this.gbKartPortraits.Controls.Add(this.btnExportPortrait);
            this.gbKartPortraits.Controls.Add(this.pnlPortrait);
            this.gbKartPortraits.Controls.Add(this.btnBGColor2);
            this.gbKartPortraits.Controls.Add(this.txtPortraitNum);
            this.gbKartPortraits.Controls.Add(this.btnNextPortrait);
            this.gbKartPortraits.Controls.Add(this.btnPrevPortrait);
            this.gbKartPortraits.Location = new System.Drawing.Point(13, 13);
            this.gbKartPortraits.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbKartPortraits.Name = "gbKartPortraits";
            this.gbKartPortraits.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbKartPortraits.Size = new System.Drawing.Size(324, 315);
            this.gbKartPortraits.TabIndex = 3;
            this.gbKartPortraits.TabStop = false;
            this.gbKartPortraits.Text = "Portraits";
            // 
            // btnImportPortrait
            // 
            this.btnImportPortrait.Location = new System.Drawing.Point(195, 210);
            this.btnImportPortrait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImportPortrait.Name = "btnImportPortrait";
            this.btnImportPortrait.Size = new System.Drawing.Size(104, 42);
            this.btnImportPortrait.TabIndex = 22;
            this.btnImportPortrait.Text = "Import";
            this.btnImportPortrait.UseVisualStyleBackColor = true;
            // 
            // btnExportPortrait
            // 
            this.btnExportPortrait.Location = new System.Drawing.Point(28, 210);
            this.btnExportPortrait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportPortrait.Name = "btnExportPortrait";
            this.btnExportPortrait.Size = new System.Drawing.Size(104, 42);
            this.btnExportPortrait.TabIndex = 21;
            this.btnExportPortrait.Text = "Export";
            this.btnExportPortrait.UseVisualStyleBackColor = true;
            // 
            // pnlPortrait
            // 
            this.pnlPortrait.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlPortrait.Controls.Add(this.pbPortrait);
            this.pnlPortrait.Location = new System.Drawing.Point(83, 36);
            this.pnlPortrait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPortrait.Name = "pnlPortrait";
            this.pnlPortrait.Size = new System.Drawing.Size(173, 160);
            this.pnlPortrait.TabIndex = 20;
            // 
            // pbPortrait
            // 
            this.pbPortrait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPortrait.Location = new System.Drawing.Point(0, 0);
            this.pbPortrait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbPortrait.Name = "pbPortrait";
            this.pbPortrait.Size = new System.Drawing.Size(173, 160);
            this.pbPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPortrait.TabIndex = 0;
            this.pbPortrait.TabStop = false;
            // 
            // btnBGColor2
            // 
            this.btnBGColor2.BackColor = System.Drawing.SystemColors.ControlText;
            this.btnBGColor2.Location = new System.Drawing.Point(24, 49);
            this.btnBGColor2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBGColor2.Name = "btnBGColor2";
            this.btnBGColor2.Size = new System.Drawing.Size(32, 30);
            this.btnBGColor2.TabIndex = 19;
            this.btnBGColor2.UseVisualStyleBackColor = false;
            // 
            // txtPortraitNum
            // 
            this.txtPortraitNum.Location = new System.Drawing.Point(136, 267);
            this.txtPortraitNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPortraitNum.Name = "txtPortraitNum";
            this.txtPortraitNum.ReadOnly = true;
            this.txtPortraitNum.Size = new System.Drawing.Size(49, 23);
            this.txtPortraitNum.TabIndex = 3;
            // 
            // btnNextPortrait
            // 
            this.btnNextPortrait.Location = new System.Drawing.Point(195, 266);
            this.btnNextPortrait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNextPortrait.Name = "btnNextPortrait";
            this.btnNextPortrait.Size = new System.Drawing.Size(31, 30);
            this.btnNextPortrait.TabIndex = 2;
            this.btnNextPortrait.Text = ">";
            this.btnNextPortrait.UseVisualStyleBackColor = true;
            // 
            // btnPrevPortrait
            // 
            this.btnPrevPortrait.Location = new System.Drawing.Point(97, 266);
            this.btnPrevPortrait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrevPortrait.Name = "btnPrevPortrait";
            this.btnPrevPortrait.Size = new System.Drawing.Size(31, 30);
            this.btnPrevPortrait.TabIndex = 1;
            this.btnPrevPortrait.Text = "<";
            this.btnPrevPortrait.UseVisualStyleBackColor = true;
            // 
            // KartPortraitsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 339);
            this.Controls.Add(this.gbKartPortraits);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "KartPortraitsForm";
            this.Text = "Kart Portraits";
            this.gbKartPortraits.ResumeLayout(false);
            this.gbKartPortraits.PerformLayout();
            this.pnlPortrait.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPortrait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbKartPortraits;
        private System.Windows.Forms.Button btnImportPortrait;
        private System.Windows.Forms.Button btnExportPortrait;
        private System.Windows.Forms.Panel pnlPortrait;
        private System.Windows.Forms.PictureBox pbPortrait;
        private System.Windows.Forms.Button btnBGColor2;
        private System.Windows.Forms.TextBox txtPortraitNum;
        private System.Windows.Forms.Button btnNextPortrait;
        private System.Windows.Forms.Button btnPrevPortrait;
    }
}