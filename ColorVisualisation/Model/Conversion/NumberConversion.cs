using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVisualisation.Model.Conversion
{
    class NumberConversion
    {
        public int ToEvenNumber(int number)
        {
            if (!IsEvenNumber(number))
                number++;
            return number;
        }

        private bool IsEvenNumber(int number)
        {
            return (number % 2 == 0);
        }
    }
}
