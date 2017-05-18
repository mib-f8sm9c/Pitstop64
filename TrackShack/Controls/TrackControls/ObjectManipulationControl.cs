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

        private float _lastRotX, _lastRotY, _lastRotZ;

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
            if (_selectedGroup == null || _selectedGroup.Elements.Count == 0)
            {
                tabControl.Enabled = false;
                lblName.Text = string.Empty;
                return;
            }

            tabControl.Enabled = true;

            if (_selectedGroup.Elements.Count == 1)
                lblName.Text = _selectedGroup.Elements[0].Name;
            else
                lblName.Text = "(Assorted Group)";

            DisableSliderEvents();
            numTX.Value = _selectedGroup.MinX;
            numTY.Value = _selectedGroup.MinY;
            numTZ.Value = _selectedGroup.MinZ;
            numRX.Value = 0.0;
            numRY.Value = 0.0;
            numRZ.Value = 0.0;
            _lastRotX = 0;
            _lastRotY = 0;
            _lastRotZ = 0;
            numSX.Value = (_selectedGroup.MaxX - _selectedGroup.MinX);
            numSY.Value = (_selectedGroup.MaxY - _selectedGroup.MinY);
            numSZ.Value = (_selectedGroup.MaxZ - _selectedGroup.MinZ);
            EnableSliderEvents();

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

        private void DisableSliderEvents()
        {
            numTX.ValueChanged -= TranslateUpdate;
            numTY.ValueChanged -= TranslateUpdate;
            numTZ.ValueChanged -= TranslateUpdate;
            numRX.ValueChanged -= RotateUpdate;
            numRY.ValueChanged -= RotateUpdate;
            numRZ.ValueChanged -= RotateUpdate;
            numSX.ValueChanged -= ScaleUpdate;
            numSY.ValueChanged -= ScaleUpdate;
            numSZ.ValueChanged -= ScaleUpdate;
        }

        private void EnableSliderEvents()
        {
            numTX.ValueChanged += TranslateUpdate;
            numTY.ValueChanged += TranslateUpdate;
            numTZ.ValueChanged += TranslateUpdate;
            numRX.ValueChanged += RotateUpdate;
            numRY.ValueChanged += RotateUpdate;
            numRZ.ValueChanged += RotateUpdate;
            numSX.ValueChanged += ScaleUpdate;
            numSY.ValueChanged += ScaleUpdate;
            numSZ.ValueChanged += ScaleUpdate;
        }

        private void TranslateUpdate(object sender, EventArgs e)
        {
            //Get the translation difference
            double dX = numTX.Value - _selectedGroup.MinX;
            double dY = numTY.Value - _selectedGroup.MinY;
            double dZ = numTZ.Value - _selectedGroup.MinZ;

            //Apply to all vertices
            foreach (IVO64Vertex vtx in _verticesMain)
            {
                vtx.X += (float)dX;
                vtx.Y += (float)dY;
                vtx.Z += (float)dZ;
            }

            //Apply to the selection group info
            //TODO: A MORE SECURE WAY OF HANDLING THIS!!
            //_selectedGroup.MinX += (float)dX;
            //_selectedGroup.MaxX += (float)dX;
            //_selectedGroup.MinY += (float)dY;
            //_selectedGroup.MaxY += (float)dY;
            //_selectedGroup.MinZ += (float)dZ;
            //_selectedGroup.MaxZ += (float)dZ;
            _selectedGroup.CalculateMinMaxes();

            btnApply.Enabled = true;
            btnCancel.Enabled = true;

            foreach (VO64GraphicsElement el in _selectedGroup.Elements)
            {
                el.UpdateVertices();
            }

            UpdateViewer();
        }

        private void RotateUpdate(object sender, EventArgs e)
        {
            //Get the scale difference
            double dX = (numRX.Value - _lastRotX) / 180 * Math.PI;
            double dY = (numRY.Value - _lastRotY) / 180 * Math.PI;
            double dZ = (numRZ.Value - _lastRotZ) / 180 * Math.PI;

            _lastRotX = (float)numRX.Value;
            _lastRotY = (float)numRY.Value;
            _lastRotZ = (float)numRZ.Value;

            double centerX = (_selectedGroup.MaxX + _selectedGroup.MinX) / 2;
            double centerY = (_selectedGroup.MaxY + _selectedGroup.MinY) / 2;
            double centerZ = (_selectedGroup.MaxZ + _selectedGroup.MinZ) / 2;

            //Apply to all vertices
            foreach (IVO64Vertex vtx in _verticesMain)
            {
                double relX = vtx.X - centerX;
                double relY = vtx.Y - centerY;
                double relZ = vtx.Z - centerZ;

                //Rotate around x-axis
                double distVector = Math.Sqrt(Math.Pow(relY, 2) + Math.Pow(relZ, 2));
                double angle = Math.Atan2(relZ, relY);

                relY = distVector * Math.Cos(angle + dX);
                relZ = distVector * Math.Sin(angle + dX);

                //Rotate around y-axis
                distVector = Math.Sqrt(Math.Pow(relX, 2) + Math.Pow(relZ, 2));
                angle = Math.Atan2(relZ, relX);

                relX = distVector * Math.Cos(angle + dY);
                relZ = distVector * Math.Sin(angle + dY);

                //Rotate around z-axis
                distVector = Math.Sqrt(Math.Pow(relX, 2) + Math.Pow(relY, 2));
                angle = Math.Atan2(relY, relX);

                relX = distVector * Math.Cos(angle + dZ);
                relY = distVector * Math.Sin(angle + dZ);

                //Apply changes to the vertex
                vtx.X = (float)(centerX + relX);
                vtx.Y = (float)(centerY + relY);
                vtx.Z = (float)(centerZ + relZ);
            }

            //Apply to the selection group info
            //TODO: A MORE SECURE WAY OF HANDLING THIS!!
            //_selectedGroup.MinX = (float)(centerX + (_selectedGroup.MinX - centerX) * dX);
            //_selectedGroup.MaxX = (float)(centerX + (_selectedGroup.MaxX - centerX) * dX);
            //_selectedGroup.MinY = (float)(centerY + (_selectedGroup.MinY - centerY) * dY);
            //_selectedGroup.MaxY = (float)(centerY + (_selectedGroup.MaxY - centerY) * dY);
            //_selectedGroup.MinZ = (float)(centerZ + (_selectedGroup.MinZ - centerZ) * dZ);
            //_selectedGroup.MaxZ = (float)(centerZ + (_selectedGroup.MaxZ - centerZ) * dZ);
            _selectedGroup.CalculateMinMaxes();

            btnApply.Enabled = true;
            btnCancel.Enabled = true;

            _lastRotX = (float)numRX.Value;
            _lastRotY = (float)numRY.Value;
            _lastRotZ = (float)numRZ.Value;

            foreach (VO64GraphicsElement el in _selectedGroup.Elements)
            {
                el.UpdateVertices();
            }

            UpdateViewer();
        }

        private void ScaleUpdate(object sender, EventArgs e)
        {
            //Get the scale difference
            double dX = numSX.Value / (_selectedGroup.MaxX - _selectedGroup.MinX);
            double dY = numSY.Value / (_selectedGroup.MaxY - _selectedGroup.MinY);
            double dZ = numSZ.Value / (_selectedGroup.MaxZ - _selectedGroup.MinZ);

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
            //_selectedGroup.MinX = (float)(centerX + (_selectedGroup.MinX - centerX) * dX);
            //_selectedGroup.MaxX = (float)(centerX + (_selectedGroup.MaxX - centerX) * dX);
            //_selectedGroup.MinY = (float)(centerY + (_selectedGroup.MinY - centerY) * dY);
            //_selectedGroup.MaxY = (float)(centerY + (_selectedGroup.MaxY - centerY) * dY);
            //_selectedGroup.MinZ = (float)(centerZ + (_selectedGroup.MinZ - centerZ) * dZ);
            //_selectedGroup.MaxZ = (float)(centerZ + (_selectedGroup.MaxZ - centerZ) * dZ);
            _selectedGroup.CalculateMinMaxes();

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
            }
            //foreach (VO64GraphicsElement el in _selectedGroup.Elements)
            //    el.UpdateVertices();

            ResetSliders();

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

            _selectedGroup.CalculateMinMaxes();

            UpdateViewer();

            btnApply.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void ResetSliders()
        {
            DisableSliderEvents();

            numTX.RecenterSlider();
            numTY.RecenterSlider();
            numTZ.RecenterSlider();
            numRX.Value = 0;
            numRY.Value = 0;
            numRZ.Value = 0;
            numSX.RecenterSlider();
            numSY.RecenterSlider();
            numSZ.RecenterSlider();

            EnableSliderEvents();
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
                return "Object Info"; //Add track name?
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
