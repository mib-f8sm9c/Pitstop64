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

namespace TrackShack.Controls.TrackControls
{
    public partial class PreviewTrackDock : TrackShackDockableWindow
    {
        public PreviewTrackDock()
        {
            InitializeComponent();

            TrackShackAlerts.TrackChanged += InitData;

            InitData();
        }

        public override void InitData()
        {
            if (TrackShackFloor.CurrentTrack != null)
                LoadTrack();
        }

        private void LoadTrack()
        {
            openGLControl.ClearGraphics();

            TrackShackFloor.LoadCurrentTrackIntoRomProject();


            //Replace this with better code. Consider bringing in that one entry in the track table, it may be more important than you think
            //Duplicated from TrackShackFloor
            int finalDLOffset = TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands.Count - 1;
            while ((TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_EndDL) ||
                (TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_MK64_EndDL))
                finalDLOffset--;

            //Now we have a non-end. Let's find the start of it!
            while (!(TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_EndDL) &&
                !(TrackShackFloor.CurrentTrack.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_MK64_EndDL))
                finalDLOffset--;

            openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands(TrackShackFloor.CurrentTrack.Track.F3DCommands, finalDLOffset));

            openGLControl.RefreshGraphics();
        }

        protected override string TitleText
        {
            get
            {
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

        public static string DockingContentId { get { return "trackWindow"; } }
    }
}
