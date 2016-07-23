using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Rom;
using Cereal64.Common.Utils;

namespace MK64Pitstop.Data
{
    public class DmaAddressBlock: N64DataElement
    {
        //Should I lock the addresses size, so it can't be too big?
        public List<DmaAddress> Addresses { get; private set; }

        public DmaAddressBlock(int offset, byte[] data, int courseID)
            : base(offset, data)
        {
        }

        private void ResetContainers()
        {
            if (Addresses == null)
                Addresses = new List<DmaAddress>();

            Addresses.Clear();
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(Addresses.ToArray());
            }
            set
            {
                ResetContainers();

                for (int i = 0; i < value.Length - 3; i += 4)
                    Addresses.Add(new DmaAddress(ByteHelper.ReadInt(value, i)));
            }
        }

        //public void LoadDmaReferences()
        //{
        //}


        public override int RawDataSize
        {
            get { return Addresses.Count * 4; }
        }
    }
}
