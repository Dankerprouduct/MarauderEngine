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

        public ComponentData Data { get; set; }
        internal T _data
        {
            get => Data as T;
            set => Data = value;
        }

        // public Type type => GetType();

        // Implement if abstract GetType does not return child type.
        public abstract Type type { get; }

        public Entity.Entity Owner { get; set; }

        public string Name
        {
            get => _data.Name;
            set => _data.Name = value;
        }


        public bool Active
        {
            get => _data.Active;
            set
            {
                if (_data == null)
                {
                    _data = new T();
                    _data.Active = value;
                }
                else
                {
                    _data.Active = value; 
                }
            }
        }

        public virtual void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            _data = new T {Name = componentName, ComponentType = type ,Active = true};
            Owner = entity; 
            entity.EntityData.Components.Add(_data);
        }

        public abstract bool FireEvent(Event eEvent);
        public abstract void UpdateComponent();
        public abstract void Destroy();
    }
}
