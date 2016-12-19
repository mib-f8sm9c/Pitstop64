namespace MK64Pitstop.Modules.Textures.SubControls
{
    partial class TKMKViewControl
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
            this.btnReplaceWith = new System.Windows.Forms.Button();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblSize = new System.Windows.Forms.Label();
            this.pnlTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReplaceWith
            // 
            this.btnReplaceWith.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplaceWith.Location = new System.Drawing.Point(274, 33);
            this.btnReplaceWith.Name = "btnReplaceWith";
            this.btnReplaceWith.Size = new System.Drawing.Size(125, 33);
            this.btnReplaceWith.TabIndex = 2;
            this.btnReplaceWith.Text = "Replace Image...";
            this.btnReplaceWith.UseVisualStyleBackColor = true;
            this.btnReplaceWith.Click += new System.EventHandler(this.btnReplaceWith_Click);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.lblSize);
            this.pnlTools.Controls.Add(this.lblName);
            this.pnlTools.Controls.Add(this.btnReplaceWith);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(427, 108);
            this.pnlTools.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(16, 13);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 17);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image files|*.bmp, *.png";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(16, 41);
            this.lblSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(35, 17);
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "Size";
            // 
            // TKMKViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTools);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TKMKViewControl";
            this.Size = new System.Drawing.Size(427, 108);
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReplaceWith;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblSize;


    }
}
