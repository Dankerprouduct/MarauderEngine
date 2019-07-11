using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEditor.Projects;


namespace MarauderEditor.Forms.EditorWindow
{
    public class AssetBrowser
    {

        public readonly ImageList ImageList;
        public readonly ListView ListView;
        public readonly GroupBox GroupBox;

        public AssetBrowser(ImageList imageList, ListView listView, GroupBox groupBox)
        {
            ImageList = imageList;
            ListView = listView;
            GroupBox = groupBox;
            ProjectManager.ProjectCreatedEvent += OnProjectLoad;
            ProjectManager.ProjectLoadedEvent += OnProjectLoad;
        }

        private void OnProjectLoad(ProjectEventArgs e)
        {
            LoadDirectory(e.Project.ProjectRoot);
        }


        public void LoadDirectory(string directoryPath = "default")
        {
            if (directoryPath == "default")
                directoryPath = ProjectManager.CurrentProject.ProjectRoot;
            var dir = new DirectoryInfo(directoryPath);
            foreach (var file in dir.GetFiles())
            {
                try
                {
                    ImageList.Images.Add(Image.FromFile(file.FullName));
                }
                catch
                {
                    Console.WriteLine("This is not an image file");
                }
            }
            ListView.View = View.LargeIcon;
            ImageList.ImageSize = new Size(32, 32);
            ListView.LargeImageList = ImageList;
            //or
            //this.listView1.View = View.SmallIcon;
            //this.listView1.SmallImageList = this.imageList1;

            for (int j = 0; j < this.ImageList.Images.Count; j++)
            {
                var item = new ListViewItem {ImageIndex = j};
                ListView.Items.Add(item);
            }
        }


    }
}
