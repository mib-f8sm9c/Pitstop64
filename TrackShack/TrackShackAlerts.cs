using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data.Tracks;

namespace TrackShack
{
    public static class TrackShackAlerts
    {
        //Here we'll have a ton of delegates that'll alert us when certain events happen.

        //Update a tracks's name: often requires a change across forms
        public delegate void TrackNameChangedEvent(TrackInfo track);
        public static event TrackNameChangedEvent TrackNameChanged = delegate { };
        public static void UpdateTrackName(TrackInfo track)
        {
            TrackNameChanged(track);
        }

    }
}
