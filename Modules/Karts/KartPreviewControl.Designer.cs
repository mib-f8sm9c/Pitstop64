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
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.cbOverlayKart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
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
            this.Controls.Add(this.cbOverlayKart);
            this.Name = "KartPreviewControl";
            this.Size = new System.Drawing.Size(223, 155);
            this.Controls.SetChildIndex(this.cbOverlayKart, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox cbOverlayKart;
    }
}
