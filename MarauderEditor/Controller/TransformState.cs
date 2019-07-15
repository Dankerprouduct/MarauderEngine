using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEditor.CustomComponents;
using MarauderEngine.Components;
using MarauderEngine.Entity;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MarauderEditor.Controller
{
    public class TransformState: IState
    {

        private static Texture2D _greenArrow, _redArrow, _yellowDot, _blueArrow;
        private Rectangle _greenArrowRect, _redArrowRect, yellowDotRect, _blueArrowRect;
        enum TransformStates
        {
            NotMoving,
            Moving,
            MovingX,
            MovingY,
            Rotate
        }

        private TransformStates _currentState;
        private Entity _selectedEntity;
        private Vector2 _lastMousePosition = Vector2.Zero;

        public static int Snapping = 128;
        public bool SnappingEnabled = false;
        public Vector2 MousePosition;
        public Vector2 MouseDownPosition;

        public TransformState(Entity selectedEntity)
        {
            _selectedEntity = selectedEntity;
        }

        public void Enter()
        {
            _currentState = TransformStates.NotMoving;
        }

        public void Exit()
        {
            
        }

        public IState Update(GameTime gameTime)
        {
            MousePosition = InputManager.Instance.GetMouseWorldPosition();
            SnappingEnabled = MarauderMenuStrip.SnappingToggle.Checked;
            Snapping = int.Parse(MarauderMenuStrip.SnappingAmmount.Text);
            // checks for escape
            if (CheckForExitState(out var state)) return state;

            UpdateTransformPositions();

            PerformTransformOperations();

            CheckForStateChange();

            if (CheckForDeleteEntityState(out var deleteEntityState)) return deleteEntityState;

            _lastMousePosition = MousePosition;
            return null;
        }

        private bool CheckForDeleteEntityState(out IState deleteEntityState)
        {
            if (InputManager.Instance.KeyPressed(Keys.Delete))
            {
                {
                    deleteEntityState = new DeleteEntityState(_selectedEntity);
                    return true;
                }
            }

            deleteEntityState = null;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_selectedEntity != null )
            {
                var position = _selectedEntity.GetComponent<TransformComponent>().Position +
                               _selectedEntity.GetComponent<SpriteComponent>().TextureCenter;
                spriteBatch.Draw(_yellowDot, yellowDotRect, Color.White);
                spriteBatch.Draw(_redArrow, _redArrowRect, Color.White);
                spriteBatch.Draw(_greenArrow, _greenArrowRect, Color.White);
                spriteBatch.Draw(_blueArrow, _blueArrowRect, Color.White);
            }
        }

        private void CheckForStateChange()
        {
            if (_currentState == TransformStates.NotMoving)
            {
                if (_redArrowRect.Contains(_lastMousePosition.ToPoint()))
                {
                    if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        _currentState = TransformStates.MovingX;
                        MouseDownPosition = MousePosition;
                    }
                }

                if (_greenArrowRect.Contains(_lastMousePosition.ToPoint()))
                {
                    if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        _currentState = TransformStates.MovingY;
                    }
                }

                if (yellowDotRect.Contains(_lastMousePosition.ToPoint()))
                {
                    if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        _currentState = TransformStates.Moving;
                    }
                }

                if (_blueArrowRect.Contains((_lastMousePosition.ToPoint())))
                {
                    
                    if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        _currentState = TransformStates.Rotate;
                    }
                }
            }

            if (_currentState == TransformStates.MovingX || _currentState == TransformStates.MovingY || _currentState == TransformStates.Moving
                || _currentState == TransformStates.Rotate)
            {
                if (InputManager.Instance.CurrentMouseState.LeftButton == ButtonState.Released &&
                    InputManager.Instance.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    _currentState = TransformStates.NotMoving;
                }

            }
        }

        private void PerformTransformOperations()
        {
            if (_currentState == TransformStates.MovingX)
            {
                MoveX();
            }

            if (_currentState == TransformStates.MovingY)
            {
                MoveY();
            }

            if (_currentState == TransformStates.Moving)
            {
                MoveXY();
            }

            if (_currentState == TransformStates.Rotate)
            {
                Rotate();
            }

            
        }

        private void Rotate()
        {
            float dif = (InputManager.Instance.GetMouseWorldPosition() - _lastMousePosition).X;
            var transform = _selectedEntity.GetComponent<TransformComponent>();

            transform.Rotation += MathHelper.ToRadians(dif);
            if (SnappingEnabled)
            {
                transform.Rotation =MathHelper.ToRadians((float)(Math.Floor((MousePosition.X - MouseDownPosition.X) / 30) * 30));
            }
        }

        private void MoveX()
        {
            float xDif = (MousePosition.X - _lastMousePosition.X);
            var transform = _selectedEntity.GetComponent<TransformComponent>();
            if (SnappingEnabled)
            {
                transform.Position = new Vector2((float) Math.Floor(MousePosition.X / Snapping) * Snapping,
                    transform.Position.Y);
            }
            else
            {
                transform.Position = new Vector2(transform.Position.X + xDif,
                    transform.Position.Y);
            }
        }

        private void MoveY()
        {
            float yDif = InputManager.Instance.GetMouseWorldPosition().Y - _lastMousePosition.Y;
            var transform = _selectedEntity.GetComponent<TransformComponent>();
            if (SnappingEnabled)
            {
                transform.Position = new Vector2(transform.Position.X, (float)Math.Floor(MousePosition.Y / Snapping) * Snapping);
            }
            else
            {
                transform.Position = new Vector2(transform.Position.X, yDif + transform.Position.Y);
            }
        }

        private void MoveXY()
        {
            float xDif = InputManager.Instance.GetMouseWorldPosition().X - _lastMousePosition.X;
            float yDif = InputManager.Instance.GetMouseWorldPosition().Y - _lastMousePosition.Y;
            var transform = _selectedEntity.GetComponent<TransformComponent>();

            if (SnappingEnabled)
            {
                transform.Position = new Vector2(
                    (float)Math.Floor(MousePosition.X / Snapping) * Snapping,
                    (float)Math.Floor(MousePosition.Y / Snapping) * Snapping);
            }
            else
            {
                transform.Position = new Vector2(xDif + transform.Position.X, transform.Position.Y + yDif);
            }

            
        }

        private void UpdateTransformPositions()
        {
            if (_selectedEntity != null)
            {
                var position = _selectedEntity.GetComponent<TransformComponent>().Position +
                               _selectedEntity.GetComponent<SpriteComponent>().TextureCenter;

                var yellowPosition = position - new Vector2((_yellowDot.Width / 2), (_yellowDot.Height / 2));
                yellowDotRect = new Rectangle((int) yellowPosition.X, (int) yellowPosition.Y, _yellowDot.Width,
                    _yellowDot.Height);

                var greenPosition = position - new Vector2((_greenArrow.Width / 2), (_greenArrow.Height / 2)) -
                                    new Vector2(0, (_greenArrow.Height / 2) + (_yellowDot.Height / 2) + 10);
                _greenArrowRect = new Rectangle((int) greenPosition.X, (int) greenPosition.Y, _greenArrow.Width,
                    _greenArrow.Height);


                var redPosition = position - new Vector2((_redArrow.Width / 2), (_redArrow.Height / 2)) +
                                  new Vector2((_redArrow.Width / 2) + (_yellowDot.Width / 2) + 10, 0);
                _redArrowRect = new Rectangle((int) redPosition.X, (int) redPosition.Y, _redArrow.Width, _redArrow.Height);

                var bluePosition = position - new Vector2((_blueArrow.Width / 2), (_blueArrow.Height / 2)) +
                                   new Vector2(_blueArrow.Width / 1.5f, (-_blueArrow.Height / 1.5f));
                _blueArrowRect = new Rectangle((int) bluePosition.X, (int) bluePosition.Y, _blueArrow.Width, _blueArrow.Height);
            }
        }

        private static bool CheckForExitState(out IState state)
        {
            if (InputManager.Instance.MouseRightClicked())
            {
                {
                    state = new IdleState();

                    return true;
                }
            }

            state = null;
            return false;
        }

        public static void LoadContent(ContentManager content)
        {
            _greenArrow = content.Load<Texture2D>("greenarrow");
            _redArrow = content.Load<Texture2D>("redarrow");
            _yellowDot = content.Load<Texture2D>("yellowbox");
            _blueArrow = content.Load<Texture2D>("bluearrow");
        }
    }
}
