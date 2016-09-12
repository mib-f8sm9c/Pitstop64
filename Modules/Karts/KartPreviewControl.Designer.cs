namespace MK64Pitstop.Modules.Karts
{
    partial class KartPreviewControl
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
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnBGColor2 = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.cbOverlayKart = new System.Windows.Forms.CheckBox();
            this.pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPreview
            // 
            this.pnlPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPreview.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlPreview.Controls.Add(this.pbPreview);
            this.pnlPreview.Location = new System.Drawing.Point(82, 12);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(130, 130);
            this.pnlPreview.TabIndex = 24;
            // 
            // pbPreview
            // 
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(130, 130);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            // 
            // btnBGColor2
            // 
            this.btnBGColor2.BackColor = System.Drawing.SystemColors.ControlText;
            this.btnBGColor2.Location = new System.Drawing.Point(27, 23);
            this.btnBGColor2.Name = "btnBGColor2";
            this.btnBGColor2.Size = new System.Drawing.Size(24, 24);
            this.btnBGColor2.TabIndex = 23;
            this.btnBGColor2.UseVisualStyleBackColor = false;
            this.btnBGColor2.Click += new System.EventHandler(this.btnBGColor2_Click);
            // 
            // cbOverlayKart
            // 
            this.cbOverlayKart.AutoSize = true;
            this.cbOverlayKart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cbOverlayKart.Location = new System.Drawing.Point(3, 72);
            this.cbOverlayKart.Name = "cbOverlayKart";
            this.cbOverlayKart.Size = new System.Drawing.Size(76, 56);
            this.cbOverlayKart.TabIndex = 25;
            this.cbOverlayKart.Text = "\r\nOverlay\r\nReference\r\nMario";
            this.cbOverlayKart.UseVisualStyleBackColor = true;
            // 
            // KartPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbOverlayKart);
            this.Controls.Add(this.pnlPreview);
            this.Controls.Add(this.btnBGColor2);
            this.Name = "KartPreviewControl";
            this.Size = new System.Drawing.Size(223, 155);
            this.pnlPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btnBGColor2;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox cbOverlayKart;
    }
}
