using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components;

namespace MarauderEngine.Systems
{
    /// <summary>
    /// Where all the components of Entities with a tag component are held.
    /// Can be used to find a single or many entities with a given tag. 
    /// </summary>
    public class EntityTagSystem : SystemManager<EntityTagSystem>
    {

        private List<TagComponent> _tagComponents = new List<TagComponent>();

        /// <summary>
        /// 
        /// </summary>
        public EntityTagSystem()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            
        }

        /// <summary>
        /// Registers tag component to active tags
        /// </summary>
        /// <param name="tagComponent"></param>
        public void RegisterTagComponent(TagComponent tagComponent)
        {
            _tagComponents.Add(tagComponent);
        }
        
        /// <summary>
        /// Removes tag component from active tags
        /// </summary>
        /// <param name="tagComponent"></param>
        public void RemoveTag(TagComponent tagComponent)
        {
            _tagComponents.Remove(tagComponent);
;        }


        /// <summary>
        /// Returns the first entity found with tag name
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public Entity.Entity FindEntityWithTag(string tagName)
        {
            return _tagComponents.First(t => t.TagName == tagName).Owner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public List<Entity.Entity> FindEntitiesWithTag(string tagName)
        {
            return _tagComponents.Where(t => t.TagName == tagName).Select(t => t.Owner).ToList();
        }
    }
}
