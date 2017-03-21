using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.Drawing;
using Cereal64.Common.Utils;
using System.Xml.Linq;

namespace Pitstop64.Data.Tracks
{
    public class TrackSkyTable : N64DataElement
    {
        public Color[] TopColors { get; private set; }

        public Color[] BottomColors { get; private set; }

        public TrackSkyTable(int offset, byte[] data)
            : base(offset, data)
        {

        }

        public TrackSkyTable(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {

        }
                
        public override byte[] RawData
        {
            get
            {
                byte[] bytes = new byte[RawDataSize];

                for(int i = 0; i < TopColors.Length; i++)
                {
                    Array.Copy(ByteHelper.CombineIntoBytes((ushort)TopColors[i].R,(ushort)TopColors[i].G,(ushort)TopColors[i].B,
                        (ushort)BottomColors[i].R,(ushort)BottomColors[i].G,(ushort)BottomColors[i].B).ToArray(), 0,
                        bytes, i * 12, 12);
                }

                return bytes;
            }
            set
            {
                int count = value.Length / 12;
                TopColors = new Color[count];
                BottomColors = new Color[count];

                for (int i = 0; i < MarioKartRomInfo.TrackCount; i++)
                {
                    byte R = (byte)ByteHelper.ReadUShort(value, i * 12);
                    byte G = (byte)ByteHelper.ReadUShort(value, i * 12 + 2);
                    byte B = (byte)ByteHelper.ReadUShort(value, i * 12 + 4);
                    TopColors[i] = Color.FromArgb(255, R, G, B);
                    
                    R = (byte)ByteHelper.ReadUShort(value, i * 12 + 6);
                    G = (byte)ByteHelper.ReadUShort(value, i * 12 + 8);
                    B = (byte)ByteHelper.ReadUShort(value, i * 12 + 10);
                    BottomColors[i] = Color.FromArgb(255, R, G, B);
                }
            }
        }

        public override int RawDataSize
        {
            get { return TopColors.Length * 12; } //Don't hardcode it in the future?
        }
    }
}
