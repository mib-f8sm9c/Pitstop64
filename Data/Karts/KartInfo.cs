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
using Ionic.Zip;
using System.IO;
using Cereal64.Common.DataElements.Encoding;

namespace Pitstop64.Data.Karts
{
    /// <summary>
    /// Stores information about a specific kart character
    /// </summary>
    public class KartInfo : RomItem
    {
        private const string NAME = "name";
        private const string ORIGINAL = "original";

        private const string ANIMATIONS = "animations";
        private const string ANIMATION = "animation";
        private const string ANIMATION_TYPE = "type";

        private const string IMAGE = "image";
        private const string IMAGE_NAME = "name";
        private const string IMAGE_DATA = "data";

        private const string PORTRAITS = "portraits";

        private const string NAME_PLATE = "namePlate";
        private const string NAME_PLATE_ALPHA = "alpha";

        private const string OFFSET = "offset";

        public const string KARTS_FILE_EXTENSION = "karts";

        public string KartName { get; set; }

        public List<KartAnimationSeries> KartAnimations { get; private set; }

        public KartImagePool KartImages { get; set; }

        public List<MK64Image> KartPortraits { get; set; }
        public MK64Image KartNamePlate { get; set; }

        //True if the kart is one of the original 8
        public bool OriginalKart { get; private set; }

        public KartInfo(string kartName, Palette palette, bool original)
        {
            KartName = kartName;
            KartImages = new KartImagePool(palette);
            KartAnimations = new List<KartAnimationSeries>();
            KartPortraits = new List<MK64Image>();
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
            KartPortraits = new List<MK64Image>();
            foreach (MK64Image block in baseKart.KartPortraits)
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
            KartPortraits = new List<MK64Image>();

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
                KartPortraits.Add(new MK64Image(portrait));
            }

            XElement namePlate = xml.Element(NAME_PLATE);
            //byte[] namePlateData = Convert.FromBase64String(namePlate.Value);
            //ushort namePlateAlpha = ushort.Parse(namePlate.Attribute(NAME_PLATE_ALPHA).Value);

            KartNamePlate = new MK64Image(namePlate.Elements().First());

            //if (RomProject.Instance.Files[0].HasElementExactlyAt(namePlateOffset))
            //{
            //    N64DataElement element = RomProject.Instance.Files[0].GetElementAt(namePlateOffset);
            //    if (element is TKMK00Block)
            //    {
            //        KartNamePlate = (TKMK00Block)element;
            //    }
            //}
        }

        public override XElement GetAsXML()
        {
            return GetAsXML(false);
        }

        public XElement GetAsXML(bool formatForExternalSave)
        {
            XElement xml = new XElement(this.GetType().ToString()); //Can derive actual type from name with N64DataElementFactory

            xml.Add(new XAttribute(NAME, KartName));
            xml.Add(new XAttribute(ORIGINAL, OriginalKart));
            xml.Add(KartImages.GetAsXml(formatForExternalSave));
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
            foreach (MK64Image image in KartPortraits)
            {
                xmlPortraits.Add(image.GetAsXML(formatForExternalSave));
            }
            xml.Add(xmlPortraits);

            //For the nameplate we need to temporarily set the index to -1
            //int namePlateOffset = KartNamePlate.FileOffset;
            //KartNamePlate.FileOffset = -1;

            XElement xmlNamePlate = new XElement(NAME_PLATE);
            xmlNamePlate.Add(KartNamePlate.GetAsXML(formatForExternalSave));
            xml.Add(xmlNamePlate);

            //KartNamePlate.FileOffset = namePlateOffset;

            //If saving for Chomp Shop use or distribution, need to cull the
            // index values for all the RomElements
            if (formatForExternalSave)
            {
                foreach(XElement xel in xml.DescendantsAndSelf())
                {
                    foreach(XAttribute at in xel.Attributes())
                    {
                        if (at.Name == OFFSET || at.Name == N64DataElement.FILEOFFSET || at.Name == MK64Image.TEXTURE_OFFSET || at.Name == MK64Image.PALETTE_OFFSET)
                            at.Value = "-1";
                    }
                }
            }

            return xml;
        }

        public override string ToString()
        {
            return KartName;
        }

        public override string GetXMLPath()
        {
            return "Karts/" + KartName;
        }


        public static void SaveKarts(string fileName, IList<KartInfo> karts)
        {
            //Here save all kart information to an external file
            Path.ChangeExtension(fileName, "karts");

            //NEED TO HANDLE IF THE FILE EXISTS, THEN UPDATE THE KARTS INSIDE THE FILE!!
            if (File.Exists(fileName))
            {
                using (ZipFile zipDest = ZipFile.Read(fileName))
                {
                    foreach (KartInfo kart in karts)
                    {
                        XElement kartXML = kart.GetAsXML(true);

                        zipDest.UpdateEntry(kart.KartName, Encoding.ASCII.GetBytes(kartXML.ToString()));
                    }

                    zipDest.Save();
                }
            }
            else
            {
                using (var fs = File.Create(fileName))
                {
                    using (ZipOutputStream s = new ZipOutputStream(fs))
                    {
                        foreach (KartInfo kart in karts)
                        {
                            XElement kartXML = kart.GetAsXML(true);

                            s.PutNextEntry(kart.KartName);

                            byte[] bytes = Encoding.ASCII.GetBytes(kartXML.ToString());
                            s.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
        }

        public static List<KartInfo> LoadFromFile(string fileName)
        {
            List<KartInfo> karts = new List<KartInfo>();

            if (!File.Exists(fileName))
                return karts;

            //Other verification goes here??

            //Here load all kart information from an external file
            using (ZipFile zip = ZipFile.Read(fileName))
            {
                foreach (ZipEntry e in zip)
                {
                    MemoryStream projectStream = new MemoryStream();
                    e.Extract(projectStream);
                    karts.Add(new KartInfo(XElement.Parse(Encoding.ASCII.GetString(projectStream.ToArray()))));
                }
            }

            return karts;
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

        private const string PALETTE = "palette";

        private const string OFFSET = "offset";

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
            if (xml.Element(PALETTE) != null)
            {
                if (xml.Element(PALETTE).Attribute(OFFSET) != null)
                {
                    int offset = int.Parse(xml.Element(PALETTE).Attribute(OFFSET).Value);
                    N64DataElement element;
                    if (offset != -1 && RomProject.Instance.Files[0].HasElementExactlyAt(offset, out element) &&
                        element is Palette)
                    {
                        ImagePalette = (Palette)element;
                    }
                    else
                        ImagePalette = new Palette(-1, Convert.FromBase64String(xml.Element(PALETTE).Value));
                }
                else
                    ImagePalette = new Palette(-1, Convert.FromBase64String(xml.Element(PALETTE).Value));
            }
            else
                ImagePalette = null;

            Images = new Dictionary<string, KartImage>();

            XElement images = xml.Element(IMAGES);
            foreach (XElement image in images.Elements())
            {
                string name = image.Attribute(IMAGE_NAME).Value.ToString();
                List<MK64Image> mkImages = new List<MK64Image>();
                XElement innerImages = image.Element(IMAGES);
                foreach (XElement innerImage in innerImages.Elements())
                {
                    mkImages.Add(new MK64Image(innerImage, ImagePalette));
                }
                KartImage kartImage = new KartImage(mkImages);

                Images.Add(name, kartImage);
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

        public void ClearPalette()
        {
            ImagePalette = null;
        }

        public XElement GetAsXml(bool formatForExternalSave)
        {
            XElement xml = new XElement(KART_IMAGE_POOL); //Can derive actual type from name with N64DataElementFactory

            if (ImagePalette != null)
            {
                XElement paletteEl = new XElement(PALETTE, Convert.ToBase64String(ImagePalette.RawData));
                paletteEl.Add(new XAttribute(OFFSET, ImagePalette.FileOffset));
                xml.Add(paletteEl);
            }

            XElement xmlImages = new XElement(IMAGES);
            foreach (string key in Images.Keys)
            {
                XElement xmlImage = new XElement(IMAGE);
                xmlImage.Add(new XAttribute(IMAGE_NAME, key));
                XElement xmlF3DEXImages = new XElement(IMAGES);
                foreach (MK64Image image in Images[key].Images)
                {
                    xmlF3DEXImages.Add(image.GetAsXML(formatForExternalSave));
                }
                xmlImage.Add(xmlF3DEXImages);

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
                        bytes.AddRange(image.Images[j].ImageReference.BasePalettes[1].RawData);
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                        bytes.AddRange(image.Images[0].ImageReference.BasePalettes[1].RawData);
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

        public List<MK64Image> Images { get; private set; }

        public KartImage(List<MK64Image> images)
        {
            Images = new List<MK64Image>();
            Images.AddRange(images);
            if(images.Count > 0 && images[0] != null)
                _name = images[0].ImageName;
        }

        public bool IsAnimated
        {
            get
            {
                return Images.Count > 1;
            }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
