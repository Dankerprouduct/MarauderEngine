using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.GUI
{
    public class UIWindow : IUIElement
    {

        public  List<IUIElement> Elements = new List<IUIElement>();

        public Point Origin { get; set; }
        public Point Size { get; set; }
        public bool Show { get; set; }
        public bool Enabled { get; set; }
        public bool Hover { get; set; }
        public Color Colour { get; set; } 

        public Rectangle rectangle { get; set; }

        public IUIElement Parent { get; set; }
        
        public UIWindow(Point origin, Point size, bool enabled)
        {
            SetProperties(origin, size, enabled);
        }

        public UIWindow(IUIElement parent, Point origin, Point size, bool enabled) :
            this(origin, size, enabled)
        {
            Parent = parent;
        }

        public UIWindow(IUIElement parent, Point origin, Point size, Color color, bool enabled) :
            this(origin, size, enabled)
        {
            Parent = parent;
            Colour = color;
        }

        public List<IUIElement> GetElements()
        {
                return Elements;
        }

        /// <summary>
        /// Adds position
        /// ex (0,1) + (2, 2) = (2, 3)
        /// </summary>
        /// <param name="origin"></param>
        public void AddPosition(Point origin)
        {
            Origin += origin;
        }

        public void AddElement(IUIElement element)
        {
            Elements.Add(element);
        }

        public void RemoveElement(IUIElement element)
        {
            Elements.Remove(element); 
        }
        
        public virtual void Update()
        {
            
            if (Parent != null)
            {
                rectangle = new Rectangle(Origin + Parent.Origin, Size);
            }
            else
            {
                rectangle = new Rectangle(Origin, Size);
            }

            foreach (var uiElement in Elements)
            {
                uiElement.Update();
            }

            foreach (var element in GetElements())
            {
                element.Show = Show;
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                if (Show)
                {
                    GUI.DrawBox(rectangle, Colour);
                }

                foreach (var uiElement in Elements)
                {
                    uiElement.Draw(spriteBatch);
                }
            }
        }

        public void SetProperties(Point origin, Point size, bool enabled)
        {
            Origin = origin;
            Size = size;
            Enabled = enabled;
            Show = true;
            rectangle = new Rectangle(origin.X, origin.Y, size.X, size.Y);
            Colour = Color.Gray; 

        }

        public void Toggle()
        {
            Enabled = !Enabled; 
        }

        public bool IsEnabled()
        {
            return Enabled;
        }

        public virtual void Close()
        {
            foreach (var element in Elements)
            {
                element.Enabled = false;
                element.Show = false; 
            }
        }
    }
}
