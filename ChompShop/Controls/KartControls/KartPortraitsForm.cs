using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace ChompShop.Controls.KartControls
{
    public partial class KartPortraitsForm : ChompShopWindow
    {
        public bool _initializing;

        public KartPortraitsForm(KartWrapper kart)
            : base(kart)
        {
            InitializeComponent();

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            ResetTitleText();

            //For now
            FakeInitData();

            _initializing = false;
        }

        private void FakeInitData()
        {
            pbPortrait.Image = ((Texture)Kart.Kart.KartPortraits[0].DecodedN64DataElement).Image;
            txtPortraitNum.Text = "1";
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartPortraits; } }

        protected override string TitleText { get { return "Kart Portraits - {0}"; } }
    }
}
