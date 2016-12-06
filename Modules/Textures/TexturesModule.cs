using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;
using MK64Pitstop.Data;

namespace MK64Pitstop.Modules.Textures
{
    /*For MK64:
        ~Make the new texture control work
        !Make a texture-loading feature
        !Enumerate a few of the textures to be loaded
        !Test the texture control, make it actually work
        -Load up the big MIO0 block, make it work!
        -Enumerate all the textures to be loaded
    */

    public class TexturesModule : IModule
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
                    _control = new TexturesControl();
                }
                return _control;
            }
        }
        private TexturesControl _control;
    }
}
