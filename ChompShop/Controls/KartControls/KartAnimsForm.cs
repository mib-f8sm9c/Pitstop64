using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;

namespace ChompShop.Controls.KartControls
{
    public partial class KartAnimsForm : ChompShopWindow
    {
        public bool _initializing;

        public KartAnimsForm(KartWrapper kart)
            : base (kart)
        {
            InitializeComponent();

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            ResetTitleText();

            //

            _initializing = false;
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartAnimations; } }

        protected override string TitleText { get { return "Kart Animations - {0}"; } }
    }
}
