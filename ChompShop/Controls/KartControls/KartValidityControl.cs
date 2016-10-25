using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;

namespace ChompShop.Controls.KartControls
{
    public partial class KartValidityControl : UserControl
    {
        private KartWrapper _kart;

        public KartValidityControl()
        {
            InitializeComponent();

            this.Visible = false;
        }

        public void SetKart(KartWrapper kart)
        {
            _kart = kart;

            if (_kart == null)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;

                SetKartValidity();
                SetNameplateValidity();
                SetPortraitsValidity();
                SetImagesValidity();
                SetAnimationValidity();
                SetAnimationConflictValidity();
            }
        }

        private void SetKartValidity()
        {
            if (_kart.ValidKart)
            {
                lblKartValid.Text = "VALID";
                lblKartValid.ForeColor = Color.Green;
            }
            else
            {
                lblKartValid.Text = "INVALID";
                lblKartValid.ForeColor = Color.Red;
            }
        }

        private void SetNameplateValidity()
        {
            if (_kart.HasValidNamePlate)
            {
                lblKartNameplate.Text = "PRESENT";
                lblKartNameplate.ForeColor = Color.Green;
            }
            else
            {
                lblKartNameplate.Text = "MISSING";
                lblKartNameplate.ForeColor = Color.Red;
            }
        }

        private void SetPortraitsValidity()
        {
            lblKartPortraits.Text = _kart.Kart.KartPortraits.Count + "/17";

            if (_kart.HasValidPortraits)
            {
                lblKartPortraits.ForeColor = Color.Green;
            }
            else
            {
                lblKartPortraits.ForeColor = Color.Red;
            }
        }

        private void SetImagesValidity()
        {
            int imageCount = _kart.Kart.KartImages.Images.Count;

            lblKartImages.Text = imageCount.ToString();

            if (imageCount > 0)
            {
                lblKartImages.ForeColor = Color.Green;
            }
            else
            {
                lblKartImages.ForeColor = Color.Red;
            }
        }

        private void SetAnimationValidity()
        {
            lblKartAnims.Text = _kart.UniqueAnimsCount + "/19";

            if (_kart.HasValidAnimations)
            {
                lblKartAnims.ForeColor = Color.Green;
            }
            else
            {
                lblKartAnims.ForeColor = Color.Red;
            }
        }

        private void SetAnimationConflictValidity()
        {
            if (_kart.HasNonconflictingAnimations)
            {
                lblConflictingAnimations.ForeColor = Color.Green;
                lblConflictingAnimations.Text = "Distinct\n Anims";
            }
            else
            {
                lblConflictingAnimations.ForeColor = Color.Red;
                lblConflictingAnimations.Text = "Overlapping\n Anims";
            }
        }
    }
}
