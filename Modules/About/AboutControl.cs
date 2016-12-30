using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;

namespace Pitstop64.Modules.About
{
    public partial class AboutControl : UserControl
    {
        private const string ABOUT_TEXT =
@"Pitstop64 created by mib_f8sm9c
Major thanks to QueueRAM, Shygoo and Rena";

        public AboutControl()
        {
            InitializeComponent();

            txtAbout.Text = ABOUT_TEXT;

            UpdateControl();
        }

        public void UpdateControl()
        {
        }
    }
}
