namespace ChompShop.Controls
{
    partial class LoadedKartsForm
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
            this.components = new System.ComponentModel.Container();
            this.lbKarts = new System.Windows.Forms.ListBox();
            this.openKartDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddKart = new System.Windows.Forms.Button();
            this.btnResetChanges = new System.Windows.Forms.Button();
            this.btnAnims = new System.Windows.Forms.Button();
            this.btnPortraits = new System.Windows.Forms.Button();
            this.btnImages = new System.Windows.Forms.Button();
            this.btnName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbKarts
            // 
            this.lbKarts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKarts.FormattingEnabled = true;
            this.lbKarts.ItemHeight = 16;
            this.lbKarts.Location = new System.Drawing.Point(16, 47);
            this.lbKarts.Margin = new System.Windows.Forms.Padding(4);
            this.lbKarts.Name = "lbKarts";
            this.lbKarts.ScrollAlwaysVisible = true;
            this.lbKarts.Size = new System.Drawing.Size(134, 164);
            this.lbKarts.TabIndex = 0;
            this.lbKarts.SelectedIndexChanged += new System.EventHandler(this.lbKarts_SelectedIndexChanged);
            // 
            // openKartDialog
            // 
            this.openKartDialog.Filter = "Karts|*.karts";
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnSaveChanges.Image = global::ChompShop.Properties.Resources.task_4x;
            this.btnSaveChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanges.Location = new System.Drawing.Point(13, 222);
            this.btnSaveChanges.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(95, 50);
            this.btnSaveChanges.TabIndex = 9;
            this.btnSaveChanges.Text = "Save Kart Changes";
            this.btnSaveChanges.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnSaveChanges, "Reset unsaved changes");
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCopy.Image = global::ChompShop.Properties.Resources.clipboard;
            this.btnCopy.Location = new System.Drawing.Point(94, 12);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(24, 24);
            this.btnCopy.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnCopy, "Duplicate selected Kart");
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.Image = global::ChompShop.Properties.Resources.minus;
            this.btnRemove.Location = new System.Drawing.Point(126, 12);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 7;
            this.toolTip.SetToolTip(this.btnRemove, "Remove selected Kart");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddKart
            // 
            this.btnAddKart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddKart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddKart.Image = global::ChompShop.Properties.Resources.plus;
            this.btnAddKart.Location = new System.Drawing.Point(62, 12);
            this.btnAddKart.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddKart.Name = "btnAddKart";
            this.btnAddKart.Size = new System.Drawing.Size(24, 24);
            this.btnAddKart.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnAddKart, "Add new Kart");
            this.btnAddKart.UseVisualStyleBackColor = true;
            this.btnAddKart.Click += new System.EventHandler(this.btnAddKart_Click);
            // 
            // btnResetChanges
            // 
            this.btnResetChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetChanges.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnResetChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnResetChanges.Image = global::ChompShop.Properties.Resources.action_undo_4x;
            this.btnResetChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetChanges.Location = new System.Drawing.Point(158, 222);
            this.btnResetChanges.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetChanges.Name = "btnResetChanges";
            this.btnResetChanges.Size = new System.Drawing.Size(95, 50);
            this.btnResetChanges.TabIndex = 5;
            this.btnResetChanges.Text = "Revert Changes";
            this.btnResetChanges.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnResetChanges, "Reset unsaved changes");
            this.btnResetChanges.UseVisualStyleBackColor = true;
            this.btnResetChanges.Click += new System.EventHandler(this.btnResetChanges_Click);
            // 
            // btnAnims
            // 
            this.btnAnims.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnims.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAnims.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnAnims.Image = global::ChompShop.Properties.Resources.video_3x;
            this.btnAnims.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnims.Location = new System.Drawing.Point(158, 149);
            this.btnAnims.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnims.Name = "btnAnims";
            this.btnAnims.Size = new System.Drawing.Size(95, 40);
            this.btnAnims.TabIndex = 4;
            this.btnAnims.Text = " Animations";
            this.btnAnims.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnAnims, "Manage Kart Animations");
            this.btnAnims.UseVisualStyleBackColor = true;
            this.btnAnims.Click += new System.EventHandler(this.btnAnims_Click);
            // 
            // btnPortraits
            // 
            this.btnPortraits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPortraits.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPortraits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPortraits.Image = global::ChompShop.Properties.Resources.image_3x;
            this.btnPortraits.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPortraits.Location = new System.Drawing.Point(158, 58);
            this.btnPortraits.Margin = new System.Windows.Forms.Padding(4);
            this.btnPortraits.Name = "btnPortraits";
            this.btnPortraits.Size = new System.Drawing.Size(95, 40);
            this.btnPortraits.TabIndex = 3;
            this.btnPortraits.Text = " Portraits";
            this.btnPortraits.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnPortraits, "Edit Kart Portrait Images");
            this.btnPortraits.UseVisualStyleBackColor = true;
            this.btnPortraits.Click += new System.EventHandler(this.btnPortraits_Click);
            // 
            // btnImages
            // 
            this.btnImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImages.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnImages.Image = global::ChompShop.Properties.Resources.grid_three_up_3x;
            this.btnImages.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImages.Location = new System.Drawing.Point(158, 103);
            this.btnImages.Margin = new System.Windows.Forms.Padding(4);
            this.btnImages.Name = "btnImages";
            this.btnImages.Size = new System.Drawing.Size(95, 40);
            this.btnImages.TabIndex = 2;
            this.btnImages.Text = " Images";
            this.btnImages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnImages, "Manage Kart Images");
            this.btnImages.UseVisualStyleBackColor = true;
            this.btnImages.Click += new System.EventHandler(this.btnImages_Click);
            // 
            // btnName
            // 
            this.btnName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnName.Image = global::ChompShop.Properties.Resources.info_3x;
            this.btnName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnName.Location = new System.Drawing.Point(158, 12);
            this.btnName.Margin = new System.Windows.Forms.Padding(4);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(95, 40);
            this.btnName.TabIndex = 1;
            this.btnName.Text = "Kart Info";
            this.btnName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnName, "Edit Kart Name");
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // LoadedKartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 285);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddKart);
            this.Controls.Add(this.btnResetChanges);
            this.Controls.Add(this.btnAnims);
            this.Controls.Add(this.btnPortraits);
            this.Controls.Add(this.btnImages);
            this.Controls.Add(this.btnName);
            this.Controls.Add(this.lbKarts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoadedKartsForm";
            this.Text = "Loaded Karts";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbKarts;
        private System.Windows.Forms.Button btnName;
        private System.Windows.Forms.Button btnImages;
        private System.Windows.Forms.Button btnPortraits;
        private System.Windows.Forms.Button btnAnims;
        private System.Windows.Forms.Button btnResetChanges;
        private System.Windows.Forms.Button btnAddKart;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.OpenFileDialog openKartDialog;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnSaveChanges;
    }
}