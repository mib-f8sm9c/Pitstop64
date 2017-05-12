using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.VisObj64.Data.OpenGL;
using TrackShack.Data;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DEX;
using Xceed.Wpf.AvalonDock.Layout;

namespace TrackShack.Controls.TrackControls
{
    public partial class ObjectHierarchyControl : TrackShackDockableWindow
    {
        private ElementNode _rightClickedNode;

        public ObjectHierarchyControl(LayoutContent content)
            : base(content)
        {
            InitializeComponent();

            //Do alert stuff later : )
            TrackShackAlerts.TrackChanged += LoadDataIntoList;
            TrackShackAlerts.RenderingGroupChanged += HandleRenderGroupChanged;

            LoadDataIntoList();
        }

        private void HandleRenderGroupChanged(VO64GraphicsCollection oldGroup, VO64GraphicsCollection newGroup)
        {
            LoadDataIntoList();
        }

        private void LoadDataIntoList()
        {
            tvObjects.Nodes.Clear();

            if(TrackShackFloor.SelectedRenderingGroup != null)
                tvObjects.Nodes.Add(CreateTreeNodeFrom(TrackShackFloor.SelectedRenderingGroup));
        }

        private TreeNode CreateTreeNodeFrom(VO64GraphicsCollection collection)
        {
            CollectionNode node = new CollectionNode(collection);

            foreach (VO64GraphicsCollection coll in collection.Collections)
                node.Nodes.Add(CreateTreeNodeFrom(coll));

            foreach (VO64GraphicsElement el in collection.Elements)
                node.Nodes.Add(new ElementNode(el));

            return node;
        }

        public override TrackShackDockableWindowType WindowType
        {
            get
            {
                return TrackShackDockableWindowType.ObjectHierarchy;
            }
        }

        public static string DockingContentId { get { return "test1"; } }

        internal interface IVO64TreeNode
        {
            bool Enabled { get; set; }

            string VO64Name { get; set; }
        }

        internal class CollectionNode : TreeNode, IVO64TreeNode
        {
            public VO64GraphicsCollection Collection { get; private set; }

            public CollectionNode(VO64GraphicsCollection collection)
            {
                Collection = collection;
                this.Name = Collection.Name;
                this.Checked = Collection.Enabled;
                this.Text = this.Name;
            }

            public bool Enabled
            {
                get
                {
                    return Collection.Enabled;
                }
                set
                {
                    Collection.Enabled = value;
                }
            }

            public string VO64Name
            {
                get
                {
                    return Collection.Name;
                }
                set
                {
                    Collection.Name = value;
                }
            }

            public override string ToString()
            {
                return Collection.Name;
            }
        }

        internal class ElementNode : TreeNode, IVO64TreeNode
        {
            public VO64GraphicsElement Element { get; private set; }

            public ElementNode(VO64GraphicsElement element)
            {
                Element = element;
                this.Name = Element.Name;
                this.Checked = Element.Enabled;
                this.Text = this.Name;
            }

            public bool Enabled
            {
                get
                {
                    return Element.Enabled;
                }
                set
                {
                    Element.Enabled = value;
                }
            }

            public string VO64Name
            {
                get
                {
                    return Element.Name;
                }
                set
                {
                    Element.Name = value;
                }
            }

            public override string ToString()
            {
                return Element.Name;
            }
        }

        private void tvObjects_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvObjects.AfterCheck -= tvObjects_AfterCheck;
            ((IVO64TreeNode)e.Node).Enabled = e.Node.Checked;
            ReflectChildNodeEnabled(e.Node, e.Node.Checked);
            if(e.Node.Checked)
                ReflectParentNodeEnabled(e.Node);
            tvObjects.AfterCheck += tvObjects_AfterCheck;

            TrackShackAlerts.UpdateViewer();
        }

        private void ReflectParentNodeEnabled(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Checked = true;
                ((IVO64TreeNode)node.Parent).Enabled = true;
                ReflectParentNodeEnabled(node.Parent);
            }
        }

        private void ReflectChildNodeEnabled(TreeNode node, bool check)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = check;
                ((IVO64TreeNode)childNode).Enabled = check;
                ReflectChildNodeEnabled(childNode, check);
            }
        }

        private void tvObjects_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ((IVO64TreeNode)e.Node).VO64Name = e.Node.Text;
            e.Node.Name = e.Node.Text;
        }

        //Code copied from https://support.microsoft.com/en-us/help/810001/how-to-display-a-context-menu-that-is-specific-to-a-selected-treeview-node-by-using-visual-c-.net-or-visual-c-2005
        private void tvObjects_MouseUp(object sender, MouseEventArgs e)
        {
            // Show menu only if the right mouse button is clicked.
            if (e.Button == MouseButtons.Right)
            {
                // Point where the mouse is clicked.
                Point p = new Point(e.X, e.Y);

                // Get the node that the user has clicked.
                IVO64TreeNode node = (IVO64TreeNode)tvObjects.GetNodeAt(p);
                if (node != null && node is ElementNode)
                {
                    _rightClickedNode = (ElementNode)node;
                    contextMenuStrip.Show(tvObjects, p);
                }
            }
        }

        private void f3DEXCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rightClickedNode != null)
            {
                ElementF3DEXCommandsForm form = new ElementF3DEXCommandsForm((F3DEXGraphicsElement)_rightClickedNode.Element);
                form.Show();
            }
        }

        private void tvObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<VO64GraphicsElement> selectedElements = null;

            if (e.Node != null)
            {
                if (e.Node is CollectionNode)
                {
                    selectedElements = ((CollectionNode)e.Node).Collection.GetAllElements();
                }
                else
                {
                    selectedElements = new List<VO64GraphicsElement>() { ((ElementNode)e.Node).Element };
                }
            }

            ElementSelectionGroup group = new ElementSelectionGroup(selectedElements);

            TrackShackAlerts.NewSelectedElements(group);
        }

        protected override string TitleText
        {
            get
            {
                return "Object Hierarchy"; //Add track name?
            }
        }

    }
}
