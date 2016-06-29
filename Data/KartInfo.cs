using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;

namespace MK64Pitstop.Data
{
    /// <summary>
    /// Stores information about a specific kart character
    /// </summary>
    public class KartInfo
    {
        private const string NAME = "name";
        private const string ORIGINAL = "original";

        private const string ANIMATIONS = "animations";
        private const string ANIMATION = "animation";
        private const string ANIMATION_TYPE = "type";

        private const string IMAGE = "image";
        private const string IMAGE_NAME = "name";

        public string KartName { get; set; }

        public List<KartAnimationSeries> KartAnimations { get; private set; }

        public KartImagePool KartImages { get; set; } 

        //True if the kart is one of the original 8
        public bool OriginalKart { get; private set; }

        public KartInfo()
            : this("NewChar", null)
        {
        }

        public KartInfo(string kartName, Palette palette, bool original = false)
        {
            KartName = kartName;
            KartImages = new KartImagePool(palette);
            KartAnimations = new List<KartAnimationSeries>();
            OriginalKart = original;
        }

        public KartInfo(XElement xml)
        {
            KartName = xml.Attribute(NAME).Value;
            OriginalKart = bool.Parse(xml.Attribute(ORIGINAL).Value);
            XElement imagePoolElement = xml.Element(KartImagePool.KART_IMAGE_POOL);
            if (imagePoolElement != null)
                KartImages = new KartImagePool(imagePoolElement);
            else
                KartImages = new KartImagePool();
            KartAnimations = new List<KartAnimationSeries>();

            XElement animations = xml.Element(ANIMATIONS);
            foreach (XElement animation in animations.Elements())
            {
                string name = animation.Attribute(NAME).Value;
                KartAnimationSeries newAnim = new KartAnimationSeries(name);

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
            xml.Add(new XAttribute(ORIGINAL, OriginalKart));
            xml.Add(KartImages.GetAsXml());
            XElement xmlAnimations = new XElement(ANIMATIONS);
            foreach (KartAnimationSeries anim in KartAnimations)
            {
                XElement xmlAnim = new XElement(ANIMATION);
                xmlAnim.Add(new XAttribute(NAME, anim.Name));
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

        public override string ToString()
        {
            return KartName;
        }
    }

    /// <summary>
    /// Holds all the images for a kart as well as the Palette that is shared between those images.
    /// </summary>
    public class KartImagePool
    {
        public Dictionary<string, KartImage> Images { get; private set; }

        public Palette ImagePalette { get; private set; }

        public const string KART_IMAGE_POOL = "kartImagePool";

        private const string IMAGES = "images";
        private const string IMAGE = "image";
        private const string IMAGE_NAME = "name";
        private const string IMAGE_OFFSET = "offset";

        private const string PALETTE_OFFSET = "offset";

        public KartImagePool()
        {
            Images = new Dictionary<string, KartImage>();
            ImagePalette = null;
        }

        public KartImagePool(Palette palette)
        {
            Images = new Dictionary<string, KartImage>();
            ImagePalette = palette;
        }

        public KartImagePool(XElement xml)
        {
            int paletteOffset;
            if (xml.Attribute(PALETTE_OFFSET) != null)
                paletteOffset = int.Parse(xml.Attribute(PALETTE_OFFSET).Value);
            else
                paletteOffset = -2;
            N64DataElement existingPalette;
            if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
                (existingPalette = RomProject.Instance.Files[0].GetElementAt(paletteOffset)) is Palette)
            {
                ImagePalette = (Palette)existingPalette;
            }
            else
                ImagePalette = null;
            //else //This should never be necessary, I'd prefer this to break than to have this use-case
            //{
            //    byte[] paletteData = new byte[0x200]; //256 2-byte color values
            //    Array.Copy(data, paletteOffset, paletteData, 0, paletteData.Length);
            //    ImagePalette = new Palette(paletteOffset, paletteData);
            //    RomProject.Instance.Files[0].AddElement(ImagePalette);
            //}

            Images = new Dictionary<string, KartImage>();

            XElement images = xml.Element(IMAGES);
            foreach (XElement image in images.Elements())
            {
                string name = image.Attribute(IMAGE_NAME).Value;
                int offset = int.Parse(image.Attribute(IMAGE_OFFSET).Value);

                //Load the ImageMIO0Block here
                if (RomProject.Instance.Files[0].HasElementExactlyAt(offset))
                {
                    N64DataElement element = RomProject.Instance.Files[0].GetElementAt(offset);
                    if (element is ImageMIO0Block)
                    {
                        if (((ImageMIO0Block)element).DecodedN64DataElement == null)
                        {
                            Texture newTexture = new Texture(0, ((ImageMIO0Block)element).DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, ImagePalette);
                            ((ImageMIO0Block)element).DecodedN64DataElement = newTexture;
                        }
                        KartImage newImage = new KartImage((ImageMIO0Block)element);
                        Images.Add(name, newImage);
                    }
                }
            }
        }

        public bool SetPalette(Palette palette)
        {
            if (palette != null)
            {
                ImagePalette = palette;
                return true;
            }
            return false;
        }

        public XElement GetAsXml()
        {
            XElement xml = new XElement(KART_IMAGE_POOL); //Can derive actual type from name with N64DataElementFactory

            if(ImagePalette != null)
                xml.Add(new XAttribute(PALETTE_OFFSET, ImagePalette.FileOffset));

            XElement xmlImages = new XElement(IMAGES);
            foreach (string key in Images.Keys)
            {
                XElement xmlImage = new XElement(IMAGE);
                xmlImage.Add(new XAttribute(IMAGE_NAME, key));
                xmlImage.Add(new XAttribute(IMAGE_OFFSET, Images[key].EncodedData.FileOffset));
                xmlImages.Add(xmlImage);
            }
            xml.Add(xmlImages);

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

        private const int TurnAnimFlag = (int)KartAnimationTypeFlag.RearTurnUp25 | (int)KartAnimationTypeFlag.RearTurnUp19 |
            (int)KartAnimationTypeFlag.RearTurnUp12 | (int)KartAnimationTypeFlag.RearTurnUp6 |
            (int)KartAnimationTypeFlag.RearTurn0 | (int)KartAnimationTypeFlag.RearTurnDown6 |
            (int)KartAnimationTypeFlag.RearTurnDown12 | (int)KartAnimationTypeFlag.RearTurnDown19 |
            (int)KartAnimationTypeFlag.RearTurnDown25;

        private const int SpinAnimFlag = (int)KartAnimationTypeFlag.FullSpinUp25 | (int)KartAnimationTypeFlag.FullSpinUp19 |
            (int)KartAnimationTypeFlag.FullSpinUp12 | (int)KartAnimationTypeFlag.FullSpinUp6 |
            (int)KartAnimationTypeFlag.FullSpin0 | (int)KartAnimationTypeFlag.FullSpinDown6 |
            (int)KartAnimationTypeFlag.FullSpinDown12 | (int)KartAnimationTypeFlag.FullSpinDown19 |
            (int)KartAnimationTypeFlag.FullSpinDown25;

        private const int CrashAnimFlag = (int)KartAnimationTypeFlag.Crash;

        public bool IsTurnAnim { get { return ((int)KartAnimationType & TurnAnimFlag) != 0; } }
        public bool IsSpinAnim { get { return ((int)KartAnimationType & SpinAnimFlag) != 0; } }
        public bool IsCrashAnim { get { return ((int)KartAnimationType & CrashAnimFlag) != 0; } }

        private const int TURN_FRAME_COUNT = 35;
        private const int SPIN_FRAME_COUNT = 20;
        private const int CRASH_FRAME_COUNT = 32;

        public string Name { get; set; }

        public int KartAnimationType { get; set; }

        public List<string> OrderedImageNames { get; private set; }

        public KartAnimationSeries(string name)
        {
            Name = name;
            KartAnimationType = 0;
            OrderedImageNames = new List<string>();
        }

        //For when the # of images doesn't match up to the
        // full frame count, use this to interpolate values
        public int GetImageIndexForTurnFrame(int frameIndex)
        {
            if (OrderedImageNames.Count == 0)
                return -1;

            double framesPerImage = (double)TURN_FRAME_COUNT / (double)OrderedImageNames.Count;
            return (int)Math.Floor(frameIndex / framesPerImage);
        }

        public int GetTurnFrameForImageIndex(int imageIndex)
        {
            if (OrderedImageNames.Count == 0)
                return -1;

            double framesPerImage = (double)TURN_FRAME_COUNT / (double)OrderedImageNames.Count;
            return (int)Math.Floor(imageIndex * framesPerImage);
        }

        public int GetImageIndexForSpinFrame(int frameIndex)
        {
            if (OrderedImageNames.Count == 0)
                return -1;

            double framesPerImage = (double)SPIN_FRAME_COUNT / (double)OrderedImageNames.Count;
            return (int)Math.Floor(frameIndex / framesPerImage);
        }

        public int GetSpinFrameForImageIndex(int imageIndex)
        {
            if (OrderedImageNames.Count == 0)
                return -1;

            double framesPerImage = (double)SPIN_FRAME_COUNT / (double)OrderedImageNames.Count;
            return (int)Math.Floor(imageIndex * framesPerImage);
        }

        public int GetImageIndexForCrashFrame(int frameIndex)
        {
            if (OrderedImageNames.Count == 0)
                return -1;

            double framesPerImage = (double)CRASH_FRAME_COUNT / (double)OrderedImageNames.Count;
            return (int)Math.Floor(frameIndex / framesPerImage);
        }

        public int GetCrashFrameForImageIndex(int imageIndex)
        {
            if (OrderedImageNames.Count == 0)
                return -1;

            double framesPerImage = (double)CRASH_FRAME_COUNT / (double)OrderedImageNames.Count;
            return (int)Math.Floor(imageIndex * framesPerImage);
        }

        public override string ToString()
        {
            return Name;
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
            if(block.DecodedN64DataElement != null)
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

        public override string ToString()
        {
            return _name;
        }
    }
}
