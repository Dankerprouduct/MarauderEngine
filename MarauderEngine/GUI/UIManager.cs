using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarauderEngine.GUI
{
    public class UIManager
    {
        public static UIManager Instance;
        private List<IUIElement> _uiElements = new List<IUIElement>();
        public MouseState CurrentMouseState;
        public MouseState PrevopisMouseState;
        public Point MousePosition;
        

        public UIManager()
        {
            Instance = this; 
        }

        public void Update()
        {
            CurrentMouseState = Mouse.GetState();
            MousePosition = new Point(CurrentMouseState.X, CurrentMouseState.Y);
            Point mousePoint = new Point(CurrentMouseState.X, CurrentMouseState.Y);

            foreach (var uiElement in _uiElements)
            {
                uiElement.Update();
            }
            


            PrevopisMouseState = CurrentMouseState;
        }


        public void AddElement(IUIElement element)
        {
            _uiElements.Add(element);
        }

        public void RemoveElement(IUIElement element)
        {
            _uiElements.Remove(element);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var uiElement in _uiElements)
            {
                uiElement.Draw(spriteBatch);
            }

        }

    }
}
