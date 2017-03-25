using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Pitstop64.Data.Tracks;
using TrackShack.Controls.TrackControls;
using TrackShack.Data;

namespace TrackShack.Controls
{
    public enum TrackShackDockableWindowType
    {
        ObjectHierarchy,
        Tools,
        PreviewTrack,
        Test1,
        Test2
    }

    //Maintain the different windows being opened/closed and watch out for unsaved changes!
    public class ControlController
    {
        //Code for restoring a window from being minimized
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        private const uint SW_RESTORE = 0x09;

        private Dictionary<TrackShackDockableWindowType, TrackShackDockableWindow> TrackShackForms;
        private TrackShackForm _parentForm;

        public ControlController(TrackShackForm parent)
        {
            TrackShackForms = new Dictionary<TrackShackDockableWindowType, TrackShackDockableWindow>();
            _parentForm = parent;
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            //Double check if there are unsaved changes?
        }

        private void HandleFormClosed(object sender, FormClosedEventArgs e)
        {
            //Remove from the dictionaries
            TrackShackDockableWindow window = (TrackShackDockableWindow)sender;
            TrackShackForms.Remove(window.WindowType);
        }

        public TrackShackDockableWindow GetWindow(TrackShackDockableWindowType type)
        {
            if (TrackShackForms.ContainsKey(type))
            {
                TrackShackDockableWindow form = TrackShackForms[type];

                return form;
            }
            else
            {
                TrackShackDockableWindow form = GenerateSingleForm(type);

                TrackShackForms.Add(type, form);

                return form;
            }

        }

        public TrackShackDockableWindow GenerateSingleForm(TrackShackDockableWindowType type)
        {
            switch (type)
            {
                case TrackShackDockableWindowType.PreviewTrack:
                    return new PreviewTrackDock();
                case TrackShackDockableWindowType.Test1:
                    return new TestControl1();
                case TrackShackDockableWindowType.Test2:
                    return new TestControl2();
                    //Settings go here
            }

            return null;
        }

        public bool WindowIsOpen(TrackShackDockableWindowType type)
        {
            return (TrackShackForms.ContainsKey(type) && TrackShackForms[type].IsActive);
        }

        public bool IsDocumentWindow(TrackShackDockableWindowType type)
        {
            switch (type)
            {
                case TrackShackDockableWindowType.PreviewTrack:
                    return true;
                default:
                    return false;
            }
        }

        public string GetContentId(TrackShackDockableWindowType type)
        {
            switch (type)
            {
                case TrackShackDockableWindowType.PreviewTrack:
                    return PreviewTrackDock.DockingContentId;
                case TrackShackDockableWindowType.Test1:
                    return TestControl1.DockingContentId;
                case TrackShackDockableWindowType.Test2:
                    return TestControl2.DockingContentId;
            }

            return string.Empty;
        }

        public bool FindDockableTypeFromContentId(string id, out TrackShackDockableWindowType type)
        {
            type = TrackShackDockableWindowType.PreviewTrack;

            if (id == PreviewTrackDock.DockingContentId)
            {
                type = TrackShackDockableWindowType.PreviewTrack;
                return true;
            }
            else if (id == TestControl1.DockingContentId)
            {
                type = TrackShackDockableWindowType.Test1;
                return true;
            }
            else if (id == TestControl2.DockingContentId)
            {
                type = TrackShackDockableWindowType.Test2;
                return true;
            }

            return false;
        }
    }
}