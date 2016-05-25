using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using MK64Pitstop.Data;

namespace MK64Pitstop.Services
{
    public static class MarioKart64Reader
    {
        public static void ReadRom()
        {
            //The rom should be loaded as the first file in the rom project
            byte[] data = RomProject.Instance.Files[0].GetAsBytes();

            //Now read the different data bits here, if they haven't been read in yet
            for (int i = 0; i < MarioKartRomInfo.TKMK00TextureLocations.Length; i++)
            {
                ushort alpha = MarioKartRomInfo.TKMK00TextureLocations[i].AlphaColor;
                int offset = MarioKartRomInfo.TKMK00TextureLocations[i].RomOffset;
                int length = MarioKartRomInfo.TKMK00TextureLocations[i].Length;

                TKMK00Block tkmk;

                byte[] bytes = new byte[length];
                Array.Copy(data, offset, bytes, 0, length);

                tkmk = new TKMK00Block(offset, bytes, alpha);
                
                RomProject.Instance.Files[0].AddElement(tkmk);
            }
        }
    }
}
