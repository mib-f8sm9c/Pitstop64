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

namespace MK64Pitstop.Data.Karts
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

        private const string PORTRAITS = "portraits";
        private const string PORTRAIT = "portrait";
        private const string IMAGE_OFFSET = "imageOffset";

        private const string NAME_PLATE = "namePlate";

        public string KartName { get; set; }

        public List<KartAnimationSeries> KartAnimations { get; private set; }

        public KartImagePool KartImages { get; set; }

        public List<ImageMIO0Block> KartPortraits { get; set; }
        public TKMK00Block KartNamePlate { get; set; }

        //True if the kart is one of the original 8
        public bool OriginalKart { get; private set; }

        public KartInfo(string kartName, Palette palette, bool original)
        {
            KartName = kartName;
            KartImages = new KartImagePool(palette);
            KartAnimations = new List<KartAnimationSeries>();
            KartPortraits = new List<ImageMIO0Block>();
            OriginalKart = original;
        }

        //Direct copy
        public KartInfo(KartInfo kart)
            : this(kart.KartName, kart)
        {
            OriginalKart = kart.OriginalKart;
        }

        //Duplicate copy
        public KartInfo(string kartName, KartInfo baseKart)
        {
            KartName = kartName;
            KartImages = new KartImagePool(baseKart.KartImages.ImagePalette);
            foreach (string key in baseKart.KartImages.Images.Keys)
                KartImages.Images.Add(key, baseKart.KartImages.Images[key]);
            KartAnimations = new List<KartAnimationSeries>();
            foreach (KartAnimationSeries anim in baseKart.KartAnimations)
            {
                KartAnimationSeries newAnim = new KartAnimationSeries(anim.Name);
                newAnim.KartAnimationType = anim.KartAnimationType;
                newAnim.OrderedImageNames.AddRange(anim.OrderedImageNames);
                KartAnimations.Add(newAnim);
            }
            KartPortraits = new List<ImageMIO0Block>();
            foreach (ImageMIO0Block block in baseKart.KartPortraits)
                KartPortraits.Add(block);
            KartNamePlate = baseKart.KartNamePlate;
            OriginalKart = false;
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
            KartPortraits = new List<ImageMIO0Block>();

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

            XElement portraits = xml.Element(PORTRAITS);
            foreach (XElement portrait in portraits.Elements())
            {
                int offset = int.Parse(portrait.Attribute(IMAGE_OFFSET).Value);

                //Load the ImageMIO0Block here
                if (RomProject.Instance.Files[0].HasElementExactlyAt(offset))
                {
                    N64DataElement element = RomProject.Instance.Files[0].GetElementAt(offset);
                    if (element is ImageMIO0Block)
                    {
                        if (((ImageMIO0Block)element).DecodedN64DataElement == null)
                        {
                            Texture newTexture = new Texture(0, ((ImageMIO0Block)element).DecodedData, Texture.ImageFormat.RGBA, Texture.PixelInfo.Size_16b, 64, 64);
                            ((ImageMIO0Block)element).DecodedN64DataElement = newTexture;
                        }
                        KartPortraits.Add((ImageMIO0Block)element);
                    }
                }
            }

            XElement namePlate = xml.Element(NAME_PLATE);
            int namePlateOffset = int.Parse(namePlate.Value);
            if (RomProject.Instance.Files[0].HasElementExactlyAt(namePlateOffset))
            {
                N64DataElement element = RomProject.Instance.Files[0].GetElementAt(namePlateOffset);
                if (element is TKMK00Block)
                {
                    KartNamePlate = (TKMK00Block)element;
                }
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

            XElement xmlPortraits = new XElement(PORTRAITS);
            foreach (ImageMIO0Block block in KartPortraits)
            {
                if (block.DecodedN64DataElement != null)
                {
                    XElement xmlPortrait = new XElement(PORTRAIT);
                    xmlPortrait.Add(new XAttribute(IMAGE_OFFSET, block.FileOffset));
                    xmlPortraits.Add(xmlPortrait);
                }
            }
            xml.Add(xmlPortraits);

            XElement xmlNamePlate = new XElement(NAME_PLATE);
            xmlNamePlate.Value = KartNamePlate.FileOffset.ToString();
            xml.Add(xmlNamePlate);

            return xml;
        }

        public void SaveToFile(string fileName)
        {
            //Here save all kart information to an external file
        }

        public static KartInfo LoadFromFile(string fileName)
        {
            //Here load all kart information from an external file
            return null;
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
        private const string IMAGE_DATA = "data";
        private const string IMAGE_ANIM_PALETTES = "animPalettes";

        private const string PALETTE = "palette";

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
            if(xml.Element(PALETTE) != null)
                ImagePalette = new Palette(-1, Convert.FromBase64String(xml.Element(PALETTE).Value));
            else
                ImagePalette = null;

            Images = new Dictionary<string, KartImage>();

            XElement images = xml.Element(IMAGES);
            foreach (XElement image in images.Elements())
            {
                string name = image.Attribute(IMAGE_NAME).Value;
                byte[] data = Convert.FromBase64String(image.Element(IMAGE_DATA).Value);

                List<Palette> animPalettes = new List<Palette>();
                XElement animsElement = image.Element(IMAGE_ANIM_PALETTES);
                if (animsElement != null)
                {
                    foreach (XElement paletteXml in animsElement.Elements())
                    {
                        animPalettes.Add(new Palette(-1, Convert.FromBase64String(paletteXml.Value)));
                    }
                }

                //Load the ImageMIO0Block here
                ImageMIO0Block block = new ImageMIO0Block(-1, data);

                Palette combinedPalette = ImagePalette;
                if (animPalettes.Count > 0)
                    combinedPalette = combinedPalette.Combine(animPalettes[0]);
                Texture newTexture = new Texture(0, ((ImageMIO0Block)element).DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, combinedPalette);
                ((ImageMIO0Block)element).DecodedN64DataElement = newTexture;

                KartImage newImage = new KartImage(block, animPalettes);
                Images.Add(name, newImage);


                if (RomProject.Instance.Files[0].HasElementExactlyAt(offset))
                {
                    N64DataElement element = RomProject.Instance.Files[0].GetElementAt(offset);
                    if (element is ImageMIO0Block)
                    {
                        if (((ImageMIO0Block)element).DecodedN64DataElement == null)
                        {
                            Palette combinedPalette = ImagePalette;
                            if (animPalettes.Count > 0)
                                combinedPalette = combinedPalette.Combine(animPalettes[0]);
                            Texture newTexture = new Texture(0, ((ImageMIO0Block)element).DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, combinedPalette);
                            ((ImageMIO0Block)element).DecodedN64DataElement = newTexture;
                        }
                        KartImage newImage = new KartImage((ImageMIO0Block)element, animPalettes);
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
                xml.Add(new XElement(PALETTE, Convert.ToBase64String(ImagePalette.RawData)));

            XElement xmlImages = new XElement(IMAGES);
            foreach (string key in Images.Keys)
            {
                XElement xmlImage = new XElement(IMAGE);
                xmlImage.Add(new XAttribute(IMAGE_NAME, key));
                xmlImage.Add(new XAttribute(IMAGE_OFFSET, Images[key].EncodedData.FileOffset));
                XElement animElement = new XElement(IMAGE_ANIM_PALETTES);
                foreach (Palette palette in Images[key].AnimationPalettes)
                {
                    if(palette != null)
                        animElement.Add(new XElement(PALETTE, Convert.ToBase64String(palette.RawData)));
                }
                xmlImage.Add(animElement);
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

        private const int TURN_FRAME_COUNT = 21;
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

        public byte[] GenerateKartAnimationPaletteData(KartImagePool images, bool isTurnAnim)
        {
            //Generate the palettes
            int paletteCount;
            if(isTurnAnim)
                paletteCount = 21;
            else
                paletteCount = 20;

            List<byte> bytes = new List<byte>();

            for(int i = 0; i < paletteCount; i++)
            {
                int imageIndex;
                if (IsTurnAnim)
                    imageIndex = GetImageIndexForTurnFrame(i);
                else //if (IsSpinAnim)
                    imageIndex = GetImageIndexForSpinFrame(i);

                KartImage image = images.Images[OrderedImageNames[imageIndex]];

                if (image.IsAnimated)
                {
                    for (int j = 0; j < 4; j++)
                        bytes.AddRange(image.AnimationPalettes[j].RawData);
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                        bytes.AddRange(image.AnimationPalettes[0].RawData);
                }
            }

            return bytes.ToArray();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    //Don't allow KartImages to be changed after creation
    public class KartImage 
    {
        public string Name { get { return _name; } }
        private string _name;

        public Bitmap Image { get; private set; }
        public ImageMIO0Block EncodedData { get; private set; }

        public List<Palette> AnimationPalettes { get; private set; } // each 64 colors

        public KartImage(ImageMIO0Block block, Palette extraPalette)
            : this(block)
        {
            if(extraPalette != null)
                AnimationPalettes.Add(extraPalette);
        }

        public KartImage(ImageMIO0Block block, List<Palette> animPalettes)
            : this(block)
        {
            AnimationPalettes.AddRange(animPalettes);
        }

        private KartImage(ImageMIO0Block block)
        {
            EncodedData = block;
            if(block.DecodedN64DataElement != null)
                Image = ((Texture)block.DecodedN64DataElement).Image;
            _name = block.ImageName;
            AnimationPalettes = new List<Palette>();
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

        public bool IsAnimated
        {
            get
            {
                return AnimationPalettes.Count > 1;
            }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
