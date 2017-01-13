using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitstop64.Data.Karts;
using System.IO;
using ChompShop.Data;
using System.Windows.Forms;
using System.ComponentModel;

namespace ChompShop
{
    public static class ChompShopFloor
    {
        public static List<KartWrapper> Karts { get { return _karts; } }
        private static List<KartWrapper> _karts = new List<KartWrapper>();

        public static KartWrapper ReferenceKart { get; private set; }
        private static KartWrapper LoadedReferenceKart { get; set; }
        private static string REF_KART_FILE = @"reference.karts";
        private static BackgroundWorker _worker;

        public static bool NewKart(string newKartName)
        {
            if (Karts.Exists(k => k.Kart.KartName == newKartName))
                return false;

            Karts.Add(new KartWrapper(new KartInfo(newKartName, null, false)));
            ChompShopAlerts.UpdateKartsLoaded();
            return true;
        }

        public static void LoadKarts(string[] kartPaths)
        {
            Pitstop64.Services.ProgressService.StartDialog("Loading Karts");

            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += new DoWorkEventHandler(LoadKartsPayload);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FinishedLoadingKarts);

            if (Pitstop64.Services.ProgressService.StartDialog("Loading Karts"))
                Pitstop64.Services.ProgressService.ProgressCancelled += new Pitstop64.Services.ProgressService.CancelProgressEvent(CancelLoading);
            _worker.RunWorkerAsync(kartPaths);
        }
        
        private static void LoadKartsPayload(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string[] kartPaths = (string[])args.Argument;
            List<KartInfo> karts = new List<KartInfo>();

            foreach(string kartPath in kartPaths)
                karts.AddRange(KartInfo.LoadFromFile(kartPath));

            args.Result = karts;
        }


        private static void FinishedLoadingKarts(object sender, RunWorkerCompletedEventArgs args)
        {
            if (!args.Cancelled && args.Error == null)
            {
                //Load in the karts

                List<KartWrapper> wrappers = new List<KartWrapper>();

                List<KartInfo> karts = (List<KartInfo>)args.Result;

                foreach (KartInfo kart in karts)
                {
                    //Check to see if it exists
                    if (Karts.Exists(k => k.Kart.KartName == kart.KartName))
                    {
                        DialogResult result = MessageBox.Show("Kart \"" + kart.KartName + "\" already exists. Replace previous Kart with this Kart? Unsaved changes will be lost!",
                            "Name Conflict", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                        if (result == DialogResult.Cancel)
                        {
                            //quit out of loading
                            wrappers.Clear();
                            break;
                        }
                        else if (result == DialogResult.No)
                        {
                            continue;
                        }
                        else //Yes
                        {
                            //Remove the kart from the list here
                            Karts.Remove(Karts.Single(k => k.Kart.KartName == kart.KartName));
                        }
                    }

                    wrappers.Add(new KartWrapper(kart));
                }

                if (wrappers.Count > 0)
                {
                    Karts.AddRange(wrappers);
                    ChompShopAlerts.UpdateKartsLoaded();
                }
            }

            Pitstop64.Services.ProgressService.StopDialog();
        }

        private static void CancelLoading()
        {
            _worker.CancelAsync();
        }

        public static void LoadKarts(string kartPath)
        {
            LoadKarts(new string[] { kartPath });
        }

        public static bool HasReferenceKartFile { get { return File.Exists(REF_KART_FILE); } }

        public static bool UsingLoadedReferenceKart { get { return LoadedReferenceKart != null && ReferenceKart == LoadedReferenceKart; } }

        public static void LoadReferenceKart()
        {
            if (LoadedReferenceKart != null)
                ReferenceKart = LoadedReferenceKart;
            else if (File.Exists(REF_KART_FILE))
            {
                List<KartInfo> karts = null;
                try
                {
                    karts = KartInfo.LoadFromFile(REF_KART_FILE);
                }
                catch
                {
                }

                if (karts == null || karts.Count == 0)
                    LoadedReferenceKart = null;
                else
                    LoadedReferenceKart = new KartWrapper(karts[0]);

                ReferenceKart = LoadedReferenceKart;
                ChompShopAlerts.UpdateReferenceKart(ReferenceKart);
            }
        }

        public static void LoadReferenceKart(KartWrapper kart)
        {
            ReferenceKart = kart;
            ChompShopAlerts.UpdateReferenceKart(ReferenceKart);
        }

        public static void SaveReferenceKart()
        {
            if (ReferenceKart != null)
            {
                SaveKartToDisk(REF_KART_FILE, new List<KartWrapper>() { ReferenceKart }, DialogResult.Yes);
                LoadedReferenceKart = ReferenceKart;
            }
        }

        public static void ClearReferenceKart()
        {
            ReferenceKart = null;
            ChompShopAlerts.UpdateReferenceKart(ReferenceKart);
        }

        public static void RemoveKart(KartWrapper kart)
        {
            if (!Karts.Contains(kart))
                return;

            if (kart.IsModified)
            {
                //Double check that they don't mind losing their changes
                if (MessageBox.Show("Kart has unexported changes. Do you really want to remove it from Chomp Shop?", "Warning", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    return;
            }

            Karts.Remove(kart);

            ChompShopAlerts.RemoveKart(kart);
        }

        public static void CopyKart(KartWrapper kart)
        {
            int nameCount = 1;
            string newName = kart.Kart.KartName;
            while (Karts.Exists(k => k.Kart.KartName == newName + nameCount.ToString()))
                nameCount++;
            newName += nameCount.ToString();

            KartWrapper kartWrapper = new KartWrapper(kart, newName);
            Karts.Add(kartWrapper);
            ChompShopAlerts.UpdateKartsLoaded();
        }

        public static void SaveKartToDisk(string kartPath, List<KartWrapper> karts, DialogResult forceSetting = DialogResult.Abort)
        {
            if (File.Exists(kartPath))
            {
                if(forceSetting == DialogResult.Abort)
                    forceSetting = MessageBox.Show("Karts file already exists. Overwrite file? (Yes overwrites, No appends)",
                        "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (forceSetting == DialogResult.Yes)
                    File.Delete(kartPath);
                else if (forceSetting == DialogResult.Cancel)
                    return;
            }
            KartInfo.SaveKarts(kartPath, karts.ConvertAll<KartInfo>(k => k.Kart));
        }

    }
}
