using System;
using MarauderEngine.Components;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Physics.Core
{
    public interface ICollider
    {
        /// <summary>
        /// Represents the center of the collider
        /// </summary>
        Particle Particle { get; set; }
        Vector2 Center { get; set; }
        int PartitionIndex { get; set; }
        int OldPartitionIndex { get; set; }
        bool Colliding { get; set; }
        bool Active { get; set; }
        int Layer { get; set; }


        IComponent Owner { get; set; }

        bool Intersects(ICollider other);
        
        bool Intersects(Point other);
        
        void Update(GameTime gameTime);

        void OnCollision(object sender, CollisionEvent args);

        void DestroyCollider();
    }
}
