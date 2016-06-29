using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;

namespace MK64Pitstop.Data
{
    public class TextBankBlock : N64DataElement
    {
        public static int TEXT_BLOCK_START = 0x0f0468;
        public static int TEXT_BLOCK_END = 0x0f1718;

        public TextBankBlock(int fileOffset, byte[] data)
            : base(fileOffset, data)
        {

        }

        public override byte[] RawData
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int RawDataSize
        {
            get { throw new NotImplementedException(); }
        }
    }
}
