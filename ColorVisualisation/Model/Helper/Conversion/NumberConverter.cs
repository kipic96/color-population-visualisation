using System;
using System.Collections;

namespace ColorVisualisation.Model.Helper.Conversion
{
    class NumberConverter
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
            return new BitArray(new int[] { (byte)number });
        }

        public static int ToInt(BitArray bitArray)
        {
            if (bitArray.Length > 32)
                throw new ArgumentException();
            var array = new int[1];
            
            bitArray.CopyTo(array, 0);
            return array[0];
        }

        public static BitArray Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
            return array;
        }
    }
}
