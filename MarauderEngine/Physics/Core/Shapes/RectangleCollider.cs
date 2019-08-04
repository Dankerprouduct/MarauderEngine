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
    [System.Obsolete]
    public class RectangleCollider: ICollider
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

        public Rectangle Rectangle;
        public RectangleCollider(Particle particle, Rectangle rectangle)
        {
            PartitionIndex = CellSpacePartition<ICollider>.Instance.PositionToIndex(particle.Position);
            Particle = particle;

            Rectangle = rectangle;
            rectangle.X = (int) particle.Position.X;
            rectangle.Y = (int) particle.Position.Y; 
            Active = true;
            this.Particle.Collider = this;
        }

        public RectangleCollider(Particle particle, Rectangle rectangle, IComponent owner)
        {
            Owner = owner;
            PartitionIndex = CellSpacePartition<ICollider>.Instance.PositionToIndex(particle.Position);
            Particle = particle;

            Rectangle = rectangle;
            rectangle.X = (int)particle.Position.X;
            rectangle.Y = (int)particle.Position.Y;
            Active = true;

            this.Particle.Collider = this;
        }
        public bool Intersects(ICollider other)
        {
            if (other is RectangleCollider)
            {
                RectangleCollider a = this;
                RectangleCollider b = (RectangleCollider) other;

                a.Rectangle.X = (int)a.Particle.Position.X;
                a.Rectangle.Y = (int)b.Particle.Position.Y;

                b.Rectangle.X = (int) b.Particle.Position.X;
                b.Rectangle.Y = (int) b.Particle.Position.Y;


                return a.Rectangle.Intersects(b.Rectangle);
            }

            if (other is Circle)
            {
                var circle = (Circle) other;
                Point pt = circle.Center.ToPoint();

                if (pt.X > Rectangle.Right) pt.X = Rectangle.Right;
                if (pt.X < Rectangle.Left) pt.X = Rectangle.Left;
                if (pt.Y > Rectangle.Bottom) pt.Y = Rectangle.Bottom;
                if (pt.Y < Rectangle.Top) pt.Y = Rectangle.Bottom;

                return (Vector2.Distance(pt.ToVector2(), circle.Center) < circle.GetRadius());
            }


            return false;
        }

        public bool Intersects(Point other)
        {
            return false;
        }

        public float GetRadius()
        {
            return ((float)Rectangle.Width / 2); 
        }

        public Rectangle GetRectangle()
        {
            Rectangle.X = (int) Particle.Position.X;
            Rectangle.Y = (int) Particle.Position.Y;
            return Rectangle;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void OnCollision(object sender, CollisionEvent args)
        {

        }

        public void DestroyCollider()
        {

        }
    }
}
