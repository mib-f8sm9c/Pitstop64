using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data.Tracks;

namespace TrackShack.Data
{
    public class TrackWrapper
    {
        public TrackInfo OriginalTrack { get; private set; }
        public TrackInfo Track { get; private set; }

        public bool IsModified { get; private set; }

        public TrackWrapper(TrackWrapper trackWrapper, string newName)
        {
            OriginalTrack = new TrackInfo(trackWrapper.Track); //Lose the original track data
            OriginalTrack.TrackName = newName;
            CopyFromOriginal();
        }

        public TrackWrapper(TrackInfo origTrack)
        {
            OriginalTrack = origTrack;
            CopyFromOriginal();
        }

        private void CopyFromOriginal()
        {
            Track = new TrackInfo(OriginalTrack);
        }

        private void CopyToOriginal()
        {
            OriginalTrack = new TrackInfo(Track);
        }

        public override string ToString()
        {
            return Track.TrackName;
        }

        //Track editing & management code goes here!

    }
}
