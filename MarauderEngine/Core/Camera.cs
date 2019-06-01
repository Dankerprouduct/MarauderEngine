using MarauderEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Steamworks;

namespace MarauderEngine.Core
{
    public class Camera : IUpdateableSystem
    {
        public static Camera Instance;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        MouseState mouseState;
        MouseState oldMouseState;

        public Matrix transform;

        Viewport viewPort;
        public Vector2 center;
        public Vector2 lerpedCenter;
        float scale = 1f;
        private float _minScale = .0001f;
        private float _maxScale = 4;

        public bool CamControl = true;
        float speed = 60;
        private float _zoomSpeed = .0005f;
        public static Vector2 position;
        private MarauderEngine.Entity.Entity focusedEntity;

        private int _topLeft;
        private int _topRight;
        private int _bottomLeft;
        private int _bottomRight;

        public bool Scrolling { get; private set; }

        public Camera(Viewport vPort)
        {
            Instance = this;
            position = new Vector2(0, 0);
            viewPort = vPort;
            Scrolling = true;
        }

        [System.Obsolete("Use Update() instead", true)]
        public void Update(GameTime gameTime)
        {

        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            UpdateCameraBoundaries();

            if (Scrolling)
            {
                if (mouseState.ScrollWheelValue > oldMouseState.ScrollWheelValue)
                {
                    if (!CamControl)
                    {
                        scale += .025f;
                    }
                    else
                    {
                        scale += _zoomSpeed;
                    }
                }

                if (mouseState.ScrollWheelValue < oldMouseState.ScrollWheelValue)
                {
                    if (!CamControl)
                    {
                        scale -= .025f;
                    }
                    else
                    {
                        scale -= _zoomSpeed;
                    }
                }
            }
            // Tree tree = new Tree(Vector2.Zero, 0, 0);

            if (scale > _maxScale)
            {
                scale = _maxScale;
            }
            if (scale <= _minScale)
            {
                scale = _minScale;
            }




            if (keyboardState.IsKeyDown(Keys.F1) && oldKeyboardState.IsKeyUp(Keys.F1) && focusedEntity != null)
            {
                CamControl = !CamControl;
            }

            if (CamControl)
            {
                center = position;
                lerpedCenter = Vector2.Lerp(lerpedCenter, center, .5f);
                keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    position.Y -= speed;
                }
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    position.Y += speed;
                }
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position.X += speed;
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    position.X -= speed;
                }
                if (keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    speed = 240;
                }
                else
                {
                    speed = 60;
                }
            }
            else
            {
                if (focusedEntity == null)
                {
                    //center = Game1.player.GetCenter();
                    center = position;
                }
                else
                {
                    center = focusedEntity.GetEntityPosition();
                    position = center;
                    lerpedCenter = Vector2.Lerp(lerpedCenter, center, .1f);

                }

            }


            transform = Matrix.CreateTranslation(new Vector3(-lerpedCenter.X, -lerpedCenter.Y, 0)) *
                Matrix.CreateScale(new Vector3(scale, scale, 1)) *
                Matrix.CreateTranslation(new Vector3(viewPort.Width * 0.5f, viewPort.Height * 0.5f, 0));

            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;
        }

        public Vector2 TopLeftPosition { get; private set; }
        public Vector2 TopRightPosition { get; private set; }
        public Vector2 BottomLeftPosition { get; private set; }
        public Vector2 BottomRightPosition { get; private set; }
        private void UpdateCameraBoundaries()
        {
            TopLeftPosition = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(transform));
            TopRightPosition =
               Vector2.Transform(new Vector2(viewPort.Width, 0), Matrix.Invert(transform));
            BottomLeftPosition = Vector2.Transform(new Vector2(0, viewPort.Height), Matrix.Invert(transform));
            BottomRightPosition =
               Vector2.Transform(new Vector2(viewPort.Width, viewPort.Height), Matrix.Invert(transform));

            _topLeft = CellSpacePartition.GetTopLeftPartition(TopLeftPosition);
            _topRight = CellSpacePartition.GetTopRightPartition(TopRightPosition);
            _bottomLeft = CellSpacePartition.GetBottomLeftPartition(BottomLeftPosition);
            _bottomRight = CellSpacePartition.GetBottomRightPartition(BottomRightPosition);


        }

        public int GetTopLeftCell()
        {
            return _topLeft;
        }

        public int GetTopRightCell()
        {
            return _topRight;
        }

        public int GetBottomLeft()
        {
            return _bottomLeft;
        }

        public int GetBottomRight()
        {
            return _bottomRight;
        }

        /// <summary>
        /// How far the camera zooms in
        /// larger number the more youre able to zoom in
        /// </summary>
        /// <param name="zoom"></param>
        public void SetMaxZoom(float zoom = 4)
        {
            _maxScale = zoom;
        }

        /// <summary>
        /// How far the camera zooms out
        /// smaller number the farther it zooms out
        /// </summary>
        /// <param name="zoom"></param>
        public void SetMinZoom(float zoom = .0001f)
        {
            _minScale = zoom;
        }

        public void SetZoom(float zoom = 1)
        {
            scale = zoom;
        }

        public void SetZoomSpeed(float zoomSpeed = .0005f)
        {
            _zoomSpeed = zoomSpeed;
        }

        public void SetFocus(MarauderEngine.Entity.Entity entity)
        {
            focusedEntity = entity;
        }

        public void SetMoveSpeed(float speed = 60)
        {
            this.speed = speed;
        }

        /// <summary>
        /// enable and disables scroll
        /// </summary>
        /// <param name="canScroll"></param>
        public void SetScroll(bool canScroll)
        {
            Scrolling = canScroll;
        }

        public MarauderEngine.Entity.Entity GetFocus()
        {
            return focusedEntity;
        }


        public Vector2 WindowToCameraSpace(Vector2 windowPosition)
        {
            // Scale for camera bounds that vary from window
            // Also, must adjust for translation if camera isn't at 0, 0 in screen space (such as a mini-map)
            return (scale * windowPosition) + center;
        }

    }
}

