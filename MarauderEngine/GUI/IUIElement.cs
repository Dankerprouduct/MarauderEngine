using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.GUI
{
    public interface IUIElement
    {

        Point Origin { get; set; }
        Point Size { get; set; }
        bool Show { get; set; }
        bool Enabled { get; set; }
        bool Hover { get; set; }
        Color Colour { get; set; }
        Rectangle rectangle { get; set; }

        IUIElement Parent { get; set; }

        void Update();

        void Draw(SpriteBatch spriteBatch);

        void SetProperties(Point origin, Point size, bool enabled);

        void AddPosition(Point origin);

        void Toggle();

        bool IsEnabled(); 
    }
}
