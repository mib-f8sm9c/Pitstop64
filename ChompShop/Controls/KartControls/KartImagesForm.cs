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
    public partial class KartImagesForm : ChompShopWindow
    {
        public bool _initializing;

        public KartImagesForm(KartWrapper kart)
        {
            InitializeComponent();
            Kart = kart;

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            this.Text = "Kart Images - " + Kart.Kart.KartName;

            //

            _initializing = false;
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartImages; } }
    }
}
