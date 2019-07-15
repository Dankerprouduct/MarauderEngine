using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEditor.CustomComponents;
using Microsoft.Xna.Framework;

namespace MarauderEditor
{
    public partial class Form1 : EditorComponent
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartEditor();
        }


    }
}
