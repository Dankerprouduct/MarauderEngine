using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components.Data;

namespace MarauderEngine.Entity
{
    public class EntityData
    {
        public string EntityName { get; set; }
        public Type EntityType { get; set; }
        public int EntityID { get; set; }
        public List<EntityData> Children = new List<EntityData>();
        public List<ComponentData> Components = new List<ComponentData>();

    }
}
