using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MK64Pitstop.Modules.About
{
    public class AboutModule : IModule
    {
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
                    _control = new AboutControl();
                }
                return _control;
            }
        }
        private AboutControl _control;
    }
}
