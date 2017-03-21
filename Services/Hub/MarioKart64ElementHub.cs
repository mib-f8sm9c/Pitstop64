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
using Pitstop64.Data.Tracks;
using Pitstop64.Services.Readers;
using Pitstop64.Data.Text;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Common.DataElements.Encoding;
using Pitstop64.Data.Tracks.Compressed;
using Pitstop64.Data.Textures;

namespace Pitstop64.Services.Hub
{
    //Serves as a place to reference important N64DataElements, as well as resources that have
    // been externally added in to help keep track of them.
    public class MarioKart64ElementHub : RomItem
    {
        private const string KARTS = "Karts";
        private const string SELECTED_KARTS = "selectedKarts";

        private const string TRACKS = "tracks";
        private const string SELECTED_TRACKS = "selectedTracks";
        
        private const string TRACKS_GRAPHICS_REFRENCE_BLOCK = "trackGraphicsReferenceBlock";

        private const string KARTS_GRAPHICS_REFERENCE_BLOCK = "kartGraphicsReferenceBlock";
        private const string KARTS_PORTRAITS_REFERENCE_TABLE = "kartPortraitsReferenceTable";
        private const string KARTS_WEIGHT_TABLE = "kartWeightTable";
        private const string KARTS_SCALE_TABLE = "kartScaleTable";
        private const string KARTS_INFO_TABLE = "kartInfoTable";

        private const string COMMON_TEXTURE_BLOCK = "commonTextureBlock";

        private const string TURN_PALETTE_BLOCK = "turnPaletteBlock";
        private const string SPIN_PALETTE_BLOCK = "spinPaletteBlock";

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

        public List<CompressedTrack> Tracks { get; private set; }
        public CompressedTrack[] SelectedTracks { get; private set; }
        public TrackSkyTable TrackSkyColorTable { get; set; }

        public TextureHub TextureHub { get; private set; }

        public KartGraphicsReferenceBlock KartGraphicsBlock { get; set; }
        public KartPortraitTable KartPortraitsTable { get; set; }
        public KartWeightTable KartWeightsTable { get; set; }
        public KartScaleTable KartScalingTable { get; set; }
        public KartInformationBlock KartStatsTable { get; set; }
        public TrackDataReferenceBlock TrackTable { get; set; }
        public CommonTexturesBlock CommonTextureBlock { get; set; }

        public TextBank TextBank { get; set; }

        public int NewElementOffset { get; private set; }

        private XElement _loadedXml;

        private const int BASE_FILE_END_OFFSET = 0xBE9160;

        private MarioKart64ElementHub()
        {
            Karts = new List<KartInfo>();
            SelectedKarts = new KartInfo[8];
            TurnKartPaletteBlocks = new List<KartPaletteBlock>();
            SpinKartPaletteBlocks = new List<KartPaletteBlock>();
            Tracks = new List<CompressedTrack>();
            SelectedTracks = new CompressedTrack[MarioKartRomInfo.TrackCount];
            TextureHub = new TextureHub();
            NewElementOffset = BASE_FILE_END_OFFSET;
        }

        public MarioKart64ElementHub(XElement xml)
        {
            Karts = new List<KartInfo>();
            SelectedKarts = new KartInfo[8];
            TurnKartPaletteBlocks = new List<KartPaletteBlock>();
            SpinKartPaletteBlocks = new List<KartPaletteBlock>();
            Tracks = new List<CompressedTrack>();
            SelectedTracks = new CompressedTrack[MarioKartRomInfo.TrackCount];
            TextureHub = new TextureHub();

            _instance = this;

            _loadedXml = xml; //Actually load the xml data at a later date (WRONG)
            LoadFromXML();
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

            //Before we start, load up all saved karts and tracks
            ProgressService.SetMessage("Loading Kart Resources");
            foreach (RomItem item in RomProject.Instance.Items)
            {
                //If the same name kart hasn't been loaded yet
                if (item is KartInfo && Karts.FirstOrDefault(k => k.KartName == ((KartInfo)item).KartName) == null)
                {
                    this.Karts.Add((KartInfo)item);
                }
                else if (item is CompressedTrack && Tracks.FirstOrDefault(t => t.TrackName == ((CompressedTrack)item).TrackName) == null)
                {
                    this.Tracks.Add((CompressedTrack)item);
                }
            }

            //Also the text bank is all elements, so we don't need an xml in here for it
            ProgressService.SetMessage("Loading Text Blocks");
            N64DataElement textRefEl, textBlockEl;
            if (RomProject.Instance.Files[0].HasElementExactlyAt(TextReferenceBlock.TEXT_REFERENCE_SECTION_1, out textRefEl) &&
                RomProject.Instance.Files[0].HasElementExactlyAt(TextBankBlock.TEXT_BLOCK_START, out textBlockEl))
            {
                TextReferenceBlock refBlock = (TextReferenceBlock)textRefEl;
                TextBankBlock bankBlock = (TextBankBlock)textBlockEl;

                TextBank = new TextBank(bankBlock, refBlock, true);
            }

            int offset;
            int count = 0;
            int fullCount = _loadedXml.Elements().Count();
            N64DataElement n64Element;

            foreach (XElement element in _loadedXml.Elements())
            {
                ProgressService.SetMessage(string.Format("Storing Special Elements {0:0.0}%", (double)count / fullCount));
                switch (element.Name.ToString())
                {
                    case TURN_PALETTE_BLOCK:
                        foreach (XElement el in element.Elements())
                        {
                            offset = int.Parse(el.Value);
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                            {
                                int paletteOffset = offset;
                                List<Palette> palettes = new List<Palette>();
                                palettes.Add((Palette)n64Element);
                                paletteOffset += 0x40 * 2;

                                for (int i = 1; i < 84; i++) //Make not hardcoded later
                                {
                                    if (!RomProject.Instance.Files[0].HasElementAt(paletteOffset, out n64Element))
                                        throw new Exception();

                                    palettes.Add((Palette)n64Element);
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
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                            {
                                int paletteOffset = offset;
                                List<Palette> palettes = new List<Palette>();
                                palettes.Add((Palette)n64Element);
                                paletteOffset += 0x40 * 2;

                                for (int i = 1; i < 80; i++) //Make not hardcoded later
                                {
                                    if (!RomProject.Instance.Files[0].HasElementAt(paletteOffset, out n64Element))
                                        throw new Exception();

                                    palettes.Add((Palette)n64Element);
                                    paletteOffset += 0x40 * 2;
                                }

                                KartPaletteBlock block = new KartPaletteBlock(offset, palettes);
                                SpinKartPaletteBlocks.Add(block);
                            }
                        }
                        break;
                    case KARTS_GRAPHICS_REFERENCE_BLOCK:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                        {
                            if (n64Element is KartGraphicsReferenceBlock)
                            {
                                KartGraphicsBlock = (KartGraphicsReferenceBlock)n64Element;
                                //KartReader.LoadKartGraphicDmaReferences(KartGraphicsBlock);
                            }
                        }
                        break;
                    case KARTS_PORTRAITS_REFERENCE_TABLE:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                        {
                            if (n64Element is KartPortraitTable)
                            {
                                KartPortraitsTable = (KartPortraitTable)n64Element;
                                //KartReader.LoadKartPortraitDmaReferences(KartPortraitsTable);
                            }
                        }
                        break;
                    case KARTS_WEIGHT_TABLE:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                        {
                            if (n64Element is KartWeightTable)
                            {
                                KartWeightsTable = (KartWeightTable)n64Element;
                            }
                        }
                        break;
                    case KARTS_SCALE_TABLE:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                        {
                            if (n64Element is KartScaleTable)
                            {
                                KartScalingTable = (KartScaleTable)n64Element;
                            }
                        }
                        break;
                    case KARTS_INFO_TABLE:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                        {
                            if (n64Element is KartInformationBlock)
                            {
                                KartStatsTable = (KartInformationBlock)n64Element;
                            }
                        }
                        break;
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
                    case SELECTED_TRACKS:
                        int trackIndex = 0;
                        foreach (XElement selTrack in element.Elements())
                        {
                            CompressedTrack selectedTrack = Tracks.SingleOrDefault(k => k.TrackName == selTrack.Name);
                            if (selectedTrack != null)
                            {
                                SelectedTracks[trackIndex] = selectedTrack;
                            }
                            trackIndex++;
                        }
                        break;
                    case TextureHub.TEXTURE_HUB:
                        TextureHub.LoadReferencesFromXML(element);
                        break;
                    case COMMON_TEXTURE_BLOCK:
                        offset = int.Parse(element.Value);
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(offset, out n64Element))
                        {
                            if (n64Element is MIO0Block)
                            {
                                CommonTextureBlock = new CommonTexturesBlock((MIO0Block)n64Element);
                            }
                        }
                        break;
                }
                count++;
            }
        }

        public override XElement GetAsXML()
        {
            XElement xml = base.GetAsXML();

            xml.Add(new XAttribute(NEW_ELEMENT_OFFSET, NewElementOffset));

            //KartReference - Only use the names of the karts selected
            //MIOBlocks/TKMK00Bocks - Offsets for each one
            //Karts - Full listing of information

            XElement newElement = new XElement(TURN_PALETTE_BLOCK);
            foreach (KartPaletteBlock block in TurnKartPaletteBlocks)
                newElement.Add(new XElement(OFFSET, block.FileOffset.ToString()));
            xml.Add(newElement);

            newElement = new XElement(SPIN_PALETTE_BLOCK);
            foreach (KartPaletteBlock block in SpinKartPaletteBlocks)
                newElement.Add(new XElement(OFFSET, block.FileOffset.ToString()));
            xml.Add(newElement);

            //And finally, the selected karts & tracks
            newElement = new XElement(SELECTED_KARTS);
            foreach (KartInfo kart in SelectedKarts)
                newElement.Add(new XElement(kart.KartName));
            xml.Add(newElement);

            newElement = new XElement(SELECTED_TRACKS);
            foreach (CompressedTrack track in SelectedTracks)
                newElement.Add(new XElement(track.TrackName));
            xml.Add(newElement);

            //Kart graphics reference
            newElement = new XElement(KARTS_GRAPHICS_REFERENCE_BLOCK);
            newElement.Value = KartGraphicsBlock.FileOffset.ToString();
            xml.Add(newElement);

            //Kart portraits reference
            newElement = new XElement(KARTS_PORTRAITS_REFERENCE_TABLE);
            newElement.Value = KartPortraitsTable.FileOffset.ToString();
            xml.Add(newElement);

            newElement = new XElement(KARTS_WEIGHT_TABLE);
            newElement.Value = KartWeightsTable.FileOffset.ToString();
            xml.Add(newElement);

            newElement = new XElement(KARTS_SCALE_TABLE);
            newElement.Value = KartScalingTable.FileOffset.ToString();
            xml.Add(newElement);

            newElement = new XElement(KARTS_INFO_TABLE);
            newElement.Value = KartStatsTable.FileOffset.ToString();
            xml.Add(newElement);

            newElement = new XElement(COMMON_TEXTURE_BLOCK);
            newElement.Value = CommonTextureBlock.EncodedData.FileOffset.ToString();
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

            bool miniPortraitIconErrorEncountered = false;

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
                        N64DataElement blockEl;

                        if (!RomProject.Instance.Files[0].HasElementAt(mkImage.TextureOffset, out blockEl))
                            throw new Exception();
                        MIO0Block block = (MIO0Block)blockEl;
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

                        N64DataElement element;
                        if (!RomProject.Instance.Files[0].HasElementExactlyAt(mkImage.TextureOffset, out element))
                            throw new Exception();
                        MIO0Block block = (MIO0Block)element;

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
                if (RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.CharacterNameplateReference[i], out tkmk) && tkmk  is TKMK00Block)
                {
                    TKMK00Block oldTkmk = (TKMK00Block)tkmk;
                    oldTkmk.ImageAlphaColor = kart.KartNamePlate.TKMKAlphaColor;
                    oldTkmk.SetImage(kart.KartNamePlate.Image);
                }

                //Set the mini portrait & icon here!
                MK64Image mkImg;
                if (!miniPortraitIconErrorEncountered && (mkImg = TextureHub.FindImage(TextureNames.KART_MINI_PORTRAIT_NAME_ARRAY[i])) != null &&
                    CommonTextureBlock.SetData(mkImg.TextureBlockOffset, kart.KartMiniPortrait.ImageReference.Texture.RawData) &&
                    CommonTextureBlock.SetData(mkImg.PaletteBlockOffset[0], kart.KartMiniPortrait.ImageReference.BasePalettes[0].RawData) &&
                    (mkImg = TextureHub.FindImage(TextureNames.MINIMAP_KART_NAME_ARRAY[i])) != null &&
                    CommonTextureBlock.SetData(mkImg.TextureBlockOffset, kart.KartMiniIcon.ImageReference.Texture.RawData))
                {
                    //Success message? or error message otherwise?
                }
                else
                {
                    miniPortraitIconErrorEncountered = true;
                }

                //Kart stats
                KartWeightsTable.KartWeights[i] = kart.KartStats.Weight;
                KartScalingTable.KartScales[i] = kart.KartStats.Scale;
                KartStatsTable.SetStatsFromKart(i, kart.KartStats);

                //Kart name
                Data.Text.TextBank.TextType textIndex = (Data.Text.TextBank.TextType)(i + Data.Text.TextBank.TextType.Kart_1_2);
                TextBank.SetText(textIndex, kart.KartName.ToUpper());
            }

            if (miniPortraitIconErrorEncountered)
            {
                //eh?
            }
            else
            {
                //Attempt to save the data
                if (!CommonTextureBlock.TrySave())
                {
                    //Ya blew it
                }
                else
                {
                    //success message?
                }
            }

        }

        public void LoadTrackTextureReferences()
        {
            foreach (CompressedTrack track in Tracks)
            {
                foreach(TrackTextureRef tRef in track.TextureBlock.TextureReferences)
                {
                    //Try to load the image ref if it doesn't have one yet
                    if (tRef.ImageReference == null)
                    {
                        //Check if an image with the file offset exists
                        int offset = (0x00FFFFFF & tRef.RomOffset) + MarioKartRomInfo.TextureBankOffset;
                        MK64Image img;
                        if((img = TextureHub.Images.SingleOrDefault(i => i.TextureOffset == offset)) == null)
                            continue;

                        //Check it's MIO0 encoded, and decodes to the right size
                        if (img.TextureEncoding != MK64Image.MK64ImageEncoding.MIO0 || img.ImageReference.Texture.RawDataSize != tRef.DecompressedSize)
                            continue;

                        //Now set the ref!
                        tRef.ImageReference = img;

                        //Interesting note: if during the saving process an MIO0 is improved in file size, the compressed size might not
                        //   match up correctly.
                    }
                }
            }
        }

        public void SaveTrackInfo()
        {
            if (TrackTable == null || SelectedTracks == null)
                return;

            for (int i = 0; i < SelectedTracks.Length; i++)
            {
                if (SelectedTracks[i] != null)
                    TrackTable.Entries[i].LoadInfoFromTrack(SelectedTracks[i]);

                //When editing the track image/name is allowed, that will go here

                //sky colors
                TrackSkyColorTable.TopColors[i] = SelectedTracks[i].TopSkyColor;
                TrackSkyColorTable.BottomColors[i] = SelectedTracks[i].BottomSkyColor;
            }
        }

        public bool ConvertNewTrack(TrackInfo newTrack, bool replaceTrack = false)
        {
            if (newTrack.F3DCommands == null || newTrack.TextureReferences == null || newTrack.Vertices == null || newTrack.TrackItems == null ||
                newTrack.F3DCommands.Commands.Count == 0 || newTrack.TextureReferences.Count == 0 || newTrack.Vertices.Vertices.Count == 0)
                    return false;

            //create the itemblock, vertexblock and textureblock
            MIO0Block iBlock = new MIO0Block(-1, MIO0.Encode(newTrack.TrackItems.Data, 0x10));
            TrackItemBlock itemBlock = new TrackItemBlock(-1, iBlock);

            MIO0Block vBlock = new MIO0Block(-1, MIO0.Encode(VertexPacker.VerticesToBytes(newTrack.Vertices.Vertices.ToList()).ToArray(), 0x10));
            TrackVertexDLBlock vertexBlock = new TrackVertexDLBlock(-1, vBlock, F3DEXPacker.CommandsToBytes(newTrack.F3DCommands.Commands.ToList()).ToArray(),
                newTrack.F3DCommands.RawDataSize - 8);

            List<TrackTextureRef> textureRefs = new List<TrackTextureRef>();
            foreach(MK64Image image in newTrack.TextureReferences)
            {
                if(TextureHub.Images.Count(i => i.ImageName == image.ImageName) != 0)
                    textureRefs.Add(new TrackTextureRef(-1, image));
                else
                {
                    //Add the MIO0 block for the image
                    if(image.TextureOffset == -1)
                    {
                        MIO0Block imgBlock = new MIO0Block(NewElementOffset, MIO0.Encode(image.ImageReference.Texture.RawData));
                        imgBlock.AddElement(image.ImageReference.Texture);
                        image.TextureBlockOffset = 0;
                        image.TextureOffset = NewElementOffset;
                        image.ImageReference.Texture.FileOffset = 0;
                        RomProject.Instance.Files[0].AddElement(imgBlock); //CAREFUL, WHAT IF IT DOESN'T WORK?
                        AdvanceNewElementOffset(imgBlock);
                    }
                    for(int i = 0; i < image.PaletteOffset.Count; i++)
                    {
                        int palOff = image.PaletteOffset[i];
                        if(palOff == -1)
                        {
                            if(image.PaletteEncoding[i] == MK64Image.MK64ImageEncoding.MIO0)
                            {
                                //Do more here in the future, but we shouldn't ever hit this point
                            }
                            else
                            {
                                image.ImageReference.BasePalettes[i].FileOffset = NewElementOffset;
                                image.PaletteOffset[i] = NewElementOffset;
                                RomProject.Instance.Files[0].AddElement(image.ImageReference.BasePalettes[i]); //CAREFUL, WHAT IF IT DOESN'T WORK?
                                AdvanceNewElementOffset(image.ImageReference.BasePalettes[i]);
                            }
                        }
                    }
                    //Add in the image
                    TextureHub.AddImage(image);

                    //Now add it to the list
                    textureRefs.Add(new TrackTextureRef(-1, image));
                }
            }
            List<DmaAddress> dlRefs = new List<DmaAddress>(newTrack.CommandReferences);

            TrackTextureRefBlock textureBlock = new TrackTextureRefBlock(-1, textureRefs, dlRefs);

            //Add the blocks to data
            itemBlock.FileOffset = NewElementOffset;
            RomProject.Instance.Files[0].AddElement(itemBlock);
            AdvanceNewElementOffset(itemBlock);

            vertexBlock.FileOffset = NewElementOffset;
            RomProject.Instance.Files[0].AddElement(vertexBlock);
            AdvanceNewElementOffset(vertexBlock);

            textureBlock.FileOffset = NewElementOffset;
            RomProject.Instance.Files[0].AddElement(textureBlock);
            AdvanceNewElementOffset(textureBlock);

            //Finally create the track
            CompressedTrack ct = new CompressedTrack(newTrack.TrackName, itemBlock, vertexBlock, textureBlock,
                (int)newTrack.Unknown1, newTrack.Unknown2, newTrack.TopColor, newTrack.BottomColor);

            if (replaceTrack)
            {
                //If an already existing track has the same name, replace it!
                int index = Tracks.FindIndex(t => t.TrackName == ct.TrackName);
                if (index == -1)
                    Tracks.Add(ct);
                else
                    Tracks[index] = ct;
            }
            else
                Tracks.Add(ct);

            return true;
        }

        public override string GetXMLPath()
        {
            return "MarioKartElementHub";
        }
    }
}
