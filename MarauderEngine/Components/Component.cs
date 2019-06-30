using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components.Data;

namespace MarauderEngine.Components
{
    public abstract class Component<T> :IComponent where T: ComponentData, new()
    {

        internal T _data;

        public Type type => GetType();

        // Implement if abstract GetType does not return child type.
        //public abstract Type type { get; }

        public Entity.Entity Owner
        {
            get => _data.Owner;
            set => _data.Owner = value;
        }

        public string Name
        {
            get => _data.Name;
            set => _data.Name = value;
        }

        public bool Active
        {
            get => _data.Active;
            set => _data.Active = value;
        }

        public virtual void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            _data = new T {Owner = entity, Name = componentName, Active = true};
        }

        public abstract bool FireEvent(Event eEvent);
        public abstract void UpdateComponent();
        public abstract void Destroy();
    }
}
