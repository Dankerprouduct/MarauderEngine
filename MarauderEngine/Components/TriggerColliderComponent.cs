using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class TriggerColliderComponent: IComponent
    {
        public MarauderEngine.Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        Rectangle _rectangle;
        bool _destroy = false;

        public TriggerColliderComponent(MarauderEngine.Entity.Entity entity)
        {
            RegisterComponent(entity, "TriggerColliderComponent");
        }

        public void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            Name = componentName;
            Owner = entity;
            Active = true;
            this._rectangle = Owner.collisionRectanlge;
        }

        public bool FireEvent(Event eEvent)
        {
            if (eEvent.id == "RayHit")
            {
                //Console.WriteLine("Polygon " + rectangle + " " + (Point)_event.parameters["Ray"]);
                //Console.WriteLine("ray " + rectangle.Contains((Point)_event.parameters["Ray"]));
                bool hit = _rectangle.Contains((Point)eEvent.parameters["Ray"]);

                if (hit)
                {
                    foreach (KeyValuePair<string, object> parameters in eEvent.parameters)
                    {

                        if (parameters.Key == "DestroyEntity")
                        {
                            //Console.WriteLine("fsa");
                            _destroy = true;
                        }
                    }
                }

                if (hit)
                {
                    //destroy = true;
                }

                return hit;
            }


            return false; 
        }

        public void UpdateComponent()
        {
            _rectangle = Owner.collisionRectanlge;
            // = entity; 

            if (_destroy)
            {
                Owner.Destroy();
            }
        }

        public void Destroy()
        {
            
        }
    }
}
