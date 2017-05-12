using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data.Tracks;
using TrackShack.Data;
using Cereal64.VisObj64.Data.OpenGL;

namespace TrackShack
{
    public static class TrackShackAlerts
    {
        //Here we'll have a ton of delegates that'll alert us when certain events happen.

        //Update a tracks's name: often requires a change across forms
        public delegate void TrackNameChangedEvent(TrackWrapper track);
        public static event TrackNameChangedEvent TrackNameChanged = delegate { };
        public static void UpdateTrackName(TrackWrapper track)
        {
            TrackNameChanged(track);
        }

        public delegate void TrackChangedEvent();
        public static event TrackChangedEvent TrackChanged = delegate { };
        public static void NewTrack()
        {
            TrackChanged();
        }

        public delegate void RenderingGroupChangedEvent(VO64GraphicsCollection oldRender, VO64GraphicsCollection newRender);
        public static event RenderingGroupChangedEvent RenderingGroupChanged = delegate { };
        public static void NewRenderingGroup(VO64GraphicsCollection oldRender, VO64GraphicsCollection newRender)
        {
            RenderingGroupChanged(oldRender, newRender);
        }

        public delegate void SelectedElementsChangedEvent(ElementSelectionGroup selectedElements);
        public static event SelectedElementsChangedEvent SelectedElementsChanged = delegate { };
        public static void NewSelectedElements(ElementSelectionGroup selectedElements)
        {
            SelectedElementsChanged(selectedElements);
            UpdateViewer();
        }

        public delegate void ViewerUpdateRequiredEvent();
        public static event ViewerUpdateRequiredEvent ViewerUpdateRequired = delegate { };
        public static void UpdateViewer()
        {
            ViewerUpdateRequired();
        }

    }
}
