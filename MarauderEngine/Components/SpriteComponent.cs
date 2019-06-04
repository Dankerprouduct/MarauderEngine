using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Core.Render;
using MarauderEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathHelper = MarauderEngine.Utilities.MathHelper;

namespace MarauderEngine.Components
{
    public class SpriteComponent : IComponent, IComparable<SpriteComponent>
    {
        public Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        private string _textureName; 

        public Vector2 TextureCenter { get; set; }
        public float SpriteScale { get; set; }

        public float Layer = 0;

        public float Rotation { get; set; }

        public bool StupidDrawing = false; 

        /// <summary>
        /// the tint of the sprite. Defaults to white
        /// </summary>
        public Color Color;

        /// <summary>
        /// a value between 0 and 1 that fades a color
        /// </summary>
        public float ColorMod;


        public Rectangle Rectangle
        {
            get
            { 
                return new Rectangle(new Point(
                    Owner.GetComponent<TransformComponent>().Position.ToPoint().X - (int)TextureCenter.X,
                    Owner.GetComponent<TransformComponent>().Position.ToPoint().Y - (int)TextureCenter.Y),
                    new Point(
                        TextureManager.GetContent<Texture2D>(_textureName).Width,
                        TextureManager.GetContent<Texture2D>(_textureName).Height));
            }
        }

        /// <summary>
        /// Draws texture to screen given the textures name
        /// Note: The parent of this component must have a TransformComponent attached to it before adding this one
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="textureName"></param>
        public SpriteComponent(Entity.Entity owner, string textureName)
        {
            RegisterComponent(owner, "SpriteComponent");
            _textureName = textureName; 
            SetTextureName(textureName);
            SpriteScale = 1;
            Color = Color.White;
            ColorMod = 1; 
        }


        public void RegisterComponent(Entity.Entity entity, string componentName)
        {
            Owner = entity;
            Name = componentName;
            Active = true; 
            //BatchManager.Instance.AddSpriteComponent(this);
        }

        public bool FireEvent(Event eEvent)
        {
            return false;
        }

        public void UpdateComponent()
        {
            Rotation = Owner.GetComponent<TransformComponent>().Rotation;
        }

        public void Destroy()
        {
            Active = false; 
        }

        /// <summary>
        /// sets the texture name that is used for drawing
        /// </summary>
        /// <param name="textureName"></param>
        public void SetTextureName(string textureName)
        {

            _textureName = textureName;
            TextureCenter = MathHelper.CenterOfImage(TextureManager.GetContent<Texture2D>(textureName)); 
        }

        /// <summary>
        /// returns the texture name used for drawing
        /// </summary>
        /// <returns></returns>
        public string GetTextureName()
        {
            return _textureName; 
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(TextureManager.GetContent<Texture2D>(_textureName), Owner.GetComponent<TransformComponent>().Position, Color.White);
            if (Active)
            {
                if (!StupidDrawing)
                {
                    spriteBatch.Draw(TextureManager.GetContent<Texture2D>(_textureName),
                        Owner.GetComponent<TransformComponent>().Position, null, Color * ColorMod,
                        Rotation, TextureCenter, SpriteScale, SpriteEffects.None,
                        Layer);
                }
                else
                {
                    spriteBatch.Draw(TextureManager.GetContent<Texture2D>(_textureName),
                        Owner.GetComponent<TransformComponent>().Position, Color);
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
