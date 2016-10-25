namespace MK64Pitstop.Modules.Info
{
    partial class RomInfoControl
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
            this.lblTempExp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTempExp
            // 
            this.lblTempExp.AutoSize = true;
            this.lblTempExp.Location = new System.Drawing.Point(37, 14);
            this.lblTempExp.Name = "lblTempExp";
            this.lblTempExp.Size = new System.Drawing.Size(220, 17);
            this.lblTempExp.TabIndex = 0;
            this.lblTempExp.Text = "(more to come here in the future!)";
            // 
            // RomInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTempExp);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RomInfoControl";
            this.Size = new System.Drawing.Size(501, 216);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTempExp;


    }
}
