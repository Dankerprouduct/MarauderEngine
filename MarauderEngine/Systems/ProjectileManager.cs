using System;
using System.Collections.Generic;
using MarauderEngine.Components;
using MarauderEngine.Core;
using MarauderEngine.Entity.Items.Projectiles;
using MarauderEngine.Graphics;
using MarauderEngine.Physics.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Systems
{
    [Obsolete("remnants of the game Space Marauders ... God bless her soul.")]
    public static class ProjectileManager
    {

        public static List<Projectile> projectiles = new List<Projectile>();

        /// <summary>
        /// adds a new projectile to the world
        /// </summary>
        /// <param name="projectile"></param>
        public static void AddProjectile(Projectile projectile)
        {
            projectile.GetComponent<PhysicsComponent>().CollisionCircle.OldPartitionIndex =
                projectile.GetCenterPartition(); 
            //World.World.Instance.AddDynamicEntity(projectile);
            projectiles.Add(projectile);
            Console.WriteLine("Added projectile: " + projectile.GetType());
            Debug.Log($"Projectiles: {projectiles.Count}", Debug.LogType.Info, 3);
        }

        /// <summary>
        /// updates all of the projectiles in the world that are set to active
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].active)
                {
                    projectiles[i].Update(gameTime);  
                    //projectiles[i].UpdateComponents(gameTime);
                    //projectiles[i].GetComponent<PhysicsComponent>().UpdateComponent();
                    int p0 = PhysicsWorld.Instance.ColliderPartition.PositionToIndex(projectiles[i].GetComponent<PhysicsComponent>().CollisionCircle);
                    var p1 = p0 + 150;
                    var p2 = p0 - 150;
                    var p3 = p0 + 151;
                    var p4 = p0 - 151;
                    var p5 = p0 - 149;
                    var p6 = p0 + 149;
                    var p7 = p0 + 1;
                    var p8 = p0 - 1;

                    //Debug.Log($"{projectiles[i].GetComponent<PhysicsComponent>().CollisionCircle.Particle.Collider.PartitionIndex}  ---  {projectiles[i].GetComponent<PhysicsComponent>().CollisionCircle.Particle.Velocity}");
                    
                    if (PhysicsWorld.Instance.CellWithinBounds(p0)) PhysicsWorld.Instance.AddCellToUpdate(p0);
                    if (PhysicsWorld.Instance.CellWithinBounds(p1)) PhysicsWorld.Instance.AddCellToUpdate(p1);
                    if (PhysicsWorld.Instance.CellWithinBounds(p2)) PhysicsWorld.Instance.AddCellToUpdate(p2);
                    if (PhysicsWorld.Instance.CellWithinBounds(p3)) PhysicsWorld.Instance.AddCellToUpdate(p3);
                    if (PhysicsWorld.Instance.CellWithinBounds(p4)) PhysicsWorld.Instance.AddCellToUpdate(p4);
                    if (PhysicsWorld.Instance.CellWithinBounds(p5)) PhysicsWorld.Instance.AddCellToUpdate(p5);
                    if (PhysicsWorld.Instance.CellWithinBounds(p6)) PhysicsWorld.Instance.AddCellToUpdate(p6);
                    if (PhysicsWorld.Instance.CellWithinBounds(p7)) PhysicsWorld.Instance.AddCellToUpdate(p7);
                    if (PhysicsWorld.Instance.CellWithinBounds(p8)) PhysicsWorld.Instance.AddCellToUpdate(p8);
                }
                else
                {

                    PhysicsWorld.Instance.ColliderPartition.Remove(projectiles[i].GetComponent<PhysicsComponent>().CollisionCircle);
                    projectiles[i].DestroyEntity();
                    projectiles.RemoveAt(i);

                }
            }
        }

        /// <summary>
        /// draws all of the projectiles in the world that are set to active
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].active)
                {
                    spriteBatch.Draw(TextureManager.Sprites[0], projectiles[i].GetComponent<TransformComponent>().Position, Color.White);
                }
            }
        }
    }
}
