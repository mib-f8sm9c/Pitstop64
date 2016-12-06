using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Data;
using System.Windows.Forms;

namespace MK64Pitstop.Modules.Textures.SubControls
{
    public interface ITextureViewControl
    {
        MK64Image Image { get; set; }

        void Activate();

        void Deactivate();

        UserControl GetAsControl();
    }
}
