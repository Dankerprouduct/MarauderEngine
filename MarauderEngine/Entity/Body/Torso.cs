using MarauderEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Entity.Body
{
    public class Torso: BodyPart
    {

        float leftBounds;
        float rightBounds;
        float oldRotation;
        float newRoation;


        public Torso(int textureID, Vector2 offset) : base(textureID, offset)
        {

        }
   
        public override void Update(Vector2 center, float rotation)
        {
            bounds = TextureManager.BodyParts[textureID].Bounds;

            // if the rotation moves out of predefined boudns then rotate -> reset boudns
            if (rotation <= leftBounds)
            {
                ResetBounds(rotation);
                oldRotation = rotation;
                
            }
            else if (rotation >= rightBounds)
            {
                ResetBounds(rotation);
                oldRotation = rotation;
            }

            newRoation = Utilities.MathHelper.CurveAngle(newRoation, oldRotation, lerpSpeed);
            base.Update(center, newRoation);
        }

        
        void ResetBounds(float rotation)
        {
            leftBounds = rotation - MathHelper.ToRadians(turnAngle);
            rightBounds = rotation + MathHelper.ToRadians(turnAngle);

        }
             
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
