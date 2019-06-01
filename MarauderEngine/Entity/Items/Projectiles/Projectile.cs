using Microsoft.Xna.Framework;

namespace MarauderEngine.Entity.Items.Projectiles
{
    public abstract class Projectile : Item
    {        
        public Vector2 direction;
        
        /// <summary>
        /// Once the projectile has either hit the end of the raypoint or the speed has slowed down to a standstill
        /// </summary>
        public abstract void OnHit();
        
    }
}
