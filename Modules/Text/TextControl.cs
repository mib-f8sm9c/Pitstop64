using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;
using Pitstop64.Services.Hub;
using Pitstop64.Data.Text;
using System.IO;

namespace Pitstop64.Modules.Text
{
    public partial class TextControl : UserControl
    {
        private List<string> _gameTexts;
        private int _freeSpace;

        private bool Modified { get { return _modified; } set { _modified = value;  UpdateModified(); } }
        private bool _modified;

        public TextControl()
        {
            InitializeComponent();

            _gameTexts = new List<string>();

            UpdateControl();
        }

        public void UpdateControl()
        {
            UpdateFromTextBank();
        }

        public void UpdateFromTextBank()
        {
            _gameTexts.Clear();
            cbGameText.Items.Clear();
            foreach (TextBank.TextType type in Enum.GetValues(typeof(TextBank.TextType)))
            {
                cbGameText.Items.Add(type.ToString());
                _gameTexts.Add(MarioKart64ElementHub.Instance.TextBank.GetText(type));
            }

            if(cbGameText.Items.Count > 0)
                cbGameText.SelectedIndex = 0;

            _freeSpace = MarioKart64ElementHub.Instance.TextBank.FreeBankSpace;
            UpdateGameTextSpace();
        }

        private void UpdateGameTextSpace()
        {
            txtGameTextSpace.Text = _freeSpace.ToString();
        }

        private void UpdateModified()
        {
            btnKartsApply.Enabled = _modified;
        }

        private void SaveChanges()
        {
            foreach (TextBank.TextType type in Enum.GetValues(typeof(TextBank.TextType)))
            {
                MarioKart64ElementHub.Instance.TextBank.SetText(type, _gameTexts[(int)type]);
            }

            MarioKart64ElementHub.Instance.TextBank.SaveTextToBlocks();
            Modified = false;
            UpdateFromTextBank();
        }

        private void btnKartsApply_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void btnKartsCancel_Click(object sender, EventArgs e)
        {
            UpdateFromTextBank();
        }

        private void btnGameTextApply_Click(object sender, EventArgs e)
        {
            if (cbGameText.SelectedIndex < 0)
                return;

            int newSpace = _freeSpace - (txtGameText.Text.Length - _gameTexts[cbGameText.SelectedIndex].Length);
            _gameTexts[cbGameText.SelectedIndex] = txtGameText.Text;
            _freeSpace = newSpace;

            UpdateGameTextSpace();

            Modified = true;
        }

        private void cbGameText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGameText.SelectedIndex < 0)
                return;

            txtGameText.Text = _gameTexts[cbGameText.SelectedIndex];
        }
    }
}
