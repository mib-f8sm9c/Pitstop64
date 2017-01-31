using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitstop64.Data.Tracks
{
    public class TrackItemsObject
    {
        public byte[] Data { get { return _data; } }
        private byte[] _data;

        //Not sure yet what goes in here! Let's keep it just a block of data for now
        public TrackItemsObject(byte[] data)
        {
            _data = data;
        }


    }
}
