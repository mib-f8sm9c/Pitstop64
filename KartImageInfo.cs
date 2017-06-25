using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Pitstop64
{
    public static class KartImageInfo
    {
        public static List<Pitstop64.MarioKartRomInfo.MK64ImageInfo> ImageLocations
        {
            get
            {
                if (_imageLocations == null)
                    LoadImageLocations();
                return _imageLocations;
            }
        }
        private static List<Pitstop64.MarioKartRomInfo.MK64ImageInfo> _imageLocations;

        private static void LoadImageLocations()
        {
            _imageLocations = new List<MarioKartRomInfo.MK64ImageInfo>(9640);
            
            //Open up a stream to the embedded text file
            string line;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("Pitstop64.KartImages.txt")))
            {
                while(!string.IsNullOrWhiteSpace(line = reader.ReadLine()))
                {
                    _imageLocations.Add(new MarioKartRomInfo.MK64ImageInfo(line));
                }
            }
        }
    }
}
