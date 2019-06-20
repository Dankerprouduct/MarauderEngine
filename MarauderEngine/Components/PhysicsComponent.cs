using System;
using MarauderEngine.Core;
using MarauderEngine.Physics.Core;
using Microsoft.Xna.Framework;
using Circle = MarauderEngine.Physics.Core.Shapes.Circle;

namespace MarauderEngine.Components
{
    public class PhysicsComponent : IComponent
    {
        public MarauderEngine.Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        private Vector2 _position;
        
        public Circle CollisionCircle;

        public event EventHandler<CollisionEvent> CollidedWithEntity;  

        public PhysicsComponent(MarauderEngine.Entity.Entity owner)
        {
            RegisterComponent(owner, "PhysicsComponent");

            RegisterPhysicsBody();
            CollisionCircle.CollidedWithEntity += OnCollision;
        }

        public PhysicsComponent(MarauderEngine.Entity.Entity owner, float colliderRadius)
        {
            RegisterComponent(owner, "PhysicsComponent");
            Active = true; 
            RegisterPhysicsBody(colliderRadius);
            CollisionCircle.CollidedWithEntity += OnCollision;
        }

        private void OnCollision(object sender, CollisionEvent e)
        {
            CollisionEvent args = e;
            CollidedWithEntity?.Invoke(this, e);
        }

        public void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            Owner = entity;
            Name = componentName;
            Active = true; 
        }

        /// <summary>
        /// Adds physics body to PhysicsSystem
        /// </summary>
        public void RegisterPhysicsBody(float radius = 30)
        {
            CollisionCircle = new Circle(new Particle(Owner.GetComponent<TransformComponent>().Position, Vector2.Zero, 1),this, radius);
            CollisionCircle.Particle.ActiveParticle = true; 
            //Physics.PhysicsSystem.Instance.AddBody(body);
            PhysicsWorld.Instance.Add(CollisionCircle);
        }

        /// <summary>
        /// Fires event to component
        /// </summary>
        /// <param name="eEvent"></param>a
        /// <returns></returns>
        public bool FireEvent(Event eEvent)
        {
            if (eEvent.id == "AddVelocity")
            {
                
                //body.AddForce((float)eEvent.parameters["direction"], (float)eEvent.parameters["force"]);
                CollisionCircle.Particle.AddForce((Vector2)eEvent.parameters["Velocity"]);
            }

            return false;
        }

        public void UpdateComponent()
        {
            //_position = body.Position;
            Owner.GetComponent<TransformComponent>().Position = CollisionCircle.Particle.Position; 
            

            
        }

        public void SetPosition(Vector2 position)
        {
            CollisionCircle.Particle.Position = position;
        }

        /// <summary>
        /// On a ZoomScale from 0 - 1
        /// </summary>
        /// <param name="dampening"></param>
        public void SetDampening(float dampening)
        {
            CollisionCircle.Particle.Dampening = dampening;
        }

        /// <summary>
        /// Set to 0 if infinite max speed
        /// </summary>
        /// <param name="maxSpeed"></param>
        public void SetMaxSpeed(float maxSpeed)
        {
            CollisionCircle.Particle.MaxSpeed = maxSpeed; 
        }


        public void Destroy()
        {
            //Debug.Log("Destroying collider", Debug.LogType.Error, 5);
            //CollisionCircle.DestroyCollider();
            //CollisionCircle.Active = false; 
            //PhysicsWorld.Instance.DestroyCollider(CollisionCircle);
            PhysicsWorld.Instance.DestroyCollider(CollisionCircle);
            //CollisionCircle = null;
            this.Active = false; 
        }

        public Vector2 GetPosition()
        {
            return CollisionCircle.Particle.Position;
        }
    }

    public class CollisionEvent: EventArgs
    {
        public bool Colliding;
        public Entity.Entity Entity1;
        public Entity.Entity Entity2; 
    }
}
