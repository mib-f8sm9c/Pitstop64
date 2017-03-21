using Pitstop64.Modules.Karts;
namespace ChompShop.Controls.KartControls
{
    partial class KartStatsForm
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
            this.openNamePlateDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveNamePlateDialog = new System.Windows.Forms.SaveFileDialog();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.kartStatsControl = new Pitstop64.Modules.Karts.KartStatsControl();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // openNamePlateDialog
            // 
            this.openNamePlateDialog.Filter = "PNG files|*.png|All files|*.*";
            // 
            // saveNamePlateDialog
            // 
            this.saveNamePlateDialog.Filter = "PNG files|*.png|All files|*.*";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnApply);
            this.pnlButtons.Controls.Add(this.btnReset);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(5, 351);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(958, 50);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(12, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(132, 38);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply Stats";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(811, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(132, 38);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset Stats";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // kartStatsControl
            // 
            this.kartStatsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kartStatsControl.Enabled = false;
            this.kartStatsControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.kartStatsControl.Location = new System.Drawing.Point(5, 5);
            this.kartStatsControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kartStatsControl.Name = "kartStatsControl";
            this.kartStatsControl.Size = new System.Drawing.Size(958, 346);
            this.kartStatsControl.Stats = null;
            this.kartStatsControl.TabIndex = 1;
            // 
            // KartStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 406);
            this.Controls.Add(this.kartStatsControl);
            this.Controls.Add(this.pnlButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KartStatsForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Kart Stats";
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openNamePlateDialog;
        private System.Windows.Forms.SaveFileDialog saveNamePlateDialog;
        private System.Windows.Forms.Panel pnlButtons;
        private KartStatsControl kartStatsControl;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnApply;
    }
}