using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components;
using MarauderEngine.Physics.Core;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Core
{
    public class Scene
    {

        public PhysicsWorld PhysicsWorld;
        public CellSpacePartition CellSpacePartition;

        // update queue for cell space partition 
        List<int> _partitionsToUpdate = new List<int>();
        /// <summary>
        /// Set this to true if you want to manually update partitions instead of allowing the scene to control it 
        /// </summary>
        public bool ManuallyUpdateScene { get; set; }

        private RenderTarget2D _scene; 
        public Scene(int width, int height)
        {
            PhysicsWorld = new PhysicsWorld(width, height,4);
            CellSpacePartition = new CellSpacePartition(width, height);
            ManuallyUpdateScene = false;
            _scene = new RenderTarget2D(SceneManagement.GraphicsDevice,
                SceneManagement.GraphicsDevice.PresentationParameters.BackBufferWidth,
                SceneManagement.GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                SceneManagement.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!ManuallyUpdateScene)
            {
                PhysicsWorld.Update(gameTime);
            }
        }

        /// <summary>
        /// Adds entity to dynamic cell partition
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddDynamicEntity(MarauderEngine.Entity.Entity entity)
        {
            CellSpacePartition.AddDynamicEntity(entity);
        }

        /// <summary>
        /// Adds entity to the static cell partition
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddStaticEntity(Entity.Entity entity)
        {
            CellSpacePartition.AddStaticEntity(entity);
        }

        public void UpdateDynamicCellPartition(GameTime gameTime)
        {
            if (EntityWithinBounds(CellSpacePartition.GetCenterPartition(Camera.Instance.center)))
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
                CellSpacePartition.dynamicCells[_partitionsToUpdate[i]].Update(gameTime);
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

        /// <summary>
        /// returns list of loaded partitions
        /// </summary>
        /// <returns></returns>
        public List<int> GetPartitionsLoaded()
        {
            return _partitionsToUpdate;
        }

        public bool EntityWithinBounds(int checkedCell)
        {
            if (checkedCell >= 0 && checkedCell < CellSpacePartition.cellLength)
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_scene, Vector2.Zero, Color.White);
            DrawDynamicCellPartition(spriteBatch);
        }
        

        [Obsolete("use Draw for now.")]
        public void DrawScene(SpriteBatch spriteBatch)
        {
            SceneManagement.GraphicsDevice.SetRenderTarget(_scene);
            
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null,
                null,
                null, Camera.Instance.transform);

            DrawDynamicCellPartition(spriteBatch);

            spriteBatch.End();

            SceneManagement.GraphicsDevice.SetRenderTarget(null);
        }

        private void DrawDynamicCellPartition(SpriteBatch spriteBatch)
        {
            
            for (int x = Camera.Instance.GetTopLeftCell(); x <= Camera.Instance.GetTopRightCell(); x += 150)
            {
                int yy = 0;
                for (int y = Camera.Instance.GetTopLeftCell(); y <= Camera.Instance.GetBottomLeft(); y++)
                {
                    var drawnCell = x + yy;

                    if (EntityWithinBounds(drawnCell))
                    {
                        CellSpacePartition.staticCells[drawnCell].Draw(spriteBatch);
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
                        CellSpacePartition.dynamicCells[drawnCell].Draw(spriteBatch);
                    }

                    yy++;
                }
            }


        }
        
        public bool FireGlobalEvent(Event _event, MarauderEngine.Entity.Entity entity)
        {
            return false;
        }
    }
}
