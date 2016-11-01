using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MK64Pitstop.Modules.Tracks
{
    public class TrackModule : IModule
    {
        //In the future, we'll need to keep track of all the track data,
        // but for now a viewer will suffice

        public void UpdateRomData()
        {
            _control.UpdateControl();
        }

        public System.Windows.Forms.Control Control
        {
            get
            {
                if (_control == null)
                {
                    _control = new TrackControl();
                }
                return _control;
            }
        }
        private TrackControl _control;
    }
}
