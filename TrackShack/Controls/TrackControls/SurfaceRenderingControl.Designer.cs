namespace TrackShack.Controls.TrackControls
{
    partial class SurfaceRenderingControl
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
            this.gbRenderGroups = new System.Windows.Forms.GroupBox();
            this.lbGroups = new System.Windows.Forms.ListBox();
            this.cbDir = new System.Windows.Forms.ComboBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.gbRenderGroups.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRenderGroups
            // 
            this.gbRenderGroups.Controls.Add(this.btnSet);
            this.gbRenderGroups.Controls.Add(this.cbDir);
            this.gbRenderGroups.Controls.Add(this.lbGroups);
            this.gbRenderGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRenderGroups.Location = new System.Drawing.Point(0, 0);
            this.gbRenderGroups.Name = "gbRenderGroups";
            this.gbRenderGroups.Size = new System.Drawing.Size(223, 293);
            this.gbRenderGroups.TabIndex = 0;
            this.gbRenderGroups.TabStop = false;
            this.gbRenderGroups.Text = "Rendering Groups";
            // 
            // lbGroups
            // 
            this.lbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGroups.FormattingEnabled = true;
            this.lbGroups.Location = new System.Drawing.Point(6, 19);
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.ScrollAlwaysVisible = true;
            this.lbGroups.Size = new System.Drawing.Size(211, 186);
            this.lbGroups.TabIndex = 0;
            this.lbGroups.SelectedIndexChanged += new System.EventHandler(this.lbGroups_SelectedIndexChanged);
            // 
            // cbDir
            // 
            this.cbDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDir.FormattingEnabled = true;
            this.cbDir.Items.AddRange(new object[] {
            "North",
            "East",
            "South",
            "West"});
            this.cbDir.Location = new System.Drawing.Point(13, 245);
            this.cbDir.Name = "cbDir";
            this.cbDir.Size = new System.Drawing.Size(121, 21);
            this.cbDir.TabIndex = 1;
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Location = new System.Drawing.Point(140, 221);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 66);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "button1";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // SurfaceRenderingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbRenderGroups);
            this.Name = "SurfaceRenderingControl";
            this.Size = new System.Drawing.Size(223, 293);
            this.gbRenderGroups.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRenderGroups;
        private System.Windows.Forms.ListBox lbGroups;
        private System.Windows.Forms.ComboBox cbDir;
        private System.Windows.Forms.Button btnSet;

    }
}
