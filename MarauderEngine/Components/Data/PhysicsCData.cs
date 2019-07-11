using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components.Data
{
    public class PhysicsCData : ComponentData
    {
        public Vector2 Position { get; set;}

        public float MaxSpeed = 0;

        public float Dampening = .95f;

        public float Radius = 30;
    }
}
