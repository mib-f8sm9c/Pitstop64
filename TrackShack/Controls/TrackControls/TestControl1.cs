using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrackShack.Controls.TrackControls
{
    public partial class TestControl1 : TrackShackDockableWindow
    {
        public TestControl1()
        {
            InitializeComponent();
        }

        public override TrackShackDockableWindowType WindowType
        {
            get
            {
                return TrackShackDockableWindowType.Test1;
            }
        }

        public static string DockingContentId { get { return "test1"; } }
    }
}
