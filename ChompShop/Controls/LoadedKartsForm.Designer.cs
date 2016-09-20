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
            this.lbKarts = new System.Windows.Forms.ListBox();
            this.btnName = new System.Windows.Forms.Button();
            this.btnImages = new System.Windows.Forms.Button();
            this.btnPortraits = new System.Windows.Forms.Button();
            this.btnAnims = new System.Windows.Forms.Button();
            this.btnResetChanges = new System.Windows.Forms.Button();
            this.btnAddKart = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.openKartDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbKarts
            // 
            this.lbKarts.FormattingEnabled = true;
            this.lbKarts.ItemHeight = 16;
            this.lbKarts.Location = new System.Drawing.Point(16, 47);
            this.lbKarts.Margin = new System.Windows.Forms.Padding(4);
            this.lbKarts.Name = "lbKarts";
            this.lbKarts.ScrollAlwaysVisible = true;
            this.lbKarts.Size = new System.Drawing.Size(159, 180);
            this.lbKarts.TabIndex = 0;
            // 
            // btnName
            // 
            this.btnName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnName.Location = new System.Drawing.Point(205, 12);
            this.btnName.Margin = new System.Windows.Forms.Padding(4);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(65, 38);
            this.btnName.TabIndex = 1;
            this.btnName.Text = "Name";
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // btnImages
            // 
            this.btnImages.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImages.Location = new System.Drawing.Point(205, 103);
            this.btnImages.Margin = new System.Windows.Forms.Padding(4);
            this.btnImages.Name = "btnImages";
            this.btnImages.Size = new System.Drawing.Size(65, 38);
            this.btnImages.TabIndex = 2;
            this.btnImages.Text = "Images/Palettes";
            this.btnImages.UseVisualStyleBackColor = true;
            this.btnImages.Click += new System.EventHandler(this.btnImages_Click);
            // 
            // btnPortraits
            // 
            this.btnPortraits.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPortraits.Location = new System.Drawing.Point(205, 58);
            this.btnPortraits.Margin = new System.Windows.Forms.Padding(4);
            this.btnPortraits.Name = "btnPortraits";
            this.btnPortraits.Size = new System.Drawing.Size(65, 38);
            this.btnPortraits.TabIndex = 3;
            this.btnPortraits.Text = "Portraits";
            this.btnPortraits.UseVisualStyleBackColor = true;
            this.btnPortraits.Click += new System.EventHandler(this.btnPortraits_Click);
            // 
            // btnAnims
            // 
            this.btnAnims.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAnims.Location = new System.Drawing.Point(205, 149);
            this.btnAnims.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnims.Name = "btnAnims";
            this.btnAnims.Size = new System.Drawing.Size(65, 38);
            this.btnAnims.TabIndex = 4;
            this.btnAnims.Text = "Anims";
            this.btnAnims.UseVisualStyleBackColor = true;
            this.btnAnims.Click += new System.EventHandler(this.btnAnims_Click);
            // 
            // btnResetChanges
            // 
            this.btnResetChanges.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnResetChanges.Location = new System.Drawing.Point(205, 194);
            this.btnResetChanges.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetChanges.Name = "btnResetChanges";
            this.btnResetChanges.Size = new System.Drawing.Size(149, 38);
            this.btnResetChanges.TabIndex = 5;
            this.btnResetChanges.Text = "Reset Changes";
            this.btnResetChanges.UseVisualStyleBackColor = true;
            // 
            // btnAddKart
            // 
            this.btnAddKart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddKart.Location = new System.Drawing.Point(87, 12);
            this.btnAddKart.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddKart.Name = "btnAddKart";
            this.btnAddKart.Size = new System.Drawing.Size(24, 24);
            this.btnAddKart.TabIndex = 6;
            this.btnAddKart.Text = "+";
            this.btnAddKart.UseVisualStyleBackColor = true;
            this.btnAddKart.Click += new System.EventHandler(this.btnAddKart_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.Location = new System.Drawing.Point(151, 12);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // openKartDialog
            // 
            this.openKartDialog.Filter = "Karts|*.karts";
            // 
            // btnCopy
            // 
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCopy.Location = new System.Drawing.Point(119, 12);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(24, 24);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "c";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // LoadedKartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 247);
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
    }
}