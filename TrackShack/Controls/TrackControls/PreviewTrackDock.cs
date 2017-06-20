using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DEX;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using TrackShack.Data;
using Cereal64.Common.DataElements;
using Cereal64.Common.Rom;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Utils;
using Cereal64.VisObj64.Data.OpenGL;
using Xceed.Wpf.AvalonDock.Layout;

namespace TrackShack.Controls.TrackControls
{
    public partial class PreviewTrackDock : TrackShackDockableWindow
    {
        private List<VO64GraphicsCollection> _renderedCollection;

        public PreviewTrackDock(LayoutContent content)
            : base(content)
        {
            InitializeComponent();

            //TrackShackAlerts.TrackChanged += InitData;
            _renderedCollection = new List<VO64GraphicsCollection>();
            TrackShackAlerts.RenderingGroupChanged += RenderGroupChanged;
            TrackShackAlerts.ViewerUpdateRequired += UpdateViewer;
            TrackShackAlerts.SelectedElementsChanged += SelectedElementsChanged;

            openGLControl.SelectedElementsChanged += openGLControl_SelectedElementsChanged;

            InitData();
        }

        public override void InitData()
        {
            if (TrackShackFloor.CurrentTrack != null)
                LoadTrack();

            SetMouseModeChecked();

            ReRender();
        }

        private void LoadTrack()
        {
            openGLControl.ClearGraphics();
            _renderedCollection.Clear();

            //TrackShackFloor.LoadCurrentTrackIntoRomProject();


            //Replace this with better code. Consider bringing in that one entry in the track table, it may be more important than you think
            //Duplicated from TrackShackFloor
            //int finalDLOffset = TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands.Count - 1;
            //while ((TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_EndDL) ||
            //    (TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_MK64_EndDL))
            //    finalDLOffset--;

            ////Now we have a non-end. Let's find the start of it!
            //while (!(TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_EndDL) &&
            //    !(TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_MK64_EndDL))
            //    finalDLOffset--;


            //debug
            //finalDLOffset = 5288;

            //openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands(TrackShackFloor.CurrentTrack.Track.F3DCommands, finalDLOffset));

            //debugRomProject.Instance.Files[3], 0x118


            //THIS WHOLE BLOCK WAS THE OLD METHOD THAT WORKED!!!
            //N64DataElement el;
            //int render = TrackShackFloor.CurrentTrack.Track.RenderTableOffset.Offset;
            //byte[] data = RomProject.Instance.Files[3].GetAsBytes();
            //List<VO64GraphicsCollection> collections = new List<VO64GraphicsCollection>();
            //for (; render < RomProject.Instance.Files[3].FileLength; render += 4)
            //{
            //    if (RomProject.Instance.Files[3].HasElementExactlyAt(ByteHelper.ReadInt(data, render) & 0x00FFFFFF, out el))
            //        openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //}


            _renderedCollection.Add(TrackShackFloor.SelectedRenderingGroup);
            


            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x90, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x278, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x3C0, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x188, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x118, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x328, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x218, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));
            //if (RomProject.Instance.Files[3].HasElementExactlyAt(0x458, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0));

            //if (RomProject.Instance.Files[1].HasElementExactlyAt(0, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0x380 / 8));

            //if (RomProject.Instance.Files[1].HasElementExactlyAt(0, out el))
            //    openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands((F3DEXCommandCollection)el, 0x9688 / 8));

        }

        private void ReRender()
        {
            openGLControl.GraphicsCollections.Clear();
            openGLControl.GraphicsCollections.AddRange(_renderedCollection);
            openGLControl.RefreshGraphics();
        }

        private void RenderGroupChanged(VO64GraphicsCollection oldC, VO64GraphicsCollection newC)
        {
            if (oldC != null && _renderedCollection.Contains(oldC))
            {
                _renderedCollection.Remove(oldC);
            }

            if (newC != null)
            {
                _renderedCollection.Insert(0, newC);
            }

            ReRender();
        }

        private void SelectedElementsChanged(ElementSelectionGroup elements)
        {
            openGLControl.ClearSelectedElements();

            if(elements != null)
            {
                foreach(VO64GraphicsElement g in elements.Elements)
                {
                    openGLControl.SelectElement(g);
                }
            }
            //else

            ReRender();
        }

        protected override string TitleText
        {
            get
            {
                if (TrackShackFloor.CurrentTrack == null)
                    return "Preview Course";

                return "Preview Course - " + TrackShackFloor.CurrentTrack.Track.TrackName;
            }
        }

        public override TrackShackDockableWindowType WindowType
        {
            get
            {
                return TrackShackDockableWindowType.PreviewTrack;
            }
        }

        public void UpdateViewer()
        {
            openGLControl.RefreshGraphics();
        }

        public static string DockingContentId { get { return "trackWindow"; } }

        private void SetMouseModeChecked()
        {
            cameraToolStripMenuItem.Checked = false;
            selectToolStripMenuItem.Checked = false;
            switch (openGLControl.MouseMode)
            {
                case VisObj64.Visualization.OpenGL.OpenGLControl.MouseFunction.Camera:
                    cameraToolStripMenuItem.Checked = true;
                    break;
                case VisObj64.Visualization.OpenGL.OpenGLControl.MouseFunction.Select:
                    selectToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openGLControl.MouseMode = VisObj64.Visualization.OpenGL.OpenGLControl.MouseFunction.Camera;
            SetMouseModeChecked();
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openGLControl.MouseMode = VisObj64.Visualization.OpenGL.OpenGLControl.MouseFunction.Select;
            SetMouseModeChecked();
        }

        private void openGLControl_SelectedElementsChanged(object sender, EventArgs e)
        {
            //Update other forms with this info
            TrackShackAlerts.SelectedElementsChanged -= SelectedElementsChanged;

            TrackShackAlerts.NewSelectedElements(new ElementSelectionGroup(openGLControl.SelectedElements.ToList()));

            TrackShackAlerts.SelectedElementsChanged += SelectedElementsChanged;
        }
    }
}
