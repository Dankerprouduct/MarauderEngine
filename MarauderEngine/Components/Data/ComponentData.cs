using System;

namespace MarauderEngine.Components.Data
{
    public abstract class ComponentData
    {

        public string Name { get; set; }

        public Type ComponentType { get; set; }

        public bool Active { get; set; }

    }
}
