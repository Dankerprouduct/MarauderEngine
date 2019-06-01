using System;
using MarauderEngine.Core;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Systems
{
    public class Particle
    {
        private int particleIndex; 
        public Vector2 position;
        public Vector2 velocity;
        public float mass;
        public float rotation;
        public float turnAngle; 
        public float force;
        public Color color; 

        public bool alive;
        int timeCounter; 
        int timeLimit;
        public int id = -1; 
        public string particleTexture;

        public float size = 1;
        public float maxSize = 1;
        public float minSize = 1;
        public float sizeRate = 1;
        public float maxDampening = 99; 
        public float minDampening = 95;
        public float fade = 1;
        public float fadeRate = .995f;
        public float minAngle = 0;
        public float maxAngle = 360;
        public float minSpeed = .0001f; 
        float initialRotation; 
        public float damping;

        public Particle(int index)
        {
            particleIndex = index;
            alive = false;
        }

        public Particle()
        {
            alive = false; 
        }
        
        [System.Obsolete]
        public Particle(Vector2 position, float mass, float rotation, float force, int lifeSpan, int id, Color color)
        {
            this.alive = true; 
            this.position = position;
            this.mass = mass;
            this.rotation = rotation;
            this.force = force; 
            AddForce(rotation, force);

            timeCounter = 0;
            timeLimit = lifeSpan;
            this.id = id;
        
            this.color = color; 
        }

        public Particle(Vector2 position, float mass, float rotation, float force, int lifeSpan, string id, Color color)
        {
            this.alive = true;
            this.position = position;
            this.mass = mass;
            this.rotation = rotation;
            this.force = force;
            AddForce(rotation, force);

            timeCounter = 0;
            timeLimit = lifeSpan;
            this.particleTexture = id;

            this.color = color;
        }

        [System.Obsolete()]
        public void CreateParticle(Vector2 position, float mass, float force, int lifeSpan, int id, Color color)
        {
            
            this.alive = true;
            this.position = position;
            this.mass = mass;

            this.rotation = MathHelper.ToRadians(Game1.random.Next((int)minAngle, (int)maxAngle));
            //Console.WriteLine(minAngle + " " + maxAngle +" " + rotation);
            AddForce(MathHelper.ToDegrees( rotation), force);
            initialRotation = rotation;
            timeCounter = 0;
            timeLimit = lifeSpan;
            this.id = id;
            
            fade = 1; 
            this.color = color; 
        }
        public void CreateParticle(Vector2 position, float mass, float force, int lifeSpan, string id, Color color)
        {

            this.alive = true;
            this.position = position;
            this.mass = mass;

            this.rotation = MathHelper.ToRadians(Game1.random.Next((int)minAngle, (int)maxAngle));
            //Console.WriteLine(minAngle + " " + maxAngle +" " + rotation);
            AddForce(MathHelper.ToDegrees(rotation), force);
            initialRotation = rotation;
            timeCounter = 0;
            timeLimit = lifeSpan;
            this.particleTexture = id;

            fade = 1;
            this.color = color;
        }


        void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {

            Timer(gameTime);

            size *= sizeRate;
            //Console.WriteLine(sizeRate);
            fade *= fadeRate;
            //color *= fade; 
            rotation += 0;//MathHelper.ToRadians(turnAngle);
            
            velocity *= damping;
            position += velocity; 
            //AddForce(rotation, velocity); 

        }
            
        void Timer(GameTime gameTime)
        {
            //timeCounter += (int)gameTime.ElapsedGameTime.TotalSeconds;

            //Console.WriteLine(velocity.Length());
            //if(fad)
            if (Math.Abs( velocity.Length()) <= minSpeed|| size > maxSize || size < minSize || fade <= 0.05f)
            {
                alive = false;
                //Debug.Log("Particle Removed", Debug.LogType.Warning);
                ParticleSystem.AddToOpenList(particleIndex);
                //Console.WriteLine("I am dead"); 
            }
        }

        public void AddForce(float rotation, float force)
        {
            velocity.X += ((float)Math.Cos(MathHelper.ToRadians(rotation)) * force) / mass;
            velocity.Y += ((float)Math.Sin(MathHelper.ToRadians(rotation)) * force) / mass;
            //position += velocity;
        }

        public void AddForce(float rotation, Vector2 velocity)
        {
            velocity.X += ((float)Math.Cos(MathHelper.ToRadians(initialRotation)) * velocity.LengthSquared()) / mass;
            velocity.Y += ((float)Math.Sin(MathHelper.ToRadians(initialRotation)) * velocity.LengthSquared()) / mass;
            position += velocity; 
        }


    }
}
