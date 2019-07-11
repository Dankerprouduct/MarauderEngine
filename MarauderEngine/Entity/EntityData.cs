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

        private List<EntityData> _children = new List<EntityData>();

        public List<EntityData> Children
        {
            get => _children;
            set => _children = value;
        }

        private List<ComponentData> _components = new List<ComponentData>();

        public List<ComponentData> Components
        {
            get => _components;
            set => _components = value;
        }

    }
}
