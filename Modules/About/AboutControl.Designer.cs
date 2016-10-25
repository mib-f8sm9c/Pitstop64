namespace MK64Pitstop.Modules.About
{
    partial class AboutControl
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
            this.btnResizeRom = new System.Windows.Forms.Button();
            this.txtRomSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbDebug = new System.Windows.Forms.GroupBox();
            this.txtAbout = new System.Windows.Forms.RichTextBox();
            this.gbDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnResizeRom
            // 
            this.btnResizeRom.Enabled = false;
            this.btnResizeRom.Location = new System.Drawing.Point(20, 79);
            this.btnResizeRom.Margin = new System.Windows.Forms.Padding(4);
            this.btnResizeRom.Name = "btnResizeRom";
            this.btnResizeRom.Size = new System.Drawing.Size(137, 28);
            this.btnResizeRom.TabIndex = 0;
            this.btnResizeRom.Text = "Debug Resize";
            this.btnResizeRom.UseVisualStyleBackColor = true;
            this.btnResizeRom.Click += new System.EventHandler(this.btnResizeRom_Click);
            // 
            // txtRomSize
            // 
            this.txtRomSize.Enabled = false;
            this.txtRomSize.Location = new System.Drawing.Point(41, 50);
            this.txtRomSize.Name = "txtRomSize";
            this.txtRomSize.Size = new System.Drawing.Size(100, 23);
            this.txtRomSize.TabIndex = 1;
            this.txtRomSize.Text = "12";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resize ROM (MB)";
            // 
            // gbDebug
            // 
            this.gbDebug.Controls.Add(this.btnResizeRom);
            this.gbDebug.Controls.Add(this.label1);
            this.gbDebug.Controls.Add(this.txtRomSize);
            this.gbDebug.Location = new System.Drawing.Point(319, 3);
            this.gbDebug.Name = "gbDebug";
            this.gbDebug.Size = new System.Drawing.Size(179, 126);
            this.gbDebug.TabIndex = 3;
            this.gbDebug.TabStop = false;
            this.gbDebug.Text = "Debug Tools";
            // 
            // txtAbout
            // 
            this.txtAbout.Location = new System.Drawing.Point(3, 3);
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.ReadOnly = true;
            this.txtAbout.Size = new System.Drawing.Size(310, 210);
            this.txtAbout.TabIndex = 6;
            this.txtAbout.Text = "";
            // 
            // AboutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAbout);
            this.Controls.Add(this.gbDebug);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AboutControl";
            this.Size = new System.Drawing.Size(501, 216);
            this.gbDebug.ResumeLayout(false);
            this.gbDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnResizeRom;
        private System.Windows.Forms.TextBox txtRomSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbDebug;
        private System.Windows.Forms.RichTextBox txtAbout;

    }
}
