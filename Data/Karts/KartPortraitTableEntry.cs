using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Utils;
using Cereal64.Common.Rom;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace Pitstop64.Data.Karts
{
    public class KartPortraitTableEntry : N64DataElement
    {
        //First 4 bytes: 0
        //Second 4 bytes: Address for ImageMIO0
        //Next 2 bytes: width
        //Next 2 bytes: height
        //Rest: 0
        //Size: 0x28
        public int ImageOffset { get; set; }
        public short ImageWidth { get; set; }
        public short ImageHeight { get; private set; }

        public ImageMIO0Block ImageReference { get { return _image; }
            set 
            {
                if (value != null)
                {
                    _image = value;
                    ImageOffset = value.FileOffset - MarioKartRomInfo.CharacterFaceMIO0Offset;
                    if (value.DecodedN64DataElement != null && value.DecodedN64DataElement is Texture)
                    {
                        Texture texture = (Texture)value.DecodedN64DataElement;
                        ImageWidth = (short)texture.Width;
                        ImageHeight = (short)texture.Height;
                    }
                }
            }
        }
        private ImageMIO0Block _image;

        public KartPortraitTableEntry(int fileOffset, byte[] data)
            : base(fileOffset, data)
        {

        }

        public KartPortraitTableEntry(int fileOffset, ImageMIO0Block block)
            : base(fileOffset, null)
        {
            ImageReference = block;

            if (block.DecodedN64DataElement is Texture)
            {
                ImageOffset = block.FileOffset - MarioKartRomInfo.CharacterFaceMIO0Offset;
                ImageWidth = (short)((Texture)block.DecodedN64DataElement).Width;
                ImageHeight = (short)((Texture)block.DecodedN64DataElement).Height;
            }
        }

        public override byte[] RawData
        {
            get
            {
                byte[] data = ByteHelper.CombineIntoBytes(new byte[4], (ImageOffset & 0x00FFFFFF) | 0x0A000000, ImageWidth,
                    ImageHeight, (int)0, (int)0, (int)0, (int)0, (int)0, (int)0, (int)0);
                return data;
            }
            set
            {
                if (value == null || value.Length != RawDataSize)
                    return;

                ImageOffset = ByteHelper.ReadInt(value, 0x4) & 0x00FFFFFF;
                ImageWidth = ByteHelper.ReadShort(value, 0x8);
                ImageHeight = ByteHelper.ReadShort(value, 0xA);
            }
        }

        public override int RawDataSize
        {
            get { return 0x28; }
        }
    }
}
