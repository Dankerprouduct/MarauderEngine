using MarauderEngine.Components;

namespace MarauderEngine.Graphics
{
    public interface IDrawable
    {
        /// <summary>
        /// The layer at which the sprite is drawn
        /// </summary>
        int Layer
        {
            get;
            set;
        }

        string TextureListName
        {
            get;
            set;
        }

        /// <summary>
        /// The ID of the texture
        /// </summary>
        int TextureId
        {
            get;
            set;
        }

        /// <summary>
        /// Transform component tells GraphicsManger where to draw
        /// </summary>
        TransformComponent TransformComponent
        {
            get;
            set; 
        }


        IDrawable Register(IDrawable drawable);

    }
}
