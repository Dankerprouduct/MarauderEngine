using System;

namespace MarauderEngine.Audio
{
    
    public delegate float OscillatorDelegate(float frequency, float time, float amplitude);

    public static class Oscillator
    {
        public static float Sine(float frequency, float time, float amplitude)
        {
            return (float)Math.Sin(frequency * time * 2 * Math.PI) * amplitude;
        }

        public static float Square(float frequency, float time, float amplitude)
        {
            return Sine(frequency, time, amplitude) >= 0 ? 1.0f * amplitude: -1.0f * amplitude;
        }

        public static float Sawtooth(float frequency, float time, float amplitude)
        {
            return (float)(2 * (time * frequency - Math.Floor(time * frequency + 0.5))) * amplitude;
        }

        public static float Triangle(float frequency, float time, float amplitude)
        {
            return Math.Abs(Sawtooth(frequency, time , amplitude)) * 2.0f - 1.0f * amplitude;
        }

        public static float Noise(float frequency, float time, float amplitude)
        {
            return (float)(Game1.random.NextDouble() - Game1.random.NextDouble()) * amplitude;
        }

        public static float Pulse(float frequency, float time, float amplitude)
        {
            double period = 1.0 / frequency;
            double timeModulusPeriod = time - Math.Floor(time / period) * period;
            double position = timeModulusPeriod / period;
            if (position <= .9f)
                return  amplitude;
            else
                return -amplitude;
        }
    }
}
