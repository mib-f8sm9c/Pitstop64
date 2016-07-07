using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Common.Rom;

namespace MK64Pitstop.Modules.Courses
{
    public partial class CourseControl : UserControl
    {

        public CourseControl()
        {
            InitializeComponent();

            UpdateControl();
        }

        public void UpdateControl()
        {
        }

        private void btnResizeRom_Click(object sender, EventArgs e)
        {
        }
    }
}
