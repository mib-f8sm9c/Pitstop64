using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChompShop.Data;
using System.ComponentModel;

namespace ChompShop.Controls
{
    [TypeDescriptionProvider(typeof(ChompShopWindowDescriptionProvider<ChompShopWindow, Form>))]
    public class ChompShopWindow : Form
    {
        public ChompShopWindow()
            : this(null)
        {
        }

        public ChompShopWindow(KartWrapper kart)
        {
            Kart = kart;

            ChompShopAlerts.KartNameChanged += new ChompShopAlerts.KartNameChangedEvent(ChompShopAlerts_KartNameChanged);
        }

        private void ChompShopAlerts_KartNameChanged(KartWrapper wrapper)
        {
            if (Kart == wrapper)
            {
                //Need invoke?
                ResetTitleText();
            }

            KartNameUpdated(wrapper);
        }

        protected virtual void KartNameUpdated(KartWrapper wrapper)
        {

        }

        public virtual void InitData()
        {
            throw new NotImplementedException();
        }

        protected void ResetTitleText()
        {
            if (Kart != null)
                this.Text = string.Format(TitleText, Kart.Kart.KartName);
            else
                this.Text = TitleText;
        }

        protected virtual string TitleText { get { throw new NotImplementedException(); } }

        public virtual ChompShopWindowType WindowType { get { throw new NotImplementedException(); } }

        public KartWrapper Kart { get; protected set; }

        protected ChompShopForm ChompShopForm { get { return (ChompShopForm)this.MdiParent; } }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChompShopWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Name = "ChompShopWindow";
            this.ResumeLayout(false);

        }
    }

    //Needed this to fix a designer bug: see 
    // http://stackoverflow.com/questions/1620847/how-can-i-get-visual-studio-2008-windows-forms-designer-to-render-a-form-that-im/2406058#2406058
    public class ChompShopWindowDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider
    {
        public ChompShopWindowDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(TAbstract)))
        {
        }

        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType == typeof(TAbstract))
                return typeof(TBase);

            return base.GetReflectionType(objectType, instance);
        }

        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            if (objectType == typeof(TAbstract))
                objectType = typeof(TBase);

            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }
}
