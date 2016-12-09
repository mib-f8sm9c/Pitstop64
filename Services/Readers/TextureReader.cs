using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Rom;
using MK64Pitstop.Services.Hub;
using MK64Pitstop.Data.Tracks;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.DataElements.Encoding;
using MK64Pitstop.Data;

namespace MK64Pitstop.Services.Readers
{
    //A COUPLE OF NOTES HERE:
    // 1. The nameplates for the karts are broken. They will not load correctly currently. Either disable kart reading or fix it.
    // 2. Instead of adding the new MIO0 blocks to the ROM, we need to add it to the results pile! Right? So it can be added after the fact!
    //     Answer: No. If those are shared between files, then we need to have the MIO0 available from the start to all of them!

    public static class TextureReader
    {
        public static void ReadRom(BackgroundWorker worker, byte[] rawData, MarioKart64ReaderResults finalResults)
        {
            TextureReaderResults results = new TextureReaderResults();

            //Here, load in the texture stuff, add it to the 
            foreach (MarioKartRomInfo.MK64ImageInfo imageInfo in MarioKartRomInfo.ImageLocations)
            {
                MK64Image image = new MK64Image(imageInfo, rawData);
                if (image.IsValidImage)
                {
                    results.AddImage(image);
                }
            }

            finalResults.TextureResults = results;
        }

        public static void ApplyResults(TextureReaderResults results)
        {
            foreach (MK64Image image in results.NewImages)
                MarioKart64ElementHub.Instance.TextureHub.AddImage(image);
        }
    }

    public class TextureReaderResults
    {
        public List<MK64Image> NewImages;
        public Dictionary<Texture, List<MK64Image>> ImagesByTexture;

        public TextureReaderResults()
        {
            NewImages = new List<MK64Image>();
            ImagesByTexture = new Dictionary<Texture, List<MK64Image>>();
        }

        public void AddImage(MK64Image image)
        {
            NewImages.Add(image);
            if (image.TextureEncoding != MK64Image.MK64ImageEncoding.TKMK00)
            {
                if (!ImagesByTexture.ContainsKey(image.ImageReference.Texture))
                {
                    ImagesByTexture.Add(image.ImageReference.Texture, new List<MK64Image>());
                }
                ImagesByTexture[image.ImageReference.Texture].Add(image);
            }
        }
    }
}
