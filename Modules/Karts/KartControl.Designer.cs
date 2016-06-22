namespace MK64Pitstop.Modules.Karts
{
    partial class KartControl
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.gbImage = new System.Windows.Forms.GroupBox();
            this.cbCharacter = new System.Windows.Forms.ComboBox();
            this.lblCharacter = new System.Windows.Forms.Label();
            this.lblImageNum = new System.Windows.Forms.Label();
            this.cbImageNum = new System.Windows.Forms.ComboBox();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.cbAnimation = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.gbImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 19);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(272, 225);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // gbImage
            // 
            this.gbImage.Controls.Add(this.pictureBox);
            this.gbImage.Location = new System.Drawing.Point(242, 4);
            this.gbImage.Margin = new System.Windows.Forms.Padding(4);
            this.gbImage.Name = "gbImage";
            this.gbImage.Padding = new System.Windows.Forms.Padding(4);
            this.gbImage.Size = new System.Drawing.Size(293, 264);
            this.gbImage.TabIndex = 1;
            this.gbImage.TabStop = false;
            this.gbImage.Text = "Image";
            // 
            // cbCharacter
            // 
            this.cbCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCharacter.FormattingEnabled = true;
            this.cbCharacter.Location = new System.Drawing.Point(27, 43);
            this.cbCharacter.Name = "cbCharacter";
            this.cbCharacter.Size = new System.Drawing.Size(175, 24);
            this.cbCharacter.TabIndex = 2;
            this.cbCharacter.SelectedIndexChanged += new System.EventHandler(this.cbCharacter_SelectedIndexChanged);
            // 
            // lblCharacter
            // 
            this.lblCharacter.AutoSize = true;
            this.lblCharacter.Location = new System.Drawing.Point(38, 23);
            this.lblCharacter.Name = "lblCharacter";
            this.lblCharacter.Size = new System.Drawing.Size(70, 17);
            this.lblCharacter.TabIndex = 3;
            this.lblCharacter.Text = "Character";
            // 
            // lblImageNum
            // 
            this.lblImageNum.AutoSize = true;
            this.lblImageNum.Location = new System.Drawing.Point(38, 182);
            this.lblImageNum.Name = "lblImageNum";
            this.lblImageNum.Size = new System.Drawing.Size(46, 17);
            this.lblImageNum.TabIndex = 5;
            this.lblImageNum.Text = "Image";
            // 
            // cbImageNum
            // 
            this.cbImageNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageNum.FormattingEnabled = true;
            this.cbImageNum.Location = new System.Drawing.Point(27, 202);
            this.cbImageNum.Name = "cbImageNum";
            this.cbImageNum.Size = new System.Drawing.Size(175, 24);
            this.cbImageNum.TabIndex = 4;
            this.cbImageNum.SelectedIndexChanged += new System.EventHandler(this.cbImageNum_SelectedIndexChanged);
            // 
            // lblAnimation
            // 
            this.lblAnimation.AutoSize = true;
            this.lblAnimation.Location = new System.Drawing.Point(38, 105);
            this.lblAnimation.Name = "lblAnimation";
            this.lblAnimation.Size = new System.Drawing.Size(70, 17);
            this.lblAnimation.TabIndex = 7;
            this.lblAnimation.Text = "Animation";
            // 
            // cbAnimation
            // 
            this.cbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnimation.FormattingEnabled = true;
            this.cbAnimation.Location = new System.Drawing.Point(27, 125);
            this.cbAnimation.Name = "cbAnimation";
            this.cbAnimation.Size = new System.Drawing.Size(175, 24);
            this.cbAnimation.TabIndex = 6;
            this.cbAnimation.SelectedIndexChanged += new System.EventHandler(this.cbAnimation_SelectedIndexChanged);
            // 
            // KartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAnimation);
            this.Controls.Add(this.cbAnimation);
            this.Controls.Add(this.lblImageNum);
            this.Controls.Add(this.cbImageNum);
            this.Controls.Add(this.lblCharacter);
            this.Controls.Add(this.cbCharacter);
            this.Controls.Add(this.gbImage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartControl";
            this.Size = new System.Drawing.Size(585, 302);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.gbImage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.GroupBox gbImage;
        private System.Windows.Forms.ComboBox cbCharacter;
        private System.Windows.Forms.Label lblCharacter;
        private System.Windows.Forms.Label lblImageNum;
        private System.Windows.Forms.ComboBox cbImageNum;
        private System.Windows.Forms.Label lblAnimation;
        private System.Windows.Forms.ComboBox cbAnimation;
    }
}
