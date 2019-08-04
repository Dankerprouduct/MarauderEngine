using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Graphics
{
    public class DrawingManager
    {
        public static DrawingManager Instance;

        /// <summary>
        /// List of all the Components being drawn
        /// </summary>
        public List<IDrawable> Drawables;


        public DrawingManager()
        {
            Drawables = new List<IDrawable>();
            Instance = this; 
        }

        /// <summary>
        /// Adds an IDrawable to the list of Drawables
        /// </summary>
        /// <param name="drawable"></param>
        public void Register(IDrawable drawable)
        {
            Drawables.Add(drawable);

            // sorts by layer
            Drawables = Drawables.OrderBy(o => o.Layer).ToList();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var drawable in Drawables)
            {
                //spriteBatch.Draw(TextureManager.TextureLists[drawable.TextureListName][drawable.TextureId],
                //    drawable.TransformComponent.Position, Color.White);
            }
        }
    }
}
