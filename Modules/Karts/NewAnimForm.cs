using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MK64Pitstop.Modules.Karts
{
    public partial class NewAnimForm : Form
    {
        public NewAnimForm()
        {
            InitializeComponent();
        }

        public string AnimationName { get { return txtAnimName.Text; } }

    }
}
