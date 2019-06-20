using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Core
{
    public class MarauderComponent: DrawableGameComponent
    {
        public SceneManagement SceneManagement;

        private SpriteBatch spriteBatch;
        public MarauderComponent(Game game) : base(game)
        {
            //SceneManagement = new SceneManagement();
            spriteBatch = (SpriteBatch) game.Services.GetService(typeof(SpriteBatch));
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            SceneManagement.CurrentScene?.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}
