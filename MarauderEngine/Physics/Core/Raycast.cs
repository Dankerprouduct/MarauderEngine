using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Physics.Core
{
    public class Raycast
    {
        public List<Point> Points = new List<Point>();
        public Raycast()
        {

        }

        public bool MakeRay(Vector2 origin, float angle, int distance, int step, out Vector2 hitPosition)
        {
            Points.Clear();
            float cosX = (float)(Math.Cos(angle));
            float cosY = (float)(Math.Sin(angle));

            for (int i = 0; i <= distance; i += step)
            {
                int x = (int)(cosX * i) + (int)origin.X;
                int y = (int)(cosY * i) + (int)origin.Y;

                Point rayPoint = new Point(x,y);
                Points.Add(rayPoint);

                 List<ICollider> collidersInCell = PhysicsWorld.Instance.ColliderPartition
                    .Cells[PhysicsWorld.Instance.ColliderPartition.PositionToIndex(rayPoint.ToVector2())].Members;

                if (collidersInCell != null)
                {
                    for (int c = 0; c < collidersInCell.Count; c++)
                    {
                        if (collidersInCell[c].Intersects(rayPoint))
                        {
                            hitPosition = rayPoint.ToVector2();
                            return true;
                        }
                    }
                }
            }

            hitPosition = Vector2.Zero;
            return false;
        }
    }
}
