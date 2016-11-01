namespace MK64Pitstop.Modules.Tracks
{
    partial class TrackControl
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
            Cereal64.VisObj64.Visualization.OpenGL.Cameras.NewCamera newCamera1 = new Cereal64.VisObj64.Visualization.OpenGL.Cameras.NewCamera();
            this.gbTrackView = new System.Windows.Forms.GroupBox();
            this.openGLControl = new VisObj64.Visualization.OpenGL.OpenGLControl();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cbTrack = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbTrackView.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTrackView
            // 
            this.gbTrackView.Controls.Add(this.openGLControl);
            this.gbTrackView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTrackView.Location = new System.Drawing.Point(145, 4);
            this.gbTrackView.Name = "gbTrackView";
            this.gbTrackView.Padding = new System.Windows.Forms.Padding(10, 4, 10, 10);
            this.gbTrackView.Size = new System.Drawing.Size(448, 359);
            this.gbTrackView.TabIndex = 1;
            this.gbTrackView.TabStop = false;
            this.gbTrackView.Text = "Track View";
            // 
            // openGLControl
            // 
            this.openGLControl.Camera = newCamera1;
            this.openGLControl.ClearColor = System.Drawing.Color.CornflowerBlue;
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.Location = new System.Drawing.Point(10, 20);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.Size = new System.Drawing.Size(428, 329);
            this.openGLControl.TabIndex = 0;
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.lblWarning);
            this.gbSettings.Controls.Add(this.btnLoad);
            this.gbSettings.Controls.Add(this.cbTrack);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbSettings.Location = new System.Drawing.Point(4, 4);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(141, 359);
            this.gbSettings.TabIndex = 2;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Location = new System.Drawing.Point(11, 116);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(120, 119);
            this.lblWarning.TabIndex = 3;
            this.lblWarning.Text = "WARNING:\r\nIncomplete code\r\nhere. May corrupt\r\nyour project, so \r\ndon\'t use if you" +
    " \r\nwant to save/\r\nexport your ROM!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(18, 70);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(114, 40);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load Track";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbTrack
            // 
            this.cbTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrack.Enabled = false;
            this.cbTrack.FormattingEnabled = true;
            this.cbTrack.Location = new System.Drawing.Point(6, 40);
            this.cbTrack.Name = "cbTrack";
            this.cbTrack.Size = new System.Drawing.Size(129, 24);
            this.cbTrack.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Track";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 102);
            this.label2.TabIndex = 4;
            this.label2.Text = "After loading a\r\ntrack, click and\r\nmove the mouse to\r\nactivate it. Only load\r\non" +
    "e track per\r\nprogram";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TrackControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTrackView);
            this.Controls.Add(this.gbSettings);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrackControl";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(597, 367);
            this.gbTrackView.ResumeLayout(false);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private VisObj64.Visualization.OpenGL.OpenGLControl openGLControl;
        private System.Windows.Forms.GroupBox gbTrackView;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTrack;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label label2;


    }
}
