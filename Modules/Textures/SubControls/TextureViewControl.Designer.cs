namespace Pitstop64.Modules.Textures.SubControls
{
    partial class TextureViewControl
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
            this.pnlTools = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.btnEditPalette = new System.Windows.Forms.Button();
            this.btnReplaceWith = new System.Windows.Forms.Button();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblFormat = new System.Windows.Forms.Label();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.pnlTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.lblEncoding);
            this.pnlTools.Controls.Add(this.lblFormat);
            this.pnlTools.Controls.Add(this.lblError);
            this.pnlTools.Controls.Add(this.btnEditPalette);
            this.pnlTools.Controls.Add(this.btnReplaceWith);
            this.pnlTools.Controls.Add(this.lblSize);
            this.pnlTools.Controls.Add(this.lblName);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(554, 101);
            this.pnlTools.TabIndex = 5;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(16, 69);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            this.lblError.TabIndex = 9;
            // 
            // btnEditPalette
            // 
            this.btnEditPalette.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnEditPalette.Location = new System.Drawing.Point(411, 53);
            this.btnEditPalette.Name = "btnEditPalette";
            this.btnEditPalette.Size = new System.Drawing.Size(125, 33);
            this.btnEditPalette.TabIndex = 8;
            this.btnEditPalette.Text = "Edit Palette...";
            this.btnEditPalette.UseVisualStyleBackColor = true;
            this.btnEditPalette.Click += new System.EventHandler(this.btnEditPalette_Click);
            // 
            // btnReplaceWith
            // 
            this.btnReplaceWith.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplaceWith.Location = new System.Drawing.Point(411, 13);
            this.btnReplaceWith.Name = "btnReplaceWith";
            this.btnReplaceWith.Size = new System.Drawing.Size(125, 33);
            this.btnReplaceWith.TabIndex = 7;
            this.btnReplaceWith.Text = "Replace Image...";
            this.btnReplaceWith.UseVisualStyleBackColor = true;
            this.btnReplaceWith.Click += new System.EventHandler(this.btnReplaceWith_Click);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(16, 41);
            this.lblSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(35, 17);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "Size";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(16, 13);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 17);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image files|*.bmp;*.png";
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(16, 69);
            this.lblFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(52, 17);
            this.lblFormat.TabIndex = 10;
            this.lblFormat.Text = "Format";
            // 
            // lblEncoding
            // 
            this.lblEncoding.AutoSize = true;
            this.lblEncoding.Location = new System.Drawing.Point(119, 41);
            this.lblEncoding.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(67, 17);
            this.lblEncoding.TabIndex = 11;
            this.lblEncoding.Text = "Encoding";
            // 
            // TextureViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTools);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TextureViewControl";
            this.Size = new System.Drawing.Size(554, 101);
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Button btnReplaceWith;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnEditPalette;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label lblEncoding;
    }
}
