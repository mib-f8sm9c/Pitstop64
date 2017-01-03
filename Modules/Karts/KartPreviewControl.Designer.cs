namespace Pitstop64.Modules.Karts
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
            this.cbOverlayKart.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbOverlayKart.AutoSize = true;
            this.cbOverlayKart.Enabled = false;
            this.cbOverlayKart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOverlayKart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cbOverlayKart.Image = global::Pitstop64.Properties.Resources.people_2x;
            this.cbOverlayKart.Location = new System.Drawing.Point(3, 69);
            this.cbOverlayKart.Name = "cbOverlayKart";
            this.cbOverlayKart.Size = new System.Drawing.Size(22, 22);
            this.cbOverlayKart.TabIndex = 25;
            this.toolTip.SetToolTip(this.cbOverlayKart, "Display Reference Kart");
            this.cbOverlayKart.UseVisualStyleBackColor = true;
            this.cbOverlayKart.CheckedChanged += new System.EventHandler(this.cbOverlayKart_CheckedChanged);
            // 
            // KartPreviewControl
            // 
            this.Controls.Add(this.cbOverlayKart);
            this.Name = "KartPreviewControl";
            this.Size = new System.Drawing.Size(172, 155);
            this.Controls.SetChildIndex(this.cbOverlayKart, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox cbOverlayKart;
    }
}
