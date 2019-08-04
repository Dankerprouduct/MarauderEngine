using System;
using MarauderEngine.Physics.Core.Shapes;
using Microsoft.Xna.Framework;
using SharpMath2;

namespace MarauderEngine.Physics.Core
{

    public class Particle
    {

        public Vector2 InitialPosition;
        private Vector2 _position; 
        public Vector2 OldPosition;
        private Vector2 baseVelocity = Vector2.Zero; 
        public Vector2 Velocity;
        public Vector2 OldVelocity;
        public Vector2 RelativeVelocity; 
        public Vector2 Acceleration;
        public Vector2 ForceAccumulator;
        public Vector2 Direction; 
        public float Dampening = 0.95f;
        public float Mass = 1;
        public float InvertedMass;
        public float Restitution;
        public float MaxSpeed = 0;
        public float Rotation = 0; 


        public ICollider Collider;
        public bool ActiveParticle;

        Particle parent;
        Vector2 parentOffset;

        public Vector2 Position
        {
            get
            {
                //return parent != null ? parent.Position + _position : Position;
                if(parent != null)
                {
                    return parent.Position - parentOffset;
                }
                else
                {
                    return _position; 
                }
            }
            set
            {
                _position = parent != null ? value - parent.Position : value; 
            }
        }

        public Particle(Vector2 position, float mass)
        {
            InitialPosition = position; 
            Position = position;
            OldPosition = position; 
            Mass = mass;
            InvertedMass = 1 / mass;
            if (mass == 0)
            {
                InvertedMass = 0;
            }
        }

        public Particle(Vector2 position, Vector2 velocity, float mass): this(position, mass)
        {
            Velocity = velocity; 
        }

        public void SetParent(Particle particle)
        {
            parent = particle;
            parentOffset = parent.Position - _position; 
        }

        public void SetMass(float mass)
        {
            Mass = mass;
        }

        public void SetInvertedMass(float invertedMass)
        {
            InvertedMass = invertedMass; 
        }

        public void Update(GameTime gameTime)
        {
            if (InvertedMass <= 0)
            {
                return; 
            }
            Integrate(gameTime);

        }
        
        public void Move(Vector2 position)
        {
            OldPosition = Position;
            Position = position;
            Velocity = OldPosition - position; 
        }
        
        public void SetBaseVelocity(Vector2 b_velocity)
        {
            
        }

        void CheckForCollision()
        {
            ICollider other;
            PhysicsWorld.Instance.CollidesWithCollider(Collider, out other, Collider.Layer);
        }

        void Integrate(GameTime gameTime)
        {
            // updates linear position         
            
            if (gameTime.ElapsedGameTime.Milliseconds > 0)
            {
                // direction


                if (parent != null)
                {
                    RelativeVelocity = parent.Velocity - Velocity;
                    AddForce(RelativeVelocity); 
                }

                // updates acceleration 
                Vector2 resultingAcc = Acceleration;
                resultingAcc += ForceAccumulator * 1;//(float) (1 / gameTime.ElapsedGameTime.Milliseconds));
                

                Velocity += resultingAcc * 1;
                //Console.WriteLine($"Vel {resultingAcc} Force Acum {ForceAccumulator}");

                Velocity += PhysicsWorld.Instance.Gravity;

                Velocity *= Dampening;
                
                Direction = new Vector2(Velocity.X, Velocity.Y);
                Rotation = (float)Math.Atan2(Direction.Y, Direction.X);

                if (MaxSpeed != 0)
                {
                    if (Velocity.Length() >= MaxSpeed)
                    {
                        Velocity.Normalize();
                        Velocity *= MaxSpeed;

                    }
                }

                ClearAccumulator();

                OldPosition = Position;
                OldVelocity = Velocity;

                if (!ActiveParticle)
                {
                    _position.X += Velocity.X;
                    _position.Y += Velocity.Y;
                }
                else
                {

                    ICollider other;
                    _position.X += (float) Velocity.X + (int) baseVelocity.X;

                    if (PhysicsWorld.Instance.CollidesWithCollider(Collider, out other, Collider.Layer))
                    {

                        _position.X = OldPosition.X;

                        //Vector2 depth = (PhysicsWorld.Instance.GetIntersectionDepth(Collider, other));
                        //Console.WriteLine(depth);
                        //_position.X += depth.X;

                        Tuple<Vector2, float> mtv = Polygon2.IntersectMTV(Collider.PhysicsCollider, other.PhysicsCollider, Position, other.Particle.Position);
                        if (mtv != null)
                        {
                            _position.X += mtv.Item1.Y * mtv.Item2;
                        }

                        if (other.Particle.InvertedMass != 0)
                        {
                            //other.Particle._position.X -= depth.X;
                        }
                        //Velocity.X = -Velocity.X * j;

                        Vector2 relativeVelocity = other.Particle.Velocity - Velocity;
                        Vector2 normal = new Vector2(-relativeVelocity.Y, relativeVelocity.X);
                        normal.Normalize();
                        //normal *= -1;

                        float velAlongNormal = Vector2.Dot(relativeVelocity, normal);
                        float totalInMass = InvertedMass + other.Particle.InvertedMass;

                        Vector2 impulse = relativeVelocity / totalInMass;

                        //Velocity.X = -Velocity.X * 1.2f;
                        Velocity.X += (impulse * InvertedMass).X ;
                        other.Particle.Velocity.X -= (impulse * other.Particle.InvertedMass).X;

                        // Events

                    }

                    _position.Y += (float)Velocity.Y + (int)baseVelocity.Y;
                    if (PhysicsWorld.Instance.CollidesWithCollider(Collider, out other, Collider.Layer))
                    {
                        _position.Y = OldPosition.Y;

                        //Vector2 depth = (PhysicsWorld.Instance.GetIntersectionDepth(Collider, other) / 1);
                        //_position.Y += depth.Y;

                        Tuple<Vector2, float> mtv = Polygon2.IntersectMTV(Collider.PhysicsCollider, other.PhysicsCollider, Position, other.Particle.Position);
                        if (mtv != null)
                        {
                            _position.Y += mtv.Item1.Y * mtv.Item2;
                        }

                        if (other.Particle.InvertedMass != 0)
                        {
                            if (mtv != null)
                            {

                            }
                            //other.Particle._position.Y -= depth.Y;
                        }

                        Vector2 relativeVelocity = other.Particle.Velocity - Velocity;
                        Vector2 normal = new Vector2(-relativeVelocity.Y, relativeVelocity.X);
                        normal.Normalize();
                        //normal *= -1; 

                        float velAlongNormal = Vector2.Dot(relativeVelocity, normal);
                        float totalInMass = InvertedMass + other.Particle.InvertedMass;

                        Vector2 impulse = relativeVelocity / totalInMass;

                        //Velocity.Y = -Velocity.Y * 1.2f;
                        Velocity.Y += (impulse * InvertedMass).Y;
                        other.Particle.Velocity.Y -= (impulse * other.Particle.InvertedMass).Y;

                        // Events
                        
                    }
                    
                }
                

            }


        }

        /// <summary>
        /// sets current position to old position
        /// </summary>
        public void RevertPosition()
        {
            Position = OldPosition;
        }
        
        public void AddForce(Vector2 force)
        {
            ForceAccumulator += force;
        }

        void ClearAccumulator()
        {
            ForceAccumulator = Vector2.Zero;
        }
    }
}
