using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
