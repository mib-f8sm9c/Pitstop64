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

        protected override string TitleText { get { return "Kart Name - {0}"; } }

        private void txtKartName_TextChanged(object sender, EventArgs e)
        {
            if(_initializing)
                return;

            //Change the name and alert everywhere
            string oldName = Kart.Kart.KartName;
            string newName = txtKartName.Text;

            if(oldName == newName)
                return;

            //Here, double check for valid characters?

            foreach (KartWrapper wrapper in ChompShopFloor.Karts)
            {
                if (wrapper.Kart.KartName == newName)
                {
                    MessageBox.Show("Name already exists. Please make a new name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _initializing = true;
                    txtKartName.Text = oldName;
                    _initializing = false;

                    return;
                }
            }

            Kart.Kart.KartName = newName;
            ChompShopAlerts.UpdateKartName(Kart);
        }
    }
}
