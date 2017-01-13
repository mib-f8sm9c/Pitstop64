using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;
using Pitstop64.Data;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.DataElements;
using Cereal64.Microcodes.F3DEX.DataElements;
using System.Drawing;
using System.ComponentModel;
using Pitstop64.Data.Karts;
using Pitstop64.Services.Hub;
using Pitstop64.Data.Courses;
using Pitstop64.Data.Text;
using System.Windows.Forms;
using Cereal64.Common.DataElements.Encoding;

namespace Pitstop64.Services.Readers
{
    public static class MarioKart64Reader
    {
        private static BackgroundWorker _worker;

        public delegate void ReadingFinishEvent(bool cancelled);

        public static event ReadingFinishEvent ReadingFinished = delegate { };

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

            /*ProgressService.SetMessage("Reading TKMK00 Textures");

            N64DataElement preExistingElement;

            //Now read the different data bits here, if they haven't been read in yet
            for (int i = 0; i < MarioKartRomInfo.TKMK00TextureLocations.Length; i++)
            {
                preExistingElement = RomProject.Instance.Files[0].GetElementAt(MarioKartRomInfo.TKMK00TextureLocations[i].RomOffset);
                if (preExistingElement != null && preExistingElement.GetType() == typeof(UnknownData))
                {
                    ushort alpha = MarioKartRomInfo.TKMK00TextureLocations[i].AlphaColor;
                    int offset = MarioKartRomInfo.TKMK00TextureLocations[i].RomOffset;
                    int length = MarioKartRomInfo.TKMK00TextureLocations[i].Length;

                    TKMK00Block tkmk;

                    byte[] bytes = new byte[length];
                    Array.Copy(rawData, offset, bytes, 0, length);

                    tkmk = new TKMK00Block(offset, bytes, alpha);

                    if (MarioKart64ElementHub.Instance.OriginalTKMK00Blocks.SingleOrDefault(t => t.FileOffset == tkmk.FileOffset) == null)
                    {
                        results.NewElements.Add(tkmk);
                        results.OriginalTKMK00Blocks.Add(tkmk);
                    }
                }
                else if (preExistingElement is TKMK00Block &&
                    MarioKart64ElementHub.Instance.OriginalTKMK00Blocks.SingleOrDefault(t => t.FileOffset == preExistingElement.FileOffset) == null)
                {
                    results.OriginalTKMK00Blocks.Add((TKMK00Block)preExistingElement);
                }
            }
*/
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

            args.Result = results;
        }

        private static void FinishedReadRom(object sender, RunWorkerCompletedEventArgs args)
        {
            if (!args.Cancelled && args.Error == null)
            {
                //Load it into the Rom Project
                ApplyResultsAsync((MarioKart64ReaderResults)args.Result);
            }
            else
            {
                ReadingFinished(true);
            }
        }

        private static void FinishedApplyResults(object sender, RunWorkerCompletedEventArgs args)
        {
            ProgressService.StopDialog();

            if (!args.Cancelled && args.Error == null)
            {
                //Need an invoke here?
                MessageBox.Show("Rom successfully loaded!");

                ReadingFinished(false);
            }
            else
            {
                MessageBox.Show("Error, rom could not successfully load");

                ReadingFinished(true);
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

          //  if (results.TrackResults != null)
          //  {
          //      ProgressService.SetMessage("Splicing track data into Rom object");
                //TrackReader.ApplyResults(results.TrackResults);
        //    }
        
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
        //public TrackReaderResults TrackResults;
        public TextureReaderResults TextureResults;
        public TextBank TextBank;

        public MarioKart64ReaderResults()
        {
            NewElements = new List<N64DataElement>();
        }
    }
}
