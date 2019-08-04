using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components;
using MarauderEngine.Physics.Core.SpatialPartition;
using Microsoft.Xna.Framework;
using SharpMath2;

namespace MarauderEngine.Physics.Core.Shapes
{
    public class Collider: ICollider
    {
        public Particle Particle { get; set; }
        public Vector2 Center { get; set; }
        public int PartitionIndex { get; set; }
        public int OldPartitionIndex { get; set; }
        public bool Colliding { get; set; }
        public bool Active { get; set; }
        public int Layer { get; set; }
        public Polygon2 PhysicsCollider { get; set; }
        public Rotation2 Rotation { get; set; }
        public IComponent Owner { get; set; }
        public event EventHandler<CollisionEvent> CollidedWithEntity;

        public Collider(Particle particle, Polygon2 polygon, IComponent owner)
        {
            Owner = owner;
            PartitionIndex = CellSpacePartition<ICollider>.Instance.PositionToIndex(particle.Position);
            Particle = particle;
            PhysicsCollider = polygon;

            Active = true;
            Particle.Collider = this;
        }

        public bool Intersects(ICollider other)
        {

            return Polygon2.Intersects(
                PhysicsCollider, other.PhysicsCollider,
                Particle.Position, other.Particle.Position, 
                false);
            
        }

        public float GetRadius()
        {

            return 0;
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
                Console.WriteLine("destroyed collider");
                PhysicsWorld.Instance.DestroyCollider(this);
            }
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
