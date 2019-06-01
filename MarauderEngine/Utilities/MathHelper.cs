using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;

namespace MarauderEngine.Utilities
{
    public static class 
        MathHelper
    {

        /// <summary>
        /// Global RNG created at runtime.
        /// </summary>
        public static Random RNG = new Random();

        /// <summary>
        /// Clamp the given value between the given min & max values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }
            else if (val.CompareTo(max) > 0)
            {
                return max; 
            }
            else
            {
                return val;
            }
        }

        /// <summary>
        /// Normalize the given value between a range min and max.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Normalize(float val, float min, float max)
        {
            var result = val;
            result = (val - min) / (max - min);
            return result;
        }

        /// <summary>
        /// Prevents anlges from resetting back to 0 after rotating to 360 degress
        /// now goes to 0 - > 360 instead of 0 -> 359 - > 360
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static float CurveAngle(float from, float to, float step)
        {
            if (step == 0)
            {
                return from;
            }

            if (from == to || step == 1) return to;

            Vector2 fromVector = new Vector2((float)Math.Cos(from), (float)Math.Sin(from));
            Vector2 toVector = new Vector2((float)Math.Cos(to), (float)Math.Sin(to));

            Vector2 currentVector = Slerp(fromVector, toVector, step);

            return (float)Math.Atan2(currentVector.Y, currentVector.X);
        }

        /// <summary>
        /// Interpolation for a vector between 0 and 1
        /// Spherical Linear Interpolation
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static Vector2 Slerp(Vector2 from, Vector2 to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            double theta = Math.Acos(Vector2.Dot(from, to));
            if (theta == 0) return to;

            double sinTheta = Math.Sin(theta);
            return (float)(Math.Sin((1 - step) * theta) / sinTheta) * from + (float)(Math.Sin(step * theta) / sinTheta) * to;
        }

        /// <summary>
        /// Returns an angle given two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float RotationFromVector2(Vector2 v1, Vector2 v2)
        {
            Vector2 direction = v1 - v2;
            direction.Normalize();

            float angle = (float)Math.Atan2(direction.Y, direction.X);
            return angle; 
        }

        /// <summary>
        /// Moves a vector from world space to entity space
        /// </summary>
        /// <param name="offSet">Entity Space</param>
        /// <param name="position">World Space (center of entity in world space)</param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Vector2 EntitySpaceVec2(Vector2 offSet, Vector2 position, float rotation)
        {

            Matrix rotationMatrix = Matrix.CreateRotationZ(rotation);

            Vector2 _pos = offSet;

            // REMEMBER THAT THE POSITON = CENTER
            return _pos = (position) + Vector2.Transform(_pos, rotationMatrix);


        }


        /// <summary>
        /// truncates vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="max"></param>
        public static void Truncate(Vector2 vector, float max)
        {
            if (vector.Length() > max)
            {
                vector.Normalize();
                vector *= max;
            }

        }


        /// <summary>
        /// returns the center of an image
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Vector2 CenterOfImage(Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2); 
        }

        /// <summary>
        /// returns the vector2 of a rotation
        /// </summary>
        /// <param name="rotation">rotation in radians</param>
        /// <returns></returns>
        public static Vector2 RotationToVector2(float rotation)
        {
            return  new Vector2((float)Math.Cos(rotation),(float)Math.Sin(rotation));
        }

        /// <summary>
        /// Min (inclusive), Max (inclusive).
        /// </summary>
        public struct IntRange
        {

            public int Min;
            public int Max;
            public float Weight;

            public IntRange(int min, int max, float weight)
            {
                Min = min;
                Max = max;
                Weight = weight;
            }

        }
        /// <summary>
        /// Min (inclusive), Max (inclusive).
        /// </summary>
        public struct FloatRange
        {
            public float Min;
            public float Max;
            public float Weight;


            public FloatRange(float min, float max, float weight)
            {
                Min = min;
                Max = max;
                Weight = weight;
            }
        }


        /// <summary>
        /// Collection of functions that allow 
        /// </summary>
        public static class RandomRange
        {

            /// <summary>
            /// Returns a weighted random integer based on input parameters.
            /// </summary>
            /// <param name="ranges"></param>
            /// <returns></returns>
            public static int WeightedRange(params IntRange[] ranges)
            {
                if (ranges.Length == 0) throw new System.ArgumentException("At least one range must be included.");
                if (ranges.Length == 1) return RNG.Next(ranges[0].Max, ranges[0].Min);

                float total = 0f;
                for (int i = 0; i < ranges.Length; i++) total += ranges[i].Weight;

                float r = RNG.Next();
                float s = 0f;

                int cnt = ranges.Length - 1;
                for (int i = 0; i < cnt; i++)
                {
                    s += ranges[i].Weight / total;
                    if (s >= r)
                    {
                        return RNG.Next(ranges[i].Max, ranges[i].Min);
                    }
                }

                return RNG.Next(ranges[cnt].Max, ranges[cnt].Min);
            }

            /// <summary>
            /// Returns a weighted random float based on input parameters.
            /// </summary>
            /// <param name="ranges"></param>
            /// <returns></returns>
            public static float WeightedRange(params FloatRange[] ranges)
            {
                if (ranges.Length == 0) throw new System.ArgumentException("At least one range must be included.");
                if (ranges.Length == 1) return RNG.GetRandomFloatInRange(ranges[0].Max, ranges[0].Min);

                float total = 0f;
                for (int i = 0; i < ranges.Length; i++) total += ranges[i].Weight;

                float r = RNG.Next();
                float s = 0f;

                int cnt = ranges.Length - 1;
                for (int i = 0; i < cnt; i++)
                {
                    s += ranges[i].Weight / total;
                    if (s >= r)
                    {
                        return RNG.GetRandomFloatInRange(ranges[i].Max, ranges[i].Min);
                    }
                }
                
                return RNG.GetRandomFloatInRange(ranges[cnt].Max, ranges[cnt].Min);
            }
            
        }

        #region perlin noise
        /// <summary>
        /// Generates perlin noise given a base white noise
        /// </summary>
        /// <param name="baseNoise"></param>
        /// <param name="OctaveCount"></param>
        /// <returns></returns>
        public static float[,] GeneratePerlinNoise(float[,] baseNoise, int OctaveCount)
        {
            int width = baseNoise.GetLength(0);
            int height = baseNoise.GetLength(1);

            float[][,] smoothNoise = new float[OctaveCount][,];


            // reg value = .5f
            float persistence = .5f;

            for (int i = 0; i < OctaveCount; i++)
            {
                smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);

            }

            float[,] perlinNoise = new float[width, height];
            float amplitude = .5f;
            float totalAmp = 0.0f;


            for (int octave = OctaveCount - 1; octave > 0; octave--)
            {
                amplitude *= persistence;
                totalAmp += amplitude;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        perlinNoise[x, y] += smoothNoise[octave][x, y] * amplitude;
                    }
                }
            }

            // normalization 
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    perlinNoise[x, y] /= totalAmp;
                    //perlinNoise[x, y] = makeMask(width, height, x, y, perlinNoise[x, y]);
                }
            }


            return perlinNoise;
        }

        /// <summary>
        /// Generates white noise, random values between 0 - 1
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static float[,] WhiteNoise(int width, int height, int seed)
        {
            Random random = new Random(seed);
            float[,] noise = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    noise[x, y] = (float)random.NextDouble() % 1;
                }
            }
            return noise;
        }

        /// <summary>
        /// Interpolates values in an array
        /// </summary>
        /// <param name="baseNoise"></param>
        /// <param name="octave"></param>
        /// <returns></returns>
        public static float[,] GenerateSmoothNoise(float[,] baseNoise, int octave)
        {


            int width = baseNoise.GetLength(0);
            int height = baseNoise.GetLength(1);

            float[,] smoothNoise = new float[width, height];

            int samplePeriod = 1 << octave;

            float sampleFrequency = 1.0f / samplePeriod;

            for (int x = 0; x < width; x++)
            {
                int sample1 = (x / samplePeriod) * samplePeriod;
                int sample2 = (sample1 + samplePeriod) % width;
                float horizonalBlend = (x - sample1) * sampleFrequency;

                for (int y = 0; y < height; y++)
                {
                    int sampley1 = (y / samplePeriod) * samplePeriod;
                    int sampley2 = (sampley1 + samplePeriod) % height;
                    float verticleBlend = (y - sampley1) * sampleFrequency;


                    float top = Interpolate(baseNoise[sample1, sampley1], baseNoise[sample2, sampley1], horizonalBlend);

                    float bottotm = Interpolate(baseNoise[sample1, sampley2], baseNoise[sample2, sampley2], horizonalBlend);

                    smoothNoise[x, y] = Interpolate(top, bottotm, verticleBlend);
                }
            }
            return smoothNoise;
        }
        
        /// <summary>
        /// Interpolates two values
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static float Interpolate(float x0, float x1, float alpha)
        {
            return x0 * (1 - alpha) + alpha * x1;
        }

        /// <summary>
        /// Makes a circular mask
        /// Great for making islands or weird lakes
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static float makeMask(int width, int height, int posX, int posY, float oldValue)
        {

            int minVal = (((height + width) / 2) / 100 * 2);
            int maxVal = (((height + width) / 2) / 100 * 25);
            if (getDistanceToEdge(posX, posY, width, height) <= minVal)
            {
                return 0;
            }
            else if (getDistanceToEdge(posX, posY, width, height) >= maxVal)
            {
                return oldValue;
            }
            else
            {
                float factor = getFactor(getDistanceToEdge(posX, posY, width, height), minVal, maxVal);
                return oldValue * factor;
            }
        }

        /// <summary>
        /// Helper function for makeMask
        /// </summary>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static float getFactor(int val, int min, int max)
        {
            int full = max - min;
            int part = val - min;
            float factor = (float)part / (float)full;
            return factor;
        }

        /// <summary>
        /// returns the distance to the edge of a map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int getDistanceToEdge(int x, int y, int width, int height)
        {
            int[] distances = new int[] { y, x, (width - x), (height - y) };
            int min = distances[0];
            foreach (var val in distances)
            {
                if (val < min)
                {
                    min = val;
                }
            }
            return min;
        }
        #endregion


    }
}
