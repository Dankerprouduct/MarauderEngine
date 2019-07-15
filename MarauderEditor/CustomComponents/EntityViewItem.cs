using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEngine.Components;
using MarauderEngine.Components.Data;
using MarauderEngine.Core;
using MarauderEngine.Entity;
using IComponent = MarauderEngine.Components.IComponent;

namespace MarauderEditor.CustomComponents
{
    public class EntityViewItem :ListViewItem
    {
        public Type EntityType;
        public string EntityName;
        public Entity Entity;
        public List<IComponent> Components = new List<IComponent>();
        public EntityData EntityData;
        public EntityViewItem(Type entity)
        {
            EntityType = entity;
            EntityName = entity.Name;
            Entity = Activator.CreateInstance(entity) as Entity;
            Entity.EntityData.EntityName = EntityName;

            ToolTipText = EntityName;
            Text = EntityName;

            Console.WriteLine(EntityName + " ===================");
            foreach (var f in entity.GetFields().Where(f => f.IsPublic))
            {
                var component = Activator.CreateInstance(f.FieldType) as IComponent;

                if (component != null)
                {
                    //Components.Add(component);
                    Entity.ForceAddComponent(component);
                    component.RegisterComponent(Entity, f.Name);
                    component.Owner = Entity;
                    Console.WriteLine($"Name:{f.Name}");
                }


            }

            Console.WriteLine(Components.Count);
        }
    }
}
