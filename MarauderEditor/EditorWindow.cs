using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEditor.Forms.EditorWindow;
using MarauderEditor.Forms.File;
using MarauderEditor.Projects;

namespace MarauderEditor
{
    public partial class EditorWindow : Form
    {

        public AssetBrowser AssetBrowser;

        public EditorWindow()
        {
            InitializeComponent();
            InitializeComponentReferences();
        }
         
        private void EditorWindow_Load(object sender, EventArgs e)
        {
            Text = "Marauder Editor"; 
        }

        private void InitializeComponentReferences()
        {
            //AssetBrowser = new AssetBrowser(AssetBrowserImageList, AssetBroswerListView,AssetBrowserGroupBox);
        }
        #region Editor Window Components

        #region AssetBrowser

       
        #endregion

        #endregion


        #region Menu Bar

        #region File

        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newProjFrom = new NewProjectForm();
            newProjFrom.ShowDialog();

        }




        #endregion

        #endregion

        private void MEditor1_Click(object sender, EventArgs e)
        {

        }
    }
}
