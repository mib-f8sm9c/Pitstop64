using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrackShack.Controls
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string titleText = string.Format("V.{0}.{1}.{2}", fvi.ProductMajorPart, fvi.ProductMinorPart, fvi.ProductBuildPart);
            if (fvi.ProductPrivatePart != 0)
                titleText += "." + fvi.ProductPrivatePart.ToString();
            this.lblVersion.Text = titleText;
        }
    }
}
