using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;
using Pitstop64.Data.Karts;

namespace Pitstop64.Modules.Karts
{
    public class KartModule : IModule
    {
        public void UpdateRomData()
        {
            _control.UpdateReferences();
        }

        public System.Windows.Forms.Control Control
        {
            get
            {
                if (_control == null)
                {
                    _control = new KartControl();
                }
                return _control;
            }
        }
        private KartControl _control;
    }
}
