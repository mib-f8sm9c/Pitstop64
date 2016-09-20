using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Data.Karts;

namespace ChompShop.Data
{
    public class KartWrapper
    {
        public KartInfo OriginalKart { get; private set; }
        public KartInfo Kart { get; private set; }

        public bool IsModified { get; private set; }

        public KartWrapper(KartWrapper kartWrapper, string newName)
        {
            OriginalKart = new KartInfo(kartWrapper.Kart); //Lose the original kart data
            OriginalKart.KartName = newName;
            CopyFromOriginal();
        }

        public KartWrapper(KartInfo origKart)
        {
            OriginalKart = origKart;
            CopyFromOriginal();
        }

        public void CopyFromOriginal()
        {
            Kart = new KartInfo(OriginalKart);
        }

        public override string ToString()
        {
            return Kart.KartName;
        }

        public bool ValidKart
        {
            get
            {
                return HasValidNamePlate && HasValidPortraits && HasValidAnimations;
            }
        }
        
        //TO DO: FILL THIS OUT!!
        public bool HasValidNamePlate, HasValidPortraits, HasValidAnimations;
    }
}
