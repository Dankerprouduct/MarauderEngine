using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarauderEngine.Systems
{
    public class InputManager : SystemManager<InputManager>
    {

        public KeyboardState CurrentKeyboardState { get; private set; }
        public KeyboardState PrevioKeyboardState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }
        public MouseState PreviousMouseState { get; private set; }

        public InputManager()
        {

        }

        public override void Initialize()
        {

        }

        /// <summary>
        /// starts the capture of mouse and keyboard states
        /// </summary>
        public void BeginInput()
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// ends the caputre of mouse and keyboard states
        /// </summary>
        public void EndInput()
        {
            PrevioKeyboardState = CurrentKeyboardState; 
            PreviousMouseState = CurrentMouseState;
        }

        /// <summary>
        /// returns true when a key was pressed once
        /// </summary>
        /// <returns></returns>
        public bool KeyPressed(Keys key)
        {
            if (CurrentKeyboardState.IsKeyDown(key) && PrevioKeyboardState.IsKeyUp(key))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// returns if the current key is down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// returns true if right mouse was clicked
        /// </summary>
        /// <returns></returns>
        public bool MouseRightClicked()
        {
            return (CurrentMouseState.RightButton == ButtonState.Pressed &&
                    PreviousMouseState.RightButton == ButtonState.Released);
        }

        /// <summary>
        /// returns true if left mouse was clicked
        /// </summary>
        /// <returns></returns>
        public bool MouseLeftClicked()
        {
            return (CurrentMouseState.LeftButton == ButtonState.Pressed &&
                    PreviousMouseState.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// returns the position of the mouse in screen coordinates
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMouseScreenPosition()
        {
            return new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
        }


        /// <summary>
        /// returns the position of the mouse in world coordinates
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMouseWorldPosition()
        {
            return Vector2.Transform(GetMouseScreenPosition(), Matrix.Invert(Camera.Instance.transform));
        }

    }
}
