using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.Components;
using Color = Microsoft.Xna.Framework.Color;
using Matrix = Microsoft.Xna.Framework.Matrix;
using Point = System.Drawing.Point;
using SpaceMarauders;
using SpaceMarauders.Entity;
using SpaceMarauders.Graphics;
using SpaceMarauders.Utilities;
using SpaceMarauders.GUI;


namespace TileEditor
{
    public class Editor: MonoGameControl
    {
        private List<Bitmap> tileBitmaps = new List<Bitmap>();
        private List<Bitmap> decorationBitmaps = new List<Bitmap>();
        public int currentSelectedItem = 0;
        public MouseState currentMouseState;
        public MouseState previousMouseState;
        private KeyboardState curKeyboardState;
        private KeyboardState prevKeyboardState;
        private Vector2 mousePosiiton;

        public bool mapCreated = false;
        private Room room; 
        private List<Texture2D> tileTextures;
        private List<Texture2D> decorationTextures;
        Camera2D camera = new Camera2D();
        private float scale = 1;

        int Mode = 1;

        public Form1 ReferenceForm1; 

        protected override void Initialize()
        {

            base.Initialize();
            room = new Room();
            LoadBitmaps();
            
            
        }

        public void LoadBitmaps()
        {
            tileBitmaps = new List<Bitmap>();
            TextureManager.LoadContent(Editor.Content);
            tileTextures = TextureManager.Tiles;
            decorationTextures = TextureManager.Decorations; 

            for (int i = 0; i < tileTextures.Count; i++)
            {
                MemoryStream memoryStream = new MemoryStream();
                tileTextures[i].SaveAsPng(memoryStream, tileTextures[i].Width, tileTextures[i].Height);

                tileBitmaps.Add( ResizeImage(new Bitmap(memoryStream),
                    tileTextures[i].Width * 1,
                    tileTextures[i].Height * 1));
                Console.WriteLine("added bitmap " + i);
            }

            for (int i = 0; i < decorationTextures.Count; i++)
            {
                MemoryStream memoryStream = new MemoryStream();
                decorationTextures[i].SaveAsPng(memoryStream, decorationTextures[i].Width, decorationTextures[i].Height);

                decorationBitmaps.Add(ResizeImage(new Bitmap(memoryStream),
                    decorationTextures[i].Width * 1,
                    decorationTextures[i].Height * 1));
                Console.WriteLine("added bitmap " + i);
            }


        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public List<Bitmap> GetBitmaps()
        {
            switch (Mode)
            {
                case 1:
                    {
                        return tileBitmaps;
                    }
                case 2:
                    {
                        return decorationBitmaps;
                    }
            }
            return null; 
        }

        public void CreateMap(int width, int height)
        {
            room.tileMap = new int[width, height];
            for (int y = 0; y < room.tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < room.tileMap.GetLength(0); x++)
                {
                    room.tileMap[x, y] = -1; 
                }
            }
            mapCreated = true; 
        }
        
        protected override void Update(GameTime gameTime)
        {

            currentMouseState = Mouse.GetState();
            curKeyboardState = Keyboard.GetState();

            mousePosiiton = Vector2.Transform(currentMouseState.Position.ToVector2(),
                Matrix.Invert(camera.Transform));

            if (mapCreated)
            {
                SwitchModes(); 
                UpdateMapLogic();
                Camera();
            }
            

            previousMouseState = currentMouseState;
            prevKeyboardState = curKeyboardState;

        }

        public void Save(string name)
        {
            GameData<Room> roomSaveData = new GameData<Room>();
            roomSaveData.folderPath = @"Saves\Rooms\";
            roomSaveData.SaveData(room, name);
            Console.WriteLine("saved "+ name);
            
        }

        void Camera()
        {
            if (currentMouseState.RightButton == ButtonState.Pressed)
            {
                if (curKeyboardState.IsKeyUp(Keys.Space))
                {
                    if (Mouse.GetState().Position.Y < 50)
                    {
                        camera.Move(new Vector2(0, -5));
                    }

                    if (Mouse.GetState().Position.Y > GraphicsDevice.Viewport.Height - 50)
                    {
                        camera.Move(new Vector2(0, 5));
                    }

                    if (Mouse.GetState().Position.X < 50)
                    {
                        camera.Move(new Vector2(-5, 0));
                    }

                    if (Mouse.GetState().Position.X > GraphicsDevice.Viewport.Width - 50)
                    {
                        camera.Move(new Vector2(5, 0));
                    }
                }
            }


            if (currentMouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue)
            {
                scale += .05f;
            }
            if (currentMouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue)
            {
                scale -= .05f;
            }
            // Tree tree = new Tree(Vector2.Zero, 0, 0);

            if (scale > 4)
            {
                scale = 4;
            }
            if (scale <= .01f)
            {
                scale = .01f;
            }

            camera.GetZoom = scale;
        }

        void SwitchModes()
        {
            if (curKeyboardState.IsKeyDown(Keys.D1) && prevKeyboardState.IsKeyUp(Keys.D1))
            {
                currentSelectedItem = 0;
                Mode = 1;
                ReferenceForm1.PopulateListView();
            }
            if (curKeyboardState.IsKeyDown(Keys.D2) && prevKeyboardState.IsKeyUp(Keys.D2))
            {
                currentSelectedItem = 0; 
                Mode = 2;
                ReferenceForm1.PopulateListView();
            }
            
        }

        void FloodFill(int x, int y, int fill, int old)
        {
            
            if ((x < 0) || (x >= room.tileMap.GetLength(0))) return;
            if ((y < 0) || (y >= room.tileMap.GetLength(1))) return;
            //Console.WriteLine(x + " " + y + " " + old);
            if (room.tileMap[x,y] == old)
            {
                room.tileMap[x, y] = fill; 
                FloodFill(x + 1, y, fill, old);
                FloodFill(x, y + 1, fill, old);
                FloodFill(x - 1, y, fill, old);
                FloodFill(x, y - 1, fill, old);
            }

        }

        void UpdateMapLogic()
        {
            if (curKeyboardState.IsKeyDown(Keys.B) && prevKeyboardState.IsKeyUp(Keys.B))
            {
                int x = (int)mousePosiiton.X / 128;
                int y = (int)mousePosiiton.Y / 128;

                FloodFill(x, y, currentSelectedItem, room.tileMap[x, y]);
            }
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                int x = (int)mousePosiiton.X / 128;
                int y = (int)mousePosiiton.Y / 128;

                // picker tool
                if (curKeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    currentSelectedItem = room.tileMap[x, y]; 
                }
                else
                {
                    try
                    {
                        switch (Mode)
                        {
                            case 1:
                                {

                                    room.tileMap[x, y] = currentSelectedItem;
                                    
                                    break;
                                }
                            case 2:
                                {
                                    if (previousMouseState.LeftButton == ButtonState.Released)
                                    {
                                        Decoration decoration = new Decoration();
                                        decoration.DecorationID = currentSelectedItem;
                                        decoration.Position = mousePosiiton;

                                        if (decorationTextures.Count > 0)
                                        {
                                            if (room.Decorations != null)
                                            {

                                                room.Decorations.Add(decoration);
                                            }
                                            else
                                            {
                                                room.Decorations = new List<Decoration>();
                                                room.Decorations.Add(decoration);
                                            }
                                        }
                                    }
                                    break;

                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OUT OF BOUNDS");
                    }
                }
                

                
            }

            if (currentMouseState.RightButton == ButtonState.Pressed)
            {
                switch (Mode)
                {
                    case 1:
                        {
                            if (curKeyboardState.IsKeyDown(Keys.Space))
                            {
                                int x = (int)mousePosiiton.X / 128;
                                int y = (int)mousePosiiton.Y / 128;

                                try
                                {

                                    room.tileMap[x, y] = -1;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"OUT OF BOUNDS {e.ToString()}");
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            if (curKeyboardState.IsKeyDown(Keys.Space))
                            {
                                int x = (int)mousePosiiton.X;
                                int y = (int)mousePosiiton.Y;

                                for(int i = 0; i < room.Decorations.Count; i++)
                                {
                                    
                                    if (room.Decorations[i].CollisionRectangle.Contains( new Microsoft.Xna.Framework.Point(x, y)))
                                    {
                                        room.Decorations.RemoveAt(i); 
                                    }
                                }
                            }

                            break;
                        }
                }

                
            }
        }

        protected override void Draw()
        {
            base.Draw();
            Editor.spriteBatch.GraphicsDevice.Clear(Color.Black); 
            Editor.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null,
                null, null, camera.GetTransformation(GraphicsDevice));
            GUI.Draw(Editor.spriteBatch);


            ///GUI.Draw2dArray(10, 10, 128, 128, tileMap.GetLength(0), tileMap.GetLength(1), 0, Color.AliceBlue);

            if (room.tileMap != null)
            {
                for (int y = -1; y < room.tileMap.GetLength(1); y++)
                {
                    for (int x = -1; x < room.tileMap.GetLength(0); x++)
                    {

                        GUI.DrawBox(
                            new Microsoft.Xna.Framework.Rectangle((x * 128) + 128, (y * 128) + 128, 128, 128), 3,
                            Color.DarkGray);

                    }
                }
                for (int y = 0; y < room.tileMap.GetLength(1); y++)
                {
                    for (int x = 0; x < room.tileMap.GetLength(0); x++)
                    {
                        if (room.tileMap[x, y] != -1)
                        {
                            GUI.spriteBatch.Draw(tileTextures[room.tileMap[x, y]], new Vector2(x * 128, y * 128),
                                Color.White);
                        }
                    }
                }

                if (room.Decorations != null)
                {
                    for (int i = 0; i < room.Decorations.Count; i++)
                    {
                        
                        GUI.spriteBatch.Draw(decorationTextures[room.Decorations[i].DecorationID], room.Decorations[i].Position,
                                    Color.White);
                    }
                }

                switch (Mode)
                {

                    case 1:
                        {
                            if (currentSelectedItem != -1)
                            {
                                Editor.spriteBatch.Draw(tileTextures[currentSelectedItem],
                                    new Vector2(((int)(mousePosiiton.X / 128)) * 128, ((int)(mousePosiiton.Y / 128)) * 128),
                                    Color.Red * .5f);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (currentSelectedItem != -1)
                            {
                                if (decorationTextures.Count > 0)
                                {
                                    Editor.spriteBatch.Draw(decorationTextures[currentSelectedItem],
                                        new Vector2(((int)(mousePosiiton.X / 32)) * 32, ((int)(mousePosiiton.Y / 32)) * 32),
                                        Color.Red * .5f);
                                }
                            }
                            break;
                        }
                }
            }
            
            Editor.spriteBatch.End();
            
        }
    }
}
