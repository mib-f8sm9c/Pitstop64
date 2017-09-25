using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.Xml.Linq;

namespace Pitstop64.Data.Text
{
    public class TextBankBlock : N64DataElement
    {
        public static int TEXT_BLOCK_START = 0x0F0468;
        //public static int TEXT_BLOCK_END = 0x0F0C4C;
        public static int TEXT_INTERRUPT_START = 0xF0950;
        public static int TEXT_INTERRUPT_END = 0xF096C;
        public static int TEXT_BLOCK_END = 0x0F1718;

        //Note: there is some text after this block, but I'm going to ignore it : )

        public static int TEXT_BLOCK_LENGTH { get { return TEXT_BLOCK_END - TEXT_BLOCK_START; } }

        //Space still available in the text block
        public int FreeSpace { get; private set; }

        private byte[] _rawData;
        private byte[] _interruptData;

        public TextBankBlock(int fileOffset, byte[] data)
            : base(fileOffset, data)
        {

        }

        public TextBankBlock(XElement xml, byte[] data)
            : base(xml, data)
        {

        }

        private void CalculateFreeSpace()
        {
            if (_rawData == null)
                return;

            int emptyCount;
            for (emptyCount = 0; emptyCount < _rawData.Length; emptyCount++)
            {
                //This skips over the table that we shall never touch
                if (_rawData.Length - 1 - emptyCount < (TEXT_INTERRUPT_END - TEXT_BLOCK_START) &&
                    (TEXT_INTERRUPT_START - TEXT_BLOCK_START) >= _rawData.Length - 1 - emptyCount)
                    continue;

                if (_rawData[_rawData.Length - 1 - emptyCount] != 0x00)
                    break;
            }

            FreeSpace = emptyCount - 1;
        }

        public override byte[] RawData
        {
            get
            {
                return _rawData;
            }
            set
            {
                if (value.Length == RawDataSize)
                {
                    _rawData = value;
                    if (_interruptData == null)
                    {
                        _interruptData = new byte[TEXT_INTERRUPT_END - TEXT_INTERRUPT_START];
                        Array.Copy(value, TEXT_INTERRUPT_START - TEXT_BLOCK_START, _interruptData, 0, _interruptData.Length);
                    }
                    else
                    {
                        Array.Copy(_interruptData, 0, value, TEXT_INTERRUPT_START - TEXT_BLOCK_START, _interruptData.Length);
                    }
                    CalculateFreeSpace();
                }
            }
        }

        public override int RawDataSize
        {
            get { return TEXT_BLOCK_LENGTH; }
        }
    }
}
