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
    public partial class NewKartForm : Form
    {
        public NewKartForm()
        {
            InitializeComponent();
        }

        public string KartName { get { return txtName.Text; } }
    }
}
