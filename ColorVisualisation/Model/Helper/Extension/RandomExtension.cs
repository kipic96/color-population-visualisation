using System;

namespace ColorVisualisation.Model.Helper.Extension
{
    static class RandomExtension
    {
        public static byte NextByte(this Random byteGenerator)
        {
            return (byte)byteGenerator.Next(byte.MinValue, byte.MaxValue);
        }

        public static bool WillEventHappen(this Random generator, double probability)
        {
            if (probability > 1 || probability < 0)
            {
                throw new ArgumentException("Probability must be from range <0,1>");
            }
            return (probability >= generator.NextDouble());
        }
    }
}
