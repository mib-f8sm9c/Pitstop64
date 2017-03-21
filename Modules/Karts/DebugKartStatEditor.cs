using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pitstop64.Services.Hub;

namespace Pitstop64.Modules.Karts
{
    public partial class DebugKartStatEditor : Form
    {
        public DebugKartStatEditor()
        {
            InitializeComponent();

            kartStatsControl1.Stats = MarioKart64ElementHub.Instance.SelectedKarts[0].KartStats;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kartStatsControl1.Save();

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
