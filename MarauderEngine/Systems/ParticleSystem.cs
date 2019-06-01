using System.Collections.Generic;
using MarauderEngine.Core;
using MarauderEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathHelper = MarauderEngine.Utilities.MathHelper;

namespace MarauderEngine.Systems
{
    public static class ParticleSystem
    {
        public static Particle[] particles;
        private static List<int> deadParticles;
        private static List<int> activeParticles;
        
        static int poolSize;
        public static int currentParticles;


        public static void Init(int _poolSize)
        {
            poolSize = _poolSize;
            particles = new Particle[poolSize]; 
            deadParticles = new List<int>();
            activeParticles = new List<int>();
            for (int i = 0; i < poolSize; i++)
            {
                particles[i] = new Particle(i); 
                deadParticles.Add(i);
            }

        }

        [System.Obsolete()]
        public static void AddParticle(Vector2 position, float mass, float force, int id, Color color)
        {

            for(int i = 0; i < particles.Length; i++)
            {
                if (!particles[i].alive)
                {
                    //Console.WriteLine("adding particle " + i); 
                    particles[i].CreateParticle(position, mass, Game1.random.Next((int)force, (int)force * 2), 1, id, color);
                    return; 
                }
            }
        }

        public static void AddParticle(Vector2 position, float mass, float force, string id, Color color)
        {

            for (int i = 0; i < particles.Length; i++)
            {
                if (!particles[i].alive)
                {
                    //Console.WriteLine("adding particle " + i); 
                    particles[i].CreateParticle(position, mass, Game1.random.Next((int)force, (int)force * 2), 1, id, color);
                    return;
                }
            }
        }

        /// <summary>
        /// dead particles
        /// </summary>
        /// <param name="i"></param>
        public static void AddToOpenList(int i)
        {
            deadParticles.Add(i);
            
        }
        
        public static void AddParticle(Vector2 position, Particle particle)
        {
            if (deadParticles.Count > 0)
            {
                for (int i = 0; i < deadParticles.Count; i++)
                {
                    if (!particles[deadParticles[i]].alive)
                    {

                        particles[deadParticles[i]].size = particle.size;
                        particles[deadParticles[i]].maxSize = particle.maxSize;
                        particles[deadParticles[i]].minSize = particle.minSize;
                        particles[deadParticles[i]].turnAngle = particle.turnAngle;
                        particles[deadParticles[i]].rotation = particle.rotation;
                        particles[deadParticles[i]].sizeRate = particle.sizeRate;
                        particles[deadParticles[i]].damping = particle.damping;
                        particles[deadParticles[i]].minDampening = particle.minDampening;
                        particles[deadParticles[i]].maxDampening = particle.maxDampening;
                        particles[deadParticles[i]].color = particle.color;
                        particles[deadParticles[i]].fadeRate = particle.fadeRate;
                        particles[deadParticles[i]].minAngle = particle.minAngle;
                        particles[deadParticles[i]].maxAngle = particle.maxAngle;


                        particles[deadParticles[i]].CreateParticle(position, particle.mass,
                            Game1.random.Next((int)particle.force, (int)particle.force * 5), 1, particle.particleTexture,
                            particle.color);
                        deadParticles.RemoveAt(i);
                        
                        return;
                    }
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            currentParticles = 0; 
            for(int i = 0; i < particles.Length; i++)
            {
                if (particles[i].alive)
                {
                    particles[i].Update(gameTime);
                    currentParticles++; 
                }
            }

            if (currentParticles >= particles.Length)
            {
                Debug.Log("HIT PARTICLE POOL SIZE MAX!", Debug.LogType.Error);
            }
                        
        } 

        public static void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].alive)
                {
                    spriteBatch.Draw(TextureManager.GetContent<Texture2D>(particles[i].particleTexture),
                        particles[i].position, null,
                        particles[i].color * particles[i].fade,
                        particles[i].rotation,
                        MathHelper.CenterOfImage(TextureManager.GetContent<Texture2D>(particles[i].particleTexture)),
                        particles[i].size,
                        SpriteEffects.None,
                        0f);

                    if (particles[i].particleTexture != null)
                    {
                        
                    }
                }
            }
        }

    }
}
