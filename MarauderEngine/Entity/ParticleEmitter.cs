using System.Collections.Generic;
using MarauderEngine.Components;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Entity
{
    public class ParticleEmitter : MarauderEngine.Entity.Entity
    {
        public bool active; 
        public int intensity;
        private List<Particle> particles = new List<Particle>();

        public ParticleEmitter(int intensity): base()
        {
            this.intensity = intensity;
            Components.Add<TransformComponent>(new TransformComponent(this));
            //this.active = true; 
        }

        public ParticleEmitter(Vector2 position, int intensity): base()
        {
            this.intensity = intensity;
            Components.Add<TransformComponent>(new TransformComponent(this));
            //this.active = true; 
        }

        public void Toggle()
        {
            active = !active; 
        }

        public override void Update(GameTime gameTime)
        {
            if (active)
            {
                for (int i = 0; i < intensity; i++)
                {
                    
                    int index = Game1.random.Next(0, particles.Count);

                    ParticleSystem.AddParticle(GetComponent<TransformComponent>().Position, particles[index]);
                }
            }
        }

        public void AddParticle(Particle particle)
        {
            particles.Add(particle); 
        }
    }
}
