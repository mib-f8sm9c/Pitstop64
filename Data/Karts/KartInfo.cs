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

        private const string MINI_KART_ICON = "miniKartIcon";
        private const string MINI_PORTRAIT = "miniPortrait";

        private const string OFFSET = "offset";

        public const string KARTS_FILE_EXTENSION = "karts";

        public string KartName { get; set; }

        public List<KartAnimationSeries> KartAnimations { get; private set; }

        public KartImagePool KartImages { get; set; }

        public List<MK64Image> KartPortraits { get; set; }
        public MK64Image KartNamePlate { get; set; }

        //if done improperly, these can FUCK UP the thing they're in
        public MK64Image KartMiniIcon { get; set; }
        public MK64Image KartMiniPortrait { get; set; }

        public KartStats KartStats { get; set; }

        //True if the kart is one of the original 8
        public bool OriginalKart { get; private set; }

        public KartInfo(string kartName, Palette palette, bool original)
        {
            KartName = kartName;
            KartImages = new KartImagePool(palette);
            KartAnimations = new List<KartAnimationSeries>();
            KartPortraits = new List<MK64Image>();
            OriginalKart = original;
            KartStats = new KartStats();
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
            KartMiniIcon = baseKart.KartMiniIcon;
            KartMiniPortrait = baseKart.KartMiniPortrait;
            KartStats = new KartStats(baseKart.KartStats);
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
            KartNamePlate = new MK64Image(namePlate.Elements().First());

            XElement miniI = xml.Element(MINI_KART_ICON);
            KartMiniIcon = new MK64Image(miniI.Elements().First());
            XElement miniP = xml.Element(MINI_PORTRAIT);
            KartMiniPortrait = new MK64Image(miniP.Elements().First());
            
            KartStats = new KartStats(xml.Element(KartStats.KART_STATS));

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

            XElement xmlMiniI = new XElement(MINI_KART_ICON);
            xmlMiniI.Add(KartMiniIcon.GetAsXML(formatForExternalSave));
            xml.Add(xmlMiniI);

            XElement xmlMiniP = new XElement(MINI_PORTRAIT);
            xmlMiniP.Add(KartMiniPortrait.GetAsXML(formatForExternalSave));
            xml.Add(xmlMiniP);

            xml.Add(KartStats.GetAsXml());

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

    public class KartStats
    {
        public const string KART_STATS = "KartStats";

        private const string UNKNOWN1 = "Unknown1", UNKNOWN2 = "Unknown2", UNKNOWN3 = "Unknown3", UNKNOWN4 = "Unknown4",
            UNKNOWN5 = "Unknown5", UNKNOWN6 = "Unknown6", UNKNOWN7 = "Unknown7", UNKNOWN8 = "Unknown8", UNKNOWN9 = "Unknown9",
            UNKNOWN10 = "Unknown10", UNKNOWN11 = "Unknown11", UNKNOWN12 = "Unknown12", UNKNOWN13 = "Unknown13", UNKNOWN14 = "Unknown14",
            UNKNOWN15 = "Unknown15", UNKNOWN16 = "Unknown16", UNKNOWN17 = "Unknown17", UNKNOWN18 = "Unknown18", UNKNOWN19 = "Unknown19",
            UNKNOWN20 = "Unknown20", UNKNOWN21 = "Unknown21", UNKNOWN22 = "Unknown22", UNKNOWN23 = "Unknown23", UNKNOWN24 = "Unknown24",
            UNKNOWN25 = "Unknown25", UNKNOWN26 = "Unknown26";
        
        private const string SPEED_50_CC = "Speed50cc", SPEED_100_CC = "Speed100cc", SPEED_150_CC = "Speed150cc",
            SPEED_EXTRA = "SpeedExtra", SPEED_BATTLE = "SpeedBattle";

        private const string FRICTION = "Friction", GRAVITY = "Gravity", TOP_SPEED = "TopSpeed", BOUNDING_BOX = "BoundingBox",
            HANDLING = "Handling", TURN_SPEED_REDUCTION_COEFFICIENT = "TurnSpeedReductionCoefficient",
             TURN_SPEED_REDUCTION_COEFFICIENT_2 = "TurnSpeedReductionCoefficient2", HOP_HEIGHT = "HopHeight",
             HOP_FALL_SPEED = "HopFallSpeed", WEIGHT = "Weight", SCALE = "Scale";

        private const string ACCEL_BLOCK = "AccelBlock", UNKNOWN_BLOCK_1 = "UnknownBlock1", UNKNOWN_BLOCK_2 = "UnknownBlock2",
             UNKNOWN_BLOCK_3 = "UnknownBlock3", UNKNOWN_BLOCK_4 = "UnknownBlock4", UNKNOWN_BLOCK_5 = "UnknownBlock5",
              UNKNOWN_BLOCK_6 = "UnknownBlock6", UNKNOWN_BLOCK_7 = "UnknownBlock7";

        //Defaults to Mario stats
        public float Unknown1, Unknown2, Unknown3, Unknown4, Unknown5,
            Unknown6, Unknown7, Unknown8, Unknown9, Unknown10,
            Unknown11, Unknown12, Unknown13, Unknown14, Unknown15,
            Unknown16, Unknown17, Unknown18, Unknown19, Unknown20,
            Unknown21, Unknown22, Unknown23, Unknown24, Unknown25, Unknown26;

        public float Speed50CC, Speed100CC, Speed150CC, SpeedExtra, SpeedBattle;

        public float Friction, Gravity, TopSpeed, BoundingBox;

        public float Handling, TurnSpeedReductionCoefficient, TurnSpeedReductionCoefficient2,
            HopHeight, HopFallSpeed, Weight;

        public float[] Block1Unknowns, Block2Unknowns, Block3Unknowns, Block4Unknowns,
            Block5Unknowns, Block6Unknowns, Block7Unknowns;

        public float[] AccelBlock;

        public float Scale;

        public KartStats()
        {
            //Default to mario
            Unknown1 = -10.0f;
            Unknown2 = -15.0f;
            Unknown3 = -20.0f;
            Unknown4 = -15.0f;
            Unknown5 = -30.0f;

            Unknown6 = 28.0f;
            Unknown7 = 28.0f;
            Unknown8 = 35.0f;
            Unknown9 = 28.0f;
            Unknown10 = 48.0f;

            Unknown11 = 3364.0f;
            Unknown12 = 3844.0f;
            Unknown13 = 4096.0f;
            Unknown14 = 3844.0f;
            Unknown15 = 2401.0f;

            Speed50CC = 290.0f;
            Speed100CC = 310.0f;
            Speed150CC = 320.0f;
            SpeedExtra = 310.0f;
            SpeedBattle = 245.0f;

            Friction = 5800.0f;
            Gravity = 2600.0f;
            Unknown16 = 0.12f;
            TopSpeed = 9.0f;
            BoundingBox = 5.5f;

            Block1Unknowns = new float[15] { 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f, 0.38f, 0.38f, 0.0f, 0.38f,
                0.1f, 0.0f, 0.38f, 0.0f};
            Block2Unknowns = new float[15] { 0.0f, 0.0f, 0.3f,
                0.3f, 0.0f, 0.3f, 0.0f, 0.58f, 0.58f, 0.0f, 0.58f,
                0.28f, 0.0f, 0.58f, 0.0f};

            Block3Unknowns = new float[15] { 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f, 0.3f, 0.3f, 0.0f, 0.0f,
                0.3f, 0.0f, 0.3f, 0.3f};
            Block4Unknowns = new float[15] { 0.0f, 0.0f, 0.0f,
                0.03f, 0.0f, 0.0f, 0.0f, 0.09f, 0.09f, 0.0f, 0.0f,
                0.09f, 0.0f, 0.09f, 0.09f};

            AccelBlock = new float[10] { 2.0f, 2.0f, 2.0f,
                1.6f, 1.4f, 1.2f, 1.0f, 0.8f, 0.6f, 0.4f};

            Block5Unknowns = new float[15] { 0.0f, 0.0f, 0.2f,
                0.2f, 0.0f, 0.4f, 0.1f, 0.2f, 0.2f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f};

            Block6Unknowns = new float[15] { 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f};

            Block7Unknowns = new float[15] { 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f};

            Handling = 1.25f;
            Unknown17 = 0.0f;
            TurnSpeedReductionCoefficient = 0.0f;
            TurnSpeedReductionCoefficient2 = 0.0f;
            Unknown18 = 2.0f;
            HopHeight = 0.93f;
            HopFallSpeed = 0.03f;

            Unknown19 = 2.2f;
            Unknown20 = 0.002f;
            Unknown21 = 2.0f;
            Unknown22 = 0.002f;
            Unknown23 = 1.2f;
            Unknown24 = 0.01f;
            Unknown25 = 3.5f;
            Unknown26 = 0.002f;

            Scale = 0.75f;
            Weight = 1.2f;

        }

        public KartStats(KartStats kartStats)
        {
            Unknown1 = kartStats.Unknown1;
            Unknown2 = kartStats.Unknown2;
            Unknown3 = kartStats.Unknown3;
            Unknown4 = kartStats.Unknown4;
            Unknown5 = kartStats.Unknown5;

            Unknown6 = kartStats.Unknown6;
            Unknown7 = kartStats.Unknown7;
            Unknown8 = kartStats.Unknown8;
            Unknown9 = kartStats.Unknown9;
            Unknown10 = kartStats.Unknown10;

            Unknown11 = kartStats.Unknown11;
            Unknown12 = kartStats.Unknown12;
            Unknown13 = kartStats.Unknown13;
            Unknown14 = kartStats.Unknown14;
            Unknown15 = kartStats.Unknown15;

            Speed50CC = kartStats.Speed50CC;
            Speed100CC = kartStats.Speed100CC;
            Speed150CC = kartStats.Speed150CC;
            SpeedExtra = kartStats.SpeedExtra;
            SpeedBattle = kartStats.SpeedBattle;

            Friction = kartStats.Friction;
            Gravity = kartStats.Gravity;
            Unknown16 = kartStats.Unknown16;
            TopSpeed = kartStats.TopSpeed;
            BoundingBox = kartStats.BoundingBox;

            Block1Unknowns = new float[15];
            Array.Copy(kartStats.Block1Unknowns, Block1Unknowns, Block1Unknowns.Length);
            Block2Unknowns = new float[15];
            Array.Copy(kartStats.Block2Unknowns, Block2Unknowns, Block2Unknowns.Length);

            Block3Unknowns = new float[15];
            Array.Copy(kartStats.Block3Unknowns, Block3Unknowns, Block3Unknowns.Length);
            Block4Unknowns = new float[15];
            Array.Copy(kartStats.Block4Unknowns, Block4Unknowns, Block4Unknowns.Length);

            AccelBlock = new float[10];
            Array.Copy(kartStats.AccelBlock, AccelBlock, AccelBlock.Length);

            Block5Unknowns = new float[15];
            Array.Copy(kartStats.Block5Unknowns, Block5Unknowns, Block5Unknowns.Length);

            Block6Unknowns = new float[15];
            Array.Copy(kartStats.Block6Unknowns, Block6Unknowns, Block6Unknowns.Length);

            Block7Unknowns = new float[15];
            Array.Copy(kartStats.Block7Unknowns, Block7Unknowns, Block7Unknowns.Length);
            
            Handling = kartStats.Handling;
            Unknown17 = kartStats.Unknown17;
            TurnSpeedReductionCoefficient = kartStats.TurnSpeedReductionCoefficient;
            TurnSpeedReductionCoefficient2 = kartStats.TurnSpeedReductionCoefficient2;
            Unknown18 = kartStats.Unknown18;
            HopHeight = kartStats.HopHeight;
            HopFallSpeed = kartStats.HopFallSpeed;

            Unknown19 = kartStats.Unknown19;
            Unknown20 = kartStats.Unknown20;
            Unknown21 = kartStats.Unknown21;
            Unknown22 = kartStats.Unknown22;
            Unknown23 = kartStats.Unknown23;
            Unknown24 = kartStats.Unknown24;
            Unknown25 = kartStats.Unknown25;
            Unknown26 = kartStats.Unknown26;

            Scale = kartStats.Scale;
            Weight = kartStats.Weight;

        }

        public KartStats(XElement xml)
        {
            Unknown1 = float.Parse(xml.Attribute(UNKNOWN1).Value.ToString());
            Unknown2 = float.Parse(xml.Attribute(UNKNOWN2).Value.ToString());
            Unknown3 = float.Parse(xml.Attribute(UNKNOWN3).Value.ToString());
            Unknown4 = float.Parse(xml.Attribute(UNKNOWN4).Value.ToString());
            Unknown5 = float.Parse(xml.Attribute(UNKNOWN5).Value.ToString());
            Unknown6 = float.Parse(xml.Attribute(UNKNOWN6).Value.ToString());
            Unknown7 = float.Parse(xml.Attribute(UNKNOWN7).Value.ToString());
            Unknown8 = float.Parse(xml.Attribute(UNKNOWN8).Value.ToString());
            Unknown9 = float.Parse(xml.Attribute(UNKNOWN9).Value.ToString());
            Unknown10 = float.Parse(xml.Attribute(UNKNOWN10).Value.ToString());
            Unknown11 = float.Parse(xml.Attribute(UNKNOWN11).Value.ToString());
            Unknown12 = float.Parse(xml.Attribute(UNKNOWN12).Value.ToString());
            Unknown13 = float.Parse(xml.Attribute(UNKNOWN13).Value.ToString());
            Unknown14 = float.Parse(xml.Attribute(UNKNOWN14).Value.ToString());
            Unknown15 = float.Parse(xml.Attribute(UNKNOWN15).Value.ToString());
            Unknown16 = float.Parse(xml.Attribute(UNKNOWN16).Value.ToString());
            Unknown17 = float.Parse(xml.Attribute(UNKNOWN17).Value.ToString());
            Unknown18 = float.Parse(xml.Attribute(UNKNOWN18).Value.ToString());
            Unknown19 = float.Parse(xml.Attribute(UNKNOWN19).Value.ToString());
            Unknown20 = float.Parse(xml.Attribute(UNKNOWN20).Value.ToString());
            Unknown21 = float.Parse(xml.Attribute(UNKNOWN21).Value.ToString());
            Unknown22 = float.Parse(xml.Attribute(UNKNOWN22).Value.ToString());
            Unknown23 = float.Parse(xml.Attribute(UNKNOWN23).Value.ToString());
            Unknown24 = float.Parse(xml.Attribute(UNKNOWN24).Value.ToString());
            Unknown25 = float.Parse(xml.Attribute(UNKNOWN25).Value.ToString());
            Unknown26 = float.Parse(xml.Attribute(UNKNOWN26).Value.ToString());

            Speed50CC = float.Parse(xml.Attribute(SPEED_50_CC).Value.ToString());
            Speed100CC = float.Parse(xml.Attribute(SPEED_100_CC).Value.ToString());
            Speed150CC = float.Parse(xml.Attribute(SPEED_150_CC).Value.ToString());
            SpeedExtra = float.Parse(xml.Attribute(SPEED_EXTRA).Value.ToString());
            SpeedBattle = float.Parse(xml.Attribute(SPEED_BATTLE).Value.ToString());

            Friction = float.Parse(xml.Attribute(FRICTION).Value.ToString());
            Gravity = float.Parse(xml.Attribute(GRAVITY).Value.ToString());
            TopSpeed = float.Parse(xml.Attribute(TOP_SPEED).Value.ToString());
            BoundingBox = float.Parse(xml.Attribute(BOUNDING_BOX).Value.ToString());
            Handling = float.Parse(xml.Attribute(HANDLING).Value.ToString());
            TurnSpeedReductionCoefficient = float.Parse(xml.Attribute(TURN_SPEED_REDUCTION_COEFFICIENT).Value.ToString());
            TurnSpeedReductionCoefficient2 = float.Parse(xml.Attribute(TURN_SPEED_REDUCTION_COEFFICIENT_2).Value.ToString());
            HopHeight = float.Parse(xml.Attribute(HOP_HEIGHT).Value.ToString());
            HopFallSpeed = float.Parse(xml.Attribute(HOP_FALL_SPEED).Value.ToString());
            Weight = float.Parse(xml.Attribute(WEIGHT).Value.ToString());
            Scale = float.Parse(xml.Attribute(SCALE).Value.ToString());

            string[] parts = xml.Attribute(ACCEL_BLOCK).Value.ToString().Split(',');
            AccelBlock = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                AccelBlock[i] = float.Parse(parts[i]);

            parts = xml.Attribute(UNKNOWN_BLOCK_1).Value.ToString().Split(',');
            Block1Unknowns = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                Block1Unknowns[i] = float.Parse(parts[i]);
            parts = xml.Attribute(UNKNOWN_BLOCK_2).Value.ToString().Split(',');
            Block2Unknowns = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                Block2Unknowns[i] = float.Parse(parts[i]);
            parts = xml.Attribute(UNKNOWN_BLOCK_3).Value.ToString().Split(',');
            Block3Unknowns = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                Block3Unknowns[i] = float.Parse(parts[i]);
            parts = xml.Attribute(UNKNOWN_BLOCK_4).Value.ToString().Split(',');
            Block4Unknowns = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                Block4Unknowns[i] = float.Parse(parts[i]);
            parts = xml.Attribute(UNKNOWN_BLOCK_5).Value.ToString().Split(',');
            Block5Unknowns = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                Block5Unknowns[i] = float.Parse(parts[i]);
            parts = xml.Attribute(UNKNOWN_BLOCK_6).Value.ToString().Split(',');
            Block6Unknowns = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                Block6Unknowns[i] = float.Parse(parts[i]);
            parts = xml.Attribute(UNKNOWN_BLOCK_7).Value.ToString().Split(',');
            Block7Unknowns = new float[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                Block7Unknowns[i] = float.Parse(parts[i]);
            
        }

        public XElement GetAsXml()
        {
            XElement xml = new XElement(KART_STATS);
            
            xml.Add(new XAttribute(UNKNOWN1, Unknown1.ToString()));
            xml.Add(new XAttribute(UNKNOWN2, Unknown2.ToString()));
            xml.Add(new XAttribute(UNKNOWN3, Unknown3.ToString()));
            xml.Add(new XAttribute(UNKNOWN4, Unknown4.ToString()));
            xml.Add(new XAttribute(UNKNOWN5, Unknown5.ToString()));
            xml.Add(new XAttribute(UNKNOWN6, Unknown6.ToString()));
            xml.Add(new XAttribute(UNKNOWN7, Unknown7.ToString()));
            xml.Add(new XAttribute(UNKNOWN8, Unknown8.ToString()));
            xml.Add(new XAttribute(UNKNOWN9, Unknown9.ToString()));
            xml.Add(new XAttribute(UNKNOWN10, Unknown10.ToString()));
            xml.Add(new XAttribute(UNKNOWN11, Unknown11.ToString()));
            xml.Add(new XAttribute(UNKNOWN12, Unknown12.ToString()));
            xml.Add(new XAttribute(UNKNOWN13, Unknown13.ToString()));
            xml.Add(new XAttribute(UNKNOWN14, Unknown14.ToString()));
            xml.Add(new XAttribute(UNKNOWN15, Unknown15.ToString()));
            xml.Add(new XAttribute(UNKNOWN16, Unknown16.ToString()));
            xml.Add(new XAttribute(UNKNOWN17, Unknown17.ToString()));
            xml.Add(new XAttribute(UNKNOWN18, Unknown18.ToString()));
            xml.Add(new XAttribute(UNKNOWN19, Unknown19.ToString()));
            xml.Add(new XAttribute(UNKNOWN20, Unknown20.ToString()));
            xml.Add(new XAttribute(UNKNOWN21, Unknown21.ToString()));
            xml.Add(new XAttribute(UNKNOWN22, Unknown22.ToString()));
            xml.Add(new XAttribute(UNKNOWN23, Unknown23.ToString()));
            xml.Add(new XAttribute(UNKNOWN24, Unknown24.ToString()));
            xml.Add(new XAttribute(UNKNOWN25, Unknown25.ToString()));
            xml.Add(new XAttribute(UNKNOWN26, Unknown26.ToString()));
            
            xml.Add(new XAttribute(SPEED_50_CC, Speed50CC.ToString()));
            xml.Add(new XAttribute(SPEED_100_CC, Speed100CC.ToString()));
            xml.Add(new XAttribute(SPEED_150_CC, Speed150CC.ToString()));
            xml.Add(new XAttribute(SPEED_EXTRA, SpeedExtra.ToString()));
            xml.Add(new XAttribute(SPEED_BATTLE, SpeedBattle.ToString()));
            
            xml.Add(new XAttribute(FRICTION, Friction.ToString()));
            xml.Add(new XAttribute(GRAVITY, Gravity.ToString()));
            xml.Add(new XAttribute(TOP_SPEED, TopSpeed.ToString()));
            xml.Add(new XAttribute(BOUNDING_BOX, BoundingBox.ToString()));
            xml.Add(new XAttribute(HANDLING, Handling.ToString()));
            xml.Add(new XAttribute(TURN_SPEED_REDUCTION_COEFFICIENT, TurnSpeedReductionCoefficient.ToString()));
            xml.Add(new XAttribute(TURN_SPEED_REDUCTION_COEFFICIENT_2, TurnSpeedReductionCoefficient2.ToString()));
            xml.Add(new XAttribute(HOP_HEIGHT, HopHeight.ToString()));
            xml.Add(new XAttribute(HOP_FALL_SPEED, HopFallSpeed.ToString()));
            xml.Add(new XAttribute(WEIGHT, Weight.ToString()));
            xml.Add(new XAttribute(SCALE, Scale.ToString()));
            
            xml.Add(new XAttribute(ACCEL_BLOCK, string.Join(",", AccelBlock)));
            xml.Add(new XAttribute(UNKNOWN_BLOCK_1, string.Join(",", Block1Unknowns)));
            xml.Add(new XAttribute(UNKNOWN_BLOCK_2, string.Join(",", Block2Unknowns)));
            xml.Add(new XAttribute(UNKNOWN_BLOCK_3, string.Join(",", Block3Unknowns)));
            xml.Add(new XAttribute(UNKNOWN_BLOCK_4, string.Join(",", Block4Unknowns)));
            xml.Add(new XAttribute(UNKNOWN_BLOCK_5, string.Join(",", Block5Unknowns)));
            xml.Add(new XAttribute(UNKNOWN_BLOCK_6, string.Join(",", Block6Unknowns)));
            xml.Add(new XAttribute(UNKNOWN_BLOCK_7, string.Join(",", Block7Unknowns)));
            
            return xml;
        }
    }

}
