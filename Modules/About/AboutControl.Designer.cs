﻿namespace MK64Pitstop.Modules.About
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
            this.txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnResizeRom
            // 
            this.btnResizeRom.Location = new System.Drawing.Point(30, 71);
            this.btnResizeRom.Margin = new System.Windows.Forms.Padding(4);
            this.btnResizeRom.Name = "btnResizeRom";
            this.btnResizeRom.Size = new System.Drawing.Size(137, 28);
            this.btnResizeRom.TabIndex = 0;
            this.btnResizeRom.Text = "Debug Resize";
            this.btnResizeRom.UseVisualStyleBackColor = true;
            this.btnResizeRom.Click += new System.EventHandler(this.btnResizeRom_Click);
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(51, 42);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(100, 23);
            this.txt.TabIndex = 1;
            this.txt.Text = "12";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resize ROM (MB)";
            // 
            // AboutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.btnResizeRom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AboutControl";
            this.Size = new System.Drawing.Size(501, 216);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnResizeRom;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Label label1;

    }
}
