using System;

namespace ColorVisualisation.Model.Extension
{
    static class RandomExtension
    {
        public static byte NextByte(this Random byteGenerator)
        {
            return (byte)byteGenerator.Next(byte.MinValue, byte.MaxValue);
        }
    }
}
