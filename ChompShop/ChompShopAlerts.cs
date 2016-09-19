using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChompShop
{
    public static class ChompShopAlerts
    {
        //Here we'll have a ton of delegates that'll alert us when certain events happen.
        public delegate void LoadedKartsChangedEvent();
        public static event LoadedKartsChangedEvent LoadedKartsChanged = delegate { };
        public static void UpdateKartsLoaded()
        {
            LoadedKartsChanged();
        }
    }
}
