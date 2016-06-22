using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Data;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;

namespace MK64Pitstop.Modules.Karts
{
    public class KartModule : IModule
    {
        KartGraphicsReferenceBlock _block;

        public void UpdateRomData()
        {
            _block = null;

            //Load the kart info here
            if (RomProject.Instance.Files.Count > 0)
            {
                foreach (N64DataElement element in RomProject.Instance.Files[0].Elements)
                {
                    if (element is KartGraphicsReferenceBlock)
                    {
                        _block = (KartGraphicsReferenceBlock)element;
                    }
                }
            }

            if (_block != null)
                _control.UpdateReferences(_block);
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
