namespace Pitstop64.Modules.Text
{
    partial class TextControl
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
            this.gbGameText = new System.Windows.Forms.GroupBox();
            this.btnKartsCancel = new System.Windows.Forms.Button();
            this.btnKartsApply = new System.Windows.Forms.Button();
            this.cbGameText = new System.Windows.Forms.ComboBox();
            this.lblGameTextName = new System.Windows.Forms.Label();
            this.lblGameTextText = new System.Windows.Forms.Label();
            this.txtGameText = new System.Windows.Forms.TextBox();
            this.lblGameTextSpace = new System.Windows.Forms.Label();
            this.txtGameTextSpace = new System.Windows.Forms.TextBox();
            this.btnGameTextApply = new System.Windows.Forms.Button();
            this.gbGameText.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbGameText
            // 
            this.gbGameText.Controls.Add(this.btnGameTextApply);
            this.gbGameText.Controls.Add(this.txtGameTextSpace);
            this.gbGameText.Controls.Add(this.lblGameTextSpace);
            this.gbGameText.Controls.Add(this.txtGameText);
            this.gbGameText.Controls.Add(this.lblGameTextText);
            this.gbGameText.Controls.Add(this.lblGameTextName);
            this.gbGameText.Controls.Add(this.cbGameText);
            this.gbGameText.Location = new System.Drawing.Point(3, 3);
            this.gbGameText.Name = "gbGameText";
            this.gbGameText.Size = new System.Drawing.Size(250, 223);
            this.gbGameText.TabIndex = 0;
            this.gbGameText.TabStop = false;
            this.gbGameText.Text = "Game Text";
            // 
            // btnKartsCancel
            // 
            this.btnKartsCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKartsCancel.Location = new System.Drawing.Point(386, 256);
            this.btnKartsCancel.Name = "btnKartsCancel";
            this.btnKartsCancel.Size = new System.Drawing.Size(112, 49);
            this.btnKartsCancel.TabIndex = 9;
            this.btnKartsCancel.Text = "Cancel";
            this.btnKartsCancel.UseVisualStyleBackColor = true;
            this.btnKartsCancel.Click += new System.EventHandler(this.btnKartsCancel_Click);
            // 
            // btnKartsApply
            // 
            this.btnKartsApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKartsApply.Enabled = false;
            this.btnKartsApply.Location = new System.Drawing.Point(268, 256);
            this.btnKartsApply.Name = "btnKartsApply";
            this.btnKartsApply.Size = new System.Drawing.Size(112, 49);
            this.btnKartsApply.TabIndex = 8;
            this.btnKartsApply.Text = "Apply";
            this.btnKartsApply.UseVisualStyleBackColor = true;
            this.btnKartsApply.Click += new System.EventHandler(this.btnKartsApply_Click);
            // 
            // cbGameText
            // 
            this.cbGameText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGameText.FormattingEnabled = true;
            this.cbGameText.Location = new System.Drawing.Point(18, 51);
            this.cbGameText.Name = "cbGameText";
            this.cbGameText.Size = new System.Drawing.Size(173, 24);
            this.cbGameText.TabIndex = 10;
            this.cbGameText.SelectedIndexChanged += new System.EventHandler(this.cbGameText_SelectedIndexChanged);
            // 
            // lblGameTextName
            // 
            this.lblGameTextName.AutoSize = true;
            this.lblGameTextName.Location = new System.Drawing.Point(25, 28);
            this.lblGameTextName.Name = "lblGameTextName";
            this.lblGameTextName.Size = new System.Drawing.Size(45, 17);
            this.lblGameTextName.TabIndex = 10;
            this.lblGameTextName.Text = "Name";
            // 
            // lblGameTextText
            // 
            this.lblGameTextText.AutoSize = true;
            this.lblGameTextText.Location = new System.Drawing.Point(25, 87);
            this.lblGameTextText.Name = "lblGameTextText";
            this.lblGameTextText.Size = new System.Drawing.Size(35, 17);
            this.lblGameTextText.TabIndex = 11;
            this.lblGameTextText.Text = "Text";
            // 
            // txtGameText
            // 
            this.txtGameText.Location = new System.Drawing.Point(18, 110);
            this.txtGameText.Name = "txtGameText";
            this.txtGameText.Size = new System.Drawing.Size(143, 23);
            this.txtGameText.TabIndex = 12;
            // 
            // lblGameTextSpace
            // 
            this.lblGameTextSpace.AutoSize = true;
            this.lblGameTextSpace.Location = new System.Drawing.Point(25, 149);
            this.lblGameTextSpace.Name = "lblGameTextSpace";
            this.lblGameTextSpace.Size = new System.Drawing.Size(119, 17);
            this.lblGameTextSpace.TabIndex = 13;
            this.lblGameTextSpace.Text = "Remaining Space";
            // 
            // txtGameTextSpace
            // 
            this.txtGameTextSpace.Location = new System.Drawing.Point(44, 169);
            this.txtGameTextSpace.Name = "txtGameTextSpace";
            this.txtGameTextSpace.ReadOnly = true;
            this.txtGameTextSpace.Size = new System.Drawing.Size(75, 23);
            this.txtGameTextSpace.TabIndex = 14;
            // 
            // btnGameTextApply
            // 
            this.btnGameTextApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameTextApply.Location = new System.Drawing.Point(167, 108);
            this.btnGameTextApply.Name = "btnGameTextApply";
            this.btnGameTextApply.Size = new System.Drawing.Size(61, 27);
            this.btnGameTextApply.TabIndex = 10;
            this.btnGameTextApply.Text = "Apply";
            this.btnGameTextApply.UseVisualStyleBackColor = true;
            this.btnGameTextApply.Click += new System.EventHandler(this.btnGameTextApply_Click);
            // 
            // TextControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnKartsCancel);
            this.Controls.Add(this.btnKartsApply);
            this.Controls.Add(this.gbGameText);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TextControl";
            this.Size = new System.Drawing.Size(501, 308);
            this.gbGameText.ResumeLayout(false);
            this.gbGameText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGameText;
        private System.Windows.Forms.Button btnKartsCancel;
        private System.Windows.Forms.Button btnKartsApply;
        private System.Windows.Forms.Label lblGameTextName;
        private System.Windows.Forms.ComboBox cbGameText;
        private System.Windows.Forms.Label lblGameTextText;
        private System.Windows.Forms.TextBox txtGameText;
        private System.Windows.Forms.Label lblGameTextSpace;
        private System.Windows.Forms.TextBox txtGameTextSpace;
        private System.Windows.Forms.Button btnGameTextApply;



    }
}
