using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Physics.Core.SpatialPartition
{
    public class CellSpacePartition<T> where T : ICollider
    {
        public struct Cell
        {
            public List<T> Members;

            public void Add(T member)
            {
                if (Members != null)
                {
                    Members.Add(member);
                }
                else
                {
                    Members = new List<T>
                    {
                        member
                    };
                }
            }

            public List<T> GetMembers()
            {
                if (Members != null)
                {
                    return Members;
                }
                else
                {
                    return new List<T>();
                }
            }

            public void Remove(T member)
            {
                Members.Remove(member);

                if (Members != null && Members.Contains(member))
                {
                    //Members?.Remove(Members.Single(s => Equals(s, member)));
                    for(int i = 0; i < Members.Count; i++)
                    {
                        if(Members[i].Particle == member.Particle)
                        {
                            Members.RemoveAt(i);
                        }
                    }
                }
            }

            public void Update(GameTime gameTime)
            {
                if (Members == null) return;
                foreach (var member in Members)
                {
                    member.Particle.Update(gameTime);
                }
            }

        }


        public static CellSpacePartition<T> Instance;

        public int PartitionSize;
        public int CellLength;
        public int NumCellsX;
        public int NumCellsY;

        public Cell[] Cells; 

        public CellSpacePartition(int cellX, int cellY, int partitionSize)
        {
            Instance = this; 

            cellX /= partitionSize;
            cellY /= partitionSize;
            
            this.PartitionSize = partitionSize;

            CellLength = cellX * cellY; 
            Cells = new Cell[CellLength];

            for (int i = 0; i < CellLength; i++)
            {
                Cells[i] = new Cell();
            }


            NumCellsX = cellX;
            NumCellsY = cellY;
        }

        /// <summary>
        /// Adds item T to its proper cell
        /// </summary>
        /// <param name="member"></param>
        public void Add(T member)
        {
            member.PartitionIndex = PositionToIndex(member.Particle.Position);
            Cells[member.PartitionIndex].Add(member);
        }

        public void ChangeCell(T member)
        {
            if (MemberWithinBounds(member.PartitionIndex))
            {
                if (Cells[member.PartitionIndex].Members != null)
                {
                    Cells[member.OldPartitionIndex].Remove(member);
                    
                    if (MemberWithinBounds(PositionToIndex(member.Particle.Position)))
                    {
                        Cells[member.PartitionIndex].Add(member);
                       
                    }

                }
                else
                {
                    Cells[member.PartitionIndex].Members = new List<T>();
                    ChangeCell(member);
                }
            }
        }

        public void Remove(T member)
        {
            Cells[member.PartitionIndex].Remove(member);
            if (MemberWithinBounds(member.PartitionIndex))
            {
                //Cells[member.PartitionIndex].Remove(member);
            }
        }

        /// <summary>
        /// Returns a cell index from a position
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int PositionToIndex(T member)
        {
            return PositionToCell(member).X * NumCellsX + PositionToCell(member).Y;
        }

        /// <summary>
        /// Converts position to cell position
        /// </summary>
        /// <param name="position">Vector2</param>
        /// <returns>index of cell</returns>
        public int PositionToIndex(Vector2 position)
        {
            int cell = PositionToCell(position).X * NumCellsX + PositionToCell(position).Y;
            if (MemberWithinBounds(cell))
            {
                return cell;
            }

            return 0; 
        }
        
        /// <summary>
        /// changes vector to to a cell point
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Point PositionToCell(T member)
        {
            int cellX = (int)(member.Particle.Position.X / (PartitionSize * ((PartitionSize * PartitionSize) * 8)) / PartitionSize);
            int cellY = (int)(member.Particle.Position.Y / (PartitionSize * ((PartitionSize * PartitionSize) * 8)) / PartitionSize);
            return new Point(cellX, cellY);
        }

        /// <summary>
        /// Returns a point from a position X, Y
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Point PositionToCell(Vector2 position)
        {
            int cellX = (int)(position.X / (PartitionSize * ((PartitionSize * PartitionSize) * 8)) / PartitionSize);
            int cellY = (int)(position.Y / (PartitionSize * ((PartitionSize * PartitionSize) * 8)) / PartitionSize);
            return new Point(cellX, cellY);
        }

        /// <summary>
        /// Checks to see if cell is within bounds of partition
        /// </summary>
        /// <param name="checkedCell">Cell you want to check</param>
        /// <returns>if checked cell is within range</returns>
        public bool MemberWithinBounds(int checkedCell)
        {
            if (checkedCell >= 0 && checkedCell < CellLength)
            {
                return true;
            }
            return false;
        }
    }
}
