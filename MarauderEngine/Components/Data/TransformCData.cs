using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components.Data
{
    public class TransformCData : ComponentData
    {
        public Vector2 Position { get; set; }

        public float _rotation { get; set; } = 0;

    }
}
