using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.Rom;
using System.Xml.Linq;
using Cereal64.Microcodes.F3DEX.DataElements;
using System.IO;
using Ionic.Zip;
using Cereal64.Common.Utils;
using System.Drawing;

namespace Pitstop64.Data.Tracks
{
    /// <summary>
    /// TrackInfo is used when exporting/importing track data, and editing it in Track Shack. 
    ///  It doesn't use any compression, so it needs to be converted to a CompressedTrack when imported to Pitstop64.
    /// </summary>
    public class TrackInfo : RomItem
    {
        private const string TRACK_INFO = "TrackInfo";
        private const string TRACK_NAME = "TrackName";
        private const string UNKNOWN_1 = "Unknown1";
        private const string UNKNOWN_2 = "Unknown2";
        private const string TOP_COLOR = "TopColor";
        private const string BOTTOM_COLOR = "BottomColor";

        private const string TRACK_ITEMS = "TrackItems";
        private const string VERTICES = "Vertices";
        private const string COMMANDS = "Commands";
        private const string TEXTURES = "Textures";
        private const string COMMAND_REFERENCES = "CommandReferences";

        public TrackItemsObject TrackItems { get; private set; }
        public VertexCollection Vertices { get; private set; }
        public F3DEXCommandCollection F3DCommands { get; private set; }
        public List<MK64Image> TextureReferences { get; private set; }
        public List<DmaAddress> CommandReferences { get; private set; }

        public uint Unknown1 { get; private set; }
        public ushort Unknown2 { get; private set; }

        public Color TopColor { get; private set; }
        public Color BottomColor { get; private set; }

        //EVEN THOUGH IT'S POINTLESS, INCLUDE THE OTHER VALUES TOO!!

        public string TrackName { get; set; }

        public TrackInfo(string trackName, TrackItemsObject items, VertexCollection verts,
            F3DEXCommandCollection commands, List<MK64Image> images, List<DmaAddress> commandRefs,
            uint unknown1, ushort unknown2, Color topColor, Color bottomColor)
            : this(trackName)
        {
            TrackItems = items;
            Vertices = verts;
            F3DCommands = commands;
            TextureReferences = images;
            CommandReferences = commandRefs;

            Unknown1 = unknown1;
            Unknown2 = unknown2;

            TopColor = topColor;
            BottomColor = bottomColor;
        }

        public TrackInfo(string trackName)
        {
            TrackName = trackName;
        }

        public TrackInfo(TrackInfo origTrack)
            : this (origTrack.TrackName, origTrack)
        {

        }

        //Duplicate copy
        public TrackInfo(string newName, TrackInfo baseTrack)
            : this(newName)
        {
            TrackItems = new TrackItemsObject(baseTrack.TrackItems.Data);
            Vertices = new VertexCollection(baseTrack.Vertices.FileOffset, baseTrack.Vertices.RawData);
            F3DCommands = new F3DEXCommandCollection(baseTrack.F3DCommands.FileOffset, baseTrack.F3DCommands.RawData); ;
            TextureReferences = new List<MK64Image>(baseTrack.TextureReferences); //Don't need to duplicate images
            CommandReferences = new List<DmaAddress>(baseTrack.CommandReferences.Count);
            foreach (DmaAddress dma in baseTrack.CommandReferences)
                CommandReferences.Add(new DmaAddress(dma.GetAsInt()));

            Unknown1 = baseTrack.Unknown1;
            Unknown2 = baseTrack.Unknown2;

            TopColor = baseTrack.TopColor;
            BottomColor = baseTrack.BottomColor;
        }
        
        public override XElement GetAsXML()
        {
            XElement xml = new XElement(TRACK_INFO);

            xml.Add(new XAttribute(TRACK_NAME, TrackName));
            xml.Add(new XAttribute(UNKNOWN_1, Unknown1.ToString()));
            xml.Add(new XAttribute(UNKNOWN_2, Unknown2.ToString()));
            xml.Add(new XAttribute(TOP_COLOR, TopColor.ToArgb().ToString()));
            xml.Add(new XAttribute(BOTTOM_COLOR, BottomColor.ToArgb().ToString()));

            return xml;
        }
        
        public override string ToString()
        {
            return TrackName;
        }

        public override string GetXMLPath()
        {
            return "Tracks/" + TrackName;
        }

        public static void SaveTrackInfo(string fileName, TrackInfo track)
        {
            //Here save all kart information to an external file
            Path.ChangeExtension(fileName, "track");

            //NEED TO HANDLE IF THE FILE EXISTS, THEN UPDATE THE KARTS INSIDE THE FILE!!
            using (var fs = File.Create(fileName))
            {
                using (ZipOutputStream s = new ZipOutputStream(fs))
                {
                    XElement trackXML = track.GetAsXML();

                    s.PutNextEntry(TRACK_INFO);
                    byte[] bytes = Encoding.ASCII.GetBytes(trackXML.ToString());
                    s.Write(bytes, 0, bytes.Length);

                    s.PutNextEntry(TRACK_ITEMS);
                    s.Write(track.TrackItems.Data, 0, track.TrackItems.Data.Length);

                    s.PutNextEntry(VERTICES);
                    s.Write(track.Vertices.RawData, 0, track.Vertices.RawDataSize);

                    s.PutNextEntry(COMMANDS);
                    s.Write(track.F3DCommands.RawData, 0, track.F3DCommands.RawDataSize);
                    
                    s.PutNextEntry(TEXTURES);
                    List<byte> textureBytes = new List<byte>();
                    XElement texturesEl = new XElement(TEXTURES);
                    foreach (MK64Image img in track.TextureReferences)
                        texturesEl.Add(img.GetAsXML(true));
                    textureBytes.AddRange(Encoding.ASCII.GetBytes(texturesEl.ToString()));
                    s.Write(textureBytes.ToArray(), 0, textureBytes.Count);

                    s.PutNextEntry(COMMAND_REFERENCES);
                    bytes = ByteHelper.CombineIntoBytes(track.CommandReferences);
                    s.Write(bytes, 0, bytes.Length);

                }
            }

        }

        public static TrackInfo LoadFromFile(string fileName)
        {
            TrackInfo track;
            
            TrackItemsObject trackItems = null;
            VertexCollection vertices = null;
            F3DEXCommandCollection commands = null;
            List<MK64Image> images = null;
            List<DmaAddress> commandReferences = null;
            string trackName = string.Empty;
            uint unknown1 = 0;
            ushort unknown2 = 0;
            Color topColor = Color.White;
            Color bottomColor = Color.White;

            using (ZipFile zip = ZipFile.Read(fileName))
            {
                XElement trackInfoEl;
                foreach (ZipEntry e in zip)
                {
                    MemoryStream projectStream = new MemoryStream();
                    e.Extract(projectStream);
                    switch (e.FileName)
                    {
                        case TRACK_INFO:
                            trackInfoEl = XElement.Parse(Encoding.ASCII.GetString(projectStream.ToArray()));
                            trackName = trackInfoEl.Attribute(TRACK_NAME).Value.ToString();
                            unknown1 = uint.Parse(trackInfoEl.Attribute(UNKNOWN_1).Value.ToString());
                            unknown2 = ushort.Parse(trackInfoEl.Attribute(UNKNOWN_2).Value.ToString());

                            break;
                        case TRACK_ITEMS:
                            trackItems = new TrackItemsObject(projectStream.ToArray());
                            break;
                        case VERTICES:
                            vertices = new VertexCollection(-1, projectStream.ToArray());
                            break;
                        case COMMANDS:
                            commands = new F3DEXCommandCollection(-1, projectStream.ToArray());
                            break;
                        case TEXTURES:
                            XElement texturesEl = XElement.Parse(Encoding.ASCII.GetString(projectStream.ToArray()));
                            images = new List<MK64Image>();
                            foreach (XElement xml in texturesEl.Elements())
                            {
                                images.Add(new MK64Image(xml));
                            }
                            break;
                        case COMMAND_REFERENCES:
                            byte[] refData = projectStream.ToArray();
                            commandReferences = new List<DmaAddress>();
                            for (int i = 0; i * 4 < refData.Length; i++)
                            {
                                commandReferences.Add(new DmaAddress(ByteHelper.ReadInt(refData, i * 4)));
                            }
                            break;
                    }
                }

                track = new TrackInfo(trackName, trackItems, vertices, commands,
                     images, commandReferences, unknown1, unknown2, topColor, bottomColor);
            }

            return track;
        }

    }
}
