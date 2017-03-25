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
        }


        private void LoadTrack()
        {
            openGLControl.ClearGraphics();

            TrackShackFloor.LoadCurrentTrackIntoRomProject();


            //Replace this with better code. Consider bringing in that one entry in the track table, it may be more important than you think
            //Duplicated from TrackShackFloor
            int finalDLOffset = Track.Track.F3DCommands.Commands.Count - 1;
            while ((Track.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_EndDL) ||
                (Track.Track.F3DCommands.Commands[finalDLOffset] is F3DEX_G_MK64_EndDL))
                finalDLOffset--;

            //Now we have a non-end. Let's find the start of it!
            while (!(Track.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_EndDL) &&
                !(Track.Track.F3DCommands.Commands[finalDLOffset - 1] is F3DEX_G_MK64_EndDL))
                finalDLOffset--;

            openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands(Track.Track.F3DCommands, finalDLOffset));

            openGLControl.RefreshGraphics();
        }

        //THIS IS DISABLED CURRENTLY!
        private void PreviewTrackForm_Shown(object sender, EventArgs e)
        {
            if (Track != null)
                LoadTrack();
        }

        protected override string TitleText
        {
            get
            {
                return "Preview Course - " + Track.Track.TrackName;
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
