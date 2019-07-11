using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Entity;
using MarauderEngine.Graphics;
using MarauderEngine.Systems;
using MarauderEngine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Core
{
    public class MarauderComponent: DrawableGameComponent
    {
        public MarauderComponent Instance;
        public TextureManager TextureManager;
        public SceneManagement SceneManagement;
        public SpriteBatch SpriteBatch;
        public ContentManager Content;
        public GraphicsDevice GraphicsDevice;

        /// <summary>
        /// Holds each type of entity in a scene
        /// </summary>
        public static Dictionary<Type, EntityData> EntityTypes = new Dictionary<Type, EntityData>();

        /// <summary>
        /// adds entity to the list of entity types
        /// </summary>
        /// <param name="entity"></param>
        public static void AddEntityType(Entity.Entity entity)
        {
            // adds this entity type to the list of entity types
            if (!EntityTypes.ContainsKey(entity.GetType().UnderlyingSystemType))
            {
                EntityTypes.Add(entity.GetType().UnderlyingSystemType, entity.EntityData);
            }
        }

        public MarauderComponent(Game game) : base(game)
        {
            Instance = this;
            //SceneManagement = new SceneManagement();
            TextureManager.Instance = new TextureManager();
            SpriteBatch = (SpriteBatch) game.Services.GetService(typeof(SpriteBatch));
            Content = (ContentManager) game.Services.GetService(typeof(ContentManager));
            GraphicsDevice = (GraphicsDevice) game.Services.GetService(typeof(GraphicsDevice));

        }

        public void InitializeSceneManagement()
        {
            SceneManagement.Instance = new SceneManagement();
            Console.WriteLine("Initialized Scene Management");
        }

        public override void Initialize()
        {

            SceneManagement.Instance = new SceneManagement();
            InputManager.Instance = new InputManager();
            Camera.Instance = new Camera(GraphicsDevice.Viewport);


            GUI.GUI gui = new GUI.GUI();
            GUI.GUI.spriteBatch = SpriteBatch;
            GUI.GUI.ScreenBounds = GraphicsDevice.Viewport.Bounds;
            
            Debug.Initialize("debugFont");

            base.Initialize();
        }

        public virtual void LoadContent()
        {
            
        }
        
        public void LoadTheContent(ContentManager content)
        {
            Content = content;
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Instance.BeginInput();
            SceneManagement.Instance?.Update(gameTime);
            Camera.Instance.Update();
            Debug.Update();

            base.Update(gameTime);
        }

        public virtual void EndUpdate(GameTime gameTime)
        {
            InputManager.Instance.EndInput();
        }
        
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null, null, Camera.Instance.transform);
            SceneManagement.CurrentScene.Draw(SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
