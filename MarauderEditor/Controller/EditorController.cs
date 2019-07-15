using System;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEditor.Controller
{

    public class EditorController
    {
        //Transform
        //RemoveState
        //AddEntityState
        //None
        public static EditorController Instance;

        public IState CurrentState;
        public EditorController()
        {
            Instance = this;
            CurrentState = new IdleState();
            CurrentState.Enter();
        }
        public void Update(GameTime gameTime)
        {

            var state = CurrentState.Update(gameTime);
            if (state != null)
            {
                CurrentState = state;
                state.Enter();
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentState.Draw(spriteBatch);
        }
    }
}
