using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Microcodes.F3DEX.DataElements;
using System.Collections.ObjectModel;
using Pitstop64.Data;
using System.Xml.Linq;
using Cereal64.Common;

namespace Pitstop64.Services.Hub
{
    /// <summary>
    /// Made to hold all the texture information in the ROM. For reference old & adding on new
    /// </summary>
    public class TextureHub : IXMLSerializable
    {
        public const string TEXTURE_HUB = "TextureHub";
        private const string IMAGES = "Images";
        private const string KART_IMAGES = "KartImages";

        public ReadOnlyCollection<MK64Image> Images { get { return _images.AsReadOnly(); } }
        private List<MK64Image> _images;

        public ReadOnlyCollection<MK64Image> KartImages { get { return _kartImages.AsReadOnly(); } }
        private List<MK64Image> _kartImages;

        private Dictionary<Texture, List<MK64Image>> _sharedTextureImages;
        private Dictionary<Palette, List<MK64Image>> _sharedPaletteImages;

        public bool HasImagesForTexture(Texture texture)
        {
            return _sharedTextureImages.ContainsKey(texture);
        }

        public List<MK64Image> ImagesForTexture(Texture texture)
        {
            return new List<MK64Image>(_sharedTextureImages[texture]);
        }

        public bool HasImagesForPalette(Palette palette)
        {
            return _sharedPaletteImages.ContainsKey(palette);
        }

        public List<MK64Image> ImagesForPalette(Palette palette)
        {
            return new List<MK64Image>(_sharedPaletteImages[palette]);
        }

        public TextureHub()
        {
            _images = new List<MK64Image>();
            _kartImages = new List<MK64Image>();
            _sharedTextureImages = new Dictionary<Texture, List<MK64Image>>();
            _sharedPaletteImages = new Dictionary<Palette, List<MK64Image>>();
        }

        public XElement GetAsXML()
        {
            XElement xml = new XElement(TEXTURE_HUB);

            XElement images = new XElement(IMAGES);

            foreach (MK64Image image in _images)
            {
                bool saveImageData = false;
                if (image.TextureOffset == -1 || image.PaletteOffset.Count(i => i == -1) > 0)
                    saveImageData = true;

                images.Add(image.GetAsXML(saveImageData));
            }

            xml.Add(images);

            XElement kartImages = new XElement(KART_IMAGES);

            foreach (MK64Image image in _kartImages)
            {
                //use this here?
                bool saveImageData = false;
                if (image.TextureOffset == -1 || image.PaletteOffset.Count(i => i == -1) > 0)
                    saveImageData = true;

                kartImages.Add(image.GetAsXML(saveImageData));
            }

            xml.Add(kartImages);

            return xml;
        }

        public void LoadReferencesFromXML(XElement xml)
        {
            //Note: This does not create the N64DataElements, it merely finds them and associates them
            ClearTextureData();

            foreach (XElement element in xml.Elements())
            {
                if (element.Name.ToString() == IMAGES)
                {
                    foreach (XElement imageEl in element.Elements())
                    {
                        MK64Image image = new MK64Image(imageEl);

                        AddImage(image);
                    }
                }
                if (element.Name.ToString() == KART_IMAGES)
                {
                    foreach (XElement imageEl in element.Elements())
                    {
                        MK64Image image = new MK64Image(imageEl);

                        AddKartImage(image);
                    }
                }
            }
        }

        public void ClearTextureData()
        {
            _images.Clear();
            _kartImages.Clear();
            _sharedTextureImages.Clear();
            _sharedPaletteImages.Clear();
        }

        public bool AddImage(MK64Image image)
        {
            if (!image.IsValidImage)
                return false;

            if (_images.Contains(image))
                return false;

            //To do: add extra checks here to make sure that the same texture/palette doesn't get added twice?

            _images.Add(image);

            if (image.ImageReference != null)
            {
                if (!_sharedTextureImages.ContainsKey(image.ImageReference.Texture))
                    _sharedTextureImages.Add(image.ImageReference.Texture, new List<MK64Image>());

                _sharedTextureImages[image.ImageReference.Texture].Add(image);

                if (image.Format == Texture.ImageFormat.CI)
                {
                    foreach(Palette p in image.ImageReference.BasePalettes)
                    {
                        if (!_sharedPaletteImages.ContainsKey(p))
                            _sharedPaletteImages.Add(p, new List<MK64Image>());

                        _sharedPaletteImages[p].Add(image);
                    }
                }
            }
            return true;
        }

        public bool RemoveImage(MK64Image image)
        {
            //Check that it exists
            if (!_images.Contains(image))
                return false;

            //We're gonna lock those original images in, sorry
            if (image.IsOriginalImage)
                return false;

            _images.Remove(image);

            if (_sharedTextureImages.ContainsKey(image.ImageReference.Texture) && _sharedTextureImages[image.ImageReference.Texture].Contains(image))
                _sharedTextureImages[image.ImageReference.Texture].Remove(image);

            foreach (Palette p in image.ImageReference.BasePalettes)
            {
                if (_sharedPaletteImages.ContainsKey(p) && _sharedPaletteImages[p].Contains(image))
                    _sharedPaletteImages[p].Remove(image);
            }

            return true;
        }

        public bool AddKartImage(MK64Image image)
        {
            if (!image.IsValidImage)
                return false;

            if (_kartImages.Contains(image))
                return false;

            //To do: add extra checks here to make sure that the same texture/palette doesn't get added twice?

            _kartImages.Add(image);

            if (image.ImageReference != null)
            {
                if (!_sharedTextureImages.ContainsKey(image.ImageReference.Texture))
                    _sharedTextureImages.Add(image.ImageReference.Texture, new List<MK64Image>());

                _sharedTextureImages[image.ImageReference.Texture].Add(image);

                if (image.Format == Texture.ImageFormat.CI)
                {
                    foreach (Palette p in image.ImageReference.BasePalettes)
                    {
                        if (!_sharedPaletteImages.ContainsKey(p))
                            _sharedPaletteImages.Add(p, new List<MK64Image>());

                        _sharedPaletteImages[p].Add(image);
                    }
                }
            }
            return true;
        }

        public bool RemoveKartImage(MK64Image image)
        {
            //Check that it exists
            if (!_kartImages.Contains(image))
                return false;

            //We're gonna lock those original images in, sorry
            if (image.IsOriginalImage)
                return false;

            _kartImages.Remove(image);

            if (_sharedTextureImages.ContainsKey(image.ImageReference.Texture) && _sharedTextureImages[image.ImageReference.Texture].Contains(image))
                _sharedTextureImages[image.ImageReference.Texture].Remove(image);

            foreach (Palette p in image.ImageReference.BasePalettes)
            {
                if (_sharedPaletteImages.ContainsKey(p) && _sharedPaletteImages[p].Contains(image))
                    _sharedPaletteImages[p].Remove(image);
            }

            return true;
        }
    }

}
