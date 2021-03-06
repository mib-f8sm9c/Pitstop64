﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;
using Cereal64.Common.DataElements;
using Pitstop64.Services;

namespace Pitstop64.Data
{
    public class TKMK00Block : N64DataElement
    {
        private const string XML_ALPHA = "ImgAlpha";
        private const string TKMK00_DATA = "TKMK00Data";

        private bool _hasChanged;

        private byte[] _rawData;

        public ushort ImageAlphaColor { get; set; }

        private TKMK00.TKMK00Header _tkmk00Header;

        public Bitmap Image
        {
            get 
            {
                if (_hasChanged)
                {
                    byte[] headerData = new byte[TKMK00.TKMK00Header.DataSize];
                    Array.Copy(_rawData, 0, headerData, 0, TKMK00.TKMK00Header.DataSize);
                    _tkmk00Header = new TKMK00.TKMK00Header(headerData);

                    _cachedImage = Cereal64.Microcodes.F3DEX.DataElements.TextureConversion.BinaryToRGBA16(
                        TKMK00.Decode(_rawData, 0, ImageAlphaColor), _tkmk00Header.Width, _tkmk00Header.Height);

                    _hasChanged = false;
                }

                return _cachedImage;
            }
        }

        public bool SetImage(Bitmap image)
        {
            //Force it to render the image if it hasn't already
            Bitmap img = this.Image;

            byte[] imgData = Cereal64.Microcodes.F3DEX.DataElements.TextureConversion.RGBA16ToBinary(image);
            byte[] compressedData = TKMK00.Encode(imgData, _tkmk00Header.Width, _tkmk00Header.Height, ImageAlphaColor);

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

        public TKMK00Block(XElement xml)
            : this(xml, Convert.FromBase64String(xml.Attribute(TKMK00_DATA).Value.ToString()))
        {

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

        public override XElement GetAsXML()
        {
            return GetAsXML(false);
        }

        public XElement GetAsXML(bool includeRawData)
        {
            XElement xml = base.GetAsXML();

            xml.Add(new XAttribute(XML_ALPHA, ImageAlphaColor));

            if (includeRawData)
            {
                xml.Add(new XAttribute(TKMK00_DATA, Convert.ToBase64String(_rawData)));
            }

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
