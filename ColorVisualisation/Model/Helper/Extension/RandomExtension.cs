using System;

namespace ColorVisualisation.Model.Helper.Extension
{
    static class RandomExtension
    {
        public static byte NextByte(this Random byteGenerator)
        {
            return (byte)byteGenerator.Next(byte.MinValue, byte.MaxValue);
        }
    }
}
