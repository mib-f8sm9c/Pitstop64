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
    public partial class TextInputForm : Form
    {
        public TextInputForm()
        {
            InitializeComponent();
        }

        public TextInputForm(string labelText, string titleText)
        {
            InitializeComponent();

            this.Text = titleText;
            lblName.Text = labelText;
        }

        public string TextOutput { get { return txtName.Text; } }

        public string LabelText
        {
            get
            {
                return lblName.Text;
            }
            set
            {
                lblName.Text = value;
            }
        }
    }
}
