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
            this.gbDirection = new System.Windows.Forms.GroupBox();
            this.radE = new System.Windows.Forms.RadioButton();
            this.radS = new System.Windows.Forms.RadioButton();
            this.radW = new System.Windows.Forms.RadioButton();
            this.radN = new System.Windows.Forms.RadioButton();
            this.lbGroups = new System.Windows.Forms.ListBox();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.gbEdit = new System.Windows.Forms.GroupBox();
            this.lblEditMode = new System.Windows.Forms.Label();
            this.cbEditMode = new System.Windows.Forms.ComboBox();
            this.lbEdits = new System.Windows.Forms.ListBox();
            this.btnAddEdit = new System.Windows.Forms.Button();
            this.btnRemoveEdit = new System.Windows.Forms.Button();
            this.gbRenderGroups.SuspendLayout();
            this.gbDirection.SuspendLayout();
            this.gbEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRenderGroups
            // 
            this.gbRenderGroups.Controls.Add(this.btnRemoveGroup);
            this.gbRenderGroups.Controls.Add(this.btnAddGroup);
            this.gbRenderGroups.Controls.Add(this.gbDirection);
            this.gbRenderGroups.Controls.Add(this.lbGroups);
            this.gbRenderGroups.Location = new System.Drawing.Point(0, 0);
            this.gbRenderGroups.Name = "gbRenderGroups";
            this.gbRenderGroups.Size = new System.Drawing.Size(223, 183);
            this.gbRenderGroups.TabIndex = 0;
            this.gbRenderGroups.TabStop = false;
            this.gbRenderGroups.Text = "Rendering Groups";
            // 
            // gbDirection
            // 
            this.gbDirection.Controls.Add(this.radE);
            this.gbDirection.Controls.Add(this.radS);
            this.gbDirection.Controls.Add(this.radW);
            this.gbDirection.Controls.Add(this.radN);
            this.gbDirection.Location = new System.Drawing.Point(160, 19);
            this.gbDirection.Name = "gbDirection";
            this.gbDirection.Size = new System.Drawing.Size(60, 108);
            this.gbDirection.TabIndex = 3;
            this.gbDirection.TabStop = false;
            this.gbDirection.Text = "Dir";
            // 
            // radE
            // 
            this.radE.AutoSize = true;
            this.radE.Location = new System.Drawing.Point(12, 81);
            this.radE.Name = "radE";
            this.radE.Size = new System.Drawing.Size(32, 17);
            this.radE.TabIndex = 3;
            this.radE.TabStop = true;
            this.radE.Text = "E";
            this.radE.UseVisualStyleBackColor = true;
            // 
            // radS
            // 
            this.radS.AutoSize = true;
            this.radS.Location = new System.Drawing.Point(12, 61);
            this.radS.Name = "radS";
            this.radS.Size = new System.Drawing.Size(32, 17);
            this.radS.TabIndex = 2;
            this.radS.TabStop = true;
            this.radS.Text = "S";
            this.radS.UseVisualStyleBackColor = true;
            // 
            // radW
            // 
            this.radW.AutoSize = true;
            this.radW.Location = new System.Drawing.Point(12, 40);
            this.radW.Name = "radW";
            this.radW.Size = new System.Drawing.Size(36, 17);
            this.radW.TabIndex = 1;
            this.radW.TabStop = true;
            this.radW.Text = "W";
            this.radW.UseVisualStyleBackColor = true;
            // 
            // radN
            // 
            this.radN.AutoSize = true;
            this.radN.Location = new System.Drawing.Point(12, 19);
            this.radN.Name = "radN";
            this.radN.Size = new System.Drawing.Size(33, 17);
            this.radN.TabIndex = 0;
            this.radN.TabStop = true;
            this.radN.Text = "N";
            this.radN.UseVisualStyleBackColor = true;
            // 
            // lbGroups
            // 
            this.lbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGroups.FormattingEnabled = true;
            this.lbGroups.Location = new System.Drawing.Point(6, 19);
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.ScrollAlwaysVisible = true;
            this.lbGroups.Size = new System.Drawing.Size(148, 108);
            this.lbGroups.TabIndex = 0;
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Enabled = false;
            this.btnAddGroup.Location = new System.Drawing.Point(6, 133);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(54, 41);
            this.btnAddGroup.TabIndex = 4;
            this.btnAddGroup.Text = "Add";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveGroup.Enabled = false;
            this.btnRemoveGroup.Location = new System.Drawing.Point(160, 133);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(58, 41);
            this.btnRemoveGroup.TabIndex = 5;
            this.btnRemoveGroup.Text = "Remove";
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            // 
            // gbEdit
            // 
            this.gbEdit.Controls.Add(this.btnRemoveEdit);
            this.gbEdit.Controls.Add(this.btnAddEdit);
            this.gbEdit.Controls.Add(this.lbEdits);
            this.gbEdit.Controls.Add(this.cbEditMode);
            this.gbEdit.Controls.Add(this.lblEditMode);
            this.gbEdit.Location = new System.Drawing.Point(0, 189);
            this.gbEdit.Name = "gbEdit";
            this.gbEdit.Size = new System.Drawing.Size(223, 204);
            this.gbEdit.TabIndex = 1;
            this.gbEdit.TabStop = false;
            this.gbEdit.Text = "Edit Contents:";
            // 
            // lblEditMode
            // 
            this.lblEditMode.AutoSize = true;
            this.lblEditMode.Location = new System.Drawing.Point(18, 24);
            this.lblEditMode.Name = "lblEditMode";
            this.lblEditMode.Size = new System.Drawing.Size(37, 13);
            this.lblEditMode.TabIndex = 0;
            this.lblEditMode.Text = "Mode:";
            // 
            // cbEditMode
            // 
            this.cbEditMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditMode.FormattingEnabled = true;
            this.cbEditMode.Items.AddRange(new object[] {
            "Surface",
            "Render"});
            this.cbEditMode.Location = new System.Drawing.Point(61, 19);
            this.cbEditMode.Name = "cbEditMode";
            this.cbEditMode.Size = new System.Drawing.Size(137, 21);
            this.cbEditMode.TabIndex = 1;
            this.cbEditMode.SelectedIndexChanged += new System.EventHandler(this.cbEditMode_SelectedIndexChanged);
            // 
            // lbEdits
            // 
            this.lbEdits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEdits.FormattingEnabled = true;
            this.lbEdits.Location = new System.Drawing.Point(6, 46);
            this.lbEdits.Name = "lbEdits";
            this.lbEdits.ScrollAlwaysVisible = true;
            this.lbEdits.Size = new System.Drawing.Size(211, 95);
            this.lbEdits.TabIndex = 2;
            // 
            // btnAddEdit
            // 
            this.btnAddEdit.Location = new System.Drawing.Point(6, 147);
            this.btnAddEdit.Name = "btnAddEdit";
            this.btnAddEdit.Size = new System.Drawing.Size(54, 41);
            this.btnAddEdit.TabIndex = 6;
            this.btnAddEdit.Text = "Add";
            this.btnAddEdit.UseVisualStyleBackColor = true;
            // 
            // btnRemoveEdit
            // 
            this.btnRemoveEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveEdit.Location = new System.Drawing.Point(159, 146);
            this.btnRemoveEdit.Name = "btnRemoveEdit";
            this.btnRemoveEdit.Size = new System.Drawing.Size(58, 41);
            this.btnRemoveEdit.TabIndex = 6;
            this.btnRemoveEdit.Text = "Remove";
            this.btnRemoveEdit.UseVisualStyleBackColor = true;
            // 
            // SurfaceRenderingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbEdit);
            this.Controls.Add(this.gbRenderGroups);
            this.Name = "SurfaceRenderingControl";
            this.Size = new System.Drawing.Size(223, 396);
            this.gbRenderGroups.ResumeLayout(false);
            this.gbDirection.ResumeLayout(false);
            this.gbDirection.PerformLayout();
            this.gbEdit.ResumeLayout(false);
            this.gbEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRenderGroups;
        private System.Windows.Forms.ListBox lbGroups;
        private System.Windows.Forms.GroupBox gbDirection;
        private System.Windows.Forms.RadioButton radE;
        private System.Windows.Forms.RadioButton radS;
        private System.Windows.Forms.RadioButton radW;
        private System.Windows.Forms.RadioButton radN;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.GroupBox gbEdit;
        private System.Windows.Forms.Label lblEditMode;
        private System.Windows.Forms.ComboBox cbEditMode;
        private System.Windows.Forms.ListBox lbEdits;
        private System.Windows.Forms.Button btnRemoveEdit;
        private System.Windows.Forms.Button btnAddEdit;

    }
}
