using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEngine.Entity;

namespace MarauderEditor.CustomComponents
{
    public class EntityProperties : PropertyGrid
    {
        public static EntityProperties Instance;
        public EntityProperties(Control parent)
        {
            Instance = this; 
            Parent = parent;
            Font = new Font("Calibri", 12, FontStyle.Regular);
            BackColor = ColorScheme.MenuItemColor;
            CommandsBorderColor = ColorScheme.MenuItemColor;
            ForeColor = Color.White;
            AllowDrop = true;
            
            ViewForeColor = Color.White;
            ViewBackColor = ColorScheme.ComponentColor;
            ViewBorderColor = ColorScheme.ComponentColor;
            CategorySplitterColor = ColorScheme.MenuItemColor;
            LineColor = ColorScheme.MenuItemColor;
            CommandsForeColor = Color.White;
            CategoryForeColor = Color.White;

            CommandsBorderColor = ColorScheme.ComponentColor;
            HelpBorderColor = ColorScheme.ComponentColor;
            HelpBackColor = ColorScheme.MenuItemColor;
            HelpForeColor = Color.White;
        }

        public void LoadEntity(EntityData item)
        {
            SelectedObject = item;
            Console.WriteLine(item.Components.Count);
            Refresh();
        }
    }
}
