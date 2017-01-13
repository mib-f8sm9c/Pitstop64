using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Microcodes.F3DEX.DataElements;
using System.Drawing;
using System.Xml.Linq;
using Cereal64.Common;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;
using Cereal64.Common.DataElements.Encoding;

namespace Pitstop64.Data
{
    public class MK64Image : IXMLSerializable
    {
        private const string MK64IMAGE = "MK64Image";

        public const string TEXTURE_OFFSET = "TextureOffset";
        private const string TEXTURE_BLOCK_OFFSET = "TextureBlockOffset";
        private const string TEXTURE_ENCODING = "TextureEncoding";

        private const string PALETTES = "Palettes";
        private const string PALETTE = "Palette";
        public const string PALETTE_OFFSET = "PaletteOffset";
        private const string PALETTE_BLOCK_OFFSET = "PaletteBlockOffset";
        private const string PALETTE_ENCODING = "PaletteEncoding";
        private const string PALETTE_COLOR_COUNT = "PaletteColorCount";
        private const string PALETTE_COLOR_OFFSET = "PaletteColorOffset";

        private const string TKMK_LENGTH = "TKMKLength";
        private const string TKMK_ALPHA_COLOR = "TKMKAlphaColor";

        private const string FORMAT = "Format";
        private const string PIXEL_SIZE = "PixelSize";
        private const string WIDTH = "Width";
        private const string HEIGHT = "Height";

        private const string IS_ORIGINAL_IMAGE = "IsOriginalImage";
        private const string IMAGE_NAME = "ImageName";
        //private const string IS_VALID_IMAGE;

        /// <summary>
        /// Offset within the ROM for the texture data
        /// </summary>
        public int TextureOffset { get; set; }

        /// <summary>
        /// Offset within the texture block for the texture data (for encoded values)
        /// </summary>
        public int TextureBlockOffset { get; set; }

        /// <summary>
        /// Byte encoding of the texture data
        /// </summary>
        public MK64ImageEncoding TextureEncoding { get; private set; }

        /// <summary>
        /// The TKMK block for the image. If the encoding is not TKMK, this will be null.
        /// </summary>
        public TKMK00Block TKMKReference { get; set; }

        /// <summary>
        /// The length of the TKMK block. Used to simplify the TKMK reading process.
        /// </summary>
        public int TKMKLength { get; set; }

        /// <summary>
        /// The alpha color of the TKMK image.
        /// </summary>
        public ushort TKMKAlphaColor { get; set; }

        /// <summary>
        /// Offset within the ROM for each palette data
        /// </summary>
        public List<int> PaletteOffset { get; private set; }

        /// <summary>
        /// Offset within the palette block for each palette data (for encoded values)
        /// </summary>
        public List<int> PaletteBlockOffset { get; private set; }

        /// <summary>
        /// The actual F3DEXImage reference
        /// </summary>
        public F3DEXImage ImageReference { get; private set; }

        /// <summary>
        /// Byte encoding of each palette data
        /// </summary>
        public List<MK64ImageEncoding> PaletteEncoding { get; private set; }

        /// <summary>
        /// Number of colors in each palette
        /// </summary>
        public List<int> PaletteColorCount { get; private set; }

        /// <summary>
        /// Offset to use in each palette
        /// </summary>
        public List<int> PaletteColorOffset { get; private set; }

        /// <summary>
        /// Pixel format of the texture
        /// </summary>
        public Texture.ImageFormat Format { get; private set; }

        /// <summary>
        /// Pixel size of the texture
        /// </summary>
        public Texture.PixelInfo PixelSize { get; private set; }

        /// <summary>
        /// Width of the texture
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Height of the texture
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// If this image is in the original rom, returns true
        /// </summary>
        public bool IsOriginalImage { get; private set; }

        /// <summary>
        /// The resulting MK64 image
        /// </summary>
        public Bitmap Image
        {
            get
            {
                if ((ImageReference == null || ImageReference.Image == null) && (TKMKReference == null || TKMKReference.Image == null))
                    return null;

                if (TKMKReference != null && TKMKReference.Image != null)
                    return TKMKReference.Image;

                return ImageReference.Image;
            }
            //set {}
        }

        /// <summary>
        /// The name for the image
        /// </summary>
        public string ImageName { get; private set; }

        /// <summary>
        /// True if the Image was successfully created from the data
        /// </summary>
        public bool IsValidImage { get; private set; }

        public enum MK64ImageEncoding
        {
            Raw,
            MIO0,
            TKMK00
        }

        //Constructors to add a new image in without needing an offset (NO MIO0 ENCODING FOR THIS YET)
        public MK64Image(F3DEXImage image, string name, bool encodeTextureInMIO0 = false)
        {
            TextureOffset = -1;
            TextureEncoding = (encodeTextureInMIO0 ? MK64ImageEncoding.MIO0 : MK64ImageEncoding.Raw);
            TextureBlockOffset = 0;

            PaletteOffset = image.BasePalettes.Select<Palette, int>(p => p.FileOffset).ToList();
            PaletteEncoding = Enumerable.Repeat(MK64ImageEncoding.Raw, image.BasePalettes.Count).ToList();
            PaletteBlockOffset = Enumerable.Repeat((int)0, image.BasePalettes.Count).ToList();
            PaletteColorCount = image.BasePalettes.Select<Palette, int>(p => p.Colors.Length).ToList();
            PaletteColorOffset = Enumerable.Repeat((int)0, image.BasePalettes.Count).ToList();

            TKMKLength = 0;
            TKMKAlphaColor = 0;

            Format = image.Texture.Format;
            PixelSize = image.Texture.PixelSize;
            Width = image.Texture.Width;
            Height = image.Texture.Height;
            IsOriginalImage = false;

            if (string.IsNullOrWhiteSpace(name))
                ImageName = TextureOffset.ToString("X");
            else
                ImageName = name;

            ImageReference = image;
            IsValidImage = image.ValidImage;
        }

        public MK64Image(TKMK00Block tkmk, string name)
        {
            TextureOffset = -1;
            TextureEncoding = MK64ImageEncoding.TKMK00;
            TextureBlockOffset = 0;

            PaletteOffset = new List<int>();
            PaletteEncoding = new List<MK64ImageEncoding>();
            PaletteBlockOffset = new List<int>();
            PaletteColorCount = new List<int>();
            PaletteColorOffset = new List<int>();

            TKMKLength = tkmk.RawDataSize;
            TKMKAlphaColor = tkmk.ImageAlphaColor;

            Format = Texture.ImageFormat.RGBA;
            PixelSize = Texture.PixelInfo.Size_16b;
            Width = tkmk.Image.Width;
            Height = tkmk.Image.Height;
            IsOriginalImage = false;

            if (string.IsNullOrWhiteSpace(name))
                ImageName = TextureOffset.ToString("X");
            else
                ImageName = name;

            TKMKReference = tkmk;
            IsValidImage = (tkmk != null);
        }

        //Special constructors for the MK64ImageInfo class
        public MK64Image(MarioKartRomInfo.MK64ImageInfo info, byte[] rawFileData = null)
            : this(info.TextureOffset, (MK64ImageEncoding)Enum.Parse(typeof(MK64ImageEncoding), info.TextureEncoding), info.TextureBlockOffset,
                    (Texture.ImageFormat)Enum.Parse(typeof(Texture.ImageFormat), info.Format), (Texture.PixelInfo)Enum.Parse(typeof(Texture.PixelInfo), info.PixelSize),
                    info.Width, info.Height, info.IsOriginal, info.PaletteOffset,
                    info.PaletteEncoding.ConvertAll(delegate(string x) { return (MK64ImageEncoding)Enum.Parse(typeof(MK64ImageEncoding), x); }),
                    info.PaletteBlockOffset, info.PaletteColorCount, info.PaletteColorOffset, info.TkmkLength, info.TkmkAlpha, info.Name, rawFileData)
        {
        }

        public MK64Image(int textureOffset, MK64ImageEncoding textureEncoding, int textureBlockOffset, Texture.ImageFormat format,
            Texture.PixelInfo pixelSize, int width, int height, bool isOriginal,
            List<int> paletteOffset, List<MK64ImageEncoding> paletteEncoding, List<int> paletteBlockOffset, List<int> paletteColorCount, List<int> paletteColorOffset,
            int tkmkLength = 0, ushort tkmkAlpha = 0, string name = "", byte[] rawFileData = null)
        {
            TextureOffset = textureOffset;
            TextureEncoding = textureEncoding;
            TextureBlockOffset = textureBlockOffset;

            PaletteOffset = paletteOffset;
            PaletteEncoding = paletteEncoding;
            PaletteBlockOffset = paletteBlockOffset;
            PaletteColorCount = paletteColorCount;
            PaletteColorOffset = paletteColorOffset;

            TKMKLength = tkmkLength;
            TKMKAlphaColor = tkmkAlpha;

            Format = format;
            PixelSize = pixelSize;
            Width = width;
            Height = height;
            IsOriginalImage = isOriginal;

            if (string.IsNullOrWhiteSpace(name))
                ImageName = textureOffset.ToString("X");
            else
                ImageName = name;

            LoadImageData(rawFileData);
        }

        public MK64Image(XElement xml, Palette existingPalette = null)
        {
            TextureOffset = int.Parse(xml.Attribute(TEXTURE_OFFSET).Value.ToString());
            TextureEncoding = (MK64ImageEncoding)Enum.Parse(typeof(MK64ImageEncoding), xml.Attribute(TEXTURE_ENCODING).Value.ToString());
            TextureBlockOffset = int.Parse(xml.Attribute(TEXTURE_BLOCK_OFFSET).Value.ToString());

            XElement palettesXML = xml.Element(PALETTES);
            PaletteOffset = new List<int>();
            PaletteEncoding = new List<MK64ImageEncoding>();
            PaletteBlockOffset = new List<int>();
            PaletteColorCount = new List<int>();
            PaletteColorOffset = new List<int>();

            foreach (XElement paletteXml in palettesXML.Elements())
            {
                PaletteOffset.Add(int.Parse(paletteXml.Attribute(PALETTE_OFFSET).Value.ToString()));
                PaletteEncoding.Add((MK64ImageEncoding)Enum.Parse(typeof(MK64ImageEncoding), paletteXml.Attribute(PALETTE_ENCODING).Value.ToString()));
                PaletteBlockOffset.Add(int.Parse(paletteXml.Attribute(PALETTE_BLOCK_OFFSET).Value.ToString()));
                PaletteColorCount.Add(int.Parse(paletteXml.Attribute(PALETTE_COLOR_COUNT).Value.ToString()));
                PaletteColorOffset.Add(int.Parse(paletteXml.Attribute(PALETTE_COLOR_OFFSET).Value.ToString()));
            }

            TKMKLength = int.Parse(xml.Attribute(TKMK_LENGTH).Value.ToString());
            TKMKAlphaColor = ushort.Parse(xml.Attribute(TKMK_ALPHA_COLOR).Value.ToString());

            Format = (Texture.ImageFormat)Enum.Parse(typeof(Texture.ImageFormat), xml.Attribute(FORMAT).Value.ToString());
            PixelSize = (Texture.PixelInfo)Enum.Parse(typeof(Texture.PixelInfo), xml.Attribute(PIXEL_SIZE).Value.ToString());
            Width = int.Parse(xml.Attribute(WIDTH).Value.ToString());
            Height = int.Parse(xml.Attribute(HEIGHT).Value.ToString());
            IsOriginalImage = bool.Parse(xml.Attribute(IS_ORIGINAL_IMAGE).Value.ToString());

            ImageName = xml.Attribute(IMAGE_NAME).Value.ToString();

            if (xml.Element(typeof(TKMK00Block).ToString()) != null)
            {
                //load TKMK00 reference here
                if(!FindExistingTKMK00())
                    TKMKReference = new TKMK00Block(xml.Element(typeof(TKMK00Block).ToString()));
            }
            else if (xml.Element(F3DEXImage.F3DEXIMAGE) != null)
            {
                //load f3deximage reference
                F3DEXImage newImage = new F3DEXImage(xml.Element(F3DEXImage.F3DEXIMAGE), existingPalette);
                if (!FindExistingF3DEXImage(newImage.Texture, newImage.BasePalettes))
                    ImageReference = newImage;
            }
            else
                LoadImageData();
        }

        public XElement GetAsXML()
        {
            return GetAsXML(false);
        }

        public XElement GetAsXML(bool exportImageData, Palette existingPalette = null)
        {
            XElement xml = new XElement(MK64IMAGE);

            xml.Add(new XAttribute(TEXTURE_OFFSET, TextureOffset));
            xml.Add(new XAttribute(TEXTURE_BLOCK_OFFSET, TextureBlockOffset));
            xml.Add(new XAttribute(TEXTURE_ENCODING, TextureEncoding));

            XElement palettesXml = new XElement(PALETTES);
            for (int i = 0; i < PaletteOffset.Count; i++)
            {
                XElement paletteXml = new XElement(PALETTE);
                paletteXml.Add(new XAttribute(PALETTE_OFFSET, PaletteOffset[i]));
                paletteXml.Add(new XAttribute(PALETTE_BLOCK_OFFSET, PaletteBlockOffset[i]));
                paletteXml.Add(new XAttribute(PALETTE_ENCODING, PaletteEncoding[i]));
                paletteXml.Add(new XAttribute(PALETTE_COLOR_COUNT, PaletteColorCount[i]));
                paletteXml.Add(new XAttribute(PALETTE_COLOR_OFFSET, PaletteColorOffset[i]));
                palettesXml.Add(paletteXml);
            }
            xml.Add(palettesXml);

            xml.Add(new XAttribute(TKMK_LENGTH, TKMKLength));
            xml.Add(new XAttribute(TKMK_ALPHA_COLOR, TKMKAlphaColor));

            xml.Add(new XAttribute(FORMAT, Format));
            xml.Add(new XAttribute(PIXEL_SIZE, PixelSize));
            xml.Add(new XAttribute(WIDTH, Width));
            xml.Add(new XAttribute(HEIGHT, Height));
            xml.Add(new XAttribute(IS_ORIGINAL_IMAGE, IsOriginalImage));
            xml.Add(new XAttribute(IMAGE_NAME, ImageName));

            if (exportImageData || TextureOffset == -1 || PaletteOffset.Count(p => p == -1) > 0)
            {
                if (TKMKReference != null)
                    xml.Add(TKMKReference.GetAsXML(true));
                else
                    xml.Add(ImageReference.GetAsXML(existingPalette));
            }

            return xml;
        }

        private bool FindExistingTKMK00()
        {
            if (RomProject.Instance.Files.Count == 0)
                return false;

            //Look for the TKMK00
            if (TextureOffset == -1)
                return false;

            N64DataElement element = RomProject.Instance.Files[0].GetElementAt(TextureOffset);
            if (element != null && element.FileOffset == TextureOffset && element is TKMK00Block)
            {
                TKMKReference = (TKMK00Block)element;
                IsValidImage = (TKMKReference != null);

                return true;
            }

            return false;
        }

        private bool FindExistingF3DEXImage(Texture newTexture, List<Palette> newPalettes)
        {
            if (RomProject.Instance.Files.Count == 0)
                return false;

            //Look for the TKMK00
            bool changesMade = false;
            
            N64DataElement element;
            Palette paletteRef = null;
            List<Palette> paletteRefs = new List<Palette>();

            if (Format == Texture.ImageFormat.CI)  //If CI format, we need to get the palette too
            {
                for (int i = 0; i < PaletteOffset.Count; i++)
                {
                    paletteRef = null;

                    element = RomProject.Instance.Files[0].GetElementAt(TextureOffset);
                    if (element != null && element.FileOffset == TextureOffset && !(element is UnknownData))
                    {
                        switch (PaletteEncoding[i])
                        {
                            case MK64ImageEncoding.Raw:

                                //Needs to be a raw palette
                                if (!(element is Palette))
                                    break;

                                paletteRef = (Palette)element;

                                break;
                            case MK64ImageEncoding.MIO0:

                                //Needs to be a mio0 block
                                if (!(element is MIO0Block))
                                    break;

                                MIO0Block block = (MIO0Block)element;
                                //System.IO.File.WriteAllBytes("test.bin",block.DecodedData);

                                //now to search inside
                                if (block.Elements.FirstOrDefault(e => e.FileOffset == PaletteBlockOffset[i]) != null)
                                {
                                    N64DataElement paletteEl = block.Elements.First(e => e.FileOffset == PaletteBlockOffset[i]);
                                    if (paletteEl is Palette)
                                        paletteRef = (Palette)paletteEl;
                                }
                                break;
                        }
                    }

                    if (paletteRef != null)
                    {
                        newPalettes[i] = paletteRef;
                        changesMade = true;
                    }
                }
            }

            //Now texture
            Texture textureRef = null;

            element = RomProject.Instance.Files[0].GetElementAt(TextureOffset);
            if (element != null && element.FileOffset == TextureOffset && !(element is UnknownData))
            {
                switch (TextureEncoding)
                {
                    case MK64ImageEncoding.Raw:

                        //Needs to be a raw texture
                        if (!(element is Texture))
                            break;

                        textureRef = (Texture)element;

                        break;
                    case MK64ImageEncoding.MIO0:

                        //Needs to be a mio0 block
                        if (!(element is MIO0Block))
                            break;

                        MIO0Block block = (MIO0Block)element;

                        //now to search inside
                        if (block.Elements.FirstOrDefault(e => e.FileOffset == TextureBlockOffset) != null)
                        {
                            N64DataElement textureEl = block.Elements.First(e => e.FileOffset == TextureBlockOffset);
                            if (textureEl is Texture)
                                textureRef = (Texture)textureEl;
                        }
                        break;
                }
            }

            if (textureRef != null)
            {
                newTexture = textureRef;
                changesMade = true;
            }

            if (changesMade)
            {
                ImageReference = new F3DEXImage(newTexture, newPalettes);
                IsValidImage = ImageReference.ValidImage;

                return true;
            }

            return false;
        }

        private void LoadImageData(byte[] rawData = null)
        {
            IsValidImage = false;

            //Check that the format is correct (ex. CI & palette offsets are valid)
            int maxTextureLocation = (rawData != null ? rawData.Length - 1 : RomProject.Instance.Files[0].FileLength - 1);

            if(TextureOffset < 0 || TextureOffset > maxTextureLocation ||
                (Format == Texture.ImageFormat.CI && PaletteOffset.Count == 0))
                    return;

            if(!TextureConversion.IsValidFormatCombo(Format, PixelSize))
                return;

            //Try to load the texture, palette & image here
            N64DataElement element;
            Palette paletteRef = null;
            List<Palette> paletteRefs = new List<Palette>();

            if (Format == Texture.ImageFormat.CI)  //If CI format, we need to get the palette too
            {
                for (int i = 0; i < PaletteOffset.Count; i++)
                {
                    paletteRef = null;
                    element = RomProject.Instance.Files[0].GetElementAt(PaletteOffset[i]);
                    if (element != null && element.FileOffset == PaletteOffset[i] && !(element is UnknownData))
                    {
                        switch (PaletteEncoding[i])
                        {
                            case MK64ImageEncoding.Raw:

                                //Needs to be a raw palette
                                if (!(element is Palette))
                                    return;

                                paletteRef = (Palette)element;

                                break;
                            case MK64ImageEncoding.MIO0:

                                //Needs to be a mio0 block
                                if (!(element is MIO0Block))
                                    return;

                                MIO0Block block = (MIO0Block)element;
                                //System.IO.File.WriteAllBytes("test.bin",block.DecodedData);

                                //now to search inside
                                if (block.Elements.FirstOrDefault(e => e.FileOffset == PaletteBlockOffset[i]) != null)
                                {
                                    N64DataElement paletteEl = block.Elements.First(e => e.FileOffset == PaletteBlockOffset[i]);
                                    if (paletteEl is Palette)
                                        paletteRef = (Palette)paletteEl;
                                }
                                else
                                {
                                    //Create the palette!
                                    byte[] data = new byte[PaletteColorCount[i] * 2];
                                    Array.Copy(block.DecodedData, PaletteBlockOffset[i], data, 0, data.Length);
                                    Palette pal = new Palette(PaletteBlockOffset[i], data);
                                    if (block.AddElement(pal))
                                        paletteRef = pal;
                                }

                                break;
                        }
                    }
                    else
                    {
                        //AVOID THIS AS MUCH AS POSSIBLE, IT WILL SLOW EVERYTHING DOWN TREMENDOUSLY!!
                        if (rawData == null)
                            rawData = RomProject.Instance.Files[0].GetAsBytes();

                        //Create the palette!
                        switch (PaletteEncoding[i])
                        {
                            case MK64ImageEncoding.Raw:

                                //Create the palette!
                                byte[] data = new byte[PaletteColorCount[i] * 2];
                                Array.Copy(rawData, PaletteOffset[i], data, 0, data.Length);

                                Palette pal = new Palette(PaletteOffset[i], data);
                                if (RomProject.Instance.Files[0].AddElement(pal))
                                    paletteRef = pal;

                                break;
                            case MK64ImageEncoding.MIO0:

                                //Create the MIO0 block!
                                MIO0Block block = MIO0Block.ReadMIO0BlockFrom(rawData, PaletteOffset[i]);
                                //System.IO.File.WriteAllBytes("test.bin",block.DecodedData);
                                if (RomProject.Instance.Files[0].AddElement(block))
                                {
                                    //Create the palette in the MIO0 block!
                                    byte[] mioData = new byte[PaletteColorCount[i] * 2];
                                    Array.Copy(block.DecodedData, PaletteBlockOffset[i], mioData, 0, mioData.Length);

                                    Palette palette = new Palette(PaletteBlockOffset[i], mioData);
                                    if (block.AddElement(palette))
                                        paletteRef = palette;
                                    else
                                        throw new Exception();
                                }

                                break;
                        }
                    }
                    paletteRefs.Add(paletteRef);
                }
            }
            
            
            Texture textureRef = null;
            TKMK00Block tkmkRef = null;

            element = RomProject.Instance.Files[0].GetElementAt(TextureOffset);
            if (element != null && element.FileOffset == TextureOffset && !(element is UnknownData))
            {
                switch (TextureEncoding)
                {
                    case MK64ImageEncoding.Raw:

                        //Needs to be a raw texture
                        if (!(element is Texture))
                            return;

                        textureRef = (Texture)element;

                        break;
                    case MK64ImageEncoding.MIO0:

                        //Needs to be a mio0 block
                        if (!(element is MIO0Block))
                            return;

                        MIO0Block block = (MIO0Block)element;

                        //now to search inside
                        if (block.Elements.FirstOrDefault(e => e.FileOffset == TextureBlockOffset) != null)
                        {
                            N64DataElement textureEl = block.Elements.First(e => e.FileOffset == TextureBlockOffset);
                            if (textureEl is Texture)
                                textureRef = (Texture)textureEl;
                        }
                        else
                        {
                            //Create the texture here
                            //Create the palette!
                            double byteSize = 1;
                            switch (PixelSize)
                            {
                                case Texture.PixelInfo.Size_4b:
                                    byteSize = 0.5;
                                    break;
                                case Texture.PixelInfo.Size_16b:
                                    byteSize = 2;
                                    break;
                                case Texture.PixelInfo.Size_32b:
                                    byteSize = 4;
                                    break;
                            }
                            byte[] data = new byte[(int)Math.Round(Width * Height * byteSize)];
                            Array.Copy(block.DecodedData, TextureBlockOffset, data, 0, data.Length);
                            Texture text = new Texture(TextureBlockOffset, data, Format, PixelSize, Width, Height);
                            
                            if (block.AddElement(text))
                                textureRef = text;
                        }

                        break;
                    case MK64ImageEncoding.TKMK00:

                        //Needs to be a tkmk00 block
                        if (!(element is TKMK00Block))
                            return;

                        tkmkRef = (TKMK00Block)element;

                        break;
                }
            }
            else //NEED TO CREATE IT HERE!
            {
                //AVOID THIS AS MUCH AS POSSIBLE, IT WILL SLOW EVERYTHING DOWN TREMENDOUSLY!!
                if(rawData == null)
                    rawData = RomProject.Instance.Files[0].GetAsBytes();

                switch (TextureEncoding)
                {
                    case MK64ImageEncoding.Raw:
                        //Just create the new texture image!
                        double byteSize = 1;
                        switch (PixelSize)
                        {
                            case Texture.PixelInfo.Size_4b:
                                byteSize = 0.5;
                                break;
                            case Texture.PixelInfo.Size_16b:
                                byteSize = 2;
                                break;
                            case Texture.PixelInfo.Size_32b:
                                byteSize = 4;
                                break;
                        }
                        byte[] data = new byte[(int)Math.Round(Width * Height * byteSize)];
                        Array.Copy(rawData, TextureOffset, data, 0, data.Length);
                        Texture texture = new Texture(TextureOffset, data, Format, PixelSize, Width, Height);

                        //Add to the rom project
                        if (RomProject.Instance.Files[0].AddElement(texture))
                        {
                            textureRef = texture;
                        }
                        break;
                    case MK64ImageEncoding.MIO0:
                        //Since no MIO0 block was found, it needs to be created
                        MIO0Block block = MIO0Block.ReadMIO0BlockFrom(rawData, TextureOffset);
                        if (RomProject.Instance.Files[0].AddElement(block))
                        {
                            double ByteSize = 1;
                            switch (PixelSize)
                            {
                                case Texture.PixelInfo.Size_4b:
                                    ByteSize = 0.5;
                                    break;
                                case Texture.PixelInfo.Size_16b:
                                    ByteSize = 2;
                                    break;
                                case Texture.PixelInfo.Size_32b:
                                    ByteSize = 4;
                                    break;
                            }
                            byte[] textData = new byte[(int)Math.Round(Width * Height * ByteSize)];
                            Array.Copy(block.DecodedData, TextureBlockOffset, textData, 0, textData.Length);
                            Texture text = new Texture(TextureBlockOffset, textData, Format, PixelSize, Width, Height);

                            if (block.AddElement(text))
                                textureRef = text;
                        }

                        break;
                    case MK64ImageEncoding.TKMK00:
                        //Need to create the TKMK00Block, and it'll handle the rest
                        TKMK00Block tkmk;

                        byte[] bytes = new byte[TKMKLength];
                        Array.Copy(rawData, TextureOffset, bytes, 0, TKMKLength);

                        tkmk = new TKMK00Block(TextureOffset, bytes, TKMKAlphaColor);

                        if (RomProject.Instance.Files[0].AddElement(tkmk))
                        {
                            tkmkRef = tkmk;
                        }

                        break;
                }
            }

            if((textureRef == null && tkmkRef == null) || (Format == Texture.ImageFormat.CI && (paletteRefs.Count == 0 || paletteRefs.Contains(null))))
                return;

            //Test to make sure the texture matches the format and such
            if(textureRef != null && Format != textureRef.Format)
                return;

            //Successfully found the palette, set the references
            TKMKReference = tkmkRef;
            if (TextureEncoding == MK64ImageEncoding.TKMK00)
            {
                IsValidImage = (TKMKReference != null);
                return;
            }

            F3DEXImage image = new F3DEXImage(textureRef, paletteRefs);
            ImageReference = image;

            IsValidImage = image.ValidImage;
        }
        
        public override string ToString()
        {
            return this.ImageName;
        }
    }
}
