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
    public enum TrackShackWindowType
    {
        Tracks,
        Export,
        TrackInfo,
        Preview,
        ElementEditor,
        TrackConstructor
    }

    //Maintain the different windows being opened/closed and watch out for unsaved changes!
    public class ControlController
    {
        //Code for restoring a window from being minimized
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        private const uint SW_RESTORE = 0x09;

        private Dictionary<TrackShackWindowType, TrackShackWindow> SingleForms;
        private Dictionary<TrackWrapper, Dictionary<TrackShackWindowType, TrackShackWindow>> TrackForms;

        private TrackShackForm _parentForm;

        public ControlController(TrackShackForm parent)
        {
            SingleForms = new Dictionary<TrackShackWindowType, TrackShackWindow>();
            TrackForms = new Dictionary<TrackWrapper, Dictionary<TrackShackWindowType, TrackShackWindow>>();

            _parentForm = parent;
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            //Double check if there are unsaved changes?
        }

        private void HandleFormClosed(object sender, FormClosedEventArgs e)
        {
            //Remove from the dictionaries
            TrackShackWindow window = (TrackShackWindow)sender;

            if (window.Track == null)
                SingleForms.Remove(window.WindowType);
            else
            {
                if (TrackForms.ContainsKey(window.Track))
                    TrackForms[window.Track].Remove(window.WindowType);
            }
        }

        public void ShowSingleForm(TrackShackWindowType type)
        {
            if (SingleForms.ContainsKey(type))
            {
                TrackShackWindow form = SingleForms[type];

                if (form.WindowState == FormWindowState.Minimized)
                    ShowWindow(form.Handle, SW_RESTORE);

                form.BringToFront();
            }
            else
            {
                TrackShackWindow form = GenerateSingleForm(type);

                SingleForms.Add(type, form);
                form.MdiParent = _parentForm;
                form.FormClosing += HandleFormClosing;
                form.FormClosed += HandleFormClosed;
                form.Show();
            }

        }

        public void ShowTrackForm(TrackWrapper track, TrackShackWindowType type)
        {
            TrackShackWindow form;
            if (TrackForms.ContainsKey(track))
            {
                if (TrackForms[track].ContainsKey(type))
                {
                    form = TrackForms[track][type];

                    if (form.WindowState == FormWindowState.Minimized)
                        ShowWindow(form.Handle, SW_RESTORE);

                    form.BringToFront();

                    return;
                }
                else
                {
                    form = GenerateTrackForm(track, type);
                }
            }
            else
            {
                TrackForms.Add(track, new Dictionary<TrackShackWindowType, TrackShackWindow>());
                form = GenerateTrackForm(track, type);
            }

            TrackForms[track].Add(type, form);
            form.MdiParent = _parentForm;
            form.FormClosing += HandleFormClosing;
            form.FormClosed += HandleFormClosed;
            form.Show();
        }

        public TrackShackWindow GenerateSingleForm(TrackShackWindowType type)
        {
            switch (type)
            {
                case TrackShackWindowType.Tracks:
                    return null;
                case TrackShackWindowType.Export:
                    return null;
                    //Settings go here
            }

            return null;
        }

        public TrackShackWindow GenerateTrackForm(TrackWrapper track, TrackShackWindowType type)
        {
            switch (type)
            {
                case TrackShackWindowType.TrackInfo:
                    return null;
                case TrackShackWindowType.Preview:
                    return new PreviewTrackForm(track);
                case TrackShackWindowType.ElementEditor:
                    return null;
                case TrackShackWindowType.TrackConstructor:
                    return null;
            }

            return null;
        }

        public bool SingleFormIsOpen(TrackShackWindowType type)
        {
            return SingleForms.ContainsKey(type);
        }

        public bool TrackFormIsOpen(TrackWrapper track, TrackShackWindowType type)
        {
            if (!TrackForms.ContainsKey(track))
                return false;

            return TrackForms[track].ContainsKey(type);
        }

        public void ClearTrackForms(TrackWrapper track)
        {
            if (TrackForms.ContainsKey(track))
            {
                List<TrackShackWindow> forms = new List<TrackShackWindow>(TrackForms[track].Values);
                foreach (TrackShackWindow form in forms)
                {
                    form.Close();
                    TrackForms[track].Remove(form.WindowType);
                }
            }
        }
    }
}