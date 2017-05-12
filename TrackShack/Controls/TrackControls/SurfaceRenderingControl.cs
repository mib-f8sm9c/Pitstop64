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
using Xceed.Wpf.AvalonDock.Layout;

namespace TrackShack.Controls.TrackControls
{
    public partial class SurfaceRenderingControl : TrackShackDockableWindow
    {
        public SurfaceRenderingControl(LayoutContent content)
            : base(content)
        {
            InitializeComponent();
            cbDir.SelectedIndex = 0;

            //Do alert stuff later : )
            TrackShackAlerts.TrackChanged += LoadDataIntoList;

            LoadDataIntoList();
        }

        private void LoadDataIntoList()
        {
            //Load from the TrackShackFloor

            lbGroups.Items.Clear();
            lbGroups.Items.Add(new RenderGroupPack("All", TrackShackFloor.RenderGroupUnion));
            int i = 1;
            foreach (SurfaceRenderGroup group in TrackShackFloor.RenderingGroups)
            {
                lbGroups.Items.Add(new SurfaceGroupPack(string.Format("Group {0}", i++), group));
            }

            if (TrackShackFloor.SelectedRenderingGroup == null || TrackShackFloor.SelectedRenderingGroup == TrackShackFloor.RenderGroupUnion)
                lbGroups.SelectedIndex = 0;
            else
            {
                lbGroups.SelectedIndex = TrackShackFloor.RenderingGroups.FindIndex(g =>
                    g.Direction1 == TrackShackFloor.SelectedRenderingGroup ||
                    g.Direction2 == TrackShackFloor.SelectedRenderingGroup ||
                    g.Direction3 == TrackShackFloor.SelectedRenderingGroup ||
                    g.Direction4 == TrackShackFloor.SelectedRenderingGroup);
            }
        }

        public override TrackShackDockableWindowType WindowType
        {
            get
            {
                return TrackShackDockableWindowType.SurfaceRendering;
            }
        }

        protected override string TitleText
        {
            get
            {
                return "Surface Rendering"; //Add track name?
            }
        }

        public static string DockingContentId { get { return "SurfaceRendering"; } }

        internal class RenderGroupPack
        {
            public string Name;
            public VO64GraphicsCollection Group;

            public RenderGroupPack(string name, VO64GraphicsCollection group)
            {
                Name = name;
                Group = group;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        internal class SurfaceGroupPack
        {
            public string Name;
            public SurfaceRenderGroup Group;

            public SurfaceGroupPack(string name, SurfaceRenderGroup group)
            {
                Name = name;
                Group = group;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void lbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbGroups.SelectedItem == null || lbGroups.SelectedItem is RenderGroupPack)
                cbDir.Enabled = false;
            else
                cbDir.Enabled = true;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (lbGroups.SelectedItem == null)
                return; //block this?

            if (lbGroups.SelectedItem is RenderGroupPack)
            {
                TrackShackFloor.SelectedRenderingGroup = ((RenderGroupPack)lbGroups.SelectedItem).Group;
            }
            else if (lbGroups.SelectedItem is SurfaceGroupPack)
            {
                switch (cbDir.SelectedIndex)
                {
                    case 0:
                        TrackShackFloor.SelectedRenderingGroup = ((SurfaceGroupPack)lbGroups.SelectedItem).Group.Direction1;
                        break;
                    case 1:
                        TrackShackFloor.SelectedRenderingGroup = ((SurfaceGroupPack)lbGroups.SelectedItem).Group.Direction2;
                        break;
                    case 2:
                        TrackShackFloor.SelectedRenderingGroup = ((SurfaceGroupPack)lbGroups.SelectedItem).Group.Direction3;
                        break;
                    case 3:
                        TrackShackFloor.SelectedRenderingGroup = ((SurfaceGroupPack)lbGroups.SelectedItem).Group.Direction4;
                        break;
                }
            }
        }
    }
}
