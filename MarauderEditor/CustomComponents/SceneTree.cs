using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MarauderEditor.CustomComponents
{
    public class SceneTree: TreeView
    {
        public static SceneTree Instance;
        public SceneTree(Control parent)
        {
            Instance = this; 
            Parent = parent;
            BackColor = ColorScheme.ComponentColor;

            //Left = parent.Left;
            Width = 250;
            Height = parent.Height;

            Scrollable = true;
            Font = new Font("Calibri", 12, FontStyle.Regular);
            ForeColor = Color.White;
            BorderStyle = BorderStyle.None;

        }

        public void LoadScene(string path)
        {
            StreamReader sR = new StreamReader(path);
            string scene = sR.ReadToEnd();
            this.LoadJsonToTreeView(scene);
        }
    }
}
