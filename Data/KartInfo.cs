using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Utils.Encoding;

namespace MK64Pitstop.Data
{
    /// <summary>
    /// Stores information about a specific kart character
    /// </summary>
    public class KartInfo
    {
        private const string NAME = "name";

        private const string IMAGES = "images";
        private const string IMAGE = "image";
        private const string IMAGE_NAME = "name";
        private const string IMAGE_OFFSET = "offset";

        private const string ANIMATIONS = "animations";
        private const string ANIMATION = "animation";
        private const string ANIMATION_TYPE = "type";

        private const string PALETTE = "palette";
        private const string PALETTE_OFFSET = "offset";

        public string KartName { get; set; }

        public Dictionary<string, KartImage> KartImagePool { get; private set; }

        public List<KartAnimationSeries> KartAnimations { get; private set; }

        public Palette ImagePalette { get; private set; }

        public KartInfo()
            : this("NewChar", null)
        {
        }

        public KartInfo(string kartName, Palette palette)
        {
            KartName = kartName;
            KartImagePool = new Dictionary<string, KartImage>();
            KartAnimations = new List<KartAnimationSeries>();
            ImagePalette = palette;
        }

        public KartInfo(XElement xml)
        {
            KartName = xml.Attribute(NAME).Value;
            KartImagePool = new Dictionary<string, KartImage>();
            KartAnimations = new List<KartAnimationSeries>();

            XElement images = xml.Element(IMAGES);
            foreach (XElement image in images.Elements())
            {
                string name = image.Attribute(IMAGE_NAME).Value;
                int offset = int.Parse(image.Attribute(IMAGE_OFFSET).Value);

                //Load the ImageMIO0Block here
                throw new NotImplementedException();

                KartImagePool.Add(name, null);
            }
            XElement animations = xml.Element(ANIMATIONS);
            foreach (XElement animation in animations.Elements())
            {
                KartAnimationSeries newAnim = new KartAnimationSeries();

                newAnim.KartAnimationType = int.Parse(animation.Attribute(ANIMATION_TYPE).Value);
                foreach (XElement image in animation.Elements())
                {
                    newAnim.OrderedImageNames.Add(image.Attribute(IMAGE_NAME).Value);
                }
                KartAnimations.Add(newAnim);
            }
        }

        public XElement GetAsXML()
        {
            XElement xml = new XElement(this.GetType().ToString()); //Can derive actual type from name with N64DataElementFactory

            xml.Add(new XAttribute(NAME, KartName));
            XElement xmlImages = new XElement(IMAGES);
            foreach (string key in KartImagePool.Keys)
            {
                XElement xmlImage = new XElement(IMAGE);
                xmlImage.Add(new XAttribute(IMAGE_NAME, key));
                //xmlImage.Add(new XAttribute(IMAGE_OFFSET, KartImagePool[key].FileOffset));
                xmlImages.Add(xmlImage);
            }
            xml.Add(xmlImages);
            XElement xmlAnimations = new XElement(ANIMATIONS);
            foreach (KartAnimationSeries anim in KartAnimations)
            {
                XElement xmlAnim = new XElement(ANIMATION);
                xmlAnim.Add(new XAttribute(ANIMATION_TYPE, anim.KartAnimationType));
                foreach (string image in anim.OrderedImageNames)
                {
                    XElement xmlImage = new XElement(IMAGE);
                    xmlImage.Add(new XAttribute(IMAGE_NAME, image));
                    xmlAnim.Add(xmlImage);
                }
                xmlAnimations.Add(xmlAnim);
            }
            xml.Add(xmlAnimations);

            return xml;
        }
    }

    /// <summary>
    /// Represents one set of images that make up one animation for the kart. Made so that it can
    ///  be a variable length of images and still work.
    /// </summary>
    public class KartAnimationSeries
    {
        //Store the animation type in a bitmask just because
        public enum KartAnimationTypeFlag
        {
            RearTurnDown25 = 1,
            RearTurnDown19 = 2,
            RearTurnDown12 = 4,
            RearTurnDown6 = 8,
            RearTurn0 = 16,
            RearTurnUp6 = 32,
            RearTurnUp12 = 64,
            RearTurnUp19 = 128,
            RearTurnUp25 = 256,
            FullSpinDown25 = 512,
            FullSpinDown19 = 1024,
            FullSpinDown12 = 2048,
            FullSpinDown6 = 4096,
            FullSpin0 = 8192,
            FullSpinUp6 = 16384,
            FullSpinUp12 = 32768,
            FullSpinUp19 = 65536,
            FullSpinUp25 = 131072,
            Crash = 262144
        }

        public int KartAnimationType { get; set; }

        public List<string> OrderedImageNames { get; private set; }

        public KartAnimationSeries()
        {
            KartAnimationType = 0;
            OrderedImageNames = new List<string>();
        }
    }

    public class KartImage
    {
        public string Name { get { return _name; } set { _name = value; EncodedData = null; } }
        private string _name;

        public Bitmap Image { get; private set; }
        public ImageMIO0Block EncodedData { get; private set; }

        public KartImage(Bitmap image, string name)
        {
            Image = image;
            _name = name;
        }

        public KartImage(ImageMIO0Block block)
        {
            EncodedData = block;
            Image = ((Texture)block.DecodedN64DataElement).Image;
            _name = block.ImageName;
        }

        public void SetImage(Bitmap image)
        {
            Image = image;
            EncodedData = null;
        }

        public void UpdatePalette(Palette palette)
        {
            GenerateEncodedData(palette);
        }

        public ImageMIO0Block GetEncodedData(Palette palette)
        {
            if (EncodedData == null)
            {
                GenerateEncodedData(palette);
            }

            return EncodedData;
        }

        private void GenerateEncodedData(Palette palette)
        {
            byte[] data = TextureConversion.CI8ToBinary(Image, palette);
            byte[] mio0Data = MIO0.Encode(data);
            EncodedData = new ImageMIO0Block(-1, mio0Data);
            EncodedData.ImageName = Name;
        }
    }
}
