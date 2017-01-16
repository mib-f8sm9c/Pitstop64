using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data;
using System.Windows.Forms;

namespace Pitstop64.Modules.Textures.SubControls
{
    public interface ITextureViewControl
    {
        MK64Image Image { get; set; }

        void Activate();

        void Deactivate();

        UserControl GetAsControl();

        TexturesControl.ImageUpdatedEvent ImageUpdated { get; set; }
    }
}
