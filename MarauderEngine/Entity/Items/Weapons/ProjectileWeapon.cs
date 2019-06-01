using MarauderEngine.Physics.Core;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Entity.Items.Weapons
{
    public class ProjectileWeapon: Item
    {

        public int maxAmmo;
        public int currentAmmo; 

        public int clipSize;
        public int currentClip;


        public Raycast raycast; 
        // the point where the ray or projectile fires from
        public Vector2 firePoint; 

        public ProjectileWeapon()
        {
            
        }
        
        public override void Use(MarauderEngine.Entity.Entity entity)
        {
            //Console.WriteLine("fired Weapon");
            
        }

    }
}
