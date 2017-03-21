using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Pitstop64.Data.Karts;
using Cereal64.Common.Rom;
using Pitstop64.Services.Hub;
using Cereal64.Microcodes.F3DEX.DataElements;
using Pitstop64.Data;
using Cereal64.Common.DataElements.Encoding;
using Pitstop64.Data.Textures;

namespace Pitstop64.Services.Readers
{
    public static class KartReader
    {
        public static void ReadRom(BackgroundWorker worker, byte[] rawData, MarioKart64ReaderResults finalResults)
        {
            KartReaderResults results = new KartReaderResults();

            //Portraits first
            KartPortraitTable portraits;
            N64DataElement element;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.CharacterFaceTableOffset, out element))
            {
                byte[] portraitBlock = new byte[MarioKartRomInfo.CharacterFaceTableLength];
                Array.Copy(rawData, MarioKartRomInfo.CharacterFaceTableOffset, portraitBlock, 0, MarioKartRomInfo.CharacterFaceTableLength);

                portraits = new KartPortraitTable(MarioKartRomInfo.CharacterFaceTableOffset, portraitBlock);
                //RomProject.Instance.Files[0].AddElement(portraits);
                results.NewElements.Add(portraits);
            }
            else
            {
                portraits = (KartPortraitTable)element;
            }

            //Add to hub here?

            //Scale, Weight & Info table here
            KartScaleTable scaleTable;
            KartWeightTable weightTable;
            KartInformationBlock infoTable;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(KartScaleTable.DefaultKartScaleBlockLocation, out element))
            {
                byte[] scaleBlock = new byte[0x20];
                Array.Copy(rawData, KartScaleTable.DefaultKartScaleBlockLocation, scaleBlock, 0, scaleBlock.Length);

                scaleTable = new KartScaleTable(KartScaleTable.DefaultKartScaleBlockLocation, scaleBlock);

                results.NewElements.Add(scaleTable);
            }
            else
            {
                scaleTable = (KartScaleTable)element;
            }
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(KartWeightTable.DefaultKartWeightBlockLocation, out element))
            {
                byte[] weightBlock = new byte[0x20];
                Array.Copy(rawData, KartWeightTable.DefaultKartWeightBlockLocation, weightBlock, 0, weightBlock.Length);

                weightTable = new KartWeightTable(KartWeightTable.DefaultKartWeightBlockLocation, weightBlock);

                results.NewElements.Add(weightTable);
            }
            else
            {
                weightTable = (KartWeightTable)element;
            }
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(KartInformationBlock.DefaultKartInformationBlock0Location, out element))
            {
                byte[] infoBlock = new byte[KartInformationBlock.DefaultKartInformationBlock0End - KartInformationBlock.DefaultKartInformationBlock0Location];
                Array.Copy(rawData, KartInformationBlock.DefaultKartInformationBlock0Location, infoBlock, 0, infoBlock.Length);

                infoTable = new KartInformationBlock(KartInformationBlock.DefaultKartInformationBlock0Location, infoBlock);

                results.NewElements.Add(infoTable);
            }
            else
            {
                infoTable = (KartInformationBlock)element;
            }

            results.KartScaleTable = scaleTable;
            results.KartWeightTable = weightTable;
            results.KartInfoBlock = infoTable;

            KartGraphicsReferenceBlock block;
            N64DataElement elBlock;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock0Location, out elBlock))
            {
                byte[] refBlock = new byte[KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceLength];
                Array.Copy(rawData, KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock0Location, refBlock, 0, KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceLength);

                block = new KartGraphicsReferenceBlock(KartGraphicsReferenceBlock.DefaultKartGraphicsReferenceBlock0Location, refBlock);
                results.NewElements.Add(block);
                //RomProject.Instance.Files[0].AddElement(block);
            }
            else
            {
                block = (KartGraphicsReferenceBlock)elBlock;
            }

            //NOTE: ALL PALETTE AND TEXTURE INFORMATION WILL ALREADY BE LOADED HERE. NO NEED TO CREATE ANYTHING.

            //FIXED!!
            ProgressService.SetMessage("Organizing Kart Images");
            LoadKartGraphicDmaReferences(block, rawData, results, worker);

            //FIXED!!
            ProgressService.SetMessage("Loading Kart Portraits");
            LoadKartPortraitDmaReferences(portraits, rawData, finalResults.TextureResults, results, worker);

            //FIXED!!
            if (MarioKart64ElementHub.Instance.Karts.Count == 0) //Has not been initialized
                LoadKartInfo(block, portraits, worker, rawData, finalResults.TextureResults, results);
            
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
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset, out existingPalette) && existingPalette is Palette)
                            {
                                List<Palette> palettes = new List<Palette>();

                                int foundPaletteOffset = paletteOffset;
                                for (int k = 0; k < KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT * 4; k++)
                                {
                                    if (RomProject.Instance.Files[0].HasElementExactlyAt(foundPaletteOffset, out existingPalette) && existingPalette is Palette)
                                        palettes.Add((Palette)existingPalette);

                                    foundPaletteOffset += 0x40 * 2;
                                }

                                KartPaletteBlock newBlock = new KartPaletteBlock(paletteOffset, palettes);
                                block.WheelPaletteReferences[i][j].ReferenceElement = newBlock;

                                results.KartPaletteBlocks.Add(newBlock);
                            }
                            else
                            {
                                //ERROR: COULD NOT FIND THE PALETTE INFORMATION!
                                throw new Exception();
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
                            if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset, out existingPalette) && existingPalette is Palette)
                            {
                                List<Palette> palettes = new List<Palette>();
                                palettes.Add((Palette)existingPalette);

                                int foundPaletteOffset = paletteOffset + 0x40 * 2;
                                for (int k = 1; k < KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT * 4; k++)
                                {
                                    if (RomProject.Instance.Files[0].HasElementExactlyAt(foundPaletteOffset, out existingPalette) && existingPalette is Palette)
                                        palettes.Add((Palette)existingPalette);

                                    foundPaletteOffset += 0x40 * 2;
                                }

                                KartPaletteBlock newBlock = new KartPaletteBlock(paletteOffset, palettes);
                                block.WheelPaletteReferences[i][j2].ReferenceElement = newBlock;

                                results.KartPaletteBlocks.Add(newBlock);
                            }
                            else
                            {
                                //ERROR: COULD NOT FIND THE PALETTE INFORMATION!
                                throw new Exception();
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

                    if (RomProject.Instance.Files[0].HasElementExactlyAt(paletteOffset, out existingPalette) && existingPalette is Palette)
                    {
                        block.CharacterPaletteReferences[i].ReferenceElement = (Palette)existingPalette;
                    }
                    else if (rawData != null)
                    {
                        //ERROR: COULD NOT FIND THE PALETTE INFORMATION!
                        throw new Exception();
                    }
                }

                //OKAY, Instructions for the next step:
                // Use the RomImageOrder to split up images the way you need to, then whatever.
                
                //Dictionary to speed up the searching algorithm
                Dictionary<int, MIO0Block> foundOffsets = new Dictionary<int, MIO0Block>();

                for (int j = 0; j < block.CharacterTurnReferences[i].Length; j++)
                {
                    if (block.CharacterTurnReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = block.CharacterTurnReferences[i][j].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;

                        MIO0Block mio;
                        N64DataElement elem;

                        if (foundOffsets.TryGetValue(mioOffset, out mio))
                        {
                            block.CharacterTurnReferences[i][j].ReferenceElement = mio;
                            continue;
                        }
                        else if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset, out elem) && elem is MIO0Block)
                        {
                            mio = (MIO0Block)elem;
                            block.CharacterTurnReferences[i][j].ReferenceElement = mio;
                        }
                        else
                        {
                            //COULD NOT FIND THE MIO0!!
                            throw new Exception();
                        }
                    }
                }

                foundOffsets.Clear();

                for (int j = 0; j < block.CharacterCrashReferences[i].Length; j++)
                {
                    if (block.CharacterCrashReferences[i][j].ReferenceElement == null)
                    {
                        mioOffset = block.CharacterCrashReferences[i][j].Offset + KartGraphicsReferenceBlock.DMA_SEGMENT_OFFSET;

                        MIO0Block mio;
                        N64DataElement elem;

                        if (foundOffsets.TryGetValue(mioOffset, out mio))
                        {
                            block.CharacterCrashReferences[i][j].ReferenceElement = mio;
                            continue;
                        }
                        else if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset, out elem) && elem is MIO0Block)
                        {
                            mio = (MIO0Block)elem;
                            block.CharacterCrashReferences[i][j].ReferenceElement = mio;
                        }
                        else
                        {
                            //COULD NOT FIND THE MIO0!!
                            throw new Exception();
                        }
                    }
                }
            }
        }

        private static void LoadKartPortraitDmaReferences(KartPortraitTable portraits, byte[] rawData, TextureReaderResults textureResults, KartReaderResults results, BackgroundWorker worker)
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
                        MIO0Block mio;

                        if (RomProject.Instance.Files[0].HasElementExactlyAt(mioOffset, out existingMio) && existingMio is MIO0Block)
                        {
                            mio = (MIO0Block)existingMio;

                            Texture texture = (Texture)mio.Element;
                            if(texture == null)
                                throw new Exception(); //ERROR

                            //NOTE: DO WE NEED TO ADD ALL 4 POSSIBLE IMAGES??
                            if (textureResults.ImagesByTexture.ContainsKey(texture))
                                kartPortraits[i].ImageReference = textureResults.ImagesByTexture[texture][0];
                            else if (MarioKart64ElementHub.Instance.TextureHub.HasImagesForTexture(texture))
                                kartPortraits[i].ImageReference = MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture(texture)[0];
                            else
                                throw new Exception();
                        }
                        else
                        {
                            //ERROR
                            throw new Exception();
                        }
                    }
                }
            }
        }

        private static void LoadKartInfo(KartGraphicsReferenceBlock block, KartPortraitTable portraits, BackgroundWorker worker, byte[] data, TextureReaderResults textureResults, KartReaderResults results)
        {
            for (int i = 0; i < KartGraphicsReferenceBlock.CHARACTER_COUNT; i++)
            {
                string kartName = Enum.GetName(typeof(MarioKartRomInfo.OriginalCharacters), i);

                KartInfo newKart = new KartInfo(kartName, (Palette)block.CharacterPaletteReferences[i].ReferenceElement, true);

                KartAnimationSeries[] turnAnims = new KartAnimationSeries[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT];
                KartAnimationSeries[] spinAnims = new KartAnimationSeries[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT];
                KartAnimationSeries crashAnim;

                MIO0Block[][] turnBlocks = new MIO0Block[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT][];
                for (int k = 0; k < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT; k++)
                {
                    turnBlocks[k] = new MIO0Block[KartGraphicsReferenceBlock.HALF_TURN_REF_COUNT];
                }

                MIO0Block[][] spinBlocks = new MIO0Block[KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT][];
                for (int k = 0; k < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT; k++)
                {
                    spinBlocks[k] = new MIO0Block[KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT];
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

                //use to generate the ordered image names
                Dictionary<MIO0Block, string> MioToImageName = new Dictionary<MIO0Block, string>();

                //Work backwards, to help with image naming
                for (short j = 0; j < KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_TURN_REF_COUNT + KartGraphicsReferenceBlock.ANIMATION_ANGLE_COUNT * KartGraphicsReferenceBlock.FULL_SPIN_REF_COUNT; j++)
                {
                    MIO0Block imageBlock = (MIO0Block)block.CharacterTurnReferences[i][j].ReferenceElement;
                    Texture texture = (Texture)imageBlock.Element;

                    List<MK64Image> images;
                    if (textureResults.ImagesByTexture.ContainsKey(texture))
                        images = new List<MK64Image>(textureResults.ImagesByTexture[texture]);
                    else if (MarioKart64ElementHub.Instance.TextureHub.HasImagesForTexture(texture))
                        images = MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture(texture);
                    else
                        throw new Exception();

                    string imageName = images[0].ImageName;

                    //Jump out if the image block has already been handled
                    if (!string.IsNullOrWhiteSpace(imageName) && newKart.KartImages.Images.ContainsKey(imageName))
                        continue;

                    int animIndex, frameIndex;
                    bool isTurnAnim;

                    GetAnimationFrameIndices(j, out animIndex, out frameIndex, out isTurnAnim);

                    if (isTurnAnim)
                    {
                        if (turnBlocks[animIndex][frameIndex] == null)
                        {
                            turnBlocks[animIndex][frameIndex] = imageBlock;
                        }
                    }
                    else
                    {
                        if (spinBlocks[animIndex][frameIndex] == null)
                        {
                            spinBlocks[animIndex][frameIndex] = imageBlock;
                        }
                    }

                    if (!newKart.KartImages.Images.ContainsKey(imageName))
                    {
                        newKart.KartImages.Images.Add(imageName, new KartImage(images));
                    }

                    MioToImageName.Add(imageBlock, imageName);
                }

                for (int j = 0; j < spinBlocks.Length; j++)
                {
                    //Don't deal with duplicate animations
                    if (j != 0 && spinAnims[j] == spinAnims[j - 1])
                        continue;

                    for (int k = 0; k < spinBlocks[j].Length; k++)
                    {
                        if (spinBlocks[j][k] != null)
                            spinAnims[j].OrderedImageNames.Add(MioToImageName[spinBlocks[j][k]]);
                    }
                }

                for (int j = 0; j < turnBlocks.Length; j++)
                {
                    for (int k = 0; k < turnBlocks[j].Length; k++)
                    {
                        if (turnBlocks[j][k] != null)
                            turnAnims[j].OrderedImageNames.Add(MioToImageName[turnBlocks[j][k]]);
                    }
                }

                for (int j = 0; j < block.CharacterCrashReferences[i].Length; j++)
                {
                    MIO0Block imageBlock = (MIO0Block)block.CharacterCrashReferences[i][j].ReferenceElement;
                    Texture texture = (Texture)imageBlock.Element;

                    List<MK64Image> images;
                    if (textureResults.ImagesByTexture.ContainsKey(texture))
                        images = new List<MK64Image>(textureResults.ImagesByTexture[texture]);
                    else if (MarioKart64ElementHub.Instance.TextureHub.HasImagesForTexture(texture))
                        images = MarioKart64ElementHub.Instance.TextureHub.ImagesForTexture(texture);
                    else
                        throw new Exception();

                    string imageName = images[0].ImageName;

                    crashAnim.OrderedImageNames.Add(imageName);

                    if (!newKart.KartImages.Images.ContainsKey(imageName))
                    {
                        newKart.KartImages.Images.Add(imageName, new KartImage(images));
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

                //Find the tkmk block in either the new images or in the hub
                MK64Image img;
                if ((img = MarioKart64ElementHub.Instance.TextureHub.Images.SingleOrDefault(im => im.ImageName == TextureNames.PORTAIT_NAME_ARRAY[i])) != null)
                    newKart.KartNamePlate = img.Duplicate();
                else if ((img = textureResults.NewImages.SingleOrDefault(im => im.ImageName == TextureNames.PORTAIT_NAME_ARRAY[i])) != null)
                    newKart.KartNamePlate = img.Duplicate();
                else
                    throw new Exception();

                //MiniKartIcon
                if ((img = MarioKart64ElementHub.Instance.TextureHub.Images.SingleOrDefault(im => im.ImageName == TextureNames.MINIMAP_KART_NAME_ARRAY[i])) != null)
                    newKart.KartMiniIcon = img.Duplicate();
                else if ((img = textureResults.NewImages.SingleOrDefault(im => im.ImageName == TextureNames.MINIMAP_KART_NAME_ARRAY[i])) != null)
                    newKart.KartMiniIcon = img.Duplicate();
                else
                    throw new Exception();

                //MiniPortrait
                if ((img = MarioKart64ElementHub.Instance.TextureHub.Images.SingleOrDefault(im => im.ImageName == TextureNames.KART_MINI_PORTRAIT_NAME_ARRAY[i])) != null)
                    newKart.KartMiniPortrait = img.Duplicate();
                else if ((img = textureResults.NewImages.SingleOrDefault(im => im.ImageName == TextureNames.KART_MINI_PORTRAIT_NAME_ARRAY[i])) != null)
                    newKart.KartMiniPortrait = img.Duplicate();
                else
                    throw new Exception();


                newKart.KartStats = results.KartInfoBlock.GetKartStatsFor(i);
                newKart.KartStats.Weight = results.KartWeightTable.KartWeights[i];
                newKart.KartStats.Scale = results.KartScaleTable.KartScales[i];

                MarioKart64ElementHub.Instance.Karts.Add(newKart);
                MarioKart64ElementHub.Instance.SelectedKarts[i] = newKart;
                RomProject.Instance.AddRomItem(newKart);
            }
        }

        public static void ApplyResults(KartReaderResults results)
        {
            foreach (N64DataElement element in results.NewElements)
            {
                if (!RomProject.Instance.Files[0].AddElement(element))
                {
                    throw new Exception();
                }
            }

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

            MarioKart64ElementHub.Instance.KartWeightsTable = results.KartWeightTable;
            MarioKart64ElementHub.Instance.KartScalingTable = results.KartScaleTable;
            MarioKart64ElementHub.Instance.KartStatsTable = results.KartInfoBlock;
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
        public KartGraphicsReferenceBlock KartGraphicsBlock;
        public KartPortraitTable KartPortraitsTable;
        public List<KartPaletteBlock> KartPaletteBlocks;

        public KartScaleTable KartScaleTable;
        public KartWeightTable KartWeightTable;
        public KartInformationBlock KartInfoBlock;

        public KartReaderResults()
        {
            NewElements = new List<N64DataElement>();
            KartPaletteBlocks = new List<KartPaletteBlock>();
        }
    }
}
