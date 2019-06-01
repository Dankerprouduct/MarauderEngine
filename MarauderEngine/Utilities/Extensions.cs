using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Utilities
{      
    
    /// <summary>
    /// Holds all extension methods.
    /// </summary>
    public static class Extensions
    {  
        
        /// <summary>
        /// Returns a random double within the given range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double NextDouble(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;

        }

        /// <summary>
        /// Returns a random float within the given range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float GetRandomFloatInRange(this Random random, float min, float max)
        {
            return (float)random.NextDouble() * (max - min) + min;

        }

        /// <summary>
        /// Randomly shuffles the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = MathHelper.RNG.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static Vector4  Power(this Vector4 vector, int power)
        {
            float x = (float)Math.Pow(vector.X, power);
            float y = (float)Math.Pow(vector.Y, power);
            float z = (float)Math.Pow(vector.Z, power);
            float w = (float)Math.Pow(vector.W, power);

            vector = new Vector4(x,y,z,w);
            return vector;
        }

    }
}
