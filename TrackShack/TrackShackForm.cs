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

        private DockingManager _dockingManager = new DockingManager();

        public TrackShackForm()
        {
            InitializeComponent();

            _controlController = new ControlController(this);

            //Load default track textures?
        }

        public DockingManager DockingManager { get { return _dockingManager; } }

        private void loadTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TrackShackFloor.CurrentTrack = new TrackWrapper(TrackInfo.LoadFromFile(openFileDialog.FileName));
                TrackShackFloor.CurrentTrackPath = openFileDialog.FileName;
                TrackShackAlerts.NewTrack();
            }
        }

        private void saveTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockingManager);

            serializer.Serialize("output.txt");
            //SaveTrack(false);
        }

        private void saveTrackAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrack(true);
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

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void objectHierarchyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewDockableWindow(TrackShackDockableWindowType.Test1);
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewDockableWindow(TrackShackDockableWindowType.Test2);
        }

        private void AddNewDockableWindow(TrackShackDockableWindowType type)
        {
            if (_controlController.WindowIsOpen(type))
            {
                //Select it?
                TrackShackDockableWindow window = _controlController.GetWindow(type);
                if (window.ParentLayout != null)
                    window.ParentLayout.IsSelected = true;
            }
            else
            {
                //Unhide it
                TrackShackDockableWindow window = _controlController.GetWindow(type);
                if (window.ParentLayout != null)
                {
                    if (window.ParentLayout is LayoutAnchorable)
                    {
                        LayoutAnchorable anchored = ((LayoutAnchorable)window.ParentLayout);
                        if (anchored.Parent != null)
                            anchored.Show();
                        else
                        {
                            LayoutAnchorablePaneGroup group = (LayoutAnchorablePaneGroup)
                                _dockingManager.Layout.RootPanel.Children.FirstOrDefault(c => c is LayoutAnchorablePaneGroup);
                            if (group != null)
                            {
                                //Add it back to the document pane
                                ((LayoutAnchorablePane)group.Children[0]).Children.Add(anchored);
                                //anchored.Hide();
                                anchored.Show();
                            }
                        }
                    }
                    else if (window.ParentLayout is LayoutDocument)
                    {
                        //THIS IS KINDA BROKEN, AVOID DOCUMENTS IF POSSIBLE
                        window.ParentLayout.IsActive = true;
                    }
                    else
                    {
                        window.ParentLayout.IsActive = true;
                    }
                }
            }
            //else //The old style, to automatically populate the window
            //{
            //    WindowsFormsHost host = new WindowsFormsHost() { Child = _controlController.GetWindow(type) };
            //    if (_controlController.IsDocumentWindow(type))
            //    {
            //        LayoutDocument document = new LayoutDocument();
            //        document.Content = host;
            //        document.ContentId = _controlController.GetContentId(type);
            //        LayoutDocumentPane pane = new LayoutDocumentPane(document);
            //        ((LayoutDocumentPane)((LayoutDocumentPaneGroup)_dockingManager.Layout.RootPanel.Children[0]).Children[0]).Children.Add(document);
            //    }
            //    else
            //    {
            //        LayoutAnchorable anchorable = new LayoutAnchorable();
            //        anchorable.Content = host;
            //        LayoutAnchorablePane p = new LayoutAnchorablePane(anchorable);
            //        LayoutAnchorablePaneGroup g = new LayoutAnchorablePaneGroup(p);

            //        _dockingManager.Layout.RootPanel.InsertChildAt(0, g);
            //        anchorable.Float();
            //    }
            //}
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

        private void LoadLayout()
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockingManager);

            serializer.LayoutSerializationCallback += (s, args) =>
            {
                TrackShackDockableWindowType type;
                if (_controlController.FindDockableTypeFromContentId(args.Model.ContentId, out type))
                {
                    TrackShackDockableWindow window = _controlController.GetWindow(type);
                    args.Content = new WindowsFormsHost() { Child = window };
                    args.Model.IsActiveChanged += window.OnIsActiveChanged;
                    window.ParentLayout = ((LayoutContent)args.Model);
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
