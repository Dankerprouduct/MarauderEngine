using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components.Data
{
    public class SpriteCData: ComponentData
    {
        public string TextureName { get; set; }

        public Vector2 TextureCenter { get; set; }
        public float SpriteScale { get; set; }

        public float Layer = 0;

        public float Rotation { get; set; }

        public bool StupidDrawing = false;

        /// <summary>
        /// the tint of the sprite. Defaults to white
        /// </summary>
        public Color Color;

        /// <summary>
        /// a value between 0 and 1 that fades a color
        /// </summary>
        public float ColorMod;
    }
}
