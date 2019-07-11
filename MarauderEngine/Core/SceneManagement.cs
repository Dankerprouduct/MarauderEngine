using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components;
using MarauderEngine.Components.Data;
using MarauderEngine.Core.DataStructures;
using MarauderEngine.Entity;
using MarauderEngine.Systems;
using MarauderEngine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IComponent = MarauderEngine.Components.IComponent;

namespace MarauderEngine.Core
{
    public class SceneManagement : SystemManager<SceneManagement>
    {
        public static Scene CurrentScene;

        List<Scene> _scenes = new List<Scene>();
        public static GraphicsDevice GraphicsDevice; 
        public SceneManagement(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            EntityTagSystem.Instance = new EntityTagSystem();
        }

        public SceneManagement()
        {
            EntityTagSystem.Instance = new EntityTagSystem();
        }
        public override void Initialize()
        {
            
        }

        public void AddScene(Scene scene)
        {
            _scenes.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {
            _scenes.Remove(scene);
        }

        public void SetScene(Scene scene)
        {
            CurrentScene = scene; 
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public void SaveScene()
        {
            CurrentScene.SaveScene();
        }

        private string folderPath = @"Saves\";
        public void SetFolderPath(string newPath)
        {
            folderPath = newPath;
        }
        public Scene LoadScene(string filename)
        {
            GameData<SceneData> LoadedSceneData = new GameData<SceneData>();
            LoadedSceneData.folderPath = folderPath;
            SceneData loadedScene = LoadedSceneData.LoadObjectData(filename);

            Scene scene = new Scene(loadedScene.SceneName, 602, 602);
            
            // add back in dynamic entities
            EntityData[] dynamicEntities = loadedScene.DynamicEntityData;

            Console.WriteLine("LOADING DYNAMIC ENTITIES");
            for (int i = 0; i < dynamicEntities.Length; i++)
            {

                Entity.Entity entity = Activator.CreateInstance(dynamicEntities[i].EntityType) as Entity.Entity;                
                entity.EntityData = new EntityData();
                entity.EntityData.Children = dynamicEntities[i].Children;
                
                

                for (int c = 0; c < dynamicEntities[i].Components.Count; c++)
                {
                    IComponent component = Activator.CreateInstance(dynamicEntities[i].Components[c].ComponentType) as IComponent;


                    entity.ForceAddComponent(component);

                    component.Data = dynamicEntities[i].Components[c];
                    component.RegisterComponent(entity, dynamicEntities[i].Components[c].Name);
                    component.Owner = entity;
                    
                    
                }
                

                scene.AddDynamicEntity(entity);
            }
            Console.WriteLine("LOADED DYNAMIC ENTITIES");
            Console.WriteLine("LOADING STATIC ENTITIES");

            // add back in static entities 
            EntityData[] staticEntities = loadedScene.StaticEntityData;

            for (int i = 0; i < staticEntities.Length; i++)
            {

                Entity.Entity entity = Activator.CreateInstance(staticEntities[i].EntityType) as Entity.Entity;
                entity.EntityData = new EntityData();
                entity.EntityData.Children = staticEntities[i].Children;

                for (int c = 0; c < staticEntities[i].Components.Count; c++)
                {
                    IComponent component = Activator.CreateInstance(staticEntities[i].Components[c].ComponentType) as IComponent;

                    entity.ForceAddComponent(component);

                    component.Data = staticEntities[i].Components[c];
                    component.RegisterComponent(entity, staticEntities[i].Components[c].Name);
                    component.Owner = entity;
                }


                scene.AddStaticEntity(entity);
                entity = null;
            }
            Console.WriteLine("LOADED STATIC ENTITIES");
            return scene;
        }

        public static T Cast<T>(object o)
        {
            return (T)o;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CellSpacePartition.DrawnEntities = 0;

            CurrentScene.DrawScene(spriteBatch);
        }

    }
}
