using MarauderEngine.Graphics;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MarauderEngine.Entity
{
    public class Decoration
    {

        public int DecorationID { get; set; }
        public Vector2 Position { get; set; }


        [JsonIgnore]
        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, TextureManager.Tiles[DecorationID].Width, TextureManager.Decorations[DecorationID].Height); 
            }
        }
    }
}
