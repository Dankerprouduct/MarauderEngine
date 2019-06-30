using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEditor.Forms.File;

namespace MarauderEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Marauder Editor"; 
        }

        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newProjFrom = new NewProjectForm();
            newProjFrom.ShowDialog();

        }
    }
}
