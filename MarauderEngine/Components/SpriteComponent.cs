using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components.Data;
using MarauderEngine.Core.Render;
using MarauderEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathHelper = MarauderEngine.Utilities.MathHelper;

namespace MarauderEngine.Components
{
    public class SpriteComponent : Component<SpriteCData>, IComparable<SpriteComponent>
    {


        public override Type type => GetType();
        public string TextureName
        {
            get => _data.TextureName;
            set => _data.TextureName = value; 
        }

        public Vector2 TextureCenter
        {
            get => _data.TextureCenter;
            set => _data.TextureCenter = value;
        }

        public float SpriteScale
        {
            get => _data.SpriteScale;
            set => _data.SpriteScale = value;
        }

        public float Layer
        {
            get => _data.Layer;
            set => _data.Layer = value;
        }

        public float Rotation
        {
            get => _data.Rotation;
            set => _data.Rotation = value;
        }

        public bool StupidDrawing
        {
            get => _data.StupidDrawing;
            set => _data.StupidDrawing = value;
        }

        /// <summary>
        /// the tint of the sprite. Defaults to white
        /// </summary>
        public Color Color
        {
            get => _data.Color;
            set => _data.Color = value;
        }

        /// <summary>
        /// a value between 0 and 1 that fades a color
        /// </summary>
        public float ColorMod
        {
            get => _data.ColorMod;
            set => _data.ColorMod = value;
        }


        public Rectangle Rectangle
        {
            get
            { 
                return new Rectangle(new Point(
                    Owner.GetComponent<TransformComponent>().Position.ToPoint().X - (int)TextureCenter.X,
                    Owner.GetComponent<TransformComponent>().Position.ToPoint().Y - (int)TextureCenter.Y),
                    new Point(
                        TextureManager.GetContent<Texture2D>(TextureName).Width,
                        TextureManager.GetContent<Texture2D>(TextureName).Height));
            }
            set => throw new NotImplementedException();
        }

        public SpriteComponent() { }
        /// <summary>
        /// Draws texture to screen given the textures name
        /// Note: The parent of this component must have a TransformComponent attached to it before adding this one
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="textureName"></param>
        public SpriteComponent(Entity.Entity owner, string textureName)
        {
            RegisterComponent(owner, "SpriteComponent");
            TextureName = textureName; 
            SetTextureName(textureName);
            SpriteScale = 1;
            Color = Color.White;
            ColorMod = 1; 
        }

        public override bool FireEvent(Event eEvent)
        {
            return false;
        }

        public override void UpdateComponent()
        {
            Rotation = Owner.GetComponent<TransformComponent>().Rotation;
        }

        public override void Destroy()
        {
            Active = false; 
        }

        /// <summary>
        /// sets the texture name that is used for drawing
        /// </summary>
        /// <param name="textureName"></param>
        public void SetTextureName(string textureName)
        {

            TextureName = textureName;
            TextureCenter = MathHelper.CenterOfImage(TextureManager.GetContent<Texture2D>(textureName)); 
        }

        /// <summary>
        /// returns the texture name used for drawing
        /// </summary>
        /// <returns></returns>
        public string GetTextureName()
        {
            return TextureName; 
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(TextureManager.GetContent<Texture2D>(TextureName), Owner.GetComponent<TransformComponent>().Position, Color.White);
            if (Active)
            {
                if (!StupidDrawing)
                {
                    spriteBatch.Draw(TextureManager.GetContent<Texture2D>(TextureName),
                        Owner.GetComponent<TransformComponent>().Position, null, Color * ColorMod,
                        Rotation, TextureCenter, SpriteScale, SpriteEffects.None,
                        Layer);
                }
                else
                {
                    spriteBatch.Draw(TextureManager.GetContent<Texture2D>(TextureName),
                        Owner.GetComponent<TransformComponent>().Position, null, Color * ColorMod,
                        0, new Vector2(0,0), SpriteScale, SpriteEffects.None,
                        Layer);
                }
            }
        }

        public int CompareTo(object obj)
        {
            return (int)(Layer * 10);
        }

        public int CompareTo(SpriteComponent other)
        {

            return this.Rotation.CompareTo(other.Layer);
        }
    }
}
