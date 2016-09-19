using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChompShop.Controls
{
    public partial class ExportKartsForm : ChompShopWindow
    {
        public ExportKartsForm()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            throw new NotImplementedException();
        }

        public override ChompShopWindowType WindowType
        {
            get { return ChompShopWindowType.ExportKarts; }
        }
    }
}
