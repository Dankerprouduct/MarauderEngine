using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using MarauderEditor.Controller;
using MarauderEngine.Components;
using MarauderEngine.Core;
using MarauderEngine.Entity;
using MarauderEngine.Graphics;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Components;
using MonoGame.Forms.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MarauderEditor.CustomComponents
{
    public class Game1 : MonoGameControl
    {
        public static Game1 Instance;
        private Texture2D pixel;
        public MarauderComponent MarauderComponent;
        public static Scene LoadedScene;
        public Game1(MarauderComponent marauderComponent): base()
        {
            //Console.WriteLine(Editor.Content);
            MarauderComponent = marauderComponent;
            Instance = this;
        }

        private TransformMovement transformMovement = new TransformMovement();

        private EditorController editorController;

        protected override void Initialize()
        {
            
            base.Initialize();
            

            Camera.Instance = new Camera(Editor.graphics.Viewport);
            Camera.Instance.SetMoveSpeed(20);
            Camera.Instance.SetZoomSpeed(.05f);

            Resize += ResizeGameWindow;


            InputManager.Instance = new InputManager();
            SceneManagement.Instance = new SceneManagement(Editor.graphics);
            var scene = new Scene("Default Scene", 602, 602);
            SceneManagement.Instance.SetScene(scene);

            pixel = new Texture2D(Editor.graphics, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            transformMovement.LoadContent(Editor.Content);
            TransformState.LoadContent(Editor.Content);

            editorController = new EditorController();

        }

        public void ResizeGameWindow(object sender, EventArgs e)
        {
            Camera.Instance = new Camera(Editor.graphics.Viewport);
            Camera.Instance.SetMoveSpeed(20);
            Camera.Instance.SetZoomSpeed(.05f);
        }

        public void LoadProject(string path)
        {
            //MarauderComponent.SceneManagement = new SceneManagement();
            SceneManagement.Instance.SetFolderPath(Path.GetDirectoryName(path));


            JsonSerializerSettings settings = new JsonSerializerSettings();
            SerializationBinder binder = new DefaultSerializationBinder();
            //binder.BindToName();
            //settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;
            //SceneManagement.Instance.SetSettings(settings);
            //var assembly = Assembly.LoadFrom(EditorComponent.AssemblyPath);
            //LoadedScene = SceneManagement.Instance.LoadScene(
            //    Path.GetFileNameWithoutExtension(path),
            //    EditorComponent.AssemblyPath);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                //Console.WriteLine($"assembly: {assembly}");
            }
            LoadedScene = SceneManagement.Instance.LoadScene(Path.GetFileNameWithoutExtension(path));

        }

        public void LoadContent<T>() where T: MarauderComponent
        {
            var marauderComponent = Activator.CreateInstance<T>();
        }

        public void LoadContent(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException();
            }

            DirectoryInfo[] directories = dir.GetDirectories();
            List<AssetBrowser.Asset> bitmaps = new List<AssetBrowser.Asset>();
            
            foreach (var dirs in directories)
            {
                if (dirs.Name != "bin")
                {
                    FileInfo[] files = dirs.GetFiles("*.*");
                    foreach (var file in files)
                    {
                        string key = Path.GetFileNameWithoutExtension(file.Name);
                        switch (file.Extension)
                        {
                            case ".xnb":
                            {
                                TextureManager.LoadContent<Texture2D>(file.Directory.Name, Path.GetFileNameWithoutExtension(file.Name), Editor.Content);
                                var asset = new AssetBrowser.Asset()
                                {
                                    Key = key,
                                    Bitmap = ConvertToBitmap(key)
                                };

                                bitmaps.Add(asset);
                                Console.WriteLine("Added something");
                                break;
                            }
                            case ".fx":
                            {
                                //TextureManager.LoadContent<Effect>(dirs.FullName, key, Editor.Content);
                                break;
                            }

                            case ".spritefont":
                            {
                                //TextureManager.LoadContent<SpriteFont>(dirs.FullName, key, Editor.Content);
                                break;
                            }


                        }
                        
                        //_contentDictionary.Add(key, content.Load<T>(folder + "/" + key));
                    }
                }
            }

            AssetBrowser.Instance.AddContent(bitmaps);


        }

        public Bitmap ConvertToBitmap(string key)
        {
            MemoryStream memoryStream = new MemoryStream();
            TextureManager.GetContent<Texture2D>(key).SaveAsPng(memoryStream,
                TextureManager.GetContent<Texture2D>(key).Width,
                TextureManager.GetContent<Texture2D>(key).Height);
            return new Bitmap(memoryStream);
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsMouseInsideControl)
            {
                InputManager.Instance.BeginInput();
                Camera.Instance.Update();
                Camera.Instance.WSADMovement();

                LoadedScene?.UpdateDynamicCellPartition(gameTime);

                //transformMovement.Update(Editor.GameTime, LoadedScene);
                editorController.Update(gameTime);



                base.Update(gameTime);
                InputManager.Instance.EndInput();
            }
        }

        private Entity ent;
        public void AddEntity(Entity entity)
        {
            ent = entity;
            LoadedScene.AddDynamicEntity(entity);
            
        }

        protected override void Draw()
        {

            base.Draw();

            Editor.graphics.Clear(new Color(18,18,18));

            Editor.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null, null, Camera.Instance.transform);
            
            
            LoadedScene?.Draw(Editor.spriteBatch);
            
            //ent?.Draw(Editor.spriteBatch);

            //transformMovement.Draw(Editor.spriteBatch);
            editorController.Draw(Editor.spriteBatch);


            Editor.spriteBatch.End();
            Editor.spriteBatch.Begin();

            Editor.spriteBatch.DrawString(Editor.Font,
                $"Mouse position: {InputManager.Instance.GetMouseWorldPosition()}",
                new Vector2(10,10), Color.White);
            



            Editor.spriteBatch.End();
        }
    }
}
