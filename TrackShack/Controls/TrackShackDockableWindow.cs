using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrackShack.Data;
using System.Windows.Forms.Integration;
using Xceed.Wpf.AvalonDock.Layout;

namespace TrackShack.Controls
{
    [TypeDescriptionProvider(typeof(TrackShackDockableWindowDescriptionProvider<TrackShackDockableWindow, UserControl>))]
    public partial class TrackShackDockableWindow : UserControl
    {
        public bool IsActive { get; private set; }

        public LayoutContent ParentLayout { get; set; }

        public TrackShackDockableWindow()
        {
            TrackShackAlerts.TrackNameChanged += new TrackShackAlerts.TrackNameChangedEvent(TrackShackAlerts_TrackNameChanged);
            IsActive = false;
            ParentLayout = null;
        }
        
        private void TrackShackAlerts_TrackNameChanged(TrackWrapper wrapper)
        {
            //if (Track == wrapper)
            //{
            //    //Need invoke?
            //    //ResetTitleText();
            //}

            TrackNameUpdated(wrapper);
        }

        protected virtual void TrackNameUpdated(TrackWrapper wrapper)
        {

        }

        public void OnIsActiveChanged(object sender, EventArgs e)
        {
            var layout = sender as Xceed.Wpf.AvalonDock.Layout.LayoutContent;
            if (layout != null)
            {
                IsActive = layout.IsActive;
            }
        }

        public virtual void InitData()
        {
            throw new NotImplementedException();
        }

        protected virtual string TitleText { get { throw new NotImplementedException(); } }

        public virtual TrackShackDockableWindowType WindowType { get { throw new NotImplementedException(); } }

        protected TrackShackForm ChompShopForm { get { return (TrackShackForm)this.ParentForm; } }

        protected WindowsFormsHost GetFormsHost { get { if(_formsHost == null) _formsHost = new WindowsFormsHost() { Child = this }; return _formsHost; } }
        private WindowsFormsHost _formsHost = null;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TrackShackWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Name = "TrackShackDockableWindow";
            this.ResumeLayout(false);

        }
    }

    //Needed this to fix a designer bug: see 
    // http://stackoverflow.com/questions/1620847/how-can-i-get-visual-studio-2008-windows-forms-designer-to-render-a-form-that-im/2406058#2406058
    public class TrackShackDockableWindowDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider
    {
        public TrackShackDockableWindowDescriptionProvider()
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
