using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MK64Pitstop.Services
{
    public partial class ProgressDialog : Form
    {
        public ProgressDialog(string message)
        {
            InitializeComponent();
            lblStatus.Text = message;
        }

        public string Message
        {
            get
            {
                return lblStatus.Text;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => { lblStatus.Text = value; }));
                }
                else
                    lblStatus.Text = value;
            }
        }

        public void CloseProgressDialog()
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
