using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitstop64.Modules.Text
{
    public class TextModule : IModule
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
                    _control = new TextControl();
                }
                return _control;
            }
        }
        private TextControl _control;
    }
}
