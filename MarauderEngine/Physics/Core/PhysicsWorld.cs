using System;
using System.Collections.Generic;
using System.Linq;
using MarauderEngine.Core;
using MarauderEngine.Physics.Core.Shapes;
using MarauderEngine.Physics.Core.SpatialPartition;
using Microsoft.Xna.Framework;
using Steamworks;
using Debug = MarauderEngine.Core.Debug;


namespace MarauderEngine.Physics.Core
{
    public class PhysicsWorld
    {
        public int[] loadedPartitions = new int[9];

        public CellSpacePartition<ICollider> ColliderPartition;

        private List<ICollider> _destroyQueue = new List<ICollider>();
        private List<ICollider> _collidersInArea;
        private List<int> partitionsToUpdate = new List<int>();
        public Vector2 Gravity = Vector2.Zero; 

        public static PhysicsWorld Instance;
        public PhysicsWorld(int width, int height, int partitionSize)
        {
            Instance = this; 

            ColliderPartition = new CellSpacePartition<ICollider>(width, height, partitionSize);
            _collidersInArea = new List<ICollider>();
            
        }

        public void SetGravity(Vector2 gravityVector)
        {
            Gravity = gravityVector; 
        }

        public void Add(ICollider collider)
        {

            Debug.Log($"Spawning Solid Collider @ {collider.Particle.Position}", Debug.LogType.Info, 2);
            ColliderPartition.Add(collider);
        }

        public void DestroyCollider(ICollider collider)
        {
            _destroyQueue.Add(collider);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _destroyQueue.Count; i++)
            {
                ColliderPartition.Remove(_destroyQueue[i]);
            }


            _collidersInArea.Clear();
            //center
            // delta = 150

            if (Camera.Instance == null)
            {
                throw  new Exception("Camera not initialized");
            }

            List<ICollider> tempColliders =
                ColliderPartition.Cells[ColliderPartition.PositionToIndex(Camera.Instance.center)].Members;

            if (tempColliders != null)
            {
                // TODO: SIMULATE EVERYTHING IN CAMERA BOUNDS
                loadedPartitions[0] = ColliderPartition.PositionToIndex(Camera.Instance.center);
                if (CellWithinBounds(loadedPartitions[0]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[0]].Members);
                }


                loadedPartitions[1] = ColliderPartition.PositionToIndex(Camera.Instance.center) + 150;
                if (CellWithinBounds(loadedPartitions[1]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[1]].GetMembers());
                }

                loadedPartitions[2] = ColliderPartition.PositionToIndex(Camera.Instance.center) - 150;
                if (CellWithinBounds(loadedPartitions[2]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[2]].GetMembers());
                }

                loadedPartitions[3] = ColliderPartition.PositionToIndex(Camera.Instance.center) - 151;
                if (CellWithinBounds(loadedPartitions[3]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[3]].GetMembers());
                }

                loadedPartitions[4] = ColliderPartition.PositionToIndex(Camera.Instance.center) - 149;
                if (CellWithinBounds(loadedPartitions[4]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[4]].GetMembers());
                }

                loadedPartitions[5] = ColliderPartition.PositionToIndex(Camera.Instance.center) + 149;
                if (CellWithinBounds(loadedPartitions[5]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[5]].GetMembers());
                }

                loadedPartitions[6] = ColliderPartition.PositionToIndex(Camera.Instance.center) + 151;
                if (CellWithinBounds(loadedPartitions[6]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[6]].GetMembers());
                }

                loadedPartitions[7] = ColliderPartition.PositionToIndex(Camera.Instance.center) + 1;
                if (CellWithinBounds(loadedPartitions[7]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[7]].GetMembers());
                }

                loadedPartitions[8] = ColliderPartition.PositionToIndex(Camera.Instance.center) - 1;
                if (CellWithinBounds(loadedPartitions[8]))
                {
                    _collidersInArea.AddRange(ColliderPartition
                        .Cells[loadedPartitions[8]].GetMembers());
                }
            }

            for (int i = 0; i < partitionsToUpdate.Count; i++)
            {
                _collidersInArea.AddRange(ColliderPartition
                    .Cells[partitionsToUpdate[i]].GetMembers());
            }

            UpdateParticles(_collidersInArea, gameTime);
            //CheckCollisions(_collidersInArea);

            partitionsToUpdate.Clear();

        }

        public void ChangeColliderPartition(ICollider member)
        {
            ColliderPartition.ChangeCell(member);
            AddCellToUpdate(member.PartitionIndex);
        }

        public void ForceUpdateCell(int cell, GameTime gameTime)
        {
            if (CellWithinBounds(cell))
            {
                if (!partitionsToUpdate.Contains(cell) && !loadedPartitions.Contains(cell))
                {
                    ColliderPartition.Cells[cell].Update(gameTime);
                }
            }
        }

        public bool CellWithinBounds(int cell)
        {
            return ColliderPartition.MemberWithinBounds(cell);
        }

        public int GetNumberOfActiveColliders()
        {
            return _collidersInArea.Count;
        }

        public void AddCellToUpdate(int cell)
        {
            if (!partitionsToUpdate.Contains(cell))
            {
                if (!loadedPartitions.Contains(cell))
                {
                    partitionsToUpdate.Add(cell);
                }
            }
        }

        Vector2 CalculateSeperatingVelocity(Particle a , Particle b, Vector2 normal)
        {
            Vector2 relativeVelocity = a.Velocity;
            relativeVelocity -= b.Velocity;
            return relativeVelocity * normal;
        }

        public void ResolveVelocity(ICollider collider)
        {

            foreach (var otherCollider in _collidersInArea)
            {
                if(!otherCollider.Active) return;
                if (collider != otherCollider)
                {
                    if (collider.Intersects(otherCollider))
                    {
                        Vector2 direction =
                            otherCollider.Particle.Position - collider.Particle.Position;

                        Vector2 normal = new Vector2(-direction.X, -direction.Y);
                        normal.Normalize();


                        Vector2 seperatingVelocity =
                            CalculateSeperatingVelocity(collider.Particle, otherCollider.Particle, normal);

                        if (seperatingVelocity.Length() > 0)
                        {
                            return;
                            
                        }

                        float e = Math.Min(collider.Particle.Restitution,
                            otherCollider.Particle.Restitution);

                        Vector2 newSepVelocity = -seperatingVelocity * e;
                        Vector2 deltaVelocity = newSepVelocity - seperatingVelocity;

                        float totalInverseMass = collider.Particle.InvertedMass + otherCollider.Particle.InvertedMass; ;

                        if(totalInverseMass <= 0) return;

                        Vector2 impulse = deltaVelocity / totalInverseMass;

                        Vector2 impulsePerIMass = normal * impulse;

                        collider.Particle.Velocity += impulsePerIMass * collider.Particle.InvertedMass;
                        collider.Particle.Velocity -= impulsePerIMass * collider.Particle.InvertedMass;

                    }
                }
            }
        }

        public void CheckCollision(ICollider collider)
        {
            foreach (var otherCollider in _collidersInArea)
            {
                if (collider != otherCollider)
                {
                    if (collider.Intersects(otherCollider))
                    {
                        
                        float totalInverseMass = collider.Particle.InvertedMass + otherCollider.Particle.InvertedMass;;
                        
                        // Generate Contact Data
                        // relative velocity
                        Vector2 relativeVelocity =
                            otherCollider.Particle.Velocity - collider.Particle.Velocity;
                        Vector2 direction =
                            otherCollider.Particle.Position - collider.Particle.Position;


                        Vector2 normal = new Vector2(-direction.X, -direction.Y);
                        normal.Normalize();

                        float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

                        if (velocityAlongNormal > 0)
                        {
                            return;
                        }




                        float e = Math.Min(collider.Particle.Restitution,
                            otherCollider.Particle.Restitution);

                        float j = -(1 + e) * velocityAlongNormal;
                        j /= totalInverseMass;


                        // Apply Impulse
                        Vector2 impulse = j * normal;


                        if (!float.IsNaN(impulse.X) && !float.IsNaN(impulse.Y))
                        {
                            
                            collider.Particle.Velocity -= (collider.Particle.InvertedMass * impulse);
                            collider.Particle.RevertPosition();

                            otherCollider.Particle.Velocity += (otherCollider.Particle.InvertedMass * impulse);
                            otherCollider.Particle.RevertPosition();
                        }
                    }
                }
            }
        }

        public bool CollidesWithCollider(ICollider collider, out ICollider colliderB)
        {
            if (!collider.Active)
            {
                colliderB = null;
                return false;

            }
            foreach (var otherCollider in _collidersInArea)
            {
                if (collider != otherCollider)
                {
                    if (otherCollider.Active)
                    {
                        if (collider.Intersects(otherCollider))
                        {
                            colliderB = otherCollider;
                            return true;

                        }
                    }
                }
            }

            colliderB = null;
            return false;
        }

        public bool CollidesWithCollider(ICollider collider, out ICollider colliderB, int layer)
        {
            if (!collider.Active)
            {
                colliderB = null;
                return false;

            }
            foreach (var otherCollider in _collidersInArea)
            {
                if (otherCollider.Layer == layer)
                {
                    if (collider != otherCollider)
                    {
                        if (otherCollider.Active)
                        {
                            if (collider.Intersects(otherCollider))
                            {
                                colliderB = otherCollider;
                                return true;

                            }
                        }
                    }
                }
            }

            colliderB = null;
            return false;
        }

        public Vector2 GetIntersectionDepth(Circle a, Circle b)
        {
            Vector2 direction = b.Particle.Position - a.Particle.Position;
            float distance = direction.Length();
            direction.Normalize();
            float depth = (a.GetRadius() + b.GetRadius()) - distance;
            return depth > 0 ? depth * direction : Vector2.Zero;
        }

        public float CalculateSeperatingVelocity()
        {
            return 0; 
        }

        public void UpdateParticles(List<ICollider> collidersInArea, GameTime gameTime)
        {
            foreach (var t in collidersInArea)
            {
                t.Update(gameTime);
                
            }
        }
        
        public void DrawDebug()
        {
            foreach (var collider in _collidersInArea)
            {
                GUI.GUI.DrawCircle(collider.Particle.Position, (int)((Circle)collider).GetRadius(), Color.Teal * .5f);
                   
            }
        }

        public override string ToString()
        {
            string s = "Partitions To Update:";
            for( int i = 0; i < partitionsToUpdate.Count; i++)
            {
                s += $" {partitionsToUpdate[i]}";
            }
            s += "\n";

            return s;
        }
    }
}
