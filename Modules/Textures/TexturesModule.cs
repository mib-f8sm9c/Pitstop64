using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;
using MK64Pitstop.Data;

namespace MK64Pitstop.Modules.Textures
{
    public class TexturesModule : IModule
    {
        private List<TKMK00Block> _tkmkTextures = new List<TKMK00Block>();

        public void UpdateRomData()
        {
            _tkmkTextures.Clear();

            //Load the textures here
            if (RomProject.Instance.Files.Count > 0)
            {
                foreach (N64DataElement element in RomProject.Instance.Files[0].Elements)
                {
                    if (element is TKMK00Block)
                    {
                        _tkmkTextures.Add((TKMK00Block)element);
                    }
                }
            }

            _control.AddTKMK00Textures(_tkmkTextures);
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
