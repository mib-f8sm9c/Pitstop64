using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Pitstop64.Data.Tracks;
using TrackShack.Controls;
using TrackShack.Data;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using System.Windows.Forms.Integration;
using Xceed.Wpf.AvalonDock.Layout;
using System.Collections.ObjectModel;

namespace TrackShack
{
    public partial class TrackShackForm : Form
    {
        private const string APPDATAFOLDER = "Pitstop64";
        private const string TRACKSHACKLAYOUT = "TrackShackLayout.xml";

        private bool unsavedChanges = false;

        public ControlController ControlController { get { return _controlController; } }
        private ControlController _controlController;

        public DockingManager DockingManager { get { return _dockingManager; } }
        private DockingManager _dockingManager = new DockingManager();

        public TrackShackForm()
        {
            InitializeComponent();

            _controlController = new ControlController(this);

            //Load default track textures?
        }

        private void loadTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TrackShackFloor.CurrentTrack = new TrackWrapper(TrackInfo.LoadFromFile(openFileDialog.FileName));
                TrackShackFloor.CurrentTrackPath = openFileDialog.FileName;
                TrackShackFloor.LoadCurrentTrackIntoRomProject();
                TrackShackAlerts.NewTrack();
            }
        }

        private void saveTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Debug code
            //XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockingManager);
            //serializer.Serialize("output.txt");
            SaveTrack(false);
        }

        private void saveTrackAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrack(true);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private void objectHierarchyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewDockableWindow(TrackShackDockableWindowType.ObjectHierarchy);
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewDockableWindow(TrackShackDockableWindowType.SurfaceRendering);
        }

        private void objectManipulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewDockableWindow(TrackShackDockableWindowType.ObjectManipulation);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pop up the about window here!
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadLayout();

            base.OnLoad(e);
        }

        private void TrackShackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save the layout data here
            SaveLayout();
        }

        private bool SaveTrack(bool askForPath)
        {
            if (askForPath || !File.Exists(TrackShackFloor.CurrentTrackPath))
            {
                if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;

                TrackShackFloor.CurrentTrackPath = saveFileDialog.FileName;
            }

            TrackInfo.SaveTrackInfo(TrackShackFloor.CurrentTrackPath, TrackShackFloor.CurrentTrack.Track);
            return true;
        }

        private void ExitProgram()
        {
            //Check for unsaved changes
            if (TrackShackFloor.CurrentTrack != null && TrackShackFloor.CurrentTrack.IsModified)
            {
                DialogResult result = MessageBox.Show("There are unsaved changes. Do you wish to save the track?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    //try to save. if fail, then return
                    if (!SaveTrack(true))
                        return;
                }
            }

            Application.Exit();
        }

        private void AddNewDockableWindow(TrackShackDockableWindowType type)
        {
            if (_controlController.WindowIsOpen(type))
            {
                //Select it?
                TrackShackDockableWindow window = _controlController.GetWindow(type);
                if (window.ParentLayout != null)
                {
                    if(window.ParentLayout is LayoutAnchorable)
                    {
                        if (((LayoutAnchorable)window.ParentLayout).IsHidden)
                        {
                            ((LayoutAnchorable)window.ParentLayout).Show();
                        }
                    }

                    if (!window.ParentLayout.IsActive)
                        DockingManager.ActiveContent = window.ParentLayout;
                }
            }
            else
            {
                //Create it
                TrackShackDockableWindow window = _controlController.GetWindow(type);
                ((LayoutAnchorable)window.ParentLayout).Content = new WindowsFormsHost() { Child = window };

                if (window.ParentLayout != null)
                {
                    if (window.ParentLayout is LayoutAnchorable)
                    {
                        ((LayoutAnchorable)window.ParentLayout).AddToLayout(DockingManager, AnchorableShowStrategy.Right);
                        ((LayoutAnchorable)window.ParentLayout).Float();
                    }
                    else if (window.ParentLayout is LayoutDocument)
                    {
                        //THIS IS KINDA BROKEN, AVOID DOCUMENTS IF POSSIBLE
                        //window.ParentLayout.IsActive = true;
                    }
                    else
                    {
                        //window.ParentLayout.IsActive = true;
                    }
                }
            }
        }

        private void LoadLayout()
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockingManager);

            serializer.LayoutSerializationCallback += (s, args) =>
            {
                TrackShackDockableWindowType type;
                if (_controlController.FindDockableTypeFromContentId(args.Model.ContentId, out type))
                {
                    TrackShackDockableWindow window = _controlController.GetWindow(type, ((LayoutContent)args.Model));
                    args.Content = new WindowsFormsHost() { Child = window };
                    args.Model.IsActiveChanged += window.OnIsActiveChanged;
                }
            };

            // The layout file for the roaming current user 
            string layoutFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                APPDATAFOLDER, TRACKSHACKLAYOUT);

            // Check if folder exists and if not, create it
            if (File.Exists(layoutFile))
                serializer.Deserialize(layoutFile);
            else
                serializer.Deserialize(
                    new System.IO.StringReader(
                    TrackShack.Properties.Settings.Default.DefaultLayout));

            dockingManagerHost.Child = _dockingManager;
        }

        private void SaveLayout()
        {
            // The folder for the roaming current user 
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(folder, APPDATAFOLDER);

            // Check if folder exists and if not, create it
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            //Save the layoutData
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockingManager);
            serializer.Serialize(Path.Combine(specificFolder, TRACKSHACKLAYOUT));
        }

    }
}
