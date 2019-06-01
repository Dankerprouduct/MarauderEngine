using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Core
{
    public class SceneManagement : SystemManager<SceneManagement>
    {
        public static Scene CurrentScene;

        List<Scene> _scenes = new List<Scene>();

        public SceneManagement()
        {

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

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);
        }

    }
}
