﻿using System.Collections.Generic;
using MarauderEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Systems
{
    public class DynamicCellPartition
    {
        public struct DynamicCell
        {
            public List<MarauderEngine.Entity.Entity> members;

            public void AddEntity(MarauderEngine.Entity.Entity entity)
            {
                if (members != null)
                {

                    members.Add(entity);
                    //Console.WriteLine("added " + entity);

                }
                else
                {
                    members = new List<MarauderEngine.Entity.Entity>
                    {
                        entity
                    };
                    //Console.WriteLine("added " + entity);

                }
            }

            public void RemoveEntity(MarauderEngine.Entity.Entity entity)
            {
                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        //Console.WriteLine("trying to remove entity...");
                        if (members[i].id == entity.id)
                        {
                            members[i].DestroyEntity();
                            members.RemoveAt(i);

                            //Console.WriteLine("removed " + entity);
                        }
                    }
                }
            }

            public void Update(GameTime gameTime)
            {
                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        if (members[i].active)
                        {
                            members[i].Update(gameTime);
                        }
                        else
                        {
                            members.RemoveAt(i);
                            //RemoveEntity(members[i]); 
                        }
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        members[i].Draw(spriteBatch);
                        GUI.GUI.DrawString(i.ToString(), members[i].GetComponent<TransformComponent>().Position, 1, 1f, Color.Green);
                    }
                }
            }

            public List<MarauderEngine.Entity.Entity> GetEntities()
            {
                return members;
            }

            public bool FireEvent(Event _event)
            {

                if (members != null && members.Count > 0)
                {
                    {
                        for (int i = 0; i < members.Count; i++)
                        {

                            if (members[i].FireEvent(_event))
                            {

                                return true;
                            }
                        }
                    }
                }

                return false;
            }

        }

        public struct StaticCell
        {
            public int x;
            public int y;
            public MarauderEngine.Entity.Entity[,] members;

            public void AddEntity(MarauderEngine.Entity.Entity entity)
            {
                if (members != null)
                {
                    //members.Add(entity);
                    int x = ((int)entity.GetComponent<TransformComponent>().Position.X % (16 * 128)) / 128;
                    int y = ((int)entity.GetComponent<TransformComponent>().Position.Y % (16 * 128)) / 128;

                    members[x, y] = entity;
                }
                else
                {
                    members = new MarauderEngine.Entity.Entity[16, 16];
                    //for (int y = 0; y < members.GetLength(1); y++)
                    //{
                    //    for (int x = 0; x < members.GetLength(0); x++)
                    //    {
                    //        members[x, y] = new MarauderEngine.Entity.Entity();
                    //    }
                    //}
                    x = entity.cellX;
                    y = entity.cellY;
                    AddEntity(entity);

                }
            }


            public List<MarauderEngine.Entity.Entity> GetEntities()
            {
                List<MarauderEngine.Entity.Entity> tempEmEntities = new List<MarauderEngine.Entity.Entity>();

                for (int x = 0; x < members.GetLength(0); x++)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {

                        tempEmEntities.Add(members[x, y]);
                    }
                }

                return tempEmEntities;
            }


            public MarauderEngine.Entity.Entity GetEntity(Point position)
            {
                int x = ((int)position.X % (16 * 128)) / 128;
                int y = ((int)position.Y % (16 * 128)) / 128;

                return members[x, y];
            }

            public Point GetEntityIndex(Point position)
            {
                int x = ((int)position.X % (16 * 128)) / 128;
                int y = ((int)position.Y % (16 * 128)) / 128;

                return new Point(x, y);
            }

            public void Update(GameTime gameTime)
            {
                if (members != null)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {
                        for (int x = 0; x < members.GetLength(0); x++)
                        {
                            if (members[x, y] != null)
                            {
                                members[x, y].Update(gameTime);
                            }
                        }
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                if (members != null)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {
                        for (int x = 0; x < members.GetLength(0); x++)
                        {
                            if (members[x, y] != null)
                            {
                                members[x, y].Draw(spriteBatch);
                            }
                        }
                    }
                }
            }

            public bool FireEvent(Event _event)
            {
                if (members != null)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {
                        for (int x = 0; x < members.GetLength(0); x++)
                        {
                            if (members[x, y] != null)
                            {
                                if (members[x, y].FireEvent(_event))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }

        public int partitionSize;
        public int cellLength;
        public int numCellsX;
        public int numCellsY;

        public Vector2 Position = Vector2.Zero; 

        public CellSpacePartition.DynamicCell[] dynamicCells;
        public CellSpacePartition.StaticCell[] staticCells;


        public DynamicCellPartition(int cellX, int cellY, Vector2 position, int partitionSize = 4)
        {
            Position = position; 

            cellX /= partitionSize;
            cellY /= partitionSize;
            this.partitionSize = partitionSize;

            int cellIndex = cellX * cellY;
            cellLength = cellIndex;
            dynamicCells = new CellSpacePartition.DynamicCell[cellIndex];
            staticCells = new CellSpacePartition.StaticCell[cellIndex];

            for (int i = 0; i < cellIndex; i++)
            {
                dynamicCells[i] = new CellSpacePartition.DynamicCell();
                staticCells[i] = new CellSpacePartition.StaticCell();

            }

            numCellsX = cellX;
            numCellsY = cellY;

        }

        /// <summary>
        /// retrieves dynamic entities in an array
        /// </summary>
        /// <returns></returns>
        public MarauderEngine.Entity.Entity[] GetDynamicEntities()
        {
            List<MarauderEngine.Entity.Entity> temp = new List<MarauderEngine.Entity.Entity>();
            for (int i = 0; i < dynamicCells.Length; i++)
            {
                if (dynamicCells[i].members != null)
                {
                    temp.AddRange(dynamicCells[i].GetEntities());
                }
            }

            return temp.ToArray();
            ///lis
        }

        // adds entity to appropriate static cell
        public void AddStaticEntity(MarauderEngine.Entity.Entity entity)
        {
            entity.SetPartitionCell(PositionToCell(entity).X, PositionToCell(entity).Y);
            entity.SetCellIndex(PositionToIndex(entity));
            staticCells[entity.cellIndex].AddEntity(entity);

        }

        /// <summary>
        /// Adds a dynamic entity to the proper cell
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(MarauderEngine.Entity.Entity entity)
        {

            entity.SetPartitionCell(PositionToCell(entity).X, PositionToCell(entity).Y);
            entity.SetCellIndex(PositionToIndex(entity));
            dynamicCells[PositionToIndex(entity)].AddEntity(entity);
            //Console.WriteLine("Added dynamic entity of type " + entity.GetType()); 
        }

        public void LoadEntity(MarauderEngine.Entity.Entity entity)
        {
            dynamicCells[PositionToIndex(entity)].AddEntity(entity);
        }


        // changes vector to to a 1D array index
        public int PositionToIndex(MarauderEngine.Entity.Entity entity)
        {
            return PositionToCell(entity).X * numCellsX + PositionToCell(entity).Y;
        }

        /// <summary>
        /// Converts position to cell position
        /// </summary>
        /// <param name="position">Vector2</param>
        /// <returns>index of cell</returns>
        public int PositionToIndex(Vector2 position)
        {
            return PositionToCell(position).X * numCellsX + PositionToCell(position).Y;
        }

        /// <summary>
        /// Changes cell property from one cell to another 
        /// </summary>
        /// <param name="entity"></param>
        public void ChangeCell(MarauderEngine.Entity.Entity entity)
        {
            //Console.WriteLine("changing cell"); 

            if (EntityWithinBounds(entity.cellIndex))
            {
                if (dynamicCells[entity.cellIndex].members != null)
                {
                    dynamicCells[entity.oldCellIndex].RemoveEntity(entity);

                    int i = PositionToIndex(entity);
                    if (i >= 0 && i < cellLength)
                    {
                        dynamicCells[PositionToIndex(entity)].AddEntity(entity);

                    }
                }
                else
                {
                    dynamicCells[entity.cellIndex].members = new List<MarauderEngine.Entity.Entity>();
                    ChangeCell(entity);
                }
            }

        }

        /// <summary>
        /// changes vector to to a cell point
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Point PositionToCell(MarauderEngine.Entity.Entity entity)
        {
            int cellX = ((int)(entity.GetEntityPosition().X - Position.X) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            int cellY = ((int)(entity.GetEntityPosition().Y - Position.Y) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            return new Point(cellX, cellY);
        }

        public Point PositionToCell(Vector2 position)
        {
            int cellX = (int)((position.X - Position.X) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            int cellY = (int)((position.Y - Position.Y) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            return new Point(cellX, cellY);
        }

        /// <summary>
        /// Checks to see if cell is within bounds of partition
        /// </summary>
        /// <param name="checkedCell">Cell you want to check</param>
        /// <returns>if checked cell is within range</returns>
        public bool EntityWithinBounds(int checkedCell)
        {
            if (checkedCell >= 0 && checkedCell < cellLength)
            {
                return true;
            }
            return false;
        }

        // updates current cell
        public void Update(GameTime gameTime)
        {

        }

    }
}

