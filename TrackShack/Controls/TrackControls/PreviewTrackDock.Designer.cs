namespace TrackShack.Controls.TrackControls
{
    partial class PreviewTrackDock
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
            this.openGLControl = new VisObj64.Visualization.OpenGL.OpenGLControl();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Camera = newCamera1;
            this.openGLControl.ClearColor = System.Drawing.Color.CornflowerBlue;
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.Location = new System.Drawing.Point(0, 0);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.Size = new System.Drawing.Size(439, 290);
            this.openGLControl.TabIndex = 0;
            // 
            // PreviewTrackDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.openGLControl);
            this.Name = "PreviewTrackDock";
            this.Size = new System.Drawing.Size(153, 151);
            this.ResumeLayout(false);

        }

        #endregion

        private VisObj64.Visualization.OpenGL.OpenGLControl openGLControl;
    }
}
