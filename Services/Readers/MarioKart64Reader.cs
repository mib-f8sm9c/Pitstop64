﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using MK64Pitstop.Data;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements;
using System.Drawing;
using System.ComponentModel;
using MK64Pitstop.Data.Karts;
using MK64Pitstop.Services.Hub;
using MK64Pitstop.Data.Tracks;
using MK64Pitstop.Data.Text;
using System.Windows.Forms;
using Cereal64.Common.DataElements.Encoding;

namespace MK64Pitstop.Services.Readers
{
    public static class MarioKart64Reader
    {
        private static BackgroundWorker _worker;

        public static void ReadRom()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += new DoWorkEventHandler(ReadRomPayload);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FinishedReadRom);

            if(ProgressService.StartDialog("Loading up MK64 Rom"))
                ProgressService.ProgressCancelled += new ProgressService.CancelProgressEvent(CancelReading);
            _worker.RunWorkerAsync();
        }

        private static void ReadRomPayload(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            MarioKart64ReaderResults results = new MarioKart64ReaderResults();

            //The rom should be loaded as the first file in the rom project
            byte[] rawData = RomProject.Instance.Files[0].GetAsBytes();

            //NOTE: THIS IS HOW YOU DO A CANCELLATION
            //if ((worker.CancellationPending == true))
            //{
            //    args.Cancel = true;
            //    return;
            //}

            //Image testing code
            //ImageMIO0Block block = ImageMIO0Block.ReadImageMIO0BlockFrom(rawData, 8000536);//new ImageMIO0Block(8000536, rawData);
            //if(block.DecodedData != null)
            //{
            //    //block.DecodedN64DataElement = null;
            //    Texture newTexture = new Texture(0, block.DecodedData, Texture.ImageFormat.RGBA, Texture.PixelInfo.Size_16b, 128, 78);

            //    newTexture.Image.Save("testing.png");
            //    //kartPortraits[i].ImageReference.DecodedN64DataElement = newTexture;
            //}

            //Text bank!
            TextBankBlock textBankBlock = null;
            TextReferenceBlock textReferenceBlock = null;
            bool previouslyLoadedText = false;

            //To do: add a function to automate this pre-existing check, like
            //        bool hasExistingElement<T:N64DataElement> (offset, out T)
            N64DataElement preExistingElement = RomProject.Instance.Files[0].GetElementAt(TextBankBlock.TEXT_BLOCK_START);
            if (preExistingElement != null && preExistingElement.GetType() == typeof(UnknownData))
            {
                byte[] bytes = new byte[TextBankBlock.TEXT_BLOCK_LENGTH];
                Array.Copy(rawData, TextBankBlock.TEXT_BLOCK_START, bytes, 0, bytes.Length);

                textBankBlock = new TextBankBlock(TextBankBlock.TEXT_BLOCK_START, bytes);
                results.NewElements.Add(textBankBlock);
            }
            else if (preExistingElement.GetType() == typeof(TextBankBlock))
            {
                previouslyLoadedText = true;
                textBankBlock = (TextBankBlock)preExistingElement;
            }

            preExistingElement = RomProject.Instance.Files[0].GetElementAt(TextReferenceBlock.TEXT_REFERENCE_SECTION_1);
            if (preExistingElement != null && preExistingElement.GetType() == typeof(UnknownData))
            {

                byte[] bytes = new byte[TextReferenceBlock.TEXT_REFERENCE_END - TextReferenceBlock.TEXT_REFERENCE_SECTION_1];
                Array.Copy(rawData, TextReferenceBlock.TEXT_REFERENCE_SECTION_1, bytes, 0, bytes.Length);

                textReferenceBlock = new TextReferenceBlock(TextReferenceBlock.TEXT_REFERENCE_SECTION_1, bytes);
                results.NewElements.Add(textReferenceBlock);
            }
            else if (preExistingElement.GetType() == typeof(TextReferenceBlock))
            {
                previouslyLoadedText = true;
                textReferenceBlock = (TextReferenceBlock)preExistingElement;
            }

            if (textBankBlock != null && textReferenceBlock != null)
            {
                TextBank textBank = new TextBank(textBankBlock, textReferenceBlock, !previouslyLoadedText);
                results.TextBank = textBank;
            }

            TextureReader.ReadRom(worker, rawData, results);

            KartReader.ReadRom(worker, rawData, results);

            //TrackReader.ReadRom(worker, rawData, results);

            //debug stuff
            //ImageMIO0Block imblock = ImageMIO0Block.ReadImageMIO0BlockFrom(data, 0x963EF0);

            //imblock.ImageName = "X";

            args.Result = results;
        }

        private static void FinishedReadRom(object sender, RunWorkerCompletedEventArgs args)
        {
            if (!args.Cancelled && args.Error == null)
            {
                //Load it into the Rom Project
                ApplyResultsAsync((MarioKart64ReaderResults)args.Result);
            }
        }

        private static void FinishedApplyResults(object sender, RunWorkerCompletedEventArgs args)
        {
            ProgressService.StopDialog();

            if (!args.Cancelled && args.Error == null)
            {
                MessageBox.Show("Rom successfully loaded!");
            }
            else
            {
                MessageBox.Show("Error, rom could not successfully load");
            }
        }

        private static void CancelReading()
        {
            _worker.CancelAsync();
            MessageBox.Show("Rom loading has been cancelled");
        }

        public static void ApplyResultsAsync(MarioKart64ReaderResults results)
        {
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += new DoWorkEventHandler(ApplyResults);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FinishedApplyResults);

            _worker.RunWorkerAsync(results);
        }

        public static void ApplyResults(object sender, DoWorkEventArgs args)
        {
            MarioKart64ReaderResults results = (MarioKart64ReaderResults)args.Argument;

            //To-do: add an error report for any sort of exceptions during saving?
            ProgressService.SetMessage("Splicing data elements into Rom object");
            foreach (N64DataElement element in results.NewElements)
                RomProject.Instance.Files[0].AddElement(element);

            if (results.KartResults != null)
            {
                ProgressService.SetMessage("Splicing kart data into Rom object");
                KartReader.ApplyResults(results.KartResults);
            }

            if (results.TrackResults != null)
            {
                ProgressService.SetMessage("Splicing track data into Rom object");
                TrackReader.ApplyResults(results.TrackResults);
            }

            if (results.TextBank != null)
            {
                ProgressService.SetMessage("Splicing text data into Rom object");
                MarioKart64ElementHub.Instance.TextBank = results.TextBank;
            }

            if (results.TextureResults != null)
            {
                ProgressService.SetMessage("Splicing texture data into Rom object");
                TextureReader.ApplyResults(results.TextureResults);
            }

            //Does this really belong here?
            if (!RomProject.Instance.Items.Contains(MarioKart64ElementHub.Instance))
                RomProject.Instance.AddRomItem(MarioKart64ElementHub.Instance);

            ProgressService.SetMessage("Finished!");
        }
    }

    public class MarioKart64ReaderResults
    {
        public List<N64DataElement> NewElements;
        public KartReaderResults KartResults;
        public TrackReaderResults TrackResults;
        public TextureReaderResults TextureResults;
        public TextBank TextBank;

        public MarioKart64ReaderResults()
        {
            NewElements = new List<N64DataElement>();
        }
    }
}
