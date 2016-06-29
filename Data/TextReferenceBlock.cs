using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;

namespace MK64Pitstop.Data
{
    public class TextReferenceBlock : N64DataElement
    {
        public static int TEXT_REFERENCE_START = 0x0E8100;
        public static int TEXT_REFERENCE_END = 0x0E8264; //????? Probably not

        public TextReferenceBlock(int fileOffset, byte[] data)
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
