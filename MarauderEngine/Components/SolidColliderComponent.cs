using System;
using System.Collections.Generic;
using MarauderEngine.Components.Data;
using MarauderEngine.Entity;
using MarauderEngine.Physics.Core;
using MarauderEngine.Physics.Core.Shapes;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class SolidColliderComponent: Component<SolidCData>
    {

        public Rectangle Rectangle;
        public Circle CollisionCircle;

        public override Type type => GetType();
        public SolidColliderComponent() { }
        public SolidColliderComponent(MarauderEngine.Entity.Entity entity)
        {
            _data = new SolidCData();
            Rectangle = entity.collisionRectanlge;
            RegisterComponent(entity, "SolidColliderComponent");
            

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
            _data = new SolidCData();
            _data.Center = center;
            Rectangle = entity.collisionRectanlge;
            RegisterComponent(entity, "SolidColliderComponent");
            
            

            //CollisionCircle = new Circle(new Particle(Owner.GetComponent<TransformComponent>().Position + center, 0),this, 64)
            //{
            //    Particle = { Restitution = 1 }
            //};

            //PhysicsWorld.Instance.Add(CollisionCircle);

        }
        public SolidColliderComponent(MarauderEngine.Entity.Entity entity, Vector2 center, int radius)
        {
            _data = new SolidCData();
            _data.Center = center;
            _data.Radius = radius;
            RegisterComponent(entity, "SolidColliderComponent");

            

        }


        public void RegisterPhysicsBody() 
        {
            Rectangle = Owner.collisionRectanlge;
            CollisionCircle = new Circle(new Particle(Owner.GetComponent<TransformComponent>().Position + _data.Center, 0), this, _data.Radius)
            {
                Particle = { Restitution = 1 }
            };

            PhysicsWorld.Instance.Add(CollisionCircle);
        }

        public override void RegisterComponent(Entity.Entity entity, string componentName)
        {
            base.RegisterComponent(entity, componentName);

            RegisterPhysicsBody();
        }

        public override bool FireEvent(Event eEvent)
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

        public override void UpdateComponent(GameTime gameTime)
        {

        }

        public override void Destroy()
        {
            
        }
    }
}
