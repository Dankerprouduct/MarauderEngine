using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Entity;

namespace MarauderEngine.Core.DataStructures
{
    public class SceneData
    {

        public string SceneName { get; set; }
        public bool ManuallyUpdateScene { get; set; }
        
        /// <summary>
        /// all of the dynamic entities in the scene
        /// </summary>
        public EntityData[] DynamicEntityData { get; set; }

        /// <summary>
        /// all of the static entities in the scene
        /// </summary>
        public EntityData[] StaticEntityData { get; set; }

    }
}
