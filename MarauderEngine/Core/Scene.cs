using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components;
using MarauderEngine.Core.DataStructures;
using MarauderEngine.Entity;
using MarauderEngine.Physics.Core;
using MarauderEngine.Systems;
using MarauderEngine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Core
{
    public class Scene
    {

        public SceneData SceneData = new SceneData();

        public string SceneName
        {
            get => SceneData.SceneName;
            set => SceneData.SceneName = value; 
        }
        public PhysicsWorld PhysicsWorld;
        public CellSpacePartition CellSpacePartition;

        // update queue for cell space partition 
        List<int> _partitionsToUpdate = new List<int>();

        /// <summary>
        /// Set this to true if you want to manually update partitions instead of allowing the scene to control it 
        /// </summary>
        public bool ManuallyUpdateScene
        {
            get => SceneData.ManuallyUpdateScene;
            set => SceneData.ManuallyUpdateScene = value;
        }

        private RenderTarget2D _scene; 

        /// <summary>
        /// It is recommended to use  602 for both the width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Scene(int width, int height)
        {
            PhysicsWorld = new PhysicsWorld(width, height,4);
            CellSpacePartition = new CellSpacePartition(width, height);
            ManuallyUpdateScene = false;

            if (string.IsNullOrEmpty(SceneName))
            {
                SceneName = "Scene";
            }

            //_scene = new RenderTarget2D(SceneManagement.GraphicsDevice,
            //    SceneManagement.GraphicsDevice.PresentationParameters.BackBufferWidth,
            //    SceneManagement.GraphicsDevice.PresentationParameters.BackBufferHeight,
            //    false,
            //    SceneManagement.GraphicsDevice.PresentationParameters.BackBufferFormat,
            //    DepthFormat.Depth24);
        }

        public Scene(string sceneName,int width, int height): this(width, height)
        {
            SceneName = sceneName;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!ManuallyUpdateScene)
            {
                PhysicsWorld.Update(gameTime);
                UpdateDynamicCellPartition(gameTime);
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

        public void SaveScene()
        {
            // save dynamic entities 
            List<EntityData> dynamicEntityList = new List<EntityData>();
            //var dynamicEntityData = CellSpacePartition.dynamicCells.Where(i => i.members != null)
            //    .SelectMany(m => m.members).Select(d => d.EntityData).ToArray();

            for (int i = 0; i < CellSpacePartition.dynamicCells.Length; i++)
            {
                if (CellSpacePartition.dynamicCells[i].members != null)
                {
                    for (int j = 0; j < CellSpacePartition.dynamicCells[i].members.Count; j++)
                    {
                        var entity = CellSpacePartition.dynamicCells[i].members[j];
                        var eType = CellSpacePartition.dynamicCells[i].members[j].GetType();
                        var data = CellSpacePartition.dynamicCells[i].members[j].EntityData;
                        data.EntityType = eType;
                        Console.WriteLine(data.EntityType);
                        dynamicEntityList.Add(data);
                    }
                }
            }

            var dynamicEntityData = dynamicEntityList.ToArray();

            //(d => d.EntityData).ToArray();

            // save static entities
            List<EntityData> staticEntityList = new List<EntityData>();
            //var staticEntityData = CellSpacePartition.staticCells.Where(i => i.members != null)
            //    .SelectMany(m => m.members).Select(d => d.EntityData).ToArray();
            // setting scene data 


            for (int i = 0; i < CellSpacePartition.staticCells.Length; i++)
            {
                if (CellSpacePartition.staticCells[i].members != null)
                {
                    for (int j = 0; j < CellSpacePartition.staticCells[i].members.Count; j++)
                    {
                        var entity = CellSpacePartition.staticCells[i].members[j];
                        var eType = CellSpacePartition.staticCells[i].members[j].GetType();
                        var data = CellSpacePartition.staticCells[i].members[j].EntityData;
                        data.EntityType = eType;
                        Console.WriteLine(data.EntityType);
                        staticEntityList.Add(data);
                    }
                }
            }

            var staticEntityData = staticEntityList.ToArray();

            SceneData.DynamicEntityData = dynamicEntityData;
            SceneData.StaticEntityData = staticEntityData;


            GameData<SceneData> CurrentSceneData = new GameData<SceneData>();
            CurrentSceneData.SaveData(SceneData, SceneName, ".json");

        }
        public bool FireGlobalEvent(Event _event, MarauderEngine.Entity.Entity entity)
        {
            return false;
        }
    }
}
