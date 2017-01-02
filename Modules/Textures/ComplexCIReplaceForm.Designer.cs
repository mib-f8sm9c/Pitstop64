namespace MK64Pitstop.Modules.Textures
{
    partial class ComplexCIReplaceForm
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
            this.lbBefore = new System.Windows.Forms.ListBox();
            this.lbAfter = new System.Windows.Forms.ListBox();
            this.imagePreview = new Cereal64.Common.Controls.ImagePreviewControl();
            this.lblBefore = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbBefore
            // 
            this.lbBefore.FormattingEnabled = true;
            this.lbBefore.ItemHeight = 16;
            this.lbBefore.Location = new System.Drawing.Point(12, 43);
            this.lbBefore.Name = "lbBefore";
            this.lbBefore.Size = new System.Drawing.Size(180, 228);
            this.lbBefore.TabIndex = 0;
            this.lbBefore.SelectedIndexChanged += new System.EventHandler(this.lbBefore_SelectedIndexChanged);
            // 
            // lbAfter
            // 
            this.lbAfter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAfter.FormattingEnabled = true;
            this.lbAfter.ItemHeight = 16;
            this.lbAfter.Location = new System.Drawing.Point(428, 43);
            this.lbAfter.Name = "lbAfter";
            this.lbAfter.Size = new System.Drawing.Size(180, 228);
            this.lbAfter.TabIndex = 1;
            this.lbAfter.SelectedIndexChanged += new System.EventHandler(this.lbAfter_SelectedIndexChanged);
            // 
            // imagePreview
            // 
            this.imagePreview.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imagePreview.ExportButtonVisible = true;
            this.imagePreview.Image = null;
            this.imagePreview.ImageName = "";
            this.imagePreview.ImageSize = new System.Drawing.Size(130, 130);
            this.imagePreview.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imagePreview.Location = new System.Drawing.Point(227, 43);
            this.imagePreview.LockImageSize = false;
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.OverlayImage = null;
            this.imagePreview.Size = new System.Drawing.Size(175, 155);
            this.imagePreview.TabIndex = 2;
            // 
            // lblBefore
            // 
            this.lblBefore.AutoSize = true;
            this.lblBefore.Location = new System.Drawing.Point(23, 9);
            this.lblBefore.Name = "lblBefore";
            this.lblBefore.Size = new System.Drawing.Size(50, 17);
            this.lblBefore.TabIndex = 3;
            this.lblBefore.Text = "Before";
            // 
            // lblAfter
            // 
            this.lblAfter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAfter.AutoSize = true;
            this.lblAfter.Location = new System.Drawing.Point(437, 9);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(38, 17);
            this.lblAfter.TabIndex = 4;
            this.lblAfter.Text = "After";
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplace.Enabled = false;
            this.btnReplace.Location = new System.Drawing.Point(440, 277);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(158, 43);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "Replace Texture...";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnApply.Location = new System.Drawing.Point(206, 321);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(213, 72);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply Changes";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Location = new System.Drawing.Point(220, 201);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(199, 85);
            this.lblWarning.TabIndex = 7;
            this.lblWarning.Text = "Warning: this form does\r\nnot validate MIO0 lengths!\r\nThat means that changes\r\nher" +
    "e may break the rom!\r\nBe careful and always backup!";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image files|*.bmp;*.png";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(498, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 37);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ComplexCIReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 405);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.lblAfter);
            this.Controls.Add(this.lblBefore);
            this.Controls.Add(this.imagePreview);
            this.Controls.Add(this.lbAfter);
            this.Controls.Add(this.lbBefore);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ComplexCIReplaceForm";
            this.Text = "Multiple Texture Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbBefore;
        private System.Windows.Forms.ListBox lbAfter;
        private Cereal64.Common.Controls.ImagePreviewControl imagePreview;
        private System.Windows.Forms.Label lblBefore;
        private System.Windows.Forms.Label lblAfter;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnCancel;
    }
}