using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Data;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;
using MK64Pitstop.Data.Karts;

namespace MK64Pitstop.Modules.Karts
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
