using System;
using System.Collections;

namespace ColorVisualisation.Model.Helper.Generator
{
    class BitArrayGenerator
    {
        public static BitArray GenerateBitArray(int arrayLenght, int onesCount)
        {
            var generator = new Random();
            var bitArray = new BitArray(arrayLenght);
            for (int i = 0; i < onesCount; i++)
            {
                bool bitSet = false;
                while (!bitSet)
                {
                    var index = generator.Next(0, arrayLenght - 1);
                    if (bitArray[index] == false)
                    {
                        bitArray[index] = true;
                        bitSet = true;
                    }
                }
            }
            return bitArray;
        }
    }
}
