namespace ChompShop.Controls.KartControls
{
    partial class KartPortraitsForm
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
            this.imagePreviewControl = new Pitstop64.Modules.Karts.ImagePreviewControl();
            this.gbKartPortraits = new System.Windows.Forms.GroupBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblRoleText = new System.Windows.Forms.Label();
            this.lblPortraitCount = new System.Windows.Forms.Label();
            this.lblCountText = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbPortraits = new System.Windows.Forms.ListBox();
            this.openPortraitDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbKartPortraits.SuspendLayout();
            this.SuspendLayout();
            // 
            // imagePreviewControl
            // 
            this.imagePreviewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePreviewControl.ExportButtonVisible = true;
            this.imagePreviewControl.Image = null;
            this.imagePreviewControl.Location = new System.Drawing.Point(185, 99);
            this.imagePreviewControl.Name = "imagePreviewControl";
            this.imagePreviewControl.OverlayImage = null;
            this.imagePreviewControl.Size = new System.Drawing.Size(175, 155);
            this.imagePreviewControl.TabIndex = 9;
            // 
            // gbKartPortraits
            // 
            this.gbKartPortraits.Controls.Add(this.lblRole);
            this.gbKartPortraits.Controls.Add(this.lblRoleText);
            this.gbKartPortraits.Controls.Add(this.imagePreviewControl);
            this.gbKartPortraits.Controls.Add(this.lblPortraitCount);
            this.gbKartPortraits.Controls.Add(this.lblCountText);
            this.gbKartPortraits.Controls.Add(this.btnRemove);
            this.gbKartPortraits.Controls.Add(this.btnDown);
            this.gbKartPortraits.Controls.Add(this.btnUp);
            this.gbKartPortraits.Controls.Add(this.btnAdd);
            this.gbKartPortraits.Controls.Add(this.lbPortraits);
            this.gbKartPortraits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbKartPortraits.Location = new System.Drawing.Point(5, 5);
            this.gbKartPortraits.Margin = new System.Windows.Forms.Padding(4);
            this.gbKartPortraits.Name = "gbKartPortraits";
            this.gbKartPortraits.Padding = new System.Windows.Forms.Padding(4);
            this.gbKartPortraits.Size = new System.Drawing.Size(376, 261);
            this.gbKartPortraits.TabIndex = 0;
            this.gbKartPortraits.TabStop = false;
            this.gbKartPortraits.Text = "Portraits";
            // 
            // lblRole
            // 
            this.lblRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(279, 32);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(42, 17);
            this.lblRole.TabIndex = 6;
            this.lblRole.Text = "None";
            // 
            // lblRoleText
            // 
            this.lblRoleText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoleText.AutoSize = true;
            this.lblRoleText.Location = new System.Drawing.Point(182, 32);
            this.lblRoleText.Name = "lblRoleText";
            this.lblRoleText.Size = new System.Drawing.Size(91, 17);
            this.lblRoleText.TabIndex = 5;
            this.lblRoleText.Text = "Portrait Role:";
            // 
            // lblPortraitCount
            // 
            this.lblPortraitCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPortraitCount.AutoSize = true;
            this.lblPortraitCount.Location = new System.Drawing.Point(279, 69);
            this.lblPortraitCount.Name = "lblPortraitCount";
            this.lblPortraitCount.Size = new System.Drawing.Size(36, 17);
            this.lblPortraitCount.TabIndex = 8;
            this.lblPortraitCount.Text = "0/17";
            // 
            // lblCountText
            // 
            this.lblCountText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountText.AutoSize = true;
            this.lblCountText.Location = new System.Drawing.Point(174, 69);
            this.lblCountText.Name = "lblCountText";
            this.lblCountText.Size = new System.Drawing.Size(99, 17);
            this.lblCountText.TabIndex = 7;
            this.lblCountText.Text = "Portrait Count:";
            // 
            // btnRemove
            // 
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Image = global::ChompShop.Properties.Resources.minus;
            this.btnRemove.Location = new System.Drawing.Point(85, 22);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(20, 20);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnDown
            // 
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Image = global::ChompShop.Properties.Resources.arrow_thick_bottom;
            this.btnDown.Location = new System.Drawing.Point(59, 22);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.TabIndex = 3;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Image = global::ChompShop.Properties.Resources.arrow_thick_top;
            this.btnUp.Location = new System.Drawing.Point(33, 22);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 2;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = global::ChompShop.Properties.Resources.plus;
            this.btnAdd.Location = new System.Drawing.Point(7, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(20, 20);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbPortraits
            // 
            this.lbPortraits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPortraits.FormattingEnabled = true;
            this.lbPortraits.ItemHeight = 16;
            this.lbPortraits.Location = new System.Drawing.Point(7, 48);
            this.lbPortraits.Name = "lbPortraits";
            this.lbPortraits.ScrollAlwaysVisible = true;
            this.lbPortraits.Size = new System.Drawing.Size(164, 196);
            this.lbPortraits.TabIndex = 0;
            this.lbPortraits.SelectedIndexChanged += new System.EventHandler(this.lbPortraits_SelectedIndexChanged);
            // 
            // openPortraitDialog
            // 
            this.openPortraitDialog.Multiselect = true;
            // 
            // KartPortraitsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 271);
            this.Controls.Add(this.gbKartPortraits);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartPortraitsForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Kart Portraits";
            this.gbKartPortraits.ResumeLayout(false);
            this.gbKartPortraits.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Pitstop64.Modules.Karts.ImagePreviewControl imagePreviewControl;
        private System.Windows.Forms.GroupBox gbKartPortraits;
        private System.Windows.Forms.OpenFileDialog openPortraitDialog;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lbPortraits;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label lblCountText;
        private System.Windows.Forms.Label lblPortraitCount;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblRoleText;
    }
}