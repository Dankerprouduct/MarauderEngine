using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components.Data
{
    public class SolidCData: ComponentData
    {
        public float Radius = 64;

        public Vector2 Center { get; set; }
    }
}
