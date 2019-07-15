using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarauderEditor.CustomComponents
{
    

    public class MarauderMenuStrip: MenuStrip
    {
        public static ToolStripButton EraserToggle;
        public static ToolStripButton BrushToggle;
        public static ToolStripButton CursorToggle;
        public static ToolStripButton SnappingToggle;
        public static ToolStripTextBox SnappingAmmount;
        public MarauderMenuStrip(Control parent)
        {
            Parent = parent;
            Renderer = new MenuRenderer();
            Font = new Font("Calibri", 12, FontStyle.Regular);
            ForeColor = Color.White;
            Dock = DockStyle.Top;
            BackColor = ColorScheme.ToolStripColor;

            ToolStripMenuItem file = new ToolStripMenuItem("File",null, null,"File");
            file.BackColor = ColorScheme.MenuItemColor;
            //file.Padding = padding;
            ToolStripMenuItem open = new ToolStripMenuItem("Load Scene");
            open.BackColor = ColorScheme.MenuItemColor;
            open.ForeColor = Color.White;
            open.Click += OpenScene;
            
            ToolStripMenuItem create = new ToolStripMenuItem("New Scene");
            create.BackColor = ColorScheme.MenuItemColor; 
            create.ForeColor= Color.White;

            ToolStripMenuItem save = new ToolStripMenuItem("Save Scene");
            save.BackColor = ColorScheme.MenuItemColor;
            save.ForeColor = Color.White;

            file.DropDownItems.Add(open);
            file.DropDownItems.Add(create);
            file.DropDownItems.Add(save);
            Items.Add(file);


            ToolStripMenuItem preferences = new ToolStripMenuItem("Preferences");
            preferences.BackColor = ColorScheme.MenuItemColor;
            Items.Add(preferences);

            // add cursorIcon 
            Bitmap cursorBitmap = new Bitmap("Images/cursoricon.png");
            CursorToggle = new ToolStripButton(cursorBitmap);
            CursorToggle.CheckOnClick = true;
            CursorToggle.Click += CheckedNewButton;
            
            Items.Add(CursorToggle);

            // add brush 
            Bitmap brushBitmap = new Bitmap("Images/brushicon.png");
            BrushToggle = new ToolStripButton(brushBitmap);
            BrushToggle.CheckOnClick = true;
            BrushToggle.Click += CheckedNewButton;
            Items.Add(BrushToggle);

            // add eraser 
            Bitmap eraserBitmap = new Bitmap("Images/erasericon.png");
            EraserToggle = new ToolStripButton(eraserBitmap);
            EraserToggle.CheckOnClick = true;
            EraserToggle.Click += CheckedNewButton;
            
            Items.Add(EraserToggle);

            // add snapping 
            Bitmap snappingBitmap = new Bitmap("Images/snapping1icon.png");
            SnappingToggle = new ToolStripButton(snappingBitmap);
            SnappingToggle.CheckOnClick = true;
            SnappingToggle.CheckStateChanged += (sender, args) =>
            {
                if (!SnappingToggle.Checked)
                {
                    SnappingToggle.Image = new Bitmap("Images/snapping1icon.png");
                }
                else
                {
                    SnappingToggle.Image = new Bitmap("Images/snapping2icon.png");
                }

                SnappingAmmount.Enabled = SnappingToggle.Checked;
            };

            Items.Add(SnappingToggle);

            // adding snapping toolbox
            SnappingAmmount = new ToolStripTextBox("snapping ammount");
            SnappingAmmount.Text = "128";
            SnappingAmmount.Size = new Size(64, SnappingAmmount.Size.Height);
            SnappingAmmount.Font = new Font("Calibri", 12, FontStyle.Regular);
            SnappingAmmount.BackColor = ColorScheme.MenuItemColor;
            SnappingAmmount.ForeColor = Color.White;
            SnappingAmmount.Enabled = SnappingToggle.Checked;
            SnappingAmmount.KeyPress += (sender, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            };
            
            Items.Add(SnappingAmmount);

        }

        private void CheckedNewButton(object sender, EventArgs e)
        {
            if(sender != EraserToggle && EraserToggle.Checked)
                EraserToggle.Checked = !((ToolStripButton) sender).Checked;
            if(sender != BrushToggle && BrushToggle.Checked)
                BrushToggle.Checked = !((ToolStripButton) sender).Checked;
            if(sender != CursorToggle && CursorToggle.Checked)
                CursorToggle.Checked = !((ToolStripButton) sender).Checked;

        }

        private void OpenScene(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\danke\\source\\repos\\SpaceMarauders\\SpaceMarauders\\bin\\Windows\\x86\\Debug\\Saves";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}

                    Game1.Instance.LoadProject(filePath);
                    SceneTree.Instance.LoadScene(filePath);
                }
            };
        }
    }

    class MenuRenderer : ToolStripProfessionalRenderer
    {
        public MenuRenderer() : base(new MyColors()) { }
    }

    class MyColors : ProfessionalColorTable
    {
        public override Color MenuItemBorder
        {
            get { return Color.Transparent; }
        }

        public override Color MenuBorder
        {
            get { return Color.Transparent; }
        }
        
        public override Color MenuItemSelected
        {
            get { return ColorScheme.FormColor; }
        }
    }
}
