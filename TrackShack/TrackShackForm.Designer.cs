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
            this.dockingManagerHost = new System.Windows.Forms.Integration.ElementHost();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTrackAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectHierarchyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectManipulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockingManagerHost
            // 
            this.dockingManagerHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockingManagerHost.Location = new System.Drawing.Point(0, 25);
            this.dockingManagerHost.Name = "dockingManagerHost";
            this.dockingManagerHost.Size = new System.Drawing.Size(997, 430);
            this.dockingManagerHost.TabIndex = 0;
            this.dockingManagerHost.Text = "elementHost1";
            this.dockingManagerHost.Child = null;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(6, 4, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(997, 25);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTrackToolStripMenuItem,
            this.saveTrackToolStripMenuItem,
            this.saveTrackAsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadTrackToolStripMenuItem
            // 
            this.loadTrackToolStripMenuItem.Name = "loadTrackToolStripMenuItem";
            this.loadTrackToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadTrackToolStripMenuItem.Text = "Load Track...";
            this.loadTrackToolStripMenuItem.Click += new System.EventHandler(this.loadTrackToolStripMenuItem_Click);
            // 
            // saveTrackToolStripMenuItem
            // 
            this.saveTrackToolStripMenuItem.Name = "saveTrackToolStripMenuItem";
            this.saveTrackToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveTrackToolStripMenuItem.Text = "Save Track";
            this.saveTrackToolStripMenuItem.Click += new System.EventHandler(this.saveTrackToolStripMenuItem_Click);
            // 
            // saveTrackAsToolStripMenuItem
            // 
            this.saveTrackAsToolStripMenuItem.Name = "saveTrackAsToolStripMenuItem";
            this.saveTrackAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveTrackAsToolStripMenuItem.Text = "Save Track As...";
            this.saveTrackAsToolStripMenuItem.Click += new System.EventHandler(this.saveTrackAsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectHierarchyToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.objectManipulationToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // objectHierarchyToolStripMenuItem
            // 
            this.objectHierarchyToolStripMenuItem.Name = "objectHierarchyToolStripMenuItem";
            this.objectHierarchyToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.objectHierarchyToolStripMenuItem.Text = "Object Hierarchy";
            this.objectHierarchyToolStripMenuItem.Click += new System.EventHandler(this.objectHierarchyToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // objectManipulationToolStripMenuItem
            // 
            this.objectManipulationToolStripMenuItem.Name = "objectManipulationToolStripMenuItem";
            this.objectManipulationToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.objectManipulationToolStripMenuItem.Text = "Object Manipulation";
            this.objectManipulationToolStripMenuItem.Click += new System.EventHandler(this.objectManipulationToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "track";
            this.saveFileDialog.Filter = "Track file|*.track|All files|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "track";
            this.openFileDialog.Filter = "Track file|*.track|All files|*.*";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // TrackShackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(997, 455);
            this.Controls.Add(this.dockingManagerHost);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrackShackForm";
            this.Text = "Track Shack";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrackShackForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Integration.ElementHost dockingManagerHost;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTrackAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectHierarchyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectManipulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

