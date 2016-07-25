using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using MK64Pitstop.Data.Karts;
using Cereal64.Common.Rom;
using MK64Pitstop.Services.Hub;
using Cereal64.Microcodes.F3DEX.DataElements;
using MK64Pitstop.Data;

namespace MK64Pitstop.Services.Readers
{
    public static class KartReader
    {
        public static void ReadRom(BackgroundWorker worker, byte[] rawData, MarioKart64ReaderResults finalResults)
        {
            KartReaderResults results = new KartReaderResults();

            //Portraits first
            KartPortraitTable portraits;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.CharacterFaceTableOffset))
            {
                ProgressService.SetMessage("Loading Kart Portraits");
                byte[] portraitBlock = new byte[MarioKartRomInfo.CharacterFaceTableLength];
                Array.Copy(rawData, MarioKartRomInfo.CharacterFaceTableOffset, portraitBlock, 0, MarioKartRomInfo.CharacterFaceTableLength);

                portraits = new KartPortraitTable(MarioKartRomInfo.CharacterFaceTableOffset, portraitBlock);
                //RomProject.Instance.Files[0].AddElement(portraits);
                results.NewElements.Add(portraits);
            }
            else
            {
                portraits = (KartPortraitTable)RomProject.Instance.Files[0].GetElementAt(MarioKartRomInfo.CharacterFaceTableOffset);
            }

            //Add to hub here?

            KartGraphicsReferenceBlock block;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock1Location))
            {
                ProgressService.SetMessage("Loading Kart Resources");
                byte[] refBlock = new byte[KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceLength];
                Array.Copy(rawData, KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock0Location, refBlock, 0, KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceLength);

                block = new KartGraphicsReferenceBlock(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock0Location, refBlock);
                results.NewElements.Add(block);
                //RomProject.Instance.Files[0].AddElement(block);
            }
            else
            {
                block = (KartGraphicsReferenceBlock)RomProject.Instance.Files[0].GetElementAt(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock1Location);
            }

            LoadKartGraphicDmaReferences(block, rawData, results, worker);
            LoadKartPortraitDmaReferences(portraits, rawData, results, worker);

            if (MarioKart64ElementHub.Instance.Karts.Count == 0) //Has not been initialized
                LoadKartInfo(block, portraits, worker, rawData, finalResults.OriginalTKMK00Blocks, results);
            
            results.KartGraphicsBlock = block;
            results.KartPortraitsTable = portraits;

            finalResults.KartResults = results;
        }

        private static void LoadKartGraphicDmaReferences(KartGraphicsReferenceBlock block, byte[] rawData, KartReaderResults results, BackgroundWorker worker)
        {
            int mioOffset;

            for (int i = 0; i < KartGraphicsReferenceBlock.CHARACTER_COUNT; i++)
            {
                string kartName = Enum.GetName(typeof(MarioKartRomInfo.OriginalCharacters), i);

                if (block.CharacterPaletteReferences[i].ReferenceElement == null)
                {
                    int paletteOffset = block.CharacterPaletteReferences[i].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;

                    N64DataElement existingPalette;

                    if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
                        (existingPalette = RomProject.Instance.Files[0].GetElementAt(paletteOffset)) is Palette)
                    {
                        block.CharacterPaletteReferences[i].ReferenceElement = (Palette)existingPalette;
                    }
                    else if (rawData != null)
                    {
                        byte[] paletteData = new byte[0x180]; //256 2-byte color values
                        Array.Copy(rawData, paletteOffset, paletteData, 0, paletteData.Length);

                        Palette newPalette = new Palette(paletteOffset, paletteData);
                        block.CharacterPaletteReferences[i].ReferenceElement = newPalette;

                        results.NewElements.Add(newPalette);
                        //RomProject.Instance.Files[0].AddElement(newPalette);
                    }
                }

                //OKAY, Instructions for the next step:
                // Use the RomImageOrder to split up images the way you need to, then whatever.
                
                for (int j = 0; j < block.CharacterTurnReferences[i].Length; j++)
                {
                    if (block.CharacterTurnReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = block.CharacterTurnReferences[i][j].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;

                        N64DataElement existingMio;
                        ImageMIO0Block mio;

                        if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset) &&
                            (existingMio = RomProject.Instance.Files[0].GetElementAt(mioOffset)) is ImageMIO0Block)
                        {
                            mio = (ImageMIO0Block)existingMio;
                            block.CharacterTurnReferences[i][j].ReferenceElement = mio;
                        }
                        else if (rawData != null)
                        {
                            mio = ImageMIO0Block.ReadImageMIO0BlockFrom(rawData, mioOffset);
                            block.CharacterTurnReferences[i][j].ReferenceElement = mio;

                            results.NewElements.Add(mio);
                            results.OriginalMIO0s.Add(mio);
                            //RomProject.Instance.Files[0].AddElement(mio);
                            //MarioKart64ElementHub.Instance.OriginalMIO0Blocks.Add(mio);
                        }
                    }

                    //Handle the encoded texture now
                    ImageMIO0Block imageMio = (ImageMIO0Block)block.CharacterTurnReferences[i][j].ReferenceElement;

                    if (imageMio != null && imageMio.DecodedN64DataElement == null)
                    {
                        Palette selectedPalette = (Palette)block.CharacterPaletteReferences[i].ReferenceElement;
                        Texture newTexture = new Texture(0, imageMio.DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, selectedPalette);
                        
                        imageMio.DecodedN64DataElement = newTexture;
                    }
                }

                for (int j = 0; j < block.CharacterCrashReferences[i].Length; j++)
                {
                    if (block.CharacterCrashReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = block.CharacterCrashReferences[i][j].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;
                        
                        N64DataElement existingMio;
                        ImageMIO0Block mio;
                        
                        if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset) &&
                            (existingMio = RomProject.Instance.Files[0].GetElementAt(mioOffset)) is ImageMIO0Block)
                        {
                            mio = (ImageMIO0Block)existingMio;
                            block.CharacterCrashReferences[i][j].ReferenceElement = mio;
                        }
                        else if (rawData != null)
                        {
                            mio = ImageMIO0Block.ReadImageMIO0BlockFrom(rawData, mioOffset);
                            block.CharacterCrashReferences[i][j].ReferenceElement = mio;

                            results.NewElements.Add(mio);
                            results.OriginalMIO0s.Add(mio);
                            //RomProject.Instance.Files[0].AddElement(mio);
                            //MarioKart64ElementHub.Instance.OriginalMIO0Blocks.Add(mio);
                        }
                    }

                    //Handle the encoded texture now
                    ImageMIO0Block imageMio = (ImageMIO0Block)block.CharacterCrashReferences[i][j].ReferenceElement;

                    if (imageMio != null && imageMio.DecodedN64DataElement == null)
                    {
                        Palette selectedPalette = (Palette)block.CharacterPaletteReferences[i].ReferenceElement;
                        Texture newTexture = new Texture(0, imageMio.DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, selectedPalette);
                        
                        imageMio.DecodedN64DataElement = newTexture;
                    }
                }
            }
        }

        private static void LoadKartPortraitDmaReferences(KartPortraitTable portraits, byte[] rawData, KartReaderResults results, BackgroundWorker worker)
        {
            int mioOffset;

            foreach (List<KartPortraitTableEntry> kartPortraits in portraits.Entries)
            {
                for (int i = 0; i < kartPortraits.Count; i++)
                {
                    if (kartPortraits[i].ImageReference == null)
                    {
                        mioOffset = kartPortraits[i].ImageOffset + MarioKartRomInfo.CharacterFaceMIO0Offset;

                        N64DataElement existingMio;
                        ImageMIO0Block mio;

                        if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset) &&
                            (existingMio = RomProject.Instance.Files[0].GetElementAt(mioOffset)) is ImageMIO0Block)
                        {
                            mio = (ImageMIO0Block)existingMio;
                            kartPortraits[i].ImageReference = mio;
                        }
                        else if(rawData != null)
                        {
                            mio = ImageMIO0Block.ReadImageMIO0BlockFrom(rawData, mioOffset);
                            kartPortraits[i].ImageReference = mio;

                            results.NewElements.Add(mio);
                            results.OriginalMIO0s.Add(mio);
                            //RomProject.Instance.Files[0].AddElement(mio);
                            //MarioKart64ElementHub.Instance.OriginalMIO0Blocks.Add(mio);
                        }

                        if (kartPortraits[i].ImageReference != null && kartPortraits[i].ImageReference.DecodedN64DataElement == null)
                        {
                            Texture newTexture = new Texture(0, kartPortraits[i].ImageReference.DecodedData, Texture.ImageFormat.RGBA, Texture.PixelInfo.Size_16b, 64, 64);

                            kartPortraits[i].ImageReference.DecodedN64DataElement = newTexture;
                        }
                    }
                }
            }
        }

        private static void LoadKartInfo(KartGraphicsReferenceBlock block, KartPortraitTable portraits, BackgroundWorker worker, byte[] data, List<TKMK00Block> nameplatesContainer, KartReaderResults results)
        {
            for (int i = 0; i < KartGraphicsReferenceBlock.CHARACTER_COUNT; i++)
            {
                string kartName = Enum.GetName(typeof(MarioKartRomInfo.OriginalCharacters), i);

                KartInfo newKart = new KartInfo(kartName, (Palette)block.CharacterPaletteReferences[i].ReferenceElement, true);

                KartAnimationSeries[] turnAnims = new KartAnimationSeries[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT];
                KartAnimationSeries[] spinAnims = new KartAnimationSeries[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT];
                KartAnimationSeries crashAnim;

                ImageMIO0Block[][] turnBlocks = new ImageMIO0Block[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT][];
                for (int k = 0; k < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT; k++)
                {
                    turnBlocks[k] = new ImageMIO0Block[KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT];
                }

                ImageMIO0Block[][] spinBlocks = new ImageMIO0Block[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT][];
                for (int k = 0; k < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT; k++)
                {
                    spinBlocks[k] = new ImageMIO0Block[KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT];
                }

                turnAnims[0] = new KartAnimationSeries(kartName + " Turn Down 25");
                turnAnims[0].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown25;
                turnAnims[1] = new KartAnimationSeries(kartName + " Turn Down 19");
                turnAnims[1].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown19;
                turnAnims[2] = new KartAnimationSeries(kartName + " Turn Down 12");
                turnAnims[2].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown12;
                turnAnims[3] = new KartAnimationSeries(kartName + " Turn Down 6");
                turnAnims[3].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnDown6;
                turnAnims[4] = new KartAnimationSeries(kartName + " Turn 0");
                turnAnims[4].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurn0;
                turnAnims[5] = new KartAnimationSeries(kartName + " Turn Up 6");
                turnAnims[5].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp6;
                turnAnims[6] = new KartAnimationSeries(kartName + " Turn Up 12");
                turnAnims[6].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp12;
                turnAnims[7] = new KartAnimationSeries(kartName + " Turn Up 19");
                turnAnims[7].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp19;
                turnAnims[8] = new KartAnimationSeries(kartName + " Turn Up 25");
                turnAnims[8].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.RearTurnUp25;

                spinAnims[0] = new KartAnimationSeries(kartName + " Spin Down 25");
                spinAnims[0].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25;
                spinAnims[1] = new KartAnimationSeries(kartName + " Spin Down 19");
                spinAnims[1].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19;
                spinAnims[2] = new KartAnimationSeries(kartName + " Spin Down 12");
                spinAnims[2].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12;
                spinAnims[3] = new KartAnimationSeries(kartName + " Spin Down 6");
                spinAnims[3].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6;
                spinAnims[4] = new KartAnimationSeries(kartName + " Spin 0");
                spinAnims[4].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpin0;
                spinAnims[5] = new KartAnimationSeries(kartName + " Spin Up 6");
                spinAnims[5].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6;
                spinAnims[6] = new KartAnimationSeries(kartName + " Spin Up 12"); ;
                spinAnims[6].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12;
                spinAnims[7] = new KartAnimationSeries(kartName + " Spin Up 19"); ;
                spinAnims[7].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19;
                spinAnims[8] = new KartAnimationSeries(kartName + " Spin Up 25");
                spinAnims[8].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25;

                crashAnim = new KartAnimationSeries(kartName + " Crash");
                crashAnim.KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.Crash;

                //Work backwards, to help with image naming
                for (short j = 0; j < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT; j++)
                {
                    ImageMIO0Block imageBlock = (ImageMIO0Block)block.CharacterTurnReferences[i][j].ReferenceElement;

                    //Determine which animation block the current image belongs in
                    if (j >= KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT)
                    {
                        //Full spin
                        int spinAnim = (j - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT) / KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT;
                        int spinIndex = (j - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT) - spinAnim * KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT;

                        imageBlock.ImageName = kartName[0] + " " + Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                            spinAnims[spinAnim].KartAnimationType) + "-" + spinIndex;

                        spinBlocks[spinAnim][spinIndex] = imageBlock;
                    }
                    else
                    {
                        //Half turn
                        int turnAnim = j / KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT;
                        int turnIndex = j - turnAnim * KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT;

                        imageBlock.ImageName = kartName[0] + " " + Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                            turnAnims[turnAnim].KartAnimationType) + "-" + turnIndex;

                        turnBlocks[turnAnim][turnIndex] = imageBlock;
                    }

                    if (!newKart.KartImages.Images.ContainsKey(imageBlock.ImageName))
                    {
                        newKart.KartImages.Images.Add(imageBlock.ImageName, new KartImage(imageBlock));
                    }
                }

                for (int j = 0; j < spinBlocks.Length; j++)
                {
                    for (int k = 0; k < spinBlocks[j].Length; k++)
                    {
                        if (spinBlocks[j][k] != null)
                            spinAnims[j].OrderedImageNames.Add(spinBlocks[j][k].ImageName);
                    }
                }

                for (int j = 0; j < turnBlocks.Length; j++)
                {
                    for (int k = 0; k < turnBlocks[j].Length; k++)
                    {
                        if (turnBlocks[j][k] != null)
                            turnAnims[j].OrderedImageNames.Add(turnBlocks[j][k].ImageName);
                    }
                }

                for (int j = 0; j < block.CharacterCrashReferences[i].Length; j++)
                {
                    ImageMIO0Block imageBlock = (ImageMIO0Block)block.CharacterCrashReferences[i][j].ReferenceElement;

                    imageBlock.ImageName = kartName[0] + " " + Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                        crashAnim.KartAnimationType) + "-" + j;

                    crashAnim.OrderedImageNames.Add(imageBlock.ImageName);

                    if (!newKart.KartImages.Images.ContainsKey(imageBlock.ImageName))
                    {
                        newKart.KartImages.Images.Add(imageBlock.ImageName, new KartImage(imageBlock));
                    }
                }

                for (int j = 0; j < turnAnims.Length; j++)
                    newKart.KartAnimations.Add(turnAnims[j]);

                for (int j = 0; j < spinAnims.Length; j++)
                    newKart.KartAnimations.Add(spinAnims[j]);

                newKart.KartAnimations.Add(crashAnim);

                for(int j = 0; j < portraits.Entries[i].Count; j++)
                    newKart.KartPortraits.Add(portraits.Entries[i][j].ImageReference);

                TKMK00Block tkmk;
                if ((tkmk = nameplatesContainer.SingleOrDefault(t => t.FileOffset == MarioKartRomInfo.CharacterNameplateReference[i])) != null)
                {
                    TKMK00Block newTkmk = new TKMK00Block(MarioKart64ElementHub.Instance.NewElementOffset, tkmk.RawData, tkmk.ImageAlphaColor);
                    newKart.KartNamePlate = newTkmk;
                    MarioKart64ElementHub.Instance.AdvanceNewElementOffset(newTkmk);
                    results.NewElements.Add(newTkmk);
                }

                MarioKart64ElementHub.Instance.Karts.Add(newKart);
                MarioKart64ElementHub.Instance.SelectedKarts[i] = newKart;
            }
        }

        public static void ApplyResults(KartReaderResults results)
        {
            foreach (N64DataElement element in results.NewElements)
                RomProject.Instance.Files[0].AddElement(element);

            foreach (ImageMIO0Block block in results.OriginalMIO0s)
                MarioKart64ElementHub.Instance.OriginalMIO0Blocks.Add(block);

            MarioKart64ElementHub.Instance.KartGraphicsBlock = results.KartGraphicsBlock;
            MarioKart64ElementHub.Instance.KartPortraitsTable = results.KartPortraitsTable;
        }
    }

    public class KartReaderResults
    {
        public List<N64DataElement> NewElements;
        public List<ImageMIO0Block> OriginalMIO0s;
        public KartGraphicsReferenceBlock KartGraphicsBlock;
        public KartPortraitTable KartPortraitsTable;

        public KartReaderResults()
        {
            NewElements = new List<N64DataElement>();
            OriginalMIO0s = new List<ImageMIO0Block>();
        }
    }
}
