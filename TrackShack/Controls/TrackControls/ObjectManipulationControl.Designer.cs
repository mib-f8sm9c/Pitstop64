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
            this.label3 = new System.Windows.Forms.Label();
            this.txtTX = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTY = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTZ = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowScale = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlScaleX = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSX = new System.Windows.Forms.TextBox();
            this.pnlScaleY = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSY = new System.Windows.Forms.TextBox();
            this.pnlScaleZ = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSZ = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flowRotate = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRotateX = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRX = new System.Windows.Forms.TextBox();
            this.pnlRotateY = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRY = new System.Windows.Forms.TextBox();
            this.pnlRotateZ = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRZ = new System.Windows.Forms.TextBox();
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
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(231, 7);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 44);
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
            this.pnlBottom.Location = new System.Drawing.Point(0, 106);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(392, 54);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(314, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 44);
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
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(392, 106);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.flowTranslate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 80);
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
            this.flowTranslate.Size = new System.Drawing.Size(378, 74);
            this.flowTranslate.TabIndex = 8;
            // 
            // pnlTranslateX
            // 
            this.pnlTranslateX.Controls.Add(this.label3);
            this.pnlTranslateX.Controls.Add(this.txtTX);
            this.pnlTranslateX.Location = new System.Drawing.Point(3, 3);
            this.pnlTranslateX.Name = "pnlTranslateX";
            this.pnlTranslateX.Size = new System.Drawing.Size(115, 54);
            this.pnlTranslateX.TabIndex = 3;
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
            // txtTX
            // 
            this.txtTX.Location = new System.Drawing.Point(8, 24);
            this.txtTX.Name = "txtTX";
            this.txtTX.Size = new System.Drawing.Size(100, 20);
            this.txtTX.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtTY);
            this.panel1.Location = new System.Drawing.Point(124, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 54);
            this.panel1.TabIndex = 6;
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
            // txtTY
            // 
            this.txtTY.Location = new System.Drawing.Point(8, 24);
            this.txtTY.Name = "txtTY";
            this.txtTY.Size = new System.Drawing.Size(100, 20);
            this.txtTY.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtTZ);
            this.panel2.Location = new System.Drawing.Point(245, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(115, 54);
            this.panel2.TabIndex = 7;
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
            // txtTZ
            // 
            this.txtTZ.Location = new System.Drawing.Point(8, 24);
            this.txtTZ.Name = "txtTZ";
            this.txtTZ.Size = new System.Drawing.Size(100, 20);
            this.txtTZ.TabIndex = 2;
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
            this.pnlScaleX.Controls.Add(this.label1);
            this.pnlScaleX.Controls.Add(this.txtSX);
            this.pnlScaleX.Location = new System.Drawing.Point(3, 3);
            this.pnlScaleX.Name = "pnlScaleX";
            this.pnlScaleX.Size = new System.Drawing.Size(115, 54);
            this.pnlScaleX.TabIndex = 3;
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
            // txtSX
            // 
            this.txtSX.Location = new System.Drawing.Point(8, 24);
            this.txtSX.Name = "txtSX";
            this.txtSX.Size = new System.Drawing.Size(100, 20);
            this.txtSX.TabIndex = 0;
            // 
            // pnlScaleY
            // 
            this.pnlScaleY.Controls.Add(this.label2);
            this.pnlScaleY.Controls.Add(this.txtSY);
            this.pnlScaleY.Location = new System.Drawing.Point(124, 3);
            this.pnlScaleY.Name = "pnlScaleY";
            this.pnlScaleY.Size = new System.Drawing.Size(115, 54);
            this.pnlScaleY.TabIndex = 6;
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
            // txtSY
            // 
            this.txtSY.Location = new System.Drawing.Point(8, 24);
            this.txtSY.Name = "txtSY";
            this.txtSY.Size = new System.Drawing.Size(100, 20);
            this.txtSY.TabIndex = 1;
            // 
            // pnlScaleZ
            // 
            this.pnlScaleZ.Controls.Add(this.label12);
            this.pnlScaleZ.Controls.Add(this.txtSZ);
            this.pnlScaleZ.Location = new System.Drawing.Point(245, 3);
            this.pnlScaleZ.Name = "pnlScaleZ";
            this.pnlScaleZ.Size = new System.Drawing.Size(115, 54);
            this.pnlScaleZ.TabIndex = 7;
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
            // txtSZ
            // 
            this.txtSZ.Location = new System.Drawing.Point(8, 24);
            this.txtSZ.Name = "txtSZ";
            this.txtSZ.Size = new System.Drawing.Size(100, 20);
            this.txtSZ.TabIndex = 2;
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
            this.pnlRotateX.Controls.Add(this.label13);
            this.pnlRotateX.Controls.Add(this.txtRX);
            this.pnlRotateX.Location = new System.Drawing.Point(3, 3);
            this.pnlRotateX.Name = "pnlRotateX";
            this.pnlRotateX.Size = new System.Drawing.Size(115, 54);
            this.pnlRotateX.TabIndex = 3;
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
            // txtRX
            // 
            this.txtRX.Location = new System.Drawing.Point(8, 24);
            this.txtRX.Name = "txtRX";
            this.txtRX.Size = new System.Drawing.Size(100, 20);
            this.txtRX.TabIndex = 0;
            // 
            // pnlRotateY
            // 
            this.pnlRotateY.Controls.Add(this.label14);
            this.pnlRotateY.Controls.Add(this.txtRY);
            this.pnlRotateY.Location = new System.Drawing.Point(124, 3);
            this.pnlRotateY.Name = "pnlRotateY";
            this.pnlRotateY.Size = new System.Drawing.Size(115, 54);
            this.pnlRotateY.TabIndex = 6;
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
            // txtRY
            // 
            this.txtRY.Location = new System.Drawing.Point(8, 24);
            this.txtRY.Name = "txtRY";
            this.txtRY.Size = new System.Drawing.Size(100, 20);
            this.txtRY.TabIndex = 1;
            // 
            // pnlRotateZ
            // 
            this.pnlRotateZ.Controls.Add(this.label15);
            this.pnlRotateZ.Controls.Add(this.txtRZ);
            this.pnlRotateZ.Location = new System.Drawing.Point(245, 3);
            this.pnlRotateZ.Name = "pnlRotateZ";
            this.pnlRotateZ.Size = new System.Drawing.Size(115, 54);
            this.pnlRotateZ.TabIndex = 7;
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
            // txtRZ
            // 
            this.txtRZ.Location = new System.Drawing.Point(8, 24);
            this.txtRZ.Name = "txtRZ";
            this.txtRZ.Size = new System.Drawing.Size(100, 20);
            this.txtRZ.TabIndex = 2;
            // 
            // ObjectManipulationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlBottom);
            this.Name = "ObjectManipulationControl";
            this.Size = new System.Drawing.Size(392, 160);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTZ;
        private System.Windows.Forms.TextBox txtTY;
        private System.Windows.Forms.TextBox txtTX;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtSZ;
        private System.Windows.Forms.TextBox txtSY;
        private System.Windows.Forms.TextBox txtSX;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtRZ;
        private System.Windows.Forms.TextBox txtRY;
        private System.Windows.Forms.TextBox txtRX;
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
    }
}
