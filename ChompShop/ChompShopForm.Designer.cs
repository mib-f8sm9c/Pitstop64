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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newKartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadKartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveKartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveKartAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lblNoKartLoaded = new System.Windows.Forms.Label();
            this.btnBGColor2 = new System.Windows.Forms.Button();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.lblUniqueAnimations = new System.Windows.Forms.Label();
            this.lblUniqueAnimsCount = new System.Windows.Forms.Label();
            this.lblUniqueFrames = new System.Windows.Forms.Label();
            this.lblUniqueFramesCount = new System.Windows.Forms.Label();
            this.gbKartPreview = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.gbKartPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(4, 469);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(992, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 4);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(992, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newKartToolStripMenuItem,
            this.loadKartToolStripMenuItem,
            this.saveKartToolStripMenuItem,
            this.saveKartAsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
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
            // saveKartToolStripMenuItem
            // 
            this.saveKartToolStripMenuItem.Name = "saveKartToolStripMenuItem";
            this.saveKartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveKartToolStripMenuItem.Text = "Save Kart";
            this.saveKartToolStripMenuItem.Click += new System.EventHandler(this.saveKartToolStripMenuItem_Click);
            // 
            // saveKartAsToolStripMenuItem
            // 
            this.saveKartAsToolStripMenuItem.Name = "saveKartAsToolStripMenuItem";
            this.saveKartAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveKartAsToolStripMenuItem.Text = "Save Kart As...";
            this.saveKartAsToolStripMenuItem.Click += new System.EventHandler(this.saveKartAsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(4, 28);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lblNoKartLoaded);
            this.splitContainer.Panel1Collapsed = true;
            this.splitContainer.Size = new System.Drawing.Size(658, 441);
            this.splitContainer.SplitterDistance = 178;
            this.splitContainer.TabIndex = 5;
            // 
            // lblNoKartLoaded
            // 
            this.lblNoKartLoaded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoKartLoaded.Location = new System.Drawing.Point(0, 0);
            this.lblNoKartLoaded.Name = "lblNoKartLoaded";
            this.lblNoKartLoaded.Size = new System.Drawing.Size(178, 100);
            this.lblNoKartLoaded.TabIndex = 0;
            this.lblNoKartLoaded.Text = "No Kart Loaded";
            this.lblNoKartLoaded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBGColor2
            // 
            this.btnBGColor2.BackColor = System.Drawing.SystemColors.ControlText;
            this.btnBGColor2.Location = new System.Drawing.Point(70, 36);
            this.btnBGColor2.Name = "btnBGColor2";
            this.btnBGColor2.Size = new System.Drawing.Size(24, 24);
            this.btnBGColor2.TabIndex = 21;
            this.btnBGColor2.UseVisualStyleBackColor = false;
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlPreview.Controls.Add(this.pbPreview);
            this.pnlPreview.Location = new System.Drawing.Point(114, 25);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(130, 130);
            this.pnlPreview.TabIndex = 22;
            // 
            // pbPreview
            // 
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(130, 130);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            // 
            // lblUniqueAnimations
            // 
            this.lblUniqueAnimations.AutoSize = true;
            this.lblUniqueAnimations.Location = new System.Drawing.Point(114, 207);
            this.lblUniqueAnimations.Name = "lblUniqueAnimations";
            this.lblUniqueAnimations.Size = new System.Drawing.Size(130, 17);
            this.lblUniqueAnimations.TabIndex = 25;
            this.lblUniqueAnimations.Text = "Unique Animations:";
            // 
            // lblUniqueAnimsCount
            // 
            this.lblUniqueAnimsCount.AutoSize = true;
            this.lblUniqueAnimsCount.Location = new System.Drawing.Point(151, 235);
            this.lblUniqueAnimsCount.Name = "lblUniqueAnimsCount";
            this.lblUniqueAnimsCount.Size = new System.Drawing.Size(44, 17);
            this.lblUniqueAnimsCount.TabIndex = 26;
            this.lblUniqueAnimsCount.Text = "0 / 19";
            // 
            // lblUniqueFrames
            // 
            this.lblUniqueFrames.AutoSize = true;
            this.lblUniqueFrames.Location = new System.Drawing.Point(114, 274);
            this.lblUniqueFrames.Name = "lblUniqueFrames";
            this.lblUniqueFrames.Size = new System.Drawing.Size(108, 17);
            this.lblUniqueFrames.TabIndex = 27;
            this.lblUniqueFrames.Text = "Unique Frames:";
            // 
            // lblUniqueFramesCount
            // 
            this.lblUniqueFramesCount.AutoSize = true;
            this.lblUniqueFramesCount.Location = new System.Drawing.Point(151, 302);
            this.lblUniqueFramesCount.Name = "lblUniqueFramesCount";
            this.lblUniqueFramesCount.Size = new System.Drawing.Size(37, 17);
            this.lblUniqueFramesCount.TabIndex = 28;
            this.lblUniqueFramesCount.Text = "0 / X";
            // 
            // gbKartPreview
            // 
            this.gbKartPreview.Controls.Add(this.lblUniqueFramesCount);
            this.gbKartPreview.Controls.Add(this.lblUniqueFrames);
            this.gbKartPreview.Controls.Add(this.lblUniqueAnimsCount);
            this.gbKartPreview.Controls.Add(this.lblUniqueAnimations);
            this.gbKartPreview.Controls.Add(this.pnlPreview);
            this.gbKartPreview.Controls.Add(this.btnBGColor2);
            this.gbKartPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbKartPreview.Location = new System.Drawing.Point(662, 28);
            this.gbKartPreview.Name = "gbKartPreview";
            this.gbKartPreview.Size = new System.Drawing.Size(334, 441);
            this.gbKartPreview.TabIndex = 1;
            this.gbKartPreview.TabStop = false;
            this.gbKartPreview.Text = "Kart Preview";
            // 
            // ChompShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 495);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.gbKartPreview);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChompShopForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "Chomp Shop";
            this.Load += new System.EventHandler(this.ChompShopForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.gbKartPreview.ResumeLayout(false);
            this.gbKartPreview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newKartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadKartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveKartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveKartAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblNoKartLoaded;
        private System.Windows.Forms.Button btnBGColor2;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Label lblUniqueAnimations;
        private System.Windows.Forms.Label lblUniqueAnimsCount;
        private System.Windows.Forms.Label lblUniqueFrames;
        private System.Windows.Forms.Label lblUniqueFramesCount;
        private System.Windows.Forms.GroupBox gbKartPreview;
    }
}

