using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.GUI
{
    public interface IUIElement
    {

        /// <summary>
        /// The position of this element relative to its parent
        /// </summary>
        Point Origin { get; set; }
        /// <summary>
        /// The size of the element
        /// </summary>
        Point Size { get; set; }
        /// <summary>
        /// Whether or not this element is drawn
        /// </summary>
        bool Show { get; set; }
        /// <summary>
        /// Whether or not this element is updated
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// returns true if the mouse if hovering above the element
        /// </summary>
        bool Hover { get; set; }
        /// <summary>
        /// The color of this element
        /// </summary>
        Color Colour { get; set; }
        /// <summary>
        /// The origin and size of the element
        /// </summary>
        Rectangle rectangle { get; set; }
        /// <summary>
        /// What IUIElement the element belongs to
        /// </summary>
        IUIElement Parent { get; set; }

        void Update();

        void Draw(SpriteBatch spriteBatch);

        void SetProperties(Point origin, Point size, bool enabled);

        void AddPosition(Point origin);

        void Toggle();

        bool IsEnabled(); 
    }
}
