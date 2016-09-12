using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Data.Karts;

namespace ChompShop
{
    public static class ChompShopFloor
    {
        public static delegate void KartChangedEvent();

        public static event KartChangedEvent KartChanged = delegate { };

        public static KartInfo CurrentKart { get; private set; }

        public static KartInfo ReferenceKart { get; private set; }

        public static void NewKart(string newKartName)
        {
            CurrentKart = new KartInfo(newKartName, null, false);
        }

        public static void LoadKart(string newKartName)
        {
            //Load up the xml here
            //CurrentKart = new KartInfo(xml);
        }

        public static void LoadReferenceKart(string newKartName)
        {
            //Load up the xml here
            //ReferenceKart = new KartInfo(xml);
        }
    }
}
