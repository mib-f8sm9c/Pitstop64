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

namespace TrackShack
{
    public partial class TrackShackForm : Form
    {
        private bool unsavedChanges = false;

        public ControlController ControlController { get { return _controlController; } }
        private ControlController _controlController;

        public TrackShackForm()
        {
            InitializeComponent();

            _controlController = new ControlController(this);

            //Load default track textures?
        }

        private void newTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TrackShackFloor.CurrentTrack = TrackInfo.LoadFromFile(openFileDialog.FileName);
                TrackShackFloor.CurrentTrackPath = openFileDialog.FileName;
            }
        }

        private void saveTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrack(false);
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

            TrackInfo.SaveTrackInfo(TrackShackFloor.CurrentTrackPath, TrackShackFloor.CurrentTrack);
            return true;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void previewTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controlController.ShowTrackForm(TrackShackFloor.CurrentTrack, TrackShackWindowType.Preview);
        }
    }
}
