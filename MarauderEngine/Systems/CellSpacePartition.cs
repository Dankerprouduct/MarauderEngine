using System;
using System.Collections.Generic;
using System.Linq;
using MarauderEngine.Components;
using MarauderEngine.Core;
using MarauderEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MarauderEngine.Systems
{
    public class CellSpacePartition
    {
        public static int DrawnEntities = 0;
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
                            //members[i].DestroyEntity();
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
                            //members.RemoveAt(i);
                            RemoveEntity(members[i]); 
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
                        if (members[i].active)
                        {
                            var position = members[i].GetComponent<TransformComponent>().Position;
                            if (position.X + 256 >= Camera.Instance.TopLeftPosition.X &&
                                position.X - 256 <= Camera.Instance.TopRightPosition.X &&
                                position.Y - 256 <= Camera.Instance.BottomLeftPosition.Y &&
                                position.Y + 256 >= Camera.Instance.TopLeftPosition.Y)
                            {
                                members[i].Draw(spriteBatch);
                                DrawnEntities++;
                            }
                        }
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
                    //for(int y = 0; y < members.GetLength(1); y++)
                    //{
                    //    for(int x = 0; x < members.GetLength(0); x++)
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
                if (members != null)
                {
                    for (int x = 0; x < members.GetLength(0); x++)
                    {
                        for (int y = 0; y < members.GetLength(1); y++)
                        {

                            tempEmEntities.Add(members[x, y]);
                        }
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
                                var position = members[x,y].GetComponent<TransformComponent>().Position;
                                if (position.X + 256 >= Camera.Instance.TopLeftPosition.X &&
                                    position.X - 256 <= Camera.Instance.TopRightPosition.X &&
                                    position.Y - 256 <= Camera.Instance.BottomLeftPosition.Y &&
                                    position.Y + 256 >= Camera.Instance.TopLeftPosition.Y)
                                {
                                    members[x,y].Draw(spriteBatch);
                                    DrawnEntities++;
                                }
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

        public Vector2 offset; 

        public DynamicCell[] dynamicCells;
        public DynamicCell[] staticCells; 

        public CellSpacePartition(int cellX, int cellY, int partitionSize = 4)
        {
            cellX /= partitionSize;
            cellY /= partitionSize;
            this.partitionSize = partitionSize;

            int cellIndex = cellX * cellY;
            cellLength = cellIndex;
            dynamicCells = new DynamicCell[cellIndex];
            staticCells = new DynamicCell[cellIndex];

            for (int i = 0; i < cellIndex; i++)
            {
                dynamicCells[i] = new DynamicCell();
                staticCells[i] = new DynamicCell();
                
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

        public Entity.Entity GetEntity(Point point)
        {
            Entity.Entity entity = null;
            Console.WriteLine(point);


            for (int i = 0; i < staticCells.Length; i++)
            {
                if (staticCells[i].members != null)
                {
                    foreach (var ent in staticCells[i].members)
                    {
                        if (!ent.HasComponent<SpriteComponent>()) continue;
                        var rect = TextureManager
                            .GetContent<Texture2D>(ent.GetComponent<SpriteComponent>()
                                .TextureName).Bounds;

                        rect = new Rectangle(ent.GetComponent<TransformComponent>().Position.ToPoint(), new Point(rect.Width, rect.Height));
                        if (rect.Contains(point))
                        {
                            entity = ent;
                        }
                    }
                }
            }

            for (int i = 0; i < dynamicCells.Length; i++)
            {
                if (dynamicCells[i].members != null)
                {
                    foreach (var ent in dynamicCells[i].members)
                    {
                        if (!ent.HasComponent<SpriteComponent>()) continue;
                        var rect = ent.GetComponent<SpriteComponent>().Rectangle;
                        rect = new Rectangle(ent.GetComponent<TransformComponent>().Position.ToPoint(), new Point(rect.Width, rect.Height));
                        if (rect.Contains(point))
                        {
                            entity = ent;
                        }
                    }
                }
            }
            
            return entity;
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
        public void AddDynamicEntity(MarauderEngine.Entity.Entity entity)
        { 

            entity.SetPartitionCell(PositionToCell(entity).X, PositionToCell(entity).Y);
            entity.SetCellIndex(PositionToIndex(entity.GetComponent<TransformComponent>().Position));
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

            if (EntityWithinBounds(entity.cellIndex))
            {
                if (dynamicCells[entity.cellIndex].members != null)
                {

                    //int i = PositionToIndex(entity);
                    //if (i >= 0 && i < cellLength)
                    //{
                    //    dynamicCells[i].AddEntity(entity);

                    //}
                    dynamicCells[entity.cellIndex].AddEntity(entity);
                    dynamicCells[entity.oldCellIndex].RemoveEntity(entity);
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
            int cellX = ((int)(entity.GetEntityPosition().X - offset.X) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            int cellY = ((int)(entity.GetEntityPosition().Y - offset.Y) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            return new Point(cellX, cellY); 
        }

        public Point PositionToCell(Vector2 position)
        {
            int cellX = (int)((position.X - offset.X) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            int cellY = (int)((position.Y - offset.Y) / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
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




        #region  Helper Functions

        public static  int GetCurrentDynamicPartition(Vector2 position)
        {
            return SceneManagement.CurrentScene.CellSpacePartition.PositionToIndex(position);
        }

        /// <summary>
        /// Returns the center partition
        /// </summary>
        /// <returns>Center partition</returns>
        public static int GetCenterPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            return center;
        }

        /// <summary>
        /// Returns the partition right of the entity 
        /// </summary>
        /// <returns>partition right of the entity</returns>
        public static int GetRightPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int right = center + 150;
            return right;
        }

        /// <summary>
        /// returns the partition left of the entity
        /// </summary>
        /// <returns>partition left of the entity</returns>
        public static int GetLeftPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int left = center - 150;
            return left;
        }

        /// <summary>
        /// returns the partition towards the bottom of the entity
        /// </summary>
        /// <returns>partition south of entity</returns>
        public static int GetBottomPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int left = center - 1;
            return left;
        }

        /// <summary>
        /// returns the partition towards the top of the entity
        /// </summary>
        /// <returns>partition towards the top of the entity</returns>
        public static int GetTopPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int top = cellIndex + 1;
            return top;
        }

        /// <summary>
        /// returns the southeast partition
        /// </summary>
        /// <returns>souith east partition</returns>
        public static int GetBottomRightPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int bottomRight = center + 1 + 150;
            return bottomRight;
        }

        /// <summary>
        /// returns the bottom left partition 
        /// </summary>
        /// <returns>bottom left partition</returns>
        public static int GetBottomLeftPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int bottomLeft = center + 1 - 150;
            return bottomLeft;
        }

        /// <summary>
        /// returns the top right partition 
        /// </summary>
        /// <returns>top right partition</returns>
        public static int GetTopRightPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int topRight = center + 150 - 1;
            return topRight;
        }

        /// <summary>
        /// returns the top left partition
        /// </summary>
        /// <returns>top left partition</returns>
        public static int GetTopLeftPartition(Vector2 position)
        {
            int cellIndex = GetCurrentDynamicPartition(position);
            int center = cellIndex;
            int topLeft = center - 150 - 1;
            return topLeft;
        }

        #endregion

    }
}
