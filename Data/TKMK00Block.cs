using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.Drawing;
using MarioKartTestingTool;
using System.Xml.Linq;

namespace MK64Pitstop.Data
{
    public class TKMK00Block : N64DataElement
    {
        private const string XML_ALPHA = "ImgAlpha";

        private bool _hasChanged;

        private byte[] _rawData;

        public ushort ImageAlphaColor { get; set; }

        private TKMK00Encoder.TKMK00Header _tkmk00Header;

        public Bitmap Image
        {
            get 
            {
                if (_hasChanged)
                {
                    byte[] headerData = new byte[TKMK00Encoder.TKMK00Header.DataSize];
                    Array.Copy(_rawData, 0, headerData, 0, TKMK00Encoder.TKMK00Header.DataSize);
                    _tkmk00Header = new TKMK00Encoder.TKMK00Header(headerData);

                    _cachedImage = Cereal64.Microcodes.F3DEX.DataElements.TextureConversion.BinaryToRGBA16(
                        TKMK00Encoder.Decode(_rawData, 0, ImageAlphaColor), _tkmk00Header.Width, _tkmk00Header.Height);

                    _hasChanged = false;
                }

                return _cachedImage;
            }
        }

        public bool SetImage(Bitmap image)
        {
            byte[] imgData = Cereal64.Microcodes.F3DEX.DataElements.TextureConversion.RGBA16ToBinary(image);
            byte[] compressedData = TKMK00Encoder.Encode(imgData, _tkmk00Header.Width, _tkmk00Header.Height, ImageAlphaColor);

            if (compressedData.Length > RawDataSize)
            {
                return false;
            }

            if (compressedData.Length < RawDataSize)
            {
                byte[] extendedData = new byte[RawDataSize];
                Array.Copy(compressedData, 0, extendedData, 0, compressedData.Length);
                compressedData = extendedData;
            }

            RawData = compressedData;

            return true;
        }

        private Bitmap _cachedImage;

        public TKMK00Block(XElement xml, byte[] rawData)
            : base(xml, rawData)
        {
            _hasChanged = true;

            ImageAlphaColor = ushort.Parse(xml.Attribute(XML_ALPHA).Value);
        }

        public TKMK00Block(int offset, byte[] rawData, ushort alphaColor)
            : base (offset, rawData)
        {
            _hasChanged = true;

            ImageAlphaColor = alphaColor;
        }

        public override byte[] RawData
        {
            get
            {
                return _rawData;
            }
            set
            {
                _rawData = value;
                _hasChanged = true;
            }
        }

        public override System.Xml.Linq.XElement GetAsXML()
        {
            XElement xml = base.GetAsXML();

            xml.Add(new XAttribute(XML_ALPHA, ImageAlphaColor));

            return xml;
        }

        public override int RawDataSize
        {
            get { return _rawData.Length; }
        }

        public override string ToString()
        {
            return string.Format("0x{0:X6}", FileOffset);
        }
    }
}
