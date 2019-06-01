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


namespace TileEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            widthTextbox.Text = "10";
            heightTextbox.Text = "10";

            editor1.ReferenceForm1 = this; 
        }

        public void PopulateListView()
        {
            listView1.Items.Clear();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            
            for (int i = 0; i < editor1.GetBitmaps().Count; i++)
            {
                imageList.Images.Add(i.ToString(), editor1.GetBitmaps()[i]);
            }

            listView1.View = View.LargeIcon;
            
            listView1.LargeImageList = imageList;

            for (int i = 0; i < imageList.Images.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                //
                listView1.Items.Add(lvi); 
                Console.WriteLine("added item "+ i );
            }

        }

        private void editor1_Click(object sender, EventArgs e)
        {
            
        }

        private void listviewPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetSelectedItem(listView1) != null)
            {
                editor1.currentSelectedItem = listView1.SelectedItems[0].Index;
                Console.WriteLine(editor1.currentSelectedItem +" : " + GetSelectedItem(listView1).Index);
            }
        }

        internal static ListViewItem GetSelectedItem(ListView listView1)
        {
            return (listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0] : null);
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            PopulateListView();
            try
            {
                editor1.CreateMap(Convert.ToInt32(widthTextbox.Text), Convert.ToInt32(heightTextbox.Text));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
            }
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {

             editor1.Save(textBox1.Text);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
