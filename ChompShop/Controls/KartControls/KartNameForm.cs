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
    public partial class KartNameForm : ChompShopWindow
    {
        public bool _initializing;

        public KartNameForm(KartWrapper kart)
        {
            InitializeComponent();
            Kart = kart;

            InitData();
        }

        public override void InitData()
        {
            _initializing = true;

            this.Text = "Kart Name - " + Kart.Kart.KartName;

            ClearView();

            if (Kart == null)
                return;

            txtKartName.Text = Kart.Kart.KartName;
            if(Kart.Kart.KartNamePlate != null)
                pbNamePlate.Image = Kart.Kart.KartNamePlate.Image;

            _initializing = false;
        }

        private void ClearView()
        {
            txtKartName.Text = string.Empty;
            pbNamePlate.Image = null;
        }

        public override ChompShopWindowType WindowType { get { return ChompShopWindowType.KartName; } }
    }
}
