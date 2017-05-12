using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Pitstop64.Data.Tracks;
using TrackShack.Controls.TrackControls;
using TrackShack.Data;
using Xceed.Wpf.AvalonDock.Layout;
using System.Windows.Forms.Integration;

namespace TrackShack.Controls
{
    public enum TrackShackDockableWindowType
    {
        PreviewTrack,
        Tools,
        ObjectHierarchy,
        SurfaceRendering,
        ObjectManipulation
    }

    //Maintain the different windows being opened/closed and watch out for unsaved changes!
    public class ControlController
    {
        ////Code for restoring a window from being minimized
        //[DllImport("user32.dll")]
        //private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        //private const uint SW_RESTORE = 0x09;

        private Dictionary<TrackShackDockableWindowType, TrackShackDockableWindow> TrackShackForms;
        private TrackShackForm _parentForm;

        public ControlController(TrackShackForm parent)
        {
            TrackShackForms = new Dictionary<TrackShackDockableWindowType, TrackShackDockableWindow>();
            _parentForm = parent;
        }

        public TrackShackDockableWindow GetWindow(TrackShackDockableWindowType type, LayoutContent content = null)
        {
            if (TrackShackForms.ContainsKey(type))
            {
                TrackShackDockableWindow form = TrackShackForms[type];

                return form;
            }
            else
            {
                TrackShackDockableWindow form = GenerateSingleForm(type, content);
                TrackShackForms.Add(type, form);

                return form;
            }

        }

        private TrackShackDockableWindow GenerateSingleForm(TrackShackDockableWindowType type, LayoutContent content)
        {
            if (content == null)
            {
                if(IsDocumentWindow(type))
                    content = new LayoutDocument();
                else
                    content = new LayoutAnchorable();
                content.ContentId = GetContentId(type);
            }

            switch (type)
            {
                case TrackShackDockableWindowType.PreviewTrack:
                    return new PreviewTrackDock(content);
                case TrackShackDockableWindowType.ObjectHierarchy:
                    return new ObjectHierarchyControl(content);
                case TrackShackDockableWindowType.SurfaceRendering:
                    return new SurfaceRenderingControl(content);
                case TrackShackDockableWindowType.ObjectManipulation:
                    return new ObjectManipulationControl(content);
            }

            return null;
        }

        public bool WindowIsOpen(TrackShackDockableWindowType type)
        {
            return (TrackShackForms.ContainsKey(type)/* && TrackShackForms[type].IsActive*/);
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
                case TrackShackDockableWindowType.ObjectHierarchy:
                    return ObjectHierarchyControl.DockingContentId;
                case TrackShackDockableWindowType.SurfaceRendering:
                    return SurfaceRenderingControl.DockingContentId;
                case TrackShackDockableWindowType.ObjectManipulation:
                    return ObjectManipulationControl.DockingContentId;
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
            else if (id == ObjectHierarchyControl.DockingContentId)
            {
                type = TrackShackDockableWindowType.ObjectHierarchy;
                return true;
            }
            else if (id == SurfaceRenderingControl.DockingContentId)
            {
                type = TrackShackDockableWindowType.SurfaceRendering;
                return true;
            }
            else if (id == ObjectManipulationControl.DockingContentId)
            {
                type = TrackShackDockableWindowType.ObjectManipulation;
                return true;
            }

            return false;
        }
    }
}