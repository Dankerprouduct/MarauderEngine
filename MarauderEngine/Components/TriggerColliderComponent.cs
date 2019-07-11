using System;
using System.Collections.Generic;
using MarauderEngine.Components.Data;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class TriggerColliderComponent: Component<TriggerColliderCData>
    {
        Rectangle _rectangle;
        bool _destroy = false;

        public override Type type => GetType();
        public TriggerColliderComponent(MarauderEngine.Entity.Entity entity)
        {
            RegisterComponent(entity, "TriggerColliderComponent");
        }

        public override void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            Name = componentName;
            Owner = entity;
            Active = true;
            this._rectangle = Owner.collisionRectanlge;
        }

        public override bool FireEvent(Event eEvent)
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

        public override void UpdateComponent(GameTime gameTime)
        {
            _rectangle = Owner.collisionRectanlge;
            // = entity; 

            if (_destroy)
            {
                Owner.Destroy();
            }
        }

        public override void Destroy()
        {
            
        }
    }
}
