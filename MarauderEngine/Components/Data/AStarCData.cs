using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarauderEngine.Components.Data
{
    public class AStarCData :ComponentData
    {
        /// <summary>
        /// the distance at which the entity will stop pathfinding and move straight towards the target
        /// </summary>
        public float IgnoreDistance = 128;
    }
}
