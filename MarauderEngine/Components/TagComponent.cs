using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Systems;

namespace MarauderEngine.Components
{
    public class TagComponent: IComponent
    {
        public Entity.Entity Owner { get; set; }

        /// <summary>
        /// The name of the component
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag associated with the Entity
        /// </summary>
        public string TagName { get; private set; }


        public bool Active { get; set; }

        public TagComponent(Entity.Entity entity, string tagName)
        {
            TagName = tagName; 
            RegisterComponent(entity, "TagComponent");
        }

        public void RegisterComponent(Entity.Entity entity, string componentName)
        {
            Owner = entity;
            Name = componentName; 
            EntityTagSystem.Instance.RegisterTagComponent(this);

        }

        public bool FireEvent(Event eEvent)
        {

            return false; 
        }

        public void UpdateComponent()
        {

        }

        public void Destroy()
        {
            EntityTagSystem.Instance.RemoveTag(this);
        }
    }
}
