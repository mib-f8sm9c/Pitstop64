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
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock0Location))
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
                block = (KartGraphicsReferenceBlock)RomProject.Instance.Files[0].GetElementAt(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock0Location);
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

            //Anim palettes
            for (int i = 0; i < KartGraphicsReferenceBlock.CHARACTER_COUNT; i++)
            {
                for(int j = 0; j < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT; j++)
                {
                    if (block.WheelPaletteReferences[i][j].ReferenceElement == null)
                    {
                        //Load the palette block
                        int paletteOffset = block.WheelPaletteReferences[i][j].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;

                        N64DataElement existingPalette;

                        //Don't double up on duplicates
                        if (results.KartPaletteBlocks.SingleOrDefault(b => b.FileOffset == paletteOffset) == null)
                        {
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
                                (existingPalette = RomProject.Instance.Files[0].GetElementAt(paletteOffset)) is Palette)
                            {
                                List<Palette> palettes = new List<Palette>();

                                int foundPaletteOffset = paletteOffset;
                                for (int k = 0; k < KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT * 4; k++)
                                {
                                    if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
                                        (existingPalette = RomProject.Instance.Files[0].GetElementAt(foundPaletteOffset)) is Palette)
                                        palettes.Add((Palette)RomProject.Instance.Files[0].GetElementAt(foundPaletteOffset));

                                    foundPaletteOffset += 0x40 * 2;
                                }

                                KartPaletteBlock newBlock = new KartPaletteBlock(paletteOffset, palettes);
                                block.WheelPaletteReferences[i][j].ReferenceElement = newBlock;

                                results.KartPaletteBlocks.Add(newBlock);
                            }
                            else if (rawData != null)
                            {
                                byte[] turnPaletteBytes = new byte[0x40 * 2 * 21 * 4];

                                Array.Copy(rawData, paletteOffset, turnPaletteBytes, 0, turnPaletteBytes.Length);
                                KartPaletteBlock paletteBlock = new KartPaletteBlock(paletteOffset, turnPaletteBytes);

                                block.WheelPaletteReferences[i][j].ReferenceElement = paletteBlock;

                                results.NewElements.AddRange(paletteBlock.Palettes);
                                results.KartPaletteBlocks.Add(paletteBlock);
                                //RomProject.Instance.Files[0].AddElement(newPalette);
                            }
                        }
                        else
                        {
                            block.WheelPaletteReferences[i][j].ReferenceElement =
                                results.KartPaletteBlocks.SingleOrDefault(b => b.FileOffset == paletteOffset);
                        }
                    }

                    int j2 = j + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT;
                    if (block.WheelPaletteReferences[i][j2].ReferenceElement == null)
                    {
                        //Load the palette block
                        int paletteOffset = block.WheelPaletteReferences[i][j2].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;

                        N64DataElement existingPalette;
                        
                        //Don't double up on duplicates
                        if (results.KartPaletteBlocks.SingleOrDefault(b => b.FileOffset == paletteOffset) == null)
                        {
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
                                (existingPalette = RomProject.Instance.Files[0].GetElementAt(paletteOffset)) is Palette)
                            {
                                List<Palette> palettes = new List<Palette>();

                                int foundPaletteOffset = paletteOffset;
                                for (int k = 0; k < KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT * 4; k++)
                                {
                                    if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset) &&
                                        (existingPalette = RomProject.Instance.Files[0].GetElementAt(foundPaletteOffset)) is Palette)
                                        palettes.Add((Palette)RomProject.Instance.Files[0].GetElementAt(foundPaletteOffset));

                                    foundPaletteOffset += 0x40 * 2;
                                }

                                KartPaletteBlock newBlock = new KartPaletteBlock(paletteOffset, palettes);
                                block.WheelPaletteReferences[i][j2].ReferenceElement = newBlock;

                                results.KartPaletteBlocks.Add(newBlock);
                            }
                            else if (rawData != null)
                            {
                                byte[] turnPaletteBytes = new byte[0x40 * 2 * 20 * 4];

                                Array.Copy(rawData, paletteOffset, turnPaletteBytes, 0, turnPaletteBytes.Length);
                                KartPaletteBlock paletteBlock = new KartPaletteBlock(paletteOffset, turnPaletteBytes);

                                block.WheelPaletteReferences[i][j2].ReferenceElement = paletteBlock;

                                results.NewElements.AddRange(paletteBlock.Palettes);
                                results.KartPaletteBlocks.Add(paletteBlock);
                                //RomProject.Instance.Files[0].AddElement(newPalette);
                            }
                        }
                        else
                        {
                            block.WheelPaletteReferences[i][j2].ReferenceElement =
                                results.KartPaletteBlocks.SingleOrDefault(b => b.FileOffset == paletteOffset);
                        }
                    }
                }
            }

            //Base palettes
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
                        byte[] paletteData = new byte[0xC0 * 2]; //192 2-byte color values
                        Array.Copy(rawData, paletteOffset, paletteData, 0, paletteData.Length);

                        Palette newPalette = new Palette(paletteOffset, paletteData);
                        block.CharacterPaletteReferences[i].ReferenceElement = newPalette;

                        results.NewElements.Add(newPalette);
                        //RomProject.Instance.Files[0].AddElement(newPalette);
                    }
                }

                //OKAY, Instructions for the next step:
                // Use the RomImageOrder to split up images the way you need to, then whatever.
                
                //Dictionary to speed up the searching algorithm
                Dictionary<int, ImageMIO0Block> foundOffsets = new Dictionary<int, ImageMIO0Block>();

                for (int j = 0; j < block.CharacterTurnReferences[i].Length; j++)
                {
                    if (block.CharacterTurnReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = block.CharacterTurnReferences[i][j].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;

                        ImageMIO0Block mio;

                        if (foundOffsets.TryGetValue(mioOffset, out mio))
                        {
                            block.CharacterTurnReferences[i][j].ReferenceElement = mio;
                            continue;
                        }
                        else if (rawData != null)
                        {
                            mio = ImageMIO0Block.ReadImageMIO0BlockFrom(rawData, mioOffset);
                            block.CharacterTurnReferences[i][j].ReferenceElement = mio;

                            results.NewElements.Add(mio);
                            results.OriginalMIO0s.Add(mio);
                            foundOffsets.Add(mioOffset, mio);
                        }
                    }

                    //Handle the encoded texture now
                    ImageMIO0Block imageMio = (ImageMIO0Block)block.CharacterTurnReferences[i][j].ReferenceElement;

                    if (imageMio != null && imageMio.DecodedN64DataElement == null)
                    {
                        Palette selectedPalette = (Palette)block.CharacterPaletteReferences[i].ReferenceElement;
                        
                        int animIndex, frameIndex;
                        bool isTurnAnim;
                        GetAnimationFrameIndices(j, out animIndex, out frameIndex, out isTurnAnim);

                        Palette firstAnimPalette;

                        //Note: looks like the wheel palette blocks.... are backwards...
                        if (isTurnAnim)
                        {
                            firstAnimPalette = ((KartPaletteBlock)block.WheelPaletteReferences[i][8-animIndex].ReferenceElement)
                                .Palettes[frameIndex * 4];
                        }
                        else
                        {
                            firstAnimPalette = ((KartPaletteBlock)block.WheelPaletteReferences[i][(8-animIndex) + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT].ReferenceElement)
                                .Palettes[frameIndex * 4];
                        }

                        Texture newTexture = new Texture(0, imageMio.DecodedData, Texture.ImageFormat.CI, Texture.PixelInfo.Size_8b, 64, 64, selectedPalette.Combine(firstAnimPalette));

                        imageMio.DecodedN64DataElement = newTexture;
                    }
                }

                foundOffsets.Clear();

                for (int j = 0; j < block.CharacterCrashReferences[i].Length; j++)
                {
                    if (block.CharacterCrashReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = block.CharacterCrashReferences[i][j].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;
                        
                        ImageMIO0Block mio;

                        if (foundOffsets.TryGetValue(mioOffset, out mio))
                        {
                            block.CharacterCrashReferences[i][j].ReferenceElement = mio;
                            continue;
                        }
                        else if (rawData != null)
                        {
                            mio = ImageMIO0Block.ReadImageMIO0BlockFrom(rawData, mioOffset);
                            block.CharacterCrashReferences[i][j].ReferenceElement = mio;

                            results.NewElements.Add(mio);
                            results.OriginalMIO0s.Add(mio);
                            foundOffsets.Add(mioOffset, mio);
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

            for (int k = 0; k < portraits.Entries.Count; k++)
            {
                List<KartPortraitTableEntry> kartPortraits = portraits.Entries[k];
                string kartName = Enum.GetName(typeof(MarioKartRomInfo.OriginalCharacters), k);

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
                            mio.ImageName = kartName + "Portrait-" + (i + 1); 

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

                DmaAddress[] wheelPaletteReferences = block.WheelPaletteReferences[i];

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
                spinAnims[0].KartAnimationType = (int)(KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25 |
                    KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown19);
                spinAnims[1] = spinAnims[0];
                spinAnims[2] = new KartAnimationSeries(kartName + " Spin Down 12");
                spinAnims[2].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12;
                spinAnims[3] = new KartAnimationSeries(kartName + " Spin 0");
                spinAnims[3].KartAnimationType = (int)(KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown6 |
                    KartAnimationSeries.KartAnimationTypeFlag.FullSpin0 | KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp6);
                spinAnims[4] = spinAnims[3];
                spinAnims[5] = spinAnims[3];
                spinAnims[6] = new KartAnimationSeries(kartName + " Spin Up 12");
                spinAnims[6].KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12;
                spinAnims[7] = new KartAnimationSeries(kartName + " Spin Up 25");
                spinAnims[7].KartAnimationType = (int)(KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp19 |
                    KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25);
                spinAnims[8] = spinAnims[7];

                crashAnim = new KartAnimationSeries(kartName + " Crash");
                crashAnim.KartAnimationType = (int)KartAnimationSeries.KartAnimationTypeFlag.Crash;

                //Work backwards, to help with image naming
                for (short j = 0; j < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT; j++)
                {
                    ImageMIO0Block imageBlock = (ImageMIO0Block)block.CharacterTurnReferences[i][j].ReferenceElement;

                    //Jump out if the image block has already been handled
                    if (!string.IsNullOrWhiteSpace(imageBlock.ImageName) && newKart.KartImages.Images.ContainsKey(imageBlock.ImageName))
                        continue;

                    int animIndex, frameIndex;
                    bool isTurnAnim;

                    List<Palette> animationPalettes = new List<Palette>();

                    GetAnimationFrameIndices(j, out animIndex, out frameIndex, out isTurnAnim);

                    if (isTurnAnim)
                    {
                        if (turnBlocks[animIndex][frameIndex] == null)
                        {
                            imageBlock.ImageName = kartName[0] + " " + Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                                turnAnims[animIndex].KartAnimationType) + "-" + frameIndex;

                            turnBlocks[animIndex][frameIndex] = imageBlock;

                            for (int p = 0; p < 4; p++)
                            {
                                animationPalettes.Add(((KartPaletteBlock)block.WheelPaletteReferences[i][(8 - animIndex)].ReferenceElement)
                                        .Palettes[frameIndex * 4 + p].Duplicate());
                            }
                        }
                    }
                    else
                    {
                        if (spinBlocks[animIndex][frameIndex] == null)
                        {
                            //Special case for spin blocks
                            string typeName;

                            switch (animIndex)
                            {
                                case 0:
                                case 1:
                                    typeName = Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                                        KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown25);
                                    break;
                                case 2:
                                    typeName = Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                                        KartAnimationSeries.KartAnimationTypeFlag.FullSpinDown12);
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                    typeName = Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                                        KartAnimationSeries.KartAnimationTypeFlag.FullSpin0);
                                    break;
                                case 6:
                                    typeName = Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                                        KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp12);
                                    break;
                                case 7:
                                case 8:
                                default:
                                    typeName = Enum.GetName(typeof(KartAnimationSeries.KartAnimationTypeFlag),
                                        KartAnimationSeries.KartAnimationTypeFlag.FullSpinUp25);
                                    break;
                            }

                            imageBlock.ImageName = kartName[0] + " " + typeName + "-" + frameIndex;

                            spinBlocks[animIndex][frameIndex] = imageBlock;

                            for (int p = 0; p < 4; p++)
                            {
                                animationPalettes.Add(((KartPaletteBlock)block.WheelPaletteReferences[i][(8 - animIndex) + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT].ReferenceElement)
                                        .Palettes[frameIndex * 4 + p].Duplicate());
                            }
                        }
                    }

                    if (!newKart.KartImages.Images.ContainsKey(imageBlock.ImageName))
                    {
                        newKart.KartImages.Images.Add(imageBlock.ImageName, new KartImage(imageBlock, animationPalettes));
                    }
                }

                for (int j = 0; j < spinBlocks.Length; j++)
                {
                    //Don't deal with duplicate animations
                    if (j != 0 && spinAnims[j] == spinAnims[j - 1])
                        continue;

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
                        newKart.KartImages.Images.Add(imageBlock.ImageName, new KartImage(imageBlock, (Palette)null));
                    }
                }

                for (int j = 0; j < turnAnims.Length; j++)
                    newKart.KartAnimations.Add(turnAnims[j]);

                for (int j = 0; j < spinAnims.Length; j++)
                {
                    //Don't store duplicate animations
                    if (j != 0 && spinAnims[j] == spinAnims[j - 1])
                        continue;

                    newKart.KartAnimations.Add(spinAnims[j]);
                }

                newKart.KartAnimations.Add(crashAnim);

                for (int j = 0; j < portraits.Entries[i].Count; j++)
                {
                    newKart.KartPortraits.Add(portraits.Entries[i][j].ImageReference);
                }

                TKMK00Block tkmk;
                if ((tkmk = nameplatesContainer.SingleOrDefault(t => t.FileOffset == MarioKartRomInfo.CharacterNameplateReference[i])) != null)
                {
                    TKMK00Block newTkmk = new TKMK00Block(-1, tkmk.RawData, tkmk.ImageAlphaColor);
                    newKart.KartNamePlate = newTkmk;
                }

                MarioKart64ElementHub.Instance.Karts.Add(newKart);
                MarioKart64ElementHub.Instance.SelectedKarts[i] = newKart;
                RomProject.Instance.AddRomItem(newKart);
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
            foreach (KartPaletteBlock block in results.KartPaletteBlocks)
            {
                if (block.Palettes.Count == 84)
                {
                    if (!MarioKart64ElementHub.Instance.TurnKartPaletteBlocks.Contains(block))
                        MarioKart64ElementHub.Instance.TurnKartPaletteBlocks.Add(block);
                }
                else
                {
                    if(!MarioKart64ElementHub.Instance.SpinKartPaletteBlocks.Contains(block))
                        MarioKart64ElementHub.Instance.SpinKartPaletteBlocks.Add(block);
                }
            }
        }

        private static void GetAnimationFrameIndices(int imageIndex, out int animIndex, out int frameIndex, out bool isTurnAnim)
        {
            //Determine which animation block the current image belongs in
            if (imageIndex >= KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT)
            {
                //Full spin
                animIndex = (imageIndex - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) / KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT;
                frameIndex = (imageIndex - KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT) - animIndex * KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT;

                isTurnAnim = false;
            }
            else
            {
                //Half turn
                animIndex = imageIndex / KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT;
                frameIndex = imageIndex - animIndex * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT;

                //The last 14 values of the turn animation are from the spin one, actually
                if (frameIndex >= KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT)
                {
                    frameIndex -= 15;
                    isTurnAnim = false;
                }
                else
                    isTurnAnim = true;
            }
        }
    }

    public class KartReaderResults
    {
        public List<N64DataElement> NewElements;
        public List<ImageMIO0Block> OriginalMIO0s;
        public KartGraphicsReferenceBlock KartGraphicsBlock;
        public KartPortraitTable KartPortraitsTable;
        public List<KartPaletteBlock> KartPaletteBlocks;

        public KartReaderResults()
        {
            NewElements = new List<N64DataElement>();
            OriginalMIO0s = new List<ImageMIO0Block>();
            KartPaletteBlocks = new List<KartPaletteBlock>();
        }
    }
}
