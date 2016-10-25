namespace ChompShop.Controls
{
    partial class ReferenceKartForm
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
            this.lbKarts = new System.Windows.Forms.ListBox();
            this.radUseFile = new System.Windows.Forms.RadioButton();
            this.radUseKart = new System.Windows.Forms.RadioButton();
            this.radDontUse = new System.Windows.Forms.RadioButton();
            this.btnSaveRef = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveKartDialog
            // 
            this.saveKartDialog.Filter = "Karts|*.karts";
            // 
            // lbKarts
            // 
            this.lbKarts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKarts.Enabled = false;
            this.lbKarts.FormattingEnabled = true;
            this.lbKarts.ItemHeight = 16;
            this.lbKarts.Location = new System.Drawing.Point(35, 63);
            this.lbKarts.Margin = new System.Windows.Forms.Padding(4);
            this.lbKarts.Name = "lbKarts";
            this.lbKarts.ScrollAlwaysVisible = true;
            this.lbKarts.Size = new System.Drawing.Size(157, 164);
            this.lbKarts.TabIndex = 2;
            this.lbKarts.SelectedIndexChanged += new System.EventHandler(this.lbKarts_SelectedIndexChanged);
            // 
            // radUseFile
            // 
            this.radUseFile.AutoSize = true;
            this.radUseFile.Location = new System.Drawing.Point(8, 10);
            this.radUseFile.Name = "radUseFile";
            this.radUseFile.Size = new System.Drawing.Size(121, 21);
            this.radUseFile.TabIndex = 0;
            this.radUseFile.TabStop = true;
            this.radUseFile.Text = "Load from Disk";
            this.radUseFile.UseVisualStyleBackColor = true;
            this.radUseFile.CheckedChanged += new System.EventHandler(this.radCheckedChanged);
            // 
            // radUseKart
            // 
            this.radUseKart.AutoSize = true;
            this.radUseKart.Location = new System.Drawing.Point(8, 37);
            this.radUseKart.Name = "radUseKart";
            this.radUseKart.Size = new System.Drawing.Size(133, 21);
            this.radUseKart.TabIndex = 1;
            this.radUseKart.TabStop = true;
            this.radUseKart.Text = "Use Existing Kart";
            this.radUseKart.UseVisualStyleBackColor = true;
            this.radUseKart.CheckedChanged += new System.EventHandler(this.radCheckedChanged);
            // 
            // radDontUse
            // 
            this.radDontUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radDontUse.AutoSize = true;
            this.radDontUse.Location = new System.Drawing.Point(8, 288);
            this.radDontUse.Name = "radDontUse";
            this.radDontUse.Size = new System.Drawing.Size(144, 21);
            this.radDontUse.TabIndex = 4;
            this.radDontUse.TabStop = true;
            this.radDontUse.Text = "No Reference Kart";
            this.radDontUse.UseVisualStyleBackColor = true;
            this.radDontUse.CheckedChanged += new System.EventHandler(this.radCheckedChanged);
            // 
            // btnSaveRef
            // 
            this.btnSaveRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveRef.Location = new System.Drawing.Point(35, 250);
            this.btnSaveRef.Name = "btnSaveRef";
            this.btnSaveRef.Size = new System.Drawing.Size(117, 32);
            this.btnSaveRef.TabIndex = 3;
            this.btnSaveRef.Text = "Save to Disk";
            this.btnSaveRef.UseVisualStyleBackColor = true;
            this.btnSaveRef.Click += new System.EventHandler(this.btnSaveRef_Click);
            // 
            // ReferenceKartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 323);
            this.Controls.Add(this.btnSaveRef);
            this.Controls.Add(this.radDontUse);
            this.Controls.Add(this.radUseKart);
            this.Controls.Add(this.radUseFile);
            this.Controls.Add(this.lbKarts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReferenceKartForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Reference Kart";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveKartDialog;
        private System.Windows.Forms.ListBox lbKarts;
        private System.Windows.Forms.RadioButton radUseFile;
        private System.Windows.Forms.RadioButton radUseKart;
        private System.Windows.Forms.RadioButton radDontUse;
        private System.Windows.Forms.Button btnSaveRef;
    }
}