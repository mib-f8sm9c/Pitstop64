using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pitstop64.Data.Tracks;
using Cereal64.Common.Rom;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DEX;
using Cereal64.Microcodes.F3DEX.DataElements;
using TrackShack.Data;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;

namespace TrackShack.Controls.TrackControls
{
    public partial class PreviewTrackForm : TrackShackWindow
    {
        public PreviewTrackForm(TrackWrapper track)
            : base(track)
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

        private void PreviewTrackForm_Shown(object sender, EventArgs e)
        {
            if(Track != null)
                LoadTrack();
        }

        protected override string TitleText
        {
            get
            {
                return "Preview Course - " + Track.Track.TrackName;
            }
        }

        public override TrackShackWindowType WindowType
        {
            get
            {
                return TrackShackWindowType.Preview;
            }
        }

    }
}
