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

namespace TrackShack.Controls.TrackControls
{
    public partial class PreviewTrackForm : TrackShackWindow
    {
        public PreviewTrackForm(TrackInfo track)
            : base(track)
        {
            InitializeComponent();
        }

        private void LoadTrack()
        {
            openGLControl.ClearGraphics();

            TrackShackFloor.LoadCurrentTrackIntoRomProject();


            openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands(Track.F3DCommands));
           
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
                return "Preview Course - " + Track.TrackName;
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
