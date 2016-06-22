using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using MK64Pitstop.Data;

namespace MK64Pitstop.Services
{
    //Serves as a place to reference important N64DataElements, as well as resources that have
    // been externally added in to help keep track of them.
    public class MarioKart64ElementHub
    {
        private static MarioKart64ElementHub _instance = null;
        private static object syncObject = new object();

        public static MarioKart64ElementHub Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (syncObject)
                    {
                        if (_instance == null)
                            _instance = new MarioKart64ElementHub();
                    }
                }
                return _instance;
            }
        }

        public List<KartInfo> Karts { get; set; }
        public KartGraphicsReferenceBlock KartGraphicsBlock { get; set; }
        public TextBankBlock TextBank { get; set; }

        public MarioKart64ElementHub()
        {
            Karts = new List<KartInfo>();
        }

        public void ClearElements()
        {

        }

        public void ReadFromRomFile(RomFile file)
        {
            //Go through the elements, split it up as it should be (for loading from xml)
        }

    }
}
