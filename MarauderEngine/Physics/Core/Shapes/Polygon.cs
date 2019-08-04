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
    /// <summary>
    /// 
    /// </summary>
    [System.Obsolete]
    public class Polygon : ICollider
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

        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        private Rectangle _rect; 

        public Polygon(Rectangle rectangle)
        {
            _rect = rectangle; 
            TopLeft = new Vector2(rectangle.X, rectangle.Y);
            TopRight = new Vector2(rectangle.X + rectangle.Width, rectangle.Y);
            BottomLeft = new Vector2(rectangle.X, rectangle.Height);
            BottomRight = new Vector2(rectangle.Width, rectangle.Height);
        }

        public bool Intersects(ICollider other)
        {
            if (other is Circle)
            {
                return CircleLineIntersection((Circle) other, TopLeft, TopRight) ||
                       CircleLineIntersection((Circle) other, TopRight, BottomRight) ||
                       CircleLineIntersection((Circle) other, BottomRight, BottomLeft) ||
                       CircleLineIntersection((Circle) other, BottomLeft, TopLeft);
            }

            if (other is Polygon)
            {
                // cry
            }

            return false;
        }
        
        bool CircleLineIntersection(Circle circle, Vector2 vA, Vector2 vB)
        {
            Vector2 d = vB - vA;
            Vector2 f = vA - circle.Center;

            float a = Vector2.Dot(d, d);
            float b = 2 * Vector2.Dot(f, d);
            float c = Vector2.Dot(f, f) - circle.GetRadius() * circle.GetRadius();

            float discriminant = b * b - 4 * a * c;

            if(discriminant < 0)
            {
                // no intersection
                return false;
            }
            else
            {
                discriminant = (float)Math.Sqrt(discriminant);

                float t1 = (-b - discriminant) / (2 * a);
                float t2 = (-b + discriminant) / (2 * b);

                if (t1 >= 0 && t1 <= 1)
                {
                    return true;
                }
                if(t2 >= 0 && t2 <= 1)
                {
                    return true;
                }

                return false;
            }
        }


        // for raycasts
        public bool Intersects(Point other)
        {
            return _rect.Contains(other);
        }

        public float GetRadius()
        {
            throw new NotImplementedException();
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
