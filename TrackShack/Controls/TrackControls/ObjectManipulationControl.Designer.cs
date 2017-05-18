namespace TrackShack.Controls.TrackControls
{
    partial class ObjectManipulationControl
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
            this.btnApply = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowTranslate = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTranslateX = new System.Windows.Forms.Panel();
            this.numTX = new Cereal64.Common.Controls.NumericSliderInput();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numTY = new Cereal64.Common.Controls.NumericSliderInput();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numTZ = new Cereal64.Common.Controls.NumericSliderInput();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowScale = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlScaleX = new System.Windows.Forms.Panel();
            this.numSX = new Cereal64.Common.Controls.NumericSliderInput();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlScaleY = new System.Windows.Forms.Panel();
            this.numSY = new Cereal64.Common.Controls.NumericSliderInput();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlScaleZ = new System.Windows.Forms.Panel();
            this.numSZ = new Cereal64.Common.Controls.NumericSliderInput();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flowRotate = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRotateX = new System.Windows.Forms.Panel();
            this.numRX = new Cereal64.Common.Controls.NumericSliderInput();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlRotateY = new System.Windows.Forms.Panel();
            this.numRY = new Cereal64.Common.Controls.NumericSliderInput();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlRotateZ = new System.Windows.Forms.Panel();
            this.numRZ = new Cereal64.Common.Controls.NumericSliderInput();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowTranslate.SuspendLayout();
            this.pnlTranslateX.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowScale.SuspendLayout();
            this.pnlScaleX.SuspendLayout();
            this.pnlScaleY.SuspendLayout();
            this.pnlScaleZ.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.flowRotate.SuspendLayout();
            this.pnlRotateX.SuspendLayout();
            this.pnlRotateY.SuspendLayout();
            this.pnlRotateZ.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(252, 7);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(68, 44);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnApply);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 146);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(392, 54);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(326, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 44);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(392, 118);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.flowTranslate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 92);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Translate";
            // 
            // flowTranslate
            // 
            this.flowTranslate.Controls.Add(this.pnlTranslateX);
            this.flowTranslate.Controls.Add(this.panel1);
            this.flowTranslate.Controls.Add(this.panel2);
            this.flowTranslate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTranslate.Location = new System.Drawing.Point(3, 3);
            this.flowTranslate.Name = "flowTranslate";
            this.flowTranslate.Size = new System.Drawing.Size(378, 86);
            this.flowTranslate.TabIndex = 8;
            // 
            // pnlTranslateX
            // 
            this.pnlTranslateX.Controls.Add(this.numTX);
            this.pnlTranslateX.Controls.Add(this.label3);
            this.pnlTranslateX.Location = new System.Drawing.Point(3, 3);
            this.pnlTranslateX.Name = "pnlTranslateX";
            this.pnlTranslateX.Size = new System.Drawing.Size(115, 68);
            this.pnlTranslateX.TabIndex = 3;
            // 
            // numTX
            // 
            this.numTX.Decimals = 2;
            this.numTX.Location = new System.Drawing.Point(0, 20);
            this.numTX.Name = "numTX";
            this.numTX.Range = 10D;
            this.numTX.Size = new System.Drawing.Size(115, 52);
            this.numTX.TabIndex = 6;
            this.numTX.Ticks = 100;
            this.numTX.Value = 0D;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "X";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numTY);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(124, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 68);
            this.panel1.TabIndex = 6;
            // 
            // numTY
            // 
            this.numTY.Decimals = 2;
            this.numTY.Location = new System.Drawing.Point(0, 20);
            this.numTY.Name = "numTY";
            this.numTY.Range = 10D;
            this.numTY.Size = new System.Drawing.Size(115, 52);
            this.numTY.TabIndex = 7;
            this.numTY.Ticks = 100;
            this.numTY.Value = 0D;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Y";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numTZ);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(245, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(115, 68);
            this.panel2.TabIndex = 7;
            // 
            // numTZ
            // 
            this.numTZ.Decimals = 2;
            this.numTZ.Location = new System.Drawing.Point(0, 20);
            this.numTZ.Name = "numTZ";
            this.numTZ.Range = 10D;
            this.numTZ.Size = new System.Drawing.Size(115, 52);
            this.numTZ.TabIndex = 7;
            this.numTZ.Ticks = 100;
            this.numTZ.Value = 0D;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(50, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Z";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.flowScale);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 80);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Scale";
            // 
            // flowScale
            // 
            this.flowScale.Controls.Add(this.pnlScaleX);
            this.flowScale.Controls.Add(this.pnlScaleY);
            this.flowScale.Controls.Add(this.pnlScaleZ);
            this.flowScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowScale.Location = new System.Drawing.Point(3, 3);
            this.flowScale.Name = "flowScale";
            this.flowScale.Size = new System.Drawing.Size(378, 74);
            this.flowScale.TabIndex = 9;
            // 
            // pnlScaleX
            // 
            this.pnlScaleX.Controls.Add(this.numSX);
            this.pnlScaleX.Controls.Add(this.label1);
            this.pnlScaleX.Location = new System.Drawing.Point(3, 3);
            this.pnlScaleX.Name = "pnlScaleX";
            this.pnlScaleX.Size = new System.Drawing.Size(115, 68);
            this.pnlScaleX.TabIndex = 3;
            // 
            // numSX
            // 
            this.numSX.Decimals = 2;
            this.numSX.Location = new System.Drawing.Point(0, 20);
            this.numSX.Name = "numSX";
            this.numSX.Range = 10D;
            this.numSX.Size = new System.Drawing.Size(115, 52);
            this.numSX.TabIndex = 7;
            this.numSX.Ticks = 100;
            this.numSX.Value = 0D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "X";
            // 
            // pnlScaleY
            // 
            this.pnlScaleY.Controls.Add(this.numSY);
            this.pnlScaleY.Controls.Add(this.label2);
            this.pnlScaleY.Location = new System.Drawing.Point(124, 3);
            this.pnlScaleY.Name = "pnlScaleY";
            this.pnlScaleY.Size = new System.Drawing.Size(115, 68);
            this.pnlScaleY.TabIndex = 6;
            // 
            // numSY
            // 
            this.numSY.Decimals = 2;
            this.numSY.Location = new System.Drawing.Point(0, 20);
            this.numSY.Name = "numSY";
            this.numSY.Range = 10D;
            this.numSY.Size = new System.Drawing.Size(115, 52);
            this.numSY.TabIndex = 7;
            this.numSY.Ticks = 100;
            this.numSY.Value = 0D;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y";
            // 
            // pnlScaleZ
            // 
            this.pnlScaleZ.Controls.Add(this.numSZ);
            this.pnlScaleZ.Controls.Add(this.label12);
            this.pnlScaleZ.Location = new System.Drawing.Point(245, 3);
            this.pnlScaleZ.Name = "pnlScaleZ";
            this.pnlScaleZ.Size = new System.Drawing.Size(115, 68);
            this.pnlScaleZ.TabIndex = 7;
            // 
            // numSZ
            // 
            this.numSZ.Decimals = 2;
            this.numSZ.Location = new System.Drawing.Point(0, 20);
            this.numSZ.Name = "numSZ";
            this.numSZ.Range = 10D;
            this.numSZ.Size = new System.Drawing.Size(115, 52);
            this.numSZ.TabIndex = 7;
            this.numSZ.Ticks = 100;
            this.numSZ.Value = 0D;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(50, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Z";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.flowRotate);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(384, 80);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rotate";
            // 
            // flowRotate
            // 
            this.flowRotate.Controls.Add(this.pnlRotateX);
            this.flowRotate.Controls.Add(this.pnlRotateY);
            this.flowRotate.Controls.Add(this.pnlRotateZ);
            this.flowRotate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowRotate.Location = new System.Drawing.Point(3, 3);
            this.flowRotate.Name = "flowRotate";
            this.flowRotate.Size = new System.Drawing.Size(378, 74);
            this.flowRotate.TabIndex = 9;
            // 
            // pnlRotateX
            // 
            this.pnlRotateX.Controls.Add(this.numRX);
            this.pnlRotateX.Controls.Add(this.label13);
            this.pnlRotateX.Location = new System.Drawing.Point(3, 3);
            this.pnlRotateX.Name = "pnlRotateX";
            this.pnlRotateX.Size = new System.Drawing.Size(115, 68);
            this.pnlRotateX.TabIndex = 3;
            // 
            // numRX
            // 
            this.numRX.Decimals = 2;
            this.numRX.Location = new System.Drawing.Point(0, 20);
            this.numRX.Name = "numRX";
            this.numRX.Range = 180D;
            this.numRX.Size = new System.Drawing.Size(115, 52);
            this.numRX.TabIndex = 7;
            this.numRX.Ticks = 360;
            this.numRX.Value = 0D;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(50, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "X";
            // 
            // pnlRotateY
            // 
            this.pnlRotateY.Controls.Add(this.numRY);
            this.pnlRotateY.Controls.Add(this.label14);
            this.pnlRotateY.Location = new System.Drawing.Point(124, 3);
            this.pnlRotateY.Name = "pnlRotateY";
            this.pnlRotateY.Size = new System.Drawing.Size(115, 68);
            this.pnlRotateY.TabIndex = 6;
            // 
            // numRY
            // 
            this.numRY.Decimals = 2;
            this.numRY.Location = new System.Drawing.Point(0, 20);
            this.numRY.Name = "numRY";
            this.numRY.Range = 180D;
            this.numRY.Size = new System.Drawing.Size(115, 52);
            this.numRY.TabIndex = 7;
            this.numRY.Ticks = 360;
            this.numRY.Value = 0D;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(50, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Y";
            // 
            // pnlRotateZ
            // 
            this.pnlRotateZ.Controls.Add(this.numRZ);
            this.pnlRotateZ.Controls.Add(this.label15);
            this.pnlRotateZ.Location = new System.Drawing.Point(245, 3);
            this.pnlRotateZ.Name = "pnlRotateZ";
            this.pnlRotateZ.Size = new System.Drawing.Size(115, 68);
            this.pnlRotateZ.TabIndex = 7;
            // 
            // numRZ
            // 
            this.numRZ.Decimals = 2;
            this.numRZ.Location = new System.Drawing.Point(0, 20);
            this.numRZ.Name = "numRZ";
            this.numRZ.Range = 180D;
            this.numRZ.Size = new System.Drawing.Size(115, 52);
            this.numRZ.TabIndex = 7;
            this.numRZ.Ticks = 360;
            this.numRZ.Value = 0D;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(50, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Z";
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblName);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(392, 28);
            this.pnlInfo.TabIndex = 9;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(21, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 15);
            this.lblName.TabIndex = 0;
            // 
            // ObjectManipulationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlBottom);
            this.Name = "ObjectManipulationControl";
            this.Size = new System.Drawing.Size(392, 200);
            this.pnlBottom.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.flowTranslate.ResumeLayout(false);
            this.pnlTranslateX.ResumeLayout(false);
            this.pnlTranslateX.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.flowScale.ResumeLayout(false);
            this.pnlScaleX.ResumeLayout(false);
            this.pnlScaleX.PerformLayout();
            this.pnlScaleY.ResumeLayout(false);
            this.pnlScaleY.PerformLayout();
            this.pnlScaleZ.ResumeLayout(false);
            this.pnlScaleZ.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.flowRotate.ResumeLayout(false);
            this.pnlRotateX.ResumeLayout(false);
            this.pnlRotateX.PerformLayout();
            this.pnlRotateY.ResumeLayout(false);
            this.pnlRotateY.PerformLayout();
            this.pnlRotateZ.ResumeLayout(false);
            this.pnlRotateZ.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel pnlTranslateX;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel flowTranslate;
        private System.Windows.Forms.FlowLayoutPanel flowScale;
        private System.Windows.Forms.Panel pnlScaleX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlScaleY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlScaleZ;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FlowLayoutPanel flowRotate;
        private System.Windows.Forms.Panel pnlRotateX;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlRotateY;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlRotateZ;
        private System.Windows.Forms.Label label15;
        private Cereal64.Common.Controls.NumericSliderInput numTX;
        private Cereal64.Common.Controls.NumericSliderInput numTY;
        private Cereal64.Common.Controls.NumericSliderInput numTZ;
        private Cereal64.Common.Controls.NumericSliderInput numSX;
        private Cereal64.Common.Controls.NumericSliderInput numSY;
        private Cereal64.Common.Controls.NumericSliderInput numSZ;
        private Cereal64.Common.Controls.NumericSliderInput numRX;
        private Cereal64.Common.Controls.NumericSliderInput numRY;
        private Cereal64.Common.Controls.NumericSliderInput numRZ;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblName;
    }
}
