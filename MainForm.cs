using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements;
using System.IO;
using MK64Pitstop.Services;
using MK64Pitstop.Modules;
using System.Reflection;
using Cereal64.Common.Utils;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using MK64Pitstop.Services.Readers;
using MK64Pitstop.Services.Hub;
using MK64Pitstop.Data.Karts;

namespace MK64Pitstop
{
    public partial class MainForm : Form
    {
        //For now, we'll start with a program that will load the rom, deserialize it, and then output the romproj next to the rom file.
        // Make sure to keep it forwards compatible (if you load a previous romproject, it may not have features deserialized by the current,
        // it needs to be able to be run through the deserializing again, but without upsetting anything that has been changed.
        
        private string _loadedFilePath = null;

        public ModuleFactory.Modules SelectedModule
        {
            get
            {
                return _selectedModule;
            }
            set
            {
                _selectedModule = value;
                UpdateSelectedModule();
            }
        }
        private ModuleFactory.Modules _selectedModule; //Don't use this please!

        private void UpdateSelectedModule()
        {
            pnlCurrentView.Controls.Clear();
            IModule module = ModuleFactory.GetModule(SelectedModule);
            pnlCurrentView.Controls.Add(module.Control);
            module.Control.Dock = DockStyle.Fill;
            module.UpdateRomData();
        }

        public MainForm()
        {
            InitializeComponent();
            SelectedModule = ModuleFactory.Modules.About;
            N64DataElementFactory.AddN64ElementsFromAssembly(Assembly.GetExecutingAssembly());
            N64DataElementFactory.AddN64ElementsFromAssembly(Assembly.GetAssembly(typeof(F3DEXCommand)));
            RomItemFactory.AddRomItemsFromAssembly(Assembly.GetExecutingAssembly());
            RomItemFactory.AddRomItemsFromAssembly(Assembly.GetAssembly(typeof(F3DEXCommand)));

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            this.Text = string.Format("{0} V.{1}.{2}.{3}", fvi.ProductName, fvi.ProductMajorPart, fvi.ProductMinorPart, fvi.ProductBuildPart);
        }

        public void NewProject()
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RomProject.Instance.Reset();

                UnknownData data = new UnknownData(0, File.ReadAllBytes(openFileDialog.FileName));
                RomFile file = new RomFile(Path.GetFileNameWithoutExtension(openFileDialog.FileName),
                    RomProject.Instance.Files.Count + 1, data);
                RomProject.Instance.AddRomFile(file);

                statusBarFile.Text = "New Project";

                MarioKart64Reader.ReadRom();

                UpdateSelectedModule();
            }
        }

        public void LoadProject()
        {
            if (openProjectDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadProjectAsync(openProjectDialog.FileName);
            }
        }

        BackgroundWorker _worker;

        private void LoadProjectAsync(string fileName)
        {
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += new DoWorkEventHandler(LoadProjectPayload);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FinishedReadProject);

            if (ProgressService.StartDialog("Loading up Rom Project"))
                ProgressService.ProgressCancelled += new ProgressService.CancelProgressEvent(CancelReading);
            _worker.RunWorkerAsync(fileName);
        }

        private void LoadProjectPayload(object sender, DoWorkEventArgs args)
        {
            RomProject.Load(openProjectDialog.FileName);
        }

        private void FinishedReadProject(object sender, RunWorkerCompletedEventArgs args)
        {
            if (!args.Cancelled && args.Error == null)
            {
                //If successful, mark flags and put the filename in the status bar
                if (InvokeRequired)
                {
                    this.Invoke((Action)(() =>
                    {
                        statusBarFile.Text = Path.GetFileNameWithoutExtension(openProjectDialog.FileName);
                        _loadedFilePath = openProjectDialog.FileName;
                    }));
                }
                else
                {
                    statusBarFile.Text = Path.GetFileNameWithoutExtension(openProjectDialog.FileName);
                    _loadedFilePath = openProjectDialog.FileName;
                }

                //Load it into the Rom Project
                ApplyProjectAsync();
            }
            else
            {
                ProgressService.StopDialog();
                MessageBox.Show("Error, could not open project");
            }
        }

        private void ApplyProjectAsync()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += new DoWorkEventHandler(ApplyProjectPayload);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FinishedApplyProject);

            if (ProgressService.StartDialog("Splicing in Rom data"))
                ProgressService.ProgressCancelled += new ProgressService.CancelProgressEvent(CancelReading);
            _worker.RunWorkerAsync();
        }

        private void ApplyProjectPayload(object sender, DoWorkEventArgs args)
        {
            foreach (RomItem item in RomProject.Instance.Items)
            {
                if (item is KartInfo)
                {
                    MarioKart64ElementHub.Instance.Karts.Add((KartInfo)item); 
                }
            }

            MarioKart64ElementHub.Instance.LoadFromXML();

            MarioKart64Reader.ReadRom();
        }

        private void FinishedApplyProject(object sender, RunWorkerCompletedEventArgs args)
        {
            if (!args.Cancelled && args.Error == null)
            {
                //If successful, mark flags and put the filename in the status bar
                if (InvokeRequired)
                {
                    this.Invoke((Action)(() =>
                    {
                        UpdateSelectedModule();
                    }));
                }
                else
                {
                    UpdateSelectedModule();
                }
            }
            else
            {
                ProgressService.StopDialog();
                MessageBox.Show("Error, could not open project");
            }
        }

        private void CancelReading()
        {
            _worker.CancelAsync();
            MessageBox.Show("Project loading has been cancelled");
        }

        public void SaveProject(bool newFile)
        {
            string path = _loadedFilePath;
            if (newFile || string.IsNullOrWhiteSpace(path))
            {
                if (saveProjectDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                path = saveProjectDialog.FileName;
            }

            RomProject.Instance.MoveRomItem(MarioKart64ElementHub.Instance, RomProject.Instance.Items.Count - 1);

            RomProject.Save(path);

            _loadedFilePath = path;
            statusBarFile.Text = Path.GetFileNameWithoutExtension(_loadedFilePath);

            if (newFile)
                MessageBox.Show("Project successfully saved!");
        }

        public void ExportRom()
        {
            saveFileDialog.FileName = Path.GetFileName(RomProject.Instance.Files[0].FileName);
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Apply changes here
                MarioKart64ElementHub.Instance.SaveKartInfo();

                byte[] newRomData = RomProject.Instance.Files[0].GetAsBytes();
                if (N64Sums.FixChecksum(newRomData)) //In the future, save this CRC to the actual project data
                {
                    File.WriteAllBytes(saveFileDialog.FileName, newRomData);
                    MessageBox.Show("Rom successfully exported!");
                }
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject(false);
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject(true);
        }

        private void exportROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportRom();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check for changes/save before quitting

            this.Close();
        }

        private void texturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModule = ModuleFactory.Modules.Textures;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModule = ModuleFactory.Modules.About;
        }

        private void kartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModule = ModuleFactory.Modules.Karts;
        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModule = ModuleFactory.Modules.Courses;
        }

        private void romInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModule = ModuleFactory.Modules.Info;
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModule = ModuleFactory.Modules.Text;
        }
    }
}
