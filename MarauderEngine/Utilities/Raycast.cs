using System;
using System.Collections.Generic;
using MarauderEngine.Components;
using MarauderEngine.Core;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Utilities
{
    [Obsolete("Use Physics.Core.Raycast", true)]
    public class Raycast
    {

        public List<Point> points;
        Point rayP1;

        /// <summary>
        /// the point at which the ray hit
        /// </summary>
        public Point hit;
        // Origin of raycast in world space.
        Vector2 originPosition;
        // Target of raycast in world space.
        Vector2 targetPosition;

        // Angle in radians at which ray is pointed.
        float angle;

        public Event rayEvent = new Event();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Raycast()
        {

            rayEvent.id = "RayHit";
            rayEvent.parameters.Add("Ray", new object());
        }

        /// <summary>
        /// Initializes Ray
        /// </summary>
        /// <param name="rayOrigin">start point </param>
        /// <param name="rayTarget">end point </param>
        public Raycast(Vector2 rayOrigin, Vector2 rayTarget)
        {
            originPosition = rayOrigin;
            targetPosition = rayTarget;

            Vector2 direction = targetPosition - originPosition;
            direction.Normalize();

            angle = (float)Math.Atan2(direction.Y, direction.X);
            angle = Microsoft.Xna.Framework.MathHelper.ToDegrees(angle);
        }


        /// <summary>
        /// Creates the ray
        /// </summary>
        /// <param name="entity">entity making the ray</param>
        /// <param name="distance">max distance</param>
        /// <param name="step">incremental checks</param>
        /// <returns></returns>
        public bool MakeRay(MarauderEngine.Entity.Entity entity, int distance, int step = 5)
        {

            points = new List<Point>(); 
            float cosX = (float)(Math.Cos(Microsoft.Xna.Framework.MathHelper.ToRadians(angle)));
            float cosY = (float)(Math.Sin(Microsoft.Xna.Framework.MathHelper.ToRadians(angle)));

            for (int i = 0; i < distance; i += step)
            {
                int x = (int)(cosX * i) + (int)entity.GetComponent<TransformComponent>().Position.X; 
                int y = (int)(cosY * i) + (int)entity.GetComponent<TransformComponent>().Position.Y; 

                Point rayPoint = new Point(x, y);
                                
                rayP1 = rayPoint;

                rayEvent.parameters["Ray"] = rayPoint; 

                points.Add(rayPoint);
                //Game1.world.FireGlobal()
                if (SceneManagement.CurrentScene.FireGlobalEvent(rayEvent, entity))
                {
                    //Console.WriteLine("hit " + rayPoint  + " "+ Game1.world.FireGlobalEvent(rayEvent, entity));
                    hit = rayPoint;
                    return true;
                    
                }

                
            }
            //Console.WriteLine("didnt hit " + false); 
            return false; 
        }

        /// <summary>
        /// Creates a Ray
        /// </summary>
        /// <param name="entity">>the entity that sends the "Ray" event</param>
        /// <param name="distance">the distance that the ray travels from</param>
        /// <param name="angle">the angle of the ray</param>
        /// <param name="step">the step that the ray takes/param>
        /// <returns></returns>
        public bool MakeRay(MarauderEngine.Entity.Entity entity, int distance, float angle, int step = 5)
        {

            points = new List<Point>();
            float cosX = (float)(Math.Cos(angle));
            float cosY = (float)(Math.Sin(angle));

            for (int i = 0; i < distance; i += step)
            {
                int x = (int)(cosX * i) + (int)entity.GetComponent<TransformComponent>().Position.X;
                int y = (int)(cosY * i) + (int)entity.GetComponent<TransformComponent>().Position.Y;

                Point rayPoint = new Point(x, y);
                //Console.WriteLine(rayPoint);
                rayP1 = rayPoint;
                rayEvent.parameters["Ray"] = rayPoint;

                points.Add(rayPoint);
                //Game1.world.FireGlobal()
                if (SceneManagement.CurrentScene.FireGlobalEvent(rayEvent, entity))
                {
                    //Console.WriteLine("hit " + rayPoint  + " "+ Game1.world.FireGlobalEvent(rayEvent, entity));
                    hit = rayPoint;
                    return true;

                }


            }
            //Console.WriteLine("didnt hit " + false); 
            return false;
        }

        /// <summary>
        /// Creates a ray
        /// </summary>
        /// <param name="entity">the entity that sends the "Ray" event</param>
        /// <param name="position">the position that the ray extends from </param>
        /// <param name="distance">the distance that the ray travels from</param>
        /// <param name="angle">the angle of the ray</param>
        /// <param name="step">the step that the ray takes</param>
        /// <returns></returns>
        public bool MakeRay(MarauderEngine.Entity.Entity entity, Vector2 position, int distance, float angle, int step = 5)
        {

            points = new List<Point>();
            float cosX = (float)(Math.Cos(angle));
            float cosY = (float)(Math.Sin(angle));

            for (int i = 0; i < distance; i += step)
            {
                int x = (int)(cosX * i) + (int)position.X;
                int y = (int)(cosY * i) + (int)position.Y;

                Point rayPoint = new Point(x, y);
                //Console.WriteLine(rayPoint);
                rayP1 = rayPoint;
                rayEvent.parameters["Ray"] = rayPoint;

                points.Add(rayPoint);
                //Game1.world.FireGlobal()
                if (SceneManagement.CurrentScene.FireGlobalEvent(rayEvent, entity))
                {
                    //Console.WriteLine("hit " + rayPoint  + " "+ Game1.world.FireGlobalEvent(rayEvent, entity));
                    hit = rayPoint;
                    return true;

                }


            }
            //Console.WriteLine("didnt hit " + false); 
            return false;
        }


    }
}
