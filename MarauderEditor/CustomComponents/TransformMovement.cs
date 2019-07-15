using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using MarauderEngine.Components;
using MarauderEngine.Core;
using MarauderEngine.Entity;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using IComponent = MarauderEngine.Components.IComponent;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MarauderEditor.CustomComponents
{
    public class TransformMovement
    {
        private Texture2D greenArrow, redArrow, yellowDot, blueArrow;
        private Rectangle greenArrowRect, redArrowRect, yellowDotRect, blueArrowRect;
        private Entity selectedEntity;
        public bool EntitySelected;

        public static int Snapping = 64;
        public Vector2 MousePosition;
        public EntityViewItem EntityBrush;
        public static TransformMovement Instance;
        public TransformMovement()
        {
            Instance = this;
        }

        public void LoadContent(ContentManager content)
        {
            greenArrow = content.Load<Texture2D>("greenarrow");
            redArrow = content.Load<Texture2D>("redarrow");
            yellowDot = content.Load<Texture2D>("yellowbox");
            blueArrow = content.Load<Texture2D>("bluearrow");
        }

        public void FocusEntity(Entity entity = null)
        {
            selectedEntity = entity;
        }

        private bool mouseMoved;
        private Vector2 lastMousePosition = Vector2.Zero;
        private Matrix oldTransform;
        enum TransformState
        {
            NotMoving,
            Moving,
            MovingX,
            MovingY,
            Rotate
        }

        private TransformState _currentState;

        public void SetBrush(EntityViewItem entity)
        {
            EntityBrush = entity; 
        }

        public void Update(GameTime gameTime, Scene loadedScene)
        {
            if (selectedEntity != null)
            {
                var position = selectedEntity.GetComponent<TransformComponent>().Position +
                               selectedEntity.GetComponent<SpriteComponent>().TextureCenter;
                
                var yellowPosition = position - new Vector2((yellowDot.Width / 2), (yellowDot.Height / 2));
                yellowDotRect = new Rectangle((int)yellowPosition.X, (int)yellowPosition.Y, yellowDot.Width, yellowDot.Height);

                var greenPosition = position - new Vector2((greenArrow.Width / 2), (greenArrow.Height / 2)) -
                                    new Vector2(0, (greenArrow.Height / 2) + (yellowDot.Height / 2) + 10);
                greenArrowRect = new Rectangle((int)greenPosition.X, (int)greenPosition.Y, greenArrow.Width, greenArrow.Height);


                var redPosition = position - new Vector2((redArrow.Width / 2), (redArrow.Height / 2)) +
                                  new Vector2((redArrow.Width / 2) + (yellowDot.Width / 2) + 10, 0);
                redArrowRect = new Rectangle((int)redPosition.X, (int)redPosition.Y, redArrow.Width, redArrow.Height);

                var bluePosition = position - new Vector2((blueArrow.Width / 2), (blueArrow.Height / 2)) +
                                   new Vector2(blueArrow.Width / 1.5f, (-blueArrow.Height / 1.5f));
                blueArrowRect = new Rectangle((int)bluePosition.X, (int)bluePosition.Y, blueArrow.Width, blueArrow.Height);


            }

            if (InputManager.Instance.CurrentMouseState.MiddleButton == ButtonState.Pressed &&
                InputManager.Instance.PreviousMouseState.MiddleButton == ButtonState.Released)
            {
                if (EntityBrush != null)
                {
                    var prefab = Activator.CreateInstance(EntityBrush.EntityType) as Entity;

                    
                    foreach (var f in EntityBrush.EntityType.GetFields().Where(f => f.IsPublic))
                    {
                        var component = Activator.CreateInstance(f.FieldType) as IComponent;

                        if (component != null)
                        {
                            //Components.Add(component);
                            prefab.ForceAddComponent(component);
                            component.RegisterComponent(prefab, f.Name);
                            component.Owner = prefab;
                            Console.WriteLine($"Name:{f.Name}");
                        }
                        
                    }

                    prefab.GetComponent<TransformComponent>().Position = InputManager.Instance.GetMouseWorldPosition();
                    Console.WriteLine("dsa "+ prefab.Components.Count);
                    if (EntityBrush.Entity.GetComponent<SpriteComponent>().TextureName != null)
                    {
                        prefab.GetComponent<SpriteComponent>()
                            .SetTextureName(EntityBrush.Entity.GetComponent<SpriteComponent>().TextureName);
                    }

                    if (prefab.GetComponent<SpriteComponent>().SpriteScale == 0)
                    {
                        prefab.GetComponent<SpriteComponent>().SpriteScale = 1; 
                    }
                    if (prefab.GetComponent<SpriteComponent>().ColorMod == 0)
                    {
                        prefab.GetComponent<SpriteComponent>().ColorMod = 1;
                    }
                    if (prefab.GetComponent<SpriteComponent>().Color == Color.Transparent)
                    {
                        prefab.GetComponent<SpriteComponent>().Color = Color.White;
                    }

                    if (prefab.GetComponent<SpriteComponent>().TextureName ==null)
                    {
                        prefab.GetComponent<SpriteComponent>().TextureName = "texturedtile1";
                        prefab.GetComponent<SpriteComponent>().SetTextureName("texturedtile1");

                        Console.WriteLine("reset texture");
                    }

                    Game1.Instance.AddEntity(prefab);
                    Console.WriteLine("added entity");
                    EntityProperties.Instance.LoadEntity(prefab.EntityData);
                    FocusEntity(prefab);
                    EntitySelected = true;
                }
            }

            if (InputManager.Instance.CurrentMouseState.Position != InputManager.Instance.PreviousMouseState.Position)
            {
                mouseMoved = true;
            }
            else
            {
                mouseMoved = false;
            }
            mouseMoved = true;
            if (EntitySelected)
            {
                if (InputManager.Instance.KeyPressed(Keys.Delete))
                {
                    selectedEntity.DestroyEntity();
                    selectedEntity = null;
                    EntitySelected = false;
                    _currentState = TransformState.NotMoving;
                }
                if (_currentState == TransformState.MovingX)
                {
                    MoveX();
                }
                if (_currentState == TransformState.MovingY)
                {
                    MoveY();
                }
                if (_currentState == TransformState.Moving)
                {
                    MoveXY();
                }
                if (_currentState == TransformState.Rotate)
                {
                    Roate();
                }
                if (_currentState == TransformState.NotMoving)
                {
                    if (redArrowRect.Contains(lastMousePosition.ToPoint()))
                    {
                        if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            _currentState = TransformState.MovingX;
                            
                        }
                    }

                    if (greenArrowRect.Contains(lastMousePosition.ToPoint()))
                    {
                        if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            _currentState = TransformState.MovingY;
                            
                        }
                    }

                    if (yellowDotRect.Contains(lastMousePosition.ToPoint()))
                    {
                        if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            _currentState = TransformState.Moving;

                        }
                    }

                    if (blueArrowRect.Contains((lastMousePosition.ToPoint())))
                    {
                        if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            _currentState = TransformState.Rotate;

                        }
                    }
                }
                else if(_currentState == TransformState.MovingX || _currentState == TransformState.MovingY || _currentState == TransformState.Moving 
                        || _currentState == TransformState.Rotate)
                {
                    if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Released &&
                        InputManager.Instance.PreviousMouseState.LeftButton == ButtonState.Released)
                    {
                        _currentState = TransformState.NotMoving;
                    }

                }

            }


            if (InputManager.Instance.MouseLeftClicked() && !EntitySelected)
            {
                var entity =
                    loadedScene?.CellSpacePartition.GetEntity(InputManager.Instance.GetMouseWorldPosition().ToPoint());
                if (entity != null)
                {
                    FocusEntity(entity);
                    EntityProperties.Instance.LoadEntity(entity.EntityData);
                    EntitySelected = true;
                }
            }
            if (InputManager.Instance.MouseRightClicked())
            {
                FocusEntity();
                _currentState = TransformState.NotMoving;
                EntitySelected = false;
            }


            lastMousePosition = InputManager.Instance.GetMouseWorldPosition();
            oldTransform = Camera.Instance.transform;
        }

        private void Roate()
        {
            float dif = (InputManager.Instance.GetMouseWorldPosition() - lastMousePosition).X;
            var transform = selectedEntity.GetComponent<TransformComponent>();
            transform.Rotation += MathHelper.ToRadians(dif);
        }

        private void MoveX()
        {

            float xDif = InputManager.Instance.GetMouseWorldPosition().X - lastMousePosition.X;
            
            var transform = selectedEntity.GetComponent<TransformComponent>();
            if (mouseMoved)
            {
                transform.Position = new Vector2((xDif + transform.Position.X) % Snapping, transform.Position.Y);
            }
        }

        private void MoveY()
        {
            float yDif = InputManager.Instance.GetMouseWorldPosition().Y - lastMousePosition.Y;
            var transform = selectedEntity.GetComponent<TransformComponent>();
            if (mouseMoved)
            {
                transform.Position = new Vector2(transform.Position.X, yDif + transform.Position.Y);
            }
        }

        private void MoveXY()
        {
            float xDif = InputManager.Instance.GetMouseWorldPosition().X - lastMousePosition.X;
            float yDif = InputManager.Instance.GetMouseWorldPosition().Y - lastMousePosition.Y;
            var transform = selectedEntity.GetComponent<TransformComponent>();
            if (mouseMoved)
            {
                transform.Position = new Vector2(xDif + transform.Position.X, transform.Position.Y + yDif);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (selectedEntity != null && EntitySelected)
            {
                var position = selectedEntity.GetComponent<TransformComponent>().Position +
                               selectedEntity.GetComponent<SpriteComponent>().TextureCenter;
                spriteBatch.Draw(yellowDot, yellowDotRect, Color.White);
                spriteBatch.Draw(redArrow, redArrowRect, Color.White);
                spriteBatch.Draw(greenArrow, greenArrowRect, Color.White);
                spriteBatch.Draw(blueArrow, blueArrowRect, Color.White);
            }
        }
    }
}
