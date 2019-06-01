using System;
using MarauderEngine.Components;
using MarauderEngine.Core;
using MarauderEngine.Physics.Core.SpatialPartition;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Physics.Core.Shapes
{
    public class Circle : ICollider
    {
        // public 
        public Particle Particle { get; set; }
        public int PartitionIndex { get; set; }
        public int OldPartitionIndex { get; set; }
        public Vector2 Center { get; set; }
        public bool Colliding { get; set; }
        public int Layer { get; set; }
        public IComponent Owner { get; set; }


        public event EventHandler<CollisionEvent> CollidedWithEntity;


        /// <summary>
        /// determines whether or not the object collides
        /// </summary>
        public bool Active { get; set; }

        // private 
        private readonly float _radius; 

        
        [System.Obsolete("please provide a owner", true)]
        public Circle(Particle particle, float radius)
        {
            PartitionIndex = CellSpacePartition<ICollider>.Instance.PositionToIndex(particle.Position);
            Particle = particle;
            _radius = radius;

            Active = true; 
            this.Particle.Collider = this;
        }

        public Circle(Particle particle,IComponent owner, float radius)
        {
            Owner = owner; 
            PartitionIndex = CellSpacePartition<ICollider>.Instance.PositionToIndex(particle.Position);
            Particle = particle;
            _radius = radius;

            Active = true;
            this.Particle.Collider = this;
        }

        public float GetRadius()
        {
            return _radius; 
        }

        public bool Intersects(ICollider collider)
        {
            Circle a = this;
            Circle b = ((Circle)collider);
            float r = b._radius + a._radius;
            r *= r;
            float valueToCheck = ((b.Particle.Position.X - a.Particle.Position.X) *
                                  (b.Particle.Position.X - a.Particle.Position.X)) +
                                 ((b.Particle.Position.Y - a.Particle.Position.Y)) *
                                 ((b.Particle.Position.Y - a.Particle.Position.Y)); 

            bool result = r >= valueToCheck;

            Colliding = result;
            if (Colliding)
            {
                // event 1
                CollisionEvent args = new CollisionEvent();
                args.Colliding = true;
                args.Entity1 = a.Owner.Owner;
                args.Entity2 = b.Owner.Owner;

                a.OnCollision(this, args);
                b.OnCollision(this, args);

            }
            return result;

        }

        public Vector2 GetCenter()
        {
            return new Vector2(Particle.Position.X - _radius,
                Particle.Position.Y - _radius);
        }

        [System.Obsolete("use the other one", true)]
        public bool Intersects(Point other)
        {
            if (Active)
            {
                Circle a = this;
                Vector2 b = other.ToVector2();
                float r = 1 + a._radius;
                r *= r;
                float valueToCheck = ((other.X - a.Particle.Position.X) *
                                      (other.X - a.Particle.Position.X)) +
                                     ((other.Y - a.Particle.Position.Y)) *
                                     ((other.Y - a.Particle.Position.Y));

                bool result = r >= valueToCheck;

                Colliding = result;
                

                return result;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            Particle.Update(gameTime);
            PartitionIndex = CellSpacePartition<ICollider>.Instance.PositionToIndex(Particle.Position);

            if (PartitionIndex != OldPartitionIndex)
            {
                PhysicsWorld.Instance.ChangeColliderPartition(this);

                OldPartitionIndex = PartitionIndex;
                //CellSpacePartition<ICollider>.Instance.ChangeCell(this);
            }

            if (Owner == null)
            {
                PhysicsWorld.Instance.DestroyCollider(this);
            }

            return;
        }

        public void OnCollision(object sender, CollisionEvent args)
        {
            CollidedWithEntity?.Invoke(this, args);
        }

        public void DestroyCollider()
        {
            Particle = null;
            
        }
    }
}
