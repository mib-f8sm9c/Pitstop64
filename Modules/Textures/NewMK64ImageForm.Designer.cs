namespace Pitstop64.Modules.Textures
{
    partial class NewMK64ImageForm
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
            this.components = new System.ComponentModel.Container();
            this.gbFormat = new System.Windows.Forms.GroupBox();
            this.gbByte = new System.Windows.Forms.GroupBox();
            this.radFourByte = new System.Windows.Forms.RadioButton();
            this.radTwoByte = new System.Windows.Forms.RadioButton();
            this.radOneByte = new System.Windows.Forms.RadioButton();
            this.radHalfByte = new System.Windows.Forms.RadioButton();
            this.gbPalette = new System.Windows.Forms.GroupBox();
            this.cbMioPalette = new System.Windows.Forms.CheckBox();
            this.radI = new System.Windows.Forms.RadioButton();
            this.radIA = new System.Windows.Forms.RadioButton();
            this.radCI = new System.Windows.Forms.RadioButton();
            this.radRGBA = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.cbEncodeMIO0 = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbFormat.SuspendLayout();
            this.gbByte.SuspendLayout();
            this.gbPalette.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFormat
            // 
            this.gbFormat.Controls.Add(this.gbByte);
            this.gbFormat.Controls.Add(this.gbPalette);
            this.gbFormat.Controls.Add(this.radI);
            this.gbFormat.Controls.Add(this.radIA);
            this.gbFormat.Controls.Add(this.radCI);
            this.gbFormat.Controls.Add(this.radRGBA);
            this.gbFormat.Location = new System.Drawing.Point(13, 13);
            this.gbFormat.Margin = new System.Windows.Forms.Padding(4);
            this.gbFormat.Name = "gbFormat";
            this.gbFormat.Padding = new System.Windows.Forms.Padding(4);
            this.gbFormat.Size = new System.Drawing.Size(227, 219);
            this.gbFormat.TabIndex = 0;
            this.gbFormat.TabStop = false;
            this.gbFormat.Text = "Image Format";
            // 
            // gbByte
            // 
            this.gbByte.Controls.Add(this.radFourByte);
            this.gbByte.Controls.Add(this.radTwoByte);
            this.gbByte.Controls.Add(this.radOneByte);
            this.gbByte.Controls.Add(this.radHalfByte);
            this.gbByte.Location = new System.Drawing.Point(7, 138);
            this.gbByte.Name = "gbByte";
            this.gbByte.Size = new System.Drawing.Size(207, 74);
            this.gbByte.TabIndex = 5;
            this.gbByte.TabStop = false;
            this.gbByte.Text = "Pixel Size (Quality)";
            // 
            // radFourByte
            // 
            this.radFourByte.AutoSize = true;
            this.radFourByte.Location = new System.Drawing.Point(113, 47);
            this.radFourByte.Name = "radFourByte";
            this.radFourByte.Size = new System.Drawing.Size(63, 20);
            this.radFourByte.TabIndex = 11;
            this.radFourByte.Text = "4 Byte";
            this.toolTip.SetToolTip(this.radFourByte, "Each pixel stored as 4 bytes");
            this.radFourByte.UseVisualStyleBackColor = true;
            // 
            // radTwoByte
            // 
            this.radTwoByte.AutoSize = true;
            this.radTwoByte.Checked = true;
            this.radTwoByte.Location = new System.Drawing.Point(16, 47);
            this.radTwoByte.Name = "radTwoByte";
            this.radTwoByte.Size = new System.Drawing.Size(63, 20);
            this.radTwoByte.TabIndex = 10;
            this.radTwoByte.TabStop = true;
            this.radTwoByte.Text = "2 Byte";
            this.toolTip.SetToolTip(this.radTwoByte, "Each pixel stored as 2 bytes");
            this.radTwoByte.UseVisualStyleBackColor = true;
            // 
            // radOneByte
            // 
            this.radOneByte.AutoSize = true;
            this.radOneByte.Location = new System.Drawing.Point(113, 21);
            this.radOneByte.Name = "radOneByte";
            this.radOneByte.Size = new System.Drawing.Size(63, 20);
            this.radOneByte.TabIndex = 9;
            this.radOneByte.Text = "1 Byte";
            this.toolTip.SetToolTip(this.radOneByte, "Each pixel stored as 1 byte");
            this.radOneByte.UseVisualStyleBackColor = true;
            // 
            // radHalfByte
            // 
            this.radHalfByte.AutoSize = true;
            this.radHalfByte.Location = new System.Drawing.Point(16, 21);
            this.radHalfByte.Name = "radHalfByte";
            this.radHalfByte.Size = new System.Drawing.Size(74, 20);
            this.radHalfByte.TabIndex = 8;
            this.radHalfByte.Text = "1/2 Byte";
            this.toolTip.SetToolTip(this.radHalfByte, "Each pixel stored as 1/2 byte");
            this.radHalfByte.UseVisualStyleBackColor = true;
            // 
            // gbPalette
            // 
            this.gbPalette.Controls.Add(this.cbMioPalette);
            this.gbPalette.Enabled = false;
            this.gbPalette.Location = new System.Drawing.Point(7, 74);
            this.gbPalette.Name = "gbPalette";
            this.gbPalette.Size = new System.Drawing.Size(207, 58);
            this.gbPalette.TabIndex = 4;
            this.gbPalette.TabStop = false;
            this.gbPalette.Text = "Palette Settings";
            // 
            // cbMioPalette
            // 
            this.cbMioPalette.AutoSize = true;
            this.cbMioPalette.Location = new System.Drawing.Point(14, 23);
            this.cbMioPalette.Name = "cbMioPalette";
            this.cbMioPalette.Size = new System.Drawing.Size(166, 20);
            this.cbMioPalette.TabIndex = 5;
            this.cbMioPalette.Text = "Encode Palette in MIO0";
            this.toolTip.SetToolTip(this.cbMioPalette, "Saves space, but makes the palette less editable");
            this.cbMioPalette.UseVisualStyleBackColor = true;
            // 
            // radI
            // 
            this.radI.AutoSize = true;
            this.radI.Location = new System.Drawing.Point(140, 48);
            this.radI.Name = "radI";
            this.radI.Size = new System.Drawing.Size(29, 20);
            this.radI.TabIndex = 3;
            this.radI.Text = "I";
            this.toolTip.SetToolTip(this.radI, "Stores greyscale images. Transparency limited to completely transparent or comple" +
        "tely opaque");
            this.radI.UseVisualStyleBackColor = true;
            this.radI.CheckedChanged += new System.EventHandler(this.radFormatChanged);
            // 
            // radIA
            // 
            this.radIA.AutoSize = true;
            this.radIA.Location = new System.Drawing.Point(33, 48);
            this.radIA.Name = "radIA";
            this.radIA.Size = new System.Drawing.Size(38, 20);
            this.radIA.TabIndex = 2;
            this.radIA.Text = "IA";
            this.toolTip.SetToolTip(this.radIA, "Stores greyscale images with variable transparencies");
            this.radIA.UseVisualStyleBackColor = true;
            this.radIA.CheckedChanged += new System.EventHandler(this.radFormatChanged);
            // 
            // radCI
            // 
            this.radCI.AutoSize = true;
            this.radCI.Location = new System.Drawing.Point(140, 22);
            this.radCI.Name = "radCI";
            this.radCI.Size = new System.Drawing.Size(38, 20);
            this.radCI.TabIndex = 1;
            this.radCI.Text = "CI";
            this.toolTip.SetToolTip(this.radCI, "Stores palette separately from texture. Saves some space, but limits unique color" +
        " count");
            this.radCI.UseVisualStyleBackColor = true;
            this.radCI.CheckedChanged += new System.EventHandler(this.radFormatChanged);
            // 
            // radRGBA
            // 
            this.radRGBA.AutoSize = true;
            this.radRGBA.Checked = true;
            this.radRGBA.Location = new System.Drawing.Point(33, 22);
            this.radRGBA.Name = "radRGBA";
            this.radRGBA.Size = new System.Drawing.Size(64, 20);
            this.radRGBA.TabIndex = 0;
            this.radRGBA.TabStop = true;
            this.radRGBA.Text = "RGBA";
            this.toolTip.SetToolTip(this.radRGBA, "Saves each pixel individually. Max quality for largest size");
            this.radRGBA.UseVisualStyleBackColor = true;
            this.radRGBA.CheckedChanged += new System.EventHandler(this.radFormatChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(12, 265);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(126, 50);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Select Image/ Images...";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(154, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 50);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image files|*.png;*.bmp|All files|*.*";
            this.openFileDialog.Multiselect = true;
            // 
            // cbEncodeMIO0
            // 
            this.cbEncodeMIO0.AutoSize = true;
            this.cbEncodeMIO0.Location = new System.Drawing.Point(12, 239);
            this.cbEncodeMIO0.Name = "cbEncodeMIO0";
            this.cbEncodeMIO0.Size = new System.Drawing.Size(169, 20);
            this.cbEncodeMIO0.TabIndex = 3;
            this.cbEncodeMIO0.Text = "Encode Texture in MIO0";
            this.toolTip.SetToolTip(this.cbEncodeMIO0, "Saves space, but makes the image less editable");
            this.cbEncodeMIO0.UseVisualStyleBackColor = true;
            // 
            // NewMK64ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 327);
            this.Controls.Add(this.cbEncodeMIO0);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbFormat);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewMK64ImageForm";
            this.Text = "New Image Settings";
            this.gbFormat.ResumeLayout(false);
            this.gbFormat.PerformLayout();
            this.gbByte.ResumeLayout(false);
            this.gbByte.PerformLayout();
            this.gbPalette.ResumeLayout(false);
            this.gbPalette.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFormat;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.RadioButton radI;
        private System.Windows.Forms.RadioButton radIA;
        private System.Windows.Forms.RadioButton radCI;
        private System.Windows.Forms.RadioButton radRGBA;
        private System.Windows.Forms.CheckBox cbEncodeMIO0;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox gbPalette;
        private System.Windows.Forms.CheckBox cbMioPalette;
        private System.Windows.Forms.GroupBox gbByte;
        private System.Windows.Forms.RadioButton radHalfByte;
        private System.Windows.Forms.RadioButton radFourByte;
        private System.Windows.Forms.RadioButton radTwoByte;
        private System.Windows.Forms.RadioButton radOneByte;
    }
}