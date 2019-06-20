using System;
using System.Collections.Generic;
using System.Threading;
using MarauderEngine.Components;
using MarauderEngine.Core;
using MarauderEngine.Core.Render;
using MarauderEngine.Graphics;
using MarauderEngine.Physics.Core;
using MarauderEngine.Physics.Core.SpatialPartition;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.World
{
    [Obsolete("use a Scene instead.", true)]
    public class World
    {

        /// <summary>
        /// if set to false you must manually update the cellspacepartition and physics partition
        /// </summary>
        public bool AutomaticUpdating = true; 

        public static World Instance;
        /// <summary>
        /// NPCS, and breakable walls live here
        /// </summary>
        public CellSpacePartition cellSpacePartition;
        
        public PhysicsWorld PhysicsWorld;
        private Thread PhysicsThread; 

        List<int> _partitionsToUpdate = new List<int>();

        private EntityTagSystem _entityTagSystem = new EntityTagSystem();

        public World(int width, int height)
        {
            Instance = this; 
            PhysicsWorld = new PhysicsWorld(width, height, 4);
            cellSpacePartition = new CellSpacePartition(width, height);            
        }

        public void Update(GameTime gameTime)
        {

            if (AutomaticUpdating)
            {
                //PhysicsThread = new Thread(() => StartPhysicsThread(gameTime));

                //if (!PhysicsThread.IsAlive)
                //{
                //}

                //PhysicsThread.Join();

                PhysicsWorld.Update(gameTime);

                UpdateDynamicCellPartition(gameTime);
            }
        }

        public void StartPhysicsThread(GameTime gameTime)
        {

            PhysicsWorld.Update(gameTime);
        }

        /// <summary>
        /// Adds entity to dynamic cell partition
        /// </summary>
        /// <param name="entity"></param>
        public void AddDynamicEntity(MarauderEngine.Entity.Entity entity)
        {
            cellSpacePartition.AddDynamicEntity(entity); 
        }

        /// <summary>
        /// Adds entity to the static cell partition
        /// </summary>
        /// <param name="entity"></param>
        public void AddStaticEntity(Entity.Entity entity)
        {
            cellSpacePartition.AddStaticEntity(entity);
        }
        
        public void UpdateDynamicCellPartition(GameTime gameTime)
        {
            if (EntityWithinBounds( CellSpacePartition.GetCenterPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetCenterPartition(Camera.Instance.center));

                //cellSpacePartition.dynamicCells[CellSpacePartition.GetCenterPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetTopLeftPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetTopLeftPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetTopLeftPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetTopPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetTopPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetTopPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetTopRightPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetTopRightPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetTopRightPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetRightPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetRightPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetRightPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetLeftPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetLeftPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetLeftPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetBottomLeftPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetBottomLeftPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetBottomLeftPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetBottomPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetBottomPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetBottomPartition(Camera.Instance.center)].Update(gameTime);
            }

            if (EntityWithinBounds(CellSpacePartition.GetBottomRightPartition(Camera.Instance.center)))
            {
                AddCellToUpdate(CellSpacePartition.GetBottomRightPartition(Camera.Instance.center));
                //cellSpacePartition.dynamicCells[CellSpacePartition.GetBottomRightPartition(Camera.Instance.center)].Update(gameTime);
            }

            for (int i = 0; i < _partitionsToUpdate.Count; i++)
            {
                cellSpacePartition.dynamicCells[_partitionsToUpdate[i]].Update(gameTime);
            }

            // clear the partitions after every update 
            _partitionsToUpdate.Clear();
        }

        public void AddCellToUpdate(int cell)
        {
            if (!_partitionsToUpdate.Contains(cell))
            {
                if (EntityWithinBounds(cell))
                {
                    _partitionsToUpdate.Add(cell);
                }
            }
        }

        public List<int> GetPartitionsLoaded()
        {
            return _partitionsToUpdate;
        }

        public void DrawDynamicCellPartition(SpriteBatch spriteBatch)
        {
            for (int x = Camera.Instance.GetTopLeftCell(); x <= Camera.Instance.GetTopRightCell(); x += 150)
            {
                int yy = 0;
                for (int y = Camera.Instance.GetTopLeftCell(); y <= Camera.Instance.GetBottomLeft(); y++)
                {
                    var drawnCell = x + yy;

                    if (EntityWithinBounds(drawnCell))
                    {
                        cellSpacePartition.staticCells[drawnCell].Draw(spriteBatch);
                    }

                    yy++;
                }
            }

            for (int x = Camera.Instance.GetTopLeftCell(); x <= Camera.Instance.GetTopRightCell(); x += 150)
            {
                int yy = 0;
                for (int y = Camera.Instance.GetTopLeftCell(); y <= Camera.Instance.GetBottomLeft(); y++)
                {
                    var drawnCell = x + yy;

                    if (EntityWithinBounds(drawnCell))
                    {
                        cellSpacePartition.dynamicCells[drawnCell].Draw(spriteBatch);
                    }

                    yy++;
                }
            }

            
        }

        public bool EntityWithinBounds(int checkedCell)
        {
            if (checkedCell >= 0 && checkedCell < cellSpacePartition.cellLength)
            {
                return true;
            }
            return false;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawDynamicCellPartition(spriteBatch); 


        } 

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {

                }
            }
        }

        public bool FireGlobalEvent(Event _event, MarauderEngine.Entity.Entity entity)
        {
            
            return false; 
        }
        
    }
}
