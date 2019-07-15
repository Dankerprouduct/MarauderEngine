using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEditor.CustomComponents;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEditor.Controller
{
    public class IdleState: IState
    {
        public void Enter()
        {
            //MarauderMenuStrip.CursorToggle.Checked = false;
        }

        public void Exit()
        {

        }

        public IState Update(GameTime gameTime)
        {
            // if cursorIcon selected
            if (MarauderMenuStrip.CursorToggle.Checked)
            {
                if (CheckForTransformState(out var state)) return state;
            }


            // add entity state 

            return null;
        }

        private static bool CheckForTransformState(out IState state)
        {
            if (InputManager.Instance.MouseLeftClicked())
            {
                var entity =
                    Game1.LoadedScene?.CellSpacePartition.GetEntity(InputManager.Instance.GetMouseWorldPosition().ToPoint());
                if (entity != null)
                {
                    EntityProperties.Instance.LoadEntity(entity.EntityData);
                    {
                        state = new TransformState(entity);
                        return true;
                    }
                }
            }

            state = null;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
