using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Utils.Encoding;
using System.Xml.Linq;

namespace MK64Pitstop.Data
{
    //A simple extended MIO0Block class that adds extra info for images being stored in it
    public class ImageMIO0Block : MIO0Block
    {
        private const string IMAGE_NAME = "imageName";

        public string ImageName { get; set; }

        public ImageMIO0Block(int offset, byte[] rawData)
            : base(offset, rawData)
        {
            ImageName = offset.ToString("X8");
        }

        public ImageMIO0Block(string name, int offset, byte[] rawData)
            : base(offset, rawData)
        {
            ImageName = name;
        }

        public ImageMIO0Block(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {
            ImageName = xml.Attribute(IMAGE_NAME).Value;
        }

        public static ImageMIO0Block ReadImageMIO0BlockFrom(byte[] data, int offset)
        {
            int mio0Length = MIO0.FindLengthOfMIO0Block(data, offset);
            byte[] mio0Data = new byte[mio0Length];
            Array.Copy(data, offset, mio0Data, 0, mio0Length);

            return new ImageMIO0Block(offset.ToString("X8"), offset, mio0Data);
        }

        public override XElement GetAsXML()
        {
            XElement xml = base.GetAsXML();
            xml.Add(new XAttribute(IMAGE_NAME, ImageName));
            return xml;
        }

        public override string ToString()
        {
            return ImageName;
        }
    }
}
