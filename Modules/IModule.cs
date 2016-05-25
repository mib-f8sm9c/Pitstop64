using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MK64Pitstop.Modules
{
    public interface IModule
    {
        System.Windows.Forms.Control Control { get; }

        void UpdateRomData();
    }
}
