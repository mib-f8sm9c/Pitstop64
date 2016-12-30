using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitstop64.Modules.Info
{
    public class RomInfoModule : IModule
    {
        //Display rom name/region/etc here. Also store the rom sizing
        // code here too.

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
                    _control = new RomInfoControl();
                }
                return _control;
            }
        }
        private RomInfoControl _control;
    }
}
