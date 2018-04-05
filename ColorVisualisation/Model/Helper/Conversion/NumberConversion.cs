using System;
using System.Collections;

namespace ColorVisualisation.Model.Helper.Conversion
{
    class NumberConversion
    {
        public static int ToEvenNumber(int number)
        {
            if (!IsEvenNumber(number))
                number++;
            return number;
        }

        private static bool IsEvenNumber(int number)
        {
            return (number % 2 == 0);
        }

        public static BitArray ToBitArray(int number)
        {
            return new BitArray(new int[] { number });
        }

        public static int ToInt(BitArray bitArray)
        {
            if (bitArray.Length > 32)
                throw new ArgumentException();
            var array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }
    }
}
