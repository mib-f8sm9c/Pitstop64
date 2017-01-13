namespace Pitstop64.Modules.Info
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
            this.gbDebug = new System.Windows.Forms.GroupBox();
            this.btnResizeRom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRomSize = new System.Windows.Forms.TextBox();
            this.btnExportMIO0 = new System.Windows.Forms.Button();
            this.cb1MIO0Data = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbDebug.SuspendLayout();
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
            // gbDebug
            // 
            this.gbDebug.Controls.Add(this.label2);
            this.gbDebug.Controls.Add(this.btnExportMIO0);
            this.gbDebug.Controls.Add(this.cb1MIO0Data);
            this.gbDebug.Controls.Add(this.btnResizeRom);
            this.gbDebug.Controls.Add(this.label1);
            this.gbDebug.Controls.Add(this.txtRomSize);
            this.gbDebug.Location = new System.Drawing.Point(40, 47);
            this.gbDebug.Name = "gbDebug";
            this.gbDebug.Size = new System.Drawing.Size(173, 225);
            this.gbDebug.TabIndex = 4;
            this.gbDebug.TabStop = false;
            this.gbDebug.Text = "Debug Tools";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resize ROM (MB)";
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
            // btnExportMIO0
            // 
            this.btnExportMIO0.Location = new System.Drawing.Point(20, 179);
            this.btnExportMIO0.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportMIO0.Name = "btnExportMIO0";
            this.btnExportMIO0.Size = new System.Drawing.Size(137, 28);
            this.btnExportMIO0.TabIndex = 6;
            this.btnExportMIO0.Text = "Export";
            this.btnExportMIO0.UseVisualStyleBackColor = true;
            // 
            // cb1MIO0Data
            // 
            this.cb1MIO0Data.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb1MIO0Data.FormattingEnabled = true;
            this.cb1MIO0Data.Location = new System.Drawing.Point(20, 148);
            this.cb1MIO0Data.Name = "cb1MIO0Data";
            this.cb1MIO0Data.Size = new System.Drawing.Size(131, 24);
            this.cb1MIO0Data.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(44, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Export MIO0";
            // 
            // RomInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbDebug);
            this.Controls.Add(this.lblTempExp);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RomInfoControl";
            this.Size = new System.Drawing.Size(501, 296);
            this.gbDebug.ResumeLayout(false);
            this.gbDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTempExp;
        private System.Windows.Forms.GroupBox gbDebug;
        private System.Windows.Forms.Button btnResizeRom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRomSize;
        private System.Windows.Forms.Button btnExportMIO0;
        private System.Windows.Forms.ComboBox cb1MIO0Data;
        private System.Windows.Forms.Label label2;


    }
}
