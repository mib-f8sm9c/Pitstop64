namespace TrackShack.Controls
{
    partial class ElementF3DEXCommandsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.f3DEXEditor = new Cereal64.Microcodes.F3DEX.Controls.F3DEXEditor();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(383, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // f3DEXEditor1
            // 
            this.f3DEXEditor.Commands = null;
            this.f3DEXEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.f3DEXEditor.FixedSize = false;
            this.f3DEXEditor.Location = new System.Drawing.Point(0, 25);
            this.f3DEXEditor.Name = "f3DEXEditor1";
            this.f3DEXEditor.Size = new System.Drawing.Size(383, 265);
            this.f3DEXEditor.TabIndex = 1;
            // 
            // ElementF3DEXCommandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 290);
            this.Controls.Add(this.f3DEXEditor);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ElementF3DEXCommandsForm";
            this.ShowIcon = false;
            this.Text = "F3DEX Commands";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Cereal64.Microcodes.F3DEX.Controls.F3DEXEditor f3DEXEditor;
    }
}