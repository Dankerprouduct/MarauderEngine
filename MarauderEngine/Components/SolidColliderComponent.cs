using System;
using System.Collections.Generic;
using MarauderEngine.Entity;
using MarauderEngine.Physics.Core;
using MarauderEngine.Physics.Core.Shapes;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class SolidColliderComponent: IComponent
    {
        public MarauderEngine.Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public Rectangle Rectangle;
        public Circle CollisionCircle; 
        public SolidColliderComponent(MarauderEngine.Entity.Entity entity)
        {
            RegisterComponent(entity, "SolidColliderComponent");
            Rectangle = entity.collisionRectanlge;

            try
            {
                CollisionCircle = new Circle(new Particle((entity).GetComponent<TransformComponent>().Position, 0),this, 64)
                {
                    Particle = {Restitution = 1}
                };
            }
            catch (Exception ex)
            {
                throw  new Exception("Entity does not have a Transform Component");
            }

            PhysicsWorld.Instance.Add(CollisionCircle);

        }

        public SolidColliderComponent(MarauderEngine.Entity.Entity entity, Vector2 center)
        {
            RegisterComponent(entity, "SolidColliderComponent");
            Rectangle = entity.collisionRectanlge;
            CollisionCircle = new Circle(new Particle(Owner.GetComponent<TransformComponent>().Position + center, 0),this, 64)
            {
                Particle = { Restitution = 1 }
            };
            
            PhysicsWorld.Instance.Add(CollisionCircle);

        }
        public SolidColliderComponent(MarauderEngine.Entity.Entity entity, Vector2 center, int radius)
        {
            RegisterComponent(entity, "SolidColliderComponent");
            Rectangle = entity.collisionRectanlge;
            CollisionCircle = new Circle(new Particle(Owner.GetComponent<TransformComponent>().Position + center, 0),this, radius)
            {
                Particle = { Restitution = 1 }
            };

            PhysicsWorld.Instance.Add(CollisionCircle);

        }

        public void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            Owner = entity; 
            Name = componentName;
            Active = true; 
        }

        public bool FireEvent(Event eEvent)
        {
            if (eEvent.id == "Collider")
            {

                foreach (KeyValuePair<string, object> parameter in eEvent.parameters)
                {

                    if (parameter.Key == "rectangle")
                    {
                        return ((Rectangle)parameter.Value).Intersects(Rectangle);


                    }

                }
            }
            return false;
        }

        public void UpdateComponent()
        {

        }

        public void Destroy()
        {
            
        }
    }
}
