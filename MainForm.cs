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
            this.Text = string.Format("Mario Kart 64 Pitstop V{0}", fvi.ProductVersion);
        }

        public void NewProject()
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RomProject.Instance.Reset();

                UnknownData data = new UnknownData(0, File.ReadAllBytes(openFileDialog.FileName));
                RomFile file = new RomFile(openFileDialog.FileName, RomProject.Instance.Files.Count + 1, data);
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
                RomProject.Load(openProjectDialog.FileName);

                //If successful, mark flags and put the filename in the status bar
                statusBarFile.Text = Path.GetFileNameWithoutExtension(openProjectDialog.FileName);

                MarioKart64Reader.ReadRom();

                UpdateSelectedModule();
            }
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

            RomProject.Save(path);

            _loadedFilePath = path;
            statusBarFile.Text = Path.GetFileNameWithoutExtension(_loadedFilePath);
        }

        public void ExportRom()
        {
            saveFileDialog.FileName = Path.GetFileName(RomProject.Instance.Files[0].FileName);
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] newRomData = RomProject.Instance.Files[0].GetAsBytes();
                if(N64Sums.FixChecksum(newRomData)) //In the future, save this CRC to the actual project data
                    File.WriteAllBytes(saveFileDialog.FileName, newRomData);
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
    }
}
