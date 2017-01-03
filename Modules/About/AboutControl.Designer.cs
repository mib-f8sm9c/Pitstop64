namespace Pitstop64.Modules.About
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportMIO0 = new System.Windows.Forms.Button();
            this.cb1MIO0Data = new System.Windows.Forms.ComboBox();
            this.txtAbout = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnResizeRom
            // 
            this.btnResizeRom.Enabled = false;
            this.btnResizeRom.Location = new System.Drawing.Point(17, 69);
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
            this.txtRomSize.Location = new System.Drawing.Point(38, 40);
            this.txtRomSize.Name = "txtRomSize";
            this.txtRomSize.Size = new System.Drawing.Size(100, 23);
            this.txtRomSize.TabIndex = 1;
            this.txtRomSize.Text = "12";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resize ROM (MB)";
            // 
            // gbDebug
            // 
            this.gbDebug.Controls.Add(this.label2);
            this.gbDebug.Controls.Add(this.btnExportMIO0);
            this.gbDebug.Controls.Add(this.cb1MIO0Data);
            this.gbDebug.Controls.Add(this.btnResizeRom);
            this.gbDebug.Controls.Add(this.label1);
            this.gbDebug.Controls.Add(this.txtRomSize);
            this.gbDebug.Location = new System.Drawing.Point(319, 3);
            this.gbDebug.Name = "gbDebug";
            this.gbDebug.Size = new System.Drawing.Size(179, 195);
            this.gbDebug.TabIndex = 3;
            this.gbDebug.TabStop = false;
            this.gbDebug.Text = "Debug Tools";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Export MIO0 Data";
            // 
            // btnExportMIO0
            // 
            this.btnExportMIO0.Location = new System.Drawing.Point(23, 160);
            this.btnExportMIO0.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportMIO0.Name = "btnExportMIO0";
            this.btnExportMIO0.Size = new System.Drawing.Size(137, 28);
            this.btnExportMIO0.TabIndex = 4;
            this.btnExportMIO0.Text = "Export";
            this.btnExportMIO0.UseVisualStyleBackColor = true;
            this.btnExportMIO0.Click += new System.EventHandler(this.btnExportMIO0_Click);
            // 
            // cb1MIO0Data
            // 
            this.cb1MIO0Data.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb1MIO0Data.FormattingEnabled = true;
            this.cb1MIO0Data.Location = new System.Drawing.Point(23, 129);
            this.cb1MIO0Data.Name = "cb1MIO0Data";
            this.cb1MIO0Data.Size = new System.Drawing.Size(131, 24);
            this.cb1MIO0Data.TabIndex = 3;
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AboutControl";
            this.Size = new System.Drawing.Size(501, 216);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtAbout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportMIO0;
        private System.Windows.Forms.ComboBox cb1MIO0Data;

    }
}
