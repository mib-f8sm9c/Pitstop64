using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Utils.Encoding;
using Pitstop64.Services.Hub;

namespace Pitstop64.Data.Textures
{
    public class CommonTexturesBlock
    {
        public MIO0Block EncodedData { get; private set; }

        private byte[] DecodedDataCopy { get; set; }

        private bool _changesMade;

        public const int CTB_OFFSET = 0x132B50;
        public const int CTB_LENGTH = 0x12920;

        //public List<Texture> IncludedTextures;
        //public List<Palette> IncludedPalettes;

        public CommonTexturesBlock(MIO0Block block)
        {
            //Load it in, set it up
            EncodedData = block;

            CreateDecodedDataCopy();

            _changesMade = false;
            
            //LoadTexturesAndPalettesFromBlock();
        }

        private void CreateDecodedDataCopy()
        {
            if(DecodedDataCopy == null || DecodedDataCopy.Length != EncodedData.DecodedData.Length)
                DecodedDataCopy = new byte[EncodedData.DecodedData.Length];

            Array.Copy(EncodedData.DecodedData, 0, DecodedDataCopy, 0, EncodedData.DecodedData.Length);

        }

        public bool SetData(int blockOffset, byte[] data, bool forceSave = false)
        {
            if (data == null || DecodedDataCopy == null || blockOffset < 0 || blockOffset + data.Length > DecodedDataCopy.Length)
            {
                return false;
            }

            Array.Copy(data, 0, DecodedDataCopy, blockOffset, data.Length);
            _changesMade = true;

            if (forceSave)
                return TrySave();
            else
                return true;
        }

        public bool TrySave()
        {
            if (_changesMade)
            {
                byte[] recodedData = MIO0.Encode(DecodedDataCopy);

                if (recodedData.Length > CTB_LENGTH)
                {
                    //Reset decoded data
                    CreateDecodedDataCopy();

                    return false;
                }

                EncodedData.RawData = recodedData;

                //Reset data in all the stupid variables
                SetNewRawData();

                _changesMade = false;
            }

            return true;
        }

        private void SetNewRawData()
        {
            foreach (N64DataElement element in EncodedData.Elements)
            {
                //set the raw data
                byte[] newData = new byte[element.RawDataSize];
                Array.Copy(DecodedDataCopy, element.FileOffset, newData, 0, element.RawDataSize);
                element.RawData = newData;

                if (element is Texture && MarioKart64ElementHub.Instance.TextureHub.HasImagesForTexture((Texture)element))
                {
                    //Update images involved
                    foreach (MK64Image img in MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture((Texture)element))
                    {
                        img.ImageReference.UpdateImage();
                    }
                }
                else if (element is Palette && MarioKart64ElementHub.Instance.TextureHub.HasImagesForPalette((Palette)element))
                {
                    //Update images involved
                    foreach (MK64Image img in MarioKart64ElementHub.Instance.TextureHub.ImagesForPalette((Palette)element))
                    {
                        img.ImageReference.UpdateImage();
                    }
                }
            }
        }

        //private void LoadTexturesAndPalettesFromBlock()
        //{
        //    foreach (N64DataElement element in EncodedData.Elements)
        //    {
        //        if (element is Texture)
        //            IncludedTextures.Add((Texture)element);
        //        else if (element is Palette)
        //            IncludedPalettes.Add((Palette)element);
        //    }
        //}

        //public void UpdateImage(MK64Image image)
        //{
        //    UpdateImages(new List<MK64Image> () { image });
        //}

        //public void UpdateImages(IList<MK64Image> images)
        //{
        //    List<Texture> involvedTextures = new List<Texture>();
        //    List<Palette> involvedPalettes = new List<Palette>();

        //    foreach (MK64Image image in images)
        //    {
        //        if (image.TextureEncoding == MK64Image.MK64ImageEncoding.MIO0 && image.TextureOffset == Offset)
        //        {
        //            involvedTextures.Add(image.ImageReference.Texture);
        //        }

        //        for(int i = 0; i < image.PaletteOffset.Count; i++)
        //        {
        //            if (image.PaletteEncoding[i] == MK64Image.MK64ImageEncoding.MIO0 && image.PaletteOffset[i] == Offset)
        //            {
        //                involvedPalettes.Add(image.ImageReference.BasePalettes[i]);
        //            }
        //        }
        //    }

        //    //eliminate those that didn't change
        //    List<Texture> NewIncludedTextures = new List<Texture>(IncludedTextures);
        //    List<Palette> NewIncludedPalettes = new List<Palette>(IncludedPalettes);

        //    Texture oldTexture;
        //    Palette oldPalette;

        //    foreach (Texture tex in involvedTextures)
        //    {
        //        //if((oldTexture = NewIncludedTextures.
        //    }
        //}
    }
}
