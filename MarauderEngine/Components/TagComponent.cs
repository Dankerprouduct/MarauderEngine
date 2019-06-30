using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components.Data;
using MarauderEngine.Systems;

namespace MarauderEngine.Components
{
    public class TagComponent: Component<TagCData>
    {

        /// <summary>
        /// The tag associated with the Entity
        /// </summary>
        public string TagName
        {
            get => _data.TagName;
            set => _data.TagName = value;
        }

        public TagComponent(Entity.Entity entity, string tagName)
        {
            TagName = tagName; 
            RegisterComponent(entity, "TagComponent");

            EntityTagSystem.Instance.RegisterTagComponent(this);
        }

        public override bool FireEvent(Event eEvent)
        {

            return false; 
        }

        public override void UpdateComponent()
        {

        }

        public override void Destroy()
        {
            EntityTagSystem.Instance.RemoveTag(this);
        }
    }
}
