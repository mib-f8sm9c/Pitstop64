using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using Pitstop64.Data;
using System.IO;

namespace ChompShop.Controls.KartControls
{
    public partial class KartStatsForm : ChompShopWindow
    {
        public bool _initializing;

        public KartStatsForm(KartWrapper kart)
            : base(kart)
        {
            InitializeComponent();

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            this.ResetTitleText();

            ClearView();

            if (Kart == null)
                return;

            //Stuff goes here
            kartStatsControl.Stats = Kart.Kart.KartStats;

            _initializing = false;
        }

        private void ClearView()
        {

        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartStats; } }

        protected override string TitleText { get { return "Kart Stats - {0}"; } }

        private void btnApply_Click(object sender, EventArgs e)
        {
            kartStatsControl.Save();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            kartStatsControl.ResetForm();
        }

    }
}
