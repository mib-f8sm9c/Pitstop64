using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using Pitstop64.Data;
using Cereal64.Common.Utils.Encoding;
using System.Xml.Linq;
using Cereal64.Common.DataElements;
using Pitstop64.Data.Karts;
using Pitstop64.Data.Courses;
using Pitstop64.Services.Readers;
using Pitstop64.Data.Text;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.DataElements.Encoding;

namespace Pitstop64.Services.Hub
{
    //Serves as a place to reference important N64DataElements, as well as resources that have
    // been externally added in to help keep track of them.
    public class MarioKart64ElementHub : RomItem
    {
        private const string SELECTED_KARTS = "selectedKarts";

        private const string TRACKS = "tracks";
        private const string TRACK_NAMES = "trackName";

        private const string SELECTED_TRACKS = "selectedTracks";

        private const string TRACKS_GRAPHICS_REFRENCE_BLOCK = "trackGraphicsReferenceBlock";

        private const string KARTS_GRAPHICS_REFERENCE_BLOCK = "kartGraphicsReferenceBlock";
        private const string KARTS_PORTRAITS_REFERENCE_TABLE = "kartPortraitsReferenceTable";

        private const string TURN_PALETTE_BLOCK = "turnPaletteBlock";
        private const string SPIN_PALETTE_BLOCK = "spinPaletteBlock";

        private const string TEXT_BANK = "textBank";
        private const string TEXT_REFERENCE = "textReference";

        private const string OFFSET = "offset";

        private const string NEW_ELEMENT_OFFSET = "newElementOffset";

        private static MarioKart64ElementHub _instance = null;
        private static object syncObject = new object();

        public static MarioKart64ElementHub Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (syncObject)
                    {
                        if (_instance == null)
                            _instance = new MarioKart64ElementHub();
                    }
                }
                return _instance;
            }
        }

        public List<KartInfo> Karts { get; private set; }
        public KartInfo[] SelectedKarts { get; private set; }
        public List<KartPaletteBlock> TurnKartPaletteBlocks { get; private set; }
        public List<KartPaletteBlock> SpinKartPaletteBlocks { get; private set; }
        public List<TrackData> Tracks { get; private set; }
        public TrackData[] SelectedTracks { get; private set; }

        public TextureHub TextureHub { get; private set; }

        //public TrackGraphicsReferenceBlock TrackGraphicsBlock { get; set; }

        public KartGraphicsReferenceBlock KartGraphicsBlock { get; set; }
        public KartPortraitTable KartPortraitsTable { get; set; }
        public TrackDataReferenceBlock TrackDataBlock { get; set; }

        //NOTE: THIS GUY ISN"T GETTING SAVED OR LOADED~!!!
        public TextBank TextBank { get; set; }

        public int NewElementOffset { get; private set; }

        private XElement _loadedXml;

        private const int BASE_FILE_END_OFFSET = 0xBF3C70;

        private MarioKart64ElementHub()
        {
            Karts = new List<KartInfo>();
            SelectedKarts = new KartInfo[8];
            TurnKartPaletteBlocks = new List<KartPaletteBlock>();
            SpinKartPaletteBlocks = new List<KartPaletteBlock>();
            Tracks = new List<TrackData>();
            SelectedTracks = new TrackData[MarioKartRomInfo.TrackCount];
            TextureHub = new TextureHub();
            NewElementOffset = BASE_FILE_END_OFFSET;
        }

        public MarioKart64ElementHub(XElement xml)
        {
            Karts = new List<KartInfo>();
            SelectedKarts = new KartInfo[8];
            TurnKartPaletteBlocks = new List<KartPaletteBlock>();
            SpinKartPaletteBlocks = new List<KartPaletteBlock>();
            Tracks = new List<TrackData>();
            SelectedTracks = new TrackData[MarioKartRomInfo.TrackCount];
            TextureHub = new TextureHub();

            _instance = this;

            _loadedXml = xml; //Actually load the xml data at a later date
        }

        public void AdvanceNewElementOffset(N64DataElement element)
        {
            NewElementOffset += element.RawDataSize;
        }

        public void LoadFromXML()
        {
            //TextBank/TextReferences - Only use the offset currently being used
            //KartReference - Only use the names of the karts selected
            //MIOBlocks/TKMK00Bocks - Offsets for each one
            //Karts - Full listing of information

            //Elements should already have been cleared
            //ClearElements();

            NewElementOffset = int.Parse(_loadedXml.Attribute(NEW_ELEMENT_OFFSET).Value);

            int offset;

            foreach (XElement element in _loadedXml.Elements())
            {
                switch (element.Name.ToString())
                {
                    case TURN_PALETTE_BLOCK:
                        foreach (XElement el in element.Elements())
                        {
                            offset = int.Parse(el.Value);
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(offset))
                            {
                                int paletteOffset = offset;
                                List<Palette> palettes = new List<Palette>();

                                for (int i = 0; i < 84; i++) //Make not hardcoded later
                                {
                                    palettes.Add((Palette)RomProject.Instance.Files[0].GetElementAt(paletteOffset));
                                    paletteOffset += 0x40 * 2;
                                }

                                KartPaletteBlock block = new KartPaletteBlock(offset, palettes);
                                TurnKartPaletteBlocks.Add(block);
                            }
                        }
                        break;
                    case SPIN_PALETTE_BLOCK:
                        foreach (XElement el in element.Elements())
                        {
                            offset = int.Parse(el.Value);
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(offset))
                            {
                                int paletteOffset = offset;
                                List<Palette> palettes = new List<Palette>();

                                for (int i = 0; i < 80; i++) //Make not hardcoded later
                                {
                                    palettes.Add((Palette)RomProject.Instance.Files[0].GetElementAt(paletteOffset));
                                    paletteOffset += 0x40 * 2;
                                }

                                KartPaletteBlock block = new KartPaletteBlock(offset, palettes);
                                SpinKartPaletteBlocks.Add(block);
                            }
                        }
                        break;
                    //case TEXT_BANK:
                    //    offset = int.Parse(element.Value);
                    //    if (RomProject.Instance.Files[0].HasElementAt(offset))
                    //    {
                    //        N64DataElement dataElement = RomProject.Instance.Files[0].GetElementAt(offset);
                    //        if (dataElement is TextBankBlock)
                    //            TextBank = (TextBankBlock)dataElement;
                    //    }
                    //    break;
                    //case TEXT_REFERENCE:
                    //    offset = int.Parse(element.Value);
                    //    if (RomProject.Instance.Files[0].HasElementAt(offset))
                    //    {
                    //        N64DataElement dataElement = RomProject.Instance.Files[0].GetElementAt(offset);
                    //        if (dataElement is TextReferenceBlock)
                    //            TextReference = (TextReferenceBlock)dataElement;
                    //    }
                    //    break;
                    case KARTS_GRAPHICS_REFERENCE_BLOCK:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementAt(offset))
                        {
                            N64DataElement dataElement = RomProject.Instance.Files[0].GetElementAt(offset);
                            if (dataElement is KartGraphicsReferenceBlock)
                            {
                                KartGraphicsBlock = (KartGraphicsReferenceBlock)dataElement;
                                //KartReader.LoadKartGraphicDmaReferences(KartGraphicsBlock);
                            }
                        }
                        break;
                    case KARTS_PORTRAITS_REFERENCE_TABLE:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementAt(offset))
                        {
                            N64DataElement dataElement = RomProject.Instance.Files[0].GetElementAt(offset);
                            if (dataElement is KartPortraitTable)
                            {
                                KartPortraitsTable = (KartPortraitTable)dataElement;
                                //KartReader.LoadKartPortraitDmaReferences(KartPortraitsTable);
                            }
                        }
                        break;
                    //case KARTS:
                    //    foreach (XElement kart in element.Elements())
                    //    {
                    //        KartInfo newKart = new KartInfo(kart);
                    //        Karts.Add(newKart);
                    //    }
                    //    break;
                    case SELECTED_KARTS:
                        int kartIndex = 0;
                        foreach(XElement selKart in element.Elements())
                        {
                            KartInfo selectedKart = Karts.SingleOrDefault(k => k.KartName == selKart.Name);
                            if (selectedKart != null)
                            {
                                SelectedKarts[kartIndex] = selectedKart;
                            }
                            kartIndex++;
                        }
                        break;
                    case TextureHub.TEXTURE_HUB:
                        TextureHub.LoadReferencesFromXML(element);
                        break;
                }
            }
        }

        public override XElement GetAsXML()
        {
            XElement xml = base.GetAsXML();

            xml.Add(new XAttribute(NEW_ELEMENT_OFFSET, NewElementOffset));

            //TextBank/TextReferences - Only use the offset currently being used
            //KartReference - Only use the names of the karts selected
            //MIOBlocks/TKMK00Bocks - Offsets for each one
            //Karts - Full listing of information

            ////Text bank
            //XElement newElement = new XElement(TEXT_BANK);
            //newElement.Value = TextBank.FileOffset.ToString();
            //xml.Add(newElement);

            ////Text reference
            //newElement = new XElement(TEXT_REFERENCE);
            //newElement.Value = TextReference.FileOffset.ToString();
            //xml.Add(newElement);

            XElement newElement = new XElement(TURN_PALETTE_BLOCK);
            foreach (KartPaletteBlock block in TurnKartPaletteBlocks)
                newElement.Add(new XElement(OFFSET, block.FileOffset.ToString()));
            xml.Add(newElement);

            newElement = new XElement(SPIN_PALETTE_BLOCK);
            foreach (KartPaletteBlock block in SpinKartPaletteBlocks)
                newElement.Add(new XElement(OFFSET, block.FileOffset.ToString()));
            xml.Add(newElement);

            //And finally, the selected karts
            newElement = new XElement(SELECTED_KARTS);
            foreach (KartInfo kart in SelectedKarts)
                newElement.Add(new XElement(kart.KartName));
            xml.Add(newElement);

            //Kart graphics reference
            newElement = new XElement(KARTS_GRAPHICS_REFERENCE_BLOCK);
            newElement.Value = KartGraphicsBlock.FileOffset.ToString();
            xml.Add(newElement);

            //Kart portraits reference
            newElement = new XElement(KARTS_PORTRAITS_REFERENCE_TABLE);
            newElement.Value = KartPortraitsTable.FileOffset.ToString();
            xml.Add(newElement);

            xml.Add(TextureHub.GetAsXML());

            return xml;
        }

        public void ClearElements()
        {
            Karts.Clear();
            Array.Clear(SelectedKarts, 0, SelectedKarts.Length);
            TurnKartPaletteBlocks.Clear();
            SpinKartPaletteBlocks.Clear();
            Tracks.Clear();
            Array.Clear(SelectedTracks, 0, SelectedTracks.Length);
            KartGraphicsBlock = null;
            //TextBank = null;
            //TextReference = null;
            TextureHub.ClearTextureData();
        }

        public void SaveKartInfo()
        {
            if (KartGraphicsBlock == null)
                return;

            //These hold the palette blocks associated with each animation
            Dictionary<KartAnimationSeries, KartPaletteBlock> TurnPaletteBlocks =
                new Dictionary<KartAnimationSeries, KartPaletteBlock>();
            Dictionary<KartAnimationSeries, KartPaletteBlock> SpinPaletteBlocks =
                new Dictionary<KartAnimationSeries, KartPaletteBlock>();
            int turnPaletteBlockIndex = 0;
            int spinPaletteBlockIndex = 0;

            for (int i = 0; i < MarioKart64ElementHub.Instance.SelectedKarts.Length; i++)
            {
                KartInfo kart = MarioKart64ElementHub.Instance.SelectedKarts[i];

                //Save the main palette
                if (kart.KartImages.ImagePalette.FileOffset == -1)
                {
                    kart.KartImages.ImagePalette.FileOffset = NewElementOffset;
                    AdvanceNewElementOffset(kart.KartImages.ImagePalette);
                    RomProject.Instance.Files[0].AddElement(kart.KartImages.ImagePalette);
                }

                KartGraphicsBlock.CharacterPaletteReferences[i] = new DmaAddress(0x0F, kart.KartImages.ImagePalette.FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
                KartGraphicsBlock.CharacterPaletteReferences[i].ReferenceElement = kart.KartImages.ImagePalette;

                //Save the kart palettes in BLOCKS!
                //but first, assign each unique animation its own PaletteBlock, adding new ones as necessary

                //Backwards, so the order is preserved
                for (int h = kart.KartAnimations.Count - 1; h >= 0; h--)
                {
                    KartAnimationSeries anim = kart.KartAnimations[h];
                    if (anim.IsTurnAnim)
                    {
                        if (!TurnPaletteBlocks.ContainsKey(anim))
                        {
                            while (this.TurnKartPaletteBlocks.Count <= turnPaletteBlockIndex)
                            {
                                byte[] newPaletteBlockData = new byte[0x40 * 2 * 20 * 4];
                                KartPaletteBlock block = new KartPaletteBlock(this.NewElementOffset, newPaletteBlockData);
                                foreach (Palette palette in block.Palettes)
                                    RomProject.Instance.Files[0].AddElement(palette);
                                this.AdvanceNewElementOffset(block);
                                this.TurnKartPaletteBlocks.Add(block);
                            }

                            TurnPaletteBlocks.Add(anim, this.TurnKartPaletteBlocks[turnPaletteBlockIndex]);
                            turnPaletteBlockIndex++;

                            byte[] testingBytes = anim.GenerateKartAnimationPaletteData(
                                kart.KartImages, true);

                            TurnPaletteBlocks[anim].RawData = testingBytes;
                        }
                    }

                    if (anim.IsSpinAnim)
                    {
                        if (!SpinPaletteBlocks.ContainsKey(anim))
                        {
                            while (this.SpinKartPaletteBlocks.Count <= spinPaletteBlockIndex)
                            {
                                byte[] newPaletteBlockData = new byte[0x40 * 2 * 20 * 4];
                                KartPaletteBlock block = new KartPaletteBlock(this.NewElementOffset, newPaletteBlockData);
                                foreach (Palette palette in block.Palettes)
                                    RomProject.Instance.Files[0].AddElement(palette);
                                this.AdvanceNewElementOffset(block);
                                this.SpinKartPaletteBlocks.Add(block);
                            }

                            SpinPaletteBlocks.Add(anim, this.SpinKartPaletteBlocks[spinPaletteBlockIndex]);
                            spinPaletteBlockIndex++;

                            SpinPaletteBlocks[anim].RawData = anim.GenerateKartAnimationPaletteData(
                                kart.KartImages, false);
                        }
                    }
                }

                List<int> setAnimPaletteBlock = new List<int>();

                for (int j = 0; j < KartGraphicsBlock.CharacterTurnReferences[i].Length; j++)
                {
                    int animFlag;
                    int frameIndex; //Theres a function for this in KartReader?
                    bool isTurnAnim = true;

                    if (j < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT)
                    {
                        animFlag = (int)Math.Round(Math.Pow(2, j / KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT));
                        frameIndex = j - (j / KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT;

                        //The last 14 values of the turn animation are from the spin one, actually
                        if (frameIndex >= KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT)
                        {
                            animFlag <<= 9; //Make it spin anim, not turn anim
                            frameIndex -= 15;
                            isTurnAnim = false; //Don't do palette block stuff for this one
                        }
                    }
                    else
                    {
                        animFlag = (int)Math.Round(Math.Pow(2, (j - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) / KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT));
                        frameIndex = j - (KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT * KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT) - ((j - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) / KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT) * KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT;
                        isTurnAnim = false;
                    }

                    KartAnimationSeries anim = kart.KartAnimations.FirstOrDefault(f => (f.KartAnimationType & animFlag) != 0);
                    if (anim != null)
                    {
                        //Need to replace animIndex with GetIndexfor(animIndex), but we need a better spin/turn/crash test
                        string imageName;
                        if (anim.IsTurnAnim)
                            imageName = anim.OrderedImageNames[anim.GetImageIndexForTurnFrame(frameIndex)];
                        else //if (anim.IsSpinAnim)
                            imageName = anim.OrderedImageNames[anim.GetImageIndexForSpinFrame(frameIndex)];

                        MK64Image mkImage = kart.KartImages.Images[imageName].Images[0];

                        //Save the image
                        if (mkImage.TextureOffset == -1)
                        {
                            //It has to be an MIO0 block
                            foreach (MK64Image editThisImage in kart.KartImages.Images[imageName].Images)
                            {
                                editThisImage.TextureBlockOffset = 0;
                                editThisImage.TextureOffset = NewElementOffset;
                            }
                            mkImage.ImageReference.Texture.FileOffset = 0;
                            MIO0Block newBlock = new MIO0Block(NewElementOffset, mkImage.ImageReference.Texture.RawData);
                            AdvanceNewElementOffset(newBlock);
                            RomProject.Instance.Files[0].AddElement(newBlock);

                        }

                        DmaAddress address = new DmaAddress(0x0F, mkImage.TextureOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
                        MIO0Block block = (MIO0Block)RomProject.Instance.Files[0].GetElementAt(mkImage.TextureOffset);
                        address.ReferenceElement = block;
                        KartGraphicsBlock.CharacterTurnReferences[i][j] = address;

                        int animIndex;
                        if (animFlag == 0)
                            animIndex = 0;
                        else
                            animIndex = (int)Math.Round(Math.Log(animFlag, 2));

                        //inverse the animation index
                        if (animIndex < 9)
                            animIndex = 8 - animIndex;
                        else
                            animIndex = (8 - (animIndex - 9)) + 9;

                        if(!setAnimPaletteBlock.Contains(animIndex))
                        {
                            if (isTurnAnim)
                            {
                                KartGraphicsBlock.WheelPaletteReferences[i][animIndex] = new DmaAddress(0x0F, TurnPaletteBlocks[anim].FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
                            }
                            else
                            {
                                KartGraphicsBlock.WheelPaletteReferences[i][animIndex] = new DmaAddress(0x0F, SpinPaletteBlocks[anim].FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
                            }
                            setAnimPaletteBlock.Add(animIndex);
                        }
                    }
                }

                for (int j = 0; j < KartGraphicsBlock.CharacterCrashReferences[i].Length; j++)
                {
                    KartAnimationSeries anim = kart.KartAnimations.FirstOrDefault(f => (f.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.Crash) != 0);
                    if (anim != null)
                    {
                        MK64Image mkImage = kart.KartImages.Images[anim.OrderedImageNames[anim.GetImageIndexForCrashFrame(j)]].Images[0];

                        if (mkImage.TextureOffset == -1)
                        {
                            foreach (MK64Image editThisImage in kart.KartImages.Images[anim.OrderedImageNames[anim.GetImageIndexForCrashFrame(j)]].Images)
                            {
                                editThisImage.TextureBlockOffset = 0;
                                editThisImage.TextureOffset = NewElementOffset;
                            }
                            mkImage.ImageReference.Texture.FileOffset = 0;
                            MIO0Block newBlock = new MIO0Block(NewElementOffset, mkImage.ImageReference.Texture.RawData);
                            AdvanceNewElementOffset(newBlock);
                            RomProject.Instance.Files[0].AddElement(newBlock);
                        }

                        MIO0Block block = (MIO0Block)RomProject.Instance.Files[0].GetElementAt(mkImage.TextureOffset);

                        //Save the image
                        if (block.FileOffset == -1)
                        {
                            block.FileOffset = NewElementOffset;
                            AdvanceNewElementOffset(block);
                            RomProject.Instance.Files[0].AddElement(block);
                        }

                        DmaAddress address = new DmaAddress(0x0F, block.FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
                        address.ReferenceElement = block;
                        KartGraphicsBlock.CharacterCrashReferences[i][j] = address;
                    }
                }

                for (int j = 0; j < kart.KartPortraits.Count; j++)
                {
                    if (kart.KartPortraits[j].TextureOffset == -1)
                    {
                        kart.KartPortraits[j].TextureBlockOffset = 0;
                        kart.KartPortraits[j].TextureOffset = NewElementOffset;
                        kart.KartPortraits[j].ImageReference.Texture.FileOffset = 0;
                        MIO0Block newBlock = new MIO0Block(NewElementOffset, kart.KartPortraits[j].ImageReference.Texture.RawData);
                        AdvanceNewElementOffset(newBlock);
                        RomProject.Instance.Files[0].AddElement(newBlock);
                    }

                    KartPortraitTableEntry entry = new KartPortraitTableEntry(kart.KartPortraits[j].TextureOffset, kart.KartPortraits[j]);
                    KartPortraitsTable.Entries[i][j] = entry;
                }

                N64DataElement tkmk;
                if (RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.CharacterNameplateReference[i]) &&
                    (tkmk = RomProject.Instance.Files[0].GetElementAt(MarioKartRomInfo.CharacterNameplateReference[i])) is TKMK00Block)
                {
                    TKMK00Block oldTkmk = (TKMK00Block)tkmk;
                    oldTkmk.ImageAlphaColor = kart.KartNamePlate.TKMKAlphaColor;
                    oldTkmk.SetImage(kart.KartNamePlate.Image);
                }
            }
        }

        public void SaveTrackInfo()
        {
            if (TrackDataBlock == null)
                return;

            for (int i = 0; i < MarioKart64ElementHub.Instance.SelectedTracks.Length; i++)
            {
                TrackData track = MarioKart64ElementHub.Instance.SelectedTracks[i];

                //Save the main palette
            //    if (kart.KartImages.ImagePalette.FileOffset == -1)
            //    {
            //        kart.KartImages.ImagePalette.FileOffset = NewElementOffset;
            //        AdvanceNewElementOffset(kart.KartImages.ImagePalette);
            //        RomProject.Instance.Files[0].AddElement(kart.KartImages.ImagePalette);
            //    }

            //    KartGraphicsBlock.CharacterPaletteReferences[i] = new DmaAddress(0x0F, kart.KartImages.ImagePalette.FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
            //    KartGraphicsBlock.CharacterPaletteReferences[i].ReferenceElement = kart.KartImages.ImagePalette;

            //    //Save the kart palettes in BLOCKS!
            //    //but first, assign each unique animation its own PaletteBlock, adding new ones as necessary

            //    //Backwards, so the order is preserved
            //    for (int h = kart.KartAnimations.Count - 1; h >= 0; h--)
            //    {
            //        KartAnimationSeries anim = kart.KartAnimations[h];
            //        if (anim.IsTurnAnim)
            //        {
            //            if (!TurnPaletteBlocks.ContainsKey(anim))
            //            {
            //                while (this.TurnKartPaletteBlocks.Count <= turnPaletteBlockIndex)
            //                {
            //                    byte[] newPaletteBlockData = new byte[0x40 * 2 * 20 * 4];
            //                    KartPaletteBlock block = new KartPaletteBlock(this.NewElementOffset, newPaletteBlockData);
            //                    foreach (Palette palette in block.Palettes)
            //                        RomProject.Instance.Files[0].AddElement(palette);
            //                    this.AdvanceNewElementOffset(block);
            //                    this.TurnKartPaletteBlocks.Add(block);
            //                }

            //                TurnPaletteBlocks.Add(anim, this.TurnKartPaletteBlocks[turnPaletteBlockIndex]);
            //                turnPaletteBlockIndex++;

            //                byte[] testingBytes = anim.GenerateKartAnimationPaletteData(
            //                    kart.KartImages, true);

            //                TurnPaletteBlocks[anim].RawData = testingBytes;
            //            }
            //        }

            //        if (anim.IsSpinAnim)
            //        {
            //            if (!SpinPaletteBlocks.ContainsKey(anim))
            //            {
            //                while (this.SpinKartPaletteBlocks.Count <= spinPaletteBlockIndex)
            //                {
            //                    byte[] newPaletteBlockData = new byte[0x40 * 2 * 20 * 4];
            //                    KartPaletteBlock block = new KartPaletteBlock(this.NewElementOffset, newPaletteBlockData);
            //                    foreach (Palette palette in block.Palettes)
            //                        RomProject.Instance.Files[0].AddElement(palette);
            //                    this.AdvanceNewElementOffset(block);
            //                    this.SpinKartPaletteBlocks.Add(block);
            //                }

            //                SpinPaletteBlocks.Add(anim, this.SpinKartPaletteBlocks[spinPaletteBlockIndex]);
            //                spinPaletteBlockIndex++;

            //                SpinPaletteBlocks[anim].RawData = anim.GenerateKartAnimationPaletteData(
            //                    kart.KartImages, false);
            //            }
            //        }
            //    }

            //    List<int> setAnimPaletteBlock = new List<int>();

            //    for (int j = 0; j < KartGraphicsBlock.CharacterTurnReferences[i].Length; j++)
            //    {
            //        int animFlag;
            //        int frameIndex; //Theres a function for this in KartReader?
            //        bool isTurnAnim = true;

            //        if (j < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT)
            //        {
            //            animFlag = (int)Math.Round(Math.Pow(2, j / KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT));
            //            frameIndex = j - (j / KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT;

            //            //The last 14 values of the turn animation are from the spin one, actually
            //            if (frameIndex >= KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT)
            //            {
            //                animFlag <<= 9; //Make it spin anim, not turn anim
            //                frameIndex -= 15;
            //                isTurnAnim = false; //Don't do palette block stuff for this one
            //            }
            //        }
            //        else
            //        {
            //            animFlag = (int)Math.Round(Math.Pow(2, (j - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) / KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT));
            //            frameIndex = j - (KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT * KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT) - ((j - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) / KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT) * KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT;
            //            isTurnAnim = false;
            //        }

            //        KartAnimationSeries anim = kart.KartAnimations.FirstOrDefault(f => (f.KartAnimationType & animFlag) != 0);
            //        if (anim != null)
            //        {
            //            //Need to replace animIndex with GetIndexfor(animIndex), but we need a better spin/turn/crash test
            //            string imageName;
            //            if (anim.IsTurnAnim)
            //                imageName = anim.OrderedImageNames[anim.GetImageIndexForTurnFrame(frameIndex)];
            //            else //if (anim.IsSpinAnim)
            //                imageName = anim.OrderedImageNames[anim.GetImageIndexForSpinFrame(frameIndex)];

            //            ImageMIO0Block block = kart.KartImages.Images[imageName].GetEncodedData(kart.KartImages.ImagePalette);

            //            //Save the image
            //            if (block.FileOffset == -1)
            //            {
            //                block.FileOffset = NewElementOffset;
            //                AdvanceNewElementOffset(block);
            //                RomProject.Instance.Files[0].AddElement(block);
            //            }

            //            DmaAddress address = new DmaAddress(0x0F, block.FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
            //            address.ReferenceElement = block;
            //            KartGraphicsBlock.CharacterTurnReferences[i][j] = address;

            //            int animIndex;
            //            if (animFlag == 0)
            //                animIndex = 0;
            //            else
            //                animIndex = (int)Math.Round(Math.Log(animFlag, 2));

            //            //inverse the animation index
            //            if (animIndex < 9)
            //                animIndex = 8 - animIndex;
            //            else
            //                animIndex = (8 - (animIndex - 9)) + 9;

            //            if (!setAnimPaletteBlock.Contains(animIndex))
            //            {
            //                if (isTurnAnim)
            //                {
            //                    KartGraphicsBlock.WheelPaletteReferences[i][animIndex] = new DmaAddress(0x0F, TurnPaletteBlocks[anim].FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
            //                }
            //                else
            //                {
            //                    KartGraphicsBlock.WheelPaletteReferences[i][animIndex] = new DmaAddress(0x0F, SpinPaletteBlocks[anim].FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
            //                }
            //                setAnimPaletteBlock.Add(animIndex);
            //            }
            //        }
            //    }

            //    for (int j = 0; j < KartGraphicsBlock.CharacterCrashReferences[i].Length; j++)
            //    {
            //        KartAnimationSeries anim = kart.KartAnimations.FirstOrDefault(f => (f.KartAnimationType & (int)KartAnimationSeries.KartAnimationTypeFlag.Crash) != 0);
            //        if (anim != null)
            //        {
            //            ImageMIO0Block block = kart.KartImages.Images[anim.OrderedImageNames[anim.GetImageIndexForCrashFrame(j)]].GetEncodedData(kart.KartImages.ImagePalette);
            //            DmaAddress address = new DmaAddress(0x0F, block.FileOffset - KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET);
            //            address.ReferenceElement = block;
            //            KartGraphicsBlock.CharacterCrashReferences[i][j] = address;
            //        }
            //    }

            //    for (int j = 0; j < kart.KartPortraits.Count; j++)
            //    {
            //        if (kart.KartPortraits[j].FileOffset == -1)
            //        {
            //            kart.KartPortraits[j].FileOffset = MarioKart64ElementHub.Instance.NewElementOffset;
            //            MarioKart64ElementHub.Instance.AdvanceNewElementOffset(kart.KartPortraits[j]);
            //            RomProject.Instance.Files[0].AddElement(kart.KartPortraits[j]);
            //        }

            //        KartPortraitTableEntry entry = new KartPortraitTableEntry(kart.KartPortraits[j].FileOffset, kart.KartPortraits[j]);
            //        KartPortraitsTable.Entries[i][j] = entry;
            //    }

            //    N64DataElement tkmk;
            //    if (RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.CharacterNameplateReference[i]) &&
            //        (tkmk = RomProject.Instance.Files[0].GetElementAt(MarioKartRomInfo.CharacterNameplateReference[i])) is TKMK00Block)
            //    {
            //        TKMK00Block oldTkmk = (TKMK00Block)tkmk;
            //        oldTkmk.ImageAlphaColor = kart.KartNamePlate.ImageAlphaColor;
            //        oldTkmk.SetImage(kart.KartNamePlate.Image);
            //    }

            }
        }

        public override string GetXMLPath()
        {
            return "MarioKartElementHub";
        }
    }
}
