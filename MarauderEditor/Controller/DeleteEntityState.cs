using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEditor.Controller
{
    public class DeleteEntityState: IState

    {
        public Entity entityToDelete;
        public DeleteEntityState(Entity entity)
        {
            entityToDelete = entity;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public IState Update(GameTime gameTime)
        {
            entityToDelete.DestroyEntity();

            return new IdleState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
