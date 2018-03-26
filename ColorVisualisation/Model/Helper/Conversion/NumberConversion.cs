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
    }
}
