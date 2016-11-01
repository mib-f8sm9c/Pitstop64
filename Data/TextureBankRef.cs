using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Utils;
using Cereal64.Common.Rom;

namespace MK64Pitstop.Data
{
    //NOT USED, CAN DELETE!

    /// <summary>
    /// This reference points to an MIO block that contains a texture
    /// </summary>
    public class TextureBankRef : N64DataElement
    {
        public List<ImageMIO0Block> Textures = new List<ImageMIO0Block>();

        public TextureBankRef(int offset, byte[] rawData)
            : base(offset, rawData)
        {
            InitDataContainers();
        }

        public void InitDataContainers()
        {
            if (Textures == null)
            {
                Textures = new List<ImageMIO0Block>();
            }
        }

        public override byte[] RawData
        {
            get
            {
                byte[] bytes = new byte[RawDataSize];
                for (int i = 0; i < Textures.Count; i++)
                {
                    Array.Copy(ByteHelper.CombineIntoBytes(Textures[i].FileOffset - MarioKartRomInfo.TextureBankOffset,
                        Textures[i].RawDataSize, Textures[i].DecodedData.Length), 0, bytes, i * 0xC, 0xC);
                }
                return bytes; // ByteHelper.CombineIntoBytes(RomOffset, CompressedSize, DecompressedSize, (int)0x00000000);
            }
            set
            {
                InitDataContainers();

                Textures.Clear();

                LoadDmaReferences();
            }
        }

        //Used ONLY to load the original 8 characters from the rom
        //NEED TO MOVE the rest of this code up to MarioKart64ElementHub!!
        //Also make sure all dataelements are properly added to the rom file!
        public void LoadDmaReferences()
        {
            byte[] data = RomProject.Instance.Files[0].GetAsBytes();
            
            int i = 0;
            byte[] blockBytes = new byte[0xC];
            //while (i < value.Length - 0xB)
            //{
            //    int offset = i * 0xC + MarioKartRomInfo.TextureBankOffset;

            //    Array.Copy(value, i * 0xC, blockBytes, 0, 0xC);

            //    //I don't know how to handle this, but oh well, do it fully later
            //    ImageMIO0Block block = new ImageMIO0Block(i * 0xC + MarioKartRomInfo.TextureBankOffset,

            //    RomOffset = ByteHelper.ReadInt(value, 0);
            //    CompressedSize = ByteHelper.ReadInt(value, 4);
            //    DecompressedSize = ByteHelper.ReadInt(value, 8);

            //    i += 0xC;
            //}
        }

        public override int RawDataSize
        {
            get { return Textures.Count * 0xC; }
        }

    }
}

