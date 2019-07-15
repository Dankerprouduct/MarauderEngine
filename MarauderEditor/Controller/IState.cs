using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEditor.Controller
{
    public interface IState
    {
        void Enter();

        void Exit();

        IState Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
