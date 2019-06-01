using System;
using System.Threading;
using MarauderEngine.Core;
using MarauderEngine.Entity.Items;
using MarauderEngine.Graphics;
using MarauderEngine.Physics.Core;
using MarauderEngine.Shaders;
using MarauderEngine.Systems;
using MarauderEngine.Utilities;
using MarauderEngine.Utilities.ScreenTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Debug = MarauderEngine.Core.Debug;

namespace MarauderEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Game1 game;
        public static Random random = new Random(92899);

        public static Camera camera;

        public static World.World world;

        public static int width = 1280;
        public static int height = 0; 

        /// <summary>
        /// Position of the mouse in the world
        /// </summary>
        public static Vector2 worldPosition;
        Vector2 mousePosition;
        
        private RenderTarget2D renderTarget1, renderTarget2;
        Bloom bloom;
        private PresentationParameters pp;
        
        private readonly FrameCounter _frameCounter = new FrameCounter();

        GifMaker gif;
        private Thread gifThread; 
        double lastUpdate = 33;
        bool gifSaveRequested = false;
        
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferWidth = width;
            
            height = (width / 16) * 9;
            
            graphics.PreferredBackBufferHeight = height;
            graphics.GraphicsProfile = GraphicsProfile.HiDef; 
            graphics.IsFullScreen = false;
            this.graphics.SynchronizeWithVerticalRetrace = true;
            this.IsFixedTimeStep = true; 
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pp = GraphicsDevice.PresentationParameters;

            gif = new GifMaker(GraphicsDevice);
            IsMouseVisible = true;
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bloom = new Bloom(GraphicsDevice, spriteBatch);
            bloom.Settings = BloomSettings.PresetSettings[3];
            renderTarget1 = new RenderTarget2D(GraphicsDevice, width, height, false, pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);
            renderTarget2 = new RenderTarget2D(GraphicsDevice, width, height, false, pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            TextureManager.LoadContent(Content);
            ParticleSystem.Init(50000);
            ItemDictionary.LoadItemDatabase();
            bloom.LoadContent(Content, pp);
           // GUI.GUI.Init();
            Reset();
            Debug.Log($"Entities: {MarauderEngine.Entity.Entity.nextAvailibleID:N}");
        }

        public void Reset()
        {


            world = new World.World(10, 10); 
            //world.AddDynamicEntity(player);
            camera = new Camera(GraphicsDevice.Viewport);
            
        }

        protected override void UnloadContent()
        {
            bloom.UnloadContent();
            renderTarget1.Dispose(); renderTarget2.Dispose();
            Debug.SaveLog();
        }
        
        protected override void Update(GameTime gameTime)
        {

            _currentKeyboardState = Keyboard.GetState(); 
            

            //bloom.Update();
            GUI.GUI.Draw(spriteBatch);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F12))
                Reset();

            // Save Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F11))
                GameManager.SaveGame();

            // Loads Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F10))
                GameManager.LoadGame();
            GameManager.Update();

            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.transform)) ;

            if (!Debug.Console.showDebug)
            {
                world.Update(gameTime);
                               
            }
            ProjectileManager.Update(gameTime);


            ParticleSystem.Update(gameTime);

            Debug.Update();

            game = this;
            camera.Update();
            
            base.Update(gameTime);

            GifStuff(gameTime);

            //GifStuff(gameTime);
            _previousKeyboardState = _currentKeyboardState;
        }
        

        public void GifStuff(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                //only send a frame every 250ms
                lastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (lastUpdate > 30)
                {

                    gifSaveRequested = true;
                    gif.AddFrame(30);
                    Debug.Log("added frame", Debug.LogType.Warning);
                    lastUpdate = 0;
                }
            }
            else if (gifSaveRequested)
            {
                Debug.Log("Saving gif", Debug.LogType.Warning);
                gifThread = new Thread(() => gif.WriteAllFrames());

                if (!gifThread.IsAlive)
                {
                    gifThread.Start();
                }
                else
                {
                    gifThread.Join();
                    Debug.Log("Saved Gif", Debug.LogType.Error);

                }

                gifSaveRequested = false;

            }

        }

        protected override void Draw(GameTime gameTime)
        {

            var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);
            var fps = $"FPS: {_frameCounter.AverageFramesPerSecond}";

            GraphicsDevice.SetRenderTarget(renderTarget1); // bloom shader
            GraphicsDevice.Clear(Color.TransparentBlack);
            // particle effects
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.transform);

            ParticleSystem.Draw(spriteBatch);
            spriteBatch.End();
            bloom.Draw(renderTarget1, renderTarget2);           

            GraphicsDevice.SetRenderTarget(null);



            // Background
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            world.DrawBackground(spriteBatch);
            spriteBatch.End();

            // player space
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.transform);
            GUI.GUI.Draw(spriteBatch);
            world.Draw(spriteBatch);

            ProjectileManager.Draw(spriteBatch);

            if (Debug.ShowTextualDebug)
            {
                PhysicsWorld.Instance.DrawDebug();
            }
            spriteBatch.End();


            // Draw bloomed layer over top: 
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(renderTarget2, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();

            // Dev stuff
            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp);
            GUI.GUI.Draw(spriteBatch);
            GUI.GUI.DrawString("DEVELOPMENT BUILD", new Vector2(GUI.GUI.ScreenBounds.X + 20, GUI.GUI.ScreenBounds.Height - 20),1, 1, Color.Gray);
            if (Debug.ShowTextualDebug)
            {

                GUI.GUI.DrawString("Mouse Position: " + worldPosition.ToString(), new Vector2(10, 10), 1, 1, Color.White);
                GUI.GUI.DrawString("Cell Mouse Position: " + (worldPosition / 128).ToPoint(), new Vector2(10, 30), 1, 1, Color.White);

                GUI.GUI.DrawString("Active Particles: " + ParticleSystem.currentParticles, new Vector2(10, 90), 1, 1, Color.White);
                GUI.GUI.DrawString($"Physics Partition: {PhysicsWorld.Instance.ColliderPartition.PositionToIndex(camera.center)}", new Vector2(10, 130), 1, 1, Color.White);
                GUI.GUI.DrawString($"{PhysicsWorld.Instance.ToString()}", new Vector2(10, 150), 1, 1, Color.White);
                GUI.GUI.DrawString($"Colliders: {PhysicsWorld.Instance.GetNumberOfActiveColliders()}", new Vector2(10, 170), 1, 1, Color.White); 

                GUI.GUI.DrawBox(GUI.GUI.ScreenBounds.Width - 82, GUI.GUI.ScreenBounds.Y, 100, 28, Color.Black *.7f);
                GUI.GUI.DrawString(fps,
                    new Vector2(
                        GUI.GUI.ScreenBounds.Width - 80,
                        GUI.GUI.ScreenBounds.Y + 10), 1, 1, Color.LimeGreen); 

                Debug.Draw(spriteBatch);
            }
            
            // draw inventory gui & regular gui
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
         
    }
}
