using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrackShack.Data;
using Cereal64.VisObj64.Data.OpenGL;
using Xceed.Wpf.AvalonDock.Layout;

namespace TrackShack.Controls.TrackControls
{
    public partial class ObjectManipulationControl : TrackShackDockableWindow
    {
        private ElementSelectionGroup _selectedGroup;
        //private ElementSelectionGroup _selectedGroup;
        private List<IVO64Vertex> _verticesMain, _verticesBackup;

        public ObjectManipulationControl(LayoutContent content)
            : base(content)
        {
            InitializeComponent();

            _selectedGroup = TrackShackFloor.SelectedGroup;
            TrackShackAlerts.SelectedElementsChanged += SelectedElementsChanged;

            InitData();
        }

        private void SelectedElementsChanged(ElementSelectionGroup group)
        {
            _selectedGroup = group;
            InitData();
        }

        public override void InitData()
        {
            if (_selectedGroup == null)
                return;

            DisableTextboxEvents();
            txtTX.Text = _selectedGroup.MinX.ToString();
            txtTY.Text = _selectedGroup.MinY.ToString();
            txtTZ.Text = _selectedGroup.MinZ.ToString();
            txtRX.Text = "0";
            txtRY.Text = "0";
            txtRZ.Text = "0";
            txtSX.Text = (_selectedGroup.MaxX - _selectedGroup.MinX).ToString();
            txtSY.Text = (_selectedGroup.MaxY - _selectedGroup.MinY).ToString();
            txtSZ.Text = (_selectedGroup.MaxZ - _selectedGroup.MinZ).ToString();
            EnableTextboxEvents();

            //Set up the vertex holders
            _verticesBackup = new List<IVO64Vertex>();
            _verticesMain = new List<IVO64Vertex>();
            foreach (VO64GraphicsElement el in _selectedGroup.Elements)
            {
                _verticesMain = _verticesMain.Union(el.Vertices).ToList();
            }
            foreach (IVO64Vertex v in _verticesMain)
            {
                _verticesBackup.Add(v.GetAsSimpleVertex());
            }
        }

        private void DisableTextboxEvents()
        {
            txtTX.TextChanged -= new EventHandler(TranslateUpdate);
            txtTY.TextChanged -= new EventHandler(TranslateUpdate);
            txtTZ.TextChanged -= new EventHandler(TranslateUpdate);
            txtRX.TextChanged -= new EventHandler(RotateUpdate);
            txtRY.TextChanged -= new EventHandler(RotateUpdate);
            txtRZ.TextChanged -= new EventHandler(RotateUpdate);
            txtSX.TextChanged -= new EventHandler(ScaleUpdate);
            txtSY.TextChanged -= new EventHandler(ScaleUpdate);
            txtSZ.TextChanged -= new EventHandler(ScaleUpdate);
        }

        private void EnableTextboxEvents()
        {
            txtTX.TextChanged += new EventHandler(TranslateUpdate);
            txtTY.TextChanged += new EventHandler(TranslateUpdate);
            txtTZ.TextChanged += new EventHandler(TranslateUpdate);
            txtRX.TextChanged += new EventHandler(RotateUpdate);
            txtRY.TextChanged += new EventHandler(RotateUpdate);
            txtRZ.TextChanged += new EventHandler(RotateUpdate);
            txtSX.TextChanged += new EventHandler(ScaleUpdate);
            txtSY.TextChanged += new EventHandler(ScaleUpdate);
            txtSZ.TextChanged += new EventHandler(ScaleUpdate);
        }

        private void TranslateUpdate(object sender, EventArgs e)
        {

        }

        private void RotateUpdate(object sender, EventArgs e)
        {

            btnApply.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void ScaleUpdate(object sender, EventArgs e)
        {
            //Get the translation difference
            double newX = double.Parse(txtSX.Text);
            double newY = double.Parse(txtSY.Text);
            double newZ = double.Parse(txtSZ.Text);



            double dX = newX / (_selectedGroup.MaxX - _selectedGroup.MinX);
            double dY = newY / (_selectedGroup.MaxY - _selectedGroup.MinY);
            double dZ = newZ / (_selectedGroup.MaxZ - _selectedGroup.MinZ);

            double centerX = (_selectedGroup.MaxX + _selectedGroup.MinX) / 2;
            double centerY = (_selectedGroup.MaxY + _selectedGroup.MinY) / 2;
            double centerZ = (_selectedGroup.MaxZ + _selectedGroup.MinZ) / 2;

            //Apply to all vertices
            foreach (IVO64Vertex vtx in _verticesMain)
            {
                vtx.X = (float)(centerX + (vtx.X - centerX) * dX);
                vtx.Y = (float)(centerY + (vtx.Y - centerY) * dY);
                vtx.Z = (float)(centerZ + (vtx.Z - centerZ) * dZ);
            }

            //Apply to the selection group info
            //TODO: A MORE SECURE WAY OF HANDLING THIS!!
            _selectedGroup.MinX = (float)(centerX + (_selectedGroup.MinX - centerX) * dX);
            _selectedGroup.MaxX = (float)(centerX + (_selectedGroup.MaxX - centerX) * dX);
            _selectedGroup.MinY = (float)(centerY + (_selectedGroup.MinY - centerY) * dY);
            _selectedGroup.MaxY = (float)(centerY + (_selectedGroup.MaxY - centerY) * dY);
            _selectedGroup.MinZ = (float)(centerZ + (_selectedGroup.MinZ - centerZ) * dZ);
            _selectedGroup.MaxZ = (float)(centerZ + (_selectedGroup.MaxZ - centerZ) * dZ);

            btnApply.Enabled = true;
            btnCancel.Enabled = true;

            foreach (VO64GraphicsElement el in _selectedGroup.Elements)
            {
                el.UpdateVertices();
            }

            UpdateViewer();
        }

        private void ApplyChanges()
        {
            for (int i = 0; i < _verticesBackup.Count; i++)
            {
                _verticesBackup[i] = _verticesMain[i].GetAsSimpleVertex();
                //_verticesBackup[i].X = _verticesMain[i].X;
                //_verticesBackup[i].Y = _verticesMain[i].Y;
                //_verticesBackup[i].Z = _verticesMain[i].Z;
                //_verticesBackup[i].U = _verticesMain[i].U;
                //_verticesBackup[i].V = _verticesMain[i].V;
                //_verticesBackup[i].NX = _verticesMain[i].NX;
                //_verticesBackup[i].NY = _verticesMain[i].NY;
                //_verticesBackup[i].NZ = _verticesMain[i].NZ;
            }
            //foreach (VO64GraphicsElement el in _selectedGroup.Elements)
            //    el.UpdateVertices();

            //UpdateViewer();

            btnApply.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void UndoChanges()
        {
            for (int i = 0; i < _verticesBackup.Count; i++)
            {
                _verticesMain[i].X = _verticesBackup[i].X;
                _verticesMain[i].Y = _verticesBackup[i].Y;
                _verticesMain[i].Z = _verticesBackup[i].Z;
                _verticesMain[i].U = _verticesBackup[i].U;
                _verticesMain[i].V = _verticesBackup[i].V;
                _verticesMain[i].NX = _verticesBackup[i].NX;
                _verticesMain[i].NY = _verticesBackup[i].NY;
                _verticesMain[i].NZ = _verticesBackup[i].NZ;
            }

            foreach (VO64GraphicsElement el in _selectedGroup.Elements)
                    el.UpdateVertices();

            UpdateViewer();

            btnApply.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void UpdateViewer()
        {
            TrackShackAlerts.SelectedElementsChanged -= SelectedElementsChanged;
            TrackShackAlerts.NewSelectedElements(_selectedGroup); //Will update the viewer too
            TrackShackAlerts.SelectedElementsChanged += SelectedElementsChanged;
        }

        public override TrackShackDockableWindowType WindowType
        {
            get
            {
                return TrackShackDockableWindowType.ObjectManipulation;
            }
        }

        public static string DockingContentId { get { return "objectManip"; } }

        protected override string TitleText
        {
            get
            {
                return "Object Manipulation"; //Add track name?
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UndoChanges();
        }


    }
}
