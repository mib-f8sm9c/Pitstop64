namespace TrackShack.Controls.TrackControls
{
    partial class ObjectHierarchyControl
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
            this.components = new System.ComponentModel.Container();
            this.gbTree = new System.Windows.Forms.GroupBox();
            this.tvObjects = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.f3DEXCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbTree.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTree
            // 
            this.gbTree.Controls.Add(this.tvObjects);
            this.gbTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTree.Location = new System.Drawing.Point(0, 0);
            this.gbTree.Name = "gbTree";
            this.gbTree.Size = new System.Drawing.Size(220, 111);
            this.gbTree.TabIndex = 0;
            this.gbTree.TabStop = false;
            this.gbTree.Text = "Objects";
            // 
            // tvObjects
            // 
            this.tvObjects.CheckBoxes = true;
            this.tvObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObjects.LabelEdit = true;
            this.tvObjects.Location = new System.Drawing.Point(3, 16);
            this.tvObjects.Name = "tvObjects";
            this.tvObjects.Size = new System.Drawing.Size(214, 92);
            this.tvObjects.TabIndex = 0;
            this.tvObjects.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvObjects_AfterLabelEdit);
            this.tvObjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvObjects_AfterCheck);
            this.tvObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObjects_AfterSelect);
            this.tvObjects.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvObjects_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.f3DEXCommandsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(173, 26);
            // 
            // f3DEXCommandsToolStripMenuItem
            // 
            this.f3DEXCommandsToolStripMenuItem.Name = "f3DEXCommandsToolStripMenuItem";
            this.f3DEXCommandsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.f3DEXCommandsToolStripMenuItem.Text = "F3DEX Commands";
            this.f3DEXCommandsToolStripMenuItem.Click += new System.EventHandler(this.f3DEXCommandsToolStripMenuItem_Click);
            // 
            // ObjectHierarchyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTree);
            this.Name = "ObjectHierarchyControl";
            this.Size = new System.Drawing.Size(220, 111);
            this.gbTree.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTree;
        private System.Windows.Forms.TreeView tvObjects;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem f3DEXCommandsToolStripMenuItem;

    }
}
