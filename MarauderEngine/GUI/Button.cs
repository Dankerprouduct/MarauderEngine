using System;
using System.Runtime.InteropServices;
using MarauderEngine.Core;
using MarauderEngine.Graphics;
using MarauderEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarauderEngine.GUI
{
    public class Button: IUIElement
    {
        public event EventHandler<ButtonToggledEventArgs> ButtonClicked;

        public Point Origin { get; set; }
        public Point Size { get; set; }
        public bool Show { get; set; }
        public Color Colour { get; set; }

        private Rectangle _rectangle;

        public Rectangle rectangle
        {
            get => _rectangle;
            set { _rectangle = value; }
        }

        public IUIElement Parent { get; set; }

        public Effect Effect; 
        public bool Enabled { get; set; }
        public bool Hover { get; set; }

        private Texture2D texture; 

        public bool Toggleable { get; set; }

        public Button(Point origin, Point size)
        {
            SetProperties(origin, size, false);
            Show = true;
            Colour = UIPalette.ButtonBackground;
        }

        public Button(IUIElement parent, Point origin, Point size) : this(origin, size)
        {
            Parent = parent;
        }

        public Button(Point origin, Point size, Color color) : this(origin, size)
        {
            Colour = color; 

        }

        public Button(IUIElement parent, Point origin, Point size, Color color) : this(parent, origin, size)
        {
            Colour = color; 
        }

        public void AddPosition(Point origin)
        {
            Origin += origin;
        }

        public void SetPosition(Point position)
        {
            rectangle = new Rectangle(position, Size);
        }

        /// <summary>
        /// Toggles the Button
        /// </summary>
        public void Toggle()
        {
            if (Toggleable)
            {
                Enabled = !Enabled;
                ButtonToggledEventArgs arg = new ButtonToggledEventArgs();
                arg.enabled = Enabled;
                ButtonClicked?.Invoke(this, arg);
            }
            else
            {
                Enabled = !Enabled;
                ButtonToggledEventArgs arg = new ButtonToggledEventArgs();
                arg.enabled = Enabled;
                ButtonClicked?.Invoke(this, arg);
                Enabled = !Enabled;
            }
        }

        public bool IsEnabled()
        {
            return Enabled; 
        }
        
        public virtual void Update()
        {
            if (Parent != null)
            {
                // do not touch
                _rectangle = new Rectangle(Origin + new Point(Parent.rectangle.X, Parent.rectangle.Y), Size);

            }
            else
            {
                _rectangle = new Rectangle(Origin, Size);
            }

            if (Show)
            {
                if (_rectangle.Contains(UIManager.Instance.MousePosition))
                {
                    Hover = true;

                    if (UIManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed &&
                        UIManager.Instance.PrevopisMouseState.LeftButton == ButtonState.Released)
                    {
                        Toggle();
                    }
                }
                else
                {
                    Hover = false;
                }
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            

            if (!Enabled)
            {
                if (Show)
                {
                    if (texture != null)
                    {
                        if (Effect == null)
                        {
                            GUI.DrawTexture(texture,
                                new Vector2(_rectangle.X, _rectangle.Y),
                                Size.X, Size.Y);
                        }
                        else
                        {
                            GUI.DrawTexture(texture, Effect,
                                new Vector2(_rectangle.X, _rectangle.Y),
                                Size.X, Size.Y);
                        }
                    }
                    else
                    {
                        GUI.DrawBox(_rectangle, Colour);
                    }
                }
            }
            else
            {
                if (Show)
                {
                    if (texture != null)
                    {
                        if (Effect == null)
                        {
                            GUI.DrawTexture(texture,
                                new Vector2(_rectangle.X, _rectangle.Y),
                                Size.X, Size.Y);
                        }
                        else
                        {
                            GUI.DrawTexture(texture, Effect,
                                new Vector2(_rectangle.X, _rectangle.Y),
                                Size.X, Size.Y);
                        }

                        GUI.DrawBox(_rectangle, Color.Red * .5f);
                    }
                    else
                    {
                        GUI.DrawBox(_rectangle, Color.Red);
                    }

                }
            }

            if (_rectangle.Contains(UIManager.Instance.MousePosition))
            {
                if (UIManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    GUI.DrawBox(_rectangle, Color.Red * .5f);
                }
            }

        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void SetProperties(Point origin, Point size, bool enabled)
        {
            Enabled = enabled;
            Origin = origin;
            Size = size;
            Toggleable = true;
            _rectangle = new Rectangle(origin.X, origin.Y, size.X, size.Y);
           
        }
    }
    public class ButtonToggledEventArgs : EventArgs
    {
        public bool enabled { get; set; }
    }
}
