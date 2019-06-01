using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Entity.Body
{
    public class Hand: BodyPart
    {

        float leftBounds;
        float rightBounds;
        float oldRotation;
        float newRoation;

        public Hand(int textureID, Vector2 offset) : base(textureID, offset)
        {

        }

        public override void Update(Vector2 center, float rotation)
        {
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
            
            positon = center;
            this.rotation = newRoation;
            currentOffset = Vector2.Lerp(currentOffset, offset, .3f); 
            positon = Vec2ToEntitySpace(currentOffset);
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
