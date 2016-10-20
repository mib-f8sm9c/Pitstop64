using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using System.Runtime.InteropServices;
using ChompShop.Controls.KartControls;

namespace ChompShop.Controls
{
    public enum ChompShopWindowType
    {
        LoadedKarts,
        ExportKarts,
        ReferenceKart,
        KartName,
        KartPortraits,
        KartImages,
        KartAnimations
    }

    //Maintain the different windows being opened/closed and watch out for unsaved changes!
    public class ControlController
    {
        //Code for restoring a window from being minimized
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        private const uint SW_RESTORE = 0x09;

        private Dictionary<ChompShopWindowType, ChompShopWindow> SingleForms;
        private Dictionary<KartWrapper, Dictionary<ChompShopWindowType, ChompShopWindow>> KartForms;

        private ChompShopForm _parentForm;

        public ControlController(ChompShopForm parent)
        {
            SingleForms = new Dictionary<ChompShopWindowType, ChompShopWindow>();
            KartForms = new Dictionary<KartWrapper, Dictionary<ChompShopWindowType, ChompShopWindow>>();

            _parentForm = parent;
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            //Double check if there are unsaved changes?
        }

        private void HandleFormClosed(object sender, FormClosedEventArgs e)
        {
            //Remove from the dictionaries
            ChompShopWindow window = (ChompShopWindow)sender;

            if (window.Kart == null)
                SingleForms.Remove(window.WindowType);
            else
            {
                if (KartForms.ContainsKey(window.Kart))
                    KartForms[window.Kart].Remove(window.WindowType);
            }
        }

        public void ShowSingleForm(ChompShopWindowType type)
        {
            if (SingleForms.ContainsKey(type))
            {
                ChompShopWindow form = SingleForms[type];

                if (form.WindowState == FormWindowState.Minimized)
                    ShowWindow(form.Handle, SW_RESTORE);

                form.BringToFront();
            }
            else
            {
                ChompShopWindow form = GenerateSingleForm(type);

                SingleForms.Add(type, form);
                form.MdiParent = _parentForm;
                form.FormClosing += HandleFormClosing;
                form.FormClosed += HandleFormClosed;
                form.Show();
            }

        }

        public void ShowKartForm(KartWrapper kart, ChompShopWindowType type)
        {
            ChompShopWindow form;
            if (KartForms.ContainsKey(kart))
            {
                if(KartForms[kart].ContainsKey(type))
                {
                    form = KartForms[kart][type];

                    if (form.WindowState == FormWindowState.Minimized)
                        ShowWindow(form.Handle, SW_RESTORE);

                    form.BringToFront();

                    return;
                }
                else
                {
                    form = GenerateKartForm(kart, type);
                }
            }
            else
            {
                KartForms.Add(kart, new Dictionary<ChompShopWindowType,ChompShopWindow>());
                form = GenerateKartForm(kart, type);
            }

            KartForms[kart].Add(type, form);
            form.MdiParent = _parentForm;
            form.FormClosing += HandleFormClosing;
            form.FormClosed += HandleFormClosed;
            form.Show();
        }

        public ChompShopWindow GenerateSingleForm(ChompShopWindowType type)
        {
            switch (type)
            {
                case ChompShopWindowType.LoadedKarts:
                    return new LoadedKartsForm();
                case ChompShopWindowType.ExportKarts:
                    return new ExportKartsForm();
                case ChompShopWindowType.ReferenceKart:
                    return new ReferenceKartForm();
            }

            return null;
        }

        public ChompShopWindow GenerateKartForm(KartWrapper wrapper, ChompShopWindowType type)
        {
            switch (type)
            {
                case ChompShopWindowType.KartName:
                    return new KartInfoForm(wrapper);
                case ChompShopWindowType.KartPortraits:
                    return new KartPortraitsForm(wrapper);
                case ChompShopWindowType.KartImages:
                    return new KartImagesForm(wrapper);
                case ChompShopWindowType.KartAnimations:
                    return new KartAnimsForm(wrapper);
            }

            return null;
        }

        public bool SingleFormIsOpen(ChompShopWindowType type)
        {
            return SingleForms.ContainsKey(type);
        }

        public bool KartFormIsOpen(KartWrapper kart, ChompShopWindowType type)
        {
            if (!KartForms.ContainsKey(kart))
                return false;

            return KartForms[kart].ContainsKey(type);
        }

        public void ClearKartForms(KartWrapper kart)
        {
            if (KartForms.ContainsKey(kart))
            {
                List<ChompShopWindow> forms = new List<ChompShopWindow>(KartForms[kart].Values);
                foreach (ChompShopWindow form in forms)
                {
                    form.Close();
                    KartForms[kart].Remove(form.WindowType);
                }
            }
        }
    }
}