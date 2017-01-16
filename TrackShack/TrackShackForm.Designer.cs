namespace TrackShack
{
    partial class TrackShackForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTrackAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackConstructorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(6, 4, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(997, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTrackToolStripMenuItem,
            this.loadTrackToolStripMenuItem,
            this.saveTrackToolStripMenuItem,
            this.saveTrackAsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackInfoToolStripMenuItem,
            this.previewTrackToolStripMenuItem,
            this.elementEditorToolStripMenuItem,
            this.trackConstructorToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 19);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // newTrackToolStripMenuItem
            // 
            this.newTrackToolStripMenuItem.Name = "newTrackToolStripMenuItem";
            this.newTrackToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newTrackToolStripMenuItem.Text = "New Track";
            // 
            // loadTrackToolStripMenuItem
            // 
            this.loadTrackToolStripMenuItem.Name = "loadTrackToolStripMenuItem";
            this.loadTrackToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadTrackToolStripMenuItem.Text = "Load Track...";
            // 
            // saveTrackToolStripMenuItem
            // 
            this.saveTrackToolStripMenuItem.Name = "saveTrackToolStripMenuItem";
            this.saveTrackToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveTrackToolStripMenuItem.Text = "Save Track";
            // 
            // saveTrackAsToolStripMenuItem
            // 
            this.saveTrackAsToolStripMenuItem.Name = "saveTrackAsToolStripMenuItem";
            this.saveTrackAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveTrackAsToolStripMenuItem.Text = "Save Track As...";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // trackInfoToolStripMenuItem
            // 
            this.trackInfoToolStripMenuItem.Name = "trackInfoToolStripMenuItem";
            this.trackInfoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.trackInfoToolStripMenuItem.Text = "Track Info";
            // 
            // previewTrackToolStripMenuItem
            // 
            this.previewTrackToolStripMenuItem.Name = "previewTrackToolStripMenuItem";
            this.previewTrackToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.previewTrackToolStripMenuItem.Text = "Preview Track";
            // 
            // elementEditorToolStripMenuItem
            // 
            this.elementEditorToolStripMenuItem.Name = "elementEditorToolStripMenuItem";
            this.elementEditorToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.elementEditorToolStripMenuItem.Text = "Element Editor";
            // 
            // trackConstructorToolStripMenuItem
            // 
            this.trackConstructorToolStripMenuItem.Name = "trackConstructorToolStripMenuItem";
            this.trackConstructorToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.trackConstructorToolStripMenuItem.Text = "Track Constructor";
            // 
            // TrackShackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(997, 455);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TrackShackForm";
            this.Text = "Track Shack";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTrackAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackConstructorToolStripMenuItem;
    }
}

