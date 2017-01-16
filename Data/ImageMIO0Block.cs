using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Common.Utils.Encoding;
using System.Xml.Linq;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace Pitstop64.Data
{
    //A simple extended MIO0Block class that adds extra info for images being stored in it
    public class ImageMIO0BlockINVALID : MIO0Block
    {
        private const string IMAGE_NAME = "imageName";

        public string ImageName { get; set; }

        public F3DEXImage Image
        {
            get { return _image; }
            set
            {
                _image = Image;

                if (Elements.Count > 0)
                    ClearElements();

                //Assume that the MIO0 will contain only the texture, it's a safe bet right now
                _elements.AddElement(_image.Texture);
            }
        }
        private F3DEXImage _image;

        public ImageMIO0BlockINVALID(int offset, byte[] rawData)
            : base(offset, rawData)
        {
            ImageName = offset.ToString("X8");
        }

        public ImageMIO0BlockINVALID(string name, int offset, byte[] rawData)
            : base(offset, rawData)
        {
            ImageName = name;
        }

        public ImageMIO0BlockINVALID(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {
            ImageName = xml.Attribute(IMAGE_NAME).Value;
        }

        public static ImageMIO0BlockINVALID ReadImageMIO0BlockFrom(byte[] data, int offset)
        {
            int mio0Length = MIO0.FindLengthOfMIO0Block(data, offset);
            byte[] mio0Data = new byte[mio0Length];
            Array.Copy(data, offset, mio0Data, 0, mio0Length);

            return new ImageMIO0BlockINVALID(offset.ToString("X8"), offset, mio0Data);
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
