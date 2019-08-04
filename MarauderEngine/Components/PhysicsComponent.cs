using System;
using MarauderEngine.Components.Data;
using MarauderEngine.Core;
using MarauderEngine.Physics.Core;
using MarauderEngine.Physics.Core.Shapes;
using Microsoft.Xna.Framework;
using SharpMath2;
using Circle = MarauderEngine.Physics.Core.Shapes.Circle;

namespace MarauderEngine.Components
{
    public class PhysicsComponent : Component<PhysicsCData>
    {
        private Vector2 _position;
        
        public Collider Collider;

        public event EventHandler<CollisionEvent> CollidedWithEntity;

        public override Type type => GetType();

        public PhysicsComponent()
        {

        }
        public PhysicsComponent(MarauderEngine.Entity.Entity owner)
        {
            RegisterComponent(owner, "PhysicsComponent");

            //RegisterPhysicsBody();
            //Collider.CollidedWithEntity += OnCollision;
        }

        public PhysicsComponent(MarauderEngine.Entity.Entity owner, float colliderRadius)
        {
            RegisterComponent(owner, "PhysicsComponent");
            _data.Radius = colliderRadius;
            Active = true; 
            
        }

        private void OnCollision(object sender, CollisionEvent e)
        {
            CollisionEvent args = e;
            CollidedWithEntity?.Invoke(this, e);
        }

        /// <summary>
        /// Adds physics body to PhysicsSystem
        /// </summary>
        public void RegisterPhysicsBody(float radius = 30)
        {

            //Collider = new Circle(new Particle(Owner.GetComponent<TransformComponent>().Position, Vector2.Zero, 1),this, radius);

            Collider = new Collider(new Particle(Owner.GetComponent<TransformComponent>().Position, Vector2.Zero, 1),
                ShapeUtils.CreateRectangle(32, 32), this);
            Collider.Particle.ActiveParticle = true; 
            //Physics.PhysicsSystem.Instance.AddBody(body);
            PhysicsWorld.Instance.Add(Collider);
        }

        public override void RegisterComponent(Entity.Entity entity, string componentName)
        {
            base.RegisterComponent(entity, componentName);
            RegisterPhysicsBody(_data.Radius);

            Collider.Particle.MaxSpeed = _data.MaxSpeed;
            Collider.Particle.Dampening = _data.Dampening;
            //Collider.SetRadius(_data.Radius);
            Collider.CollidedWithEntity += OnCollision;
        }

        /// <summary>
        /// Fires event to component
        /// </summary>
        /// <param name="eEvent"></param>a
        /// <returns></returns>
        public override bool FireEvent(Event eEvent)
        {
            if (eEvent.id == "AddVelocity")
            {
                
                //body.AddForce((float)eEvent.parameters["direction"], (float)eEvent.parameters["force"]);
                Collider.Particle.AddForce((Vector2)eEvent.parameters["Velocity"]);
            }

            return false;
        }

        public override void UpdateComponent(GameTime gameTime)
        {
            //_position = body.Position;
            Owner.GetComponent<TransformComponent>().Position = Collider.Particle.Position; 
            
            
        }
        
        public void SetPosition(Vector2 position)
        {
            Collider.Particle.Position = position;
        }

        /// <summary>
        /// On a ZoomScale from 0 - 1
        /// </summary>
        /// <param name="dampening"></param>
        public void SetDampening(float dampening)
        {
            Collider.Particle.Dampening = _data.Dampening;
        }

        /// <summary>
        /// Set to 0 if infinite max speed
        /// </summary>
        /// <param name="maxSpeed"></param>
        public void SetMaxSpeed(float maxSpeed)
        {
            Collider.Particle.MaxSpeed = _data.MaxSpeed; 
        }

        public void SetRadius(float radius)
        {
            //Collider.SetRadius(radius);
            _data.Radius = radius;
        }


        public override void Destroy()
        {
            //Debug.Log("Destroying collider", Debug.LogType.Error, 5);
            //Collider.DestroyCollider();
            //Collider.Active = false; 
            //PhysicsWorld.Instance.DestroyCollider(Collider);
            PhysicsWorld.Instance.DestroyCollider(Collider);
            //Collider = null;
            this.Active = false; 
        }

        public Vector2 GetPosition()
        {
            return Collider.Particle.Position;
        }
    }

    public class CollisionEvent: EventArgs
    {
        public bool Colliding;
        public Entity.Entity Entity1;
        public Entity.Entity Entity2; 
    }
}
