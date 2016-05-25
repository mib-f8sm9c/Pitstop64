using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Utils;

namespace MarioKartTestingTool
{
    public class TextureMIORef : N64DataElement
    {
        [CategoryAttribute("Texture MIO Settings"),
        DescriptionAttribute("Offset of the texture MIO block offset by 0x641F70")]
        public int RomOffset { get; set; }

        [CategoryAttribute("Texture MIO Settings"),
        DescriptionAttribute("Size of the MIO encoded data in bytes")]
        public int CompressedSize { get; set; }

        [CategoryAttribute("Texture MIO Settings"),
        DescriptionAttribute("Size of the decoded data in bytes")]
        public int DecompressedSize { get; set; }


        public TextureMIORef(int offset, byte[] rawData)
            : base(offset, rawData)
        {
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(RomOffset, CompressedSize, DecompressedSize, (int)0x00000000);
            }
            set
            {
                RomOffset = ByteHelper.ReadInt(value, 0);
                CompressedSize = ByteHelper.ReadInt(value, 4);
                DecompressedSize = ByteHelper.ReadInt(value, 8);
            }
        }

        public override int RawDataSize
        {
            get { return 0x10; }
        }

    }
}
