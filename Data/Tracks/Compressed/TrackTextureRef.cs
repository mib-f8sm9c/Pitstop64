using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Utils;
using Cereal64.Common.DataElements.Encoding;
using Cereal64.Common.Rom;

namespace Pitstop64.Data.Tracks.Compressed
{
    [AlternateXMLNames(new string[] { "CourseTextureRef" })]
    public class TrackTextureRef : N64DataElement
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

        public MK64Image ImageReference
        {
            get
            {
                return _imageReference;
            }
            set
            {
                if (value != null && value.IsValidImage && value.TextureEncoding == MK64Image.MK64ImageEncoding.MIO0 && value.TextureBlockOffset == 0)
                {
                    N64DataElement block;
                    if (RomProject.Instance.Files[0].HasElementExactlyAt(value.TextureOffset, out block) && block is MIO0Block)
                    {
                        _imageReference = value;

                        RomOffset = 0x0F000000 | (block.FileOffset - MarioKartRomInfo.TextureBankOffset);
                        CompressedSize = block.RawDataSize;
                        DecompressedSize = value.ImageReference.Texture.RawDataSize;
                    }

                }
            }
        }
        private MK64Image _imageReference;

        public TrackTextureRef(int offset, byte[] rawData)
            : base(offset, rawData)
        {
        }

        public TrackTextureRef(int offset, MK64Image img)
            : base(offset, null)
        {
            ImageReference = img;
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(RomOffset, CompressedSize, DecompressedSize, (int)0x00000000);
            }
            set
            {
                if (value == null)
                    return;

                RomOffset = ByteHelper.ReadInt(value, 0);
                CompressedSize = ByteHelper.ReadInt(value, 4);
                DecompressedSize = ByteHelper.ReadInt(value, 8);
            }
        }

        public override int RawDataSize
        {
            get { return TRACK_TEXTURE_REF_SIZE; }
        }

        public const int TRACK_TEXTURE_REF_SIZE = 0x10;

        public override string ToString()
        {
            if (_imageReference != null)
                return _imageReference.ImageName.ToString();
            else
                return base.ToString();
        }
    }
}
