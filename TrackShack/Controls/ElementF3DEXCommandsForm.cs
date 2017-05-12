using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DEX;

namespace TrackShack.Controls
{
    public partial class ElementF3DEXCommandsForm : Form
    {
        public ElementF3DEXCommandsForm(F3DEXGraphicsElement element)
        {
            InitializeComponent();

            f3DEXEditor.Commands = new Cereal64.Microcodes.F3DEX.DataElements.F3DEXCommandCollection(0, element.Commands);
        }
    }
}
