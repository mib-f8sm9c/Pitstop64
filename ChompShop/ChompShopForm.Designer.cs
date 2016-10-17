namespace ChompShop
{
    partial class ChompShopForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newKartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadKartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadedKartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openKartDialog = new System.Windows.Forms.OpenFileDialog();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceKartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 473);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(6, 4, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1000, 25);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newKartToolStripMenuItem,
            this.loadKartToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newKartToolStripMenuItem
            // 
            this.newKartToolStripMenuItem.Name = "newKartToolStripMenuItem";
            this.newKartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newKartToolStripMenuItem.Text = "New Kart";
            this.newKartToolStripMenuItem.Click += new System.EventHandler(this.newKartToolStripMenuItem_Click);
            // 
            // loadKartToolStripMenuItem
            // 
            this.loadKartToolStripMenuItem.Name = "loadKartToolStripMenuItem";
            this.loadKartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadKartToolStripMenuItem.Text = "Load Kart...";
            this.loadKartToolStripMenuItem.Click += new System.EventHandler(this.loadKartToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadedKartsToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 19);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // loadedKartsToolStripMenuItem
            // 
            this.loadedKartsToolStripMenuItem.Name = "loadedKartsToolStripMenuItem";
            this.loadedKartsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadedKartsToolStripMenuItem.Text = "Loaded Karts";
            this.loadedKartsToolStripMenuItem.Click += new System.EventHandler(this.loadedKartsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // openKartDialog
            // 
            this.openKartDialog.Filter = "Karts|*.karts";
            this.openKartDialog.Multiselect = true;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.referenceKartToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // referenceKartToolStripMenuItem
            // 
            this.referenceKartToolStripMenuItem.Name = "referenceKartToolStripMenuItem";
            this.referenceKartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.referenceKartToolStripMenuItem.Text = "Reference Kart";
            this.referenceKartToolStripMenuItem.Click += new System.EventHandler(this.referenceKartToolStripMenuItem_Click);
            // 
            // ChompShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1000, 495);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChompShopForm";
            this.Text = "Chomp Shop";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newKartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadKartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openKartDialog;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadedKartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceKartToolStripMenuItem;
    }
}

