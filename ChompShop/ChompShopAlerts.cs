using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChompShop.Data;

namespace ChompShop
{
    public static class ChompShopAlerts
    {
        //Here we'll have a ton of delegates that'll alert us when certain events happen.

        //Basically a kart gets added or removed from the list
        public delegate void LoadedKartsChangedEvent();
        public static event LoadedKartsChangedEvent LoadedKartsChanged = delegate { };
        public static void UpdateKartsLoaded()
        {
            LoadedKartsChanged();
        }

        //More specifically a kart gets removed from the list
        public delegate void KartRemovedEvent(KartWrapper kart);
        public static event KartRemovedEvent KartRemoved = delegate { };
        public static void RemoveKart(KartWrapper kart)
        {
            KartRemoved(kart);
            LoadedKartsChanged();
        }

        //Update a kart's name: often requires a big change across forms
        public delegate void KartNameChangedEvent(KartWrapper kart);
        public static event KartNameChangedEvent KartNameChanged = delegate { };
        public static void UpdateKartName(KartWrapper kart)
        {
            KartNameChanged(kart);
        }

        //Change the reference kart: needs to update the kart viewers in all the windows
        public delegate void ReferenceKartChangedEvent(KartWrapper refKart);
        public static event ReferenceKartChangedEvent ReferenceKartChanged = delegate { };
        public static void UpdateReferenceKart(KartWrapper kart)
        {
            ReferenceKartChanged(kart);
        }

        //Kart image got removed: needs to affect the animation window
        public delegate void KartImageRemovedEvent(KartWrapper kart);
        public static event KartImageRemovedEvent KartImageRemoved = delegate { };
        public static void UpdateKartImages(KartWrapper kart)
        {
            KartImageRemoved(kart);
        }
    }
}
